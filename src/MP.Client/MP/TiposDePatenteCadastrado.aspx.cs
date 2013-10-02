using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class TiposDePatenteCadastrado : System.Web.UI.Page
    {
        private const string ID_OBJETO = "ID_OBJETO";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ExibaTelaInicial();
            }
            
        }

        private void ExibaTelaInicial()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            Session[CHAVE_ESTADO] = Estado.Inicial;
            Session[ID_OBJETO] = null;

            // carregar grid Tipo de patentes Cadastradas
        }

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Consulta,
            Modifica,
            Remove
        }

        

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            //switch (((RadToolBarButton)e.Item).CommandName)
            //{
            //    case "btnNovo":
            //        btnNovo_Click();
            //        break;
            //    case "btnModificar":
            //        btnModificar_Click();
            //        break;
            //    case "btnExcluir":
            //        btnExclui_Click();
            //        break;
            //    case "btnSalvar":
            //        btnSalva_Click();
            //        break;
            //    case "btnCancelar":
            //        btnCancela_Click();
            //        break;
            //    case "btnSim":
            //        btnSim_Click();
            //        break;
            //    case "btnNao":
            //        btnNao_Click();
            //        break;
            //}
        }
    }
}