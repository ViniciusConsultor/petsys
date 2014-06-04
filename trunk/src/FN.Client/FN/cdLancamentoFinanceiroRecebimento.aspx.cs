using System;
using System.Collections.Generic;
using System.Web.UI;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using Telerik.Web.UI;


namespace FN.Client.FN
{
    public partial class cdLancamentoFinanceiroRecebimento : SuperPagina
    {
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_ITEM_FINANCEIRO_RECEBIMENTO";
        private const string CHAVE_ID_LANCAMENTO_RECEBIMENTO = "CHAVE_ID_LANCAMENTO_RECEBIMENTO";

        private enum Estado : byte
        {
            Novo,
            Modifica,
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            Nullable<long> id = null;

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                id = Convert.ToInt64(Request.QueryString["Id"]);

            if (id == null)
                ExibaTelaNovo();
            else
                ExibaTelaDetalhes(id.Value);
        }

        private void MostreItemFinanceiro(IItemLancamentoFinanceiroRecebimento itemDeLacamento)
        {
            ViewState[CHAVE_ID_LANCAMENTO_RECEBIMENTO] = itemDeLacamento.ID.Value;

            ctrlCliente.ClienteSelecionado = itemDeLacamento.Cliente;
            txtDataDoLancamento.SelectedDate = itemDeLacamento.DataDoLancamento;
            
            ctrlTipoLacamentoFinanceiroRecebimento.TipoLacamentoSelecionado = itemDeLacamento.TipoLacamento;
            ctrlTipoLacamentoFinanceiroRecebimento.Codigo = itemDeLacamento.TipoLacamento.ID.ToString();
            txtObservacao.Text = itemDeLacamento.Observacao;
            txtValor.Value = itemDeLacamento.Valor;
            txtDataDoVencimento.SelectedDate = itemDeLacamento.DataDoVencimento;
            txtDescricao.Text = itemDeLacamento.Descricao;
        }

        private void ExibaTelaDetalhes(long id)
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
            LimpaTela();

            IItemLancamentoFinanceiroRecebimento itemLancamento = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                itemLancamento = servico.Obtenha(id);

            if (itemLancamento != null) MostreItemFinanceiro(itemLancamento);
        }

        private void ExibaTelaNovo()
        {
            ViewState[CHAVE_ESTADO] = Estado.Novo;
            LimpaTela();
            txtDataDoLancamento.SelectedDate = DateTime.Now;
        }

        private void LimpaTela()
        {
            ViewState[CHAVE_ID_LANCAMENTO_RECEBIMENTO] = null;

            var controle = pnlDadosDaConta as Control;
            UtilidadesWeb.LimparComponente(ref controle);

            ctrlCliente.Inicializa();
            ctrlTipoLacamentoFinanceiroRecebimento.Inicializa();
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsitencias = new List<string>();

            if (ctrlCliente.ClienteSelecionado == null) inconsitencias.Add("É necessário informar cliente.");
            if (!txtDataDoLancamento.SelectedDate.HasValue) inconsitencias.Add("É necessário informar a data do lançamento.");
            if (!txtDataDoVencimento.SelectedDate.HasValue) inconsitencias.Add("É necessário informar a data de vencimento.");
            if (!txtValor.Value.HasValue) inconsitencias.Add("É necessário informar o valor do lançamento.");
            if (ctrlTipoLacamentoFinanceiroRecebimento.TipoLacamentoSelecionado == null) inconsitencias.Add("É necessário informar o tipo do lançamento.");
            
            return inconsitencias;
        }

        private IItemLancamentoFinanceiroRecebimento MontaObjeto()
        {
            var itemDeLacamento = FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                itemDeLacamento.ID = Convert.ToInt64(ViewState[CHAVE_ID_LANCAMENTO_RECEBIMENTO]);

            itemDeLacamento.Cliente = ctrlCliente.ClienteSelecionado;
            itemDeLacamento.DataDoLancamento = txtDataDoLancamento.SelectedDate.Value;
            itemDeLacamento.DataDoVencimento = txtDataDoVencimento.SelectedDate.Value;
            itemDeLacamento.Situacao = Situacao.AguardandoCobranca;
            itemDeLacamento.TipoLacamento = ctrlTipoLacamentoFinanceiroRecebimento.TipoLacamentoSelecionado;
            itemDeLacamento.Observacao = txtObservacao.Text;
            itemDeLacamento.Valor = txtValor.Value.Value;
            itemDeLacamento.Descricao = txtDescricao.Text;

            return itemDeLacamento;
        }

        private void ExibaTelaModificar()
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
        }

        protected void btnSalvar_Click()
        {
            var inconsitencias = VerifiqueCamposObrigatorios();

            if (inconsitencias.Count != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsistencias(inconsitencias),
                                                        false);
                return;
            }

            var itemDeLacamento = MontaObjeto();
            string mensagem;

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Insira(itemDeLacamento);
                        mensagem = "Lançamento financeiro de recebimento inserido com sucesso.";
                    }
                    else
                    {
                        servico.Modifique(itemDeLacamento);
                        mensagem = "Lançamento financeiro de recebimento modificado com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(mensagem), false);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.AtualizaJanela(String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmGerenciamentoDeItensFinanceiros.aspx"), "FN/frmGerenciamentoDeItensFinanceiros_aspx"), false);

                ExibaTelaModificar();

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
                case "btnSalvar":
                    btnSalvar_Click();
                    break;
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.FN.007";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}