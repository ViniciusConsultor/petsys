using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.FN.Servicos;
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmConfiguracaoGeralFinaceiro : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CarregueConfiguracaoGeral();
        }

        private void CarregueConfiguracaoGeral()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracaoGeralFinanceiro>())
                MostreConfiguracao(servico.Obtenha());
        }

        private void MostreConfiguracao(IConfiguracaoGeralFinanceiro configuracaoGeral)
        {
            if(configuracaoGeral == null)
                return;

            if (!string.IsNullOrEmpty(configuracaoGeral.InstrucoesDoBoleto))
                txtInstrucoesDoBoleto.Text = configuracaoGeral.InstrucoesDoBoleto;
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

        private bool PodeSalvarConfiguracao()
        {
            if(string.IsNullOrEmpty(txtInstrucoesDoBoleto.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Informe as instruções do boleto."), false);
                return false;
            }

            return true;
        }

        private IConfiguracaoGeralFinanceiro ObtenhaConfiguracao()
        {
            var configuracao = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoGeralFinanceiro>();

            configuracao.InstrucoesDoBoleto = txtInstrucoesDoBoleto.Text;
            return configuracao;
        }

        private void btnSalvar_Click()
        {
            if(!PodeSalvarConfiguracao())
                return;

            var configuracao = ObtenhaConfiguracao();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracaoGeralFinanceiro>())
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
            return "FUN.FN.006";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }
    }
}