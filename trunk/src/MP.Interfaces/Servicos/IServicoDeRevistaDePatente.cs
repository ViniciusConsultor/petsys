using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeRevistaDePatente : IDisposable
    {
        void Inserir(IList<IRevistaDePatente> listaDeObjetoRevistaDeMarcas);
        IList<IRevistaDePatente> ObtenhaRevistasAProcessar(int quantidadeDeRegistros);
        IList<IRevistaDePatente> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros);
        IList<IRevistaDePatente> ObtenhaProcessosExistentesDeAcordoComARevistaXml(IRevistaDePatente revistaDePatentes, XmlDocument revistaXml);
        IList<IRevistaDePatente> ObtenhaTodosOsProcessosDaRevistaXML(XmlDocument revistaXml, IFiltroLeituraDeRevistaDePatentes filtro);
    }
}
