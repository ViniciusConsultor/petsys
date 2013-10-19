using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class SituacaoDoProcessoDePatente
    {
        private string _codigoSituacaoProcessoDePatente;
        private string _descricaoSituacaoProcessoDePatente;

        // Classificação da situação do processo de patente

        public static SituacaoDoProcessoDePatente PedidoInternacional = new SituacaoDoProcessoDePatente("1", "Pedido Internacional PCT/BR Designado ou Eleito");
        public static SituacaoDoProcessoDePatente Deposito = new SituacaoDoProcessoDePatente("2", "Depósito");
        public static SituacaoDoProcessoDePatente PublicaçãoDePedido = new SituacaoDoProcessoDePatente("3", "Publicação de Pedido");
        public static SituacaoDoProcessoDePatente PedidoDeExame = new SituacaoDoProcessoDePatente("4", "Pedido de Exame");
        public static SituacaoDoProcessoDePatente ExigenciasTecnicasEFormais = new SituacaoDoProcessoDePatente("6", "Exigências Técnicas e Formais");
        public static SituacaoDoProcessoDePatente CienciaDoParecer = new SituacaoDoProcessoDePatente("7", "Ciência do Parecer");
        public static SituacaoDoProcessoDePatente AnuidadeDoPedido = new SituacaoDoProcessoDePatente("8", "Anuidade do Pedido");
        public static SituacaoDoProcessoDePatente Decisao = new SituacaoDoProcessoDePatente("9", "Decisão");
        public static SituacaoDoProcessoDePatente Desistencia = new SituacaoDoProcessoDePatente("10", "Desistência");
        public static SituacaoDoProcessoDePatente Arquivamento = new SituacaoDoProcessoDePatente("11", "Arquivamento");
        public static SituacaoDoProcessoDePatente Recurso = new SituacaoDoProcessoDePatente("12", "Recurso");
        public static SituacaoDoProcessoDePatente OutrosReferentesAPedidos = new SituacaoDoProcessoDePatente("15", "Outros Referentes a Pedidos");
        public static SituacaoDoProcessoDePatente ConcessaoDePatenteOuCertificadoDeAdicaoDeInvencao = new SituacaoDoProcessoDePatente("16", "Concessão de Patente ou Certificado de Adição de Invenção");
        public static SituacaoDoProcessoDePatente NulidadeAdministrativa = new SituacaoDoProcessoDePatente("17", "Nulidade Administrativa");
        public static SituacaoDoProcessoDePatente Caducidade = new SituacaoDoProcessoDePatente("18", "Caducidade");
        public static SituacaoDoProcessoDePatente NotificacaoDeDecisaoJudicial = new SituacaoDoProcessoDePatente("19", "Notificação de Decisão Judicial");
        public static SituacaoDoProcessoDePatente ExtincaoDePatenteECertificadoDeAdicaoDeInvencao = new SituacaoDoProcessoDePatente("21", "Extinção de Patente e Certificado de Adição de Invenção");
        public static SituacaoDoProcessoDePatente OutrosReferentesAPatentesECertificadosDeAdicaoDeInvencao = new SituacaoDoProcessoDePatente("22", "Outros Referentes a Patentes e Certificados de Adição de Invenção");
        public static SituacaoDoProcessoDePatente ProcessamentoDePedidosSegundoArtigos230E231 = new SituacaoDoProcessoDePatente("23", "Processamento de Pedidos Segundo Artigos 230 e 231 da Lei 9279/96");
        public static SituacaoDoProcessoDePatente AnuidadeDePatente = new SituacaoDoProcessoDePatente("24", "Anuidade de Patente");
        public static SituacaoDoProcessoDePatente AnotacaoDeAlteracaoDeNomeEOuSede = new SituacaoDoProcessoDePatente("PR", "INPI - Presidência");
        public static SituacaoDoProcessoDePatente Outros = new SituacaoDoProcessoDePatente("OU", "Outros");
        public static SituacaoDoProcessoDePatente DesenhoIndustrial = new SituacaoDoProcessoDePatente("DI", "Desenho Industrial");


        private static IList<SituacaoDoProcessoDePatente> SituacoesDoProcessoDePatente = new List
            <SituacaoDoProcessoDePatente>()
                                                                                             {
                                                                                                 PedidoInternacional,
                                                                                                 Deposito,
                                                                                                 PublicaçãoDePedido,
                                                                                                 PedidoDeExame,
                                                                                                 ExigenciasTecnicasEFormais,
                                                                                                 CienciaDoParecer,
                                                                                                 AnuidadeDoPedido,
                                                                                                 Decisao,
                                                                                                 Desistencia,
                                                                                                 Arquivamento,
                                                                                                 Recurso,
                                                                                                 OutrosReferentesAPedidos,
                                                                                                 ConcessaoDePatenteOuCertificadoDeAdicaoDeInvencao,
                                                                                                 NulidadeAdministrativa,
                                                                                                 Caducidade,
                                                                                                 NotificacaoDeDecisaoJudicial,
                                                                                                 ExtincaoDePatenteECertificadoDeAdicaoDeInvencao,
                                                                                                 OutrosReferentesAPatentesECertificadosDeAdicaoDeInvencao,
                                                                                                 ProcessamentoDePedidosSegundoArtigos230E231,
                                                                                                 AnuidadeDePatente,
                                                                                                 AnotacaoDeAlteracaoDeNomeEOuSede,
                                                                                                 Outros,
                                                                                                 DesenhoIndustrial
                                                                                             };

        private SituacaoDoProcessoDePatente(string codigoSituacaoDeProcesso, string descricao)
        {
            CodigoSituacaoProcessoDePatente = codigoSituacaoDeProcesso;
            DescricaoSituacaoProcessoDePatente = descricao;
        }

        public string CodigoSituacaoProcessoDePatente
        {
            get { return _codigoSituacaoProcessoDePatente; }
            private set { _codigoSituacaoProcessoDePatente = value; }
        }

        public string DescricaoSituacaoProcessoDePatente
        {
            get { return _descricaoSituacaoProcessoDePatente; }
            private set { _descricaoSituacaoProcessoDePatente = value; }
        }

        public static IList<SituacaoDoProcessoDePatente> ObtenhaSituacoesDoProcessoDePatente()
        {
            return SituacoesDoProcessoDePatente;
        }

        public static SituacaoDoProcessoDePatente ObtenhaPorCodigo(string codigo)
        {
            return ObtenhaSituacoesDoProcessoDePatente().FirstOrDefault(situacao => situacao.CodigoSituacaoProcessoDePatente.Equals(codigo));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as SituacaoDoProcessoDePatente;

            return objeto != null && objeto.CodigoSituacaoProcessoDePatente == CodigoSituacaoProcessoDePatente;
        }

        public override int GetHashCode()
        {
            return _codigoSituacaoProcessoDePatente.GetHashCode();
        }
    }
}
