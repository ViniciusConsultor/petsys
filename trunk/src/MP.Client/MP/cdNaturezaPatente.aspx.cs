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
    public partial class cdNaturezaPatente : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_NATUREZAPATENTE";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_NATUREZAPATENTE";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlNaturezaPatente.NaturezaPatenteFoiSelecionada += MostreNaturezaPatente;

            if (!IsPostBack)
                this.ExibaTelaInicial();
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

            Control controlePanel = this.pnlDadosDoTipo;
            Control control = RadDock1;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);

            UtilidadesWeb.LimparComponente(ref controlePanel);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            ctrlNaturezaPatente.Inicializa();
            ctrlNaturezaPatente.EnableLoadOnDemand = true;
            ctrlNaturezaPatente.ShowDropDownOnTextboxClick = true;
            ctrlNaturezaPatente.AutoPostBack = true;
            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;
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

            Control controlePanel = this.pnlDadosDoTipo;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Novo;

            ctrlNaturezaPatente.Inicializa();
            ctrlNaturezaPatente.EnableLoadOnDemand = false;
            ctrlNaturezaPatente.ShowDropDownOnTextboxClick = false;
            ctrlNaturezaPatente.AutoPostBack = false;
            ctrlNaturezaPatente.TextoItemVazio = string.Empty;
            CarregueCombosFormulario();
        }

        // alterar este método
        private void CarregueCombosFormulario()
        {
            var itemNaoPgtoIntermediario = new RadComboBoxItem("Não", "0");
            var itemSimPgtoIntermediario = new RadComboBoxItem("Sim", "1");

            this.cbPgtoIntermediario.Items.Add(itemNaoPgtoIntermediario);
            this.cbPgtoIntermediario.Items.Add(itemSimPgtoIntermediario);

            itemNaoPgtoIntermediario.DataBind();
            itemSimPgtoIntermediario.DataBind();

            var itemNaoPgtoInterPedidoExame = new RadComboBoxItem("Não", "0");
            var itemSimPgtoInterPedidoExame = new RadComboBoxItem("Sim", "1");

            this.cbPgtoInterPedidoExame.Items.Add(itemNaoPgtoInterPedidoExame);
            this.cbPgtoInterPedidoExame.Items.Add(itemSimPgtoInterPedidoExame);

            itemNaoPgtoInterPedidoExame.DataBind();
            itemSimPgtoInterPedidoExame.DataBind();
        }

        private void ExibaTelaModificar()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            Control controlePanel = this.pnlDadosDoTipo;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
            ctrlNaturezaPatente.EnableLoadOnDemand = false;
            ctrlNaturezaPatente.ShowDropDownOnTextboxClick = false;
            ctrlNaturezaPatente.AutoPostBack =false;
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

            Control controlePanel = pnlDadosDoTipo;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            Control control = RadDock1;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);
        }

        protected void btnCancelar_Click()
        {
            ExibaTelaInicial();
        }

        private INaturezaPatente MontaObjetoTipoDePatente()
        {
            var naturezaPatente = FabricaGenerica.GetInstancia().CrieObjeto<INaturezaPatente>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                naturezaPatente.IdNaturezaPatente = Convert.ToInt64(ViewState[ID_OBJETO]);

            naturezaPatente.DescricaoNaturezaPatente = ctrlNaturezaPatente.DescricaoNaturezaPatente;
            naturezaPatente.SiglaNatureza = txtSigla.Text;
            naturezaPatente.DescricaoPagamento = txtDescricaoPagamento.Text;
            naturezaPatente.DescricaoPagamentoIntermediario = txtDescricaoPagamentoIntermediario.Text;
            naturezaPatente.SequenciaInicioPagamento = Convert.ToInt32(txtIniciarPagamentoSequencia.Text);
            naturezaPatente.TempoEntrePagamento = Convert.ToInt32(txtIntervaloPagamentos.Text);
            naturezaPatente.TempoEntrePagamentoIntermediario = Convert.ToInt32(txtIntervaloPagamentoIntermediario.Text);
            naturezaPatente.QuantidadePagamento = Convert.ToInt32(txtQuantidadePagamentos.Text);
            naturezaPatente.QuantidadePagamentoIntermediario = Convert.ToInt32(txtQuantidadePagamentoIntermediario.Text);
            naturezaPatente.InicioIntermediarioSequencia = Convert.ToInt32(txtSequenciaInicioPagamentoIntermediario.Text);
            naturezaPatente.TempoInicioAnos = Convert.ToInt32(txtTempoInicioPagamentos.Text);
            naturezaPatente.TemPedidoDeExame = cbPgtoInterPedidoExame.SelectedValue != "0";
            naturezaPatente.TemPagamentoIntermediario = cbPgtoIntermediario.SelectedValue != "0";

            return naturezaPatente;
        }

        private string validaErrosDePreenchimento()
        {
            string mensagem = string.Empty;

            if (string.IsNullOrEmpty(ctrlNaturezaPatente.DescricaoNaturezaPatente))
                mensagem = mensagem + "Descrição da natureza patente, ";

            if (string.IsNullOrEmpty(txtSigla.Text))
                mensagem = mensagem + "Sigla, ";
            
            if (string.IsNullOrEmpty(txtTempoInicioPagamentos.Text))
                mensagem = mensagem + "Tempo para início dos Pagtos , ";
            
            if (string.IsNullOrEmpty(txtQuantidadePagamentos.Text))
                mensagem = mensagem + "Quantidade de Pagtos. , ";
            
            if (string.IsNullOrEmpty(txtIntervaloPagamentos.Text))
                mensagem = mensagem + "Intervalo entre Pagtos , ";
            
            if (string.IsNullOrEmpty(txtIniciarPagamentoSequencia.Text))
                mensagem = mensagem + "Iniciar Pagtos. da sequencia , ";
            
            if (string.IsNullOrEmpty(txtDescricaoPagamento.Text))
                mensagem = mensagem + "Descrição para o Pagto , ";
            
            if (string.IsNullOrEmpty(txtIniciarPagamentoSequencia.Text))
                mensagem = mensagem + "Número de sequencia Pagtos intermediarios , ";
            
            if (string.IsNullOrEmpty(txtIntervaloPagamentoIntermediario.Text))
                mensagem = mensagem + "Intervalo de Pagto intermediário , ";
            
            if (string.IsNullOrEmpty(txtDescricaoPagamentoIntermediario.Text))
                mensagem = mensagem + "Descrição Pagto intermediarios , ";

            return mensagem;
        }

        private void btnSalvar_Click()
        {
            string mensagem;

            if (!string.IsNullOrEmpty(validaErrosDePreenchimento()))
            {
                var erros = validaErrosDePreenchimento();
                var mensagemDeErro = "Campo(s) " + erros + "precisa(m) ser preenchido(s)";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(mensagemDeErro), false);
                return;
            }

            var naturezaPatente = MontaObjetoTipoDePatente();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeNaturezaPatente>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        INaturezaPatente existeNaturezaPatenteCadastrado = verificaSeJaExisteNaturezaPatenteCadastrado(naturezaPatente.DescricaoNaturezaPatente, naturezaPatente.SiglaNatureza, servico);

                        if (existeNaturezaPatenteCadastrado != null)
                        {
                            mensagem = "Já existe uma natureza de patente cadastrada com esta descrição ou sigla";

                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(mensagem), false);
                            return;
                        }

                        servico.Inserir(naturezaPatente);
                        mensagem = "Natureza de patente cadastrada com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(naturezaPatente);
                        mensagem = "Natureza de patente modificada com sucesso.";
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

        private INaturezaPatente verificaSeJaExisteNaturezaPatenteCadastrado(string descricaoNaturezaPatente, string siglaTipo, IServicoDeNaturezaPatente servico)
        {
            var naturezaPatente = servico.obtenhaNaturezaPatentePelaDescricaoOuSigla(descricaoNaturezaPatente, siglaTipo);
            return naturezaPatente;
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
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeNaturezaPatente>())
                    servico.Excluir(Convert.ToInt64(ViewState[ID_OBJETO]));

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Natureza de patente excluída com sucesso."), false);
                ExibaTelaInicial();
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
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
                    btnSalvar_Click();
                    break;
                case "btnCancelar":
                    btnCancelar_Click();
                    break;
                case "btnSim":
                    btnSim_Click();
                    break;
                case "btnNao":
                    btnNao_Click();
                    break;
            }
        }

        private void ExibaTelaConsultar()
        {
            Control controle = pnlDadosDoTipo;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;
            UtilidadesWeb.HabilitaComponentes(ref controle, false);
        }

        private void MostreNaturezaPatente(INaturezaPatente naturezaPatente)
        {
            ViewState[ID_OBJETO] = naturezaPatente.IdNaturezaPatente.Value.ToString();

            ctrlNaturezaPatente.DescricaoNaturezaPatente = naturezaPatente.DescricaoNaturezaPatente;
            ctrlNaturezaPatente.SiglaTipo = naturezaPatente.SiglaNatureza;

            txtDescricaoPagamento.Text = naturezaPatente.DescricaoPagamento;
            txtDescricaoPagamentoIntermediario.Text = naturezaPatente.DescricaoPagamentoIntermediario;
            txtIniciarPagamentoSequencia.Text = naturezaPatente.SequenciaInicioPagamento.ToString();
            txtIntervaloPagamentos.Text = naturezaPatente.TempoEntrePagamento.ToString();
            txtIntervaloPagamentoIntermediario.Text = naturezaPatente.TempoEntrePagamentoIntermediario.ToString();
            txtQuantidadePagamentos.Text = naturezaPatente.QuantidadePagamento.ToString();
            txtQuantidadePagamentoIntermediario.Text = naturezaPatente.QuantidadePagamentoIntermediario.ToString();
            txtSequenciaInicioPagamentoIntermediario.Text = naturezaPatente.InicioIntermediarioSequencia.ToString();
            txtSigla.Text = naturezaPatente.SiglaNatureza;
            txtTempoInicioPagamentos.Text = naturezaPatente.TempoInicioAnos.ToString();

            CarregueCombosFormulario();

            cbPgtoInterPedidoExame.SelectedValue = naturezaPatente.TemPedidoDeExame ? "1" : "0";
            cbPgtoIntermediario.SelectedValue = naturezaPatente.TemPagamentoIntermediario ? "1" : "0";

            ExibaTelaConsultar();
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.001";
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