using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Interfaces.FN.Negocio;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class ctrlSituacao : System.Web.UI.UserControl
    {

        public event SituacaoFoiSelecionadoEventHandler SituacaoFoiSelecionada;
        public delegate void SituacaoFoiSelecionadoEventHandler(Situacao situacao);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cboSituacao_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            if (!cboSituacao.AutoPostBack) return;

            Situacao situacao = null;

            if (string.IsNullOrEmpty(((RadComboBox)sender).SelectedValue))
            {
                LimparControle();
                return;
            }

            situacao = Situacao.Obtenha(Convert.ToInt16(((RadComboBox)sender).SelectedValue));

            SituacaoSelecionada = situacao;

            if (SituacaoFoiSelecionada != null)
                SituacaoFoiSelecionada(situacao);
        }

        public void Inicializa()
        {
            LimparControle();
            CarregueCombo();
        }

        private void CarregueCombo()
        {
            foreach (var situacao in Situacao.ObtenhaTodos())
            {
                var item = new RadComboBoxItem(situacao.Descricao, situacao.ID.ToString());
                
                cboSituacao.Items.Add(item);
                item.DataBind();
            }
        }

        private void LimparControle()
        {
            var controle = cboSituacao as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboSituacao.ClearSelection();
        }

        public string Codigo
        {
            get { return cboSituacao.SelectedValue; }
            set
            {
                var situacao = Situacao.Obtenha(Convert.ToInt16(value));

                if (situacao != null)
                {
                    cboSituacao.SelectedValue = situacao.ID.ToString();
                    cboSituacao.Text = situacao.Descricao;
                }
            }
        }

        public Situacao SituacaoSelecionada
        {
            get { return (Situacao)ViewState[ClientID]; }
            set { ViewState.Add(this.ClientID, value); }
        }

       
        public bool AutoPostBack
        {
            set { cboSituacao.AutoPostBack = value; }
        }
    }
}