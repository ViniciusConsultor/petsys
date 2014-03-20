using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmBoletosGerados : SuperPagina
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ExibaTelaInicial()
        {
            CarregaBoletosGerados(grdBoletosGerados.PageSize, 0);
        }

        private void CarregaBoletosGerados(int quantidadeDeBoletos, int offset)
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
            {
                var listaDeBoletosGerados = servico.obtenhaBoletosGerados(quantidadeDeBoletos, offset);

                if(listaDeBoletosGerados.Count > 0)
                {
                    grdBoletosGerados.VirtualItemCount = listaDeBoletosGerados.Count;
                    grdBoletosGerados.DataSource = ConvertaBoletosGeradosParaDTO(listaDeBoletosGerados);
                    grdBoletosGerados.DataBind();
                }
                
            }
        }

        private IList<DTOBoletosGerados> ConvertaBoletosGeradosParaDTO(IList<IBoletosGerados> listaDeBoletosGerados)
        {
            var listaDeBoletos = new List<DTOBoletosGerados>();

            foreach (var boletosGerado in listaDeBoletosGerados)
            {
                var dto = new DTOBoletosGerados();

                if (boletosGerado.DataGeracao.HasValue)
                dto.DataGeracao = boletosGerado.DataGeracao.Value.ToString("dd/MM/yyyy");

                if(boletosGerado.DataVencimento.HasValue)
                    dto.DataVencimento = boletosGerado.DataVencimento.Value.ToString("dd/MM/yyyy");

                if (boletosGerado.ID.HasValue) 
                    dto.ID = boletosGerado.ID.Value.ToString();

                if(boletosGerado.NossoNumero.HasValue)
                    dto.NossoNumero = boletosGerado.NossoNumero.Value.ToString();

                dto.NumeroBoleto = boletosGerado.NumeroBoleto;

                dto.Valor = boletosGerado.Valor > 0 ? boletosGerado.Valor.ToString() : "0";

                if (boletosGerado.Cliente != null && boletosGerado.Cliente.Pessoa != null)
                    dto.Cliente = boletosGerado.Cliente.Pessoa.Nome;

                dto.Observacao = !string.IsNullOrEmpty(boletosGerado.Observacao) ? boletosGerado.Observacao : null;

                dto.Cedente = boletosGerado.Cedente != null && boletosGerado.Cedente.Pessoa != null ? 
                    boletosGerado.Cedente.Pessoa.Nome : null;

                dto.Instrucoes = !string.IsNullOrEmpty(boletosGerado.Instrucoes) ? boletosGerado.Instrucoes : null;

                listaDeBoletos.Add(dto);
            }

            return listaDeBoletos;
        }

        protected void grdBoletosGerados_OnPageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            if (e.NewPageIndex >= 0)
            {
                var offSet = 0;

                if (e.NewPageIndex > 0)
                    offSet = e.NewPageIndex * grdBoletosGerados.PageSize;

                CarregaBoletosGerados(grdBoletosGerados.PageSize, offSet);

            }
        }

        protected void grdBoletosGerados_OnItemCommand(object sender, GridCommandEventArgs e)
        {
            long id = 0;

            if (e.CommandName != "Page" && e.CommandName != "ChangePageSize")
                id = Convert.ToInt64((e.Item.Cells[4].Text));

            switch (e.CommandName)
            {
                case "Excluir":

                    try
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                        {
                            servico.Excluir(id);
                        }

                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInformacao(
                                                                    "Boleto excluído com sucesso."), false);
                        ExibaTelaInicial();
                    }
                    catch (BussinesException ex)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), false);
                    }

                    break;
                case "Modificar":
                    var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmBoletoAvulso.aspx",
                                            "?Id=", id);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.ExibeJanela(url,
                                                                                       "Reimprimir boleto",
                                                                                       800, 550, "frmBoletoAvulso_aspx"), false);
                    break;
            }
        }

        protected override string ObtenhaIdFuncao()
        {
            return "FUN.FN.005";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}