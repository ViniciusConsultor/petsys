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
using Compartilhados.Interfaces.Core.Servicos;
using Core.Interfaces.Servicos;
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

        private void btnMigrar_Click(object sender, EventArgs e)
        {
            MigrePessoas();
            MessageBox.Show("Dados migrados com sucesso!");
        }

        private void MigrePessoas()
        {
            DataSet dataSetDadosLegados = new DataSet();

            using (var conexaoSolureg = new OleDbConnection(txtStringDeConexaoSolureg.Text))
            {
                conexaoSolureg.Open();

                var sql = "select * from contato " +
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
                        }

                        MontaPessoaJuridica(linha, ref pessoa);
                        continue;
                    }

                    if (idContatoAnterior != UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO"))
                    {
                        pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                        pessoas.Add(pessoa);
                        idContatoAnterior = UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO");
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
                    }

                    MontaPessoaJuridica(linha, ref pessoa);
                    continue;
                }

                if (idContatoAnterior != UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO"))
                {
                    pessoa = FabricaGenerica.GetInstancia().CrieObjeto<IPessoaFisica>();
                    pessoas.Add(pessoa);
                    idContatoAnterior = UtilidadesDePersistencia.getValorInteger(linha, "IDCONTATO");
                }

                MontaPessoaFisica(linha, ref pessoa);
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

                pessoa.AdicioneEndereco(endereco);
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
            }

            CarregueGruposDeAtividade();
            CarregueMunicipios();
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
            catch( Exception ex)
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

            if (MunicipioSTR.Contains("MONTIVIDÍU"))
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

            

            return municipios[MunicipioSTR + "|" + uf];
        }
    }
}
