using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
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

        public void Inserir(IList<IRevistaDeMarcas> listaDeObjetoRevistaDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.InserirDadosRevistaXml(listaDeObjetoRevistaDeMarcas);
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

        public IList<IRevistaDeMarcas> ObtenhaProcessosExistentesDeAcordoComARevistaXml(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml)
        {
            IList<IRevistaDeMarcas> listaDeProcessosExistentesNaRevista = new List<IRevistaDeMarcas>();

            listaDeProcessosExistentesNaRevista = LerRevistaXMLParaProcessosExistentes(revistaDeMarcas, revistaXml);

            return listaDeProcessosExistentesNaRevista;
        }

        
        private IList<IRevistaDeMarcas> LerRevistaXMLParaProcessosExistentes(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml)
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
                    
                    var despachos = processo["despachos"];

                    if (despachos != null)
                    {
                        var despacho = despachos["despacho"];

                        if (despacho != null)
                        {
                            objetoRevista.CodigoDespachoAtual = despacho.Attributes.GetNamedItem("codigo").Value;
                            
                                var textoDoDespacho = despacho["texto-complementar"];

                                objetoRevista.TextoDoDespacho = textoDoDespacho != null ? textoDoDespacho.InnerText : null;
                        }
                            
                    }

                    var classeNacional = processo["classe-nacional"];

                    if(classeNacional != null)
                    {
                        //objetoRevista.codigoClasseNacional = classeNacional.Attributes.GetNamedItem("codigo").Value;

                        var especificacaoClasseNacional = classeNacional["especificacao"];

                        if(especificacaoClasseNacional != null)
                        {
                            //objetoRevista.especificacaoClasseNacional = especificacaoClasseNacional.InnerText;
                        }

                        var subClassesNacional = classeNacional["sub-classes-nacional"];

                        if (subClassesNacional != null)
                        {
                            //objetoRevista.listaDeSubClassesNacional = new List<string>();

                            foreach (XmlNode subClasseNacional in subClassesNacional)
                            {
                                //if (subClasseNacional.Attributes != null)
                                //    objetoRevista.listaDeSubClassesNacional.Add(
                                //        subClasseNacional.Attributes.GetNamedItem("codigo").Value);
                            }
                        }
                    }

                    var apostila = processo["apostila"];

                    objetoRevista.Apostila = apostila != null ? apostila.InnerText : null;

                    var prioridadeUnionista = processo["prioridade-unionista"];

                    if (prioridadeUnionista != null)
                    {
                        var prioridade = prioridadeUnionista["prioridade"];

                        if (prioridade != null)
                        {
                            //objetoRevista.dataPrioridadeUnionista = prioridade.Attributes.GetNamedItem("data").Value;
                            //objetoRevista.numeroPrioridadeUnionista = prioridade.Attributes.GetNamedItem("numero").Value;
                            //objetoRevista.paisPrioridadeUnionista = prioridade.Attributes.GetNamedItem("pais").Value;
                        }
                    }

                    //var sobrestadores = processo["sobrestadores"];

                    //if (sobrestadores != null)
                    //{
                    //    objetoRevista.dicionarioDeSobrestatores = new Dictionary<string, string>();

                    //    foreach (XmlNode sobrestador in sobrestadores)
                    //    {
                    //        if(sobrestador.Attributes != null)
                    //        {
                    //            dicionarioDeSobrestatores.Add(sobrestador.Attributes.GetNamedItem("processo").Value,
                    //                sobrestador.Attributes.GetNamedItem("marca").Value);
                    //        }
                    //    }
                    //}

                    objetoRevista.NumeroRevistaMarcas = Convert.ToInt32(numeroRevista);
                    objetoRevista.DataPublicacao = Convert.ToDateTime(dataRevista);
                    objetoRevista.DataProcessamento = DateTime.Now;

                    objetoRevista.Processada = revistaDeMarcas.Processada;
                    objetoRevista.ExtensaoArquivo = revistaDeMarcas.ExtensaoArquivo;

                    listaDeProcessosDaRevistaDeMarcas.Add(objetoRevista);
                }

                string codigoDespachoDaProcessoRevista = string.Empty;

                IList<IRevistaDeMarcas> listaDeDadosDaRevistaASerSalvo = new List<IRevistaDeMarcas>();

                if (listaDeProcessosDaRevistaDeMarcas.Count > 0)
                {
                    // verifica se o processo está cadastrado na base

                    IList<long> listaDeNumerosDeProcessosCadastrados = new List<long>();

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                    {
                        listaDeNumerosDeProcessosCadastrados = servico.ObtenhaTodosNumerosDeProcessosCadastrados();

                        foreach (var processo in listaDeProcessosDaRevistaDeMarcas)
                        {
                            if(listaDeNumerosDeProcessosCadastrados.Contains(processo.NumeroProcessoDeMarca))
                            {
                                var objetoRevistaASerSalvo = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

                                var processoDeMarcaExistente = servico.ObtenhaProcessoDeMarcaPeloNumero(processo.NumeroProcessoDeMarca);

                                        if (processoDeMarcaExistente.IdProcessoDeMarca != null)
                                        {
                                            objetoRevistaASerSalvo.IdRevistaMarcas = GeradorDeID.getInstancia().getProximoID();

                                            codigoDespachoDaProcessoRevista = processo.CodigoDespachoAtual;

                                            objetoRevistaASerSalvo.Apostila = processo.Apostila;

                                            if (processoDeMarcaExistente.Despacho != null)
                                            {
                                                objetoRevistaASerSalvo.CodigoDespachoAnterior = processoDeMarcaExistente.Despacho.CodigoDespacho;
                                                processoDeMarcaExistente.Despacho.CodigoDespacho = codigoDespachoDaProcessoRevista; 
                                            }
                                            else if (processoDeMarcaExistente.Despacho == null && !string.IsNullOrEmpty(codigoDespachoDaProcessoRevista))
                                            {
                                                objetoRevistaASerSalvo.CodigoDespachoAnterior = null;

                                                IList<IDespachoDeMarcas> listaDeDespachos = new List<IDespachoDeMarcas>();

                                                using (var servicoDespacho = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
                                                {
                                                    listaDeDespachos = servicoDespacho.ObtenhaPorCodigoDoDespachoComoFiltro(
                                                        codigoDespachoDaProcessoRevista, int.MaxValue);
                                                }

                                                if (listaDeDespachos.Count > 0)
                                                {
                                                    processoDeMarcaExistente.Despacho = listaDeDespachos[0];
                                                }
                                            }

                                            objetoRevistaASerSalvo.CodigoDespachoAtual = processo.CodigoDespachoAtual;
                                            objetoRevistaASerSalvo.DataProcessamento = processo.DataProcessamento;
                                            objetoRevistaASerSalvo.DataPublicacao = processo.DataPublicacao;
                                            objetoRevistaASerSalvo.ExtensaoArquivo = processo.ExtensaoArquivo;
                                            objetoRevistaASerSalvo.NumeroProcessoDeMarca = processo.NumeroProcessoDeMarca;
                                            objetoRevistaASerSalvo.NumeroRevistaMarcas = processo.NumeroRevistaMarcas;
                                            objetoRevistaASerSalvo.Processada = processo.Processada;
                                            objetoRevistaASerSalvo.TextoDoDespacho = processo.TextoDoDespacho;

                                            listaDeDadosDaRevistaASerSalvo.Add(objetoRevistaASerSalvo);

                                            servico.Modificar(processoDeMarcaExistente);
                            }
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
