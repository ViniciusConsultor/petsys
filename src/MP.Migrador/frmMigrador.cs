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
                    numero = "NÚMERO " +  UtilidadesDePersistencia.GetValor(linha, "NUMERO").Trim();

                endereco.Complemento = (complemento + " " + numero).Trim();

                if (!Information.IsDBNull(linha["CEP"]))
                    endereco.CEP = new CEP(UtilidadesDePersistencia.GetValorLong(linha, "CEP"));

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
        }
    }
}
