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
    public partial class CadastroDeTiposDePatentes : SuperPagina
    {
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_TIPOPATENTE";
        private const string CHAVE_ID_TIPOPATENTE = "CHAVE_ID_TIPOPATENTE";

        private enum Estado : byte
        {
            Novo,
            Modifica,
            Remove
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var IdTipoPatente = default(long?);

                if (!string.IsNullOrEmpty(Request.QueryString["IdTipoPatente"]))
                {
                    IdTipoPatente = Convert.ToInt64(Request.QueryString["IdTipoPatente"]);
                }

                if (IdTipoPatente == null)
                {
                    this.ExibaTelaNovo();
                }
                else
                {
                    this.ExibaTelaDetalhes(IdTipoPatente.Value);
                }
            }
        }

        private void ExibaTelaDetalhes(long idTipoPatente)
        {
            if (ViewState != null)
                ViewState[CHAVE_ESTADO] = Estado.Modifica;

            LimpaDados();

            ITipoDePatente tipoDePatente;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDePatente>())
            {
                tipoDePatente = servico.obtenhaTipoDePatentePeloId(idTipoPatente);
            }

            if(tipoDePatente != null)
            {
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

                if(tipoDePatente.TemPedidoDeExame)
                {
                    this.cbPgtoInterPedidoExame.SelectedValue = "SIM";
                }
                else
                {
                    this.cbPgtoInterPedidoExame.SelectedValue = "NAO";
                }
                
                if(tipoDePatente.TemPagamentoIntermediario)
                {
                    this.cbPgtoIntermediario.SelectedValue = "SIM";
                }
                else
                {
                    this.cbPgtoIntermediario.SelectedValue = "NAO";
                }

                ViewState[CHAVE_ID_TIPOPATENTE] = tipoDePatente.IdTipoDePatente.Value.ToString();
            }
        }

        private void btnSalva_Click()
        {
            string Mensagem = "";
            ITipoDePatente tipoDePatente;
            string Inconsistencia = string.Empty;

            //Inconsistencia = ConsisteDados();

            if (!string.IsNullOrEmpty(Inconsistencia))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), new Guid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), false);
                return;
            }

            tipoDePatente = MontaObjeto();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDePatente>())
                {
                    if(Convert.ToByte(ViewState[CHAVE_ESTADO]).Equals(Estado.Novo))
                    {
                        servico.Inserir(tipoDePatente);
                        Mensagem = "Tipo de patente cadastrado com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(tipoDePatente);
                        Mensagem = "Tipo de patente modificado com sucesso.";
                    }
                }

                ExibaTelaNovo();

                ScriptManager.RegisterClientScriptBlock(this, GetType(), new Guid().ToString(), UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), false);
            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), new Guid().ToString(), UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }
        }

        private ITipoDePatente MontaObjeto()
        {
            ITipoDePatente tipoDePatente;

            tipoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<ITipoDePatente>();

            tipoDePatente.DescricaoPagamento = this.txtDescricaoPagamento.Text;
            tipoDePatente.DescricaoPagamentoIntermediario = this.txtDescricaoPagamentoIntermediario.Text;
            tipoDePatente.DescricaoTipoDePatente = this.txtDescricao.Text;
            tipoDePatente.SequenciaInicioPagamento = Convert.ToInt32(this.txtIniciarPagamentoSequencia.Text);
            tipoDePatente.TempoEntrePagamento = Convert.ToInt32(this.txtIntervaloPagamentos.Text);
            tipoDePatente.TempoEntrePagamentoIntermediario = Convert.ToInt32(txtIntervaloPagamentoIntermediario.Text);
            tipoDePatente.QuantidadePagamento = Convert.ToInt32(this.txtQuantidadePagamentos.Text);
            tipoDePatente.QuantidadePagamentoIntermediario =
                Convert.ToInt32(this.txtQuantidadePagamentoIntermediario.Text);
            tipoDePatente.InicioIntermediarioSequencia = Convert.ToInt32(this.txtSequenciaInicioPagamentoIntermediario.Text);
            tipoDePatente.SiglaTipo = this.txtSigla.Text;
            tipoDePatente.TempoInicioAnos = Convert.ToInt32(this.txtTempoInicioPagamentos.Text);

            //if (cbPgtoIntermediario.SelectedValue.Equals("SIM"))
            //    tipoDePatente.TemPagamentoIntermediario = 1;
            //else
            //    tipoDePatente.TemPagamentoIntermediario = 0;

            //if (cbPgtoInterPedidoExame.SelectedValue.Equals("SIM"))
            //    tipoDePatente.TemPedidoDeExame = 1;
            //else
            //    tipoDePatente.TemPedidoDeExame = 0;

            return tipoDePatente;
        }

        private void ExibaTelaNovo()
        {
            if (ViewState != null) 
                ViewState[CHAVE_ESTADO] = Estado.Novo;

            LimpaDados();
        }

        private void LimpaDados()
        {
            if (ViewState != null) 
                ViewState[CHAVE_ID_TIPOPATENTE] = null;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            this.txtDescricaoPagamento.Text = string.Empty;
            this.txtDescricaoPagamentoIntermediario.Text = string.Empty;
            txtDescricao.Text = string.Empty;
            txtIniciarPagamentoSequencia.Text = string.Empty;
            this.txtIntervaloPagamentos.Text = string.Empty;
            this.txtIntervaloPagamentoIntermediario.Text = string.Empty;
            this.txtQuantidadePagamentos.Text = string.Empty;
            this.txtQuantidadePagamentoIntermediario.Text = string.Empty;
            this.txtSequenciaInicioPagamentoIntermediario.Text = string.Empty;
            this.txtSigla.Text = string.Empty;
            this.txtTempoInicioPagamentos.Text = string.Empty;
            this.cbPgtoInterPedidoExame.ClearSelection();
            this.cbPgtoIntermediario.ClearSelection();
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                //case "btnNovo":
                //    btnNovo_Click();
                //    break;
                //case "btnModificar":
                //    btnModificar_Click();
                //    break;
                //case "btnExcluir":
                //    btnExclui_Click();
                //    break;
                //case "btnSalvar":
                //    btnSalva_Click();
                //    break;
                //case "btnCancelar":
                //    btnCancela_Click();
                //    break;
                //case "btnSim":
                //    btnSim_Click();
                //    break;
                //case "btnNao":
                //    btnNao_Click();
                //    break;
            }
        }

        protected void btnNovo_Click()
        {
            ExibaTelaNovo();
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

            ViewState[CHAVE_ESTADO] = Estado.Modifica;
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
        }

        protected void btnCancela_Click()
        {
            //ExibaTelaInicial();
        }

        protected override string ObtenhaIdFuncao()
        {
            throw new NotImplementedException();
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            throw new NotImplementedException();
        }
    }
}