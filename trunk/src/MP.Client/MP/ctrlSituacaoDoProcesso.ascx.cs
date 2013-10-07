using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;
using Compartilhados.Componentes.Web;

namespace MP.Client.MP
{
    public partial class ctrlSituacaoDoProcesso : System.Web.UI.UserControl
    {
        public static event SituacaoDoProcessoFoiSelecionadaEventHandler SituacaoDoProcessoFoiSelecionada;
        public delegate void SituacaoDoProcessoFoiSelecionadaEventHandler(ISituacaoDoProcesso situacaoDoProcesso);

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
            Control controlePanel = pnlSituacaoDoProcesso;
            UtilidadesWeb.LimparComponente(ref controlePanel);
            SituacaoDoProcessoSelecionada = null;
        }

        public bool EnableLoadOnDemand
        {
            set { cboSituacaoDoProcesso.EnableLoadOnDemand = value; }
        }

        public bool ShowDropDownOnTextboxClick
        {
            set { cboSituacaoDoProcesso.ShowDropDownOnTextboxClick = value; }
        }

        public string IdSituacaoProcesso
        {
            get { return cboSituacaoDoProcesso.Text; }
            set { cboSituacaoDoProcesso.Text = value; }
        }

        public string DescricaoSituacao
        {
            get { return cboSituacaoDoProcesso.Attributes["DescricaoSituacao"]; }
            set { cboSituacaoDoProcesso.Attributes["DescricaoSituacao"] = value; }
        }

        public ISituacaoDoProcesso SituacaoDoProcessoSelecionada
        {
            get { return (ISituacaoDoProcesso)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

        public string TextoItemVazio
        {
            set { cboSituacaoDoProcesso.EmptyMessage = value; }
        }

        protected void cboSituacaoDoProcesso_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            IList<ISituacaoDoProcesso> listaSituacaoDoProcesso = new List<ISituacaoDoProcesso>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeSituacaoDoProcesso>())
            {
                listaSituacaoDoProcesso = servico.obtenhaTodasSituacoesDoProcesso();
            }

            if (listaSituacaoDoProcesso.Count > 0)
            {
                foreach (var situacaoDoProcesso in listaSituacaoDoProcesso)
                {
                    var item = new RadComboBoxItem(situacaoDoProcesso.IdSituacaoProcesso.Value.ToString(), situacaoDoProcesso.IdSituacaoProcesso.Value.ToString());

                    item.Attributes.Add("DescricaoSituacao",
                                        situacaoDoProcesso.DescricaoSituacao ?? "Não informada");

                    this.cboSituacaoDoProcesso.Items.Add(item);
                    item.DataBind();
                }
            }
        }

        protected void cboSituacaoDoProcesso_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ISituacaoDoProcesso situacaoDoProcesso = null;

            if (string.IsNullOrEmpty(((RadComboBox)o).SelectedValue))
                return;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeSituacaoDoProcesso>())
            {
                situacaoDoProcesso = servico.obtenhaSituacaoDoProcessoPeloId(Convert.ToInt64(((RadComboBox)o).SelectedValue));
            }

            cboSituacaoDoProcesso.Text = situacaoDoProcesso.DescricaoSituacao;

            SituacaoDoProcessoSelecionada = situacaoDoProcesso;

            if (SituacaoDoProcessoFoiSelecionada != null)
            {
                SituacaoDoProcessoFoiSelecionada(situacaoDoProcesso);
            }
        }

        public bool AutoPostBack
        {
            set { cboSituacaoDoProcesso.AutoPostBack = value; }
        }

        public ctrlSituacaoDoProcesso()
	    {
		    Load += Page_Load;
	    }
    }
}