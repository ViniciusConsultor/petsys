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
    public partial class ctrlSituacaoDoProcessoDePatente : System.Web.UI.UserControl
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
            var controle = cboSituacaoDoProcessoDePatente as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboSituacaoDoProcessoDePatente.ClearSelection();
        }

        public string Codigo
        {
            get { return cboSituacaoDoProcessoDePatente.SelectedValue; }
            set
            {
                cboSituacaoDoProcessoDePatente.SelectedValue = value;
            }
        }

        private void CarregueCombo()
        {
            foreach (var situacao in SituacaoDoProcessoDePatente.ObtenhaSituacoesDoProcessoDePatente())
            {
                var item = new RadComboBoxItem(situacao.DescricaoSituacaoProcessoDePatente, situacao.CodigoSituacaoProcessoDePatente);

                item.Attributes.Add("Codigo", situacao.CodigoSituacaoProcessoDePatente);

                cboSituacaoDoProcessoDePatente.Items.Add(item);
                item.DataBind();
            }
        }
    }
}