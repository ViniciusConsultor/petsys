using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace FN.Client.FN
{
    public class GeradorDePopupsWeb : Singleton<GeradorDePopupsWeb>
    {
        public const string ConteudoDeExibicaoDePopup = "conteudoDeExibicaoDePopup";

        public void AbraPopupModal(string contextRoot, string titulo, int largura, int altura, string nomeComponente)
        {
            var paginaChamadora = (Page)HttpContext.Current.Handler;
            var sb = new StringBuilder();

            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "cssDoJQueryUIDialog",
                string.Format("<link href='{0}/scripts/jquery/ui/jquery-ui.css' rel='stylesheet' type='text/css' media='all' />", contextRoot));

            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "jQuery",
                string.Format("<script src='{0}/scripts/jquery/jquery.js'></script>", contextRoot));

            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "jQueryUIDialog",
                string.Format("<script src='{0}/scripts/jquery/ui/jquery-ui.js'></script>", contextRoot));


            sb.Append("<script>");
            sb.Append("$(document).ready(function() {");
            sb.Append("$('#" + nomeComponente + "').dialog({height: " + altura + ", width: " + largura + " ,modal: true, closeOnEscape: false, title: '" + titulo + "', open: function(event, ui) { $('.ui-dialog-titlebar-close').hide(); }});});");
            sb.Append("</script>");

            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "JQueryUIDialog", sb.ToString());
        }

        public void abraPopup(string titulo, string contextRoot, string URL, int largura, int altura, bool fecha, bool abrirNaMesmaPaginaSeEstiverEmPopup)
        {
            //var gereciadorDePopup = new GerenciadorDePopup();

            var paginaChamadora = (Page)HttpContext.Current.Handler;
            var paginaAberta = paginaChamadora.Request.AppRelativeCurrentExecutionFilePath;

            //if (gereciadorDePopup.PaginaNaoPermitePopup(paginaAberta))
            //    paginaChamadora.Response.Redirect(string.Format("{0}/{1}", contextRoot, URL), true);
            //else
            //    abraPopup(titulo, contextRoot, URL, largura, altura, fecha);

            abraPopup(titulo, contextRoot, URL, largura, altura, fecha);
        }

        public void abraPopupMobile(string titulo, string contextRoot, string URL, int largura, int altura, bool fecha)
        {
            var paginaChamadora = (Page)HttpContext.Current.Handler;
            paginaChamadora.Response.Redirect(string.Format("{0}/{1}", contextRoot, URL), true);
        }

        public void abraPopup(string titulo, string contextRoot, string URL, int largura, int altura, bool fecha)
        {
            Page paginaChamadora = (Page)HttpContext.Current.Handler;
            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "init",
                "<script>var GB_ANIMATION = true;</script>");
            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "jQuery",
                string.Format("<script src='{0}/scripts/jquery/jquery.js'></script>", contextRoot));

            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "FileUpload",
                string.Format("<script src='{0}/scripts/UploaderList.js'></script>", contextRoot));
            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "ListaDeArquivos",
                string.Format("<script src='{0}/scripts/FileUploader.js'></script>", contextRoot));
            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "ExtensoesJQueryParaFileUpload",
                string.Format("<script src='{0}/scripts/jquery/jquery-extensions.js'></script>", contextRoot));


            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "jquery_bgIFrame",
                string.Format("<script src='{0}/scripts/jquery/plugins/BgIFrame/jquery_bgIFrame.js'></script>", contextRoot));
            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "GreyBox",
                string.Format("<script src='{0}/scripts/jquery/plugins/greybox/greybox.js'></script>", contextRoot));
            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), "cssDoGreyBox",
                string.Format("<link href='{0}/scripts/jquery/plugins/greybox/greybox.css' rel='stylesheet' type='text/css' media='all' />", contextRoot));

            string script = "" +
                "<script type='text/javascript'>" +
                    "$(document).ready(function(){" +
                        string.Format("GB_show('{0}', '{1}', '{2}', {3},{4},'{5}');", contextRoot, titulo.Replace("'", "\\'"), URL, altura, largura, fecha) +
                //"$(function() {$('#GB_overlay').bgiframe();});" +
                                "});" +
                                    "</script>";

            paginaChamadora.ClientScript.RegisterStartupScript(typeof(GeradorDePopupsWeb), ConteudoDeExibicaoDePopup, script);
        }
    }
}