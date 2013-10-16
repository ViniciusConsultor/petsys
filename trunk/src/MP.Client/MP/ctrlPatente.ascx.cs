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
    public partial class ctrlPatente : UserControl
    {
        public static event PatenteFoiSelecionadaEventHandler PatenteFoiSelecionada;
        public delegate void PatenteFoiSelecionadaEventHandler(IPatente patente);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Incializa()
        {
            LimparControle();
        }

        protected void cboProcurador_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                IPatente patente = null;

                if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
                    return;

                int codigoSelecionado = int.Parse(((RadComboBox)sender).SelectedValue);
                patente = servico.ObtenhaPatente(codigoSelecionado);

                if (PatenteFoiSelecionada != null)
                {
                    PatenteSelecionada = patente;
                    PatenteFoiSelecionada(patente);
                }
            }
        }

        public IPatente PatenteSelecionada
        {
            get { return (IPatente)ViewState[ClientID]; }
            set { ViewState.Add(ClientID, value); }
        }

        protected void cboProcurador_OnItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                cboPatente.Items.Clear();

                foreach (IPatente patente in servico.ObtenhaPatentesPeloTitulo(e.Text, 50))
                {
                    var item = new RadComboBoxItem(patente.TituloPatente, patente.Identificador.ToString());

                    item.Attributes.Add("TipoDePatente", patente.TipoDePatente.DescricaoTipoDePatente);
                    item.Attributes.Add("Resumo", patente.Resumo);
                    item.Attributes.Add("DataDeCadastro", patente.DataCadastro != null ? patente.DataCadastro.Value.ToString("dd/MM/yyyy") : "Data não informada");

                    cboPatente.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        private void LimparControle()
        {
            var controle = cboPatente as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboPatente.ClearSelection();
            PatenteFoiSelecionada = null;
        }

        public bool EnableLoadOnDemand
        {
            set { cboPatente.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboPatente.ShowDropDownOnTextboxClick = value; }
        }
    }
}