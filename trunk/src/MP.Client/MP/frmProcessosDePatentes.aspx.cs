﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmProcessosDePatentes : SuperPagina
    {
      
        private const string CHAVE_FILTRO_APLICADO = "CHAVE_FILTRO_APLICADO_PROCESSO_DE_PATENTE";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();

        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.009";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        private IFiltro FiltroAplicado
        {
            get { return (IFiltro)ViewState[CHAVE_FILTRO_APLICADO]; }
            set { ViewState[CHAVE_FILTRO_APLICADO] = value; }
        }

        private void ExibaTelaInicial()
        {
            Control controle1 = pnlFiltro;
            UtilidadesWeb.LimparComponente(ref controle1);

            Control controle2 = rdkProcessosDePatentes;
            UtilidadesWeb.LimparComponente(ref controle2);

            CarregaOpcoesDeFiltro();
            EscondaTodosOsPanelsDeFiltro();
            pnlProcesso.Visible = true;
            cboTipoDeFiltro.SelectedValue = "1";
            ctrlOperacaoFiltro1.Inicializa();
            ctrlTipoDePatente1.Inicializa();
            
            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatenteSemFiltro>();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        private void MostraProcessos(IFiltro filtro, int quantidadeDeProcessos, int offSet)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
            {
                grdProcessosDePatentes.VirtualItemCount = servico.ObtenhaQuantidadeDeProcessosCadastrados(filtro);
                grdProcessosDePatentes.DataSource = servico.ObtenhaProcessosDePatentes(filtro, quantidadeDeProcessos, offSet);
                grdProcessosDePatentes.DataBind();
            }
        }

        protected void btnPesquisarPorTituloDaPatente_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            if (string.IsNullOrEmpty(txtTituloPatente.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe o título da patente."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorTituloDaPatente>();
            filtro.Operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));
            filtro.ValorDoFiltro = txtDataDeEntrada.SelectedDate.Value.ToString("yyyyMMdd");
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        protected void btnPesquisarPorTipoDePatente_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            if (ctrlTipoDePatente1.TipoDePatenteSelecionada == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Selecione um tipo de patente."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorTipoDePatente>();
            filtro.Operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));
            filtro.ValorDoFiltro = ctrlTipoDePatente1.TipoDePatenteSelecionada.IdTipoDePatente.ToString();
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        private void CarregaOpcoesDeFiltro()
        {
            cboTipoDeFiltro.Items.Clear();
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Processo", "1"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Data de entrada", "2"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Título da patente", "3"));
            cboTipoDeFiltro.Items.Add(new RadComboBoxItem("Tipo de patente", "4"));
        }


        protected void cboTipoDeFiltro_OnSelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            EscondaTodosOsPanelsDeFiltro();

            switch (cboTipoDeFiltro.SelectedValue)
            {
                case "1":
                    pnlProcesso.Visible = true;
                    break;
                case "2":
                    pnlDataDeEntrada.Visible = true;
                    break;
                case "3":
                    pnlTituloPatente.Visible = true;
                    break;
                case "4":
                    pnlTipoDePatente.Visible = true;
                    break;
            }
        }


        private void EscondaTodosOsPanelsDeFiltro()
        {
            pnlProcesso.Visible = false;
            pnlDataDeEntrada.Visible = false;
            pnlTituloPatente.Visible = false;
            pnlTipoDePatente.Visible = false;
        }
        
        protected void btnPesquisarPorDataDeEntrada_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            if (!txtDataDeEntrada.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe uma data de entrada."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorDataDeEntrada>();
            filtro.Operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));
            filtro.ValorDoFiltro = txtDataDeEntrada.SelectedDate.Value.ToString("yyyyMMdd");
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
        }

        
        protected void btnPesquisarPorProcesso_OnClick(object sender, ImageClickEventArgs e)
        {
            if (!OpcaoDeOperacaodeFiltroEstaSelecionada())
            {
                ExibaMensagemDeFaltaDeSelecaoDaOpcaoDeFiltro();
                return;
            }

            if (!txtProcesso.Value.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInconsitencia("Informe um número de processo."), false);
                return;
            }

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPatentePorProcesso>();
            filtro.Operacao = OperacaoDeFiltro.Obtenha(Convert.ToByte(ctrlOperacaoFiltro1.Codigo));
            filtro.ValorDoFiltro = txtProcesso.Text;
            FiltroAplicado = filtro;
            MostraProcessos(filtro, grdProcessosDePatentes.PageSize, 0);
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

        protected void btnNovo_Click()
        {
            var URL = ObtenhaURLCadastrodePatente();
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(URL, "Novo processo de patente", 800, 600), false);
        }



        private string ObtenhaURLCadastrodePatente()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDePatente.aspx");
        }

        private string ObtenhaURLLeituraDeRevistaDePatente()
        {
            return String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDePatente.aspx");
        }

        private void Recarregue()
        {
            MostraProcessos(FiltroAplicado, grdProcessosDePatentes.PageSize, 0);
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnNovo":
                    btnNovo_Click();
                    break;
                case "btnRecarregar":
                    Recarregue();
                    break;
                case "btnLerRevista":

                    break;

            }
        }

        protected void grdProcessosDePatentes_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdProcessosDePatentes.PageSize;

                MostraProcessos(FiltroAplicado, grdProcessosDePatentes.PageSize, offSet);

            }
        }

        protected void grdProcessosDePatentes_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;
            int indiceSelecionado;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
                id = Convert.ToInt64((e.Item.Cells[4].Text));
                indiceSelecionado = e.Item.ItemIndex;
            }

            switch (e.CommandName)
            {
                case "Excluir":

                    try
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                        {
                            servico.Excluir(id);
                        }

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInformacao(
                                                                    "Processo de patente excluído com sucesso."), false);
                        ExibaTelaInicial();
                    }
                    catch (BussinesException ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                    }

                    break;
                case "Modificar":
                    var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/cdProcessoDePatente.aspx",
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Modificar processo de patente",
                                                                                       800, 600), false);
                    break;
            }
        }
    }
}