using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using MP.Interfaces.Negocio;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmDetalhesLeituraDaRevista : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            int indice = 0;
            IList<ILeituraRevistaDeMarcas> listaDeProcessos = new List<ILeituraRevistaDeMarcas>();

            if (!String.IsNullOrEmpty(Request.QueryString["Indice"]))
            {
                indice = Convert.ToInt32(Request.QueryString["Indice"]);
            }

            listaDeProcessos = (IList<ILeituraRevistaDeMarcas>) Session["CHAVE_PROCESSOS_REUSLTADO_FILTRO"];

            CarregueDetahes(listaDeProcessos[indice]);
        }

        private void CarregueDetahes(ILeituraRevistaDeMarcas processo)
        {
            txtProcesso.Text = processo.NumeroDoProcesso;
            txtDataDeposito.Text = processo.DataDeDeposito;
            txtDataDeConcessao.Text = processo.DataDeConcessao;
            txtDataDeVigencia.Text = processo.DataDeVigencia;
            txtDespacho.Text = processo.CodigoDoDespacho;
            txtTextoComplementar.Text = processo.TextoDoDespacho;
            txtTitular.Text = processo.Titular;
            txtPais.Text = processo.Pais;
            txtEstado.Text = processo.Uf;
            txtMarca.Text = processo.Marca;
            txtApresentacao.Text = processo.Apresentacao;
            txtNatureza.Text = processo.Natureza;
            txtTraducao.Text = processo.TraducaoDaMarca;
            txtNCL.Text = processo.NCL;
            txtEdicaoNCL.Text = processo.EdicaoNCL;
            txtEspecificacaoNCL.Text = processo.EspecificacaoNCL;

            if(processo.ClasseViena != null)
            CarregueComboClasseViena(processo);

            if (processo.ClasseNacional != null)
                CarregueComboClasseNacional(processo);

            txtApostila.Text = processo.Apostila;
            txtProcurador.Text = processo.Procurador;
            txtDataPrioridadeUnionista.Text = processo.DataPrioridadeUnionista;
            txtNumeroPriUnionista.Text = processo.NumeroPrioridadeUnionista;
            txtPaisPriUnionista.Text = processo.PaisPrioridadeUnionista;

            if(processo.DicionarioSobrestadores != null && processo.DicionarioSobrestadores.Count > 0)
            {
                CarregueComboSobrestadores(processo);
            }

            txtNumeroProtocoloDespacho.Text = processo.NumeroProtocoloDespacho;
            txtDataProtocoloDespacho.Text = processo.DataProtocoloDespacho;
            txtCodigoServicoProtocoloDespacho.Text = processo.CodigoServicoProtocoloDespacho;
            txtRazaoSocialRequerenteProtocoloDespacho.Text = processo.RazaoSocialRequerenteProtocoloDespacho;
            txtPaisRequerenteProtocoloDespacho.Text = processo.PaisRequerenteProtocoloDespacho;
            txtEstadoRequerenteProtocoloDespacho.Text = processo.EstadoRequerenteProtocoloDespacho;
            txtProcuradorProtocoloDespacho.Text = processo.ProcuradorProtocoloDespacho;

        }

        private void CarregueComboSobrestadores(ILeituraRevistaDeMarcas processo)
        {
            cboSobrestador.Items.Clear();
            cboSobrestador.Attributes.Clear();

            foreach (var processoSobrestador in processo.DicionarioSobrestadores.Keys)
            {
                var item = new RadComboBoxItem(processoSobrestador, processoSobrestador);

                item.Attributes.Add("Marca",
                                    processo.DicionarioSobrestadores[processoSobrestador] ?? "Não informada");

                this.cboSobrestador.Items.Add(item);
                item.DataBind();
            }
        }

        private void CarregueComboClasseNacional(ILeituraRevistaDeMarcas processo)
        {
            cboClassificacaoNacional.Items.Clear();
            cboClassificacaoNacional.Attributes.Clear();

            foreach (var codigo in processo.ClasseNacional.listaDeCodigosDeSubClasse)
            {
                var item = new RadComboBoxItem(processo.ClasseNacional.CodigoClasseNacional, processo.ClasseNacional.CodigoClasseNacional);

                item.Attributes.Add("Especificacao",
                                    processo.ClasseNacional.EspecificacaoClasseNacional ?? "Não informada");

                item.Attributes.Add("SubClasse",
                                    codigo ?? "Não informada");

                this.cboClassificacaoNacional.Items.Add(item);
                item.DataBind();
            }
        }

        private void CarregueComboClasseViena(ILeituraRevistaDeMarcas processo)
        {
            cboClassificacaoViena.Items.Clear();
            cboClassificacaoViena.Attributes.Clear();

            foreach (var codigo in processo.ClasseViena.ListaDeCodigosClasseViena)
            {
                var item = new RadComboBoxItem(processo.ClasseViena.EdicaoClasseViena, processo.ClasseViena.EdicaoClasseViena);

                item.Attributes.Add("Codigo",
                                    codigo ?? "Não informada");

                this.cboClassificacaoViena.Items.Add(item);
                item.DataBind();
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return string.Empty;
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}