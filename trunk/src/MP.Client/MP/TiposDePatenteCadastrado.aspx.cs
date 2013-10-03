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
    public partial class TiposDePatenteCadastrado : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ExibaTelaInicial();
            }
            
        }

        private void ExibaTelaInicial()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            Session[CHAVE_ESTADO] = Estado.Inicial;
            Session[ID_OBJETO] = null;

            CarregueGridTipoDePatente();
        }

        private void CarregueGridTipoDePatente()
        {
            try
            {
                IList<ITipoDePatente> listaTipoDePatente = new List<ITipoDePatente>();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTipoDePatente>())
                {
                    listaTipoDePatente = servico.obtenhaTodosTiposDePatentes();
                }

                if(listaTipoDePatente.Count > 0)
                {
                    this.RadGridTipoDePatente.DataSource = listaTipoDePatente;
                    this.RadGridTipoDePatente.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Consulta,
            Modifica,
            Remove
        }

        protected void btnNovo_Click()
        {
            ExibaTelaNovo();
        }

        private void ExibaTelaNovo()
        {
            Session[CHAVE_ESTADO] = Estado.Novo;

            var URL = ObtenhaURL();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), new Guid().ToString(), UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de Tipos de Patentes", 650, 450), false);
        }

        private string ObtenhaURL()
        {
            var URL = UtilidadesWeb.ObtenhaURLCorrente();

            URL = string.Concat(URL, "CadastroDeTiposDePatentes.aspx");
            return URL;
        }

        public ITipoDePatente TipoPatenteSelecionada
        {
            get { return (ITipoDePatente)Session[ClientID]; }
            set { Session.Add(this.ClientID, value); }
        }

        private void ExibaTelaModificar()
        {
            Session[CHAVE_ESTADO] = Estado.Modifica;

            var idSelecionado = this.RadGridTipoDePatente.SelectedValue;

            var URL = ObtenhaURL();
            URL = string.Concat(URL, "?Id=", TipoPatenteSelecionada.IdTipoPatente.ToString());
            ScriptManager.RegisterClientScriptBlock(this, GetType(), new Guid().ToString(), UtilidadesWeb.ExibeJanelaModal(URL, "Cadastro de Tipos de Patentes", 650, 450), false);
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private void btnModificar_Click()
        {
            ExibaTelaModificar();
        }
       
        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnModificar":
                    btnModificar_Click();
                    break;
                case "btnCancelar":
                    btnCancela_Click();
                    break;
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return string.Empty;
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar; 
        }
    }
}