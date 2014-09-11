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
    public partial class frmGerarContaAReceber : SuperPagina
    {
        private const string IDS_ITENS = "ITENS_FINANCEIROS";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            Nullable<long> id = null;

            if (!String.IsNullOrEmpty(Request.QueryString["ItensFinanceiros"]))
                ViewState[IDS_ITENS] = Request.QueryString["ItensFinanceiros"];

            LimpaTela();
        }
        
        private IItemLancamentoFinanceiroRecebimento ObtenhaItemDeRecebimento(long id)
        {
            IItemLancamentoFinanceiroRecebimento itemLancamento = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                itemLancamento =  servico.Obtenha(id);
            
            itemLancamento.Situacao = Situacao.CobrancaEmAberto;
            return itemLancamento;

        }

        private void LimpaTela()
        {
            var controle = pnlDadosDosItens as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            ctrlFormaRecebimento.Inicializa();
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsitencias = new List<string>();

            if (ctrlFormaRecebimento.FormaDeRecebimentoSelecionada == null) inconsitencias.Add("É necessário informar o tipo do recebimento do lançamento financeiro.");
            
            return inconsitencias;
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

            var arrayDeItens = (ViewState[IDS_ITENS] as string).Split('|');
            var itens = new List<string>(arrayDeItens);


            try {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                {


                    foreach (var item in itens)
                    {
                        var itemDeLancamento = ObtenhaItemDeRecebimento(Convert.ToInt64(item));
                        itemDeLancamento.FormaDeRecebimento = ctrlFormaRecebimento.FormaDeRecebimentoSelecionada;
                        servico.Modifique(itemDeLancamento);
                    }
                }
                
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Itens financeiros de recebimento agora são itens de conta a receber."), false);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.AtualizaJanela(String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmGerenciamentoDeItensFinanceiros.aspx"), "FN_frmGerenciamentoDeItensFinanceiros_aspx"), false);
                
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