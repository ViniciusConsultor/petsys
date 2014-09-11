using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using MP.Interfaces.Negocio;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmDetalhesLeituraDaRevistaPatente : SuperPagina
    {
        private const string CHAVE_REVISTA_DETALHADA = "CHAVE_REVISTA_DETALHADA";

        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
                return;

           if(Session["CHAVE_REVISTA_DETALHADA"] != null)
               CarregueDetalhes((IRevistaDePatente)Session[CHAVE_REVISTA_DETALHADA]);
        }

        private void CarregueDetalhes(IRevistaDePatente revistaDePatente)
        {
            txtNumeroDoProcesso.Text = revistaDePatente.NumeroDoProcessoFormatado;
            txtNumeroDaRevista.Text = revistaDePatente.NumeroRevistaPatente.ToString();
            txtDataPublicacao.Text = revistaDePatente.DataPublicacao.HasValue ?  revistaDePatente.DataPublicacao.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtDataProcessamento.Text = revistaDePatente.DataProcessamento.HasValue ? revistaDePatente.DataProcessamento.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtDataDeDeposito.Text = revistaDePatente.DataDeDeposito.HasValue ? revistaDePatente.DataDeDeposito.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtNumeroProcessoDaPatente.Text = revistaDePatente.NumeroProcessoDaPatente;
            txtNumeroDoPedido.Text = revistaDePatente.NumeroDoPedido;
            txtDataDaPublicacaoDoPedido.Text = revistaDePatente.DataDaPublicacaoDoPedido.HasValue ? revistaDePatente.DataDaPublicacaoDoPedido.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtDataDeConcessao.Text = revistaDePatente.DataDeConcessao.HasValue ? revistaDePatente.DataDeConcessao.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtPrioridadeUnionista.Text = revistaDePatente.PrioridadeUnionista;
            txtClassificacaoInternacional.Text = revistaDePatente.ClassificacaoInternacional;
            txtTitulo.Text = revistaDePatente.Titulo;
            txtResumo.Text = revistaDePatente.Resumo;
            txtDadosDoPedidoDaPatente.Text = revistaDePatente.DadosDoPedidoDaPatente;
            txtDadosDoPedidoOriginal.Text = revistaDePatente.DadosDoPedidoOriginal;
            txtPrioridadeInterna.Text = revistaDePatente.PrioridadeInterna;
            txtDepositante.Text = revistaDePatente.Depositante;
            txtInventor.Text = revistaDePatente.Inventor;
            txtTitular.Text = revistaDePatente.Titular;
            txtProcurador.Text = revistaDePatente.Procurador;
            txtDadosDepositoInternacional.Text = revistaDePatente.DadosDepositoInternacional;
            txtDadosPublicacaoInternacional.Text = revistaDePatente.DadosPublicacaoInternacional;
            txtCodigoDoDespacho.Text = revistaDePatente.CodigoDoDespacho;
            txtCodigoDoDespachoAnterior.Text = revistaDePatente.CodigoDoDespachoAnterior;
            txtResponsavelPagamentoImpostoDeRenda.Text = revistaDePatente.ResponsavelPagamentoImpostoDeRenda;
            txtComplemento.Text = revistaDePatente.Complemento;
            txtDecisao.Text = revistaDePatente.Decisao;
            txtCedente.Text = revistaDePatente.Cedente;
            txtCessionaria.Text = revistaDePatente.Cessionaria;
            txtObservacao.Text = revistaDePatente.Observacao;
            txtUltimaInformacao.Text = revistaDePatente.UltimaInformacao;
            txtCertificadoDeAverbacao.Text = revistaDePatente.CertificadoDeAverbacao;
            txtPaisCedente.Text = revistaDePatente.PaisCedente;
            txtPaisDaCessionaria.Text = revistaDePatente.PaisDaCessionaria;
            txtSetor.Text = revistaDePatente.Setor;
            txtEnderecoDaCessionaria.Text = revistaDePatente.EnderecoDaCessionaria;
            txtNaturezaDoDocumento.Text = revistaDePatente.NaturezaDoDocumento;
            txtMoedaDePagamento.Text = revistaDePatente.MoedaDePagamento;
            txtValor.Text = revistaDePatente.Valor;
            txtPagamento.Text = revistaDePatente.Pagamento;
            txtPrazo.Text = revistaDePatente.Prazo;
            txtServicosIsentosDeAverbacao.Text = revistaDePatente.ServicosIsentosDeAverbacao;
            txtCriador.Text = revistaDePatente.Criador;
            txtLinguagem.Text = revistaDePatente.Linguagem;
            txtCampoDeAplicacao.Text = revistaDePatente.CampoDeAplicacao;
            txtTipoDePrograma.Text = revistaDePatente.TipoDePrograma;
            txtDataDaCriacao.Text = revistaDePatente.DataDaCriacao.HasValue ? revistaDePatente.DataDaCriacao.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtRegimeDeGuarda.Text = revistaDePatente.RegimeDeGuarda;
            txtRequerente.Text = revistaDePatente.Requerente;
            txtRedacao.Text = revistaDePatente.Redacao;
            txtDataDaProrrogacao.Text = revistaDePatente.DataDaProrrogacao.HasValue ? revistaDePatente.DataDaProrrogacao.Value.ToString("dd/MM/yyyy") : string.Empty;
            txtClassificacaoNacional.Text = revistaDePatente.ClassificacaoNacional;
            txtClassificacao.Text = revistaDePatente.Classificacao;
        }

        protected override string ObtenhaIdFuncao()
        {
            return string.Empty;
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}