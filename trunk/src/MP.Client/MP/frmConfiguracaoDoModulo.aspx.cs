using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class frmConfiguracaoDoModulo : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();

        }

        private void ExibaTelaInicial()
        {
            ctrlCedente.Inicializa();
            ctrlCedente.BotaoNovoEhVisivel = true;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracoesDoModulo>())
            {
                var configuracao = servico.ObtenhaConfiguracao();

                if (configuracao != null)
                {
                    if (configuracao.ConfiguracaoDeBoletoBancario != null)
                        MostreConfiguracaoDeBoletoBancario(configuracao.ConfiguracaoDeBoletoBancario);

                    if (configuracao.ConfiguracaoDeIndicesFinanceiros != null)
                        MostreConfiguracaoDeIndicesFinanceiros(configuracao.ConfiguracaoDeIndicesFinanceiros);
                }

            }
        }
        
        private void MostreConfiguracaoDeIndicesFinanceiros(IConfiguracaoDeIndicesFinanceiros configuracaoDeIndices)
        {
            txtValorSalarioMinimo.Value = configuracaoDeIndices.ValorDoSalarioMinimo;
        }

        private void MostreConfiguracaoDeBoletoBancario(IConfiguracaoDeBoletoBancario configuracaoDeBoleto)
        {
            ctrlCedente.CedenteSelecionado = configuracaoDeBoleto.Cedente;

            if (!string.IsNullOrEmpty(configuracaoDeBoleto.ImagemDeCabecalhoDoReciboDoSacado))
                imgImagem.ImageUrl = configuracaoDeBoleto.ImagemDeCabecalhoDoReciboDoSacado;
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton) e.Item).CommandName)
            {
                case "btnSalvar":
                    btnSalvar_Click();
                    break;
            }
        }

        private IConfiguracaoDeIndicesFinanceiros ObtenhaConfiguracaoDeIndiceFinanceiro()
        {
            if (!txtValorSalarioMinimo.Value.HasValue) return null;

            var configuracao = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeIndicesFinanceiros>();

            configuracao.ValorDoSalarioMinimo = txtValorSalarioMinimo.Value;

            return configuracao;
        }

        private IConfiguracaoDeBoletoBancario ObtenhaConfiguracaoDeBoletoBancario()
        {
            if (ctrlCedente.CedenteSelecionado == null) return null;

            var configuracao = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeBoletoBancario>();

            configuracao.Cedente = ctrlCedente.CedenteSelecionado;
            
            if (!string.IsNullOrEmpty(imgImagem.ImageUrl))
                configuracao.ImagemDeCabecalhoDoReciboDoSacado = imgImagem.ImageUrl;

            return configuracao;
        }

        private void btnSalvar_Click()
        {
            var configuracao = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeModulo>();

            configuracao.ConfiguracaoDeIndicesFinanceiros = ObtenhaConfiguracaoDeIndiceFinanceiro();
            configuracao.ConfiguracaoDeBoletoBancario = ObtenhaConfiguracaoDeBoletoBancario();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracoesDoModulo>())
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
            return "FUN.MP.018";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        protected void uplImagem_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if (uplImagem.UploadedFiles.Count > 0)
                {
                    var arquivo = uplImagem.UploadedFiles[0];
                    var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_IMAGEM_MARCA);

                    UtilidadesWeb.CrieDiretorio(pastaDeDestino);

                    var caminhoArquivo = Path.Combine(pastaDeDestino, arquivo.GetNameWithoutExtension() + arquivo.GetExtension());

                    arquivo.SaveAs(caminhoArquivo);
                    imgImagem.ImageUrl = string.Concat(UtilidadesWeb.URL_IMAGEM_MARCA, "/", arquivo.GetNameWithoutExtension() + arquivo.GetExtension());
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar imagem, exceção: ", ex);
            }
        }
    }
}