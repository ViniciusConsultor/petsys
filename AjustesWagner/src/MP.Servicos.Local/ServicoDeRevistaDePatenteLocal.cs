using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                IList<IProcessoDePatente> processosCadastrados = new List<IProcessoDePatente>();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                {
                    processosCadastrados = servico.ObtenhaTodosProcessosAtivos();

                    foreach (IRevistaDePatente processoDaRevista in listaDeRevistasDePatentes)
                    {
                        bool verificaSeProcessoExiste = false;

                        processoDaRevista.DataProcessamento = DateTime.Now;

                        if (processoDaRevista != null && processoDaRevista.NumeroDoProcesso != null)
                        {
                            processoDaRevista.NumeroDoProcesso = processoDaRevista.NumeroDoProcesso.Replace("-", string.Empty);

                            if (processoDaRevista.NumeroDoProcesso.Length == 15)
                                verificaSeProcessoExiste = processosCadastrados.ToList().Find(processoDePatente => processoDePatente.Pais != null && processoDePatente.Patente != null &&
                                    (processoDePatente.Pais.Sigla + processoDePatente.Patente.NaturezaPatente.SiglaNatureza + processoDePatente.Processo) == processoDaRevista.NumeroDoProcesso) != null;
                            else
                                verificaSeProcessoExiste = processosCadastrados.ToList().Find(processoDePatente => processoDePatente.Processo == processoDaRevista.NumeroDoProcesso) != null;
                        }

                        if (verificaSeProcessoExiste)
                        {
                            var processoDePatente = processoDaRevista.NumeroDoProcesso.Length == 15 ?
                                servico.ObtenhaPeloNumeroDoProcesso(processoDaRevista.NumeroDoProcesso.Remove(0, 4)) :
                                servico.ObtenhaPeloNumeroDoProcesso(processoDaRevista.NumeroDoProcesso);

                            if(processoDePatente != null && processoDePatente.IdProcessoDePatente != null)
                            {
                                processoDaRevista.IdRevistaPatente = GeradorDeID.getInstancia().getProximoID();

                                if (processoDaRevista.DataDeConcessao != null)
                                    processoDePatente.DataDaConcessao = processoDaRevista.DataDeConcessao;

                                if (processoDaRevista.DataPublicacao != null)
                                    processoDePatente.DataDaPublicacao = processoDaRevista.DataPublicacao;

                                if (processoDaRevista.DataDaCriacao != null)
                                    processoDePatente.DataDaVigencia = processoDaRevista.DataDaCriacao;

                                if (processoDaRevista.DataDeDeposito != null)
                                    processoDePatente.DataDoDeposito = processoDaRevista.DataDeDeposito;

                                if (processoDaRevista.DataProcessamento != null)
                                    processoDePatente.DataDoExame = processoDaRevista.DataProcessamento;

                                if (processoDaRevista.CodigoDoDespacho != null)
                                    AtualizeDespachoNoProcesso(processoDaRevista.CodigoDoDespacho, processoDePatente);

                                if (!string.IsNullOrEmpty(processoDaRevista.Titulo))
                                    processoDePatente.Patente.TituloPatente = processoDaRevista.Titulo;

                                if (!string.IsNullOrEmpty(processoDaRevista.Classificacao))
                                {
                                    bool jaExisteGravacao = processoDePatente.Patente.Classificacoes.Any(classificacaoPatente => classificacaoPatente.Classificacao.Equals(processoDaRevista.Classificacao));

                                    if(!jaExisteGravacao)
                                    {
                                        if(processoDePatente.Patente.Classificacoes == null)
                                        processoDePatente.Patente.Classificacoes = new List<IClassificacaoPatente>();

                                        var classificacao = FabricaGenerica.GetInstancia().CrieObjeto<IClassificacaoPatente>();
                                        classificacao.Classificacao = processoDaRevista.Classificacao;
                                        classificacao.TipoClassificacao = TipoClassificacaoPatente.Nacional;
                                        processoDePatente.Patente.Classificacoes.Add(classificacao);
                                    }
                                }

                                if (!string.IsNullOrEmpty(processoDaRevista.ClassificacaoNacional))
                                {
                                    bool jaExisteGravacao = processoDePatente.Patente.Classificacoes.Any(classificacaoPatente => classificacaoPatente.Classificacao.Equals(processoDaRevista.ClassificacaoNacional));

                                    if(!jaExisteGravacao)
                                    {
                                        if (processoDePatente.Patente.Classificacoes == null)
                                        processoDePatente.Patente.Classificacoes = new List<IClassificacaoPatente>();

                                        var classificacao = FabricaGenerica.GetInstancia().CrieObjeto<IClassificacaoPatente>();
                                        classificacao.Classificacao = processoDaRevista.ClassificacaoNacional;
                                        classificacao.TipoClassificacao = TipoClassificacaoPatente.Nacional;
                                        processoDePatente.Patente.Classificacoes.Add(classificacao);
                                    }
                                }

                                if (!string.IsNullOrEmpty(processoDaRevista.ClassificacaoInternacional))
                                {
                                    bool jaExisteGravacao = processoDePatente.Patente.Classificacoes.Any(classificacaoPatente => classificacaoPatente.Classificacao.Equals(processoDaRevista.ClassificacaoInternacional));

                                    if(!jaExisteGravacao)
                                    {
                                        if (processoDePatente.Patente.Classificacoes == null)
                                        processoDePatente.Patente.Classificacoes = new List<IClassificacaoPatente>();

                                        var classificacao = FabricaGenerica.GetInstancia().CrieObjeto<IClassificacaoPatente>();
                                        classificacao.Classificacao = processoDaRevista.ClassificacaoInternacional;
                                        classificacao.TipoClassificacao = TipoClassificacaoPatente.Internacional;
                                        processoDePatente.Patente.Classificacoes.Add(classificacao);
                                    }
                                }

                                if (!string.IsNullOrEmpty(processoDaRevista.Observacao))
                                    processoDePatente.Patente.Observacao += processoDaRevista.Observacao;

                                if (!string.IsNullOrEmpty(processoDaRevista.Observacao))
                                    processoDePatente.Patente.Resumo += processoDaRevista.Resumo;


                                if (!string.IsNullOrEmpty(processoDaRevista.ClassificacaoInternacional))
                                {
                                    processoDePatente.PCT = FabricaGenerica.GetInstancia().CrieObjeto<IPCT>();
                                    processoDePatente.PCT.DataDaPublicacao = processoDaRevista.DataPublicacao;
                                    processoDePatente.PCT.DataDoDeposito = processoDaRevista.DataDeDeposito;
                                    processoDePatente.PCT.Numero = processoDaRevista.NumeroDoProcesso;
                                    processoDePatente.PCT.NumeroWO = processoDaRevista.DadosPublicacaoInternacional;
                                }

                                processoDePatente.ProcessoEhEstrangeiro = string.IsNullOrEmpty(processoDaRevista.ClassificacaoInternacional);

                                processoDaRevista.Processada = true;
                                processoDaRevista.ExtensaoArquivo = ".XML";
                                listaDeRevistasASeremSalvas.Add(processoDaRevista);
                                Excluir(processoDaRevista.NumeroRevistaPatente);
                                servico.AtualizeProcessoAposLeituraDaRevista(processoDePatente);
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
                if(xmlNode.Attributes.GetNamedItem(tagDoNo) == null)
                    return DateTime.MinValue;

                string valorTag = xmlNode.Attributes.GetNamedItem(tagDoNo).Value;
                DateTime dataDaTag;

                DateTime.TryParse(xmlNode.Attributes.GetNamedItem(tagDoNo).Value, out dataDaTag);

                if (dataDaTag != DateTime.MinValue)
                    return dataDaTag;

                DateTime.TryParse(xmlNode.Attributes.GetNamedItem(tagDoNo).Value.Substring(0, 10), out dataDaTag);

                return dataDaTag;
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
                despacho = servicoDespacho.ObtenhaDespachoPeloCodigo(codigoDoDespacho);

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
                    revistaDePatente.NumeroDoProcessoFormatado = revistaDePatente.NumeroProcessoDaPatente;

                    if(revistaDePatente.NumeroProcessoDaPatente.StartsWith("BR"))
                    {
                        if (revistaDePatente.NumeroProcessoDaPatente.Length <= 19)
                        {
                            revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroProcessoDaPatente.Replace("-", "").Replace(" ", "");
                            revistaDePatente.NumeroProcessoDaPatente = revistaDePatente.NumeroProcessoDaPatente.Replace("-", "").Replace(" ", "");

                            if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                                revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroProcessoDaPatente.Substring(2, 2);
                        }
                        else
                        {
                            string numeroFiltrado = revistaDePatente.NumeroProcessoDaPatente.Substring(0, 19);
                            revistaDePatente.NumeroProcessoDaPatente = numeroFiltrado.Replace("-", "").Replace(" ", "");

                            revistaDePatente.NumeroDoProcesso = numeroFiltrado.Replace("-", "").Replace(" ", "");
                            revistaDePatente.NumeroProcessoDaPatente = numeroFiltrado.Replace("-", "").Replace(" ", "");

                            if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                                revistaDePatente.NaturezaDoDocumento = numeroFiltrado.Substring(2, 2);
                        }
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
                    revistaDePatente.NumeroDoProcessoFormatado = revistaDePatente.NumeroProcessoDaPatente;

                    if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                        revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroProcessoDaPatente;
                }
                else if (!string.IsNullOrEmpty(revistaDePatente.NumeroDoPedido) && revistaDePatente.NumeroDoPedido.Length > 10)
                {
                    revistaDePatente.NumeroDoProcessoFormatado = revistaDePatente.NumeroDoPedido;

                    if (revistaDePatente.NumeroDoPedido.StartsWith("BR"))
                    {
                        if (revistaDePatente.NumeroDoPedido.Length <= 19)
                        {
                            revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroDoPedido.Replace("-", "").Replace(" ", "");
                            revistaDePatente.NumeroDoPedido = revistaDePatente.NumeroDoPedido.Replace("-", "").Replace(" ", "");

                            if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                                revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroDoPedido.Substring(2, 2);
                        }
                        else
                        {
                            string numeroFiltrado = revistaDePatente.NumeroDoPedido.Substring(0, 19);

                            revistaDePatente.NumeroDoProcesso = numeroFiltrado.Replace("-", "").Replace(" ", "");
                            revistaDePatente.NumeroProcessoDaPatente = numeroFiltrado.Replace("-", "").Replace(" ", "");
                            revistaDePatente.NumeroDoPedido = numeroFiltrado.Replace("-", "").Replace(" ", "");

                            if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                                revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroDoProcesso.Substring(2, 2);
                        }
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
                    if (!string.IsNullOrEmpty(revistaDePatente.NumeroProcessoDaPatente))
                    {
                        revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroProcessoDaPatente;
                        revistaDePatente.NumeroDoPedido = revistaDePatente.NumeroProcessoDaPatente;
                        revistaDePatente.NumeroDoProcessoFormatado = revistaDePatente.NumeroProcessoDaPatente;

                        if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                            revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroProcessoDaPatente;
                    }
                    else
                    {
                        revistaDePatente.NumeroDoProcesso = revistaDePatente.NumeroDoPedido;
                        revistaDePatente.NumeroDoPedido = revistaDePatente.NumeroDoPedido;
                        revistaDePatente.NumeroDoProcessoFormatado = revistaDePatente.NumeroDoPedido;

                        if (string.IsNullOrEmpty(revistaDePatente.NaturezaDoDocumento))
                            revistaDePatente.NaturezaDoDocumento = revistaDePatente.NumeroDoPedido;    
                    }
                    
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
                
                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NumeroDoProcesso) && processo.NumeroDoProcesso != null
                    && processo.NumeroDoProcesso.ToUpper().Contains(filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NumeroDoProcesso].ToUpper()))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.CodigoDoRegistro) && !string.IsNullOrEmpty(processo.CodigoDoDespacho)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.CodigoDoRegistro].Equals(processo.CodigoDoDespacho, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NumeroDaPatente) && !string.IsNullOrEmpty(processo.NumeroProcessoDaPatente)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NumeroDaPatente].Equals(processo.NumeroProcessoDaPatente, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NumeroDoPedido) && !string.IsNullOrEmpty(processo.NumeroDoPedido)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NumeroDoPedido].Equals(processo.NumeroDoPedido, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.DataDoDeposito) && processo.DataDeDeposito.HasValue
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.DataDoDeposito].Equals(processo.DataDeDeposito.Value.ToString("dd/MM/yyyy")))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.PrioridadeUnionista) && !string.IsNullOrEmpty(processo.PrioridadeUnionista)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.PrioridadeUnionista].Equals(processo.PrioridadeUnionista, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.DataDaPublicacao) && processo.DataPublicacao.HasValue
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.DataDaPublicacao].Equals(processo.DataPublicacao.Value.ToString("dd/MM/yyyy")))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.DataDeConcenssao) && processo.DataDeConcessao.HasValue
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.DataDeConcenssao].Equals(processo.DataDeConcessao.Value.ToString("dd/MM/yyyy")))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.ClassificacaoInternacional) && !string.IsNullOrEmpty(processo.ClassificacaoInternacional)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.ClassificacaoInternacional].Equals(processo.ClassificacaoInternacional, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.Titulo) && !string.IsNullOrEmpty(processo.Titulo)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.Titulo].Equals(processo.Titulo, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.Resumo) && !string.IsNullOrEmpty(processo.Resumo)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.Resumo].Equals(processo.Resumo, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.PatentePrincipalAdicao) && processo.NumeroDoProcesso != null
                    && processo.NumeroDoProcesso.ToUpper().Contains(filtro.ValoresDoFiltro[EnumeradorFiltroPatente.PatentePrincipalAdicao].ToUpper()))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.PatentePrincipalDivisao) && processo.NumeroDoProcesso != null
                    && processo.NumeroDoProcesso.ToUpper().Contains(filtro.ValoresDoFiltro[EnumeradorFiltroPatente.PatentePrincipalDivisao].ToUpper()))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.PrioridadeInterna) && !string.IsNullOrEmpty(processo.PrioridadeInterna)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.PrioridadeInterna].Equals(processo.PrioridadeInterna, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NomeDoDepositante) && !string.IsNullOrEmpty(processo.Depositante)
                    && processo.Depositante.ToUpper().Contains(filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NomeDoDepositante].ToUpper()))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NomeDoInventor) && !string.IsNullOrEmpty(processo.Inventor)
                    && processo.Inventor.ToUpper().Contains(filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NomeDoInventor].ToUpper()))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NomeDoTitular) && !string.IsNullOrEmpty(processo.Titular)
                    && processo.Titular.ToUpper().Contains(filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NomeDoTitular].ToUpper()))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NomeDoProcurador) && !string.IsNullOrEmpty(processo.Procurador)
                    && processo.Procurador.ToUpper().Contains(filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NomeDoProcurador].ToUpper()))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.PaisesDesignados) && !string.IsNullOrEmpty(processo.PaisesDesignados)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.PaisesDesignados].Equals(processo.PaisesDesignados, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.DataDeInicioFaseNacional) && processo.DataInicioFaseNacional.HasValue
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.DataDeInicioFaseNacional].Equals(processo.DataInicioFaseNacional.Value.ToString("dd/MM/yyyy")))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NumeroIdiomaDataDepositoInternacional) && !string.IsNullOrEmpty(processo.DadosDepositoInternacional)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NumeroIdiomaDataDepositoInternacional].Equals(processo.DadosDepositoInternacional, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.NumeroIdiomaDataPublicacaoInternacional) && !string.IsNullOrEmpty(processo.DadosPublicacaoInternacional)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.NumeroIdiomaDataPublicacaoInternacional].Equals(processo.DadosPublicacaoInternacional, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.Rp) && !string.IsNullOrEmpty(processo.ResponsavelPagamentoImpostoDeRenda)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.Rp].Equals(processo.ResponsavelPagamentoImpostoDeRenda, StringComparison.InvariantCultureIgnoreCase))
                    deveAdicionarProcesso = true;

                if (filtro.ValoresDoFiltro.Keys.Contains(EnumeradorFiltroPatente.Estado) && !string.IsNullOrEmpty(processo.UFTitular)
                    && filtro.ValoresDoFiltro[EnumeradorFiltroPatente.Estado].Equals(processo.UFTitular, StringComparison.InvariantCultureIgnoreCase))
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


        public IList<IRevistaDePatente> ObtenhaRevistasProcessadas(int numeroDaRevistaDePatente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDeRevistaDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDePatente>();

            try
            {
                return mapeadorDeRevistaDePatente.ObtenhaRevistasProcessadas(numeroDaRevistaDePatente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IList<IRevistaDePatente> ObtenhaPublicacoes(string numeroDoProcesso)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDeRevistaDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDePatente>();

            try
            {
                return mapeadorDeRevistaDePatente.ObtenhaPublicacoes(numeroDoProcesso);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }    
        }
    }
}
