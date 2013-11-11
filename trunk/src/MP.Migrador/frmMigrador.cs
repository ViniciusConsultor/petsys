using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.Documento;
using Compartilhados.Interfaces.Core.Negocio.Telefone;
using Compartilhados.Interfaces.Core.Servicos;
using Core.Interfaces.Servicos;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Microsoft.VisualBasic;

namespace MP.Migrador
{
    public partial class frmMigrador : Form
    {
        public frmMigrador()
        {
            InitializeComponent();
        }

        private IDictionary<string, IGrupoDeAtividade> gruposDeAtividades;
        private IDictionary<string, IMunicipio> municipios;
        private IDictionary<string, ITipoDeEndereco> tiposDeEndereco;
        private IDictionary<string, IDespachoDeMarcas> despachoDeMarcas;

        private IDictionary<string, IPessoa> pessoasMigradas = new Dictionary<string, IPessoa>(); 
        private IDictionary<string, IMarcas> marcasMigradas = new Dictionary<string, IMarcas>();
        private IDictionary<string, string> grupoDeAtividadeDoContato = new Dictionary<string, string>();
        private HashSet<string> idsDeClientesCadastros = new HashSet<string>();  
        private HashSet<string> idsDeProduradoresCadastrados = new HashSet<string>();
        private IDictionary<string, IList<IRadicalMarcas>> radicaisComChaveLegada = new Dictionary<string, IList<IRadicalMarcas>>();
        
        private void btnMigrar_Click(object sender, EventArgs e)
        {
            CarregueRadicais();
            MigrePessoas();
            MigreMarcas();
            MigreProcessoDeMarca();

            MessageBox.Show("Dados migrados com sucesso!");
        }

        private void CarregueRadicais()
        {
             DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSolureg = new OleDbConnection(txtStringDeConexaoSolureg.Text))
            {
                conexaoSolureg.Open();

                var sql =  "select idmarca, descricao_radical, codigo_ncl from marcas_radical " + 
                           "left join ncl_radical on ncl_radical.idmarcas_radical = marcas_radical.idmarcas_radical " +
                           "left join ncl on ncl.idncl = ncl_radical.idncl";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoSolureg))
                    data.Fill(dataSetDadosLegados);

                conexaoSolureg.Close();
            }

              var dados = dataSetDadosLegados.Tables[0];
            
              foreach (DataRow linha in dados.Rows)
              {

                  if (!radicaisComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "idmarca")) )
                    radicaisComChaveLegada.Add(UtilidadesDePersistencia.GetValor(linha, "idmarca"), new List<IRadicalMarcas>());
                    
                  var radical = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalMarcas>();
                  radical.DescricaoRadical = UtilidadesDePersistencia.GetValor(linha, "descricao_radical").Trim();

                 if (!Information.IsDBNull(linha["codigo_ncl"]))
                 {
                     var codigoNCL = UtilidadesDePersistencia.GetValor(linha, "codigo_ncl").Trim();

                     if (codigoNCL.Length == 1) codigoNCL = "0" + codigoNCL;

                     radical.NCL = NCL.ObtenhaPorCodigo(codigoNCL);

                     radicaisComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "idmarca")].Add(radical);
                 }
              }
        }

        private void MigreProcessoDeMarca()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSolureg = new OleDbConnection(txtStringDeConexaoSolureg.Text))
            {
                conexaoSolureg.Open();

                var sql =
                    "select numero_processo, DATA_PROCESSO, PROCESSO_DE_TERCEIRO, IDCONTATO_PROCURADOR, IDDESPACHO_ATUAL, " +
                    "CODIGO_DESPACHO, idmarca " +
                    "from processo " +
                    "left join DESPACHO on IDDESPACHO = IDDESPACHO_ATUAL";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoSolureg))
                    data.Fill(dataSetDadosLegados);

                conexaoSolureg.Close();
            }

              var dados = dataSetDadosLegados.Tables[0];
            
              foreach (DataRow linha in dados.Rows)
              {
                  var processo = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDeMarca>();
                  
                  if (!Information.IsDBNull(linha["DATA_PROCESSO"]))
                  {
                      var data = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "DATA_PROCESSO"));

                    processo.DataDoCadastro = data;
                    processo.DataDoDeposito = data;
                  }
                  else
                    processo.DataDoCadastro = DateTime.Now;
                  
                  if (!Information.IsDBNull(linha["CODIGO_DESPACHO"]))
                  {
                      var codigoDoDespacho = UtilidadesDePersistencia.GetValor(linha, "CODIGO_DESPACHO");

                      if (codigoDoDespacho.Length ==1 ) codigoDoDespacho = "00" + codigoDoDespacho;

                      if (codigoDoDespacho.Length ==2 ) codigoDoDespacho = "0" + codigoDoDespacho;

                      processo.Despacho = despachoDeMarcas[codigoDoDespacho];

                  }

                  if (!Information.IsDBNull(linha["numero_processo"]))
                      processo.Processo = UtilidadesDePersistencia.GetValorLong(linha, "numero_processo");
                  else
                      processo.Processo = -1;

                  processo.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(linha, "PROCESSO_DE_TERCEIRO");

                  if (!Information.IsDBNull(linha["IDCONTATO_PROCURADOR"]))
                      processo.Procurador = CadastreProcurador(pessoasMigradas[UtilidadesDePersistencia.GetValor(linha, "IDCONTATO_PROCURADOR")]);

                  processo.Marca = marcasMigradas[UtilidadesDePersistencia.GetValor(linha, "idmarca")];

                  using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                      servico.Inserir(processo);
              }
        }

        private IProcurador CadastreProcurador(IPessoa pessoa)
        {
            var procurador = FabricaGenerica.GetInstancia().CrieObjeto<IProcurador>(new object[] { pessoa });
            
            if (!idsDeProduradoresCadastrados.Contains(pessoa.ID.Value.ToString()))
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
                    servico.Inserir(procurador);

                idsDeProduradoresCadastrados.Add(pessoa.ID.Value.ToString());
            }

            return procurador;
        }

        private void MigreMarcas()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSolureg = new OleDbConnection(txtStringDeConexaoSolureg.Text))
            {
                conexaoSolureg.Open();

                var sql = "select idmarca, codigo_ncl, descricao_apres, descricao_nat, idcontato, " +
	                      "descricao_marca, especificacao_prod_serv, imagem_marca, observacao_marca, idclasse, " +
	                      "idclasse_item1, idclasse_item2, idclasse_item3 "+
                          "from marcas " +
                          "inner join ncl on marcas.idncl = ncl.idncl " + 
                          "inner join apresentacao on marcas.idapresentacao = apresentacao.idapresentacao " + 
                          "inner join natureza on marcas.idnatureza = natureza.idnatureza";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoSolureg))
                    data.Fill(dataSetDadosLegados);

                conexaoSolureg.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            
            foreach (DataRow linha in dados.Rows)
            {
                var marca = FabricaGenerica.GetInstancia().CrieObjeto<IMarcas>();
                IGrupoDeAtividade grupoDeAtividade = null;

                if (radicaisComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "idmarca")))
                    marca.AdicioneRadicaisMarcas(radicaisComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "idmarca")]);

                if (grupoDeAtividadeDoContato.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "idcontato")))
                {
                    var descricaoDoGrupo = grupoDeAtividadeDoContato[UtilidadesDePersistencia.GetValor(linha, "idcontato")].ToUpper().Trim();

                    if (descricaoDoGrupo.Equals("COMERCIO")) descricaoDoGrupo = "COMÉRCIO";

                    if (descricaoDoGrupo.Equals("MEDICO E FARMACÊUTICO")) descricaoDoGrupo = "MÉDICO E FARMACÊUTICO";
                    grupoDeAtividade = gruposDeAtividades[descricaoDoGrupo];
                }

                marca.Cliente = CadastreCliente(pessoasMigradas[UtilidadesDePersistencia.GetValor(linha, "idcontato")], grupoDeAtividade);
                marca.Apresentacao = Apresentacao.ObtenhaPorNome(UtilidadesDePersistencia.GetValor(linha, "descricao_apres").Trim());

                if (!Information.IsDBNull(linha["descricao_marca"]))
                    marca.DescricaoDaMarca = UtilidadesDePersistencia.GetValor(linha, "descricao_marca").Trim();

                if (!Information.IsDBNull(linha["especificacao_prod_serv"]))
                    marca.EspecificacaoDeProdutosEServicos = UtilidadesDePersistencia.GetValor(linha, "especificacao_prod_serv").Trim();

                if (!Information.IsDBNull(linha["imagem_marca"]))
                    marca.ImagemDaMarca = UtilidadesDePersistencia.GetValor(linha, "imagem_marca");

                var codigoNcl = UtilidadesDePersistencia.GetValor(linha, "codigo_ncl");

                if (codigoNcl.Length == 1) codigoNcl = "0" + codigoNcl;

                marca.NCL = NCL.ObtenhaPorCodigo(codigoNcl);

                if (!Information.IsDBNull(linha["idclasse"]))
                    marca.CodigoDaClasse = UtilidadesDePersistencia.getValorInteger(linha, "idclasse");

                if (!Information.IsDBNull(linha["idclasse_item1"]))
                    marca.CodigoDaSubClasse1 = UtilidadesDePersistencia.getValorInteger(linha, "idclasse_item1");

                if (!Information.IsDBNull(linha["idclasse_item2"]))
                    marca.CodigoDaSubClasse2 = UtilidadesDePersistencia.getValorInteger(linha, "idclasse_item2");

                if (!Information.IsDBNull(linha["idclasse_item3"]))
                    marca.CodigoDaSubClasse3 = UtilidadesDePersistencia.getValorInteger(linha, "idclasse_item3");

                if (!Information.IsDBNull(linha["observacao_marca"]))
                    marca.ObservacaoDaMarca = UtilidadesDePersistencia.GetValor(linha, "observacao_marca");

                marca.Natureza =
                    NaturezaDeMarca.ObtenhaPorNome(UtilidadesDePersistencia.GetValor(linha, "descricao_nat").Trim());

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeMarcas>())
                {
                    servico.Inserir(marca);
                    marcasMigradas.Add  (UtilidadesDePersistencia.GetValor(linha,"idmarca"), marca);
                }
            }
        }

        private ICliente CadastreCliente(IPessoa pessoa, IGrupoDeAtividade grupoDeAtividade )
        {

            var cliente = FabricaGenerica.GetInstancia().CrieObjeto<ICliente>(new object[] {pessoa});

            cliente.GrupoDeAtividade = grupoDeAtividade;

            cliente.DataDoCadastro = DateTime.Now;
            

            if (!idsDeClientesCadastros.Contains(pessoa.ID.Value.ToString()))
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeCliente>())
                    servico.Inserir(cliente);

                idsDeClientesCadastros.Add(pessoa.ID.Value.ToString());
            }
            

            return cliente;
        }

        private void MigrePessoas()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSolureg = new OleDbConnection(txtStringDeConexaoSolureg.Text))
            {
                conexaoSolureg.Open();

                var sql = "select contato.idcontato, cnpj_cpf, contato.nome_contato, " +
                        "nome_fantasia, dt_nasc_abertura, sg_cartorio_junta_orgao, insc_estadual_ci, " +
                        "nr_reg_cart_junta_org, dt_reg_junta_cart_org, objetivo_social, email, dominio, " +
                        "observacao_contato, contato.idgrupo_atividade, grupo_atividade.descricao desricaogrupoatividade, idtelefone, num_telefone," +
                        "telefone.nome_contato contatotelefone, idendereco, logradouro, numero,bairro, complemento, cidade, endereco.idestado," +
                        "cep, idtipo_endereco, sg_estado,  estado.descricao descricaoestado, endereco.idpais, sigla_pais," +
                        "paises.descricao descricaopais, tipo_endereco.descricao descricaotipoendereco " +
                        "from contato  " +
                          "left join grupo_atividade on grupo_atividade.idgrupo_atividade =  contato.idgrupo_atividade " +
                          "left join telefone on  telefone.idcontato = contato.idcontato " +
                          "left join endereco on endereco.idcontato = contato.idcontato " +
                          "left join estado on estado.idestado =  endereco.idestado " +
                          "left join paises on paises.idpais =  endereco.idpais " +
                          "left join tipo_endereco on tipo_endereco.idtipo_endereo = endereco.idtipo_endereco " +
                          "order by contato.idcontato";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoSolureg))
                    data.Fill(dataSetDadosLegados);

                conexaoSolureg.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            var idContatoAnterior = 0;
            IPessoa pessoa = null;
            var pessoas = new List<IPessoa>();

            foreach (DataRow linha in dados.Rows)
            {
                if (!Information.IsDBNull(linha["CNPJ_CPF"]))
                {
                    //Pessoa juridica
                    if (UtilidadesDePersistencia.GetValor(linha, "CNPJ_CPF").Trim().Length > 11)
                    {
                        if (idContatoAnterior != UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO"))
                        {
                            pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaJuridica>();
                            pessoas.Add(pessoa);
                            idContatoAnterior = UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO");
                            pessoasMigradas.Add(idContatoAnterior.ToString(),pessoa);
                        }

                        MontaPessoaJuridica(linha, ref pessoa);
                        continue;
                    }

                    if (idContatoAnterior != UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO"))
                    {
                        pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                        pessoas.Add(pessoa);
                        idContatoAnterior = UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO");
                        pessoasMigradas.Add(idContatoAnterior.ToString(), pessoa);
                    }

                    MontaPessoaFisica(linha, ref pessoa);
                    continue;
                }
                //se o cpf ou cnpj não estiver cadastrado verificamos o nome fantasia, caso o mesmo não exista é pessoa fisica
                if (!Information.IsDBNull(linha["NOME_FANTASIA"]))
                {
                    if (idContatoAnterior != UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO"))
                    {
                        pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaJuridica>();
                        pessoas.Add(pessoa);
                        idContatoAnterior = UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO");
                        pessoasMigradas.Add(idContatoAnterior.ToString(), pessoa);
                    }

                    MontaPessoaJuridica(linha, ref pessoa);
                    continue;
                }

                if (idContatoAnterior != UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO"))
                {
                    pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                    pessoas.Add(pessoa);
                    idContatoAnterior = UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO");
                    pessoasMigradas.Add(idContatoAnterior.ToString(), pessoa);
                }

                MontaPessoaFisica(linha, ref pessoa);
            }

            foreach (var pessoa1 in pessoas)
            {
                if (pessoa1.Tipo.Equals(TipoDePessoa.Fisica))
                {
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                        servico.Inserir((IPessoaFisica)pessoa1);

                    continue;
                }

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaJuridica>())
                    servico.Inserir((IPessoaJuridica)pessoa1);

            }
        }

        private void MontaPessoaJuridica(DataRow linha, ref IPessoa pessoa)
        {
            MontaPessoa(linha, ref pessoa);

            if (!Information.IsDBNull(linha["NOME_FANTASIA"]))
                ((IPessoaJuridica)pessoa).NomeFantasia = UtilidadesDePersistencia.GetValor(linha, "NOME_FANTASIA").Trim();

            if (!Information.IsDBNull(linha["CNPJ_CPF"]))
            {
                pessoa.AdicioneDocumento(FabricaGenerica.GetInstancia().CrieObjeto<ICNPJ>(new object[] { UtilidadesDePersistencia.GetValor(linha, "CNPJ_CPF").Trim() }));
            }
        }

        private void MontaPessoa(DataRow linha, ref IPessoa pessoa)
        {

            if (!Information.IsDBNull(linha["desricaogrupoatividade"]))
                if (!grupoDeAtividadeDoContato.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "IDCONTATO")))
                    grupoDeAtividadeDoContato.Add(UtilidadesDePersistencia.GetValor(linha, "IDCONTATO"), UtilidadesDePersistencia.GetValor(linha, "desricaogrupoatividade"));

            pessoa.Nome = UtilidadesDePersistencia.GetValor(linha, "NOME_CONTATO").Trim();

            if (!Information.IsDBNull(linha["EMAIL"]))
                pessoa.EnderecoDeEmail = UtilidadesDePersistencia.GetValor(linha, "EMAIL").Trim();

            if (!Information.IsDBNull(linha["DOMINIO"]))
                pessoa.Site = UtilidadesDePersistencia.GetValor(linha, "DOMINIO").Trim();


            if (!Information.IsDBNull(linha["LOGRADOURO"]))
            {
                var endereco = FabricaGenerica.GetInstancia().CrieObjeto<IEndereco>();

                endereco.Logradouro = UtilidadesDePersistencia.GetValor(linha, "LOGRADOURO").Trim();

                if (!Information.IsDBNull(linha["BAIRRO"]))
                    endereco.Bairro = UtilidadesDePersistencia.GetValor(linha, "BAIRRO").Trim();

                var complemento = "";
                var numero = "S/N";

                if (!Information.IsDBNull(linha["COMPLEMENTO"]))
                    complemento = UtilidadesDePersistencia.GetValor(linha, "COMPLEMENTO").Trim();

                if (!Information.IsDBNull(linha["NUMERO"]))
                    numero = "NÚMERO " + UtilidadesDePersistencia.GetValor(linha, "NUMERO").Trim();

                endereco.Complemento = (complemento + " " + numero).Trim();

                if (!Information.IsDBNull(linha["CEP"]))
                    endereco.CEP = new CEP(UtilidadesDePersistencia.GetValorLong(linha, "CEP"));

                if (!Information.IsDBNull(linha["CIDADE"]))
                    endereco.Municipio = DescubraMunicipio(UtilidadesDePersistencia.GetValor(linha, "CIDADE"), UtilidadesDePersistencia.GetValor(linha, "SG_ESTADO"));


                if (!Information.IsDBNull(linha["descricaotipoendereco"]))
                    endereco.TipoDeEndereco = DescubraTipoDeEndereco(UtilidadesDePersistencia.GetValor(linha, "descricaotipoendereco"));
                else
                    endereco.TipoDeEndereco = DescubraTipoDeEndereco("PADRÃO");

                pessoa.AdicioneEndereco(endereco);
            }


            if (!Information.IsDBNull(linha["num_telefone"]))
            {
                var telefone = FabricaGenerica.GetInstancia().CrieObjeto<ITelefone>();

                var numero = ObtenhaApenasNumeros(UtilidadesDePersistencia.GetValor(linha, "num_telefone"));

                short DDD = 0;
                long Numero;

                if (numero.Length == 10)
                {
                    DDD = Convert.ToInt16((Strings.Mid(numero, 1, 2)));
                    Numero = Convert.ToInt64((Strings.Mid(numero, 3)));
                }
                else
                {
                    Numero = Convert.ToInt64(numero);
                }

                telefone.DDD = DDD;
                telefone.Numero = Numero;

                if (Numero.ToString().StartsWith("9"))
                    telefone.Tipo = TipoDeTelefone.Celular;
                else
                    telefone.Tipo = TipoDeTelefone.Comercial;

                pessoa.AdicioneTelefone(telefone);
            }
        }

        private void MontaPessoaFisica(DataRow linha, ref IPessoa pessoa)
        {
            MontaPessoa(linha, ref pessoa);

            if (!Information.IsDBNull(linha["CNPJ_CPF"]))
            {
                pessoa.AdicioneDocumento(FabricaGenerica.GetInstancia().CrieObjeto<ICPF>(new object[] { UtilidadesDePersistencia.GetValor(linha, "CNPJ_CPF").Trim() }));
            }

            ((IPessoaFisica)pessoa).EstadoCivil = EstadoCivil.Ignorado;
            ((IPessoaFisica)pessoa).Nacionalidade = Nacionalidade.Brasileira;
            ((IPessoaFisica)pessoa).Sexo = Sexo.Masculino;

        }

        private void frmMigrador_Load(object sender, EventArgs e)
        {
            txtStringDeConexaoSolureg.Text =
                @"Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SoluReg;Data Source=.\sqlexpress";

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConexao>())
            {
                var conexao = servico.ObtenhaConexao();
                FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao = conexao;
                FabricaDeContexto.GetInstancia().GetContextoAtual().EmpresaLogada = new EmpresaVisivel(15644,"");
            }

            CarregueGruposDeAtividade();
            CarregueMunicipios();
            CarregueTiposDeEndereco();
            CarregueDespachosDeMarcas();
        }

        private void CarregueGruposDeAtividade()
        {

            try
            {
                gruposDeAtividades = new Dictionary<string, IGrupoDeAtividade>();

                var conexao = FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao;

                DataSet dataSet = new DataSet();

                using (var conexaoPadrao = new OleDbConnection("Provider=SQLOLEDB.1;" + conexao.StringDeConexao))
                {
                    conexaoPadrao.Open();

                    var sql = "SELECT * FROM NCL_GRUPO_DE_ATIVIDADE ";

                    using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoPadrao))
                        data.Fill(dataSet);

                    conexaoPadrao.Close();
                }

                var dados = dataSet.Tables[0];

                foreach (DataRow linha in dados.Rows)
                {
                    var grupoDeAtividade = FabricaGenerica.GetInstancia().CrieObjeto<IGrupoDeAtividade>();

                    grupoDeAtividade.ID = UtilidadesDePersistencia.GetValorLong(linha, "ID");
                    grupoDeAtividade.Nome = UtilidadesDePersistencia.GetValor(linha, "NOME");

                    gruposDeAtividades.Add(grupoDeAtividade.Nome, grupoDeAtividade);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void CarregueMunicipios()
        {
            municipios = new Dictionary<string, IMunicipio>();

            var conexao = FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao;

            DataSet dataSet = new DataSet();

            using (var conexaoPadrao = new OleDbConnection("Provider=SQLOLEDB.1;" + conexao.StringDeConexao))
            {
                conexaoPadrao.Open();

                var sql = "SELECT * FROM NCL_MUNICIPIO ";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoPadrao))
                    data.Fill(dataSet);

                conexaoPadrao.Close();
            }

            var dados = dataSet.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var municipio = FabricaGenerica.GetInstancia().CrieObjeto<IMunicipio>();

                municipio.ID = UtilidadesDePersistencia.GetValorLong(linha, "ID");
                municipio.Nome = UtilidadesDePersistencia.GetValor(linha, "NOME");
                municipio.UF = UF.Obtenha(UtilidadesDePersistencia.getValorShort(linha, "UF"));

                municipios.Add(municipio.Nome + "|" + municipio.UF.Sigla, municipio);
            }
        }

        private void CarregueTiposDeEndereco()
        {
            tiposDeEndereco = new Dictionary<string, ITipoDeEndereco>();

            var conexao = FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao;

            DataSet dataSet = new DataSet();

            using (var conexaoPadrao = new OleDbConnection("Provider=SQLOLEDB.1;" + conexao.StringDeConexao))
            {
                conexaoPadrao.Open();

                var sql = "select * from ncl_tipo_endereco ";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoPadrao))
                    data.Fill(dataSet);

                conexaoPadrao.Close();
            }

            var dados = dataSet.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var tipo = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDeEndereco>();

                tipo.ID = UtilidadesDePersistencia.GetValorLong(linha, "ID");
                tipo.Nome = UtilidadesDePersistencia.GetValor(linha, "NOME");

                tiposDeEndereco.Add(tipo.Nome, tipo);
            }
        }

        private void CarregueDespachosDeMarcas()
        {
            despachoDeMarcas = new Dictionary<string, IDespachoDeMarcas>();

            var conexao = FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao;

            DataSet dataSet = new DataSet();

            using (var conexaoPadrao = new OleDbConnection("Provider=SQLOLEDB.1;" + conexao.StringDeConexao))
            {
                conexaoPadrao.Open();

                var sql = "select * from mp_despacho_marca ";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoPadrao))
                    data.Fill(dataSet);

                conexaoPadrao.Close();
            }

            var dados = dataSet.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var despacho = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();

                despacho.IdDespacho = UtilidadesDePersistencia.GetValorLong(linha, "IDDESPACHO");
                despacho.CodigoDespacho = UtilidadesDePersistencia.GetValor(linha, "CODIGO_DESPACHO");

                despachoDeMarcas.Add(despacho.CodigoDespacho, despacho);
            }
        }

        private ITipoDeEndereco DescubraTipoDeEndereco(string nomeDoTipo)
        {
            return tiposDeEndereco[nomeDoTipo];
        }


        private IMunicipio DescubraMunicipio(string nomeMunicipio, string uf)
        {
            string MunicipioSTR = nomeMunicipio.Trim().ToUpper();

            if (string.IsNullOrEmpty(MunicipioSTR)) return null;

            if (MunicipioSTR.Equals("GOIANIA") || MunicipioSTR.Equals("GOANIA") || MunicipioSTR.Equals("GOIÃNIA") || MunicipioSTR.Equals("GOIÂNIA GO"))
                MunicipioSTR = "GOIÂNIA";

            if (MunicipioSTR.Equals("ANAPOLIS") || MunicipioSTR.Equals("ANAPOLIES") || MunicipioSTR.Equals("ANAPÓLIS"))
                MunicipioSTR = "ANÁPOLIS";

            if (MunicipioSTR.Equals("BALNEARIO CAMBORIU"))
                MunicipioSTR = "BALNEÁRIO CAMBORIÚ";

            if (MunicipioSTR.Equals("ED. BRITÂNIA"))
                MunicipioSTR = "BRITÂNIA";

            if (MunicipioSTR.Equals("GOIANAPOLIS"))
                MunicipioSTR = "GOIANÁPOLIS";

            if (MunicipioSTR.Contains("NIQUELANDIA"))
                MunicipioSTR = "NIQUELÂNDIA";

            if (MunicipioSTR.Contains("ACRÉUNA"))
                MunicipioSTR = "ACREÚNA";

            if (MunicipioSTR.Contains("ALEXANIA"))
                MunicipioSTR = "ALEXÂNIA";

            if (MunicipioSTR.Equals("AP. DE GOIÂNIA") || MunicipioSTR.Equals("APARECIDA DE GOIANIA") ||
                MunicipioSTR.Equals("APARECIDA DE GOIÃNIA") || MunicipioSTR.Equals("APARECIDA DE GÕIANIA"))
                MunicipioSTR = "APARECIDA DE GOIÂNIA";

            if (MunicipioSTR.Contains("ARAGOIANIA"))
                MunicipioSTR = "ARAGOIÂNIA";

            if (MunicipioSTR.Contains("BRASILEIA") || MunicipioSTR.Contains("BRASILIA"))
                MunicipioSTR = "BRASÍLIA";

            if (MunicipioSTR.Contains("CAÇÚ"))
                MunicipioSTR = "CAÇU";

            if (MunicipioSTR.Contains("CATURAI"))
                MunicipioSTR = "CATURAÍ";

            if (MunicipioSTR.Contains("COCALZINHO DE GOIAS"))
                MunicipioSTR = "COCALZINHO DE GOIÁS";

            if (MunicipioSTR.Contains("EDEIA"))
                MunicipioSTR = "EDÉIA";

            if (MunicipioSTR.Contains("STO. ANTÔNIO DE GOIÁS"))
                MunicipioSTR = "SANTO ANTÔNIO DE GOIÁS";

            if (MunicipioSTR.Contains("UBERLÂNDIA"))
                uf = "MG";

            if (MunicipioSTR.Contains("MONTIVIDÍU") || MunicipioSTR.Contains("MONTIVÍDIU"))
                MunicipioSTR = "MONTIVIDIU";


            if (MunicipioSTR.Contains("GOIANIRA"))
                MunicipioSTR = "GOIANDIRA";

            if (MunicipioSTR.Contains("TEREZOPÓLIS DE GOIÁS"))
                MunicipioSTR = "TEREZÓPOLIS DE GOIÁS";

            if (MunicipioSTR.Contains("PIRENOPÓLIS") || MunicipioSTR.Contains("PIRENOPOLIS"))
                MunicipioSTR = "PIRENÓPOLIS";

            if (MunicipioSTR.Contains("PLANALTINA DE GOIÁS"))
                MunicipioSTR = "PLANALTINA";

            if (MunicipioSTR.Contains("POV. DE CRUZLÂNDIA"))
                return null;

            if (MunicipioSTR.Contains("SILVANIA"))
                MunicipioSTR = "SILVÂNIA";

            if (MunicipioSTR.Contains("SÃO MIGUEL DO PASSA QUATRO"))
                return null;

            if (MunicipioSTR.Contains("IBIRUBA"))
                return null;

            if (MunicipioSTR.Contains("NEROPÓLIS") || MunicipioSTR.Contains("NEROPOLIS"))
                MunicipioSTR = "NERÓPOLIS";

            if (MunicipioSTR.Contains("MOZARLANDIA"))
                MunicipioSTR = "MOZARLÂNDIA";

            if (MunicipioSTR.Contains("SAO PAULO"))
                MunicipioSTR = "SÃO PAULO";

            if (MunicipioSTR.Contains("BELA VISTA DE GOIAS"))
                MunicipioSTR = "BELA VISTA DE GOIÁS";

            if (MunicipioSTR.Contains("HEITORAI"))
                MunicipioSTR = "HEITORAÍ";


            if (MunicipioSTR.Contains("JARAGUA"))
                MunicipioSTR = "JARAGUÁ";

            if (MunicipioSTR.Contains("PETROLINA DE GOIAS"))
                MunicipioSTR = "PETROLINA DE GOIÁS";

            if (MunicipioSTR.Contains("MONTES CLAROS DE GOIAS"))
                MunicipioSTR = "MONTES CLAROS DE GOIÁS";

            if (MunicipioSTR.Contains("CAMPOS BELOS"))
                uf = "GO";

            if (MunicipioSTR.Contains("PARAISO DO TOCANTINS"))
                MunicipioSTR = "PARAÍSO DO TOCANTINS";


            if (MunicipioSTR.Contains("VITORIA DAS MISSÕES"))
                MunicipioSTR = "VITÓRIA DAS MISSÕES";

            if (MunicipioSTR.Contains("JATAI"))
                MunicipioSTR = "JATAÍ";

            if (MunicipioSTR.Contains("NOVA IGUAÇU DE GOIAS") || MunicipioSTR.Contains("NOVA IGUAÇÚ DE GOIÁS"))
                MunicipioSTR = "NOVA IGUAÇU DE GOIÁS";

            if (MunicipioSTR.Contains("BRASÍLIA"))
                uf = "DF";

            if (MunicipioSTR.Contains("CAXIAS DO SUL"))
                uf = "RS";

            if (MunicipioSTR.Contains("ITABERAI"))
                MunicipioSTR = "ITABERAÍ";

            if (MunicipioSTR.Contains("GUAÍRA"))
                return null;

            if (MunicipioSTR.Contains("SÃO GERALDO DO ARGUAIA"))
            {
                MunicipioSTR = "SÃO GERALDO DO ARAGUAIA";
                uf = "PA";
            }

            if (MunicipioSTR.Contains("MOSSORÓ"))
                return null;

            if (MunicipioSTR.Contains("HAYWARD")) return null;

            if (MunicipioSTR.Contains("RONDONOPOLIS"))
                MunicipioSTR = "RONDONÓPOLIS";

            if (MunicipioSTR.Contains("MATRICHÃ"))
                MunicipioSTR = "MATRINCHÃ";

            if (MunicipioSTR.Contains("MARINGÁ")) uf = "PR";

            return municipios[MunicipioSTR + "|" + uf];
        }

        private string ObtenhaApenasNumeros(string NumeroStrDesformatado )
        {
            var NumeroAux = new StringBuilder();

            foreach (var Caracter in NumeroStrDesformatado.Where(Caracter => Char.IsNumber(Caracter)))
                NumeroAux.Append(Caracter);

            return Convert.ToInt64(NumeroAux.ToString()).ToString();
        }


        private DateTime ObtenhaData(string dataStr)
        {
            var datanumero = ObtenhaApenasNumeros(Strings.Mid(dataStr, 1, 10));

            if (datanumero.Length == 7)
                datanumero = "0" + datanumero;

            datanumero = Strings.Mid(datanumero, 1, 8);

            

            var ano = Convert.ToInt32(Strings.Mid(datanumero, 5, 4));
            var mes = Convert.ToInt32(Strings.Mid(datanumero, 3, 2));
            var dia = Convert.ToInt32(Strings.Mid(datanumero, 1, 2));
            return new DateTime(ano, mes, dia);
        }

    }
}
