using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class ctrlMarcas : System.Web.UI.UserControl
    {
        public static event MarcaFoiSelecionadaEventHandler MarcaFoiSelecionada;
        public delegate void MarcaFoiSelecionadaEventHandler(IMarcas marca);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            AutoPostBack = true;
            LimparControle();
        }

        private void LimparControle()
        {
            var controlePanel = cboMarcas as Control;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            MarcaSelecionada = null;
            cboMarcas.ClearSelection();
        }

        public bool EnableLoadOnDemand
        {
            set { cboMarcas.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboMarcas.ShowDropDownOnTextboxClick = value; }
        }

        public string DescricaoDaMarca
        {
            get { return cboMarcas.Text; }
            set { cboMarcas.Text = value; }
        }

        public string Apresentacao
        {
            get { return cboMarcas.Attributes["Apresentacao"]; }
            set { cboMarcas.Attributes["Apresentacao"] = value; }
        }

        public string Natureza
        {
            get { return cboMarcas.Attributes["Natureza"]; }
            set { cboMarcas.Attributes["Natureza"] = value; }
        }

        public string NCL
        {
            get { return cboMarcas.Attributes["NCL"]; }
            set { cboMarcas.Attributes["NCL"] = value; }
        }

        public IMarcas MarcaSelecionada
        {
            get { return (IMarcas)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        public string TextoItemVazio
        {
            set { cboMarcas.EmptyMessage = value; }
        }

        protected void cboMarca_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<IMarcas> listaDeMarcas = new List<IMarcas>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeMarcas>())
                listaDeMarcas = servico.obtenhaMarcasPelaDescricaoComoFiltro(e.Text, 50);

            if (listaDeMarcas.Count > 0)
            {
                foreach (var marca in listaDeMarcas)
                {
                    var item = new RadComboBoxItem(marca.DescricaoDaMarca, marca.IdMarca.Value.ToString());

                    item.Attributes.Add("Apresentacao",
                                        marca.Apresentacao.Nome ?? "Não informada");

                    item.Attributes.Add("Natureza",
                                        marca.Natureza.Nome ?? "Não informada");

                    item.Attributes.Add("NCL",
                                        marca.NCL.Codigo.ToString() ?? "Não informada");

                    this.cboMarcas.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboMarca_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboMarcas.AutoPostBack) return;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
            {
                LimparControle();
                return;                
            }

            IMarcas marca;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeMarcas>())
                marca = servico.obtenhaMarcasPeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));

            MarcaSelecionada = marca;

            if (MarcaFoiSelecionada != null)
                MarcaFoiSelecionada(marca);
        }

        public bool AutoPostBack
        {
            set { cboMarcas.AutoPostBack = value; }
        }

        public bool BotaoNovoEhVisivel
        {
            set { btnNovo.Visible = value; }
        }

        public bool BotaoDetalharEhVisivel
        {
            set { btnDetalhar.Visible = value; }   
        }
        
        protected override void OnPreRender(EventArgs e)
        {
            var principal = FabricaDeContexto.GetInstancia().GetContextoAtual();

            if (btnNovo.Visible)
                btnNovo.Visible = principal.EstaAutorizado(btnNovo.CommandArgument);

            if (btnDetalhar.Visible)
                btnDetalhar.Visible = principal.EstaAutorizado(btnDetalhar.CommandArgument);

            base.OnPreRender(e);
        }

        protected void btnNovo_OnClick(object sender, ImageClickEventArgs e)
        {
            var URL = ObtenhaURL();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Cadastro de marcas", 800, 550, "MP_cdMarcas_aspx"),
                                                false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual();
            URL = String.Concat(URL, "MP/cdMarcas.aspx");
            return URL;
        }
        
        protected void btnDetalhar_OnClick(object sender, ImageClickEventArgs e)
        {
            var url = ObtenhaURL();
            url = String.Concat(url, "?Id=", MarcaSelecionada.IdMarca.Value.ToString());
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.ExibeJanela(url, "Cadastro de marcas", 800, 550, "MP_cdMarcas_aspx"),
                                                    false);
        }
    }
}