﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Utilidades;
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
            
            var pastaDeDestinoDaDescompactacao = Path.Combine(pastaDeArmazenamentoDasRevistas, "ArquivosDescompactados");

            DescompacteArquivos(pastaDeArmazenamentoDasRevistas, pastaDeDestinoDaDescompactacao);

            var informacoesDoDiretorio = new DirectoryInfo(pastaDeDestinoDaDescompactacao);
            var arquivosDesompactados = informacoesDoDiretorio.GetFiles();

            IDictionary<int, IList<DTOProcessoMarcaRevista>> listaDeProcessosDeMarcasDeRevista = new Dictionary<int, IList<DTOProcessoMarcaRevista>>();

            foreach (var arquivo in arquivosDesompactados)
                listaDeProcessosDeMarcasDeRevista.Add(ExtraiaProcessoDeMarcasDeRevistaDoArquivo(arquivo));

           GraveEmLote(listaDeProcessosDeMarcasDeRevista);
        }

        private void DescompacteArquivos(string pastaDeArmazenamentoDasRevistas, string pastaDeDestino)
        {
            var informacoesDoDiretorio = new DirectoryInfo(pastaDeArmazenamentoDasRevistas);
            var arquivosCompactados = informacoesDoDiretorio.GetFiles();

            foreach (var arquivoCompactado in arquivosCompactados)
                Util.DescompacteArquivoZip(arquivoCompactado.FullName, pastaDeDestino);
                
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

        public int ObtenhaQuantidadeDeResultadoDaPesquisa(IFiltro filtro)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoMarcaRevista>();

            try
            {
                return mapeador.ObtenhaQuantidadeDeResultadoDaPesquisa(filtro);
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
                GC.Collect();
            } 
        }


        private string TransformeTXTParaXml(FileInfo arquivoTxt)
        {
            var numeroDaRevista = arquivoTxt.Name.Substring(2,4);
            var dataRevista = arquivoTxt.LastWriteTime;

            using (var arquivo = new StreamReader(arquivoTxt.FullName))
            {
                TradutorDeRevistaTxtParaRevistaXml.TraduzaRevistaDeMarcas(dataRevista, numeroDaRevista, arquivo, arquivoTxt.Directory.FullName + "\\");
                arquivo.Close();
            }

            return Path.Combine(arquivoTxt.Directory.FullName, numeroDaRevista + ".xml");
        }

        private KeyValuePair<int, IList<DTOProcessoMarcaRevista>> ExtraiaProcessoDeMarcasDeRevistaDoArquivo(FileInfo arquivo)
        {
            //Verificar se arquivo é um txt, se for converter primeiro
            var arquivoDaRevista = XDocument.Load(arquivo.Extension.ToUpper().Equals(".TXT") ? TransformeTXTParaXml(arquivo) : arquivo.FullName);
            var conteudoDaRevista = arquivoDaRevista.Element("revista");
            var numeroDaRevista = int.Parse(conteudoDaRevista.Attribute("numero").Value);
            var dataDaPublicacaoDaRevista = DateTime.Parse(conteudoDaRevista.Attribute("data").Value);

            var listaDeProcessosNaRevista = (from conteudoProcesso in conteudoDaRevista.Elements("processo")
                select new DTOProcessoMarcaRevista
                {
                    ID = Guid.NewGuid().ToString(),
                    NumeroDaRevista = numeroDaRevista,
                    DataDePublicacaoDaRevista = dataDaPublicacaoDaRevista,
                    NumeroProcessoDeMarca = conteudoProcesso.Attribute("numero").Value,
                    DataDaConcessao = conteudoProcesso.Attribute("data-concessao") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-concessao").Value),
                    DataDaVigencia = conteudoProcesso.Attribute("data-vigencia") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-vigencia").Value),
                    DataDoDeposito = conteudoProcesso.Attribute("data-deposito") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-deposito").Value),
                    CodigoDoDespacho = conteudoProcesso.Element("despachos").Element("despacho").Attribute("codigo").Value,
                    NomeDoDespacho =  conteudoProcesso.Element("despachos").Element("despacho").Attribute("nome")== null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("despachos").Element("despacho").Attribute("nome").Value),
                    Titular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("nome-razao-social") == null ? null :UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("titulares").Element("titular").Attribute("nome-razao-social").Value),
                    UFTitular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("uf") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("uf").Value,
                    PaisTitular = conteudoProcesso.Element("titulares") == null ? null : conteudoProcesso.Element("titulares").Element("titular") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("pais") == null ? null : conteudoProcesso.Element("titulares").Element("titular").Attribute("pais").Value,
                    Procurador = conteudoProcesso.Element("procurador") == null ? null :  UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("procurador").Value),
                    Marca = conteudoProcesso.Element("marca") == null ? null : conteudoProcesso.Element("marca").Element("nome") == null ? null : UtilidadesDeString.RemoveAcentos(conteudoProcesso.Element("marca").Element("nome").Value),
                    Apresentacao = conteudoProcesso.Element("marca") == null ? null : conteudoProcesso.Element("marca").Attribute("apresentacao") == null ? null : conteudoProcesso.Element("marca").Attribute("apresentacao").Value,
                    Natureza = conteudoProcesso.Element("marca") == null ? null : conteudoProcesso.Element("marca").Attribute("natureza") == null ? null : conteudoProcesso.Element("marca").Attribute("natureza").Value,
                    CodigoClasseNice =  conteudoProcesso.Element("classe-nice") == null ? null : conteudoProcesso.Element("classe-nice").Attribute("codigo").Value,
                    EdicaoClasseViena = conteudoProcesso.Element("classes-vienna") == null ? null : conteudoProcesso.Element("classes-vienna").Attribute("edicao").Value,
                    CodigosClasseViena = conteudoProcesso.Element("classes-vienna") == null ? null : 
                        (from conteudoClasseViena in conteudoProcesso.Element("classes-vienna").Elements("classe-vienna")
                         let xAttribute = conteudoClasseViena.Attribute("codigo")
                         where xAttribute != null
                         select xAttribute.Value).ToList(),
                    CodigoClasseNacional = conteudoProcesso.Element("classe-nacional") == null ? null : conteudoProcesso.Element("classe-nacional").Attribute("codigo").Value,
                    CodigosSubClasseNacional = conteudoProcesso.Element("classe-nacional") == null ? null :
                          conteudoProcesso.Element("classe-nacional").Elements("sub-classes-nacional") == null ? null :
                        (from conteudoClasseNacional in conteudoProcesso.Element("classe-nacional").Elements("sub-classes-nacional").Elements("sub-classe-nacional")
                         let xAttribute = conteudoClasseNacional.Attribute("codigo")
                         where xAttribute != null
                         select xAttribute.Value).ToList(),                                                                                                                                                               

                }).ToList();

            return new KeyValuePair<int, IList<DTOProcessoMarcaRevista>>(numeroDaRevista, listaDeProcessosNaRevista);
        }
    }
}
