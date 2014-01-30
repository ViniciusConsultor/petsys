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
    public partial class ctrlPeriodo : System.Web.UI.UserControl
    {
        public event PeriodoFoiSelecionadoEventHandler PeriodoFoiSelecionado;
        public delegate void PeriodoFoiSelecionadoEventHandler(Periodo periodo);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            LimparControle();
            CarregueCombo();
        }

        private void CarregueCombo()
        {
            foreach (var periodo in Periodo.ObtenhaTodas())
            {
                var item = new RadComboBoxItem(periodo.Descricao, periodo.Codigo.ToString());

                item.Attributes.Add("Codigo", periodo.Codigo.ToString());

                cboPeriodo.Items.Add(item);
                item.DataBind();
            }
        }

        private void LimparControle()
        {
            var controle = cboPeriodo as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboPeriodo.ClearSelection();
        }

        public string Codigo
        {
            get { return cboPeriodo.SelectedValue; }
            set
            {
                var periodo = Periodo.ObtenhaPorCodigo(Convert.ToInt32(value));

                if (periodo != null)
                {
                    cboPeriodo.SelectedValue = periodo.Codigo.ToString();
                    cboPeriodo.Text = periodo.Descricao;
                }
            }
        }

        public Periodo PeriodoSelecionado
        {
            get { return (Periodo)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        protected void cboPeriodo_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboPeriodo.AutoPostBack) return;

            Periodo periodo = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
            {
                LimparControle();
                return;
            }

            periodo = Periodo.ObtenhaPorCodigo(Convert.ToInt32(((RadComboBox)o).SelectedValue));

            PeriodoSelecionado = periodo;

            if (PeriodoFoiSelecionado != null)
                PeriodoFoiSelecionado(periodo);
        }

        public bool AutoPostBack
        {
            set { cboPeriodo.AutoPostBack = value; }
        }
    }
}