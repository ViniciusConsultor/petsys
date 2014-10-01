using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;

namespace FN.Client.FN
{
    public partial class ctrlJustificativaCancelamentoContaAReceber : UserControl
    {
        public delegate void UsuarioPediuParaCancelarEventHandler();
        public event UsuarioPediuParaCancelarEventHandler UsuarioPediuParaCancelar;

        public delegate void UsuarioPediuParaFecharEventHandler();
        public event UsuarioPediuParaFecharEventHandler UsuarioPediuParaFechar;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancelar_ButtonClick(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(JustificativaInformada()))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                               UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                   "A justificativa do cancelamento deve ser informada."), false);
                return;
            }

            UsuarioPediuParaCancelar();
        }

        public string JustificativaInformada()
        {
            return txtJustificativa.Text;
        }

        protected void btnFechar_ButtonClick(object sender, EventArgs e)
        {
            UsuarioPediuParaFechar();
        }

        public void LimparJustificativa()
        {
            txtJustificativa.Text = string.Empty;
        }
    }
}