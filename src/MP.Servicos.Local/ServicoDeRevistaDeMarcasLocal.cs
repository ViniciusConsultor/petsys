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

        //public IList<IRevistaDeMarcas> InserirELerRevistaXmlERetornarProcessosExistentes(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml)
        //{
        //    ServerUtils.setCredencial(_Credencial);

        //    var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

        //    try
        //    {
        //        // FAZER A LEITURA DA REVISTA E ATUALIZAR OS CAMPOS NECESSÁRIOS

        //        IList<IRevistaDeMarcas> listaDeProcessosExistentesNaRevista = new List<IRevistaDeMarcas>();

        //        listaDeProcessosExistentesNaRevista = LerRevistaXMLParaProcessosExistentes(revistaDeMarcas, revistaXml);

        //        ServerUtils.BeginTransaction();
        //        mapeador.InserirDadosRevistaXml(listaDeProcessosExistentesNaRevista);
        //        ServerUtils.CommitTransaction();

        //        return listaDeProcessosExistentesNaRevista;
        //    }
        //    catch
        //    {
        //        ServerUtils.RollbackTransaction();
        //        throw;
        //    }
        //    finally
        //    {
        //        ServerUtils.libereRecursos();
        //    }
        //}

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
                        objetoRevista.CodigoDespachoAtual = despachos["despacho"].Attributes.GetNamedItem("codigo").Value;
                    }

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

                    //using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                    //{
                    //    foreach (var processo in listaDeProcessosDaRevistaDeMarcas)
                    //    {
                    //        var objetoRevistaASerSalvo = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

                    //        IProcessoDeMarca processoDeMarcaExistente = servico.ObtenhaProcessoDeMarcaPeloNumero(processo.NumeroProcessoDeMarca);

                    //        // validar codigo.

                    //        if (processoDeMarcaExistente.IdProcessoDeMarca != null)
                    //        {
                    //            objetoRevistaASerSalvo.IdRevistaMarcas = GeradorDeID.getInstancia().getProximoID();

                    //            codigoDespachoDaProcessoRevista = processo.CodigoDespachoAtual;

                    //            objetoRevistaASerSalvo.Apostila = processo.Apostila;

                    //            if (processoDeMarcaExistente.Despacho != null)
                    //            {
                    //                objetoRevistaASerSalvo.CodigoDespachoAnterior = processoDeMarcaExistente.Despacho.CodigoDespacho;
                    //                processoDeMarcaExistente.Despacho.CodigoDespacho = codigoDespachoDaProcessoRevista; 
                    //            }
                    //            else if (processoDeMarcaExistente.Despacho == null && !string.IsNullOrEmpty(codigoDespachoDaProcessoRevista))
                    //            {
                    //                objetoRevistaASerSalvo.CodigoDespachoAnterior = null;

                    //                IList<IDespachoDeMarcas> listaDeDespachos = new List<IDespachoDeMarcas>();

                    //                using (var servicoDespacho = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
                    //                {
                    //                    listaDeDespachos = servicoDespacho.ObtenhaPorCodigoDoDespachoComoFiltro(
                    //                        codigoDespachoDaProcessoRevista, int.MaxValue);
                    //                }

                    //                if (listaDeDespachos.Count > 0)
                    //                {
                    //                    processoDeMarcaExistente.Despacho = listaDeDespachos[0];
                    //                }
                    //            }
                                

                    //            objetoRevistaASerSalvo.CodigoDespachoAtual = processo.CodigoDespachoAtual;
                    //            objetoRevistaASerSalvo.DataProcessamento = processo.DataProcessamento;
                    //            objetoRevistaASerSalvo.DataPublicacao = processo.DataPublicacao;
                    //            objetoRevistaASerSalvo.ExtensaoArquivo = processo.ExtensaoArquivo;
                    //            objetoRevistaASerSalvo.NumeroProcessoDeMarca = processo.NumeroProcessoDeMarca;
                    //            objetoRevistaASerSalvo.NumeroRevistaMarcas = processo.NumeroRevistaMarcas;
                    //            objetoRevistaASerSalvo.Processada = processo.Processada;
                    //            objetoRevistaASerSalvo.TextoDoDespacho = processo.TextoDoDespacho;

                    //            listaDeDadosDaRevistaASerSalvo.Add(objetoRevistaASerSalvo);

                    //            servico.Modificar(processoDeMarcaExistente);
                    //        }
                    //    }
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
