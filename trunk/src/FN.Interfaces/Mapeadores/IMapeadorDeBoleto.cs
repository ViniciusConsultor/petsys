using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using FN.Interfaces.Negocio;

namespace FN.Interfaces.Mapeadores
{
    public interface IMapeadorDeBoleto
    {
        IBoletosGerados obtenhaBoletoPeloId(long idBoleto);
        IBoletosGerados obtenhaBoletoPeloNossoNumero(long numero);

        void Inserir(IBoletosGerados boletoGerado);
        void Excluir(long idBoleto);

        IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto(long idCedente);

        void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto);

        void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto);

        IConfiguracaoDeBoletoBancario ObtenhaConfiguracao();
        void SalveConfiguracao(IConfiguracaoDeBoletoBancario configuracao);

        void AtualizarBoletoGerado(IBoletosGerados boletoGerado);

        int ObtenhaQuantidadeDeBoletos(IFiltro filtro);
        IList<IBoletosGerados> obtenhaBoletosGerados(IFiltro filtro, int quantidadeDeRegistros, int offSet);
        void AtualizarStatusDoBoletoGerado(long numeroDoBoletoGerado, string statusDoBoleto);
    }
}
