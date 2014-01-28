using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoletoNet;
using Compartilhados.Componentes.Web;
using MP.Client.Relatorios;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmBoletoBancarioCEF : SuperPagina
    {
        public const string carteira = "SR";
        public const int codigoDoBanco = 104;
        public const string CHAVE_BOLETO = "CHAVE_BOLETO";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGerarBoleto_ButtonClick(object sender, EventArgs e)
        {
            // Boleto para CEF, com carteira SR e nosso número com 11 dígitos.

            //if(ExisteErroDePreenchimento())
            //{
            //    return;
            //}

            var vencimento = txtVencimento.SelectedDate.Value.ToString("dd/MM/yyyy");
            var valorBoleto = txtValor.Text;

            //var numeroDocumento =  busca no banco
            var numeroDocumento = "00001";

            // Numero do documento - numero de controle interno, não afeta o calculo da linha digitavel e nem o codigo de barras
            // mais pode ser útil, exemplo: quando o cliente ligar, vc pode consultar por este número e ver 
            // se já foi efetuado o pagamento

            // cedente

            var proximoNossoNumero = "10000709";// busca no banco

            var cedente_nossoNumeroBoleto = "82" + proximoNossoNumero; // o final do nosso número é incrementado ao final

            var cedente_cpfCnpj = "222.111.111-00"; // busca no banco
            var cedente_nome = "ALTERNATIVA MARCAS E PATENTES"; // busca no banco
            var cedente_agencia = "1394"; // busca no banco
            var cedente_conta = "302864"; // busca no banco
            var cedente_digitoConta = "4"; // busca no banco

            var cedente_codigo = "00100302864"; // operacao + conta - busca no banco

            var cedente = new Cedente(cedente_cpfCnpj, cedente_nome, cedente_agencia, cedente_conta, cedente_digitoConta) { Codigo = cedente_codigo };

            var boleto = new Boleto(Convert.ToDateTime(vencimento),
                                    (decimal)Convert.ToDouble(valorBoleto), carteira, cedente_nossoNumeroBoleto,
                                    cedente) { NumeroDocumento = numeroDocumento };

            //sacado         
            var sacado_cpfCnpj = txtCNPJCPF.Text;
            var sacado_nome = txtNome.Text;
            var sacado_endereco = txtEndereco.Text;

            var sacado = new Sacado(sacado_cpfCnpj, sacado_nome);
            boleto.Sacado = sacado;
            boleto.Sacado.Endereco.End = sacado_endereco;
            boleto.Sacado.Endereco.Bairro = txtBairro.Text;
            boleto.Sacado.Endereco.Cidade = txtCidade.Text;
            boleto.Sacado.Endereco.CEP = txtCep.Text;
            boleto.Sacado.Endereco.UF = txtEstado.Text;

            var instrucaoCaixa = new Instrucao_Caixa { Descricao = "Não Receber após o vencimento" };

            boleto.Instrucoes.Add(instrucaoCaixa);

            var especificacao = new EspecieDocumento_Caixa();
            boleto.EspecieDocumento = new EspecieDocumento_Caixa(especificacao.getCodigoEspecieByEnum(EnumEspecieDocumento_Caixa.Recibo));

            boleto.DataProcessamento = DateTime.Now;
            boleto.DataDocumento = DateTime.Now;

            var urlImagemLogo = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/Imagens/teste.jpg";

            var boletoBancario = new BoletoBancario(urlImagemLogo);
            boletoBancario.CodigoBanco = codigoDoBanco;
            boletoBancario.Boleto = boleto;
            boletoBancario.MostrarCodigoCarteira = true;

            boletoBancario.Boleto.Valida();

            boletoBancario.MostrarComprovanteEntrega = false;

            Session.Add(CHAVE_BOLETO, boletoBancario);

            var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + "BoletoCEF.aspx";

            Response.Redirect(url);

            //var htmlGerado = boletoBancario.MontaHtml();

            //var caminho = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS);

            //var im = HtmlRenderer.HtmlRender.RenderToImage(htmlGerado);

            //var nomeDoArquivoDeSaida = String.Concat(DateTime.Now.ToString("yyyyMMddhhmmss"), ".jpg");

            //im.Save(Path.Combine(caminho, nomeDoArquivoDeSaida));

            //im.Dispose();

            //var url = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/" +
            //              nomeDoArquivoDeSaida;

            

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraArquivoParaDownload(url, "Imprimir"), false);

            // incrementar o nosso numero e o numero do documento e atualizar no banco.
        }

        
        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.017";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}