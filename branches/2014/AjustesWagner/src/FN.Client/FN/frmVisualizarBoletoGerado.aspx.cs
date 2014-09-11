using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FN.Client.FN
{
    public partial class frmVisualizarBoletoGerado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            string Id = null;

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                Id = Request.QueryString["Id"];

            if (Id == null)
                return;
            
            ExibaBoleto(Id);
        }

        private void ExibaBoleto(string id)
        {
            var boleto = Session[id] as Control;

            pnlBoletoGerado.Controls.Add(boleto);

            Session[id] = null;
        }


        protected void btnPdf_click(object sender, EventArgs e)
        {
            var pastaDeDestinoTemp = Server.MapPath(Util.URL_IMAGEM_CABECALHO_BOLETO + "/temp/");

            var caminhoArquivoNovo = Path.Combine(pastaDeDestinoTemp, "Boleto.pdf");

            FazerDownloadArquivo("Boleto.pdf", caminhoArquivoNovo);
        }

        private void FazerDownloadArquivo(string nomeArquivo, string caminhoArquivo)
        {
            var arquivoTemporarioGerado = new StreamReader(caminhoArquivo);
            var bytesDeRetorno = ConvertaStreamReaderParaArrayBytes(arquivoTemporarioGerado);

            Response.BufferOutput = true;
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment;filename= " + nomeArquivo);
            Response.BinaryWrite(bytesDeRetorno);
            Response.Flush();
            Response.End();
        }

        public static byte[] ConvertaStreamReaderParaArrayBytes(StreamReader streamReader)
        {
            byte[] bytes;

            using (var memstream = new MemoryStream())
            {
                var buffer = new byte[512];
                int bytesRead;
                while ((bytesRead = streamReader.BaseStream.Read(buffer, 0, buffer.Length)) > 0)
                    memstream.Write(buffer, 0, bytesRead); bytes = memstream.ToArray();
            }

            return bytes;
        }
    }
}