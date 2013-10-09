using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeTipoDeProcedimentoInterno
    {
        IList<ITipoDeProcedimentoInterno> obtenhaTodosTiposDeProcedimentoInterno();
        ITipoDeProcedimentoInterno obtenhaTipoProcedimentoInternoPeloId(long idTipoProcedimentosInternos);
        ITipoDeProcedimentoInterno obtenhaTipoProcedimentoInternoPelaDescricao(string descricao);
        void Inserir(ITipoDeProcedimentoInterno tipoProcedimentoInterno);
        void Modificar(ITipoDeProcedimentoInterno tipoProcedimentoInterno);
        void Excluir(long idTipoProcedimentoInterno);
    }
}
