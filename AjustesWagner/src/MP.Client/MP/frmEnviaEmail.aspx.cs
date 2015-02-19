﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Serialization;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Servicos;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmEnviaEmail : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimpaTela();

                Nullable<long> id = null;
                string tipo = null;
                bool processaTemplateDespacho = false;

                if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                    id = Convert.ToInt64(Request.QueryString["Id"]);

                if (!String.IsNullOrEmpty(Request.QueryString["Tipo"]))
                    tipo = Request.QueryString["Tipo"];

                if (!String.IsNullOrEmpty(Request.QueryString["Despacho"]))
                    processaTemplateDespacho = true;

                if (id == null || tipo == null) return;

                ViewState.Add("TIPO", tipo);

                if (tipo == "P")
                    MostraDadosDoEmailParaPatente(id.Value, processaTemplateDespacho);
                else
                    MostraDadosDoEmailParaMarca(id.Value, processaTemplateDespacho);
            }
        }

        private string ObtenhaContexto()
        {
            if (ViewState["TIPO"].Equals("P"))
                return "PATENTE";

            return "MARCA";
        }

        private IList<string> Destinarios
        {
            get { return (IList<string>)ViewState["DESTINARIOSDEEMAIL"]; }
            set { ViewState["DESTINARIOSDEEMAIL"] = value; }
        }

        private IList<string> DestinariosCC
        {
            get { return (IList<string>)ViewState["DESTINARIOSDEEMAILCC"]; }
            set { ViewState["DESTINARIOSDEEMAILCC"] = value; }
        }

        private IList<string> DestinariosCo
        {
            get { return (IList<string>)ViewState["DESTINARIOSDEEMAILCCO"]; }
            set { ViewState["DESTINARIOSDEEMAILCCO"] = value; }
        }

        private IProcessoDeMarca ProcessoDeMarca
        {
            get { return (IProcessoDeMarca)ViewState["MARCASELECIONADA"]; }
            set { ViewState["MARCASELECIONADA"] = value; }
        }

        private IProcessoDePatente ProcessoDePatente
        {
            get { return (IProcessoDePatente)ViewState["PATENTESELECIONADA"]; }
            set { ViewState["PATENTESELECIONADA"] = value; }
        }

        private void MostraDadosDoEmailParaPatente(long id , bool processaTemplateDespacho)
        {
            pnlEscolhaDeDestinariosDeMarca.Visible = false;
            pnlEscolhaDeDestinatoriosPatente.Visible = true;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
            {
                var processo = servico.Obtenha(id);

                if (processo == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                           UtilidadesWeb.MostraMensagemDeInconsitencia("O processo de patente não existe no Banco de Dados."),
                                                           false);
                    return;
                }

                ProcessoDePatente = processo;

                if (processaTemplateDespacho)
                {

                    if (processo.Despacho == null)
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                    "O processo de patente ainda não possui despacho cadastrado. O corpo do e-mail não será preenchido automaticamente pelo template vinculado ao despacho."),
                                                                false);
                    else
                    {
                        if (processo.Despacho.TemplateDeEmail == null)
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                    UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                        "O despacho do processo de patente não possui template de e-mail cadastrado. O corpo do e-mail não será preenchido automaticamente pelo template vinculado ao despacho."),
                                                                    false);
                        else
                            ctrlTemplateDeEmail.TextoDoTemplate = processo.Despacho.TemplateDeEmail.Template;
                    }
                }

            }
        }

        private void MostraDadosDoEmailParaMarca(long id, bool processaTemplateDespacho)
        {

            pnlEscolhaDeDestinariosDeMarca.Visible = true;
            pnlEscolhaDeDestinatoriosPatente.Visible = false;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
            {
                var processo = servico.Obtenha(id);

                if (processo == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                           UtilidadesWeb.MostraMensagemDeInconsitencia("O processo de marca não existe no Banco de Dados."),
                                                           false);
                    return;
                }

                ProcessoDeMarca = processo;


                if (processaTemplateDespacho)
                {


                    if (processo.Despacho == null)
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                    "O processo de marca ainda não possui despacho cadastrado. O corpo do e-mail não será preenchido automaticamente pelo template vinculado ao despacho."),
                                                                false);
                    else
                    {
                        if (processo.Despacho.TemplateDeEmail == null)
                            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                    UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                        "O despacho do processo de marca não possui template de e-mail cadastrado. O corpo do e-mail não será preenchido automaticamente pelo template vinculado ao despacho."),
                                                                    false);
                        else
                            ctrlTemplateDeEmail.TextoDoTemplate = processo.Despacho.TemplateDeEmail.Template;
                    }
                }
            }
        }

        private void LimpaTela()
        {
            ctrlTemplateDeEmail.Inicializa();
            var controle = pnlDadosDoEmail as Control;
            UtilidadesWeb.LimparComponente(ref controle);

            Destinarios = new List<string>();
            ExibaDestinarios();
            Anexos = new ConcurrentDictionary<string, Stream>();

            DestinariosCC = new List<string>();
            ExibaDestinariosCC();

            DestinariosCo = new List<string>();
            ExibaDestinariosCCo();
            ExibaArquivosAnexados();
        }

        protected void rtbToolBar_ButtonClick(object sender, RadToolBarEventArgs e)
        {
            switch (((RadToolBarButton)e.Item).CommandName)
            {
                case "btnEnviar":
                    btnEnviar_Click();
                    break;
            }
        }

        private void btnEnviar_Click()
        {
            IConfiguracaoDoSistema configuracao = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracoesDoSistema>())
                configuracao = servico.ObtenhaConfiguracaoDoSistema();

            if (configuracao == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                             UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                 "A configuração geral do sistema não foi encontrada."),
                                                             false);
                return;
            }

            var configuracaoDeEmail = configuracao.ConfiguracaoDeEmailDoSistema;

            if (configuracaoDeEmail == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                             UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                 "A configuração de e-mail do sistema não foi encontrada."),
                                                             false);
                return;
            }

            GerenciadorDeEmail.EnviaEmail(txtAssunto.Text,
                                          Destinarios, DestinariosCC, DestinariosCo, ctrlTemplateDeEmail.TextoDoTemplate, Anexos, ObtenhaContexto(), true);

            LimpaTela();

            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                             UtilidadesWeb.MostraMensagemDeInformacao(
                                                                 "E-mail enviado com sucesso."),
                                                             false);


        }

        private IDictionary<string, Stream> Anexos
        {
            get { return (IDictionary<string, Stream>)Session["ANEXOSEMAIL"]; }
            set { Session["ANEXOSEMAIL"] = value; }
        }

        protected void uplAnexos_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            if (uplAnexos.UploadedFiles.Count > 0)
            {
                UploadedFileCollection arquivos = uplAnexos.UploadedFiles;

                if (Anexos == null)
                    Anexos = new ConcurrentDictionary<string, Stream>();

                foreach (UploadedFile arquivo in arquivos)
                    Anexos.Add(arquivo.GetName(), arquivo.InputStream);

                ExibaArquivosAnexados();
            }

        }

        protected override string ObtenhaIdFuncao()
        {
            return "";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return rtbToolBar;
        }

        protected void chkProcuradorMarca_OnCheckedChanged(object sender, EventArgs e)
        {
            var procurador = ProcessoDeMarca.Procurador;

            if (procurador == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "O processo da marca ainda não possui procurador."),
                                                            false);
                return;
            }


            if (procurador.Pessoa.EnderecosDeEmails == null || procurador.Pessoa.EnderecosDeEmails.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "O procurador do processo da marca não possui e-mail cadastrado. Cadastre-o ou informe manualmente."),
                                                            false);
                return;
            }

            foreach (EnderecoDeEmail enderecoDeEmail in procurador.Pessoa.EnderecosDeEmails)
            {
                if (chkProcuradorMarca.Checked)
                {
                    if (VerificaSeEmailFoiAdicionadoNaLista(enderecoDeEmail.ToString())) return;
                    Destinarios.Add(enderecoDeEmail.ToString());
                }
                else
                    Destinarios.Remove(enderecoDeEmail.ToString());
            }

            ExibaDestinarios();
        }

        private bool VerificaSeEmailFoiAdicionadoNaLista(string email)
        {
            if (Destinarios.Contains(email))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "O e-mail informado já está na lista de destinários."),
                                                            false);
                return true;
            }

            return false;
        }

        private void ExibaDestinarios()
        {
            grdDestinatarios.DataSource = Destinarios;
            grdDestinatarios.DataBind();
        }

        private void ExibaArquivosAnexados()
        {
            grdAnexos.DataSource = Anexos.Keys;
            grdAnexos.DataBind();
        }

        private void ExibaDestinariosCCo()
        {
            grdDestinatariosCCo.DataSource = DestinariosCo;
            grdDestinatariosCCo.DataBind();
        }

        private void ExibaDestinariosCC()
        {
            grdDestinariosCC.DataSource = DestinariosCC;
            grdDestinariosCC.DataBind();
        }


        protected void chkClienteMarca_OnCheckedChanged(object sender, EventArgs e)
        {

            var cliente = ProcessoDeMarca.Marca.Cliente;

            if (cliente == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "A marca não possui cliente."),
                                                            false);
                return;
            }


            if (cliente.Pessoa.EnderecosDeEmails == null || cliente.Pessoa.EnderecosDeEmails.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "O cliente da marca não possui e-mail cadastrado. Cadastre-o ou informe manualmente."),
                                                            false);
                return;
            }

            foreach (EnderecoDeEmail enderecoDeEmail in cliente.Pessoa.EnderecosDeEmails)
            {
                if (chkClienteMarca.Checked)
                {
                    if (VerificaSeEmailFoiAdicionadoNaLista(enderecoDeEmail.ToString())) return;
                    Destinarios.Add(enderecoDeEmail.ToString());
                }
                else
                    Destinarios.Remove(enderecoDeEmail.ToString());    
            }

            ExibaDestinarios();

        }

        protected void chkProcuradorPatente_OnCheckedChanged(object sender, EventArgs e)
        {
            var procurador = ProcessoDePatente.Procurador;

            if (procurador == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "O processo da patente ainda não possui procurador."),
                                                            false);
                return;
            }


            if (procurador.Pessoa.EnderecosDeEmails == null || procurador.Pessoa.EnderecosDeEmails.Count == 0   )
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "O procurador do processo da patente não possui e-mail cadastrado. Cadastre-o ou informe manualmente."),
                                                            false);
                return;
            }

            foreach (EnderecoDeEmail enderecoDeEmail in procurador.Pessoa.EnderecosDeEmails)
            {
                if (chkProcuradorPatente.Checked)
                {
                    if (VerificaSeEmailFoiAdicionadoNaLista(enderecoDeEmail.ToString())) return;
                    Destinarios.Add(enderecoDeEmail.ToString());
                }
                else
                    Destinarios.Remove(enderecoDeEmail.ToString());    
            }

            ExibaDestinarios();
        }

        protected void chkClientesPatente_OnCheckedChanged(object sender, EventArgs e)
        {
            var clientes = ProcessoDePatente.Patente.Clientes;

            if (clientes == null || clientes.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "A patente não possui nenhum cliente vinculado."),
                                                            false);
                return;
            }

            var listaDeEmails = new List<string>();

            foreach (var cliente in clientes)
                if (cliente.Pessoa.EnderecosDeEmails != null && cliente.Pessoa.EnderecosDeEmails.Count > 0)
                    listaDeEmails.AddRange(cliente.Pessoa.EnderecosDeEmails.Select(enderecoDeEmail => enderecoDeEmail.ToString()));


            if (listaDeEmails.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "Nenhum dos clientes da patente possue e-mail cadastrado. Cadastre-os ou os informe manualmente."),
                                                            false);
                return;
            }

            foreach (var email in listaDeEmails)
            {
                if (chkClientesPatente.Checked)
                {
                    if (VerificaSeEmailFoiAdicionadoNaLista(email)) return;

                    Destinarios.Add(email);
                }
                else
                    Destinarios.Remove(email);
            }

            ExibaDestinarios();
        }

        protected void chkInventoresPatente_OnCheckedChanged(object sender, EventArgs e)
        {
            var inventores = ProcessoDePatente.Patente.Inventores;

            if (inventores == null || inventores.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "A patente não possui nenhum inventor vinculado."),
                                                            false);
                return;
            }

            var listaDeEmails = new List<string>();

            foreach (var inventor in inventores)
                if (inventor.Pessoa.EnderecosDeEmails != null && inventor.Pessoa.EnderecosDeEmails.Count > 0)
                    listaDeEmails.AddRange(inventor.Pessoa.EnderecosDeEmails.Select(enderecoDeEmail => enderecoDeEmail.ToString()));

            if (listaDeEmails.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "Nenhum dos inventores da patente possue e-mail cadastrado. Cadastre-os ou os informe manualmente."),
                                                            false);
                return;
            }

            foreach (var email in listaDeEmails)
            {
                if (chkInventoresPatente.Checked)
                {
                    if (VerificaSeEmailFoiAdicionadoNaLista(email)) return;

                    Destinarios.Add(email);
                }
                else
                    Destinarios.Remove(email);
            }

            ExibaDestinarios();
        }

        protected void chkTitularesPatente_OnCheckedChanged(object sender, EventArgs e)
        {
            var titulares = ProcessoDePatente.Patente.Titulares;

            if (titulares == null || titulares.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "A patente não possui nenhum titular vinculado."),
                                                            false);
                return;
            }

            var listaDeEmails = new List<string>();

            foreach (var titular in titulares)
                if (titular.Pessoa.EnderecosDeEmails != null && titular.Pessoa.EnderecosDeEmails.Count > 0)
                    listaDeEmails.AddRange(titular.Pessoa.EnderecosDeEmails.Select(enderecoDeEmail => enderecoDeEmail.ToString()));

            if (listaDeEmails.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                            UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                "Nenhum dos titulares da patente possue e-mail cadastrado. Cadastre-os ou os informe manualmente."),
                                                            false);
                return;
            }

            foreach (var email in listaDeEmails)
            {
                if (chkInventoresPatente.Checked)
                {
                    if (VerificaSeEmailFoiAdicionadoNaLista(email)) return;

                    Destinarios.Add(email);
                }
                else
                    Destinarios.Remove(email);
            }

            ExibaDestinarios();
        }

        protected void btnAdicionarDestinatario_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDestinarioManual.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                           UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                               "Informe um e-mail."),
                                                           false);
                return;
            }


            if (Destinarios.Contains(txtDestinarioManual.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                          UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                              "E-mail já informado na lista de destinatários CC."),
                                                          false);
                return;
            }

            Destinarios.Add(txtDestinarioManual.Text);
            ExibaDestinarios();
        }

        protected void grdDestinatarios_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            var indiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                indiceSelecionado = e.Item.ItemIndex;

            switch (e.CommandName)
            {
                case "Excluir":
                    Destinarios.RemoveAt(indiceSelecionado);
                    ExibaDestinarios();
                    break;
            }
        }

        protected void grdDestinatarios_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref grdDestinatarios,Destinarios,e);
        }

        protected void grdAnexos_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            string nomeDoArquivo = null;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
               nomeDoArquivo = e.Item.Cells[3].Text;

            switch (e.CommandName)
            {
                case "Excluir":

                    Anexos.Remove(nomeDoArquivo);
                    ExibaArquivosAnexados();
                    break;
            }
        }

        protected void grdAnexos_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref  grdAnexos, Anexos.Keys, e);
        }

        protected void btnAdicionarDestinarioCCo_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDestinarioCCoManual.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                           UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                               "Informe um e-mail."),
                                                           false);
                return;
            }


            if (DestinariosCo.Contains(txtDestinarioCCoManual.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                          UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                              "E-mail já informado na lista de destinatários CCo."),
                                                          false);
                return;
            }

            DestinariosCo.Add(txtDestinarioCCoManual.Text);
            ExibaDestinariosCCo();
        }

        protected void grdDestinatariosCCo_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            var indiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                indiceSelecionado = e.Item.ItemIndex;

            switch (e.CommandName)
            {
                case "Excluir":
                    DestinariosCo.RemoveAt(indiceSelecionado);
                    ExibaDestinariosCCo();
                    break;
            }
        }

        protected void grdDestinatariosCCo_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref  grdDestinatariosCCo, DestinariosCo, e);
        }

        protected void btnAdicionarDestinarioCC_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailManualCC.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                           UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                               "Informe um e-mail."),
                                                           false);
                return;
            }


            if (DestinariosCo.Contains(txtEmailManualCC.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                          UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                              "E-mail já informado na lista de destinatários CC."),
                                                          false);
                return;
            }

            DestinariosCC.Add(txtEmailManualCC.Text);
            ExibaDestinariosCC();
        }

        protected void grdDestinatariosCC_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            var indiceSelecionado = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                indiceSelecionado = e.Item.ItemIndex;

            switch (e.CommandName)
            {
                case "Excluir":
                    DestinariosCC.RemoveAt(indiceSelecionado);
                    ExibaDestinariosCC();
                    break;
            }
        }

        protected void grdDestinatariosCC_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            UtilidadesWeb.PaginacaoDataGrid(ref  grdDestinariosCC, DestinariosCo, e);
        }
    }
}