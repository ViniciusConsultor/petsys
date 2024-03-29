﻿using System;
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
    public partial class ctrlNaturezaDeMarca : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Inicializa()
        {
            LimparControle();
            CarregueCombo();
        }

        private void LimparControle()
        {
            var controle = cboNatureza as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            cboNatureza.ClearSelection();
        }


        public string Codigo
        {
            get { return cboNatureza.SelectedValue; }
            set
            {
                var natureza = NaturezaDeMarca.ObtenhaPorCodigo(Convert.ToInt32(value));

                if (natureza != null)
                {
                    cboNatureza.SelectedValue = natureza.Codigo.ToString();
                    cboNatureza.Text = natureza.Nome;
                }
            }
        }


        private void CarregueCombo()
        {
            foreach (var natureza in NaturezaDeMarca.ObtenhaTodas())
            {
                var item = new RadComboBoxItem(natureza.Nome, natureza.Codigo.ToString());

                item.Attributes.Add("Codigo", natureza.Codigo.ToString());

                cboNatureza.Items.Add(item);
                item.DataBind();
            }
        }
    }
}