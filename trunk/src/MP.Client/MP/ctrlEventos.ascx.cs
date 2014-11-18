using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class ctrlEventos : System.Web.UI.UserControl
    {
        private const string CHAVE_EVENTOS = "CHAVE_EVENTOS";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void LimpaTela()
        {
            txtDataDoEvento.SelectedDate = null;
            txtDescricaoEvento.Text = "";
        }

        public void Inicializa()
        {
            var controle = pnlEventos as Control;
            UtilidadesWeb.LimparComponente(ref controle);

            LimpaTela();
            ViewState[CHAVE_EVENTOS] = new List<IEvento>();
            ExibaEventos();
        }

        public IList<IEvento> Eventos()
        {
            return (IList<IEvento>)ViewState[CHAVE_EVENTOS];
        }

        public void SetaEventos(IList<IEvento> eventos)
        {
            ViewState[CHAVE_EVENTOS] = eventos;
            ExibaEventos();
        }

        private void ExibaEventos()
        {
            var eventos = Eventos();

            if (eventos == null)
                eventos = new List<IEvento>();

            grdEventos.DataSource = eventos;
            grdEventos.DataBind();
        }

        protected void grdEventos_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdEventos, Eventos(), e);
        }


        private IList<string> VerifiqueInconsistencias()
        {
            var inconsistencias = new List<string>();

            if (!txtDataDoEvento.SelectedDate.HasValue)
                inconsistencias.Add("A data do evento deve ser informada.");

            if (string.IsNullOrEmpty(txtDescricaoEvento.Text))
                inconsistencias.Add("A descrição do evento deve ser informada.");


            return inconsistencias;
        }

        protected void btnAdicionarEvento_OnClick(object sender, EventArgs e)
        {

            var inconsistencias = VerifiqueInconsistencias();

            if (inconsistencias.Count > 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                      UtilidadesWeb.MostraMensagemDeInconsistencias(inconsistencias), false);
                return;
            }

            var eventos = Eventos();

            if (eventos == null)
                eventos = new List<IEvento>();

            eventos.Add(MontaEvento());
            ViewState[CHAVE_EVENTOS] = eventos;
            ExibaEventos();
            LimpaTela();
        }


        private IEvento MontaEvento()
        {
            var evento = FabricaGenerica.GetInstancia().CrieObjeto<IEvento>();

            evento.Data = txtDataDoEvento.SelectedDate.Value;
            evento.Descricao = txtDescricaoEvento.Text;

            return evento;
        }

        protected void grdEventos_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            int indiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                indiceSelecionado = e.Item.ItemIndex;


            if (e.CommandName == "Excluir")
            {
                var eventos = Eventos();
                eventos.RemoveAt(indiceSelecionado);
                ViewState[CHAVE_EVENTOS] = eventos;
                ExibaEventos();
            }
        }
    }
}