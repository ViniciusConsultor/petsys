using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class cdProcessoDePatente : System.Web.UI.Page
    {
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PROCESSO_DE_PATENTE";
        private const string CHAVE_ID = "CHAVE_ID_PROCESSO_DE_PATENTE";

        private enum Estado : byte
        {
            Novo,
            Modifica,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (IsPostBack) return;

            Nullable<long> id = null;

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                id = Convert.ToInt64(Request.QueryString["Id"]);

            if (id == null)
                ExibaTelaNovo();
            else
                ExibaTelaDetalhes(id.Value);
        }

        private void ExibaTelaNovo()
        {
            ViewState[CHAVE_ESTADO] = Estado.Novo;
            LimpaTela();
            txtDataDeEntrada.SelectedDate = DateTime.Now;
        }

        private void ExibaTelaDetalhes(long id)
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
            LimpaTela();

            IProcessoDePatente processoDePatente = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
            {
                processoDePatente = servico.Obtenha(id);
            }

            if (processoDePatente != null) MostreProcessoDePatente(processoDePatente);
        }

        private void ExibaTelaModificar()
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
        }


        private void MostreProcessoDePatente(IProcessoDePatente processoDePatente)
        {
            ViewState[CHAVE_ID] = processoDePatente.IdProcessoDePatente;
            ctrlPatente1.PatenteSelecionada = processoDePatente.Patente;
            ctrlPatente1.TituloPatente = processoDePatente.Patente.TituloPatente;

            txtProcesso.Text = processoDePatente.Processo;

            txtDataDeEntrada.SelectedDate = processoDePatente.DataDeEntrada;

            rblProcessoEhDeTerceiro.SelectedValue = processoDePatente.ProcessoEhDeTerceiro ? "1" : "0";
        }

        private void LimpaTela()
        {
            ViewState[CHAVE_ID] = null;

            var controle = pnlProcessoDeMarca as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            ctrlPatente1.Inicializa();
            ctrlProcurador.Inicializa();

            ctrlPatente1.BotaoNovoEhVisivel = true;
            ctrlProcurador.BotaoNovoEhVisivel = true;

            rblProcessoEhDeTerceiro.Items.Clear();
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Não", "0"));
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Sim", "1"));
            rblProcessoEhDeTerceiro.SelectedValue = "0";

            rblProcessoEhEstrangeiro.Items.Clear();
            rblProcessoEhEstrangeiro.Items.Add(new ListItem("Não", "0"));
            rblProcessoEhEstrangeiro.Items.Add(new ListItem("Sim", "1"));
            rblProcessoEhEstrangeiro.SelectedValue = "0";


            rblSituacao.Items.Clear();
            rblSituacao.Items.Add(new ListItem("Não", "0"));
            rblSituacao.Items.Add(new ListItem("Sim", "1"));
            rblSituacao.SelectedValue = "0";
        }

        private IProcessoDePatente MontaObjeto()
        {
            var processoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDePatente>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                processoDePatente.IdProcessoDePatente = Convert.ToInt64(ViewState[CHAVE_ID]);

            processoDePatente.Patente = ctrlPatente1.PatenteSelecionada;
            processoDePatente.Processo = txtProcesso.Text;
            processoDePatente.DataDeEntrada = txtDataDeEntrada.SelectedDate.Value;
            processoDePatente.ProcessoEhDeTerceiro = rblProcessoEhDeTerceiro.SelectedValue != "0";
            processoDePatente.Procurador = ctrlProcurador.ProcuradorSelecionado;

            return processoDePatente;
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsitencias = new List<string>();

            if (ctrlPatente1.PatenteSelecionada == null) inconsitencias.Add("É necessário informar uma patente.");

            if (string.IsNullOrEmpty(txtProcesso.Text)) inconsitencias.Add("É necessário informar o processo da patente.");

            if (!txtDataDeEntrada.SelectedDate.HasValue) inconsitencias.Add("É necessário informar a data de entrada.");

            if (!txtDataDeEntrada.SelectedDate.HasValue) inconsitencias.Add("É necessário informar a data de entrada.");

            if (rblProcessoEhDeTerceiro.SelectedValue !="0" && ctrlProcurador.ProcuradorSelecionado == null) inconsitencias.Add("É necessário informar o procurador.");
            
            return inconsitencias;
        }

        protected void btnSalvar_Click()
        {
            var inconsitencias = VerifiqueCamposObrigatorios();

            if (inconsitencias.Count != 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsistencias(inconsitencias),
                                                        false);
                return;
            }

            var processoDePatente = MontaObjeto();
            string mensagem;

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(processoDePatente);
                        mensagem = "Processo de patente cadastrado com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(processoDePatente);
                        mensagem = "Processo de patente modificado com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(mensagem), false);
                ExibaTelaModificar();

            }
            catch (BussinesException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }

        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnSalvar":
                    btnSalvar_Click();
                    break;
            }
        }
    }
}