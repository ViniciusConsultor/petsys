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
    public partial class cdProcessoDeMarca : System.Web.UI.Page
    {
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PROCESSO_DE_MARCA";
        private const string CHAVE_ID = "CHAVE_ID_PROCESSO_DE_MARCA";
        
        private enum Estado : byte
        {
            Novo,
            Modifica,
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlMarcas1.BotaoNovoEhVisivel = true;
            ctrlDespacho.BotaoNovoEhVisivel = true;
            ctrlProcurador.BotaoNovoEhVisivel = true;

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

            IProcessoDeMarca processoDeMarca = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                processoDeMarca = servico.Obtenha(id);

            if (processoDeMarca != null) MostreProcessoDeMarca(processoDeMarca);
         
        }

        private void ExibaTelaModificar()
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
        }


        private void MostreProcessoDeMarca(IProcessoDeMarca processoDeMarca)
        {
            ViewState[CHAVE_ID] = processoDeMarca.IdProcessoDeMarca;
            ctrlMarcas1.MarcaSelecionada = processoDeMarca.Marca;
            ctrlMarcas1.DescricaoDaMarca = processoDeMarca.Marca.DescricaoDaMarca;
            txtProcesso.Text = processoDeMarca.Processo.ToString();
            rblEstaAtivo.SelectedValue = processoDeMarca.Ativo ? "1" : "0";
            txtDataDeCadastro.SelectedDate = processoDeMarca.DataDoCadastro;
            txtDataDeDeposito.SelectedDate = processoDeMarca.DataDoDeposito;
            txtDataDeConcessao.SelectedDate = processoDeMarca.DataDeConcessao;
            txtDataDeVigencia.SelectedDate = processoDeMarca.DataDaVigencia;
            rblProcessoEhDeTerceiro.SelectedValue = processoDeMarca.ProcessoEhDeTerceiro ? "1" : "0";

            if (processoDeMarca.Despacho != null)
            {
                ctrlDespacho.DespachoDeMarcasSelecionada = processoDeMarca.Despacho;
                ctrlDespacho.CodigoDespacho = processoDeMarca.Despacho.CodigoDespacho.ToString();
            }

            txtTextoComplementarDoDespacho.Text = processoDeMarca.TextoComplementarDoDespacho;
            
            if (processoDeMarca.Procurador != null)
            {
                ctrlProcurador.ProcuradorSelecionado = processoDeMarca.Procurador;
                ctrlProcurador.Nome = processoDeMarca.Procurador.Pessoa.Nome;
            }

            txtApostila.Text = processoDeMarca.Apostila;

        }

        private void LimpaTela()
        {
            ViewState[CHAVE_ID] = null;

            var controle = pnlProcessoDeMarca as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            ctrlMarcas1.Inicializa();
            ctrlDespacho.Inicializa();
            ctrlProcurador.Inicializa();
            rblProcessoEhDeTerceiro.Items.Clear();
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Não", "0"));
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Sim", "1"));
            rblProcessoEhDeTerceiro.SelectedValue = "0";
            txtDataDeCadastro.Enabled = false;

            rblEstaAtivo.Items.Clear();
            rblEstaAtivo.Items.Add(new ListItem("Não", "0"));
            rblEstaAtivo.Items.Add(new ListItem("Sim", "1"));
            rblEstaAtivo.SelectedValue = "1";
        }

        private IProcessoDeMarca MontaObjeto()
        {
            var processoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDeMarca>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                processoDeMarca.IdProcessoDeMarca = Convert.ToInt64(ViewState[CHAVE_ID]);
            
            processoDeMarca.Marca = ctrlMarcas1.MarcaSelecionada;
            processoDeMarca.Processo = Convert.ToInt64(txtProcesso.Text);
            processoDeMarca.DataDoCadastro = txtDataDeCadastro.SelectedDate.Value;
            processoDeMarca.DataDoDeposito = txtDataDeDeposito.SelectedDate;
            processoDeMarca.DataDeConcessao = txtDataDeConcessao.SelectedDate;
            processoDeMarca.Ativo = rblEstaAtivo.SelectedValue != "0";
            processoDeMarca.ProcessoEhDeTerceiro = rblProcessoEhDeTerceiro.SelectedValue != "0";
            if (ctrlDespacho.DespachoDeMarcasSelecionada != null) processoDeMarca.Despacho = ctrlDespacho.DespachoDeMarcasSelecionada;
            processoDeMarca.TextoComplementarDoDespacho = txtTextoComplementarDoDespacho.Text;
            if (ctrlProcurador.ProcuradorSelecionado != null)
                processoDeMarca.Procurador = ctrlProcurador.ProcuradorSelecionado;
            processoDeMarca.Apostila = txtApostila.Text;
            
            return processoDeMarca;
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsitencias = new List<string>();

            if (ctrlMarcas1.MarcaSelecionada == null) inconsitencias.Add("É necessário informar uma marca.");

            if (!txtProcesso.Value.HasValue) inconsitencias.Add("É necessário informar número do processo.");
            
            if (rblProcessoEhDeTerceiro.SelectedValue == "1" && ctrlProcurador.ProcuradorSelecionado == null ) inconsitencias.Add("É necessário informar um procurador.");

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

            var processoDeMarca = MontaObjeto();
            string mensagem;

            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(processoDeMarca);
                        mensagem = "Processo de marca cadastrado com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(processoDeMarca);
                        mensagem = "Processo de marca modificada com sucesso.";
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