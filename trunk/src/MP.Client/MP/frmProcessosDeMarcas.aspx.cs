using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmProcessosDeMarcas : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.007";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkProcessosDeMarcas;
            UtilidadesWeb.LimparComponente(ref controle2);

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;

            CarregaOpcoesDeFiltro();


        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Apresentação","1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente","2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Data de entrada","3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Marca","3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Natureza","4"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("NCL","5"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Processo","6"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Protocolo","7"));

        }



    }
}