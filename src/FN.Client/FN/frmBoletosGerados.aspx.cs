using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Fabricas;
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmBoletosGerados : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ExibaTelaInicial()
        {
            CarregaBoletosGerados();
        }

        private void CarregaBoletosGerados()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
            {
                var listaDeBoletosGerados = servico.obtenhaBoletosGerados(grdBoletosGerados.PageSize, 0);

                if(listaDeBoletosGerados.Count > 0)
                {
                    grdBoletosGerados.VirtualItemCount = listaDeBoletosGerados.Count;
                    grdBoletosGerados.DataSource = ConvertaBoletosGeradosParaDTO(listaDeBoletosGerados);
                    grdBoletosGerados.DataBind();
                }
                
            }
        }

        private IList<DTOBoletosGerados> ConvertaBoletosGeradosParaDTO(IList<IBoletosGerados> listaDeBoletosGerados)
        {
            var listaDeBoletos = new List<DTOBoletosGerados>();

            foreach (var boletosGerado in listaDeBoletosGerados)
            {
                var dto = new DTOBoletosGerados();

                // implementar dto
            }

            return listaDeBoletos;
        }

        protected void grdBoletosGerados_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void grdBoletosGerados_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}