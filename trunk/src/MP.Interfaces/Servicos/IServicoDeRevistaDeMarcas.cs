using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeRevistaDeMarcas : IServico
    {
        void Inserir(IList<IRevistaDeMarcas> listaDeObjetoRevistaDeMarcas);
        void Modificar(IRevistaDeMarcas revistaDeMarcas);
        IList<IRevistaDeMarcas> ObtenhaRevistasAProcessar(int quantidadeDeRegistros);
        IList<IRevistaDeMarcas> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros);
        IList<IRevistaDeMarcas> ObtenhaProcessosExistentesDeAcordoComARevistaXml(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml);
    }
}
