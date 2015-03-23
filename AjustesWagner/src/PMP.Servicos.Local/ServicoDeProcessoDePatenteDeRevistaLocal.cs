// -----------------------------------------------------------------------
// <copyright file="ServicoDeProcessoDePatenteDeRevistaLocal.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.IO;
using System.Xml.Linq;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Utilidades;
using PMP.Interfaces.Mapeadores;
using PMP.Interfaces.Servicos;
using PMP.Interfaces.Utilidades;

namespace PMP.Servicos.Local
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ServicoDeProcessoDePatenteDeRevistaLocal : Servico, IServicoDeProcessoDePatenteDeRevista
    {
        public ServicoDeProcessoDePatenteDeRevistaLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        private void DescompacteArquivos(string pastaDeArmazenamentoDasRevistas, string pastaDeDestino)
        {
            var informacoesDoDiretorio = new DirectoryInfo(pastaDeArmazenamentoDasRevistas);
            var arquivosCompactados = informacoesDoDiretorio.GetFiles();

            foreach (var arquivoCompactado in arquivosCompactados)
                Util.DescompacteArquivoZip(arquivoCompactado.FullName, pastaDeDestino);

        }

        private string TransformeTXTParaXml(FileInfo arquivoTxt)
        {
            var numeroDaRevista = arquivoTxt.Name.Substring(2, 4);
            var dataRevista = arquivoTxt.LastWriteTime;

            using (var arquivo = new StreamReader(arquivoTxt.FullName))
            {
                TradutorDeRevistaPatenteTXTParaRevistaPatenteXML.TraduzaRevistaDePatente(dataRevista, numeroDaRevista, arquivo, arquivoTxt.Directory.FullName + "\\");
                arquivo.Close();
            }

            return Path.Combine(arquivoTxt.Directory.FullName, numeroDaRevista + ".xml");
        }

        private KeyValuePair<int, IList<DTOProcessoPatenteRevista>> ExtraiaProcessoDePatenteDeRevistaDoArquivo(FileInfo arquivo)
        {
            //Verificar se arquivo é um txt, se for converter primeiro
            var arquivoDaRevista = XDocument.Load(arquivo.Extension.ToUpper().Equals(".TXT") ? TransformeTXTParaXml(arquivo) : arquivo.FullName);
            var conteudoDaRevista = arquivoDaRevista.Element("revista");
            var numeroDaRevista = int.Parse(conteudoDaRevista.Attribute("numero").Value);
            var dataDaPublicacaoDaRevista = DateTime.Parse(conteudoDaRevista.Attribute("data").Value);

            var listaDeProcessosNaRevista = (from conteudoProcesso in conteudoDaRevista.Elements("processo")
                                             select new DTOProcessoPatenteRevista()
                                             {
                                                 ID = Guid.NewGuid().ToString(),
                                                 NumeroDaRevista = numeroDaRevista,
                                                 DataDePublicacaoDaRevista = dataDaPublicacaoDaRevista,
                                                 NumeroProcesso = conteudoProcesso.Element("patente") == null ? null : conteudoProcesso.Element("patente").Attribute("numero").Value,
                                                 Procurador = conteudoProcesso.Element("procurador") == null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("procurador").Value),
                                                 DataDoDeposito = conteudoProcesso.Attribute("data-deposito") == null ? (DateTime?)null : DateTime.Parse(conteudoProcesso.Attribute("data-deposito").Value),
                                                 DataDaConcessao = conteudoProcesso.Attribute("data-concenssao") == null ? (DateTime?)null : DateTime.Parse(conteudoProcesso.Attribute("data-concenssao").Value),
                                                 Despacho = conteudoProcesso.Element("despachos").Element("despacho").Attribute("codigo").Value,
                                                 Titular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("nome-razao-social") == null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("titulares").Element("titular").Attribute("nome-razao-social").Value),
                                                 UFTitular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("uf") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("uf").Value,
                                                 PaisTitular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("pais") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("pais").Value,
                                                 Titulo = conteudoProcesso.Element("patente") == null ? null : conteudoProcesso.Element("patente").Element("titulo") == null ?  null :  UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("patente").Element("titulo").Value),
                                                 Inventores = conteudoProcesso.Element("patente") == null ? null : conteudoProcesso.Element("patente").Element("inventores") == null ? null :  UtilidadesDePersistencia.MapeieStringParaListaDeString(UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("patente").Element("inventores").Value),','),
                                                 Depositante = conteudoProcesso.Element("depositante") == null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("depositante").Value),
                                                 UFDepositante = conteudoProcesso.Element("depositante") == null ? null : conteudoProcesso.Element("depositante").Attribute("uf") == null ? null : conteudoProcesso.Element("depositante").Attribute("uf").Value,
                                                 PaisDepositante = conteudoProcesso.Element("depositante") == null ? null : conteudoProcesso.Element("depositante").Attribute("pais") == null ? null : conteudoProcesso.Element("depositante").Attribute("pais").Value,
                                                 NumeroDoPedido = conteudoProcesso.Element("numeroDoPedido") == null ? null : conteudoProcesso.Element("numeroDoPedido").Value,
                                                 
                                             }).ToList();

            return new KeyValuePair<int, IList<DTOProcessoPatenteRevista>>(numeroDaRevista, listaDeProcessosNaRevista);
        }

        public IList<string> ProcesseEmLote(string pastaDeArmazenamentoDasRevistas)
        {
            if (!Directory.Exists(pastaDeArmazenamentoDasRevistas))
                throw new ApplicationException("Pasta informada não existe!");

            var pastaDeDestinoDaDescompactacao = Path.Combine(pastaDeArmazenamentoDasRevistas, "ArquivosDescompactados");

            DescompacteArquivos(pastaDeArmazenamentoDasRevistas, pastaDeDestinoDaDescompactacao);

            var informacoesDoDiretorio = new DirectoryInfo(pastaDeDestinoDaDescompactacao);
            var arquivosDesompactados = informacoesDoDiretorio.GetFiles();

            IDictionary<int, IList<DTOProcessoPatenteRevista>> listaDeProcessosDePatenteDeRevista = new Dictionary<int, IList<DTOProcessoPatenteRevista>>();

            foreach (var arquivo in arquivosDesompactados)
                listaDeProcessosDePatenteDeRevista.Add(ExtraiaProcessoDePatenteDeRevistaDoArquivo(arquivo));

            return GraveEmLote(listaDeProcessosDePatenteDeRevista);
        }

        private IList<string> GraveEmLote(IDictionary<int, IList<DTOProcessoPatenteRevista>> listaDeProcessosDePatenteDeRevista)
        {
            return null;
        }

        public IList<DTOProcessoPatenteRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatenteRevista>();

            try
            {
                return mapeador.ObtenhaResultadoDaPesquisa(filtro, quantidadeDeRegistros, offSet);
            }
            finally
            {
                ServerUtils.libereRecursos();
            } 
        }

        public int ObtenhaQuantidadeDeResultadoDaPesquisa(IFiltro filtro)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatenteRevista>();

            try
            {
                return mapeador.ObtenhaQuantidadeDeResultadoDaPesquisa(filtro);
            }
            finally
            {
                ServerUtils.libereRecursos();
            } 
        }
    }
}
