using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
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
        private IDictionary<string, IDespachoDePatentes> despachoDePatente;
        private IDictionary<string, IPais> paises;
        private IDictionary<string, INaturezaPatente> naturezasPatentes;

        private IDictionary<string, IPessoa> pessoasMigradas = new Dictionary<string, IPessoa>();
        private IDictionary<string, IMarcas> marcasMigradas = new Dictionary<string, IMarcas>();
        private IDictionary<string, string> grupoDeAtividadeDoContato = new Dictionary<string, string>();
        private HashSet<string> idsDeClientesCadastros = new HashSet<string>();
        private HashSet<string> idsDeProduradoresCadastrados = new HashSet<string>();
        private HashSet<string> idsDeInventoresCadastrados = new HashSet<string>();
        private IDictionary<string, IList<IRadicalMarcas>> radicaisComChaveLegada = new Dictionary<string, IList<IRadicalMarcas>>();


        private IDictionary<string, IList<IClassificacaoPatente>> classificacoesDeDesenhoComChaveLegada = new Dictionary<string, IList<IClassificacaoPatente>>();
        private IDictionary<string, IList<IClassificacaoPatente>> classificacoesDePatenteComChaveLegada = new Dictionary<string, IList<IClassificacaoPatente>>();
        private IDictionary<string, IPasta> pastaMigradas = new Dictionary<string, IPasta>();
        private IDictionary<string, IList<IPrioridadeUnionistaPatente>> prioridadesUnionistaDePatenteComChaveLegada = new Dictionary<string, IList<IPrioridadeUnionistaPatente>>();
        private IDictionary<string, IList<IInventor>> inventoresDePatenteComChaveLegada = new Dictionary<string, IList<IInventor>>();
        private IDictionary<string, ICliente> clientesDePatenteComChaveLegada = new Dictionary<string, ICliente>();
        private IDictionary<string, IProcurador> advogadosDePatenteComChaveLegada = new Dictionary<string, IProcurador>();
        private IDictionary<string, IList<IInventor>> inventoresDeDesenhoComChaveLegada = new Dictionary<string, IList<IInventor>>();
        private IDictionary<string, ITitular> titularesDePatenteComChaveLegada = new Dictionary<string, ITitular>();

        private void btnMigrar_Click(object sender, EventArgs e)
        {
            CarregueRadicais();
            MigrePessoas();
            MigreMarcas();
            MigreProcessoDeMarca();

            if (chkMigraPatentes.Checked)
            {
                CarregueECadastreAdvogadosDaPatente();
                CarregueECadastrePastas();
                CarregueECadastreClientesDaPatente();
                CarregueECadastreInventoresDaPatente();

                CarregueClassificacaoDaPatente();
                CarreguePrioridadeUnionistaPatente();
                CarregueECadastreInventores();
                MigrePatentesEProcessosDePatentes();

                CarregueClassificacaoDeDesenho();
                CarregueECadastreInventoresPatenteDI();
                MigrePatentesDIEProcessosDePatentes();
            }

            MessageBox.Show("Dados migrados com sucesso!");
        }


        private void CarregueECadastreAdvogadosDaPatente()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select * from Advogados";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {

                IPessoa pessoa = null;


                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                {
                    var pessoas = servico.ObtenhaPessoasPorNomeComoFiltro(UtilidadesDePersistencia.GetValor(linha, "Nome_Advogado").Trim(), 1);

                    if (pessoas.Count == 0)
                    {
                        pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                        pessoa.Nome = UtilidadesDePersistencia.GetValor(linha, "Nome_Advogado").Trim();
                        ((IPessoaFisica)pessoa).EstadoCivil = EstadoCivil.Ignorado;
                        ((IPessoaFisica)pessoa).Sexo = Sexo.Masculino;
                        ((IPessoaFisica)pessoa).Nacionalidade = Nacionalidade.Brasileira;
                        servico.Inserir((IPessoaFisica)pessoa);
                    }
                    else
                    {
                        pessoa = pessoas[0];
                    }
                }


                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
                {
                    IProcurador procurador = servico.ObtenhaProcurador(pessoa);

                    if (procurador == null)
                    {
                        procurador = FabricaGenerica.GetInstancia().CrieObjeto<IProcurador>(new object[] { pessoa });
                        servico.Inserir(procurador);
                    }

                    advogadosDePatenteComChaveLegada.Add(UtilidadesDePersistencia.GetValor(linha, "Código_Advogado"), procurador);
                }
            }

        }

        private void CarregueECadastrePastas()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select  * from Pastas  ";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var pasta = FabricaGenerica.GetInstancia().CrieObjeto<IPasta>();

                pasta.Codigo = UtilidadesDePersistencia.GetValor(linha, "CodPasta").Trim();
                pasta.Nome = UtilidadesDePersistencia.GetValor(linha, "DescPasta").Trim();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePasta>())
                {
                    servico.Inserir(pasta);

                    pastaMigradas.Add(pasta.Codigo, pasta);
                }
            }
        }

        private IList<IAnuidadePatente> ObtenhaAnuidadesDaPatente(DataRow linha, IServicoDePatente servico, INaturezaPatente natureza)
        {
            var dataDoDeposito = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_depósito"));

            IList<IAnuidadePatente> anuidades;
            var nomenclaturaColuna = "DatPag";

            if (natureza.SiglaNatureza.Equals("DI"))
            {
                anuidades = servico.CalculeAnuidadesPatentesDeNaturezaDI(dataDoDeposito);
                nomenclaturaColuna = "";
            }
            else
                anuidades = servico.CalculeAnuidadesPatentesDeNaturezaPIeMU(dataDoDeposito);

            for (var i = 0; i < anuidades.Count; i++)
            {
                var indice = i + 3;

                if (!Information.IsDBNull(linha[nomenclaturaColuna + indice.ToString()]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, nomenclaturaColuna + indice.ToString())))
                {
                    anuidades[i].DataPagamento = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, nomenclaturaColuna + indice.ToString()));
                    anuidades[i].AnuidadePaga = true;
                }
                    
            }

            return anuidades;
        }

        private void MigrePatentesEProcessosDePatentes()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select * from Patentes ";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];


            using (var servicoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                using (var servicoDeProcessoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                {

                    foreach (DataRow linha in dados.Rows)
                    {
                        var patente = FabricaGenerica.GetInstancia().CrieObjeto<IPatente>();

                        var numeroDaPatente = UtilidadesDePersistencia.GetValor(linha, "Número_Patente").Trim();

                        patente.Clientes.Add(clientesDePatenteComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Código_Cliente")]);

                        if (titularesDePatenteComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "Código_Titular")))
                            patente.Titulares.Add(titularesDePatenteComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Código_Titular")]);

                        patente.DataCadastro = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_cadastro"));

                        if (inventoresDePatenteComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente").Trim()))
                            patente.Inventores = inventoresDePatenteComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente").Trim()];

                        patente.NaturezaPatente = naturezasPatentes[UtilidadesDePersistencia.GetValor(linha, "nat_patente")];

                        patente.Anuidades = ObtenhaAnuidadesDaPatente(linha, servicoDePatente, patente.NaturezaPatente);

                        if (classificacoesDePatenteComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente").Trim()))
                            patente.Classificacoes =
                                classificacoesDePatenteComChaveLegada[
                                    UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente").Trim()];

                        //Verificar isso com o Mauro
                        patente.ObrigacaoGerada = false;

                        if (!Information.IsDBNull(linha["observações"]))
                            patente.Observacao = UtilidadesDePersistencia.GetValor(linha, "observações");

                        if (prioridadesUnionistaDePatenteComChaveLegada.ContainsKey(numeroDaPatente))
                            patente.PrioridadesUnionista = prioridadesUnionistaDePatenteComChaveLegada[numeroDaPatente];

                        if (!Information.IsDBNull(linha["Reivindicações"]))
                            patente.QuantidadeReivindicacao = UtilidadesDePersistencia.getValorInteger(linha,
                                                                                                       "Reivindicações");

                        if (!Information.IsDBNull(linha["Resumo"]))
                            patente.Resumo = UtilidadesDePersistencia.GetValor(linha, "Resumo");

                        if (!Information.IsDBNull(linha["Título"]))
                            patente.TituloPatente = UtilidadesDePersistencia.GetValor(linha, "Título");

                        servicoDePatente.Insira(patente);

                        var processoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDePatente>();

                        processoDePatente.Ativo = !UtilidadesDePersistencia.GetValorBooleano(linha, "Processo_Desativo");

                        processoDePatente.DataDoCadastro = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_cadastro"));

                        processoDePatente.DataDoDeposito = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_depósito"));

                        if (!Information.IsDBNull(linha["data_concessão"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_concessão")))
                            processoDePatente.DataDaConcessao =
                                ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_concessão"));

                        if (!Information.IsDBNull(linha["data_vigência"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_vigência")))
                            processoDePatente.DataDaVigencia = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_vigência"));

                        if (!Information.IsDBNull(linha["data_publicação"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_publicação")))
                            processoDePatente.DataDaPublicacao = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_publicação"));

                        if (!Information.IsDBNull(linha["data_exame"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_exame")))
                            processoDePatente.DataDoExame = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_exame"));

                        processoDePatente.Patente = patente;
                        processoDePatente.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(linha, "patente_terceiro");
                        processoDePatente.ProcessoEhEstrangeiro = UtilidadesDePersistencia.GetValorBooleano(linha, "patente_estrangeira");
                        processoDePatente.Processo = numeroDaPatente;


                        if (!Information.IsDBNull(linha["Código_Advogado"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Código_Advogado")))
                            processoDePatente.Procurador = advogadosDePatenteComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Código_Advogado")];


                        if (!Information.IsDBNull(linha["pct"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "pct")))
                        {
                            var ehPCT = UtilidadesDePersistencia.GetValorBooleano(linha, "pct");


                            if (ehPCT)
                            {
                                var pct = FabricaGenerica.GetInstancia().CrieObjeto<IPCT>();

                                pct.Numero = UtilidadesDePersistencia.GetValor(linha, "num_pct");
                                pct.DataDoDeposito = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_depósito_pct"));

                                if (!string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "num_wo")))
                                    pct.NumeroWO = UtilidadesDePersistencia.GetValor(linha, "num_wo");

                                if (!string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_publicação_pct")))
                                    pct.DataDaPublicacao = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_publicação_pct"));

                                processoDePatente.PCT = pct;

                            }

                        }

                        if (!Information.IsDBNull(linha["codPasta"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "codPasta")))
                            processoDePatente.Pasta =
                                pastaMigradas[UtilidadesDePersistencia.GetValor(linha, "codPasta")];

                        if (!Information.IsDBNull(linha["despacho_RPI"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "despacho_RPI")))

                            if (despachoDePatente.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "despacho_RPI").Trim().ToUpper()))
                                processoDePatente.Despacho = despachoDePatente[UtilidadesDePersistencia.GetValor(linha, "despacho_RPI").Trim().ToUpper()];

                        servicoDeProcessoDePatente.Inserir(processoDePatente);




                    }
                }

            }

        }

        private void MigrePatentesDIEProcessosDePatentes()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select * from DesenhoIndustrial ";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];


            using (var servicoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                using (var servicoDeProcessoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                {

                    foreach (DataRow linha in dados.Rows)
                    {
                        var patente = FabricaGenerica.GetInstancia().CrieObjeto<IPatente>();

                        var numeroDaPatente = UtilidadesDePersistencia.GetValor(linha, "Número_Desenho").Trim();

                        patente.Clientes.Add(clientesDePatenteComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Código_Cliente")]);

                        if (titularesDePatenteComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "Código_Titular")))
                            patente.Titulares.Add(titularesDePatenteComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Código_Titular")]);

                        patente.DataCadastro = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_cadastro"));

                        if (inventoresDeDesenhoComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_DEsenho").Trim()))
                            patente.Inventores = inventoresDeDesenhoComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Nat_Número_DEsenho").Trim()];

                        patente.NaturezaPatente = naturezasPatentes[UtilidadesDePersistencia.GetValor(linha, "nat_desenho")];

                        //patente.Anuidades = ObtenhaAnuidadesDaPatente(linha, servicoDePatente, patente.NaturezaPatente);

                        if (classificacoesDeDesenhoComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_DEsenho").Trim()))
                            patente.Classificacoes =
                                classificacoesDeDesenhoComChaveLegada[
                                    UtilidadesDePersistencia.GetValor(linha, "Nat_Número_DEsenho").Trim()];

                        //Verificar isso com o Mauro
                        patente.ObrigacaoGerada = false;

                        if (!Information.IsDBNull(linha["observações"]))
                            patente.Observacao = UtilidadesDePersistencia.GetValor(linha, "observações");

                        if (prioridadesUnionistaDePatenteComChaveLegada.ContainsKey(numeroDaPatente))
                            patente.PrioridadesUnionista = prioridadesUnionistaDePatenteComChaveLegada[numeroDaPatente];

                        if (!Information.IsDBNull(linha["Reivindicações"]))
                            patente.QuantidadeReivindicacao = UtilidadesDePersistencia.getValorInteger(linha,
                                                                                                       "Reivindicações");

                        if (!Information.IsDBNull(linha["Resumo"]))
                            patente.Resumo = UtilidadesDePersistencia.GetValor(linha, "Resumo");

                        if (!Information.IsDBNull(linha["Título"]))
                            patente.TituloPatente = UtilidadesDePersistencia.GetValor(linha, "Título");

                        servicoDePatente.Insira(patente);

                        var processoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDePatente>();

                        processoDePatente.Ativo = !UtilidadesDePersistencia.GetValorBooleano(linha, "Processo_Desativo");

                        processoDePatente.DataDoCadastro = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_cadastro"));

                        if (!Information.IsDBNull(linha["data_depósito"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_depósito")))
                            processoDePatente.DataDoDeposito = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_depósito"));

                        if (!Information.IsDBNull(linha["data_concessão"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_concessão")))
                            processoDePatente.DataDaConcessao =
                                ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_concessão"));

                        if (!Information.IsDBNull(linha["data_vigência"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_vigência")))
                            processoDePatente.DataDaVigencia = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_vigência"));

                        if (!Information.IsDBNull(linha["data_publicação"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "data_publicação")))
                            processoDePatente.DataDaPublicacao = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_publicação"));
                        
                        processoDePatente.Patente = patente;
                        processoDePatente.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(linha, "desenho_terceiro");
                        processoDePatente.ProcessoEhEstrangeiro = false;
                        processoDePatente.Processo = numeroDaPatente;


                        if (!Information.IsDBNull(linha["Código_Advogado"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Código_Advogado")))
                            processoDePatente.Procurador = advogadosDePatenteComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Código_Advogado")];

                        
                        if (!Information.IsDBNull(linha["codPasta"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "codPasta")))
                            processoDePatente.Pasta =
                                pastaMigradas[UtilidadesDePersistencia.GetValor(linha, "codPasta")];

                        if (!Information.IsDBNull(linha["despacho_RPI"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "despacho_RPI")))

                            if (despachoDePatente.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "despacho_RPI").Trim().ToUpper()))
                                processoDePatente.Despacho = despachoDePatente[UtilidadesDePersistencia.GetValor(linha, "despacho_RPI").Trim().ToUpper()];

                        servicoDeProcessoDePatente.Inserir(processoDePatente);

                    }
                }

            }

        }

        private void AtualizePessoa(IPessoa pessoa, DataRow linha)
        {
            var temQAtualizar = false;

            if (pessoa.Enderecos ==null || pessoa.Enderecos.Count ==0)
            {
                var enderecos = MonteEndereco(linha);

                if (enderecos.Count != 0)
                    pessoa.AdicioneEnderecos(enderecos);

                temQAtualizar = true;
            }

            if (pessoa.Contatos() == null || pessoa.Contatos().Count == 0)
            {
                pessoa.AdicioneContatos(ObtenhaContatosDaPessoa(linha));
                temQAtualizar = true;
            }


            if (temQAtualizar) 
                if (pessoa.Tipo == TipoDePessoa.Fisica)
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                        servico.Modificar((IPessoaFisica) pessoa);
                else
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaJuridica>())
                        servico.Modificar((IPessoaJuridica)pessoa);

        }

        private void CarregueECadastreInventoresDaPatente()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select * from Clientes";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var ehPessoaJuridica = false;

                if (UtilidadesDePersistencia.GetValor(linha, "Código_Titular").Equals("0") || UtilidadesDePersistencia.GetValor(linha, "Código_Titular").Equals("3")) continue;

                if (!Information.IsDBNull(linha["cgc"]))
                    if (UtilidadesDePersistencia.GetValor(linha, "cgc").Trim().Length > 14)
                        ehPessoaJuridica = true;

                IPessoa pessoa = null;

                if (ehPessoaJuridica)
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaJuridica>())
                    {
                        var pessoas = servico.ObtenhaPessoasPorNomeComoFiltro(UtilidadesDePersistencia.GetValor(linha, "Nome_Cliente").Trim(), 1);

                        if (pessoas.Count == 0)
                        {
                            pessoa = MontePessoaJuridicaPatente(linha);
                            servico.Inserir((IPessoaJuridica)pessoa);
                        }
                        else
                        {
                            pessoa = pessoas[0];
                            AtualizePessoa(pessoa, linha);
                        }
                    }

                else
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                    {
                        var pessoas = servico.ObtenhaPessoasPorNomeComoFiltro(UtilidadesDePersistencia.GetValor(linha, "Nome_Cliente").Trim(), 1);

                        if (pessoas.Count == 0)
                        {
                            pessoa = MontePessoaFisicaPatente(linha);
                            servico.Inserir((IPessoaFisica)pessoa);
                        }
                        else
                        {
                            pessoa = pessoas[0];
                            AtualizePessoa(pessoa, linha);
                        }
                    }


                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTitular>())
                {
                    var titular = servico.Obtenha(pessoa);

                    if (titular == null)
                    {
                        titular = FabricaGenerica.GetInstancia().CrieObjeto<ITitular>(new object[] { pessoa });

                        if (!Information.IsDBNull(linha["Observações"]))
                            titular.InformacoesAdicionais = UtilidadesDePersistencia.GetValor(linha, "Observações").Trim();

                        titular.DataDoCadastro = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "Data_Cadastramento").Trim());

                        servico.Inserir(titular);
                    }

                    titularesDePatenteComChaveLegada.Add(UtilidadesDePersistencia.GetValor(linha, "Código_Titular"), titular);
                }
            }

        }

        private void CarregueECadastreClientesDaPatente()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select * from Clientes";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var ehPessoaJuridica = false;

                if (UtilidadesDePersistencia.GetValor(linha, "Código_Cliente").Equals("0")) continue;

                if (!Information.IsDBNull(linha["cgc"]))
                    if (UtilidadesDePersistencia.GetValor(linha, "cgc").Trim().Length > 14)
                        ehPessoaJuridica = true;

                IPessoa pessoa = null;

                if (ehPessoaJuridica)
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaJuridica>())
                    {
                        var pessoas = servico.ObtenhaPessoasPorNomeComoFiltro(UtilidadesDePersistencia.GetValor(linha, "Nome_Cliente").Trim(), 1);

                        if (pessoas.Count == 0)
                        {
                            pessoa = MontePessoaJuridicaPatente(linha);
                            servico.Inserir((IPessoaJuridica)pessoa);
                        }
                        else
                        {
                            pessoa = pessoas[0];
                            AtualizePessoa(pessoa,linha);
                        }
                    }

                else
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                    {
                        var pessoas = servico.ObtenhaPessoasPorNomeComoFiltro(UtilidadesDePersistencia.GetValor(linha, "Nome_Cliente").Trim(), 1);

                        if (pessoas.Count == 0)
                        {
                            pessoa = MontePessoaFisicaPatente(linha);
                            servico.Inserir((IPessoaFisica)pessoa);
                        }
                        else
                        {
                            pessoa = pessoas[0];
                            AtualizePessoa(pessoa, linha);
                        }
                    }


                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeCliente>())
                {
                    ICliente cliente = servico.Obtenha(pessoa);

                    if (cliente == null)
                    {
                        cliente = FabricaGenerica.GetInstancia().CrieObjeto<ICliente>(new object[] { pessoa });

                        if (!Information.IsDBNull(linha["Observações"]))
                            cliente.InformacoesAdicionais = UtilidadesDePersistencia.GetValor(linha, "Observações").Trim();

                        cliente.DataDoCadastro = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "Data_Cadastramento").Trim());

                        servico.Inserir(cliente);
                    }

                    clientesDePatenteComChaveLegada.Add(UtilidadesDePersistencia.GetValor(linha, "Código_Cliente"), cliente);
                }
            }

        }

        private IList<IEndereco> MonteEndereco(DataRow linha)
        {
            var enderecos = new List<IEndereco>();

            if (!Information.IsDBNull(linha["Ender_Sede"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Ender_Sede")))
            {
                var endereco = FabricaGenerica.GetInstancia().CrieObjeto<IEndereco>();

                endereco.Logradouro = UtilidadesDePersistencia.GetValor(linha, "Ender_Sede").Trim();

                if (!Information.IsDBNull(linha["Bairro_Sede"]) &&
                    !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Bairro_Sede")))
                    endereco.Bairro = UtilidadesDePersistencia.GetValor(linha, "Bairro_Sede").Trim();

                if (!Information.IsDBNull(linha["CEP_Sede"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "CEP_Sede")))
                    endereco.CEP = new CEP(Convert.ToInt64(ObtenhaApenasNumeros(UtilidadesDePersistencia.GetValor(linha, "CEP_Sede"))));

                if (!Information.IsDBNull(linha["Cidade_Sede"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Cidade_Sede")))
                    endereco.Municipio = DescubraMunicipio(UtilidadesDePersistencia.GetValor(linha, "Cidade_Sede"),
                                                           UtilidadesDePersistencia.GetValor(linha, "UF_sede"));
               
               endereco.TipoDeEndereco = DescubraTipoDeEndereco("PADRÃO");

                enderecos.Add(endereco);
            }

            if (!Information.IsDBNull(linha["Ender_Corresp"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Ender_Corresp")))
            {
                var endereco = FabricaGenerica.GetInstancia().CrieObjeto<IEndereco>();

                endereco.Logradouro = UtilidadesDePersistencia.GetValor(linha, "Ender_Corresp").Trim();

                if (!Information.IsDBNull(linha["Bairro_Corresp"]) &&
                    !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Bairro_Corresp")))
                    endereco.Bairro = UtilidadesDePersistencia.GetValor(linha, "Bairro_Corresp").Trim();

                if (!Information.IsDBNull(linha["CEP_Corresp"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "CEP_Corresp")))
                    endereco.CEP = new CEP(Convert.ToInt64(ObtenhaApenasNumeros(UtilidadesDePersistencia.GetValor(linha, "CEP_Corresp"))));

                if (!Information.IsDBNull(linha["Cidade_Corresp"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Cidade_Corresp")))
                    endereco.Municipio = DescubraMunicipio(UtilidadesDePersistencia.GetValor(linha, "Cidade_Corresp"),
                                                           UtilidadesDePersistencia.GetValor(linha, "UF_Corresp"));

                endereco.TipoDeEndereco = DescubraTipoDeEndereco("CORRESPONDÊNCIA");
                enderecos.Add(endereco);
            }

            return enderecos;
        }

        private IList<string> ObtenhaContatosDaPessoa(DataRow linha)
        {
            var contatos = new List<string>();

            if (!Information.IsDBNull(linha["Contato1"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Contato1").Trim()))
                contatos.Add(UtilidadesDePersistencia.GetValor(linha, "Contato1").Trim());

            if (!Information.IsDBNull(linha["Contato2"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Contato2").Trim()))
                if (!contatos.Contains(UtilidadesDePersistencia.GetValor(linha, "Contato2").Trim()))
                    contatos.Add(UtilidadesDePersistencia.GetValor(linha, "Contato2").Trim());

            if (!Information.IsDBNull(linha["Contato3"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Contato3").Trim()))
                if (!contatos.Contains(UtilidadesDePersistencia.GetValor(linha, "Contato3").Trim()))
                    contatos.Add(UtilidadesDePersistencia.GetValor(linha, "Contato3").Trim());

            if (!Information.IsDBNull(linha["Contato4"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Contato4").Trim()))
                if (!contatos.Contains(UtilidadesDePersistencia.GetValor(linha, "Contato4").Trim()))
                    contatos.Add(UtilidadesDePersistencia.GetValor(linha, "Contato4").Trim());

            if (!Information.IsDBNull(linha["Contato5"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Contato5").Trim()))
                if (!contatos.Contains(UtilidadesDePersistencia.GetValor(linha, "Contato5").Trim()))
                    contatos.Add(UtilidadesDePersistencia.GetValor(linha, "Contato5").Trim());

            return contatos;
        }

        private IPessoaFisica MontePessoaFisicaPatente(DataRow linha)
        {
            IPessoaFisica pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();

            pessoa.Nome = UtilidadesDePersistencia.GetValor(linha, "Nome_Cliente").Trim();

            var endereco = MonteEndereco(linha);

            if (endereco != null)
                pessoa.AdicioneEnderecos(endereco);

            var contatos = ObtenhaContatosDaPessoa(linha);

            if (contatos.Count > 0) pessoa.AdicioneContatos(contatos);

            if (!Information.IsDBNull(linha["cgc"]))
            {
                if (!string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "cgc").Trim()))
                {
                    var cpf =
                        FabricaGenerica.GetInstancia().CrieObjeto<ICPF>(new object[]
                                                                            {
                                                                               ObtenhaApenasNumeros(UtilidadesDePersistencia.GetValor(
                                                                                    linha, "cgc").Trim())
                                                                            });

                    try
                    {
                        if (cpf.EhValido())
                            pessoa.AdicioneDocumento(cpf);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

            if (!Information.IsDBNull(linha["email1"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "email1").Trim()))
                pessoa.EnderecoDeEmail = UtilidadesDePersistencia.GetValor(linha, "email1").Trim();

            pessoa.EstadoCivil = EstadoCivil.Ignorado;
            pessoa.Sexo = Sexo.Masculino;
            pessoa.Nacionalidade = Nacionalidade.Brasileira;

            return pessoa;
        }

        private IPessoa MontePessoaJuridicaPatente(DataRow linha)
        {
            IPessoaJuridica pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaJuridica>();

            pessoa.Nome = UtilidadesDePersistencia.GetValor(linha, "Nome_Cliente").Trim();

            var endereco = MonteEndereco(linha);

            if (endereco != null)
                pessoa.AdicioneEnderecos(endereco);

            var contatos = ObtenhaContatosDaPessoa(linha);

            if (contatos.Count > 0) pessoa.AdicioneContatos(contatos);

            if (!Information.IsDBNull(linha["cgc"]))
            {
                if (!string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "cgc").Trim()))
                {
                    pessoa.AdicioneDocumento(
                        FabricaGenerica.GetInstancia().CrieObjeto<ICNPJ>(new object[]
                                                                             {
                                                                                ObtenhaApenasNumeros(UtilidadesDePersistencia.GetValor(
                                                                                     linha, "cgc").Trim())
                                                                             }));
                }
            }

            return pessoa;
        }

        private void CadastreEArmazeneInventor(string codigoDaPatente, string nomeDoInventor)
        {
            IPessoa pessoa = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
            {
                var pessoas = servico.ObtenhaPessoasPorNomeComoFiltro(nomeDoInventor.Trim(), 1);

                if (pessoas.Count == 0)
                {
                    pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                    pessoa.Nome = nomeDoInventor.Trim();
                    ((IPessoaFisica)pessoa).EstadoCivil = EstadoCivil.Ignorado;
                    ((IPessoaFisica)pessoa).Sexo = Sexo.Masculino;
                    ((IPessoaFisica)pessoa).Nacionalidade = Nacionalidade.Brasileira;
                    servico.Inserir((IPessoaFisica)pessoa);
                }
                else
                {
                    pessoa = pessoas[0];
                }
            }

            if (!inventoresDePatenteComChaveLegada.ContainsKey(codigoDaPatente))
                inventoresDePatenteComChaveLegada.Add(codigoDaPatente, new List<IInventor>());

            inventoresDePatenteComChaveLegada[codigoDaPatente].Add(CadastreInventor(pessoa));
        }

        private void CadastreEArmazeneInventorDeDesenho(string codigoDaPatente, string nomeDoInventor)
        {
            IPessoa pessoa = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
            {
                var pessoas = servico.ObtenhaPessoasPorNomeComoFiltro(nomeDoInventor.Trim(), 1);

                if (pessoas.Count == 0)
                {
                    pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                    pessoa.Nome = nomeDoInventor.Trim();
                    ((IPessoaFisica)pessoa).EstadoCivil = EstadoCivil.Ignorado;
                    ((IPessoaFisica)pessoa).Sexo = Sexo.Masculino;
                    ((IPessoaFisica)pessoa).Nacionalidade = Nacionalidade.Brasileira;
                    servico.Inserir((IPessoaFisica)pessoa);
                }
                else
                {
                    pessoa = pessoas[0];
                }
            }

            if (!inventoresDeDesenhoComChaveLegada.ContainsKey(codigoDaPatente))
                inventoresDeDesenhoComChaveLegada.Add(codigoDaPatente, new List<IInventor>());

            inventoresDeDesenhoComChaveLegada[codigoDaPatente].Add(CadastreInventor(pessoa));
        }

        private void CarregueECadastreInventores()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select * from patentes";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                if (!Information.IsDBNull(linha["Inventores"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Inventores").Trim()))
                {
                    var inventores = UtilidadesDePersistencia.GetValor(linha, "Inventores").Trim();

                    if (inventores.Contains("/"))
                    {
                        var arrayComBarra = inventores.Split('/');

                        foreach (var inventor in arrayComBarra)
                        {
                            CadastreEArmazeneInventor(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente"), inventor);
                        }
                        continue;
                    }

                    if (inventores.Contains("\n"))
                    {
                        var arrayComQuebra = inventores.Split('\n');

                        foreach (var inventor in arrayComQuebra)
                        {
                            CadastreEArmazeneInventor(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente"), inventor);
                        }
                        continue;
                    }

                    if (inventores.Contains(","))
                    {
                        var arrayComVirgula = inventores.Split(',');

                        foreach (var inventor in arrayComVirgula)
                        {
                            CadastreEArmazeneInventor(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente"), inventor);
                        }
                        continue;
                    }

                    if (inventores.Contains(";"))
                    {
                        var arrayComPontoEVirgula = inventores.Split(';');

                        foreach (var inventor in arrayComPontoEVirgula)
                        {
                            CadastreEArmazeneInventor(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente"), inventor);
                        }
                        continue;
                    }


                    CadastreEArmazeneInventor(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente"), inventores);
                }

            }
        }

        private void CarregueECadastreInventoresPatenteDI()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select * from desenhoindustrial";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                if (!Information.IsDBNull(linha["Inventores"]) && !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "Inventores").Trim()))
                {
                    var inventores = UtilidadesDePersistencia.GetValor(linha, "Inventores").Trim();

                    if (inventores.Contains("/"))
                    {
                        var arrayComBarra = inventores.Split('/');

                        foreach (var inventor in arrayComBarra)
                        {
                            CadastreEArmazeneInventorDeDesenho(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Desenho"), inventor);
                        }
                        continue;
                    }

                    if (inventores.Contains("\n"))
                    {
                        var arrayComQuebra = inventores.Split('\n');

                        foreach (var inventor in arrayComQuebra)
                        {
                            CadastreEArmazeneInventorDeDesenho(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Desenho"), inventor);
                        }
                        continue;
                    }

                    if (inventores.Contains(","))
                    {
                        var arrayComVirgula = inventores.Split(',');

                        foreach (var inventor in arrayComVirgula)
                        {
                            CadastreEArmazeneInventorDeDesenho(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Desenho"), inventor);
                        }
                        continue;
                    }

                    if (inventores.Contains(";"))
                    {
                        var arrayComPontoEVirgula = inventores.Split(';');

                        foreach (var inventor in arrayComPontoEVirgula)
                        {
                            CadastreEArmazeneInventorDeDesenho(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Desenho"), inventor);
                        }
                        continue;
                    }


                    CadastreEArmazeneInventorDeDesenho(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Desenho"), inventores);
                }

            }
        }

        private IInventor CadastreInventor(IPessoa pessoa)
        {
            var inventor = FabricaGenerica.GetInstancia().CrieObjeto<IInventor>(new object[] { pessoa });

            inventor.DataDoCadastro = DateTime.Now;

            if (!idsDeInventoresCadastrados.Contains(pessoa.ID.Value.ToString()))
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
                    servico.Inserir(inventor);

                idsDeInventoresCadastrados.Add(pessoa.ID.Value.ToString());
            }

            return inventor;
        }

        private void CarreguePrioridadeUnionistaPatente()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select * from prioridades";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var idPatente = ObtenhaApenasNumeros(UtilidadesDePersistencia.GetValor(linha, "Número_Patente"));

                if (!prioridadesUnionistaDePatenteComChaveLegada.ContainsKey(idPatente))
                    prioridadesUnionistaDePatenteComChaveLegada.Add(idPatente, new List<IPrioridadeUnionistaPatente>());

                var prioridadeUnionista = FabricaGenerica.GetInstancia().CrieObjeto<IPrioridadeUnionistaPatente>();

                string sigla = null;

                if (UtilidadesDePersistencia.GetValor(linha, "País_Prioridade").Equals("BRASIL"))
                    sigla = "BR";
                else
                    sigla = "US";

                prioridadeUnionista.Pais = paises[sigla];
                prioridadeUnionista.NumeroPrioridade = UtilidadesDePersistencia.GetValor(linha, "num_prioridade").Trim();
                prioridadeUnionista.DataPrioridade = ObtenhaData(UtilidadesDePersistencia.GetValor(linha, "data_prioridade").Trim());

                prioridadesUnionistaDePatenteComChaveLegada[idPatente].Add(prioridadeUnionista);
            }
        }


        private void CarregueClassificacaoDaPatente()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select  * from ClassificPatentes  ";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                if (!classificacoesDePatenteComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente").Trim()))
                    classificacoesDePatenteComChaveLegada.Add(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente").Trim(), new List<IClassificacaoPatente>());

                var classificacao = FabricaGenerica.GetInstancia().CrieObjeto<IClassificacaoPatente>();

                classificacao.TipoClassificacao = TipoClassificacaoPatente.Nacional;
                classificacao.Classificacao = UtilidadesDePersistencia.GetValor(linha, "Classificação").Trim();
                if (!Information.IsDBNull(linha["Descrição"]))
                    classificacao.DescricaoClassificacao = UtilidadesDePersistencia.GetValor(linha, "Descrição").Trim();
                classificacoesDePatenteComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Patente").Trim()].Add(classificacao);
            }
        }

        private void CarregueClassificacaoDeDesenho()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSiscopat = new OdbcConnection(txtStrConexaoSiscopat.Text))
            {
                conexaoSiscopat.Open();

                var sql = "select  * from ClassificDesenhos  ";

                using (var data = new OdbcDataAdapter(sql, conexaoSiscopat))
                    data.Fill(dataSetDadosLegados);

                conexaoSiscopat.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                if (!classificacoesDeDesenhoComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Desenho").Trim()))
                    classificacoesDeDesenhoComChaveLegada.Add(UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Desenho").Trim(), new List<IClassificacaoPatente>());

                var classificacao = FabricaGenerica.GetInstancia().CrieObjeto<IClassificacaoPatente>();

                classificacao.TipoClassificacao = TipoClassificacaoPatente.Nacional;
                classificacao.Classificacao = UtilidadesDePersistencia.GetValor(linha, "Classificação").Trim();
                if (!Information.IsDBNull(linha["Descrição"]))
                    classificacao.DescricaoClassificacao = UtilidadesDePersistencia.GetValor(linha, "Descrição").Trim();
                classificacoesDeDesenhoComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "Nat_Número_Desenho").Trim()].Add(classificacao);
            }
        }

        private void CarregueRadicais()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSolureg = new OleDbConnection(txtStringDeConexaoSolureg.Text))
            {
                conexaoSolureg.Open();

                var sql = "select idmarca, descricao_radical, codigo_ncl from marcas_radical " +
                           "left join ncl_radical on ncl_radical.idmarcas_radical = marcas_radical.idmarcas_radical " +
                           "left join ncl on ncl.idncl = ncl_radical.idncl";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoSolureg))
                    data.Fill(dataSetDadosLegados);

                conexaoSolureg.Close();
            }

            var dados = dataSetDadosLegados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {

                if (!radicaisComChaveLegada.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "idmarca")))
                    radicaisComChaveLegada.Add(UtilidadesDePersistencia.GetValor(linha, "idmarca"), new List<IRadicalMarcas>());

                var radical = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalMarcas>();
                radical.DescricaoRadical = UtilidadesDePersistencia.GetValor(linha, "descricao_radical").Trim();

                if (!Information.IsDBNull(linha["codigo_ncl"]))
                {
                    var codigoNCL = UtilidadesDePersistencia.GetValor(linha, "codigo_ncl").Trim();

                    if (codigoNCL.Length == 1) codigoNCL = "0" + codigoNCL;
                    radical.NCL = NCL.ObtenhaPorCodigo(codigoNCL);
                }

                radicaisComChaveLegada[UtilidadesDePersistencia.GetValor(linha, "idmarca")].Add(radical);
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

                    if (codigoDoDespacho.Length == 1) codigoDoDespacho = "00" + codigoDoDespacho;

                    if (codigoDoDespacho.Length == 2) codigoDoDespacho = "0" + codigoDoDespacho;

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
                          "idclasse_item1, idclasse_item2, idclasse_item3 " +
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
                    marcasMigradas.Add(UtilidadesDePersistencia.GetValor(linha, "idmarca"), marca);
                }
            }
        }

        private ICliente CadastreCliente(IPessoa pessoa, IGrupoDeAtividade grupoDeAtividade)
        {

            var cliente = FabricaGenerica.GetInstancia().CrieObjeto<ICliente>(new object[] { pessoa });

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
                if (!string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "CNPJ_CPF").Trim()))
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
                if (!string.IsNullOrEmpty(UtilidadesDePersistencia.GetValor(linha, "CNPJ_CPF").Trim()))
                {
                    var cpf =
                        FabricaGenerica.GetInstancia().CrieObjeto<ICPF>(new object[]
                                                                            {
                                                                                UtilidadesDePersistencia.GetValor(
                                                                                    linha, "CNPJ_CPF").Trim()
                                                                            });

                    try
                    {
                        if (cpf.EhValido())
                            pessoa.AdicioneDocumento(cpf);
                    }
                    catch (Exception ex)
                    {

                    }



                }

            }

            ((IPessoaFisica)pessoa).EstadoCivil = EstadoCivil.Ignorado;
            ((IPessoaFisica)pessoa).Nacionalidade = Nacionalidade.Brasileira;
            ((IPessoaFisica)pessoa).Sexo = Sexo.Masculino;

        }

        private void frmMigrador_Load(object sender, EventArgs e)
        {
            txtStringDeConexaoSolureg.Text =
                @"Provider=SQLOLEDB.1;Password=sa;Persist Security Info=True;User ID=sa;Initial Catalog=SoluReg;Data Source=.\sqlexpress";

            txtStrConexaoSiscopat.Text = @"DSN=Siscopat";

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConexao>())
            {
                var conexao = servico.ObtenhaConexao();
                FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao = conexao;
                FabricaDeContexto.GetInstancia().GetContextoAtual().EmpresaLogada = new EmpresaVisivel(15645, "");
            }

            CarregueGruposDeAtividade();
            CarregueMunicipios();
            CarregueTiposDeEndereco();
            CarregueDespachosDeMarcas();
            CarregueDespachosDePatentes();
            CarreguePaises();
            CarregueNaturezasDePatente();
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

        private void CarregueNaturezasDePatente()
        {
            naturezasPatentes = new Dictionary<string, INaturezaPatente>();

            var conexao = FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao;

            DataSet dataSet = new DataSet();

            using (var conexaoPadrao = new OleDbConnection("Provider=SQLOLEDB.1;" + conexao.StringDeConexao))
            {
                conexaoPadrao.Open();

                var sql = "select * from mp_natureza_patente";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoPadrao))
                    data.Fill(dataSet);

                conexaoPadrao.Close();
            }

            var dados = dataSet.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var natureza = FabricaGenerica.GetInstancia().CrieObjeto<INaturezaPatente>();

                natureza.IdNaturezaPatente = UtilidadesDePersistencia.GetValorLong(linha, "IDNATUREZA_PATENTE");
                natureza.SiglaNatureza = UtilidadesDePersistencia.GetValor(linha, "SIGLA_NATUREZA");
                naturezasPatentes.Add(natureza.SiglaNatureza, natureza);
            }
        }

        private void CarreguePaises()
        {
            paises = new Dictionary<string, IPais>();

            var conexao = FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao;

            DataSet dataSet = new DataSet();

            using (var conexaoPadrao = new OleDbConnection("Provider=SQLOLEDB.1;" + conexao.StringDeConexao))
            {
                conexaoPadrao.Open();

                var sql = "select * from ncl_pais ";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoPadrao))
                    data.Fill(dataSet);

                conexaoPadrao.Close();
            }

            var dados = dataSet.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var pais = FabricaGenerica.GetInstancia().CrieObjeto<IPais>();

                pais.ID = UtilidadesDePersistencia.GetValorLong(linha, "ID");
                pais.Nome = UtilidadesDePersistencia.GetValor(linha, "NOME");
                pais.Sigla = UtilidadesDePersistencia.GetValor(linha, "SIGLA");

                paises.Add(pais.Sigla, pais);
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

        private void CarregueDespachosDePatentes()
        {
            despachoDePatente = new Dictionary<string, IDespachoDePatentes>();

            var conexao = FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao;

            DataSet dataSet = new DataSet();

            using (var conexaoPadrao = new OleDbConnection("Provider=SQLOLEDB.1;" + conexao.StringDeConexao))
            {
                conexaoPadrao.Open();

                var sql = "select * from mp_despacho_patente ";

                using (OleDbDataAdapter data = new OleDbDataAdapter(sql, conexaoPadrao))
                    data.Fill(dataSet);

                conexaoPadrao.Close();
            }

            var dados = dataSet.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                var despacho = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDePatentes>();

                despacho.IdDespachoDePatente = UtilidadesDePersistencia.GetValorLong(linha, "IDDESPACHOPATENTE");
                despacho.Codigo = UtilidadesDePersistencia.GetValor(linha, "CODIGO");

                despachoDePatente.Add(despacho.Codigo, despacho);
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

            if (MunicipioSTR.Equals("GOIANIA") || MunicipioSTR.Equals("GOANIA") || MunicipioSTR.Equals("GOIÃNIA") || MunicipioSTR.Equals("GOIÂNIA GO") ||
            MunicipioSTR.Equals("GOIÂNIA74") || MunicipioSTR.Equals("GOIÂNIA"))
            {
                MunicipioSTR = "GOIÂNIA";
                uf = "GO";
            }
                


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

            if (MunicipioSTR.Contains("IBIRUBA") || MunicipioSTR.Contains("IBIRUBÁ"))
            {
                MunicipioSTR = "IBIRUBÁ";
                uf = "RS";

            }
                

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

            if (MunicipioSTR.Contains("HAYWARD"))
                return null;

            if (MunicipioSTR.Contains("AGUAS CLARAS"))
                return null;
            
            if (MunicipioSTR.Contains("HAYWARD")) return null;

            if (MunicipioSTR.Contains("RONDONOPOLIS"))
                MunicipioSTR = "RONDONÓPOLIS";

            if (MunicipioSTR.Contains("MATRICHÃ"))
                MunicipioSTR = "MATRINCHÃ";

            if (MunicipioSTR.Contains("MARINGÁ")) uf = "PR";

            if (MunicipioSTR.Contains("SÃO LUIZ DE MONTES BELOS"))
                MunicipioSTR = "SÃO LUÍS DE MONTES BELOS";

            if (MunicipioSTR.Contains("CIDADE DE SALTO"))
                return null;

            
            return municipios[MunicipioSTR + "|" + uf];
        }

        private string ObtenhaApenasNumeros(string NumeroStrDesformatado)
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
