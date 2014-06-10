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

namespace MP.Migrador
{
    public partial class frmMigradorDeVersoes : Form
    {
        public frmMigradorDeVersoes()
        {
            InitializeComponent();
        }

        private void btnConverter_Click(object sender, EventArgs e)
        {
            IDictionary<string, string> enderecosDeEmails = new Dictionary<string, string>();
            var dataSetDados = new DataSet();

            using (var conexao = new OleDbConnection(txtStringConexao.Text))
            {
                conexao.Open();

                var sql = "SELECT ID, ENDEMAIL FROM NCL_PESSOA WHERE ENDEMAIL IS NOT NULL AND ENDEMAIL <> ''";

                using (var data = new OleDbDataAdapter(sql, conexao))
                    data.Fill(dataSetDados);

                conexao.Close();
            }

            var dados = dataSetDados.Tables[0];

            foreach (DataRow linha in dados.Rows)
            {
                if(!enderecosDeEmails.ContainsKey(UtilidadesDePersistencia.GetValor(linha, "ID")))
                    enderecosDeEmails.Add(UtilidadesDePersistencia.GetValor(linha, "ID"), UtilidadesDePersistencia.GetValor(linha, "ENDEMAIL"));
            }

            using (var conexao = new OleDbConnection(txtStringConexao.Text))
            {
                conexao.Open();
                var comnando = conexao.CreateCommand();
                var transacao = conexao.BeginTransaction();
                comnando.Transaction = transacao;

                try
                {
                    foreach (KeyValuePair<string, string> item in enderecosDeEmails)
                    {
                        string[] emailsSeparadosPorPontoEVirgular = item.Value.Split(Convert.ToChar(";"));

                        foreach (string email in emailsSeparadosPorPontoEVirgular)
                            if (!string.IsNullOrEmpty(email.Trim()))
                            {
                                comnando.CommandText = "INSERT INTO NCL_PESSOAEMAIL(IDPESSOA, ENDEMAIL) VALUES(" + item.Key + ", '" + email.Trim() + "')";
                                comnando.ExecuteNonQuery();
                            }
                    }
                    
                    transacao.Commit();
                }
                catch (Exception)
                {
                    MessageBox.Show("Ocorreu um erro ao executar a migração!");
                    transacao.Rollback();
                }finally
                {
                    conexao.Close();
                }
            }

            MessageBox.Show("Migração executada com sucesso!");
        }
    }
}
