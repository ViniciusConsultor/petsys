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
    public partial class ctrlDespachoDeMarcas : System.Web.UI.UserControl
    {
        public static event DespachoDeMarcasFoiSelecionadaEventHandler DespachoDeMarcasFoiSelecionada;
        public delegate void DespachoDeMarcasFoiSelecionadaEventHandler(IDespachoDeMarcas despachoDeMarcas);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            AutoPostBack = true;
            LimparControle();
        }

        private void LimparControle()
        {
            Control controlePanel = pnlDespachoDeMarcas;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            DespachoDeMarcasSelecionada = null;
            cboDespachoDeMarcas.ClearSelection();
        }

        public bool EnableLoadOnDemand
        {
            set { cboDespachoDeMarcas.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboDespachoDeMarcas.ShowDropDownOnTextboxClick = value; }
        }

        public string CodigoDespacho
        {
            get { return cboDespachoDeMarcas.Text; }
            set { cboDespachoDeMarcas.Text = value; }
        }

        public string SituacaoProcesso
        {
            get { return cboDespachoDeMarcas.Attributes["SituacaoProcesso"]; }
            set { cboDespachoDeMarcas.Attributes["SituacaoProcesso"] = value; }
        }

        public string Registro
        {
            get { return cboDespachoDeMarcas.Attributes["Registro"]; }
            set { cboDespachoDeMarcas.Attributes["Registro"] = value; }
        }

        public IDespachoDeMarcas DespachoDeMarcasSelecionada
        {
            get { return (IDespachoDeMarcas)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        public string TextoItemVazio
        {
            set { cboDespachoDeMarcas.EmptyMessage = value; }
        }

        protected void cboDespachoDeMarcas_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<IDespachoDeMarcas> listaDespachoDeMarcas = new List<IDespachoDeMarcas>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
            {
                listaDespachoDeMarcas = servico.ObtenhaPorCodigoDoDespachoComoFiltro(e.Text, 50);
            }
            
            if (listaDespachoDeMarcas.Count > 0)
            {
                foreach (var despachoDeMarcas in listaDespachoDeMarcas)
                {
                    var item = new RadComboBoxItem(despachoDeMarcas.CodigoDespacho.ToString(), despachoDeMarcas.IdDespacho.Value.ToString());

                    if(despachoDeMarcas.SituacaoProcesso != null)
                    {
                        item.Attributes.Add("SituacaoProcesso",
                                        despachoDeMarcas.SituacaoProcesso.DescricaoSituacao ?? "Não informada");
                    }
                    
                    if (despachoDeMarcas.Registro)
                    {
                        item.Attributes.Add("Registro",
                                            "Sim" ?? "Não informada");
                    }
                    else
                    {
                        item.Attributes.Add("Registro",
                                            "Não" ?? "Não informada");
                    }

                    this.cboDespachoDeMarcas.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboDespachoDeMarcas_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            IDespachoDeMarcas despachoDeMarcas = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
                return;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
            {
                despachoDeMarcas = servico.obtenhaDespachoDeMarcasPeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));
            }

            DespachoDeMarcasSelecionada = despachoDeMarcas;

            if (DespachoDeMarcasFoiSelecionada != null)
            {
                DespachoDeMarcasFoiSelecionada(despachoDeMarcas);
            }
        }

        public bool AutoPostBack
        {
            set { cboDespachoDeMarcas.AutoPostBack = value; }
        }

        public ctrlDespachoDeMarcas()
	    {
		    Load += Page_Load;
	    }
    }
}