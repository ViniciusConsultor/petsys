using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeRevistaDeMarcasLocal : Servico , IServicoDeRevistaDeMarcas
    {
        public ServicoDeRevistaDeMarcasLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void InserirELerRevistaXml(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                // FAZER A LEITURA DA REVISTA E ATUALIZAR OS CAMPOS NECESSÁRIOS

                IList<IRevistaDeMarcas> listaDeDadosDaRevistaASerSalvos = new List<IRevistaDeMarcas>();

                listaDeDadosDaRevistaASerSalvos = LerRevistaXML(revistaDeMarcas, revistaXml);

                ServerUtils.BeginTransaction();
                //mapeador.InserirELerRevistaXml(listaDeDadosDaRevistaASerSalvos);
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

        private IList<IRevistaDeMarcas> LerRevistaXML(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml)
        {
            try
            {
                var numeroRevista = string.Empty;
                var dataRevista = string.Empty;

                IList<IRevistaDeMarcas> listaDeProcessosDaRevistaDeMarcas = new List<IRevistaDeMarcas>();

                XmlNodeList dadosRevista = revistaXml.GetElementsByTagName("revista");

                for (int i = 0; i < dadosRevista.Count; i++)
                {
                    numeroRevista = dadosRevista[i].Attributes.GetNamedItem("numero").Value;
                    dataRevista = dadosRevista[i].Attributes.GetNamedItem("data").Value;
                }

                XmlNodeList xmlprocessos = revistaXml.GetElementsByTagName("processo");

                foreach (XmlNode processo in xmlprocessos)
                {
                    var objetoRevista = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

                    objetoRevista.NumeroProcessoDeMarca = Convert.ToInt64(processo.Attributes.GetNamedItem("numero").Value);
                    // objetoRevista.DataPublicacao = Convert.ToDateTime(processo.Attributes.GetNamedItem("data-deposito").Value);

                    var despachos = processo["despachos"];

                    if (despachos != null)
                    {
                        objetoRevista.CodigoDespacho = despachos["despacho"].Attributes.GetNamedItem("codigo").Value;
                    }

                    //var titulares = processo["titulares"];

                    //if (titulares != null)
                    //{
                    //    objetoRevista.titular =
                    //        titulares["titular"].Attributes.GetNamedItem("nome-razao-social").Value;

                    //    objetoRevista.pais = titulares["titular"].Attributes.GetNamedItem("pais").Value;
                    //    objetoRevista.uf = titulares["titular"].Attributes.GetNamedItem("uf").Value;
                    //}

                    //var marca = processo["marca"];

                    //if (marca != null)
                    //{
                    //    objetoRevista.apresentacao = marca.Attributes.GetNamedItem("apresentacao").Value;
                    //    objetoRevista.natureza = marca.Attributes.GetNamedItem("natureza").Value;

                    //    var descricaoMarca = marca["nome"];
                    //    if (descricaoMarca != null) objetoRevista.descricaoMarca = descricaoMarca.InnerText;
                    //}

                    //var classesViena = processo["classes-vienna"];

                    //if (classesViena != null)
                    //{
                    //    objetoRevista.edicaoClasseViena = classesViena.Attributes.GetNamedItem("edicao").Value;

                    //    objetoRevista.listaDeClassesViena = new List<string>();

                    //    foreach (XmlNode classeViena in classesViena)
                    //    {
                    //        if (classeViena.Attributes != null)
                    //            objetoRevista.listaDeClassesViena.Add(
                    //                classeViena.Attributes.GetNamedItem("codigo").Value);
                    //    }
                    //}

                    //var classesNice = processo["classe-nice"];

                    //if (classesNice != null)
                    //{
                    //    objetoRevista.NCL = classesNice.Attributes.GetNamedItem("codigo").Value;

                    //    var especificacao = classesNice["especificacao"];

                    //    if (especificacao != null)
                    //        objetoRevista.especificacao = especificacao.InnerText;
                    //}

                    //objetoRevista.procurador = procurador.InnerText;

                    objetoRevista.NumeroRevistaMarcas = Convert.ToInt32(numeroRevista);
                    objetoRevista.DataPublicacao = Convert.ToDateTime(dataRevista);
                    objetoRevista.DataProcessamento = DateTime.Now;

                    objetoRevista.Apostila = string.Empty;
                    objetoRevista.TextoDoDespacho = string.Empty;

                    objetoRevista.Processada = revistaDeMarcas.Processada;
                    objetoRevista.ExtensaoArquivo = revistaDeMarcas.ExtensaoArquivo;

                    listaDeProcessosDaRevistaDeMarcas.Add(objetoRevista);
                }

                string codigoDespachoDaProcessoRevista = string.Empty;

                IList<IRevistaDeMarcas> listaDeDadosDaRevistaASerSalvo = new List<IRevistaDeMarcas>();

                if (listaDeProcessosDaRevistaDeMarcas.Count > 0)
                {
                    // verifica se o processo está cadastrado na base

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                    {
                        foreach (var processo in listaDeProcessosDaRevistaDeMarcas)
                        {
                            var objetoRevistaASerSalvo = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

                            IProcessoDeMarca processoDeMarcaExistente = servico.ObtenhaProcessoDeMarcaPeloNumero(processo.NumeroProcessoDeMarca);

                            // validar codigo.

                            if (processoDeMarcaExistente != null)
                            {
                                codigoDespachoDaProcessoRevista = processo.CodigoDespacho;

                                objetoRevistaASerSalvo.Apostila = processo.Apostila;
                                objetoRevistaASerSalvo.CodigoDespacho = processoDeMarcaExistente.Despacho.CodigoDespacho;
                                objetoRevistaASerSalvo.DataProcessamento = processo.DataProcessamento;
                                objetoRevistaASerSalvo.DataPublicacao = processo.DataPublicacao;
                                objetoRevistaASerSalvo.ExtensaoArquivo = processo.ExtensaoArquivo;
                                objetoRevistaASerSalvo.IdRevistaMarcas = processo.IdRevistaMarcas;
                                objetoRevistaASerSalvo.NumeroProcessoDeMarca = processo.NumeroProcessoDeMarca;
                                objetoRevistaASerSalvo.NumeroRevistaMarcas = processo.NumeroRevistaMarcas;
                                objetoRevistaASerSalvo.Processada = processo.Processada;
                                objetoRevistaASerSalvo.TextoDoDespacho = processo.TextoDoDespacho;

                                listaDeDadosDaRevistaASerSalvo.Add(objetoRevistaASerSalvo);

                                processoDeMarcaExistente.Despacho.CodigoDespacho = codigoDespachoDaProcessoRevista;

                                servico.Modificar(processoDeMarcaExistente);
                            }
                        }
                    }
                }

                return listaDeDadosDaRevistaASerSalvo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Modificar(IRevistaDeMarcas revistaDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Modificar(revistaDeMarcas);
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

        public IList<IRevistaDeMarcas> ObtenhaRevistasAProcessar(int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                return mapeador.ObtenhaRevistasAProcessar(quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IRevistaDeMarcas> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                return mapeador.ObtenhaRevistasJaProcessadas(quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
