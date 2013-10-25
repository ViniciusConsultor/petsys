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
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

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
            InicializaDespachosDePatentes();
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

         private void InicializaDespachosDePatentes()
         {
             Cursor = Cursors.WaitCursor;
             toolStripStatusLabel2.Text = "Abrindo arquivo de despachos de patentes...";

             var arquivo = new StreamReader(diretorio + @"\DespachosDeMarcas.csv");

             var Despachos = new List<IDespachoDeMarcas>();

             toolStripStatusLabel2.Text = "Iniciando a leitura do arquivo...";

             while (!arquivo.EndOfStream)
             {
                 var linha = arquivo.ReadLine();

                 if (!String.IsNullOrEmpty(linha))
                     Despachos.Add(CriaDespachoDeMarca(linha));
             }


             toolStripStatusLabel2.Text = "Dados carregados com sucesso..";
             toolStripStatusLabel2.Text = "Iniciando processamento..";
             toolStripProgressBar1.Visible = true;
             toolStripProgressBar1.Maximum = Despachos.Count;
             Activate();
             toolStripStatusLabel2.Text = "Inserindo despachos de marcas";

             using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
             {
                 foreach (var despacho in Despachos)
                 {
                     Application.DoEvents();
                     servico.Inserir(despacho);
                     toolStripStatusLabel2.Text = "Despacho - " + despacho.CodigoDespacho + " inserido com sucesso...";
                     toolStripProgressBar1.Increment(1);
                 }
             }

             toolStripStatusLabel2.Text = "Total de despachos inseridos: " + Despachos.Count;

             Cursor = Cursors.Default;
             toolStripProgressBar1.Value = 0;
             toolStripProgressBar1.Visible = false;
             toolStripStatusLabel2.Text = "";
         }


        private IDespachoDeMarcas CriaDespachoDeMarca(string linha)
        {
            var pedacos = linha.Split('\t');

            var despacho = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();

            //despacho.CodigoDespacho = pedacos[0];
            //despacho.Registro = pedacos[3];
            //despacho.DetalheDespacho = 

            return null;
        }

    }
}
