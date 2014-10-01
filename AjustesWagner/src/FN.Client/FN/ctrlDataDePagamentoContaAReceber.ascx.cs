using System;
using System.Web.UI;
using Compartilhados.Componentes.Web;

namespace FN.Client.FN
{
    public partial class ctrlDataDePagamentoContaAReceber : System.Web.UI.UserControl
    {
        public delegate void UsuarioPediuParaGravarEventHandler();
        public event UsuarioPediuParaGravarEventHandler UsuarioPediuParaGravar;

        public delegate void UsuarioPediuParaFecharDataDePagamentoEventHandler();
        public event UsuarioPediuParaFecharDataDePagamentoEventHandler UsuarioPediuParaFecharDataDePagamento;

        public delegate void UsuarioPediuParaFecharDataDePagamentoColetivoEventHandler();
        public event UsuarioPediuParaFecharDataDePagamentoColetivoEventHandler UsuarioPediuParaFecharDataDePagamentoColetivo;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnReceber_ButtonClick(object sender, EventArgs e)
        {
            if (DataInformada() == null )
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                                UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                                    "A data do recebimento deve ser informada."), false);
                return;
            }


            UsuarioPediuParaGravar();
        }

        public DateTime? DataInformada()
        {
            return txtDataDeRecebimento.SelectedDate;
        }

        public void LimparDataSelecionada()
        {
            txtDataDeRecebimento.Clear();
        }

        protected void btnFechar_ButtonClick(object sender, EventArgs e)
        {
            if (UsuarioPediuParaFecharDataDePagamento != null)
                UsuarioPediuParaFecharDataDePagamento();

            if (UsuarioPediuParaFecharDataDePagamentoColetivo != null)
            UsuarioPediuParaFecharDataDePagamentoColetivo();
        }
    }
}