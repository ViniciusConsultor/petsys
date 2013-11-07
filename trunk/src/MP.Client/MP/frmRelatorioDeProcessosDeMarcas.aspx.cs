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
    public partial class frmRelatorioDeProcessosDeMarcas : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                AjusteControles();
        }

        private void AjusteControles()
        {
            var controle = pnlFiltro as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            ctrlCliente.Inicializa();
            ctrlCliente.BotaoNovoEhVisivel = false;
            ctrlDespacho.Inicializa();
            ctrlDespacho.BotaoNovoEhVisivel = false;
            DespachosSelecionados = new List<IDespachoDeMarcas>();
            MostraDespachosSelecionados();
        }
        
        private void MostraDespachosSelecionados()
        {
            grdDespachos.DataSource = DespachosSelecionados;
            grdDespachos.DataBind();
        }

        private IList<IDespachoDeMarcas> DespachosSelecionados
        {
            get { return (IList<IDespachoDeMarcas>) ViewState["DESPACHOS_SELECIONADOS"]; }
            set { ViewState["DESPACHOS_SELECIONADOS"] = value; }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        protected void btnAdicionarDespacho_OnClick(object sender, ImageClickEventArgs e)
        {
            var despachoSelecionado = ctrlDespacho.DespachoDeMarcasSelecionada;

            if (!DespachosSelecionados.Contains(despachoSelecionado))
            {
                DespachosSelecionados.Add(despachoSelecionado);
                MostraDespachosSelecionados();
                ctrlDespacho.Inicializa();
                return;
            }

            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInconsitencia("O despacho já foi adicionado na lista"),
                                                       false);
        }

        protected void grdDespachos_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdDespachos,DespachosSelecionados,e);
        }

        protected void grdDespachos_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int indiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
            {
            indiceSelecionado = e.Item.ItemIndex;
            }

            switch (e.CommandName)
            {
                case "Excluir":
                    DespachosSelecionados.RemoveAt(indiceSelecionado);
                    MostraDespachosSelecionados();
                    break;
            }
        }

        protected void btnPesquisar_OnClick(object sender, ImageClickEventArgs e)
        {
            long? IDCliente = null;
            long? IDGrupoDeAtividade= null;
            IList<string> IDsDosDespachos = null;

            if (ctrlCliente.ClienteSelecionado != null)
                IDCliente = ctrlCliente.ClienteSelecionado.Pessoa.ID;

            if (ctrlGrupoDeAtividade.GrupoSelecionado != null)
                IDGrupoDeAtividade = ctrlGrupoDeAtividade.GrupoSelecionado.ID;
           
            if (DespachosSelecionados.Count > 0)
            {
                IDsDosDespachos = DespachosSelecionados.Select(despacho => despacho.IdDespacho.Value.ToString()).ToList();
            }
            
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
            {
                var processos = servico.ObtenhaProcessosDeMarcas(IDCliente, IDGrupoDeAtividade, IDsDosDespachos);
            }
        }
    }
}