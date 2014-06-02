using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    public partial class cdProcessoDeMarca : SuperPagina
    {
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_PROCESSO_DE_MARCA";
        private const string CHAVE_ID_PROCESSO_DE_MARCA = "CHAVE_ID_PROCESSO_DE_MARCA";
        private const string CHAVE_RADICAIS = "CHAVE_RADICAIS";
        private const string CHAVE_ID_MARCA = "CHAVE_ID_MARCA";

        private enum Estado : byte
        {
            Novo,
            Modifica,
        }

        private void MostreDespacho(IDespachoDeMarcas despacho)
        {
            ctrlDespacho.DespachoDeMarcasSelecionada = despacho;
            ctrlDespacho.CodigoDespacho = despacho.CodigoDespacho;

            txtProvidencia.Text = despacho.Providencia;
            txtPrazoParaProvidencia.Text = despacho.PrazoParaProvidenciaEmDias.ToString();
            txtSituacaoDoProcesso.Text = despacho.SituacaoProcesso;
            txtDescricaoDoDespacho.Text = despacho.DescricaoDespacho;
        }

        private IMarcas MontaObjetoMarca()
        {
            var marca = FabricaGenerica.GetInstancia().CrieObjeto<IMarcas>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                marca.IdMarca = Convert.ToInt64(ViewState[CHAVE_ID_MARCA]);

            marca.Apresentacao = Apresentacao.ObtenhaPorCodigo(Convert.ToInt32(ctrlApresentacao.Codigo));

            marca.Cliente = ctrlCliente.ClienteSelecionado;

            if (txtClasse.Value.HasValue)
                marca.CodigoDaClasse = Convert.ToInt32(txtClasse.Text);

            if (txtSubClasse1.Value.HasValue)
                marca.CodigoDaSubClasse1 = Convert.ToInt32(txtSubClasse1.Text);

            if (txtSubClasse2.Value.HasValue)
                marca.CodigoDaSubClasse2 = Convert.ToInt32(txtSubClasse2.Text);

            if (txtSubClasse3.Value.HasValue)
                marca.CodigoDaSubClasse3 = Convert.ToInt32(txtSubClasse3.Text);

            marca.DescricaoDaMarca = txtNomeDaMarca.Text;

            if (!string.IsNullOrEmpty(imgImagemMarca.ImageUrl))
                marca.ImagemDaMarca = imgImagemMarca.ImageUrl;
            else
                marca.ImagemDaMarca = Util.URL_IMAGEM_SEM_FOTO_MARCA;

            marca.NCL = ctrlNCL.NCLSelecionado;
            marca.Natureza = NaturezaDeMarca.ObtenhaPorCodigo(Convert.ToInt32(ctrlNatureza.Codigo));

            marca.EspecificacaoDeProdutosEServicos = txtEspecificacao.Text;
            marca.ObservacaoDaMarca = txtObservacao.Text;

            if (rblPagaManutencao.SelectedValue.Equals("1"))
            {
                var manutencao = FabricaGenerica.GetInstancia().CrieObjeto<IManutencao>();

                manutencao.DataDaProximaManutencao = txtDataDaPrimeiraManutencao.SelectedDate;
                manutencao.FormaDeCobranca = FormaCobrancaManutencao.ObtenhaPorCodigo(rblFormaDeCobranca.SelectedValue);
                manutencao.Periodo = ctrlPeriodo.PeriodoSelecionado;
                manutencao.ValorDeCobranca = txtValor.Value.Value;
                marca.Manutencao = manutencao;
            }

            marca.AdicioneRadicaisMarcas((IList<IRadicalMarcas>)ViewState[CHAVE_RADICAIS]);

            return marca;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlDespacho.DespachoDeMarcasFoiSelecionada += MostreDespacho;
            ctrlDespacho.BotaoNovoEhVisivel = true;
            ctrlProcurador.BotaoNovoEhVisivel = true;
            ctrlCliente.BotaoNovoEhVisivel = true;

            
            if (IsPostBack) return;

            Nullable<long> id = null;

            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                id = Convert.ToInt64(Request.QueryString["Id"]);

            if (id == null)
                ExibaTelaNovo();
            else
                ExibaTelaDetalhes(id.Value);
        }

        private void CarregueComponentes()
        {
            rblPagaManutencao.Items.Clear();
            rblPagaManutencao.Items.Add(new ListItem("  Sim  ", "1"));
            rblPagaManutencao.Items.Add(new ListItem("  Não", "0"));
            rblPagaManutencao.SelectedValue = "0";

            pnlDadosDaManutencao.Visible = false;

            rblFormaDeCobranca.Items.Clear();

            foreach (var formaDeCobranca in FormaCobrancaManutencao.ObtenhaTodas())
                rblFormaDeCobranca.Items.Add(new ListItem(formaDeCobranca.Descricao, formaDeCobranca.Codigo));

            rblFormaDeCobranca.SelectedValue = FormaCobrancaManutencao.ValorFixo.Codigo;
            FormataValorManutencao(FormaCobrancaManutencao.ValorFixo);
            txtDataDaPrimeiraManutencao.Clear();
        }

        private void MostreMarcas(IMarcas marca)
        {
            ViewState[CHAVE_ID_MARCA] = marca.IdMarca.Value.ToString();

            ctrlApresentacao.Codigo = marca.Apresentacao.Codigo.ToString();
            ctrlCliente.ClienteSelecionado = marca.Cliente;
            ctrlNCL.Codigo = marca.NCL.Codigo;
            ctrlNCL.NCLSelecionado = marca.NCL;
            ctrlNatureza.Codigo = marca.Natureza.Codigo.ToString();
            txtNomeDaMarca.Text = marca.DescricaoDaMarca;

            if (marca.CodigoDaClasse.HasValue)
                txtClasse.Text = marca.CodigoDaClasse.ToString();

            if (marca.CodigoDaSubClasse1.HasValue)
                txtSubClasse1.Text = marca.CodigoDaSubClasse1.ToString();

            if (marca.CodigoDaSubClasse2.HasValue)
                txtSubClasse2.Text = marca.CodigoDaSubClasse2.ToString();

            if (marca.CodigoDaSubClasse3.HasValue)
                txtSubClasse3.Text = marca.CodigoDaSubClasse3.ToString();

            txtEspecificacao.Text = marca.EspecificacaoDeProdutosEServicos;
            txtObservacao.Text = marca.ObservacaoDaMarca;

            if (string.IsNullOrEmpty(marca.ImagemDaMarca))
                imgImagemMarca.ImageUrl = Util.URL_IMAGEM_SEM_FOTO_MARCA;
            else
                imgImagemMarca.ImageUrl = marca.ImagemDaMarca;

            rblPagaManutencao.SelectedValue = marca.Manutencao != null ? "1" : "0";

            if (marca.Manutencao != null)
            {
                pnlDadosDaManutencao.Visible = true;

                txtDataDaPrimeiraManutencao.SelectedDate = marca.Manutencao.DataDaProximaManutencao;
                ctrlPeriodo.Codigo = marca.Manutencao.Periodo.Codigo.ToString();
                ctrlPeriodo.PeriodoSelecionado = marca.Manutencao.Periodo;
                rblFormaDeCobranca.SelectedValue = marca.Manutencao.FormaDeCobranca.Codigo;
                txtValor.Value = marca.Manutencao.ValorDeCobranca;
                FormataValorManutencao(marca.Manutencao.FormaDeCobranca);
            }

            IList<IRadicalMarcas> listaDeRadicalMarcas = new List<IRadicalMarcas>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRadicalMarcas>())
                listaDeRadicalMarcas = servico.obtenhaRadicalMarcasPeloIdDaMarcaComoFiltro(marca.IdMarca.Value,
                                                                                           int.MaxValue);

            if (listaDeRadicalMarcas.Count > 0)
                MostraRadicalMarcas(listaDeRadicalMarcas);
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

            txtDataDeDeposito.Enabled = FabricaDeContexto.GetInstancia().GetContextoAtual().EstaAutorizado("OPE.MP.007.0004");


            IProcessoDeMarca processoDeMarca = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                processoDeMarca = servico.Obtenha(id);

            if (processoDeMarca != null)
            {
                if (processoDeMarca.Marca != null && processoDeMarca.Marca.Manutencao != null)
                {
                    if(processoDeMarca.Marca.Manutencao.ManutencaoEstaVencida())
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                       UtilidadesWeb.MostraMensagemDeInformacao("Processo possui manutenção vencida."), false);
                }

                MostreProcessoDeMarca(processoDeMarca);
            }
        }

        private void ExibaTelaModificar()
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
        }

        private void MostreProcessoDeMarca(IProcessoDeMarca processoDeMarca)
        {
            MostreMarcas(processoDeMarca.Marca);
            ViewState[CHAVE_ID_PROCESSO_DE_MARCA] = processoDeMarca.IdProcessoDeMarca;
            txtProcesso.Text = processoDeMarca.Processo.ToString();
            rblEstaAtivo.SelectedValue = processoDeMarca.Ativo ? "1" : "0";
            txtDataDeCadastro.SelectedDate = processoDeMarca.DataDoCadastro;
            txtDataDeDeposito.SelectedDate = processoDeMarca.DataDoDeposito;
            txtDataDeConcessao.SelectedDate = processoDeMarca.DataDeConcessao;
            txtDataDeVigencia.SelectedDate = processoDeMarca.DataDaVigencia;
            rblProcessoEhDeTerceiro.SelectedValue = processoDeMarca.ProcessoEhDeTerceiro ? "1" : "0";

            if (processoDeMarca.Despacho != null)
                MostreDespacho(processoDeMarca.Despacho);

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
            ViewState[CHAVE_ID_PROCESSO_DE_MARCA] = null;
            ViewState[CHAVE_ID_MARCA] = null;

            var controle = pnlDadosDaMarca as Control;
            UtilidadesWeb.LimparComponente(ref controle);

            var controle1 = pnlClassificacao as Control;
            UtilidadesWeb.LimparComponente(ref controle1);

            var controle2 = pnlComplemento as Control;
            UtilidadesWeb.LimparComponente(ref controle2);

            var controle3 = pnlManutencao as Control;
            UtilidadesWeb.LimparComponente(ref controle3);

            var controle4 = pnlRadicais as Control;
            UtilidadesWeb.LimparComponente(ref controle4);

            ctrlDespacho.Inicializa();
            ctrlProcurador.Inicializa();
            rblProcessoEhDeTerceiro.Items.Clear();
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Não", "0"));
            rblProcessoEhDeTerceiro.Items.Add(new ListItem("Sim", "1"));
            rblProcessoEhDeTerceiro.SelectedValue = "0";
            txtDataDeCadastro.Enabled = false;
            txtDataDeDeposito.Enabled = true;

            rblEstaAtivo.Items.Clear();
            rblEstaAtivo.Items.Add(new ListItem("Não", "0"));
            rblEstaAtivo.Items.Add(new ListItem("Sim", "1"));
            rblEstaAtivo.SelectedValue = "1";

            ctrlApresentacao.Inicializa();
            ctrlNCL.Inicializa();
            ctrlNatureza.Inicializa();
            ctrlCliente.Inicializa();
            ctrlNCLRadical.Inicializa();
            ctrlPeriodo.Inicializa();
            
            MostraRadicalMarcas(new List<IRadicalMarcas>());
            CarregueComponentes();

            this.RadTabStrip1.Tabs[0].Selected = true;
            this.RadPageView1.Selected = true;

            imgImagemMarca.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO;
        }

        private IProcessoDeMarca MontaObjeto()
        {
            var processoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDeMarca>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                processoDeMarca.IdProcessoDeMarca = Convert.ToInt64(ViewState[CHAVE_ID_PROCESSO_DE_MARCA]);

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

            processoDeMarca.Marca = MontaObjetoMarca();

            return processoDeMarca;
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsitencias = new List<string>();

            if (ctrlNCL.NCLSelecionado == null) inconsitencias.Add("É necessário informar a classificação.");

            if (string.IsNullOrEmpty(ctrlNatureza.Codigo)) inconsitencias.Add("É necessário informar a natureza.");

            if (string.IsNullOrEmpty(ctrlApresentacao.Codigo)) inconsitencias.Add("É necessário informar a apresentação.");

            if (ctrlCliente.ClienteSelecionado == null) inconsitencias.Add("É necessário informar o cliente.");

            if (!txtProcesso.Value.HasValue) inconsitencias.Add("É necessário informar número do processo.");

            if (rblProcessoEhDeTerceiro.SelectedValue == "1" && ctrlProcurador.ProcuradorSelecionado == null) inconsitencias.Add("É necessário informar um procurador.");

            //Verifica se a apresentação é figurativa. Se for não é necessário validar se foi informado a descrição da marca
            if (!string.IsNullOrEmpty(ctrlApresentacao.Codigo))
            {
                var apresentacao = Apresentacao.ObtenhaPorCodigo(Convert.ToInt32(ctrlApresentacao.Codigo));

                if (!apresentacao.Equals(Apresentacao.Figurativa))
                    if (string.IsNullOrEmpty(txtNomeDaMarca.Text)) inconsitencias.Add("É necessário informar o nome da marca.");
            }

           
            if (rblPagaManutencao.SelectedValue == "1")
            {
                if (!txtDataDaPrimeiraManutencao.SelectedDate.HasValue)
                    inconsitencias.Add("É necessário informar a data da primeira manutenção.");

                if (string.IsNullOrEmpty(rblFormaDeCobranca.SelectedValue))
                    inconsitencias.Add("É necessário informar a forma de cobrança.");

                if (ctrlPeriodo.PeriodoSelecionado == null)
                    inconsitencias.Add("É necessário informar o período de cobrança.");

                if (!txtValor.Value.HasValue)
                    inconsitencias.Add("É necessário informar o valor de cobrança.");
            }

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
                        mensagem = "Processo de marca modificado com sucesso.";
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

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.007";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }

        protected void uplImagem_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                if (uplImagem.UploadedFiles.Count > 0)
                {
                    var arquivo = uplImagem.UploadedFiles[0];
                    var pastaDeDestino = Server.MapPath(Util.URL_IMAGEM_MARCA);

                    UtilidadesWeb.CrieDiretorio(pastaDeDestino);

                    var caminhoArquivo = Path.Combine(pastaDeDestino, arquivo.GetNameWithoutExtension() + arquivo.GetExtension());

                    arquivo.SaveAs(caminhoArquivo);
                    imgImagemMarca.ImageUrl = string.Concat(Util.URL_IMAGEM_MARCA, "/", arquivo.GetNameWithoutExtension() + arquivo.GetExtension());
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar imagem, exceção: ", ex);
            }
        }

        protected void btnRadical_ButtonClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRadical.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Campo radical com preenchimento obrigatório."), false);
            }
            else
            {
                IList<IRadicalMarcas> listaDeRadicais = new List<IRadicalMarcas>();

                listaDeRadicais = (IList<IRadicalMarcas>)ViewState[CHAVE_RADICAIS];

                var radical = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalMarcas>();

                radical.DescricaoRadical = txtRadical.Text;

                if (ctrlNCLRadical.NCLSelecionado != null)
                    radical.NCL = ctrlNCLRadical.NCLSelecionado;

                if (listaDeRadicais.Count > 0)
                {
                    if (!listaDeRadicais.Contains(radical))
                    {
                        listaDeRadicais.Add(radical);
                        MostraRadicalMarcas(listaDeRadicais);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Radical já adicionado."), false);
                    }
                }
                else
                {
                    listaDeRadicais.Add(radical);
                    MostraRadicalMarcas(listaDeRadicais);
                }
            }
        }

        protected void grdRadicais_ItemCommand(object sender, GridCommandEventArgs e)
        {
            var IndiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                IndiceSelecionado = e.Item.ItemIndex;


            if (e.CommandName == "Excluir")
            {
                IList<IRadicalMarcas> listaRadicalMarcas = null;
                listaRadicalMarcas = (IList<IRadicalMarcas>)ViewState[CHAVE_RADICAIS];
                listaRadicalMarcas.RemoveAt(IndiceSelecionado);
                MostraRadicalMarcas(listaRadicalMarcas);
            }
        }

        private void MostraRadicalMarcas(IList<IRadicalMarcas> listaRadicalMarcas)
        {
            grdRadicais.MasterTableView.DataSource = listaRadicalMarcas;
            grdRadicais.DataBind();
            ViewState.Add(CHAVE_RADICAIS, listaRadicalMarcas);
        }

        protected void grdRadicais_ItemCreated(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                var gridItem = (GridDataItem)e.Item;

                foreach (GridColumn column in grdRadicais.MasterTableView.RenderColumns)
                    if ((column is GridButtonColumn))
                        gridItem[column.UniqueName].ToolTip = column.HeaderTooltip;
            }
        }

        protected void grdRadicais_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdRadicais, ViewState[CHAVE_RADICAIS], e);
        }

        protected void rblPagaManutencao_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var rblManutencao = sender as RadioButtonList;

            if (rblManutencao != null && rblManutencao.SelectedValue == "1")
                pnlDadosDaManutencao.Visible = true;
            else
            {
                pnlDadosDaManutencao.Visible = false;
                rblFormaDeCobranca.ClearSelection();
                txtValor.Text = "";
                txtValor.Value = null;
                ctrlPeriodo.Inicializa();
                txtDataDaPrimeiraManutencao.Clear();
            }
        }

        protected void rblFormaDeCobranca_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            FormataValorManutencao(FormaCobrancaManutencao.ObtenhaPorCodigo(rblFormaDeCobranca.SelectedValue));
        }   

        private void FormataValorManutencao(FormaCobrancaManutencao formaCobranca)
        {
            if (formaCobranca.Equals(FormaCobrancaManutencao.ValorFixo))
            {
                txtValor.Type = NumericType.Currency;
                return;
            }


            txtValor.Type = NumericType.Percent;
        }
    }
}