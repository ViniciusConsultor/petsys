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
    public partial class ctrlFiltroRevistaPatente : UserControl
    {
        protected void Page_Load(object sender, EventArgs e) { }

        public void Inicializa()
        {
            LimparControle();
            CarregueCombo();
            ctrlUF.Inicializa();
        }

        private void LimparControle()
        {
            var controleFiltroPatente = cboFiltroPatente as Control;
            UtilidadesWeb.LimparComponente(ref controleFiltroPatente);
            cboFiltroPatente.ClearSelection();
            txtValor.Text = string.Empty;
        }

        private void CarregueCombo()
        {
            foreach (EnumeradorFiltroPatente enumerador in EnumeradorFiltroPatente.ObtenhaTodos())
            {
                var item = new RadComboBoxItem(enumerador.Descricao, enumerador.Id.ToString());
                item.Attributes.Add("Codigo", enumerador.Id.ToString());
                cboFiltroPatente.Items.Add(item);
                item.DataBind();
            }
        }

        public string Codigo
        {
            get { return cboFiltroPatente.SelectedValue; }
            set { cboFiltroPatente.SelectedValue = value; }
        }

        public string ValorFiltro
        {
            get { return txtValor.Text; }
        }

        public string EstadoSelecionado
        {
            get
            {
                if (ctrlUF.Sigla == null)
                    return string.Empty;

                return ctrlUF.Sigla.Sigla;
            }
        }

        public string NomeDoProcurador
        {
            get { return ctrlProcurador.Nome; }
        }

        public string Despacho
        {
            get { return ctrlDespachoDePatentes.CodigoDespachoDePatentes; }
        }
    }
}