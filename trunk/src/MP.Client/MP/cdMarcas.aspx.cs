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
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class cdMarcas : SuperPagina
    {
        private const string ID_OBJETO = "ID_OBJETO_CD_MARCAS";
        private const string CHAVE_ESTADO = "CHAVE_ESTADO_CD_MARCAS";
        private const string CHAVE_RADICAIS = "CHAVE_RADICAIS";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlMarcas.MarcaFoiSelecionada += MostreMarcas;

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

            var controlePanelMarca = this.pnlDadosDaMarca as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelMarca, true);

            UtilidadesWeb.LimparComponente(ref controlePanelMarca);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelMarca, false);

            var controlePanelComplemento = this.pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, true);

            UtilidadesWeb.LimparComponente(ref controlePanelComplemento);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, false);

            var controleGrid = this.grdRadicais as Control;
            UtilidadesWeb.LimparComponente(ref controleGrid);

            ctrlMarcas.Inicializa();
            ctrlMarcas.EnableLoadOnDemand = true;
            ctrlMarcas.ShowDropDownOnTextboxClick = true;
            ctrlMarcas.AutoPostBack = true;
            ViewState[CHAVE_ESTADO] = Estado.Inicial;
            ViewState[ID_OBJETO] = null;

            ctrlApresentacao.Inicializa();
            ctrlNCL.Inicializa();
            ctrlNatureza.Inicializa();
            ctrlCliente.Inicializa();
            ctrlNCLRadical.Inicializa();

            this.RadTabStrip1.Tabs[0].Selected = true;
            this.RadPageView1.Selected = true;

            MostraRadicalMarcas(new List<IRadicalMarcas>());

            imgImagemMarca.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO;

            ctrlCliente.BotaoNovoEhVisivel = true;
            ctrlMarcas.BotaoNovoEhVisivel = false;
        }

        protected void btnNovo_Click()
        {
            ExibaTelaNovo();
        }

        private void ExibaTelaNovo()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            var controlePanel = this.pnlDadosDaMarca as Control;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);

            var controlePanelComplemento = this.pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, true);

            ViewState[CHAVE_ESTADO] = Estado.Novo;

            ctrlMarcas.Inicializa();
            ctrlMarcas.EnableLoadOnDemand = false;
            ctrlMarcas.ShowDropDownOnTextboxClick = false;
            ctrlMarcas.AutoPostBack = false;
            ctrlMarcas.TextoItemVazio = string.Empty;

            imgImagemMarca.ImageUrl = UtilidadesWeb.URL_IMAGEM_SEM_FOTO;

            MostraRadicalMarcas(new List<IRadicalMarcas>());
        }

        private void ExibaTelaModificar()
        {
            grdRadicais.Columns[0].Display = true;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            var controlePanel = this.pnlDadosDaMarca as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, true);

            var controlePanelComplemento = this.pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, true);

            ViewState[CHAVE_ESTADO] = Estado.Modifica;
            ctrlMarcas.EnableLoadOnDemand = false;
            ctrlMarcas.ShowDropDownOnTextboxClick = false;
            ctrlMarcas.AutoPostBack = false;
            
        }

        private void ExibaTelaExcluir()
        {
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = true;

            ViewState[CHAVE_ESTADO] = Estado.Remove;

            var controlePanel = this.pnlDadosDaMarca as Control;

            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);
            UtilidadesWeb.HabilitaComponentes(ref controlePanel, false);

            var controlePanelComplemento = this.pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, false);
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, false);
        }

        protected void btnCancela_Click()
        {
            ExibaTelaInicial();
        }

        private IMarcas MontaObjetoMarca()
        {
            var marca = FabricaGenerica.GetInstancia().CrieObjeto<IMarcas>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
            {
                marca.IdMarca = Convert.ToInt64(ViewState[ID_OBJETO]);
            }

            marca.Apresentacao = Apresentacao.ObtenhaPorCodigo(Convert.ToInt32(ctrlApresentacao.Codigo));

            marca.Cliente = ctrlCliente.ClienteSelecionado;

             if (txtClasse.Value.HasValue)
                marca.CodigoDaClasse =   Convert.ToInt32(txtClasse.Text);

             if (txtSubClasse1.Value.HasValue)
                 marca.CodigoDaSubClasse1 = Convert.ToInt32(txtSubClasse1.Text);
            
            if (txtSubClasse2.Value.HasValue)
                 marca.CodigoDaSubClasse2 = Convert.ToInt32(txtSubClasse2.Text);

            if (txtSubClasse3.Value.HasValue)
                marca.CodigoDaSubClasse3 = Convert.ToInt32(txtSubClasse3.Text);

            marca.DescricaoDaMarca = ctrlMarcas.DescricaoDaMarca;
            marca.ImagemDaMarca = imgImagemMarca.ImageUrl;
            marca.NCL = NCL.ObtenhaPorCodigo(ctrlNCL.Codigo);
            marca.Natureza = NaturezaDeMarca.ObtenhaPorCodigo(Convert.ToInt32(ctrlNatureza.Codigo));

            marca.EspecificacaoDeProdutosEServicos = txtEspecificacao.Text;
            marca.ObservacaoDaMarca = txtObservacao.Text;

            marca.AdicioneRadicaisMarcas((IList<IRadicalMarcas>) ViewState[CHAVE_RADICAIS]);

            return marca;
        }

        private string validaErrosDePreenchimento()
        {
            string mensagem = string.Empty;

            if (string.IsNullOrEmpty(ctrlNCL.Codigo))
            {
                mensagem = mensagem + "Classificação de NCL, ";
            }
            if (string.IsNullOrEmpty(ctrlNatureza.Codigo))
            {
                mensagem = mensagem + "Natureza, ";
            }
            if (string.IsNullOrEmpty(ctrlApresentacao.Codigo))
            {
                mensagem = mensagem + "Apresentação , ";
            }
            if (ctrlCliente.ClienteSelecionado == null)
            {
                mensagem = mensagem + "Cliente , ";
            }

            return mensagem;
        }

        private void btnSalva_Click()
        {
            string mensagem;

            try
            {
                if (!string.IsNullOrEmpty(validaErrosDePreenchimento()))
                {
                    var erros = validaErrosDePreenchimento();

                    var mensagemDeErro = "Campo(s) " + erros + "precisa(m) ser preenchido(s)";

                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(mensagemDeErro), false);

                    return;
                }

                var marca = MontaObjetoMarca();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeMarcas>())
                {
                    if (ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                    {
                        servico.Inserir(marca);
                        mensagem = "Marca cadastrada com sucesso.";
                    }
                    else
                    {
                        servico.Modificar(marca);
                        mensagem = "Marca modificada com sucesso.";
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(mensagem), false);
                ExibaTelaInicial();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
            }
        }

        private void btnModificar_Click()
        {
            ExibaTelaModificar();
        }

        private void btnExclui_Click()
        {
            ExibaTelaExcluir();
        }

        private void btnNao_Click()
        {
            ExibaTelaInicial();
        }

        private void btnSim_Click()
        {
            try
            {
                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeMarcas>())
                {
                    servico.Excluir(Convert.ToInt64(ViewState[ID_OBJETO]));
                }

                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao(
                                                            "Marca excluída com sucesso."), false);
                ExibaTelaInicial();
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
                case "btnNovo":
                    btnNovo_Click();
                    break;
                case "btnModificar":
                    btnModificar_Click();
                    break;
                case "btnExcluir":
                    btnExclui_Click();
                    break;
                case "btnSalvar":
                    btnSalva_Click();
                    break;
                case "btnCancelar":
                    btnCancela_Click();
                    break;
                case "btnSim":
                    btnSim_Click();
                    break;
                case "btnNao":
                    btnNao_Click();
                    break;
            }
        }

        private void ExibaTelaConsultar()
        {
            grdRadicais.Columns[0].Display = true;

            var controle = pnlDadosDaMarca as Control;

            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNovo")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnModificar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnExcluir")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSalvar")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnCancelar")).Visible = true;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnSim")).Visible = false;
            ((RadToolBarButton)rtbToolBar.FindButtonByCommandName("btnNao")).Visible = false;

            UtilidadesWeb.HabilitaComponentes(ref controle, false);

            var controlePanelComplemento = this.pnlComplemento as Control;
            UtilidadesWeb.HabilitaComponentes(ref controlePanelComplemento, false);
        }

        private void MostreMarcas(IMarcas marca)
        {
            ViewState[ID_OBJETO] = marca.IdMarca.Value.ToString();

            ctrlApresentacao.Codigo = marca.Apresentacao.Codigo.ToString();
            ctrlCliente.ClienteSelecionado = marca.Cliente;
            ctrlNCL.Codigo = marca.NCL.Codigo.ToString();
            ctrlNatureza.Codigo = marca.Natureza.Codigo.ToString();


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

            imgImagemMarca.ImageUrl = marca.ImagemDaMarca;

            IList<IRadicalMarcas> listaDeRadicalMarcas = new List<IRadicalMarcas>();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeRadicalMarcas>())
            {
                listaDeRadicalMarcas = servico.obtenhaRadicalMarcasPeloIdDaMarcaComoFiltro(marca.IdMarca.Value,
                                                                                           int.MaxValue);
            }

            if (listaDeRadicalMarcas.Count > 0)
            {
                MostraRadicalMarcas(listaDeRadicalMarcas);
            }

            ExibaTelaConsultar();
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.008";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        private enum Estado : byte
        {
            Inicial = 1,
            Novo,
            Modifica,
            Remove
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

                listaDeRadicais = (IList<IRadicalMarcas>) ViewState[CHAVE_RADICAIS];
                
                var radical = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalMarcas>();

                radical.DescricaoRadical = txtRadical.Text;

                if (ctrlNCLRadical != null && !string.IsNullOrEmpty(ctrlNCLRadical.Codigo))
                {
                    radical.NCL = NCL.ObtenhaPorCodigo(ctrlNCLRadical.Codigo);
                }

                if(listaDeRadicais.Count > 0)
                {
                    if(!listaDeRadicais.Contains(radical))
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
                listaRadicalMarcas = (IList<IRadicalMarcas>) ViewState[CHAVE_RADICAIS];
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


       
    }
}