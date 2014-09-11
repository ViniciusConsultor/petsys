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
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmConfiguracaoDoBoleto : SuperPagina
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

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
            {
                var configuracao = servico.ObtenhaConfiguracao();

                if (configuracao != null)
                    MostreConfiguracaoDeBoletoBancario(configuracao);
            }
        }

        private void MostreConfiguracaoDeBoletoBancario(IConfiguracaoDeBoletoBancario configuracaoDeBoleto)
        {
            ctrlCedente.CedenteSelecionado = configuracaoDeBoleto.Cedente;

            if (!string.IsNullOrEmpty(configuracaoDeBoleto.ImagemDeCabecalhoDoReciboDoSacado))
                imgImagem.ImageUrl = configuracaoDeBoleto.ImagemDeCabecalhoDoReciboDoSacado;
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

        private IConfiguracaoDeBoletoBancario ObtenhaConfiguracaoDeBoletoBancario()
        {
            var configuracao = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeBoletoBancario>();

            configuracao.Cedente = ctrlCedente.CedenteSelecionado;

            if (!string.IsNullOrEmpty(imgImagem.ImageUrl))
                configuracao.ImagemDeCabecalhoDoReciboDoSacado = imgImagem.ImageUrl;

            return configuracao;
        }

        private void btnSalvar_Click()
        {
            var configuracaoDeBoletoBancario = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeBoletoBancario>();

            configuracaoDeBoletoBancario = ObtenhaConfiguracaoDeBoletoBancario();

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                    servico.SalveConfiguracao(configuracaoDeBoletoBancario);

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
            return "FUN.FN.003";
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
                    var pastaDeDestino = Server.MapPath(Util.URL_IMAGEM_CABECALHO_BOLETO);

                    Compartilhados.Util.CrieDiretorio(pastaDeDestino);

                    var caminhoArquivo = Path.Combine(pastaDeDestino, arquivo.GetNameWithoutExtension() + arquivo.GetExtension());

                    arquivo.SaveAs(caminhoArquivo);
                    imgImagem.ImageUrl = string.Concat(Util.URL_IMAGEM_CABECALHO_BOLETO, "/", arquivo.GetNameWithoutExtension() + arquivo.GetExtension());
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar imagem, exceção: ", ex);
            }
        }
    }
}