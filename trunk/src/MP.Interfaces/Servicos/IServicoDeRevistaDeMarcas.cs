using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeRevistaDeMarcas : IServico
    {
        void Inserir(IList<IRevistaDeMarcas> listaDeObjetoRevistaDeMarcas);
        void Modificar(IRevistaDeMarcas revistaDeMarcas);
        IList<IRevistaDeMarcas> ObtenhaRevistasAProcessar(int quantidadeDeRegistros);
        IList<IRevistaDeMarcas> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros);
        IList<IRevistaDeMarcas> ObtenhaProcessosExistentesDeAcordoComARevistaXml(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml);
        IList<ILeituraRevistaDeMarcas> ObtenhaResultadoDaConsultaPorFiltroXML(XmlDocument revistaXml, IFiltroLeituraDeRevistaDeMarcas filtro);
        IList<ILeituraRevistaDeMarcas> ObtenhaObjetoDeLeituraRevistaDeMarcas(
            IList<IRevistaDeMarcas> listaDeProcessosExistentes);

        IList<ILeituraRevistaDeMarcas> obtenhaTodosOsProcessosDaRevistaDeMarcasXML(XmlDocument revistaXml);
        IDictionary<IList<ILeituraRevistaDeMarcas>, IList<ILeituraRevistaDeMarcas>> obtenhaListaDasMarcasColidentesEClientes(IList<ILeituraRevistaDeMarcas> listaDeProcessosDaRevistaComMarcaExistente, IList<IProcessoDeMarca> listaDeProcessosDeMarcasComRadicalCadastrado);
    }
}
