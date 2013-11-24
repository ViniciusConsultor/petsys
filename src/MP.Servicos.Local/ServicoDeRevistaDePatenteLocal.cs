using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using MP.Interfaces.Negocio;
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
        }

        public void Modificar(IRevistaDePatente revistaDeMarcas)
        {
        }

        public IList<IRevistaDePatente> ObtenhaRevistasAProcessar(int quantidadeDeRegistros)
        {
            return new List<IRevistaDePatente>();
        }

        public IList<IRevistaDePatente> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros)
        {
            return new List<IRevistaDePatente>();
        }


        public IList<IRevistaDePatente> ObtenhaProcessosExistentesDeAcordoComARevistaXml(IRevistaDePatente revistaDePatentes, XmlDocument revistaXml)
        {
            return LeiaRevistaXMLEPreenchaProcessosExistentes(revistaDePatentes, revistaXml);
        }

        private IList<IRevistaDePatente> LeiaRevistaXMLEPreenchaProcessosExistentes(IRevistaDePatente revistaDePatentes, XmlDocument revistaXml)
        {
            var numeroRevista = string.Empty;
            var dataRevista = string.Empty;
            IList<IRevistaDePatente> listaDeRevistasDePatentes = new List<IRevistaDePatente>();
            XmlNodeList dadosDaRevistaDePatente = revistaXml.GetElementsByTagName("revista");
            XmlNodeList processosDaRevista = revistaXml.GetElementsByTagName("processo");
            var listaDeRevistasASeremSalvas = new List<IRevistaDePatente>();

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

                listaDeRevistasDePatentes.Add(revistaDePatente);
            }

            if (listaDeRevistasDePatentes.Count > 0)
            {
                IList<long> listaDeNumerosDeProcessosCadastrados = new List<long>();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDePatente>())
                {
                    listaDeNumerosDeProcessosCadastrados = servico.ObtenhaTodosNumerosDeProcessosCadastrados();

                    foreach (IRevistaDePatente processo in listaDeRevistasDePatentes)
                    {
                        if(listaDeNumerosDeProcessosCadastrados.Contains(processo.NumeroDoProcesso))
                        {
                            var revistaASerSalva = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDePatente>();
                            var processoDePatenteExistente = servico.Obtenha(processo.NumeroDoProcesso);

                            if(processoDePatenteExistente.IdProcessoDePatente != null)
                            {
                                revistaASerSalva.IdRevistaPatente = GeradorDeID.getInstancia().getProximoID();

                                if (processo.DataDeConcessao != null)
                                    processoDePatenteExistente.DataDaConcessao = processo.DataDeConcessao;

                                if (processo.DataPublicacao != null)
                                    processoDePatenteExistente.DataDaPublicacao = processo.DataPublicacao;

                                //if (processo.DataPublicacao != null)
                                //    processoDePatenteExistente.DataDaVigencia = processo.DataPublicacao;

                                if (processo.DataDeDeposito != null)
                                    processoDePatenteExistente.DataDoDeposito = processo.DataDeDeposito;

                                //if (processo.DataPublicacao != null)
                                //  processoDePatenteExistente.DataDoExame = processo.DataPublicacao;

                                if (processo.CodigoDoDespacho != null)
                                {
                                    revistaASerSalva.CodigoDoDespachoAnterior = processoDePatenteExistente.Despacho == null
                                                                                    ? null : processoDePatenteExistente.Despacho.Codigo;

                                    AtualizeDespachoNoProcesso(processo.CodigoDoDespacho, processoDePatenteExistente);
                                }

                                //if (string.IsNullOrEmpty(processo.Procurador))
                                //    processoDePatenteExistente.Procurador = processo.Procurador;

                                //if (processo.DataPublicacao != null)
                                //    processoDePatenteExistente.PCT = processo.NumeroProcessoDaPatente;

                                processoDePatenteExistente.ProcessoEhEstrangeiro = string.IsNullOrEmpty(processo.ClassificacaoInternacional);

                                listaDeRevistasASeremSalvas.Add(revistaASerSalva);

                                servico.Modificar(processoDePatenteExistente);
                            }
                        }
                    }
                }
            }

            return listaDeRevistasDePatentes;
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
                    revistaDePatente.TipoDePrograma = processo["titulares"]["titular"].Attributes.GetNamedItem("nome-razao-social").Value;

                if (processo["titulares"]["titular"].Attributes.GetNamedItem("pais") != null)
                    revistaDePatente.TipoDePrograma = processo["titulares"]["titular"].Attributes.GetNamedItem("pais").Value;

                if (processo["titulares"]["titular"].Attributes.GetNamedItem("uf") != null)
                    revistaDePatente.TipoDePrograma = processo["titulares"]["titular"].Attributes.GetNamedItem("uf").Value;
            }
        }

        private DateTime? VerifiqueERetorneValorDataDoNo(XmlNode xmlNode, string tagDoNo)
        {
            return xmlNode.Attributes.GetNamedItem(tagDoNo) != null ? Convert.ToDateTime(xmlNode.Attributes.GetNamedItem(tagDoNo).Value) : (DateTime?)null;
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
                    revistaDePatente.Titulo = processo["patente"]["titulo"].Value;

                if (processo["patente"]["dadosDaPatente"] != null)
                    revistaDePatente.DadosDoPedidoOriginal = processo["patente"]["dadosDaPatente"].Value;

                if (processo["patente"]["natureza"] != null)
                    revistaDePatente.NaturezaDoDocumento = processo["patente"]["natureza"].Value;

                if (processo["patente"]["observacao"] != null)
                    revistaDePatente.Observacao = processo["patente"]["observacao"].Value;

                if (processo["patente"]["resumo"] != null)
                    revistaDePatente.Resumo = processo["patente"]["resumo"].Value;

                if (processo["patente"]["inventores"] != null)
                    revistaDePatente.Inventor = processo["patente"]["inventores"].Value;

                if (processo["patente"]["classificacaoInternacional"] != null)
                    revistaDePatente.ClassificacaoInternacional = processo["patente"]["classificacaoInternacional"].Value;

                if (processo["patente"]["classificacaoNacional"] != null)
                    revistaDePatente.ClassificacaoNacional = processo["patente"]["classificacaoNacional"].Value;

                if (processo["patente"]["adicaoPatente"] != null)
                    revistaDePatente.DadosDoPedidoDaPatente = processo["patente"]["adicaoPatente"].Value;

                if (processo["patente"]["adicaoPatente"] != null)
                    revistaDePatente.DadosDoPedidoDaPatente = processo["patente"]["adicaoPatente"].Value;

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
            if (xmlNode["patente"] == null) return;

            long numeroDoPedido;

            long.TryParse(xmlNode["patente"].Value, out numeroDoPedido);
            revistaDePatente.NumeroDoPedido = numeroDoPedido;

            revistaDePatente.DataPublicacao = VerifiqueERetorneValorDataDoNo(xmlNode, "data-publicacao");
        }

        private void PreenchaPrioridadeUnionista(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["prioridadeUnionista"] == null) return;

            revistaDePatente.PrioridadeUnionista = xmlNode["prioridadeUnionista"].Value;
        }

        private void PreenchaDepositante(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["depositante"] == null) return;

            revistaDePatente.Depositante = xmlNode["depositante"].Value;
        }

        private void PreenchaProcurador(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["procurador"] == null) return;

            revistaDePatente.Procurador = xmlNode["procurador"].Value;
        }

        private void PreenchaPaisesDesignados(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisesDesignados"] == null) return;

            revistaDePatente.PaisesDesignados = xmlNode["paisesDesignados"].Value;
        }

        private void PreenchaDadosDepositoInternacional(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisesDepositoInternacional"] == null) return;

            revistaDePatente.DadosDepositoInternacional = xmlNode["paisesDepositoInternacional"].Value;
        }

        private void PreenchaDadosPublicacaoInternacional(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisesPublicacaoInternacional"] == null) return;

            revistaDePatente.DadosPublicacaoInternacional = xmlNode["paisesPublicacaoInternacional"].Value;
        }

        private void PreenchaResponsavelIR(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["responsavelIR"] == null) return;

            revistaDePatente.ResponsavelPagamentoImpostoDeRenda = xmlNode["responsavelIR"].Value;
        }

        private void PreenchaComplemento(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["complemento"] == null) return;

            revistaDePatente.ResponsavelPagamentoImpostoDeRenda = xmlNode["complemento"].Value;
        }

        private void PreenchaDecisao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["decisao"] == null) return;

            revistaDePatente.Decisao = xmlNode["decisao"].Value;
        }

        private void PreenchaRecorrente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["recorrente"] == null) return;

            revistaDePatente.Recorrente= xmlNode["recorrente"].Value;
        }

        private void PreenchaCedente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["cedente"] == null) return;

            revistaDePatente.Recorrente = xmlNode["cedente"].Value;
        }

        private void PreenchaCessionaria(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["cessionaria"] == null) return;

            revistaDePatente.Recorrente = xmlNode["cessionaria"].Value;
        }

        private void PreenchaObservacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["observacao"] == null) return;

            revistaDePatente.Recorrente = xmlNode["observacao"].Value;
        }

        private void PreenchaUltimaInformacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["ultimaInformacao"] == null) return;

            revistaDePatente.Recorrente = xmlNode["ultimaInformacao"].Value;
        }

        private void PreenchaCertificadoAverbacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["certificadoAverbacao"] == null) return;

            revistaDePatente.Recorrente = xmlNode["certificadoAverbacao"].Value;
        }

        private void PreenchaPaisCedente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisCedente"] == null) return;

            revistaDePatente.Recorrente = xmlNode["paisCedente"].Value;
        }

        private void PreenchaPaisCessionaria(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["paisCessionaria"] == null) return;

            revistaDePatente.Recorrente = xmlNode["paisCessionaria"].Value;
            revistaDePatente.EnderecoDaCessionaria = VerifiqueERetorneValorStringDoNo(xmlNode["paisCessionaria"], "endereco");
        }

        private void PreenchaSetor(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["setor"] == null) return;

            revistaDePatente.Recorrente = xmlNode["setor"].Value;
        }

        private void PreenchaInsentosDeAverbacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["isentosAverbacao"] == null) return;

            revistaDePatente.Recorrente = xmlNode["isentosAverbacao"].Value;
        }

        private void PreenchaRegimeDeGuarda(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["regimeGuarda"] == null) return;

            revistaDePatente.RegimeDeGuarda = xmlNode["regimeGuarda"].Value;
        }

        private void PreenchaRequerente(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["requerente"] == null) return;

            revistaDePatente.Requerente = xmlNode["requerente"].Value;
        }

        private void PreenchaRedacao(XmlNode xmlNode, IRevistaDePatente revistaDePatente)
        {
            if (xmlNode["redacao"] == null) return;

            revistaDePatente.Redacao = xmlNode["redacao"].Value;
        }

        private void AtualizeDespachoNoProcesso(string codigoDoDespacho, IProcessoDePatente processoDePatente)
        {
            if (string.IsNullOrEmpty(codigoDoDespacho)) return;

            IDespachoDePatentes despacho;

            using (var servicoDespacho = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                despacho = servicoDespacho.obtenhaDespachoDePatentesPeloId(Convert.ToInt64(codigoDoDespacho));

            if (despacho != null)
                processoDePatente.Despacho = despacho;
        }
    }
}
