using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeProcedimentosInternos
    {
        IList<IProcedimentosInternos> obtenhaTodosProcedimentosInternos();
        IProcedimentosInternos obtenhaProcedimentosInternosPeloId(long idProcedimentosInternos);
        IProcedimentosInternos obtenhaProcedimentosInternosPelaDescricao(string descricaoProcedimentosInternos);
        void Inserir(IProcedimentosInternos procedimentosInternos);
        void Modificar(IProcedimentosInternos procedimentosInternos);
        void Excluir(long idProcedimentosInternos);
    }
}
