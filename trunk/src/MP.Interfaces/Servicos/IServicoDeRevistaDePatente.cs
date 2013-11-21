using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeRevistaDePatente : IDisposable
    {
        void Inserir(IList<IRevistaDePatente> listaDeObjetoRevistaDeMarcas);
        void Modificar(IRevistaDePatente revistaDeMarcas);
        IList<IRevistaDePatente> ObtenhaRevistasAProcessar(int quantidadeDeRegistros);
        IList<IRevistaDePatente> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros);
        IList<IRevistaDePatente> ObtenhaProcessosExistentesDeAcordoComARevistaXml(IRevistaDePatente revistaDePatentes, XmlDocument revistaXml);
        //IList<ILeituraRevistaDeMarcas> ObtenhaResultadoDaConsultaPorFiltroXML(XmlDocument revistaXml, IFiltroLeituraDeRevistaDeMarcas filtro);
        //IList<ILeituraRevistaDeMarcas> ObtenhaObjetoDeLeituraRevistaDeMarcas(IList<IRevistaDePatente> listaDeProcessosExistentes);
    }
}
