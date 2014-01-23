using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmEnviaEmail : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpaTela();

                Nullable<long> id = null;
                string tipo = null;

                if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                    id = Convert.ToInt64(Request.QueryString["Id"]);

                if (!String.IsNullOrEmpty(Request.QueryString["Tipo"]))
                    tipo = Request.QueryString["Tipo"];

                if (id == null || tipo == null) return;

                if (tipo == "P")
                    MostraDadosDoEmailParaPatente(id.Value);
                else
                    MostraDadosDoEmailParaMarca(id.Value);
            }
        }

        private void MostraDadosDoEmailParaPatente (long id)
        {
           using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
           {
               var processo = servico.Obtenha(id);

               if (processo == null)
               {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                           UtilidadesWeb.MostraMensagemDeInconsitencia("O processo de patente não existe no Banco de Dados."),
                                                           false);
                   return;
               }

               if (processo.Despacho == null)
                   ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                           UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                               "O processo de patente ainda não possui despacho cadastrado. O corpo do e-mail não será preenchido automaticamente pelo template vinculado ao despacho."),
                                                           false);
               else
               {
                   if (processo.Despacho.TemplateDeEmail == null)
                       ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                           "O despacho do processo de patente não possui template de e-mail cadastrado. O corpo do e-mail não será preenchido automaticamente pelo template vinculado ao despacho."),
                                                       false);
                   else
                    ctrlTemplateDeEmail.TextoDoTemplate = processo.Despacho.TemplateDeEmail.Template;
               } 

               
           }
        }

        private void MostraDadosDoEmailParaMarca(long id)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
            {
                var processo = servico.Obtenha(id);

                if (processo == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                           UtilidadesWeb.MostraMensagemDeInconsitencia("O processo de marca não existe no Banco de Dados."),
                                                           false);
                    return;
                }

                if (processo.Despacho == null)
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "O processo de marca ainda não possui despacho cadastrado. O corpo do e-mail não será preenchido automaticamente pelo template vinculado ao despacho."),
                                                            false);
                else
                {
                    if (processo.Despacho.TemplateDeEmail == null)
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                            "O despacho do processo de marca não possui template de e-mail cadastrado. O corpo do e-mail não será preenchido automaticamente pelo template vinculado ao despacho."),
                                                        false);
                    else
                        ctrlTemplateDeEmail.TextoDoTemplate = processo.Despacho.TemplateDeEmail.Template;
                }


            }
        }

        private void LimpaTela()
        {
            ctrlTemplateDeEmail.Inicializa();
            var controle = pnlDadosDoEmail as Control;
            UtilidadesWeb.LimparComponente(ref controle);
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            
        }

        protected void uplAnexos_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            
        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }
    }
}