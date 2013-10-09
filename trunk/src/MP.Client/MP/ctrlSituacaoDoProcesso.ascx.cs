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
        //public static event SituacaoDoProcessoFoiSelecionadaEventHandler SituacaoDoProcessoFoiSelecionada;
        //public delegate void SituacaoDoProcessoFoiSelecionadaEventHandler(ISituacaoDoProcesso situacaoDoProcesso);

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void Inicializa()
        {
            //AutoPostBack = true;
            LimparControle();
            CarregueCombo();
        }

        private void LimparControle()
        {
            var controle = cboSituacaoDoProcesso as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboSituacaoDoProcesso.ClearSelection();
        }
        
        public string Codigo
        {
            get { return cboSituacaoDoProcesso.SelectedValue; }
            set
            {
                var descricaoSituacao =
                        SituacaoDoProcesso.RetornaDescricaoPorCodigo(Convert.ToInt32(value));

                cboSituacaoDoProcesso.Items.Clear();
                CarregueCombo();

                    cboSituacaoDoProcesso.Text = descricaoSituacao;
                    cboSituacaoDoProcesso.SelectedValue = value;
            }
        }

        private void CarregueCombo()
        {
            foreach (var situacao in SituacaoDoProcesso.ObtenhaSituacoesDoProcesso())
            {
                var item = new RadComboBoxItem(situacao.DescricaoSituacao, situacao.CodigoSituacaoProcesso.ToString());

                item.Attributes.Add("Codigo", situacao.CodigoSituacaoProcesso.ToString());

                cboSituacaoDoProcesso.Items.Add(item);
                item.DataBind();
            }
        }

        protected void cboSituacaoDoProcesso_ItemsRequested(object sender, RadComboBoxItemsRequestedEventArgs e)
        {
            
        }

        protected void cboSituacaoDoProcesso_SelectedIndexChanged(object o, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            
        }
    }
}