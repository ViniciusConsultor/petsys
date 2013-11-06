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
    public partial class ctrlPatente : UserControl
    {
        public static event PatenteFoiSelecionadaEventHandler PatenteFoiSelecionada;
        public delegate void PatenteFoiSelecionadaEventHandler(IPatente patente);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            LimparControle();
        }

        public bool AutoPostBack
        {
            set { cboPatente.AutoPostBack = value; }
        }

        protected void cboPatente_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboPatente.AutoPostBack) return;
            
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                IPatente patente = null;

                if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
                {
                    LimparControle();
                    return;
                }

                int codigoSelecionado = int.Parse(((RadComboBox)sender).SelectedValue);
                patente = servico.ObtenhaPatente(codigoSelecionado);

                PatenteSelecionada = patente;

                if (PatenteFoiSelecionada != null)
                    PatenteFoiSelecionada(patente);
                
            }
        }

        public IPatente PatenteSelecionada
        {
            get { return (IPatente)ViewState[ClientID]; }
            set { ViewState.Add(ClientID, value); }
        }

        protected void cboPatente_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                cboPatente.Items.Clear();

                foreach (IPatente patente in servico.ObtenhaPatentesPeloTitulo(e.Text, 50))
                {
                    var item = new RadComboBoxItem(patente.TituloPatente, patente.Identificador.ToString());

                    item.Attributes.Add("TipoDePatente", patente.NaturezaPatente.DescricaoNaturezaPatente);
                    item.Attributes.Add("Resumo", patente.Resumo);
                    item.Attributes.Add("DataDeCadastro", patente.DataCadastro != null ? patente.DataCadastro.Value.ToString("dd/MM/yyyy") : "Data não informada");

                    cboPatente.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        private void LimparControle()
        {
            var controle = cboPatente as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboPatente.ClearSelection();
            PatenteFoiSelecionada = null;
            BotaoNovoEhVisivel = false;
        }

        public bool EnableLoadOnDemand
        {
            set { cboPatente.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboPatente.ShowDropDownOnTextboxClick = value; }
        }

        public string TituloPatente
        {
            get { return cboPatente.Text; }
            set { cboPatente.Text = value; }
        }

        public bool BotaoNovoEhVisivel
        {
            set { btnNovo.Visible = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {

            var principal = FabricaDeContexto.GetInstancia().GetContextoAtual();

            if (btnNovo.Visible)
                btnNovo.Visible = principal.EstaAutorizado(btnNovo.CommandArgument);

            base.OnPreRender(e);
        }

        protected void btnNovo_OnClick(object sender, ImageClickEventArgs e)
        {
            var URL = ObtenhaURL();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Cadastro de patente", 800, 550),
                                                false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual();
            URL = String.Concat(URL, "MP/cdPatente.aspx");
            return URL;
        }
    }
}