using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Compartilhados;
using Compartilhados.Fabricas;
using Core.Interfaces.Servicos;

namespace InicializaBancoMP
{
    public partial class frmPrincipal : Form
    {
        private string diretorio;

        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConexao>())
            {
                var conexao = servico.ObtenhaConexao();
                FabricaDeContexto.GetInstancia().GetContextoAtual().Conexao = conexao;
            }
            
            Cursor = Cursors.Default;
            toolStripStatusLabel2.Text = "";
            diretorio = Environment.CurrentDirectory;
        } 
    }
}
