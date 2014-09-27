using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class ctrlDataDePagamentoContaAReceber : System.Web.UI.UserControl
    {
        public delegate void UsuarioPediuParaGravarEventHandler();
        public event UsuarioPediuParaGravarEventHandler UsuarioPediuParaGravar;

        protected void Page_Load(object sender, EventArgs e)
        {
            //btnReceber.Click += btnReceber_ButtonClick;
        }

        protected void btnReceber_ButtonClick(object sender, EventArgs e)
        {
            UsuarioPediuParaGravar();
        }

        public DateTime DataInformada()
        {
            return this.txtDataDeRecebimento.SelectedDate.Value;
        }
    }
}