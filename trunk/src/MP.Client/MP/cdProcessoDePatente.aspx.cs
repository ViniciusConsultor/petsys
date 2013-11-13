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
    public partial class cdProcessoDePatente : SuperPagina
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
            txtDataDeCadastro.SelectedDate = DateTime.Now;
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
            txtDataDeCadastro.SelectedDate = processoDePatente.DataDoCadastro;
            txtDataDeConcessao.SelectedDate = processoDePatente.DataDaConcessao;
            txtDataDePublicacao.SelectedDate = processoDePatente.DataDaPublicacao;
            txtDataDoDeposito.SelectedDate = processoDePatente.DataDoDeposito;
            txtDataDaVigencia.SelectedDate = processoDePatente.DataDaVigencia;
            txtDataDoExame.SelectedDate = processoDePatente.DataDoExame;
            rblProcessoEhDeTerceiro.SelectedValue = processoDePatente.ProcessoEhDeTerceiro ? "1" : "0";

            if (processoDePatente.Procurador != null)
            {
                ctrlProcurador.ProcuradorSelecionado = processoDePatente.Procurador;
                ctrlProcurador.Nome = processoDePatente.Procurador.Pessoa.Nome;
            }

            rblProcessoEhEstrangeiro.SelectedValue = processoDePatente.ProcessoEhEstrangeiro ? "1" : "0";
            rblEstaAtivo.SelectedValue = processoDePatente.Ativo ? "1" : "0";

            if (processoDePatente.Pasta != null)
            {
                ctrlPasta.PastaSelecionada = processoDePatente.Pasta;
                ctrlPasta.Nome = processoDePatente.Pasta.Nome;
            }


            if (processoDePatente.Despacho != null)
            {
                ctrlDespachoDePatentes.DespachoDePatentesSelecionada = processoDePatente.Despacho;
                ctrlDespachoDePatentes.CodigoDespachoDePatentes = processoDePatente.Despacho.Codigo;
            }


            if (processoDePatente.PCT != null)
            {
                rblEHPCT.SelectedValue = "1";
                txtNumeroPCT.Text = processoDePatente.PCT.Numero;
                txtNumeroPCTWO.Text = processoDePatente.PCT.NumeroWO;
                txtDataDaPublicacaoPCT.SelectedDate = processoDePatente.PCT.DataDaPublicacao;
                txtDataDoDepositoPCT.SelectedDate = processoDePatente.PCT.DataDoDeposito;
            }
        }

        private void LimpaTela()
        {
            ViewState[CHAVE_ID] = null;

            var controle = pnlProcessoDeMarca as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            ctrlPatente1.Inicializa();
            ctrlProcurador.Inicializa();
            ctrlDespachoDePatentes.Inicializa();
            ctrlPasta.Inicializa();

            ctrlPatente1.BotaoNovoEhVisivel = true;
            ctrlProcurador.BotaoNovoEhVisivel = true;
            ctrlDespachoDePatentes.BotaoNovoEhVisivel = true;
            ctrlPasta.BotaoNovoEhVisivel = true;

            rblProcessoEhDeTerceiro.Items.Clear();
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Não", "0"));
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Sim", "1"));
            rblProcessoEhDeTerceiro.SelectedValue = "0";

            rblProcessoEhEstrangeiro.Items.Clear();
            rblProcessoEhEstrangeiro.Items.Add(new ListItem("Não", "0"));
            rblProcessoEhEstrangeiro.Items.Add(new ListItem("Sim", "1"));
            rblProcessoEhEstrangeiro.SelectedValue = "0";


            rblEstaAtivo.Items.Clear();
            rblEstaAtivo.Items.Add(new ListItem("Não", "0"));
            rblEstaAtivo.Items.Add(new ListItem("Sim", "1"));
            rblEstaAtivo.SelectedValue = "1";

            rblEstaAtivo.Items.Clear();
            rblEstaAtivo.Items.Add(new ListItem("Não", "0"));
            rblEstaAtivo.Items.Add(new ListItem("Sim", "1"));
            rblEstaAtivo.SelectedValue = "1";

            rblEHPCT.Items.Clear();
            rblEHPCT.Items.Add(new ListItem("Não", "0"));
            rblEHPCT.Items.Add(new ListItem("Sim", "1"));
            rblEHPCT.SelectedValue = "0";
            MostraPCT(false);

            txtDataDeCadastro.Enabled = false;

        }

        private void MostraPCT( bool mostra)
        {
            pnlPCT.Visible = mostra;
        }

        private IProcessoDePatente MontaObjeto()
        {
            var processoDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDePatente>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                processoDePatente.IdProcessoDePatente = Convert.ToInt64(ViewState[CHAVE_ID]);

            processoDePatente.Patente = ctrlPatente1.PatenteSelecionada;
            processoDePatente.Processo = txtProcesso.Text;
            processoDePatente.DataDoCadastro = txtDataDeCadastro.SelectedDate.Value;
            processoDePatente.DataDaConcessao = txtDataDeConcessao.SelectedDate;
            processoDePatente.DataDaPublicacao = txtDataDePublicacao.SelectedDate;
            processoDePatente.DataDoDeposito = txtDataDoDeposito.SelectedDate;
            processoDePatente.DataDaVigencia = txtDataDaVigencia.SelectedDate;
            processoDePatente.DataDoExame = txtDataDoExame.SelectedDate;
            processoDePatente.ProcessoEhDeTerceiro = rblProcessoEhDeTerceiro.SelectedValue != "0";
            processoDePatente.Procurador = ctrlProcurador.ProcuradorSelecionado;
            processoDePatente.ProcessoEhEstrangeiro = rblProcessoEhEstrangeiro.SelectedValue != "0";
            processoDePatente.Ativo = rblEstaAtivo.SelectedValue != "0";
            processoDePatente.Despacho = ctrlDespachoDePatentes.DespachoDePatentesSelecionada;
            processoDePatente.Pasta = ctrlPasta.PastaSelecionada;

            if (rblEHPCT.SelectedValue != "0")
            {
                var pct = FabricaGenerica.GetInstancia().CrieObjeto<IPCT>();
                pct.Numero = txtNumeroPCT.Text;
                pct.NumeroWO = txtNumeroPCTWO.Text;
                pct.DataDaPublicacao = txtDataDaPublicacaoPCT.SelectedDate;
                pct.DataDoDeposito = txtDataDoDepositoPCT.SelectedDate;
            }

            return processoDePatente;
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsitencias = new List<string>();

            if (ctrlPatente1.PatenteSelecionada == null) inconsitencias.Add("É necessário informar uma patente.");

            if (string.IsNullOrEmpty(txtProcesso.Text)) inconsitencias.Add("É necessário informar o número do processo da patente.");

            if (!txtDataDeCadastro.SelectedDate.HasValue) inconsitencias.Add("É necessário informar a data de cadastro.");
            
            if (rblProcessoEhDeTerceiro.SelectedValue =="0" && ctrlProcurador.ProcuradorSelecionado == null) inconsitencias.Add("É necessário informar o procurador.");
            
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

        protected void rblEHPCT_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            MostraPCT((sender as RadioButtonList).SelectedValue == "1");
        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }
    }
}