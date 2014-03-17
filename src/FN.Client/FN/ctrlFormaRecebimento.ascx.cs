using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Interfaces.FN.Negocio;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class ctrlFormaRecebimento : System.Web.UI.UserControl
    {
        public event FormaDeRecebimentoFoiSelecionadoEventHandler FormaDeRecebimentoFoiSelecionada;
        public delegate void FormaDeRecebimentoFoiSelecionadoEventHandler(FormaDeRecebimento situacao);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cboFormaDeRecebimento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboFormaDeRecebimento.AutoPostBack) return;

            FormaDeRecebimento forma = null;

            if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
            {
                LimparControle();
                return;
            }

            forma = FormaDeRecebimento.Obtenha(Convert.ToInt16(((RadComboBox)sender).SelectedValue));

            FormaDeRecebimentoSelecionada = forma;

            if (FormaDeRecebimentoFoiSelecionada != null)
                FormaDeRecebimentoFoiSelecionada(forma);
        }

        public void Inicializa()
        {
            LimparControle();
            CarregueCombo();
        }

        private void CarregueCombo()
        {
            foreach (var forma in FormaDeRecebimento.ObtenhaTodos())
            {
                var item = new RadComboBoxItem(forma.Descricao, forma.ID.ToString());

                cboFormaDeRecebimento.Items.Add(item);
                item.DataBind();
            }
        }

        private void LimparControle()
        {
            var controle = cboFormaDeRecebimento as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboFormaDeRecebimento.ClearSelection();
        }

        public string Codigo
        {
            get { return cboFormaDeRecebimento.SelectedValue; }
            set
            {
                var forma = FormaDeRecebimento.Obtenha(Convert.ToInt16(value));

                if (forma != null)
                {
                    cboFormaDeRecebimento.SelectedValue = forma.ID.ToString();
                    cboFormaDeRecebimento.Text = forma.Descricao;
                }
            }
        }

        public FormaDeRecebimento FormaDeRecebimentoSelecionada
        {
            get { return (FormaDeRecebimento)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }


        public bool AutoPostBack
        {
            set { cboFormaDeRecebimento.AutoPostBack = value; }
        }
    }
}