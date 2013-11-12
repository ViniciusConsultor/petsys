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
using MP.Interfaces.Utilidades;

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
            InicializaDespachosDeMarcas();
            InicializaDespachosDePatentes();
            InicializaNaturezasDePatente();

            MessageBox.Show("Dados inicializados com sucesso!");
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

        private void InicializaNaturezasDePatente()
        {
            Cursor = Cursors.WaitCursor;
            toolStripStatusLabel2.Text = "Abrindo arquivo de despachos de marcas...";

            var arquivo = new StreamReader(diretorio + @"\NaturezasDePatente.txt");

            var Naturezas = new List<INaturezaPatente>();

            toolStripStatusLabel2.Text = "Iniciando a leitura do arquivo...";

            while (!arquivo.EndOfStream)
            {
                var linha = arquivo.ReadLine();

                if (!String.IsNullOrEmpty(linha))
                    Naturezas.Add(CriaNaturezaDePatente(linha));
            }


            toolStripStatusLabel2.Text = "Dados carregados com sucesso..";
            toolStripStatusLabel2.Text = "Iniciando processamento..";
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Maximum = Naturezas.Count;
            Activate();
            toolStripStatusLabel2.Text = "Inserindo naturezas de patente";

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeNaturezaPatente>())
            {
                foreach (var natureza in Naturezas)
                {
                    Application.DoEvents();
                    servico.Inserir(natureza);
                    toolStripStatusLabel2.Text = "Natureza - " + natureza.SiglaNatureza + " inserido com sucesso...";
                    toolStripProgressBar1.Increment(1);
                }
            }

            toolStripStatusLabel2.Text = "Total de naturezas de patente inseridas: " + Naturezas.Count;

            Cursor = Cursors.Default;
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel2.Text = "";   
        }

        private INaturezaPatente CriaNaturezaDePatente(string linha)
        {
            var pedacos = linha.Split('\t');

            var natureza = FabricaGenerica.GetInstancia().CrieObjeto<INaturezaPatente>();

            natureza.DescricaoNaturezaPatente = pedacos[0];
            natureza.SiglaNatureza = pedacos[1];

            if (!string.IsNullOrEmpty(pedacos[2]))
                natureza.TempoInicioAnos = Convert.ToInt32(pedacos[2]);

            if (!string.IsNullOrEmpty(pedacos[3]))
                natureza.QuantidadePagamento = Convert.ToInt32(pedacos[3]);
            
            if (!string.IsNullOrEmpty(pedacos[4]))
                natureza.TempoEntrePagamento = Convert.ToInt32(pedacos[4]);

            if (!string.IsNullOrEmpty(pedacos[5]))
                natureza.SequenciaInicioPagamento = Convert.ToInt32(pedacos[5]);

            if (!string.IsNullOrEmpty(pedacos[6]))
                natureza.TemPagamentoIntermediario = pedacos[6].Equals("1");

            if (!string.IsNullOrEmpty(pedacos[7]))
                natureza.InicioIntermediarioSequencia = Convert.ToInt32(pedacos[7]);

            if (!string.IsNullOrEmpty(pedacos[8]))
                natureza.QuantidadePagamentoIntermediario = Convert.ToInt32(pedacos[8]);

            if (!string.IsNullOrEmpty(pedacos[9]))
                natureza.DescricaoPagamento = pedacos[9];

            if (!string.IsNullOrEmpty(pedacos[10]))
                natureza.DescricaoPagamentoIntermediario = pedacos[10];
            
            if (!string.IsNullOrEmpty(pedacos[11]))
                natureza.TemPedidoDeExame = pedacos[11].Equals("1");

            return natureza;
        }

         private void InicializaDespachosDeMarcas()
         {
             Cursor = Cursors.WaitCursor;
             toolStripStatusLabel2.Text = "Abrindo arquivo de despachos de marcas...";

             var arquivo = new StreamReader(diretorio + @"\DespachosDeMarcas.txt");

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

             toolStripStatusLabel2.Text = "Total de despachos de marcas inseridos: " + Despachos.Count;

             Cursor = Cursors.Default;
             toolStripProgressBar1.Value = 0;
             toolStripProgressBar1.Visible = false;
             toolStripStatusLabel2.Text = "";
         }


        private IDespachoDeMarcas CriaDespachoDeMarca(string linha)
        {
            var pedacos = linha.Split('\t');

            var despacho = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();

            despacho.CodigoDespacho = pedacos[0];
            
            if (!string.IsNullOrEmpty(pedacos[1]))
                despacho.SituacaoProcesso = pedacos[1];

            despacho.PrazoParaProvidenciaEmDias = Convert.ToInt32(pedacos[2]);

            if (!string.IsNullOrEmpty(pedacos[3]))
                despacho.Providencia = pedacos[3];

            despacho.DescricaoDespacho = pedacos[4];

            despacho.DesativaProcesso = pedacos[5].Equals("VERDADEIRO", StringComparison.InvariantCultureIgnoreCase);
            despacho.DesativaPesquisaDeColidencia = pedacos[6].Equals("VERDADEIRO", StringComparison.InvariantCultureIgnoreCase);
            
            return despacho;
        }

        private void InicializaDespachosDePatentes()
        {
            Cursor = Cursors.WaitCursor;
            toolStripStatusLabel2.Text = "Abrindo arquivo de despachos de patentes...";

            var arquivo = new StreamReader(diretorio + @"\DespachosDePatentes.txt");

            var Despachos = new List<IDespachoDePatentes>();

            toolStripStatusLabel2.Text = "Iniciando a leitura do arquivo...";

            while (!arquivo.EndOfStream)
            {
                var linha = arquivo.ReadLine();

                if (!String.IsNullOrEmpty(linha))
                    Despachos.Add(CriaDespachoDePatente(linha));
            }


            toolStripStatusLabel2.Text = "Dados carregados com sucesso..";
            toolStripStatusLabel2.Text = "Iniciando processamento..";
            toolStripProgressBar1.Visible = true;
            toolStripProgressBar1.Maximum = Despachos.Count;
            Activate();
            toolStripStatusLabel2.Text = "Inserindo despachos de marcas";

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
            {
                foreach (var despacho in Despachos)
                {
                    Application.DoEvents();
                    servico.Inserir(despacho);
                    toolStripStatusLabel2.Text = "Despacho - " + despacho.Codigo + " inserido com sucesso...";
                    toolStripProgressBar1.Increment(1);
                }
            }

            toolStripStatusLabel2.Text = "Total de despachos inseridos: " + Despachos.Count;

            Cursor = Cursors.Default;
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
            toolStripStatusLabel2.Text = "";
        }


        private IDespachoDePatentes CriaDespachoDePatente(string linha)
        {
            var pedacos = linha.Split('\t');

            var despacho = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDePatentes>();

            despacho.Codigo = pedacos[0];
            
            if (!string.IsNullOrEmpty(pedacos[1]))
                despacho.Titulo = pedacos[1];

            if (!string.IsNullOrEmpty(pedacos[2]))
                despacho.Situacao = pedacos[2];

            if (!string.IsNullOrEmpty(pedacos[3]))
                despacho.PrazoProvidencia =  Convert.ToInt32(pedacos[3]);

            if (!string.IsNullOrEmpty(pedacos[4]))
                despacho.TipoProvidencia = pedacos[4];

            
            despacho.DesativaProcesso = pedacos[5].Equals("VERDADEIRO", StringComparison.InvariantCultureIgnoreCase);
            despacho.AgendarPagamento = pedacos[6].Equals("VERDADEIRO", StringComparison.InvariantCultureIgnoreCase);

            if (!string.IsNullOrEmpty(pedacos[7]))
                despacho.Descricao = pedacos[7];

            return despacho;
        }

    }
}
