using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using PMP.Interfaces.Servicos;
using Telerik.Web.UI;
using Compartilhados;

namespace PMP.Client.PMP
{
    public partial class frmProcessarEmLote : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void uplRevista_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            var pastaDeDestino = Server.MapPath(UtilPMP.URL_REVISTA_MARCA_PESQUISA);
            
            pastaDeDestino = Path.Combine(pastaDeDestino, Guid.NewGuid().ToString());

            try
            {
                if (uplRevista.UploadedFiles.Count > 0)
                {
                    Util.CrieDiretorio(pastaDeDestino);

                    foreach (UploadedFile arquivo in  uplRevista.UploadedFiles)
                        arquivo.SaveAs(Path.Combine(pastaDeDestino, arquivo.GetName()));
                    
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarcaDeRevista>())
                        servico.ProcesseEmLote(pastaDeDestino);

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInformacao("Processamento realizado com sucesso."), false);
                    
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao processar dados para o banco de dados de pesquisa de marcas.", ex);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb. MostraMensagemDeInconsitencia("Erro ao processar dados para o banco de dados de pesquisa de marcas. Consulte o log para detalhar o problema."), false);
                
            }
            finally
            {
                Util.ApagueDiretorio(pastaDeDestino, true);
            }

            
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.PMP.001";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}