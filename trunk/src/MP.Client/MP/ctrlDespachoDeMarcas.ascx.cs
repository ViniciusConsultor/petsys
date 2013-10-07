﻿using System;
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
                if (!string.IsNullOrEmpty(e.Text))
                {
                    listaDespachoDeMarcas = servico.ObtenhaPorCodigoDoDespachoComoFiltro(e.Text, int.MaxValue);
                }
                else
                {
                    listaDespachoDeMarcas = servico.obtenhaTodosDespachoDeMarcas();
                }
            }

             IList<ISituacaoDoProcesso> listaSituacaoDoProcesso = new List<ISituacaoDoProcesso>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeSituacaoDoProcesso>())
            {
                listaSituacaoDoProcesso = servico.obtenhaTodasSituacoesDoProcesso();
            }

            if (listaDespachoDeMarcas.Count > 0)
            {
                foreach (var despachoDeMarcas in listaDespachoDeMarcas)
                {
                    var item = new RadComboBoxItem(despachoDeMarcas.CodigoDespacho.ToString(), despachoDeMarcas.IdDespacho.Value.ToString());

                    if(listaSituacaoDoProcesso.Count > 0)
                    {
                        foreach (var situacaoDoProcesso in listaSituacaoDoProcesso)
                        {
                            if(situacaoDoProcesso.IdSituacaoProcesso.Value.ToString().Equals(despachoDeMarcas.IdSituacaoProcesso.Value.ToString()))
                            {
                                item.Attributes.Add("SituacaoProcesso",
                                        situacaoDoProcesso.DescricaoSituacao ?? "Não informada");
                                break;
                            }
                        }
                    }

                    item.Attributes.Add("Registro",
                                        despachoDeMarcas.Registro ?? "Não informada");

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

            txtDescricaoDetalhada.Text = despachoDeMarcas.DetalheDespacho;
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