using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeDespachoDeMarcas
    {
        IList<IDespachoDeMarcas> obtenhaTodosDespachoDeMarcas();
        IDespachoDeMarcas obtenhaDespachoDeMarcasPeloId(long idDespachoDeMarcas);
        IList<IDespachoDeMarcas> ObtenhaPorCodigoDoDespachoComoFiltro(string codigo, int quantidadeMaximaDeRegistros);
        void Inserir(IDespachoDeMarcas despachoDeMarcas);
        void Modificar(IDespachoDeMarcas despachoDeMarcas);
        void Excluir(long idDespachoDeMarcas);
    }
}
