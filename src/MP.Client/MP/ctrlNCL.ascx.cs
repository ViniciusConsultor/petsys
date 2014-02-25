using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class ctrlNCL : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool EnableLoadOnDemand
        {
            set { cboNCL.EnableLoadOnDemand = value; }
        }

        public bool AutoPostBack
        {
            set { cboNCL.AutoPostBack = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboNCL.ShowDropDownOnTextboxClick = value; }
        }

        public void Inicializa()
        {
            AutoPostBack = true;
            LimparControle();
        }

        private void LimparControle()
        {
            var controle = cboNCL as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboNCL.ClearSelection();
        }

        
        public string Codigo
        {
            get { return cboNCL.Text; }
            set {cboNCL.Text = value;}
        }

        public NCL NCLSelecionado
        {
            get { return (NCL)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        protected void cboNCL_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            var ncls = NCL.ObtenhaPorCodigoComoFiltro(e.Text);

            if (ncls.Count > 0)
            {
                foreach (var ncl in ncls)
                {
                    var item = new RadComboBoxItem(ncl.Codigo, ncl.Codigo);

                    item.Attributes.Add("Descricao", ncl.Descricao);
                    item.Attributes.Add("Natureza", ncl.NaturezaDeMarca.Nome);

                    cboNCL.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboNCL_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboNCL.AutoPostBack) return;

            if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
            {
                LimparControle();
                return;
            }

            var ncl = NCL.ObtenhaPorCodigo(((RadComboBox) sender).SelectedValue);

            NCLSelecionado = ncl;
        }
    }
}