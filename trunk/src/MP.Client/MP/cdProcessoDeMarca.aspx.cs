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
        }

        private void ExibaTelaDetalhes(long id)
        {
            ViewState[CHAVE_ESTADO] = Estado.Modifica;
            LimpaTela();

            IProcessoDeMarca processoDeMarca = null;

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
            {
                processoDeMarca = servico.Obtenha(id);
            }

            if (processoDeMarca != null) MostreProcessoDeMarca(processoDeMarca);

        }


        private void MostreProcessoDeMarca(IProcessoDeMarca processoDeMarca)
        {
            ViewState[CHAVE_ID] = processoDeMarca.IdProcessoDeMarca;
            ctrlMarcas1.MarcaSelecionada = processoDeMarca.Marca;
            ctrlMarcas1.DescricaoDaMarca = processoDeMarca.Marca.DescricaoDaMarca;
            if (processoDeMarca.Protocolo.HasValue) txtProtocolo.Text = processoDeMarca.Protocolo.Value.ToString();
            if (processoDeMarca.Processo.HasValue) txtProcesso.Text = processoDeMarca.Processo.Value.ToString();
            txtDataDeEntrada.SelectedDate = processoDeMarca.DataDeEntrada;
            txtDataDeConcessao.SelectedDate = processoDeMarca.DataDeConcessao;
            rblProcessoEhDeTerceiro.SelectedValue = processoDeMarca.ProcessoEhDeTerceiro ? "1" : "0";
            if (processoDeMarca.Despacho != null) ctrlDespacho.DespachoDeMarcasSelecionada = processoDeMarca.Despacho;
            txtDataRenovacao.SelectedDate = processoDeMarca.DataDeRenovacao;
            
            if (processoDeMarca.Procurador != null)
            {
                ctrlProcurador.ProcuradorSelecionado = processoDeMarca.Procurador;
                ctrlProcurador.Nome = processoDeMarca.Procurador.Pessoa.Nome;
            }

            if (processoDeMarca.SituacaoDoProcesso != null)
                ctrlSituacao.Codigo = processoDeMarca.SituacaoDoProcesso.CodigoSituacaoProcesso.ToString();

        }

        private void LimpaTela()
        {
            ViewState[CHAVE_ID] = null;

            var controle = pnlProcessoDeMarca as Control;
            UtilidadesWeb.LimparComponente(ref controle);
            ctrlMarcas1.Inicializa();
            ctrlDespacho.Inicializa();
            ctrlProcurador.Inicializa();
            ctrlProcurador.RotuloComponente = "Procurador";
        }

        private IProcessoDeMarca MontaObjeto()
        {
            var processoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDeMarca>();

            if (!ViewState[CHAVE_ESTADO].Equals(Estado.Novo))
                processoDeMarca.IdProcessoDeMarca = Convert.ToInt64(ViewState[CHAVE_ID]);
            
            processoDeMarca.Marca = ctrlMarcas1.MarcaSelecionada;

            if (!String.IsNullOrEmpty(txtProtocolo.Text))
                processoDeMarca.Protocolo = Convert.ToInt64(txtProtocolo.Text);

            if (!String.IsNullOrEmpty(txtProcesso.Text))
                processoDeMarca.Processo = Convert.ToInt64(txtProcesso.Text);

            processoDeMarca.DataDeEntrada = txtDataDeEntrada.SelectedDate.Value;
            processoDeMarca.DataDeConcessao = txtDataDeConcessao.SelectedDate;

            processoDeMarca.ProcessoEhDeTerceiro = Convert.ToBoolean(rblProcessoEhDeTerceiro.SelectedValue);

            if (ctrlDespacho.DespachoDeMarcasSelecionada != null) processoDeMarca.Despacho = ctrlDespacho.DespachoDeMarcasSelecionada;

            processoDeMarca.DataDeRenovacao = txtDataRenovacao.SelectedDate;

            if (ctrlProcurador.ProcuradorSelecionado != null)
                processoDeMarca.Procurador = ctrlProcurador.ProcuradorSelecionado;
            
            if (!string.IsNullOrEmpty(ctrlSituacao.Codigo))
                processoDeMarca.SituacaoDoProcesso = SituacaoDoProcesso.ObtenhaPorCodigo(Convert.ToInt32(ctrlSituacao.Codigo));

            return processoDeMarca;
        }

        private IList<string> VerifiqueCamposObrigatorios()
        {
            var inconsitencias = new List<string>();

            if (ctrlMarcas1.MarcaSelecionada == null) inconsitencias.Add("É necessário informar uma marca.");

            if (ctrlProcurador.ProcuradorSelecionado == null ) inconsitencias.Add("É necessário informar um procurador.");

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