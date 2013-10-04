using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class cdTipoDePatente : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_TIPODEPATENTE";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_TIPODEPATENTE";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlTipoDePatente.TipoDePatenteFoiSelecionada += MostreTipoDePatente;

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

            Control controlePanel = this.PanelCdTipoDePatente;

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            PanelCdTipoDePatente.Visible = false;
            ctrlTipoDePatente.Visible = true;

            ctrlTipoDePatente.Inicializa();
            //ctrlTipoDePatente.BotaoDetalharEhVisivel = false;
            //ctrlTipoDePatente.BotaoNovoEhVisivel = true;
            ctrlTipoDePatente.EnableLoadOnDemand = true;
            ctrlTipoDePatente.ShowDropDownOnTextboxClick = true;
            ctrlTipoDePatente.AutoPostBack = true;
            ctrlTipoDePatente.EhObrigatorio = false;

            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;

            // talvez carregue combo
        }

        protected void btnNovo_Click()
        {
            ExibaTelaNovo();
        }

        private void ExibaTelaNovo()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            Control controlePanel = this.PanelCdTipoDePatente;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);

            PanelCdTipoDePatente.Visible = true;
            ctrlTipoDePatente.Visible = false;
            //ctrlTipoDePatente.vi


            ViewState[CHAVE_ESTADO] = Estado.Novo;

            ctrlTipoDePatente.Inicializa();
            ctrlTipoDePatente.EnableLoadOnDemand = false;
            ctrlTipoDePatente.ShowDropDownOnTextboxClick = false;
            ctrlTipoDePatente.AutoPostBack = false;
            ctrlTipoDePatente.EhObrigatorio = true;
            ctrlTipoDePatente.TextoItemVazio = string.Empty;

            CarregueCombosFormulario();
        }

        private void CarregueCombosFormulario()
        {
            var itemNaoInterPedidoExame = new RadComboBoxItem("Não", "0");
            var itemSimInterPedidoExame = new RadComboBoxItem("Sim", "1");

            cbPgtoInterPedidoExame.Items.Add(itemNaoInterPedidoExame);
            cbPgtoInterPedidoExame.Items.Add(itemSimInterPedidoExame);

            itemNaoInterPedidoExame.DataBind();
            itemSimInterPedidoExame.DataBind();

            cbPgtoInterPedidoExame.ClearSelection();

            var itemNaoIntermediario = new RadComboBoxItem("Não", "0");
            var itemSimIntermediario = new RadComboBoxItem("Sim", "1");

            cbPgtoIntermediario.Items.Add(itemNaoIntermediario);
            cbPgtoIntermediario.Items.Add(itemSimIntermediario);

            itemNaoIntermediario.DataBind();
            itemNaoIntermediario.DataBind();

            cbPgtoIntermediario.ClearSelection();
        }

        private void ExibaTelaModificar()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            Control controlePanel = this.PanelCdTipoDePatente;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Modifica;

            ctrlTipoDePatente.EnableLoadOnDemand = false;
            ctrlTipoDePatente.ShowDropDownOnTextboxClick = false;
        }

        private void ExibaTelaExcluir()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = true;

            ViewState[CHAVE_ESTADO] = Estado.Remove;

            Control controlePanel = this.PanelCdTipoDePatente;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private ITipoDePatente MontaObjetoTipoDePatente()
        {
            var tipoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDePatente>();

            if (!Convert.ToByte(ViewState[CHAVE_ESTADO]).Equals(Estado.Novo))
            {
                tipoDePatente.IdTipoDePatente = Convert.ToInt64(ViewState[ID_OBJETO]);
            }

            tipoDePatente.DescricaoTipoDePatente = this.txtDescricao.Text;
            tipoDePatente.SiglaTipo = this.txtSigla.Text;

            tipoDePatente.DescricaoPagamentoIntermediario = this.txtDescricaoPagamentoIntermediario.Text;
            tipoDePatente.SequenciaInicioPagamento = Convert.ToInt32(this.txtIniciarPagamentoSequencia.Text);
            tipoDePatente.TempoEntrePagamento = Convert.ToInt32(this.txtIntervaloPagamentos.Text);
            tipoDePatente.TempoEntrePagamentoIntermediario = Convert.ToInt32(txtIntervaloPagamentoIntermediario.Text);
            tipoDePatente.QuantidadePagamento = Convert.ToInt32(this.txtQuantidadePagamentos.Text);
            tipoDePatente.QuantidadePagamentoIntermediario =
                Convert.ToInt32(this.txtQuantidadePagamentoIntermediario.Text);
            tipoDePatente.InicioIntermediarioSequencia = Convert.ToInt32(this.txtSequenciaInicioPagamentoIntermediario.Text);

            tipoDePatente.TempoInicioAnos = Convert.ToInt32(this.txtTempoInicioPagamentos.Text);

            if(cbPgtoInterPedidoExame.SelectedValue == "0" )
            {
                //tipoDePatente.TemPedidoDeExame = "0"
            }
            else
            {
                
            }

            if(cbPgtoIntermediario.SelectedValue == "0")
            {
                
            }
            else
            {
                
            }

            return tipoDePatente;
        }

        private void btnSalva_Click()
        {
            string mensagem;
            var tipoDePatente = MontaObjetoTipoDePatente();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDePatente>())
                {
                    if (!Convert.ToByte(ViewState[CHAVE_ESTADO]).Equals(Estado.Novo))
                    {
                        servico.Inserir(tipoDePatente);
                        mensagem = "Tipo de patente cadastrada com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(tipoDePatente);
                        mensagem = "Tipo de patente modificada com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(mensagem), false);
                ExibaTelaInicial();

            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }
        }

        private void btnModificar_Click()
        {
            ExibaTelaModificar();
        }

        private void btnExclui_Click()
        {
            ExibaTelaExcluir();
        }

        private void btnNao_Click()
        {
            ExibaTelaInicial();
        }

        private void btnSim_Click()
        {
            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDePatente>())
                {
                    servico.Excluir(Convert.ToInt64(ViewState[ID_OBJETO]));
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            "Pessoa excluida com sucesso."), false);
                ExibaTelaInicial();
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnNovo":
                    btnNovo_Click();
                    break;
                case "btnModificar":
                    btnModificar_Click();
                    break;
                case "btnExcluir":
                    btnExclui_Click();
                    break;
                case "btnSalvar":
                    btnSalva_Click();
                    break;
                case "btnCancelar":
                    btnCancela_Click();
                    break;
                case "btnSim":
                    btnSim_Click();
                    break;
                case "btnNao":
                    btnNao_Click();
                    break;
            }
        }

        private void MostreTipoDePatente(ITipoDePatente tipoDePatente)
        {
            ViewState[ID_OBJETO] = tipoDePatente.IdTipoDePatente.Value.ToString();

            ctrlTipoDePatente.DescricaoTipoDePatente = tipoDePatente.DescricaoTipoDePatente;
            ctrlTipoDePatente.SiglaTipo = tipoDePatente.SiglaTipo;
            this.txtDescricaoPagamento.Text = tipoDePatente.DescricaoPagamento;
            this.txtDescricaoPagamentoIntermediario.Text = tipoDePatente.DescricaoPagamentoIntermediario;
            this.txtDescricao.Text = tipoDePatente.DescricaoTipoDePatente;
            this.txtIniciarPagamentoSequencia.Text = tipoDePatente.SequenciaInicioPagamento.ToString();
            this.txtIntervaloPagamentos.Text = tipoDePatente.TempoEntrePagamento.ToString();
            this.txtIntervaloPagamentoIntermediario.Text = tipoDePatente.TempoEntrePagamentoIntermediario.ToString();
            this.txtQuantidadePagamentos.Text = tipoDePatente.QuantidadePagamento.ToString();
            this.txtQuantidadePagamentoIntermediario.Text = tipoDePatente.QuantidadePagamentoIntermediario.ToString();
            this.txtSequenciaInicioPagamentoIntermediario.Text = tipoDePatente.InicioIntermediarioSequencia.ToString();
            this.txtSigla.Text = tipoDePatente.SiglaTipo;
            this.txtTempoInicioPagamentos.Text = tipoDePatente.TempoInicioAnos.ToString();

            if (tipoDePatente.TemPedidoDeExame)
            {
                this.cbPgtoInterPedidoExame.SelectedValue = "SIM";
            }
            else
            {
                this.cbPgtoInterPedidoExame.SelectedValue = "NAO";
            }

            if (tipoDePatente.TemPagamentoIntermediario)
            {
                this.cbPgtoIntermediario.SelectedValue = "SIM";
            }
            else
            {
                this.cbPgtoIntermediario.SelectedValue = "NAO";
            }

            //ExibaTelaConsultar();
        }

        protected override string ObtenhaIdFuncao()
        {
            return string.Empty;
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Consulta,
            Modifica,
            Remove
        }
    }
}