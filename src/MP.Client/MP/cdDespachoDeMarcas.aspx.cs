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
    public partial class cdDespachoDeMarcas : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_DESPACHODEMARCAS";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_DESPACHODEMARCAS";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlDespachoDeMarcas.DespachoDeMarcasFoiSelecionada += ExibaTelaDespachoDeMarcaSelecionado;

            if (!IsPostBack)
            {
                this.ExibaTelaInicial();
            }
        }

        private void ExibaTelaDespachoDeMarcaSelecionado(IDespachoDeMarcas despachoDeMarcas)
        {
            ViewState[ID_OBJETO] = despachoDeMarcas.IdDespacho.Value.ToString();

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
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

            Control controlePanel = this.PanelCdDespachoDeMarcas;

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            PanelCdDespachoDeMarcas.Visible = false;
            ctrlDespachoDeMarcas.Visible = true;

            ctrlDespachoDeMarcas.Inicializa();
            ctrlDespachoDeMarcas.EnableLoadOnDemand = true;
            ctrlDespachoDeMarcas.ShowDropDownOnTextboxClick = true;
            ctrlDespachoDeMarcas.AutoPostBack = true;

            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;
        }

        // alterar este método
        private void CarregueCombosFormulario()
        {
            cboConcessaoDeRegistro.Items.Clear();

            var itemNaoPgtoIntermediario = new RadComboBoxItem("Não", "0");
            var itemSimPgtoIntermediario = new RadComboBoxItem("Sim", "1");

            this.cboConcessaoDeRegistro.Items.Add(itemNaoPgtoIntermediario);
            this.cboConcessaoDeRegistro.Items.Add(itemSimPgtoIntermediario);

            itemNaoPgtoIntermediario.DataBind();
            itemSimPgtoIntermediario.DataBind();
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

            Control controlePanel = this.PanelCdDespachoDeMarcas;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Novo;

            PanelCdDespachoDeMarcas.Visible = true;
            ctrlDespachoDeMarcas.Visible = false;

            ctrlDespachoDeMarcas.Inicializa();
            ctrlDespachoDeMarcas.EnableLoadOnDemand = false;
            ctrlDespachoDeMarcas.ShowDropDownOnTextboxClick = false;
            ctrlDespachoDeMarcas.AutoPostBack = false;
            ctrlDespachoDeMarcas.TextoItemVazio = string.Empty;

            CarregueCombosFormulario();
        }

        private void ExibaTelaModificar()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            Control controlePanel = this.PanelCdDespachoDeMarcas;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Modifica;

            PanelCdDespachoDeMarcas.Visible = true;

            ctrlDespachoDeMarcas.Visible = false;
            ctrlDespachoDeMarcas.EnableLoadOnDemand = false;
            ctrlDespachoDeMarcas.ShowDropDownOnTextboxClick = false;

            CarregueCombosFormulario();

            MostreDespachoDeMarcas();
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

            Control controlePanel = this.PanelCdDespachoDeMarcas;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            PanelCdDespachoDeMarcas.Visible = false;
            ctrlDespachoDeMarcas.Visible = false;
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private IDespachoDeMarcas MontaObjetoDespachoDeMarcas()
        {
            var despachoDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
            {
                despachoDeMarcas.IdDespacho = Convert.ToInt64(ViewState[ID_OBJETO]);
            }

            despachoDeMarcas.CodigoDespacho = Convert.ToInt32(this.txtCodigo.Text);
            despachoDeMarcas.DetalheDespacho = txtDescricao.Text;
            despachoDeMarcas.Registro = cboConcessaoDeRegistro.SelectedValue;
            despachoDeMarcas.IdSituacaoProcesso = Convert.ToInt64(ctrlSituacaoDoProcesso.SituacaoDoProcessoSelecionada.IdSituacaoProcesso);

            return despachoDeMarcas;
        }

        private void btnSalva_Click()
        {
            string mensagem;
            var despachoDeMarcas = MontaObjetoDespachoDeMarcas();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(despachoDeMarcas);
                        mensagem = "Despacho de marcas cadastrado com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(despachoDeMarcas);
                        mensagem = "Despacho de marcas modificado com sucesso.";
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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
                {
                    servico.Excluir(Convert.ToInt64(ViewState[ID_OBJETO]));
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            "Despacho de marcas excluído com sucesso."), false);
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

        private void MostreDespachoDeMarcas()
        {
            var despachoDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();
            
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
            {
                despachoDeMarcas = servico.obtenhaDespachoDeMarcasPeloId(Convert.ToInt64(ViewState[ID_OBJETO]));
            }

            this.txtCodigo.Text = despachoDeMarcas.CodigoDespacho.ToString();
            this.txtDescricao.Text = despachoDeMarcas.DetalheDespacho;

            if (despachoDeMarcas.Registro.Equals("0"))
            {
                this.cboConcessaoDeRegistro.SelectedValue = "0";
            }
            else
            {
                this.cboConcessaoDeRegistro.SelectedValue = "1";
            }

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeSituacaoDoProcesso>())
            {
                var situacaoDoProcesso = servico.obtenhaSituacaoDoProcessoPeloId(despachoDeMarcas.IdSituacaoProcesso.Value);

                ctrlSituacaoDoProcesso.IdSituacaoProcesso = situacaoDoProcesso.DescricaoSituacao;
            }
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