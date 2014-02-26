using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Web.UI;
using Microsoft.VisualBasic;
//Envio por email
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;

[assembly: WebResource("BoletoNet.BoletoImpressao.BoletoNet.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("BoletoNet.Imagens.barra.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.corte.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.barrainterna.gif", "image/gif")]
//[assembly: WebResource("BoletoNet.Imagens.ponto.gif", "image/gif")]

namespace BoletoNet
{
    [Serializable(),
    Designer(typeof(BoletoBancarioDesigner)),
    ToolboxBitmap(typeof(BoletoBancario)),
    ToolboxData("<{0}:BoletoBancario Runat=\"server\"></{0}:BoletoBancario>")]
    public class BoletoBancario : System.Web.UI.Control
    {
        #region Variaveis

        private Banco _ibanco = null;
        private short _codigoBanco = 0;
        private Boleto _boleto;
        private Cedente _cedente;
        private Sacado _sacado;
        private List<IInstrucao> _instrucoes = new List<IInstrucao>();
        private string _instrucoesHtml = string.Empty;
        private bool _mostrarCodigoCarteira = false;
        private bool _formatoCarne = false;
        private string _instrucoesReciboSacadoHtml = string.Empty;
        private List<IInstrucao> _instrucoesReciboSacado = new List<IInstrucao>();
        #endregion Variaveis

        #region Propriedades

        public BoletoBancario()
        {
            
        }

        public BoletoBancario(string urlImagemPersonalizadaReciboSacado)
        {
            UrlImagemPersonalizada = urlImagemPersonalizadaReciboSacado;   
        }

        [Browsable(true), Description("C�digo do banco em que ser� gerado o boleto. Ex. 341-Ita�, 237-Bradesco")]
        public short CodigoBanco
        {
            get { return _codigoBanco; }
            set { _codigoBanco = value; }
        }

        /// <summary>
        /// Mostra o c�digo da carteira
        /// </summary>
        [Browsable(true), Description("Mostra a descri��o da carteira")]
        public bool MostrarCodigoCarteira
        {
            get { return _mostrarCodigoCarteira; }
            set { _mostrarCodigoCarteira = value; }
        }

        /// <summary>
        /// Mostra o c�digo da carteira
        /// </summary>
        [Browsable(true), Description("Formata o boleto no layout de carn�")]
        public bool FormatoCarne
        {
            get { return _formatoCarne; }
            set { _formatoCarne = value; }
        }

        [Browsable(false)]
        public Boleto Boleto
        {
            get { return _boleto; }
            set
            {
                _boleto = value;

                if (_ibanco == null)
                    _boleto.Banco = this.Banco;

                _cedente = _boleto.Cedente;
                _sacado = _boleto.Sacado;
            }
        }

        [Browsable(false)]
        public Sacado Sacado
        {
            get { return _sacado; }
        }

        [Browsable(false)]
        public Cedente Cedente
        {
            get { return _cedente; }
        }

        [Browsable(false)]
        public Banco Banco
        {
            get
            {
                if ((_ibanco == null) ||
                    (_ibanco.Codigo != _codigoBanco))
                {
                    _ibanco = new Banco(_codigoBanco);
                }

                if (_boleto != null)
                    _boleto.Banco = _ibanco;

                return _ibanco;
            }
        }

        #region Propriedades
        [Browsable(true), Description("Mostra o comprovante de entrega sem dados para marcar")]
        public bool MostrarComprovanteEntregaLivre
        {
            get { return Utils.ToBool(ViewState["1"]); }
            set { ViewState["1"] = value; }
        }

        [Browsable(true), Description("Mostra o comprovante de entrega")]
        public bool MostrarComprovanteEntrega
        {
            get { return Utils.ToBool(ViewState["2"]); }
            set { ViewState["2"] = value; }
        }

        [Browsable(true), Description("Oculta as intru��es do boleto")]
        public bool OcultarEnderecoSacado
        {
            get { return Utils.ToBool(ViewState["3"]); }
            set { ViewState["3"] = value; }
        }

        [Browsable(true), Description("Oculta as intru��es do boleto")]
        public bool OcultarInstrucoes
        {
            get { return Utils.ToBool(ViewState["4"]); }
            set { ViewState["4"] = value; }
        }

        [Browsable(true), Description("Oculta o recibo do sacado do boleto")]
        public bool OcultarReciboSacado
        {
            get { return Utils.ToBool(ViewState["5"]); }
            set { ViewState["5"] = value; }
        }

        [Browsable(true), Description("Gerar arquivo de remessa")]
        public bool GerarArquivoRemessa
        {
            get { return Utils.ToBool(ViewState["6"]); }
            set { ViewState["6"] = value; }
        }
        /// <summary> 
        /// Mostra o termo "Contra Apresenta��o" na data de vencimento do boleto
        /// </summary>
        public bool MostrarContraApresentacaoNaDataVencimento
        {
            get { return Utils.ToBool(ViewState["7"]); }
            set { ViewState["7"] = value; }
        }

        [Browsable(true), Description("Mostra o endere�o do Cedente")]
        public bool MostrarEnderecoCedente
        {
            get { return Utils.ToBool(ViewState["8"]); }
            set { ViewState["8"] = value; }
        }
        #endregion Propriedades

        /// <summary> 
        /// Retorna o campo para a 1 linha da instrucao.
        /// </summary>
        public List<IInstrucao> Instrucoes
        {
            get
            {
                return _instrucoes;
            }

            set
            {
                _instrucoes = value;
            }
        }

        //public List<IInstrucao> InstrucoesReciboSacado
        //{
        //    get
        //    {
        //        return _instrucoesReciboSacado;
        //    }

        //    set
        //    {
        //        _instrucoesReciboSacado = value;
        //    }
        //}


        #endregion Propriedades

        public static string UrlLogo(int banco)
        {
            Page page = System.Web.HttpContext.Current.CurrentHandler as Page;
            return page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens." + Utils.FormatCode(banco.ToString(), 3) + ".jpg");
        }

        #region Override
        protected override void OnPreRender(EventArgs e)
        {
            string alias = "BoletoNet.BoletoImpressao.BoletoNet.css";
            string csslink = "<link rel=\"stylesheet\" type=\"text/css\" href=\"" +
                Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), alias) + "\" />";

            LiteralControl include = new LiteralControl(csslink);
            Page.Header.Controls.Add(include);

            base.OnPreRender(e);
        }

        protected override void OnLoad(EventArgs e)
        {
        }

        [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "Execution")]
        protected override void Render(HtmlTextWriter output)
        {
            if (_ibanco == null)
            {
                output.Write("<b>Erro gerando o boleto banc�rio: faltou definir o banco.</b>");
                return;
            }
            //string urlImagemLogo = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens." + "logo-boleto" + ".jpg");

            //string urlImagemLogoPersonalizada = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.logo-boleto.jpg");
            //UrlImagemPersonalizada = urlImagemLogoPersonalizada;
            string urlImagemLogo = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens." + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg");
            string urlImagemBarra = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.barra.gif");
            //string urlImagemBarraInterna = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.barrainterna.gif");
            //string urlImagemCorte = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.corte.gif");
            //string urlImagemPonto = Page.ClientScript.GetWebResourceUrl(typeof(BoletoBancario), "BoletoNet.Imagens.ponto.gif");

            //Atribui os valores ao html do boleto banc�rio
            //output.Write(MontaHtml(urlImagemCorte, urlImagemLogo, urlImagemBarra, urlImagemPonto, urlImagemBarraInterna,
            //    "<img src=\"ImagemCodigoBarra.ashx?code=" + Boleto.CodigoBarra.Codigo + "\" alt=\"C�digo de Barras\" />"));
            output.Write(MontaHtml(urlImagemLogo, urlImagemBarra, "<img src=\"ImagemCodigoBarra.ashx?code=" + Boleto.CodigoBarra.Codigo + "\" alt=\"C�digo de Barras\" />"));
        }
        #endregion Override

        public string UrlImagemPersonalizada { get; set; }

        #region Html
        public string GeraHtmlInstrucoes()
        {
            try
            {
                StringBuilder html = new StringBuilder();

                string titulo = "Instru��es de Impress�o";
                string instrucoes = "Imprimir em impressora jato de tinta (ink jet) ou laser em qualidade normal. (N�o use modo econ�mico).<br>Utilize folha A4 (210 x 297 mm) ou Carta (216 x 279 mm) - Corte na linha indicada<br>";

                html.Append(Html.Instrucoes);
                html.Append("<br />");

                return html.ToString()
                    .Replace("@TITULO", titulo)
                    .Replace("@INSTRUCAO", instrucoes);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a execu��o da transa��o.", ex);
            }
        }

        private string GeraHtmlCarne(string telefone, string htmlBoleto)
        {
            StringBuilder html = new StringBuilder();

            html.Append(Html.Carne);

            return html.ToString()
                .Replace("@TELEFONE", telefone)
                .Replace("#BOLETO#", htmlBoleto);
        }

        public string GeraHtmlReciboSacado()
        {
            try
            {

                StringBuilder html = new StringBuilder();
                
                if(!string.IsNullOrEmpty(UrlImagemPersonalizada))
                {
                    html.Append(Html.ReciboSacadoPartePersonalizada1);
                    html.Append("<br />");
                    html.Append(Html.ReciboSacadoPartePersonalizada2);
                }
                else
                {
                    html.Append(Html.ReciboSacadoParte1);
                    html.Append("<br />");
                    html.Append(Html.ReciboSacadoParte2);
                }


                //html.Append(Html.ReciboSacadoParte1);
                //html.Append("<br />");
                //html.Append(Html.ReciboSacadoParte2);
                html.Append(Html.ReciboSacadoParte3);

                if (MostrarEnderecoCedente)
                {
                    html.Append(Html.ReciboSacadoParte10);
                }

                html.Append(Html.ReciboSacadoParte4);
                html.Append(Html.ReciboSacadoParte5);
                html.Append(Html.ReciboSacadoParte6);
                html.Append(Html.ReciboSacadoParte7);

                if (Instrucoes.Count == 0)
                    html.Append(Html.ReciboSacadoParte8);

                MontaInstrucoesReciboDoSacado();

                return html.ToString().Replace("@INSTRUCOESRECIBOSACADO", _instrucoesReciboSacadoHtml);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a execu��o da transa��o.", ex);
            }
        }

        public string GeraHtmlReciboCedente()
        {
            try
            {
                StringBuilder html = new StringBuilder();

                html.Append(Html.ReciboCedenteParte1);
                html.Append(Html.ReciboCedenteParte2);
                html.Append(Html.ReciboCedenteParte3);
                html.Append(Html.ReciboCedenteParte4);
                html.Append(Html.ReciboCedenteParte5);
                html.Append(Html.ReciboCedenteParte6);
                html.Append(Html.ReciboCedenteParte7);
                html.Append(Html.ReciboCedenteParte8);
                html.Append(Html.ReciboCedenteParte9);
                html.Append(Html.ReciboCedenteParte10);
                html.Append(Html.ReciboCedenteParte11);
                html.Append(Html.ReciboCedenteParte12);

                //Para Banco Ita�, o texto "(Texto de responsabilidade do cedente)" deve ser
                //"(Todas as informa��es deste bloqueto s�o de exclusiva responsabilidade do cedente)".
                if (Boleto.Banco.Codigo == 341)
                {
                    html.Replace("(Texto de responsabilidade do cedente)", "(Todas as informa��es deste bloqueto s�o de exclusiva responsabilidade do cedente)");
                }

                //Para carteiras "17-019" e "18-019" do Banco do Brasil, a ficha de compensa��o n�o possui c�digo da carteira
                //na formata��o do campo.
                if (Boleto.Banco.Codigo == 1 & (Boleto.Carteira.Equals("17-019") | Boleto.Carteira.Equals("18-019")))
                {
                    html.Replace("Carteira /", "");
                    html.Replace("@NOSSONUMERO", "@NOSSONUMEROBB");
                }
                else
                {
                    //Para SANTANDER, a ficha de compensa��o n�o possui c�digo da carteira - por jsoda em 08/12/2012
                    if (Boleto.Banco.Codigo == 33)
                        html.Replace("Carteira /", "");
                }

                MontaInstrucoes();

                return html.ToString().Replace("@INSTRUCOES", _instrucoesHtml);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro na execu��o da transa��o.", ex);
            }
        }

        public string HtmlComprovanteEntrega
        {
            get
            {
                StringBuilder html = new StringBuilder();

                html.Append(Html.ComprovanteEntrega1);
                html.Append(Html.ComprovanteEntrega2);
                html.Append(Html.ComprovanteEntrega3);
                html.Append(Html.ComprovanteEntrega4);
                html.Append(Html.ComprovanteEntrega5);
                html.Append(Html.ComprovanteEntrega6);

                html.Append(MostrarComprovanteEntregaLivre ? Html.ComprovanteEntrega71 : Html.ComprovanteEntrega7);

                html.Append("<br />");
                return html.ToString();
            }
        }

        private void MontaInstrucoes()
        {
            if (string.IsNullOrEmpty(_instrucoesHtml))
                if (Boleto.Instrucoes.Count > 0)
                {
                    _instrucoesHtml = string.Empty;
                    //Flavio(fhlviana@hotmail.com) - retirei a tag <span> de cada instru��o por n�o ser mais necess�ras desde que dentro
                    //da div que contem as instru��es a classe cpN se aplica, que � a mesma, em conteudo, da classe cp
                    foreach (IInstrucao instrucao in Boleto.Instrucoes)
                        _instrucoesHtml += string.Format("{0}<br />", instrucao.Descricao);

                    _instrucoesHtml = Strings.Left(_instrucoesHtml, _instrucoesHtml.Length - 6);
                }
        }

        private void MontaInstrucoesReciboDoSacado()
        {
            if (string.IsNullOrEmpty(_instrucoesReciboSacadoHtml))
                if (Boleto.Instrucoes.Count > 0)
                {
                    _instrucoesReciboSacadoHtml = string.Empty;

                    foreach (IInstrucao instrucao in Boleto.Instrucoes)
                    {
                        var descricaoDoReciboDoSacado = instrucao.DescricaoReciboDoSacado;

                        if (instrucao.DescricaoReciboDoSacado.Contains("\n"))
                        {
                            descricaoDoReciboDoSacado = instrucao.DescricaoReciboDoSacado.Replace("\n", "<br>");
                        }

                        _instrucoesReciboSacadoHtml += string.Format("{0}<br />", descricaoDoReciboDoSacado);
                    }
                        

                    _instrucoesReciboSacadoHtml = Strings.Left(_instrucoesReciboSacadoHtml, _instrucoesReciboSacadoHtml.Length - 6);
                }
        }

        //private string MontaHtml(string urlImagemCorte, string urlImagemLogo, string urlImagemBarra, string urlImagemPonto, string urlImagemBarraInterna, string imagemCodigoBarras)
        private string MontaHtml(string urlImagemLogo, string urlImagemBarra, string imagemCodigoBarras)
        {

            var html = new StringBuilder();
            string enderecoCedente = "";

            //Oculta o cabe�alho das instru��es do boleto
            if (!OcultarInstrucoes)
                html.Append(GeraHtmlInstrucoes());

            if (!FormatoCarne)
            {
                //Mostra o comprovante de entrega
                if (MostrarComprovanteEntrega | MostrarComprovanteEntregaLivre)
                {
                    html.Append(HtmlComprovanteEntrega);
                    //Html da linha pontilhada
                    if(OcultarReciboSacado)
                        html.Append(Html.ReciboSacadoParte8);
                }

                //Oculta o recibo do sacabo do boleto
                if (!OcultarReciboSacado)
                {
                    html.Append(GeraHtmlReciboSacado());

                    //Caso mostre o Endere�o do Cedente
                    if (MostrarEnderecoCedente)
                    {
                        if (Cedente.Endereco == null)
                            throw new ArgumentNullException("Endere�o do Cedente");

                        enderecoCedente = string.Concat(Cedente.Endereco.End, " , ");

                        if (Cedente.Endereco.CEP == String.Empty)
                        {
                            enderecoCedente += string.Format("{0} - {1}/{2}", Cedente.Endereco.Bairro,
                                                             Cedente.Endereco.Cidade, Cedente.Endereco.UF);
                        }
                        else
                        {
                            enderecoCedente += string.Format("{0} - {1}/{2} - CEP: {3}", Cedente.Endereco.Bairro,
                                                             Cedente.Endereco.Cidade, Cedente.Endereco.UF,
                                                             Utils.FormataCEP(Cedente.Endereco.CEP));
                        }

                    }
                }
            }

            string sacado = "";
            //Flavio(fhlviana@hotmail.com) - adicionei a possibilidade de o boleto n�o ter, necess�riamente, que informar o CPF ou CNPJ do sacado.
            //Formata o CPF/CNPJ(se houver) e o Nome do Sacado para apresenta��o
            if (Sacado.CPFCNPJ == string.Empty)
            {
                sacado = Sacado.Nome;
            }
            else
            {
                if (Sacado.CPFCNPJ.Length <= 11)
                    sacado = string.Format("{0}  CPF: {1}", Sacado.Nome, Utils.FormataCPF(Sacado.CPFCNPJ));
                else
                    sacado = string.Format("{0}  CNPJ: {1}", Sacado.Nome, Utils.FormataCNPJ(Sacado.CPFCNPJ));
            }

            String infoSacado = Sacado.InformacoesSacado.GeraHTML(false);

            //Caso n�o oculte o Endere�o do Sacado,
            if (!OcultarEnderecoSacado)
            {
                String enderecoSacado = "";

                if (Sacado.Endereco.CEP == String.Empty)
                    enderecoSacado = string.Format("{0} - {1}/{2}", Sacado.Endereco.Bairro, Sacado.Endereco.Cidade, Sacado.Endereco.UF);
                else
                    enderecoSacado = string.Format("{0} - {1}/{2} - CEP: {3}", Sacado.Endereco.Bairro,
                    Sacado.Endereco.Cidade, Sacado.Endereco.UF, Utils.FormataCEP(Sacado.Endereco.CEP));

                if (Sacado.Endereco.End != string.Empty && enderecoSacado != string.Empty)
                    if (infoSacado == string.Empty)
                        infoSacado += InfoSacado.Render(Sacado.Endereco.End, enderecoSacado, false);
                    else
                        infoSacado += InfoSacado.Render(Sacado.Endereco.End, enderecoSacado, true);
                //"Informa��es do Sacado" foi introduzido para possibilitar que o boleto na informe somente o endere�o do sacado
                //como em outras situa�oes onde se imprime matriculas, codigos e etc, sobre o sacado.
                //Sendo assim o endere�o do sacado passa a ser uma Informa�ao do Sacado que � adicionada no momento da renderiza��o
                //de acordo com a flag "OcultarEnderecoSacado"
            }

            string agenciaConta = Utils.FormataAgenciaConta(Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.DigitoAgencia, Cedente.ContaBancaria.Conta, Cedente.ContaBancaria.DigitoConta);

            // Trecho adicionado por Fabr�cio Nogueira de Almeida :fna - fnalmeida@gmail.com - 09/12/2008
            /* Esse c�digo foi inserido pq no campo Ag�ncia/Cod Cedente, estava sendo impresso sempre a ag�ncia / n�mero da conta
             * No boleto da caixa que eu fiz, coloquei no m�todo validarBoleto um trecho para calcular o d�gito do cedente, e adicionei esse atributo na classe cedente
             * O trecho abaixo testa se esse digito foi calculado, se foi insere no campo Agencia/Cod Cedente, a ag�ncia e o c�digo com seu digito
             * caso contr�rio mostra a ag�ncia / conta, como era anteriormente.
             * Com esse c�digo ele ira atender as necessidades do boleto caixa e n�o afetar� os demais
             * Caso queira que apare�a o Ag�ncia/cod. cedente para outros boletos, basta calcular e setar o digito, como foi feito no boleto Caixa 
             */

            string agenciaCodigoCedente;

            if (!Cedente.DigitoCedente.Equals(-1))
            {
                if (!String.IsNullOrEmpty(Cedente.ContaBancaria.OperacaConta))
                    agenciaCodigoCedente = string.Format("{0}/{1}.{2}-{3}", Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.OperacaConta, Utils.FormatCode(Cedente.Codigo.ToString(), 6), Cedente.DigitoCedente.ToString());
                /*else if (Boleto.Banco.Codigo == 748)// para Sicredi a formata��o fica assim "Agencia.UAPosto.Cedente" - por sidneiklein 14/10/2013    
                    agenciaCodigoCedente = string.Format("{0}.{1}.{2}", Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.OperacaConta, Cedente.Codigo);
                else if (Boleto.Banco.Codigo == 41)// para Banrisul a formata��o fica Agencia.DigitoAgencia/Codigo cedente formatado.
                    agenciaCodigoCedente = string.Format("{0}.{1}/{2}.{3}.{4}", Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.DigitoAgencia, Cedente.Codigo.Substring(4, 6), Cedente.Codigo.Substring(10, 1), Cedente.DigitoCedente);
                else if (Boleto.Banco.Codigo == 1)
                    agenciaCodigoCedente = string.Format("{0}-{1}/{2}-{3}", Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.DigitoAgencia, Utils.FormatCode(Cedente.ContaBancaria.Conta, 6), Cedente.ContaBancaria.DigitoConta);
                else if(Boleto.Banco.Codigo == 104)
                    agenciaCodigoCedente = string.Format("{0}/{1}-{2}", Cedente.ContaBancaria.Agencia, Utils.FormatCode(Cedente.Codigo.ToString(), 6), Cedente.DigitoCedente.ToString());
                else
                    agenciaCodigoCedente = string.Format("{0}/{1}-{2}", Cedente.ContaBancaria.Agencia, Utils.FormatCode(Cedente.Codigo.ToString(), 6), Cedente.DigitoCedente.ToString());*/

                //Jefhtavares em 07/01/2014 - tirei esse emaranhado de ElseIf e coloquei um switch case

                switch (Boleto.Banco.Codigo)
                {
                    case 748:
                        agenciaCodigoCedente = string.Format("{0}.{1}.{2}", Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.OperacaConta, Cedente.Codigo);
                        break;
                    case 41:
                        agenciaCodigoCedente = string.Format("{0}.{1}/{2}.{3}.{4}", Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.DigitoAgencia, Cedente.Codigo.Substring(4, 6), Cedente.Codigo.Substring(10, 1), Cedente.DigitoCedente);
                        break;
                    case 1:
                        agenciaCodigoCedente = string.Format("{0}-{1}/{2}-{3}", Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.DigitoAgencia, Utils.FormatCode(Cedente.ContaBancaria.Conta, 6), Cedente.ContaBancaria.DigitoConta);
                        break;                    
                    default:
                        agenciaCodigoCedente = string.Format("{0}/{1}-{2}", Cedente.ContaBancaria.Agencia, Utils.FormatCode(Cedente.Codigo.ToString(), 6), Cedente.DigitoCedente.ToString());
                        break;
                }
            }
            else
            {
                //Para banco SANTANDER, a formata��o do campo "Agencia/Identif.Cedente" - por jsoda em 07/05/2012
                if (Boleto.Banco.Codigo == 33)
                    agenciaCodigoCedente = string.Format("{0}-{1}/{2}", Cedente.ContaBancaria.Agencia, Cedente.ContaBancaria.DigitoAgencia, Utils.FormatCode(Cedente.Codigo.ToString(), 6));
                else if (Boleto.Banco.Codigo == 399) // para Banco HSBC mostra apenas c�digo Cedente - por Ponce em 08/06/2012
                    agenciaCodigoCedente = Utils.FormatCode(Cedente.Codigo.ToString(), 7);                
                else
                    agenciaCodigoCedente = agenciaConta;
            }

            if (!FormatoCarne)
                html.Append(GeraHtmlReciboCedente());
            else
            {
                html.Append(GeraHtmlCarne("", GeraHtmlReciboCedente()));
            }

            string dataVencimento = Boleto.DataVencimento.ToString("dd/MM/yyyy");

            if (MostrarContraApresentacaoNaDataVencimento)
                dataVencimento = "Contra Apresenta��o";

            return html
                .Replace("@CODIGOBANCO", Utils.FormatCode(_ibanco.Codigo.ToString(), 3))
                .Replace("@DIGITOBANCO", _ibanco.Digito.ToString())
                //.Replace("@URLIMAGEMBARRAINTERNA", urlImagemBarraInterna)
                //.Replace("@URLIMAGEMCORTE", urlImagemCorte)
                //.Replace("@URLIMAGEMPONTO", urlImagemPonto)
                .Replace("@URLIMAGEMLOGOPERSONALIZADA", UrlImagemPersonalizada)
                .Replace("@URLIMAGEMLOGO", urlImagemLogo)
                .Replace("@URLIMAGEMBARRA", urlImagemBarra)
                .Replace("@LINHADIGITAVEL", Boleto.CodigoBarra.LinhaDigitavel)
                .Replace("@LOCALPAGAMENTO", Boleto.LocalPagamento)
                .Replace("@DATAVENCIMENTO", dataVencimento)
                .Replace("@CEDENTE", Cedente.Nome)
                .Replace("@DATADOCUMENTO", Boleto.DataDocumento.ToString("dd/MM/yyyy"))
                .Replace("@NUMERODOCUMENTO", Boleto.NumeroDocumento)
                .Replace("@ESPECIEDOCUMENTO", EspecieDocumento.ValidaSigla(Boleto.EspecieDocumento))
                .Replace("@DATAPROCESSAMENTO", Boleto.DataProcessamento.ToString("dd/MM/yyyy"))

            #region Implementa��o para o Banco do Brasil
                //Vari�vel inserida para atender �s especifica��es das carteiras "17-019" e "18-019" do Banco do Brasil
                //apenas para a ficha de compensa��o.
                //Como a vari�vel n�o existir� se n�o forem as carteiras "17-019" e "18-019", n�o foi colocado o [if].
                    .Replace("@NOSSONUMEROBB", Boleto.Banco.Codigo == 1 & (Boleto.Carteira.Equals("17-019") | Boleto.Carteira.Equals("18-019")) ? Boleto.NossoNumero.Substring(3) : string.Empty)
            #endregion Implementa��o para o Banco do Brasil

                .Replace("@NOSSONUMERO", Boleto.NossoNumero)
                .Replace("@CARTEIRA", FormataDescricaoCarteira())
                .Replace("@ESPECIE", Boleto.Especie)
                .Replace("@QUANTIDADE", (Boleto.QuantidadeMoeda == 0 ? "" : Boleto.QuantidadeMoeda.ToString()))
                .Replace("@VALORDOCUMENTO", Boleto.ValorMoeda)
                .Replace("@=VALORDOCUMENTO", (Boleto.ValorBoleto == 0 ? "" : Boleto.ValorBoleto.ToString("R$ ##,##0.00")))
                .Replace("@VALORCOBRADO", (Boleto.ValorCobrado == 0 ? "" : Boleto.ValorCobrado.ToString("R$ ##,##0.00")))
                .Replace("@OUTROSACRESCIMOS", "")
                .Replace("@OUTRASDEDUCOES", "")
                .Replace("@DESCONTOS", (Boleto.ValorDesconto == 0 ? "" : Boleto.ValorDesconto.ToString("R$ ##,##0.00")))
                .Replace("@AGENCIACONTA", agenciaCodigoCedente)
                .Replace("@SACADO", sacado)
                .Replace("@INFOSACADO", infoSacado)
                .Replace("@AGENCIACODIGOCEDENTE", agenciaCodigoCedente)
                .Replace("@CPFCNPJ", Cedente.CPFCNPJ)
                .Replace("@MORAMULTA", (Boleto.ValorMulta == 0 ? "" : Boleto.ValorMulta.ToString("R$ ##,##0.00")))
                .Replace("@AUTENTICACAOMECANICA", "")
                .Replace("@USODOBANCO", Boleto.UsoBanco)
                .Replace("@IMAGEMCODIGOBARRA", imagemCodigoBarras)
                .Replace("@ACEITE", Boleto.Aceite).ToString()
                .Replace("@ENDERECOCEDENTE", MostrarEnderecoCedente ? enderecoCedente : "");

        }

        private string FormataDescricaoCarteira()
        {

            if (MostrarCodigoCarteira)
            {
                string descricaoCarteira = "";
                int carteira = Utils.ToInt32(Boleto.Carteira);

                switch (Banco.Codigo)
                {
                    case 1:
                        descricaoCarteira = new Carteira_BancoBrasil(carteira).Codigo;
                        break;
                    case 353:
                    case 8:
                    case 33:
                        descricaoCarteira = new Carteira_Santander(carteira).Codigo;
                        break;
                    case 104:
                        descricaoCarteira = new Carteira_Caixa(carteira).Codigo;
                        break;
                    case 341:
                        descricaoCarteira = new Carteira_Itau(carteira).Codigo;
                        break;

                    default:
                        throw new Exception(string.Format("A descri��o para o banco {0} n�o foi implementada.", Boleto.Banco));
                        throw new Exception(string.Format("A descri��o da carteira {0} / banco {1} n�o foi implementada (marque false na propriedade MostrarCodigoCarteira)", carteira, Banco.Codigo));

                }

                if (string.IsNullOrEmpty(descricaoCarteira))
                {
                    throw new Exception("O c�digo da carteira n�o foi implementado.");
                }

                return string.Format("{0} - {1}", Boleto.Carteira,
                     descricaoCarteira);
        }
            else
            {
                return Boleto.Carteira;
            }
        }

        #endregion Html

        #region Gera��o do Html OffLine

        /// <summary>
        /// Fun��o utilizada para gerar o html do boleto sem que o mesmo esteja dentro de uma p�gina Web.
        /// </summary>
        /// <param name="srcLogo">Local apontado pela imagem de logo.</param>
        /// <param name="srcBarra">Local apontado pela imagem de barra.</param>
        /// <param name="srcCodigoBarra">Local apontado pela imagem do c�digo de barras.</param>
        /// <returns>StringBuilder cont�ndo o c�digo html do boleto banc�rio.</returns>
        protected StringBuilder HtmlOffLine(string textoNoComecoDoEmail, string srcLogo, string srcBarra, string srcCodigoBarra)
        {//protected StringBuilder HtmlOffLine(string srcCorte, string srcLogo, string srcBarra, string srcPonto, string srcBarraInterna, string srcCodigoBarra)
            this.OnLoad(EventArgs.Empty);

            StringBuilder html = new StringBuilder();
            HtmlOfflineHeader(html);
            if (textoNoComecoDoEmail != null && textoNoComecoDoEmail != "")
            {
                html.Append(textoNoComecoDoEmail);
            }
            html.Append(MontaHtml(srcLogo, srcBarra, "<img src=\"" + srcCodigoBarra + "\" alt=\"C�digo de Barras\" />"));
            HtmlOfflineFooter(html);
            return html;
        }




        /// <summary>
        /// Monta o Header de um email com pelo menos um boleto dentro.
        /// </summary>
        /// <param name="saida">StringBuilder onde o conteudo sera salvo.</param>
        protected static void HtmlOfflineHeader(StringBuilder html)
        {
            html.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            html.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            html.Append("<head>");
            html.Append("<title>Boleto.Net</title>");

            #region Css
            {
                Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNet.BoletoImpressao.BoletoNet.css");

                using (StreamReader sr = new StreamReader(stream))
                {
                    html.Append("<style type='text/css'>");
                    html.Append(sr.ReadToEnd());
                    html.Append("</style>");
                    sr.Close();
                    sr.Dispose();
                }
            }
            #endregion Css

            html.Append("</head>");
            html.Append("<body>");
        }


        /// <summary>
        /// Monta o Footer de um email com pelo menos um boleto dentro.
        /// </summary>
        /// <param name="saida">StringBuilder onde o conteudo sera salvo.</param>
        protected static void HtmlOfflineFooter(StringBuilder saida)
        {
            saida.Append("</body>");
            saida.Append("</html>");
        }


        /// <summary>
        /// Junta varios boletos em uma unica AlternateView, para todos serem mandados juntos no mesmo email
        /// </summary>
        /// <param name="arrayDeBoletos">Array contendo os boletos a serem mesclados</param>
        /// <returns></returns>
        public static AlternateView GeraHtmlDeVariosBoletosParaEmail(BoletoBancario[] arrayDeBoletos)
        {
            return GeraHtmlDeVariosBoletosParaEmail(null, arrayDeBoletos);
        }

        /// <summary>
        /// Junta varios boletos em uma unica AlternateView, para todos serem mandados juntos no mesmo email
        /// </summary>
        /// <param name="textoNoComecoDoEmail">Texto em HTML a ser adicionado no comeco do email</param>
        /// <param name="arrayDeBoletos">Array contendo os boletos a serem mesclados</param>
        /// <returns>AlternateView com os dados de todos os boleto.</returns>
        public static AlternateView GeraHtmlDeVariosBoletosParaEmail(string textoNoComecoDoEmail, BoletoBancario[] arrayDeBoletos)
        {
            var corpoDoEmail = new StringBuilder();

            var linkedResources = new List<LinkedResource>();
            HtmlOfflineHeader(corpoDoEmail);
            if (textoNoComecoDoEmail != null && textoNoComecoDoEmail != "")
            {
                corpoDoEmail.Append(textoNoComecoDoEmail);
            }
            foreach (var umBoleto in arrayDeBoletos)
            {
                if (umBoleto != null)
                {
                    LinkedResource lrImagemLogo;
                    LinkedResource lrImagemBarra;
                    LinkedResource lrImagemCodigoBarra;
                    umBoleto.GeraGraficosParaEmailOffLine(out lrImagemLogo, out lrImagemBarra, out lrImagemCodigoBarra);
                    var theOutput = umBoleto.MontaHtml(
                        "cid:" + lrImagemLogo.ContentId,
                        "cid:" + lrImagemBarra.ContentId,
                        "<img src=\"cid:" + lrImagemCodigoBarra.ContentId + "\" alt=\"C�digo de Barras\" />");

                    corpoDoEmail.Append(theOutput);

                    linkedResources.Add(lrImagemLogo);
                    linkedResources.Add(lrImagemBarra);
                    linkedResources.Add(lrImagemCodigoBarra);
                }
            }
            HtmlOfflineFooter(corpoDoEmail);



            AlternateView av = AlternateView.CreateAlternateViewFromString(corpoDoEmail.ToString(), Encoding.Default, "text/html");
            foreach (var theResource in linkedResources)
            {
                av.LinkedResources.Add(theResource);
            }



            return av;
        }


        /// <summary>
        /// Fun��o utilizada gerar o AlternateView necess�rio para enviar um boleto banc�rio por e-mail.
        /// </summary>
        /// <returns>AlternateView com os dados do boleto.</returns>
        public AlternateView HtmlBoletoParaEnvioEmail()
        {
            return HtmlBoletoParaEnvioEmail(null);
        }


        /// <summary>
        /// Fun��o utilizada gerar o AlternateView necess�rio para enviar um boleto banc�rio por e-mail.
        /// </summary>
        /// <param name="textoNoComecoDoEmail">Texto (em HTML) a ser incluido no come�o do Email.</param>
        /// <returns>AlternateView com os dados do boleto.</returns>
        public AlternateView HtmlBoletoParaEnvioEmail(string textoNoComecoDoEmail)
        {
            LinkedResource lrImagemLogo;
            LinkedResource lrImagemBarra;
            LinkedResource lrImagemCodigoBarra;

            GeraGraficosParaEmailOffLine(out lrImagemLogo, out lrImagemBarra, out lrImagemCodigoBarra);
            StringBuilder html = HtmlOffLine(textoNoComecoDoEmail, "cid:" + lrImagemLogo.ContentId, "cid:" + lrImagemBarra.ContentId, "cid:" + lrImagemCodigoBarra.ContentId);

            AlternateView av = AlternateView.CreateAlternateViewFromString(html.ToString(), Encoding.Default, "text/html");

            av.LinkedResources.Add(lrImagemLogo);
            av.LinkedResources.Add(lrImagemBarra);
            av.LinkedResources.Add(lrImagemCodigoBarra);
            return av;
        }

        /// <summary>
        /// Gera as tres imagens necess�rias para o Boleto
        /// </summary>
        /// <param name="lrImagemLogo">O Logo do Banco</param>
        /// <param name="lrImagemBarra">A Barra Horizontal</param>
        /// <param name="lrImagemCodigoBarra">O C�digo de Barras</param>
        void GeraGraficosParaEmailOffLine(out LinkedResource lrImagemLogo, out LinkedResource lrImagemBarra, out LinkedResource lrImagemCodigoBarra)
        {
            this.OnLoad(EventArgs.Empty);

            var randomSufix = new Random().Next().ToString(); // para podermos colocar no mesmo email varios boletos diferentes

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNet.Imagens." + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg");
            lrImagemLogo = new LinkedResource(stream, MediaTypeNames.Image.Jpeg);
            lrImagemLogo.ContentId = "logo" + randomSufix;


            MemoryStream ms = new MemoryStream(Utils.ConvertImageToByte(Html.barra));
            lrImagemBarra = new LinkedResource(ms, MediaTypeNames.Image.Gif);
            lrImagemBarra.ContentId = "barra" + randomSufix; ;

            C2of5i cb = new C2of5i(Boleto.CodigoBarra.Codigo, 1, 50, Boleto.CodigoBarra.Codigo.Length);
            ms = new MemoryStream(Utils.ConvertImageToByte(cb.ToBitmap()));

            lrImagemCodigoBarra = new LinkedResource(ms, MediaTypeNames.Image.Gif);
            lrImagemCodigoBarra.ContentId = "codigobarra" + randomSufix; ;

        }


        /// <summary>
        /// Fun��o utilizada para gravar em um arquivo local o conte�do do boleto. Este arquivo pode ser aberto em um browser sem que o site esteja no ar.
        /// </summary>
        /// <param name="fileName">Path do arquivo que deve conter o c�digo html.</param>
        public void MontaHtmlNoArquivoLocal(string fileName)
        {
            using (FileStream f = new FileStream(fileName, FileMode.Create))
            {
                StreamWriter w = new StreamWriter(f, System.Text.Encoding.Default);
                w.Write(MontaHtml());
                w.Close();
                f.Close();
            }
        }

        /// <summary>
        /// Monta o Html do boleto banc�rio
        /// </summary>
        /// <returns>string</returns>
        public string MontaHtml()
        {
            return MontaHtml(null);
        }

        /// <summary>
        /// Monta o Html do boleto banc�rio
        /// </summary>
        /// <param name="fileName">Caminho do arquivo</param>
        /// <returns>Html do boleto gerado</returns>
        public string MontaHtml(string fileName)
        {
            if (fileName == null)
                fileName = System.IO.Path.GetTempPath();

            this.OnLoad(EventArgs.Empty);

            //string fnCorte = fileName + @"BoletoNetCorte.gif";
            //if (!System.IO.File.Exists(fnCorte))
            //    Html.corte.Save(fnCorte);

            string fnLogo = fileName + @"BoletoNet" + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg";

            if (!System.IO.File.Exists(fnLogo))
                Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNet.Imagens." + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg")).Save(fnLogo);

            //string fnPonto = fileName + @"BoletoNetPonto.gif";
            //if (!System.IO.File.Exists(fnPonto))
            //    Html.ponto.Save(fnPonto);

            //string fnBarraInterna = fileName + @"BoletoNetBarraInterna.gif";
            //if (!File.Exists(fnBarraInterna))
            //    Html.barrainterna.Save(fnBarraInterna);

            string fnBarra = fileName + @"BoletoNetBarra.gif";
            if (!File.Exists(fnBarra))
                Html.barra.Save(fnBarra);

            string fnCodigoBarras = System.IO.Path.GetTempFileName();
            C2of5i cb = new C2of5i(Boleto.CodigoBarra.Codigo, 1, 50, Boleto.CodigoBarra.Codigo.Length);
            cb.ToBitmap().Save(fnCodigoBarras);

            //return HtmlOffLine(fnCorte, fnLogo, fnBarra, fnPonto, fnBarraInterna, fnCodigoBarras).ToString();
            return HtmlOffLine(null, fnLogo, fnBarra, fnCodigoBarras).ToString();
        }

        /// <summary>
        /// Monta o Html do boleto banc�rio para View ASP.Net MVC
        /// <code>
        /// <para>Exemplo:</para>
        /// <para>public ActionResult VisualizarBoleto(string Id)</para>
        /// <para>{</para>
        /// <para>    BoletoBancario bb = new BoletoBancario();</para>
        /// <para>    //...</para>
        /// <para>    ViewBag.Boleto = bb.MontaHtml("/Content/Boletos/", "teste1");</para>
        /// <para>    return View();</para>
        /// <para>}</para>
        /// <para>//Na view</para>
        /// <para>@{Layout = null;}@Html.Raw(ViewBag.Boleto)</para>
        /// </code>
        /// </summary>
        /// <param name="Url">Pasta dos boletos. Exemplo MontaHtml("/Content/Boletos/", "000100")</param>
        /// <param name="fileName">Nome do arquivo para o boleto</param>
        /// <returns>Html do boleto gerado</returns>
        /// <desenvolvedor>Sandro Ribeiro</desenvolvedor>
        /// <criacao>16/11/2012</criacao>
        public string MontaHtml(string url, string fileName, bool useMapPathSecure = true)
        {
            //Vari�vel para o caminho f�sico do servidor
            string pathServer = "";

            //Verifica se o usu�rio informou uma url v�lida
            if (url == null)
            {
                //Obriga o usu�rio a especificar uma url v�lida
                throw new ArgumentException("Voc� precisa informar uma pasta padr�o.");
            }
            else
            {
                if (useMapPathSecure)
                {
                    //Verifica se o usu�rio usou barras no in�cio e no final da url
                    if (url.Substring(url.Length - 1, 1) != "/")
                        url = url + "/";
                    if (url.Substring(0, 1) != "/")
                        url = url + "/";
                    //Mapeia o caminho f�sico dos arquivos
                    pathServer = MapPathSecure(string.Format("~{0}", url));
                }

                //Verifica se o caminho existe
                if (!Directory.Exists(pathServer))
                    throw new ArgumentException("A o caminho f�sico '{0}' n�o existe.", pathServer);
            }
            //Verifica o nome do arquivo
            if (fileName == null)
            {
                fileName = DateTime.Now.Ticks.ToString();
            }
            else
            {
                if (fileName == "")
                    fileName = DateTime.Now.Ticks.ToString();
            }

            //Mantive o padr�o 
            this.OnLoad(EventArgs.Empty);

            //Prepara o arquivo da logo para ser salvo
            string fnLogo = pathServer + @"BoletoNet" + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg";
            //Prepara o arquivo da logo para ser usado no html
            string fnLogoUrl = url + @"BoletoNet" + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg";

            //Salvo a imagem apenas 1 vez com o c�digo do banco
            if (!System.IO.File.Exists(fnLogo))
                Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("BoletoNet.Imagens." + Utils.FormatCode(_ibanco.Codigo.ToString(), 3) + ".jpg")).Save(fnLogo);

            //Prepara o arquivo da barra para ser salvo
            string fnBarra = pathServer + @"BoletoNetBarra.gif";
            //Prepara o arquivo da barra para ser usado no html
            string fnBarraUrl = url + @"BoletoNetBarra.gif";

            //Salvo a imagem apenas 1 vez
            if (!File.Exists(fnBarra))
                Html.barra.Save(fnBarra);

            //Prepara o arquivo do c�digo de barras para ser salvo
            string fnCodigoBarras = string.Format("{0}{1}_codigoBarras.jpg", pathServer, fileName);
            //Prepara o arquivo do c�digo de barras para ser usado no html
            string fnCodigoBarrasUrl = string.Format("{0}{1}_codigoBarras.jpg", url, fileName);

            C2of5i cb = new C2of5i(Boleto.CodigoBarra.Codigo, 1, 50, Boleto.CodigoBarra.Codigo.Length);

            //Salva o arquivo conforme o fileName
            cb.ToBitmap().Save(fnCodigoBarras);

            //Retorna o Html para ser usado na view
            return HtmlOffLine(null, fnLogoUrl, fnBarraUrl, fnCodigoBarrasUrl).ToString();
        }

        #endregion Gera��o do Html OffLine

        public System.Drawing.Image GeraImagemCodigoBarras(Boleto boleto)
        {
            C2of5i cb = new C2of5i(boleto.CodigoBarra.Codigo, 1, 50, boleto.CodigoBarra.Codigo.Length);
            return cb.ToBitmap();
        }
    }
}