using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class ctrlInventor : System.Web.UI.UserControl
    {
        public static event InventorFoiSelecionadoEventHandler InventorFoiSelecionado;
        public delegate void InventorFoiSelecionadoEventHandler(IInventor inventor);
        private const string ID_VIEW_INVENTOR = "ID_VIEW_INVENTOR";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            ViewState[ID_VIEW_INVENTOR] = null;
            LimparControle();
            CarregueCombo();
        }

        private void LimparControle()
        {
            var controle = cboInventor as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboInventor.ClearSelection();
        }

        private void CarregueCombo()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
            {
                cboInventor.Items.Clear();
                Inventores = new List<IInventor>();

                foreach (IInventor inventor in servico.ObtenhaPorNomeComoFiltro(string.Empty, 50))
                {
                    var item = new RadComboBoxItem(inventor.Pessoa.Nome, inventor.Pessoa.ID.ToString());

                    item.Attributes.Add("DataDoCadastro", inventor.DataDoCadastro.ToString());
                    item.Attributes.Add("InformacoesAdicionais", inventor.InformacoesAdicionais);

                    cboInventor.Items.Add(item);
                    item.DataBind();

                    Inventores.Add(inventor);
                }
            }
        }

        private IList<IInventor> Inventores
        {
            get { return (IList<IInventor>)ViewState[ID_VIEW_INVENTOR]; }
            set { ViewState[ID_VIEW_INVENTOR] = value; }
        }

        protected void cboInventor_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
            {
                cboInventor.Items.Clear();
                Inventores = new List<IInventor>();

                foreach (IInventor inventor in servico.ObtenhaPorNomeComoFiltro(e.Text, 50))
                {
                    var item = new RadComboBoxItem(inventor.Pessoa.Nome, inventor.Pessoa.ID.ToString());

                    item.Attributes.Add("DataDoCadastro", inventor.DataDoCadastro.ToString());
                    item.Attributes.Add("InformacoesAdicionais", inventor.InformacoesAdicionais);

                    cboInventor.Items.Add(item);
                    item.DataBind();

                    Inventores.Add(inventor);
                }
            }
        }

        protected void cboInventor_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            IInventor inventor = null;

            if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
                return;

            int codigoSelecionado = int.Parse(((RadComboBox)sender).SelectedValue);
            inventor = Inventores.ToList().Find(inventorAtual => inventorAtual.Pessoa.ID == codigoSelecionado);

            if (InventorFoiSelecionado != null)
                InventorFoiSelecionado(inventor);
        }
    }
}