using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Negocio.Filtros.Marca;
using PMP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace PMP.Client.PMP
{
    public partial class frmDetalharPesquisaDeMarca : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                MostreDetalhes(Request.QueryString["Id"]);
        }

        private void MostreDetalhes(string ID)
        {
            
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarcaDeRevista>())
            {
                var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroPorId>();
                filtro.ValorDoFiltro = ID;
                filtro.Operacao = OperacaoDeFiltro.IgualA;
                var processo =  servico.ObtenhaResultadoDaPesquisa(filtro, 1, 0)[0] ;

                lblCodigoDespacho.Text = processo.CodigoDoDespacho;
                lblDataDeConcessao.Text = processo.DataDaConcessao.HasValue
                                         ? processo.DataDaConcessao.Value.ToString("dd/MM/yyyy")
                                         : null;
                lblDataDeVigencia.Text = processo.DataDaVigencia.HasValue
                    ? processo.DataDaVigencia.Value.ToString("dd/MM/yyyy") : null;

                lblDataDoDeposito.Text = processo.DataDoDeposito.HasValue
                                             ? processo.DataDoDeposito.Value.ToString("dd/MM/yyyy")
                                             : null;
                lblDataPublicacaoRPI.Text = processo.DataDePublicacaoDaRevista.ToString("dd/MM/yyyy");
                lblDespacho.Text = processo.NomeDoDespacho;
                lblMarca.Text = processo.Marca;
                lblNumeroProcessoDeMarca.Text = processo.NumeroProcessoDeMarca;
                lblNumeroRPI.Text = processo.NumeroDaRevista.ToString();
                lblPaisDoTitular.Text = processo.PaisTitular;
                lblUFTitular.Text = processo.UFTitular;
                lblTitular.Text = processo.Titular;
                lblProcurador.Text = processo.Procurador;
                lblNatureza.Text = processo.Natureza;
                lblApresentacao.Text = processo.Apresentacao;
                lblClasseNice.Text = processo.CodigoClasseNice;
                lblEdicaoClasseViena.Text = processo.EdicaoClasseViena;
                lblClasseViena.Text = processo.CodigosClasseViena == null
                                          ? null
                                          : UtilidadesWeb.ObtenhaListaDeStringComQuebraDeLinhaWeb(
                                              processo.CodigosClasseViena);
                lblCodigoClasseNacional.Text = processo.CodigoClasseNacional;
                lblSubClasseNacional.Text = processo.CodigosSubClasseNacional == null
                                          ? null
                                          : UtilidadesWeb.ObtenhaListaDeStringComQuebraDeLinhaWeb(
                                              processo.CodigosSubClasseNacional);
            }


        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
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