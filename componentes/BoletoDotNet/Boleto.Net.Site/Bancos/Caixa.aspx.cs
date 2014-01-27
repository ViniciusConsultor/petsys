using System;
using BoletoNet;

public partial class Bancos_Caixa : System.Web.UI.Page
{
    void Page_PreInit(object sender, EventArgs e)
    {
        if (IsPostBack)
            MasterPageFile = "~/MasterPrint.master";
    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime vencimento = new DateTime(2014, 01, 01);

        Cedente c = new Cedente("000.000.000-00", "Boleto.net ILTDA", "1394", "302864", "4");

        c.Codigo = "00100302864";


        Boleto b = new Boleto(vencimento, 50, "SR", "8210000709", c);

        b.Sacado = new Sacado("000.000.000-00", "Nome do seu Cliente ");
        b.Sacado.Endereco.End = "Endere�o do seu Cliente ";
        b.Sacado.Endereco.Bairro = "Bairro";
        b.Sacado.Endereco.Cidade = "Cidade";
        b.Sacado.Endereco.CEP = "00000000";
        b.Sacado.Endereco.UF = "UF";

        //Adiciona as instru��es ao boleto
        #region Instru��es
        Instrucao_Caixa item; 
        //ImportanciaporDiaDesconto
        item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.Multa, Convert.ToDecimal(0));
        b.Instrucoes.Add(item);
        item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.JurosdeMora, Convert.ToDecimal(0));
        b.Instrucoes.Add(item);
        item = new Instrucao_Caixa((int)EnumInstrucoes_Caixa.NaoReceberAposNDias, 90);
        b.Instrucoes.Add(item);
        #endregion Instru��es
        EspecieDocumento_Caixa ed = new EspecieDocumento_Caixa();
        b.EspecieDocumento = new EspecieDocumento_Caixa(ed.getCodigoEspecieByEnum(EnumEspecieDocumento_Caixa.DuplicataMercantil));
        b.NumeroDocumento = "00001";
        b.DataProcessamento = DateTime.Now;
        b.DataDocumento = DateTime.Now;
        boletoBancario.Boleto = b;
        boletoBancario.Boleto.Valida();
        //lblCodigoBarras.Text = b.CodigoBarra.Codigo.ToString();

        boletoBancario.MostrarComprovanteEntrega = (Request.Url.Query == "?show");
    }
}
