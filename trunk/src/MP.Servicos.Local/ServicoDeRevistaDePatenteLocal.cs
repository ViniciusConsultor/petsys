using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeRevistaDePatenteLocal : Servico, IServicoDeRevistaDePatente
    {
        public ServicoDeRevistaDePatenteLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Inserir(IList<IRevistaDePatente> listaDeObjetoRevistaDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDePatente>();

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

        public IList<IRevistaDePatente> ObtenhaRevistasAProcessar(int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaRevistasAProcessar(quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IList<IRevistaDePatente> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaRevistasJaProcessadas(quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }


        public IList<IRevistaDePatente> ObtenhaProcessosExistentesDeAcordoComARevistaXml(IRevistaDePatente revistaDePatentes, XmlDocument revistaXml)
        {
            return LeiaRevistaXMLEPreenchaProcessosExistentes(revistaXml);
        }

        private IList<IRevistaDePatente> LeiaRevistaXMLEPreenchaProcessosExistentes(XmlDocument revistaXml)
        {
            IList<IRevistaDePatente> listaDeRevistasDePatentes = CarregueDadosDeTodaRevistaXML(revistaXml);
            var listaDeRevistasASeremSalvas = new List<IRevistaDePatente>();

            if (listaDeRevistasDePatentes.Count > 0)
            {
                IList<string> listaDeNumerosDeProcessosCadastrados = new List<string>();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                {
                    listaDeNumerosDeProcessosCadastrados = servico.ObtenhaTodosNumerosDeProcessosCadastrados();

                    foreach (IRevistaDePatente processo in listaDeRevistasDePatentes)
                    {
                        if (processo.NumeroDoProcesso != null && listaDeNumerosDeProcessosCadastrados.Contains(processo.NumeroDoProcesso.Replace("-", "")))
                        {
                            var revistaASerSalva = processo;
                            var processoDePatenteExistente = servico.ObtenhaPeloNumeroDoProcesso(processo.NumeroDoProcesso);

                            if(processoDePatenteExistente != null && processoDePatenteExistente.IdProcessoDePatente != null)
                            {
                                revistaASerSalva.IdRevistaPatente = GeradorDeID.getInstancia().getProximoID();

                                if (processo.DataDeConcessao != null)
                                {
                                    processoDePatenteExistente.DataDaConcessao = processo.DataDeConcessao;
                                    revistaASerSalva.DataDeConcessao = processo.DataDeConcessao;
                                }

                                if (processo.DataPublicacao != null)
                                {
                                    processoDePatenteExistente.DataDaPublicacao = processo.DataPublicacao;
                                    revistaASerSalva.DataPublicacao = processo.DataPublicacao;
                                }

                                if (processo.DataDaCriacao != null)
                                {
                                    processoDePatenteExistente.DataDaVigencia = processo.DataDaCriacao;
                                    revistaASerSalva.DataDaCriacao = processo.DataDaCriacao;
                                }

                                if (processo.DataDeDeposito != null)
                                {
                                    processoDePatenteExistente.DataDoDeposito = processo.DataDeDeposito;
                                    revistaASerSalva.DataDeDeposito = processo.DataDeDeposito;
                                }

                                if (processo.DataProcessamento != null)
                                {
                                    processoDePatenteExistente.DataDoExame = processo.DataProcessamento;
                                    revistaASerSalva.DataProcessamento = processo.DataProcessamento;
                                }

                                if (processo.CodigoDoDespacho != null)
                                {
                                    revistaASerSalva.CodigoDoDespachoAnterior = processoDePatenteExistente.Despacho == null
                                                                                    ? null : processoDePatenteExistente.Despacho.Codigo;

                                    AtualizeDespachoNoProcesso(processo.CodigoDoDespacho, processoDePatenteExistente);
                                }

                                if (!string.IsNullOrEmpty(processo.ClassificacaoInternacional))
                                {
                                    processoDePatenteExistente.PCT = FabricaGenerica.GetInstancia().CrieObjeto<IPCT>();
                                    processoDePatenteExistente.PCT.DataDaPublicacao = processo.DataPublicacao;
                                    processoDePatenteExistente.PCT.DataDoDeposito = processo.DataDeDeposito;
                                    processoDePatenteExistente.PCT.Numero = processo.NumeroDoProcesso;
                                    processoDePatenteExistente.PCT.NumeroWO = processo.DadosPublicacaoInternacional;
                                }

                                processoDePatenteExistente.ProcessoEhEstrangeiro = string.IsNullOrEmpty(processo.ClassificacaoInternacional);

                                revistaASerSalva.Processada = true;
                                revistaASerSalva.ExtensaoArquivo = ".XML";
                                listaDeRevistasASeremSalvas.Add(revistaASerSalva);
                                servico.AtualizeProcessoAposLeituraDaRevista(processoDePatenteExistente);
                            }
                        }
                    }
                }
            }

            return listaDeRevistasASeremSalvas;
        }

        private void PreenchaCodigoDoDespacho(XmlNode processo, IRevistaDePatente revistaDePatente)
        {
            if(processo["despachos"] != null && processo["despachos"]["despacho"] != null)
                revistaDePatente.CodigoDoDespacho = processo["despachos"]["despacho"].Attributes.GetNamedItem("codigo").Value;
        }

        private void PreenchaTitulares(XmlNode processo, IRevistaDePatente revistaDePatente)
        {
            if (processo["titulares"] != null && processo["titulares"]["titular"] != null)
            {
                if(processo["titulares"]["titular"].Attributes.GetNamedItem("nome-razao-social") != null)
                    revistaDePatente.Titular = processo["titulares"]["titular"].Attributes.GetNamedItem("nome-razao-social").Value;

                if (processo["titulares"]["titular"].Attributes.GetNamedItem("pais") != null)
                    revistaDePatente.PaisTitular = processo["titulares"]["titular"].Attributes.GetNamedItem("pais").Value;

                if (processo["titulares"]["titular"].Attributes.GetNamedItem("uf") != null)
                    revistaDePatente.UFTitular = processo["titulares"]["titular"].Attributes.GetNamedItem("uf").Value;
            }
        }

        private DateTime? VerifiqueERetorneValorDataDoNo(XmlNode xmlNode, string tagDoNo)
        {
            try
            {
                return xmlNode.Attributes.GetNamedItem(tagDoNo) != null ? Convert.ToDateTime(xmlNode.Attributes.GetNamedItem(tagDoNo).Value) : (DateTime?)DateTime.MinValue;
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        private string VerifiqueERetorneValorStringDoNo(XmlNode xmlNode, string tagDoNo)
        {
            return xmlNode.Attributes.GetNamedItem(tagDoNo) != null ? xmlNode.Attributes.GetNamedItem(tagDoNo).Value : string.Empty;
        }

        private void PreenchaPatente(XmlNode processo, IRevistaDePatente revistaDePatente)
        {
            if (processo["patente"] != null)
            {
                revistaDePatente.NumeroProcessoDaPatente = VerifiqueERetorneValorStringDoNo(processo["patente"], "numero");

                if (processo["patente"]["titulo"] != null)
                    revistaDePatente.Titulo = processo["patente"]["titulo"].InnerText;

                if (processo["patente"]["dadosDaPatente"] != null)
                    revistaDePatente.DadosDoPedidoOriginal = processo["patente"]["dadosDaPatente"].InnerText;

                if (processo["patente"]["natureza"] != null)
                    revistaDePatente.NaturezaDoDocumento = processo["patente"]["natureza"].InnerText;

                if (processo["patente"]["observacao"] != null)
                    revistaDePatente.Observacao = processo["patente"]["observacao"].InnerText;

                if (processo["patente"]["resumo"] != null)
                    revistaDePatente.Resumo = processo["patente"]["resumo"].InnerText;

                if (processo["patente"]["inventores"] != null)
                    revistaDePatente.Inventor = processo["patente"]["inventores"].InnerText;

                if (processo["patente"]["classificacaoInternacional"] != null)
                    revistaDePatente.ClassificacaoInternacional = processo["patente"]["classificacaoInternacional"].InnerText;

                if (processo["patente"]["classificacaoNacional"] != null)
                    revistaDePatente.ClassificacaoNacional = processo["patente"]["classificacaoNacional"].InnerText;

                if (processo["patente"]["adicaoPatente"] != null)
                    revistaDePatente.DadosDoPedidoDaPatente = processo["patente"]["adicaoPatente"].InnerText;

                if (processo["patente"]["programaDeComputador"] != null)
                    PreenchaInformacoesPatenteProgramaDeComputador(processo["patente"]["programaDeComputador"], revistaDePatente);
            }
        }

        private void PreenchaInformacoesPatenteProgramaDeComputador(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            revistaDePatente.Criador = VerifiqueERetorneValorStringDoNo(xmlNode, "criador");
            revistaDePatente.Linguagem = VerifiqueERetorneValorStringDoNo(xmlNode, "liguagem");
            revistaDePatente.CampoDeAplicacao = VerifiqueERetorneValorStringDoNo(xmlNode, "campo-aplicacao");
            revistaDePatente.TipoDePrograma = VerifiqueERetorneValorStringDoNo(xmlNode, "tipo-programa");
            revistaDePatente.DataDaCriacao = VerifiqueERetorneValorDataDoNo(xmlNode, "data-criacao");
        }

        private void PreenchaNumeroDoPedidoParaRevistaDePatente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["numeroDoPedido"] != null)
                revistaDePatente.NumeroDoPedido = xmlNode["numeroDoPedido"].InnerText;
        }

        private void PreenchaPrioridadeUnionista(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["prioridadeUnionista"] == null) return;

            revistaDePatente.PrioridadeUnionista = xmlNode["prioridadeUnionista"].InnerText;
        }

        private void PreenchaDepositante(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["depositante"] == null) return;

            revistaDePatente.Depositante = xmlNode["depositante"].InnerText;
        }

        private void PreenchaProcurador(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["procurador"] == null) return;

            revistaDePatente.Procurador = xmlNode["procurador"].InnerText;
        }

        private void PreenchaPaisesDesignados(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisesDesignados"] == null) return;

            revistaDePatente.PaisesDesignados = xmlNode["paisesDesignados"].InnerText;
        }

        private void PreenchaDadosDepositoInternacional(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisesDepositoInternacional"] == null) return;

            revistaDePatente.DadosDepositoInternacional = xmlNode["paisesDepositoInternacional"].InnerText;
        }

        private void PreenchaDadosPublicacaoInternacional(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisesPublicacaoInternacional"] == null) return;

            revistaDePatente.DadosPublicacaoInternacional = xmlNode["paisesPublicacaoInternacional"].InnerText;
        }

        private void PreenchaResponsavelIR(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["responsavelIR"] == null) return;

            revistaDePatente.ResponsavelPagamentoImpostoDeRenda = xmlNode["responsavelIR"].InnerText;
        }

        private void PreenchaComplemento(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["complemento"] == null) return;

            revistaDePatente.Complemento = xmlNode["complemento"].InnerText;
        }

        private void PreenchaDecisao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["decisao"] == null) return;

            revistaDePatente.Decisao = xmlNode["decisao"].InnerText;
        }

        private void PreenchaRecorrente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["recorrente"] == null) return;

            revistaDePatente.Recorrente= xmlNode["recorrente"].InnerText;
        }

        private void PreenchaCedente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["cedente"] == null) return;

            revistaDePatente.Cedente = xmlNode["cedente"].InnerText;
        }

        private void PreenchaCessionaria(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["cessionaria"] == null) return;

            revistaDePatente.Cessionaria = xmlNode["cessionaria"].InnerText;
        }

        private void PreenchaObservacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["observacao"] == null) return;

            revistaDePatente.Observacao = xmlNode["observacao"].InnerText;
        }

        private void PreenchaUltimaInformacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["ultimaInformacao"] == null) return;

            revistaDePatente.UltimaInformacao = xmlNode["ultimaInformacao"].InnerText;
        }

        private void PreenchaCertificadoAverbacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["certificadoAverbacao"] == null) return;

            revistaDePatente.CertificadoDeAverbacao = xmlNode["certificadoAverbacao"].InnerText;
        }

        private void PreenchaPaisCedente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisCedente"] == null) return;

            revistaDePatente.PaisCedente = xmlNode["paisCedente"].InnerText;
        }

        private void PreenchaPaisCessionaria(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisCessionaria"] == null) return;

            revistaDePatente.PaisDaCessionaria = xmlNode["paisCessionaria"].InnerText;
            revistaDePatente.EnderecoDaCessionaria = VerifiqueERetorneValorStringDoNo(xmlNode["paisCessionaria"], "endereco");
        }

        private void PreenchaSetor(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["setor"] == null) return;

            revistaDePatente.Setor = xmlNode["setor"].InnerText;
        }

        private void PreenchaInsentosDeAverbacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["isentosAverbacao"] == null) return;

            revistaDePatente.ServicosIsentosDeAverbacao = xmlNode["isentosAverbacao"].InnerText;
        }

        private void PreenchaRegimeDeGuarda(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["regimeGuarda"] == null) return;

            revistaDePatente.RegimeDeGuarda = xmlNode["regimeGuarda"].InnerText;
        }

        private void PreenchaRequerente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["requerente"] == null) return;

            revistaDePatente.Requerente = xmlNode["requerente"].InnerText;
        }

        private void PreenchaRedacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["redacao"] == null) return;

            revistaDePatente.Redacao = xmlNode["redacao"].InnerText;
        }

        private void AtualizeDespachoNoProcesso(string codigoDoDespacho, IProcessoDePatente processoDePatente)
        {
            if (string.IsNullOrEmpty(codigoDoDespacho)) return;

            IDespachoDePatentes despacho;

            using (var servicoDespacho = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                despacho = servicoDespacho.ObtenhaDespachoPeloCodigo(codigoDoDespacho, int.MaxValue);

            if (despacho != null)
                processoDePatente.Despacho = despacho;
        }

        public IList<IRevistaDePatente> CarregueDadosDeTodaRevistaXML(XmlDocument revistaXml)
        {
            var numeroRevista = string.Empty;
            var dataRevista = string.Empty;
            IList<IRevistaDePatente> listaDeRevistasDePatentes = new List<IRevistaDePatente>();
            XmlNodeList dadosDaRevistaDePatente = revistaXml.GetElementsByTagName("revista");
            XmlNodeList processosDaRevista = revistaXml.GetElementsByTagName("processo");

            if (dadosDaRevistaDePatente.Count > 0 && dadosDaRevistaDePatente[0] != null && dadosDaRevistaDePatente[0].Attributes != null &&
                dadosDaRevistaDePatente[0].Attributes.GetNamedItem("numero") != null && dadosDaRevistaDePatente[0].Attributes.GetNamedItem("data") != null)
            {
                numeroRevista = dadosDaRevistaDePatente[0].Attributes.GetNamedItem("numero").Value;
                dataRevista = dadosDaRevistaDePatente[0].Attributes.GetNamedItem("data").Value;
            }

            foreach (XmlNode processo in processosDaRevista)
            {
                var revistaDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDePatente>();

                if (processo.Attributes.GetNamedItem("numero") != null)
                    revistaDePatente.NumeroProcessoDaPatente = processo.Attributes.GetNamedItem("numero").Value;

                revistaDePatente.NumeroRevistaPatente = Convert.ToInt32(numeroRevista);
                revistaDePatente.DataPublicacao = Convert.ToDateTime(dataRevista);
                revistaDePatente.DataDeDeposito = VerifiqueERetorneValorDataDoNo(processo, "data-deposito");
                revistaDePatente.DataDaProrrogacao = VerifiqueERetorneValorDataDoNo(processo, "data-prorrogacao");
                revistaDePatente.DataDeConcessao = VerifiqueERetorneValorDataDoNo(processo, "data-concenssao");
                revistaDePatente.DataInicioFaseNacional = VerifiqueERetorneValorDataDoNo(processo, "data-inicio-fase-nacional");
                revistaDePatente.MoedaDePagamento = VerifiqueERetorneValorStringDoNo(processo, "moeda-pagamento");
                revistaDePatente.Valor = VerifiqueERetorneValorStringDoNo(processo, "valor");
                revistaDePatente.Pagamento = VerifiqueERetorneValorStringDoNo(processo, "pagamento");
                revistaDePatente.Prazo = VerifiqueERetorneValorStringDoNo(processo, "prazo");
                PreenchaCodigoDoDespacho(processo, revistaDePatente);
                PreenchaTitulares(processo, revistaDePatente);
                PreenchaPatente(processo, revistaDePatente);
                PreenchaNumeroDoPedidoParaRevistaDePatente(processo, revistaDePatente);
                PreenchaPrioridadeUnionista(processo, revistaDePatente);
                PreenchaDepositante(processo, revistaDePatente);
                PreenchaProcurador(processo, revistaDePatente);
                PreenchaPaisesDesignados(processo, revistaDePatente);
                PreenchaDadosDepositoInternacional(processo, revistaDePatente);
                PreenchaDadosPublicacaoInternacional(processo, revistaDePatente);
                PreenchaResponsavelIR(processo, revistaDePatente);
                PreenchaComplemento(processo, revistaDePatente);
                PreenchaDecisao(processo, revistaDePatente);
                PreenchaRecorrente(processo, revistaDePatente);
                PreenchaCedente(processo, revistaDePatente);
                PreenchaCessionaria(processo, revistaDePatente);
                PreenchaObservacao(processo, revistaDePatente);
                PreenchaUltimaInformacao(processo, revistaDePatente);
                PreenchaCertificadoAverbacao(processo, revistaDePatente);
                PreenchaPaisCedente(processo, revistaDePatente);
                PreenchaPaisCessionaria(processo, revistaDePatente);
                PreenchaSetor(processo, revistaDePatente);
                PreenchaInsentosDeAverbacao(processo, revistaDePatente);
                PreenchaRegimeDeGuarda(processo, revistaDePatente);
                PreenchaRequerente(processo, revistaDePatente);
                PreenchaRedacao(processo, revistaDePatente);

                if (!string.IsNullOrEmpty(revistaDePatente.NumeroProcessoDaPatente) && revistaDePatente.NumeroProcessoDaPatente.Length > 10)
                {
                    if(revistaDePatente.NumeroProcessoDaPatente.StartsWith("BR"))
                    {
                        revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroProcessoDaPatente.Substring(5, 14).Replace("-", "").Replace(" ", "");
                        revistaDePatente.NumeroProcessoDaPatente = revistaDePatente.NumeroProcessoDaPatente.Substring(5, 14).Replace("-", "").Replace(" ", "");

                        if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                            revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroProcessoDaPatente.Substring(3, 2);
                    }
                    else
                    {
                        revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroProcessoDaPatente.Substring(3, 9).Replace("-", "");
                        revistaDePatente.NumeroProcessoDaPatente = revistaDePatente.NumeroProcessoDaPatente.Substring(3, 9).Replace("-", "");

                        if(string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                            revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroProcessoDaPatente.Substring(0, 3);
                    }
                    
                }
                else if (!string.IsNullOrEmpty(revistaDePatente.NumeroProcessoDaPatente) && revistaDePatente.NumeroProcessoDaPatente.Length < 10)
                {
                    revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroProcessoDaPatente;
                    revistaDePatente.NumeroProcessoDaPatente = revistaDePatente.NumeroProcessoDaPatente;

                    if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                        revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroProcessoDaPatente;
                }
                else if (!string.IsNullOrEmpty(revistaDePatente.NumeroDoPedido) && revistaDePatente.NumeroDoPedido.Length > 10)
                {
                    if (revistaDePatente.NumeroDoPedido.StartsWith("BR"))
                    {
                        revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroDoPedido.Substring(5, 14).Replace("-", "").Replace(" ", "");
                        revistaDePatente.NumeroDoPedido = revistaDePatente.NumeroDoPedido.Substring(5, 14).Replace("-", "").Replace(" ", "");

                        if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                            revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroDoPedido.Substring(3, 2);
                    }
                    else
                    {
                        revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroDoPedido.Substring(3, 9).Replace("-", "");
                        revistaDePatente.NumeroDoPedido = revistaDePatente.NumeroDoPedido.Substring(3, 9).Replace("-", "");

                        if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                            revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroDoPedido.Substring(0, 3);
                    }
                }
                else
                {
                    revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroDoPedido;
                    revistaDePatente.NumeroDoPedido = revistaDePatente.NumeroDoPedido;

                    if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                        revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroDoPedido;
                }

                listaDeRevistasDePatentes.Add(revistaDePatente);
            }

            return listaDeRevistasDePatentes;
        }

        public IList<IRevistaDePatente> ObtenhaTodosOsProcessosDaRevistaXML(XmlDocument revistaXml, IFiltroLeituraDeRevistaDePatentes filtro)
        {
            IList<IRevistaDePatente> todosProcessoDaRevista = CarregueDadosDeTodaRevistaXML(revistaXml);
            IList<IRevistaDePatente> revistasFiltradas = new List<IRevistaDePatente>();

            foreach (IRevistaDePatente processo in todosProcessoDaRevista)
            {
                bool deveAdicionarProcesso = false;

                if (!string.IsNullOrEmpty(filtro.NumeroDoProcesso) && processo.NumeroDoProcesso.Contains(filtro.NumeroDoProcesso))
                    deveAdicionarProcesso = true;

                if (!string.IsNullOrEmpty(filtro.Depositante) && filtro.Depositante.Equals(processo.Depositante))
                    deveAdicionarProcesso = true;

                if (!string.IsNullOrEmpty(filtro.Titular) && filtro.Titular.Equals(processo.Titular))
                    deveAdicionarProcesso = true;

                if (filtro.Procurador != null && filtro.Procurador.Pessoa.Nome.Equals(processo.Procurador))
                    deveAdicionarProcesso = true;

                 if(deveAdicionarProcesso)
                    revistasFiltradas.Add(processo);
            }

            return revistasFiltradas;
        }

        public void Excluir(int numeroDaRevistaDePatente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDePatente>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(numeroDaRevistaDePatente);
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
    }
}
