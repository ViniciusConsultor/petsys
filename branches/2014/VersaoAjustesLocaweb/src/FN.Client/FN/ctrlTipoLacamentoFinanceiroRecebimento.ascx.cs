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
    public partial class ctrlTipoLacamentoFinanceiroRecebimento : System.Web.UI.UserControl
    {
        public event TipoLacamentoFoiSelecionadoEventHandler TipoLacamentoFoiSelecionado;
        public delegate void TipoLacamentoFoiSelecionadoEventHandler(TipoLacamentoFinanceiroRecebimento tipoLacamento);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa(IList<TipoLacamentoFinanceiroRecebimento> tiposADesconsiderar )
        {
            LimparControle();
            CarregueCombo(tiposADesconsiderar);
        }

        private void CarregueCombo(IList<TipoLacamentoFinanceiroRecebimento> tiposADesconsiderar)
        {
            if (tiposADesconsiderar  == null) tiposADesconsiderar = new List<TipoLacamentoFinanceiroRecebimento>();

            foreach (var tipo in TipoLacamentoFinanceiroRecebimento.ObtenhaTodos())
            {
                if (!tiposADesconsiderar.Contains(tipo))
                {
                    var item = new RadComboBoxItem(tipo.Descricao, tipo.ID.ToString());

                    cboTipoLacamento.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        private void LimparControle()
        {
            var controle = cboTipoLacamento as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboTipoLacamento.ClearSelection();
        }

        public string Codigo
        {
            get { return cboTipoLacamento.SelectedValue; }
            set
            {
                var tipo = TipoLacamentoFinanceiroRecebimento.Obtenha(Convert.ToInt16(value));

                if (tipo != null)
                {
                    cboTipoLacamento.SelectedValue = tipo.ID.ToString();
                    cboTipoLacamento.Text = tipo.Descricao;
                }
            }
        }

        public TipoLacamentoFinanceiroRecebimento TipoLacamentoSelecionado
        {
            get { return (TipoLacamentoFinanceiroRecebimento)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }


        public bool AutoPostBack
        {
            set { cboTipoLacamento.AutoPostBack = value; }
        }

        protected void cboTipoLacamento_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboTipoLacamento.AutoPostBack) return;

            TipoLacamentoFinanceiroRecebimento tipo = null;

            if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
            {
                LimparControle();
                return;
            }

            tipo = TipoLacamentoFinanceiroRecebimento.Obtenha(Convert.ToInt16(((RadComboBox)sender).SelectedValue));

            TipoLacamentoSelecionado = tipo;

            if (TipoLacamentoFoiSelecionado != null)
                TipoLacamentoFoiSelecionado(tipo);
        }
    }
}