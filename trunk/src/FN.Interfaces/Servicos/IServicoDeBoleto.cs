using System.Collections.Generic;
using Compartilhados;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Negocio;
using FN.Interfaces.Negocio;

namespace FN.Interfaces.Servicos
{
    public interface IServicoDeBoleto : IServico
    {
        IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto();
        void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto);
        void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto);
        IBoletosGerados obtenhaBoletoPeloId(long idBoleto);
        IBoletosGerados obtenhaBoletoPeloNossoNumero(long numero);
        void Inserir(IBoletosGerados boletoGerado, bool gerarItemFinanceiro, TipoLacamentoFinanceiroRecebimento tipoLacamento);
        void Excluir(long idBoleto);

        IConfiguracaoDeBoletoBancario ObtenhaConfiguracao();
        void SalveConfiguracao(IConfiguracaoDeBoletoBancario configuracao);

        void AtualizarBoletoGerado(IBoletosGerados boletoGerado);
        IList<IBoletosGerados> obtenhaBoletosGerados(IFiltro filtro, int quantidadeDeRegistros, int offSet);
        int ObtenhaQuantidadeDeBoletos(IFiltro filtro);
        void AtualizarStatusDoBoletoGerado(long numeroDoBoletoGerado, string statusDoBoleto);
    }
}