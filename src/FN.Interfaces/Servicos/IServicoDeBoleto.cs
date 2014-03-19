﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
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
        void Inserir(IBoletosGerados boletoGerado);
        void Excluir(long idBoleto);

        IConfiguracaoDeBoletoBancario ObtenhaConfiguracao();
        void SalveConfiguracao(IConfiguracaoDeBoletoBancario configuracao);

        IList<IBoletosGerados> obtenhaBoletosGerados(int quantidadeDeRegistros, int offSet);
    }
}