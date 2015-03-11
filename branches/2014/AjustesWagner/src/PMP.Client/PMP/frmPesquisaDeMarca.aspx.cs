using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Negocio.Filtros.Marca;
using PMP.Interfaces.Servicos;
using PMP.Interfaces.Utilidades;
using Telerik.Web.UI;

namespace PMP.Client.PMP
{
    public partial class frmPesquisaDeMarca : System.Web.UI.Page
    {

        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_PESQUISA_DE_MARCA";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkProcessosDeMarcas;
            UtilidadesWeb.LimparComponente(ref controle2);

            ctrlOperacaoFiltro1.Inicializa();
            ctrlOperacaoFiltro1.Codigo = OperacaoDeFiltro.IgualA.ID.ToString();
            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();

            CarregaOpcoesRevista();
            rblOpcaoDeRevista.SelectedValue = "T";
            txtNumeroRevista.Visible = false;
            txtNumeroRevista.Text = string.Empty;
            txtNumeroRevista.Value = null;
            
            pnlProcesso.Visible = true;
            cboTipoDeFiltro.SelectedValue = "3";

            grdProcessosDeMarcas.DataSource = new List<DTOProcessoMarcaRevista>();
            grdProcessosDeMarcas.DataBind();
        }

        private void CarregaOpcoesRevista()
        {
            rblOpcaoDeRevista.Items.Clear();
            rblOpcaoDeRevista.Items.Add(new ListItem("Todas","T"));
            rblOpcaoDeRevista.Items.Add(new ListItem("Especifica", "E"));
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Titular", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Marca", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Processo", "3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Procurador", "4"));
        }

        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlTitular.Visible = false;
            pnlMarca.Visible = false;
            pnlProcesso.Visible = false;
            pnlProcurador.Visible = false;
        }

        private IFiltro FiltroAplicado
        {
            get { return (IFiltro)ViewState[CHAVE_FILTRO_APLICADO]; }
            set { ViewState[CHAVE_FILTRO_APLICADO] = value; }
        }

        private void MostraProcessos(IFiltro filtro, int quantidadeDeProcessos, int offSet)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarcaDeRevista>())
            {
                grdProcessosDeMarcas.VirtualItemCount = servico.ObtenhaQuantidadeDeResultadoDaPesquisa(filtro);
                grdProcessosDeMarcas.DataSource = servico.ObtenhaResultadoDaPesquisa(filtro, quantidadeDeProcessos,
                                                                                     offSet);
                grdProcessosDeMarcas.DataBind();
            }

        }

        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EscondaTodosOsPanelsDeFiltro();

            switch (cboTipoDeFiltro.SelectedValue)
            {
                case "1":
                    pnlTitular.Visible = true;
                    break;
                case "2":
                    pnlMarca.Visible = true;
                    break;
                case "3":
                    pnlProcesso.Visible = true;
                    break;
                case "4":
                    pnlProcurador.Visible = true;
                    break;
            }
        }

        private bool OpcaoDeOperacaodeFiltroEstaSelecionada()
        {
            return !string.IsNullOrEmpty(ctrlOperacaoFiltro1.Codigo);
        }

        private void ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro()
        {
            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione uma opção de filtro."), false);
        }

        protected void btnPesquisarPorTitular_OnClick_(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            if (operacao.Equals(OperacaoDeFiltro.Intervalo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), false);
                return;
            }
            
            if (string.IsNullOrEmpty(txtTitular.Text) && string.IsNullOrEmpty(txtUF.Text) && string.IsNullOrEmpty(txtPais.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                            "É necessário informar o nome ou a UF ou o País do Titular."), false);
                return;

            }
            
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorTitular>();
            filtro.ValorDoFiltro = txtTitular.Text;
            filtro.UF = txtUF.Text;
            filtro.Pais = txtPais.Text;
            filtro.Operacao = operacao;
            filtro.NumeroDaRevista = (int?)txtNumeroRevista.Value;

            FiltroAplicado = filtro;
            

            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void btnPesquisarPorMarca_OnClick(object sender, ImageClickEventArgs e)
        {

            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            if (operacao.Equals(OperacaoDeFiltro.Intervalo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), false);
                return;
            }

            if (string.IsNullOrEmpty(txtMarca.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                            "É necessário informar o nome da Marca."), false);
                return;

            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorMarca>();
            filtro.ValorDoFiltro = txtMarca.Text;
            filtro.NCL = txtNCL.Text;
            filtro.Operacao = operacao;
            FiltroAplicado = filtro;
            filtro.NumeroDaRevista = (int?)txtNumeroRevista.Value;

            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void btnPesquisarPorProcesso_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            if (operacao.Equals(OperacaoDeFiltro.Intervalo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), false);
                return;
            }

            if (string.IsNullOrEmpty(txtProcesso.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                            "É necessário informar o número do Processo."), false);
                return;

            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorNumeroDeProcesso>();
            filtro.ValorDoFiltro = txtProcesso.Text;
            filtro.Operacao = operacao;

            FiltroAplicado = filtro;
            filtro.NumeroDaRevista = (int?)txtNumeroRevista.Value;

            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void btnPesquisarPorProcurador_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            var operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));

            if (operacao.Equals(OperacaoDeFiltro.Intervalo))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Para o filtro selecionado essa opção de filtro não está disponível."), false);
                return;
            }

            if (string.IsNullOrEmpty(txtProcurador.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                            "É necessário informar o nome do Procurador."), false);
                return;

            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorProcurador>();
            filtro.ValorDoFiltro = txtProcurador.Text;
            filtro.Operacao = operacao;
            FiltroAplicado = filtro;
            filtro.NumeroDaRevista = (int?)txtNumeroRevista.Value;

            MostraProcessos(filtro, grdProcessosDeMarcas.PageSize, 0);
        }

        protected void grdProcessosDeMarcas_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdProcessosDeMarcas.PageSize;

                MostraProcessos(FiltroAplicado, grdProcessosDeMarcas.PageSize, offSet);

            }
        }

        protected void grdProcessosDeMarcas_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }

        protected void grdProcessosDeMarcas_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            
        }


        protected void rblOpcaoDeRevista_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var opcao = sender as RadioButtonList;

            txtNumeroRevista.Text = string.Empty;
            txtNumeroRevista.Value = null;

            switch (opcao.SelectedValue)
            {
                case "T" :
                    txtNumeroRevista.Visible = false;
                    break;
                case "E" :
                    txtNumeroRevista.Visible = true;
                    break;
            }
            
        }
    }
}