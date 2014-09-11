using System;
using System.Web.UI;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmConfiguracaoDeIndicesFinaceiros : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ExibaTelaInicial()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracaoDeIndicesFinanceiros>())
            {
                var configuracao = servico.Obtenha();

                if (configuracao != null && configuracao.ValorDoSalarioMinimo.HasValue)
                    MostreConfiguracaoDeIndicesFinanceiros(configuracao);
            }
        }

        private void MostreConfiguracaoDeIndicesFinanceiros(IConfiguracaoDeIndicesFinanceiros configuracaoDeIndices)
        {
            txtValorSalarioMinimo.Value = configuracaoDeIndices.ValorDoSalarioMinimo;
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

        private IConfiguracaoDeIndicesFinanceiros ObtenhaConfiguracaoDeIndiceFinanceiro()
        {
            var configuracao = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeIndicesFinanceiros>();

            configuracao.ValorDoSalarioMinimo = txtValorSalarioMinimo.Value;
            return configuracao;
        }

        private void btnSalvar_Click()
        {
            var configuracao = ObtenhaConfiguracaoDeIndiceFinanceiro();
            
            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracaoDeIndicesFinanceiros>())
                    servico.Salve(configuracao);

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Configuração salva com sucesso."), false);

            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }

        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.FN.004";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }
    }
}