using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using Compartilhados.Fabricas;
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

            if (dadosDaRevistaDePatente.Count > 0 && dadosDaRevistaDePatente[0] != null && dadosDaRevistaDePatente[0].Attributes != null &&
                dadosDaRevistaDePatente[0].Attributes.GetNamedItem("numero") != null && dadosDaRevistaDePatente[0].Attributes.GetNamedItem("data") != null)
            {
                numeroRevista = dadosDaRevistaDePatente[0].Attributes.GetNamedItem("numero").Value;
                dataRevista = dadosDaRevistaDePatente[0].Attributes.GetNamedItem("data").Value;
            }

            foreach (XmlNode processo in processosDaRevista)
            {
                var revistaDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDePatente>();

                revistaDePatente.NumeroProcessoDaPatente = Convert.ToInt64(processo.Attributes.GetNamedItem("numero").Value);

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
    }
}
