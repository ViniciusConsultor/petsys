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
            if(!IsPostBack)
                ExibaTelaInicial();

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
            EscondaTodosOsPanelsDeFiltro();
            pnlProtocolo.Visible = true;
            cboTipoDeFiltro.SelectedValue = "8";

            ctrlApresentacao1.Inicializa();
            ctrlNCL1.Inicializa();
            ctrlNatureza1.Inicializa();
            ctrlOperacaoFiltro1.Inicializa();
            ctrlCliente1.Inicializa();
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Apresentação","1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Cliente","2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Data de entrada","3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Marca","4"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Natureza","5"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("NCL","6"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Processo","7"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Protocolo","8"));

        }


        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EscondaTodosOsPanelsDeFiltro();

            switch (cboTipoDeFiltro.SelectedValue)
            {
                case "1" :
                    pnlApresentacao.Visible = true;
                    break;
                case  "2" :
                    pnlCliente.Visible = true;
                    break;
                case "3":
                    pnlDataDeEntrada.Visible = true;
                    break;
                case "4":
                    pnlMarca.Visible = true;
                    break;
                case "5":
                    pnlNatureza.Visible = true;
                    break;
                case "6":
                    pnlNCL.Visible = true;
                    break;
                case "7":
                    pnlProcesso.Visible = true;
                    break;
                case "8":
                    pnlProtocolo.Visible = true;
                    break;
            }
        }


        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlApresentacao.Visible = false;
            pnlCliente.Visible = false;
            pnlDataDeEntrada.Visible = false;
            pnlMarca.Visible = false;
            pnlNatureza.Visible = false;
            pnlNCL.Visible = false;
            pnlProcesso.Visible = false;
            pnlProcesso.Visible = false;
            pnlProtocolo.Visible = false;
        }


        protected void btnPesquisarPorApresentacao_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

        }

        protected void btnPesquisarPorCliente_OnClick_(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }
        }

        protected void btnPesquisarPorDataDeEntrada_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }
        }

        protected void btnPesquisarPorMarca_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }
        }

        protected void btnPesquisarPorNatureza_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }
        }

        protected void btnPesquisarPorNCL_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }
        }

        protected void btnPesquisarPorProcesso_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }
        }

        protected void btnPesquisarPorProtoloco_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }
        }

        private bool OpcaoDeOperacaodeFiltroEstaSelecionada()
        {
            return !string.IsNullOrEmpty(ctrlOperacaoFiltro1.Codigo);
        }

        private void ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
        {
           ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma opção de filtro"), false);
        }
    }
}