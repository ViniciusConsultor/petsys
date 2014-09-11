using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeTipoDeProcedimentoInterno : IServico
    {
        ITipoDeProcedimentoInterno obtenhaTipoProcedimentoInternoPeloId(long idTipoProcedimentosInternos);
        IList<ITipoDeProcedimentoInterno> obtenhaTipoProcedimentoInternoPelaDescricao(string descricao);
        void Inserir(ITipoDeProcedimentoInterno tipoProcedimentoInterno);
        void Modificar(ITipoDeProcedimentoInterno tipoProcedimentoInterno);
        void Excluir(long idTipoProcedimentoInterno);
    }
}
