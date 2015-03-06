using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using PMP.Interfaces.Mapeadores;
using PMP.Interfaces.Servicos;
using System.IO;
using PMP.Interfaces.Utilidades;

namespace PMP.Servicos.Local
{
    public class ServicoDeProcessoDeMarcaDeRevistaLocal : Servico, IServicoDeProcessoDeMarcaDeRevista
    {
        public ServicoDeProcessoDeMarcaDeRevistaLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }

        public void ProcesseEmLote(string pastaDeArmazenamentoDasRevistas)
        {
            if (!Directory.Exists(pastaDeArmazenamentoDasRevistas))
                throw new ApplicationException("Pasta informada não existe!");
            
            var informacoesDoDiretorio = new DirectoryInfo(pastaDeArmazenamentoDasRevistas);
            var arquivosAProcessar = informacoesDoDiretorio.GetFiles();
            IDictionary<int, IList<DTOProcessoMarcaRevista>> listaDeProcessosDeMarcasDeRevista = new Dictionary<int, IList<DTOProcessoMarcaRevista>>();
            
            foreach (var arquivo in arquivosAProcessar)
                listaDeProcessosDeMarcasDeRevista.Add(ExtraiaProcessoDeMarcasDeRevistaDoArquivo(arquivo.FullName));

           GraveEmLote(listaDeProcessosDeMarcasDeRevista);
        }

        public IList<DTOProcessoMarcaRevista> ObtenhaResultadoDaPesquisa(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoMarcaRevista>();

            try
            {
                return mapeador.ObtenhaResultadoDaPesquisa(filtro, quantidadeDeRegistros, offSet);
                
            }
            finally
            {
                ServerUtils.libereRecursos();
            } 
        }

        private void GraveEmLote(IDictionary<int, IList<DTOProcessoMarcaRevista>> listaDeProcessosDeMarcasDeRevista)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoMarcaRevista>();

            try
            {
               ServerUtils.BeginTransaction();
               mapeador.GraveEmLote(listaDeProcessosDeMarcasDeRevista);
               ServerUtils.CommitTransaction();
            }
            catch
            {
                ServerUtils.RollbackTransaction();
                throw;
            }
            finally
            {
                ServerUtils.libereRecursos();
            } 
        }

        private KeyValuePair<int, IList<DTOProcessoMarcaRevista>> ExtraiaProcessoDeMarcasDeRevistaDoArquivo(string caminhoCompletoDoArquivo)
        {
            //Verificar se arquivo é um txt, se for converter primeiro

            var arquivoDaRevista = XDocument.Load(caminhoCompletoDoArquivo);
            var conteudoDaRevista = arquivoDaRevista.Element("revista");
            var numeroDaRevista = int.Parse(conteudoDaRevista.Attribute("numero").Value);
            var dataDaPublicacaoDaRevista = DateTime.Parse(conteudoDaRevista.Attribute("data").Value);

            var listaDeProcessosNaRevista = (from conteudoProcesso in conteudoDaRevista.Elements("processo")
                select new DTOProcessoMarcaRevista
                {
                    NumeroDaRevista = numeroDaRevista,
                    DataDePublicacaoDaRevista = dataDaPublicacaoDaRevista,
                    NumeroProcessoDeMarca = conteudoProcesso.Attribute("numero").Value,
                    DataDaConcessao = conteudoProcesso.Attribute("data-concessao") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-concessao").Value),
                    DataDaVigencia = conteudoProcesso.Attribute("data-vigencia") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-vigencia").Value),
                    DataDoDeposito = conteudoProcesso.Attribute("data-deposito") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-deposito").Value),
                    CodigoDoDespacho = conteudoProcesso.Element("despachos").Element("despacho").Attribute("codigo").Value,
                    NomeDoDespacho =  conteudoProcesso.Element("despachos").Element("despacho").Attribute("nome")== null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("despachos").Element("despacho").Attribute("nome").Value),
                    TextoComplementarDoDespacho = conteudoProcesso.Element("despachos").Element("despacho").Element("texto-complementar") == null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("despachos").Element("despacho").Element("texto-complementar").Value),
                    Titular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("nome-razao-social") == null ? null :UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("titulares").Element("titular").Attribute("nome-razao-social").Value),
                    UFTitular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("uf") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("uf").Value,
                    PaisTitular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("pais") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("pais").Value,
                    Procurador = conteudoProcesso.Element("procurador") == null ? null :  UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("procurador").Value),
                    Marca = conteudoProcesso.Element("marca") == null ? null : conteudoProcesso.Element("marca").Element("nome") == null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("marca").Element("nome").Value),
                    Apresentacao = conteudoProcesso.Element("marca") == null ? null : conteudoProcesso.Element("marca").Attribute("apresentacao") == null ? null : conteudoProcesso.Element("marca").Attribute("apresentacao").Value,
                    Natureza = conteudoProcesso.Element("marca") == null ? null : conteudoProcesso.Element("marca").Attribute("natureza") == null ? null : conteudoProcesso.Element("marca").Attribute("natureza").Value,
                    Apostila = conteudoProcesso.Element("apostila") == null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("apostila").Value),
                
                }).ToList();

            return  new KeyValuePair<int, IList<DTOProcessoMarcaRevista>>(numeroDaRevista, listaDeProcessosNaRevista);
        }
    }
}
