using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FN.Interfaces.Negocio;

namespace FN.Interfaces.Mapeadores
{
    public interface IMapeadorDeBoleto
    {
        IBoletosGerados obtenhaBoletoPeloId(long idBoleto);
        IBoletosGerados obtenhaBoletoPeloNossoNumero(long numero);

        void Inserir(IBoletosGerados boletoGerado);
        void Excluir(long idBoleto);

        IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto();

        void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto);

        void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto);

        IConfiguracaoDeBoletoBancario ObtenhaConfiguracao();
        void SalveConfiguracao(IConfiguracaoDeBoletoBancario configuracao);

        IList<IBoletosGerados> obtenhaBoletosGerados(int quantidadeDeRegistros, int offSet);

        void AtualizarBoletoGerado(IBoletosGerados boletoGerado);
    }
}
