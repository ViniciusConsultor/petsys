using System;
using System.Collections.Generic;
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
            marca.ImagemDaMarca = imgImagemMarca.ImageUrl;
            marca.NCL = NCL.ObtenhaPorCodigo(ctrlNCL.Codigo);
            marca.Natureza = NaturezaDeMarca.ObtenhaPorCodigo(Convert.ToInt32(ctrlNatureza.Codigo));

            marca.EspecificacaoDeProdutosEServicos = txtEspecificacao.Text;
            marca.ObservacaoDaMarca = txtObservacao.Text;

            marca.PagaManutencao = rblPagaManutencao.SelectedValue.Equals("1");

            if (!string.IsNullOrEmpty(ctrlPeriodo.Codigo))
                marca.Periodo = ctrlPeriodo.Codigo;

            if (!string.IsNullOrEmpty(rblFormaDeCobranca.SelectedValue))
                marca.FormaDeCobranca = rblFormaDeCobranca.SelectedValue;

            if (!string.IsNullOrEmpty(txtValor.Text))
                marca.ValorDeCobranca = Convert.ToDouble(txtValor.Text);

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
            rblFormaDeCobranca.Items.Add(new ListItem("% Salário mínimo:   ", "S"));
            rblFormaDeCobranca.Items.Add(new ListItem("   Valor em R$:", "R"));
            txtValor.Visible = false;
            lblValor.Visible = false;
        }

        private void MostreMarcas(IMarcas marca)
        {
            ViewState[CHAVE_ID_MARCA] = marca.IdMarca.Value.ToString();

            ctrlApresentacao.Codigo = marca.Apresentacao.Codigo.ToString();
            ctrlCliente.ClienteSelecionado = marca.Cliente;
            ctrlNCL.Codigo = marca.NCL.Codigo.ToString();
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
            {
                imgImagemMarca.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO;
            }
            else
            {
                imgImagemMarca.ImageUrl = UtilidadesWeb.URL_IMAGEM_MARCA + "/" + marca.ImagemDaMarca;
            }

            rblPagaManutencao.SelectedValue = marca.PagaManutencao ? "1" : "0";

            if (marca.PagaManutencao)
            {
                pnlDadosDaManutencao.Visible = true;

                if (!string.IsNullOrEmpty(marca.Periodo))
                    ctrlPeriodo.Codigo = marca.Periodo;

                if (!string.IsNullOrEmpty(marca.FormaDeCobranca))
                {
                    txtValor.Visible = true;

                    rblFormaDeCobranca.SelectedValue = marca.FormaDeCobranca;

                    if (marca.ValorDeCobranca > 0)
                        txtValor.Text = marca.ValorDeCobranca.ToString();
                }
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

            if (string.IsNullOrEmpty(txtNomeDaMarca.Text)) inconsitencias.Add("É necessário informar o nome da marca.");

            if (string.IsNullOrEmpty(ctrlNCL.Codigo)) inconsitencias.Add("É necessário informar a classificação.");
            
            if (string.IsNullOrEmpty(ctrlNatureza.Codigo)) inconsitencias.Add("É necessário informar a natureza.");
            
            if (string.IsNullOrEmpty(ctrlApresentacao.Codigo)) inconsitencias.Add("É necessário informar a apresentação.");
            
            if (ctrlCliente.ClienteSelecionado == null) inconsitencias.Add("É necessário informar o cliente.");
            
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
                    var pastaDeDestino = Server.MapPath(UtilidadesWeb.URL_IMAGEM_MARCA);

                    UtilidadesWeb.CrieDiretorio(pastaDeDestino);

                    var caminhoArquivo = Path.Combine(pastaDeDestino, arquivo.GetNameWithoutExtension() + arquivo.GetExtension());

                    arquivo.SaveAs(caminhoArquivo);

                    UtilidadesWeb.redimensionaImagem(pastaDeDestino, arquivo.GetName(), 200, 200);

                    imgImagemMarca.ImageUrl = string.Concat(UtilidadesWeb.URL_IMAGEM_MARCA, "/", arquivo.GetNameWithoutExtension() + arquivo.GetExtension());
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

                if (ctrlNCLRadical != null && !string.IsNullOrEmpty(ctrlNCLRadical.Codigo))
                    radical.NCL = NCL.ObtenhaPorCodigo(ctrlNCLRadical.Codigo);

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
                txtValor.Text = null;
                ctrlPeriodo.Inicializa();
            }
        }

        protected void rblFormaDeCobranca_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var rblFormaDeCobranca = sender as RadioButtonList;

            if (rblFormaDeCobranca != null && !string.IsNullOrEmpty(rblFormaDeCobranca.SelectedValue))
            {
                lblValor.Visible = true;
                txtValor.Visible = true;
            }
        } 
    }
}