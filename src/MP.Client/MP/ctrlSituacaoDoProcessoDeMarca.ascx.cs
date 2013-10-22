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
    public partial class ctrlSituacaoDoProcessoDeMarca : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Inicializa()
        {
            LimparControle();
            CarregueCombo();
        }

        private void LimparControle()
        {
            var controle = cboSituacaoDoProcesso as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboSituacaoDoProcesso.ClearSelection();
        }

        public string Codigo
        {
            get { return cboSituacaoDoProcesso.SelectedValue; }
            set
            {
                cboSituacaoDoProcesso.SelectedValue = value;
            }
        }

        private void CarregueCombo()
        {
            foreach (var situacao in SituacaoDoProcessoDeMarca.ObtenhaSituacoesDoProcesso())
            {
                var item = new RadComboBoxItem(situacao.DescricaoSituacao, situacao.CodigoSituacaoProcesso.ToString());

                item.Attributes.Add("Codigo", situacao.CodigoSituacaoProcesso.ToString());

                cboSituacaoDoProcesso.Items.Add(item);
                item.DataBind();
            }
        }
    }
}