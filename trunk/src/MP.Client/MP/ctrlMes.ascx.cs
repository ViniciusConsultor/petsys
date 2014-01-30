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
    public partial class ctrlMes : System.Web.UI.UserControl
    {
        //public event MesFoiSelecionadoEventHandler MesFoiSelecionado;
        //public delegate void MesFoiSelecionadoEventHandler(Mes mes);

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
            foreach (var mes in Mes.ObtenhaTodas())
            {
                var item = new RadComboBoxItem(mes.Descricao, mes.Codigo.ToString());

                item.Attributes.Add("Codigo", mes.Codigo.ToString());

                cboMes.Items.Add(item);
                item.DataBind();
            }
        }

        private void LimparControle()
        {
            var controle = cboMes as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboMes.ClearSelection();
        }

        public string Codigo
        {
            get { return cboMes.SelectedValue; }
            set
            {
                var mes = Mes.ObtenhaPorCodigo(Convert.ToInt32(value));

                if (mes != null)
                {
                    cboMes.SelectedValue = mes.Codigo.ToString();
                    cboMes.Text = mes.Descricao;
                }
            }
        }

        //public Mes MesSelecionado
        //{
        //    get { return (Mes)ViewState[ClientID]; }
        //    set { ViewState.Add(this.ClientID, value); }
        //}

        protected void cboMes_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            //if (!cboMes.AutoPostBack) return;

            //Mes mes = null;

            //if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
            //{
            //    LimparControle();
            //    return;
            //}

            //mes = Mes.ObtenhaPorCodigo(Convert.ToInt32(((RadComboBox) o).SelectedValue));

            //MesSelecionado = mes;

            //if (MesFoiSelecionado != null)
            //    MesFoiSelecionado(mes);
        }

        //public bool AutoPostBack
        //{
        //    set { cboMes.AutoPostBack = value; }
        //}
    }
}