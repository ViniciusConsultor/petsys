using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeRevistaDeMarcas
    {
        void InserirDadosRevistaXml(IList<IRevistaDeMarcas> listaDeProcessosExistentesNaRevista);
        void Modificar(IRevistaDeMarcas revistaDeMarcas);
        IList<IRevistaDeMarcas> ObtenhaRevistasAProcessar(int quantidadeDeRegistros);
        IList<IRevistaDeMarcas> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros);
    }
}
