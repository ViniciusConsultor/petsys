using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Interfaces;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeRevistaDePatente : IMapeadorDeRevistaDePatente
    {
        public void InserirDadosRevistaXml(IList<IRevistaDePatente> listaDeProcessosExistentesNaRevista)
        {
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            foreach (var processoDaRevistaDePatente in listaDeProcessosExistentesNaRevista)
            {
                var sql = new StringBuilder();
                processoDaRevistaDePatente.IdRevistaPatente = GeradorDeID.getInstancia().getProximoID();

                sql.Append("INSERT INTO MP_REVISTA_PATENTE (");
                sql.Append("IDREVISTAPATENTE, NUMEROREVISTAPATENTE, DATAPUBLICACAO, DATAPROCESSAMENTO, PROCESSADA, EXTENSAOARQUIVO, ");
                sql.Append("DATADODEPOSITO, NUMEROPROCESSOPATENTE, NUMERODOPEDIDO, DATAPUBLICPEDIDO, DATACONCESSAO, PRIORIDADEUNIONISTA, ");
                sql.Append("CLASSIFICACAOINTER, TITULO, RESUMO, DADOSPEDIDOPATENTE, DADOSPATENTEORIGINAL, PRIORIDADEINTERNA, DEPOSITANTE, ");
                sql.Append("INVENTOR, TITULAR, UFTITULAR, PAISTITULAR, PROCURADOR, PAISESDESIGNADOS, DATAINICIOFASENAC, DADOSDEPOSINTER, ");
                sql.Append("DADOSPUBLICINTER, CODIGODESPACHOANTERIOR, CODIGODESPACHOATUAL, RESPPGTOIMPRENDA, COMPLEMENTO, DECISAO, RECORRENTE, ");
                sql.Append("NUMERODOPROCESSO, CEDENTE, CESSIONARIA, OBSERVACAO, ULTIMAINFORMACAO, CERTIFAVERBACAO, PAISCEDENTE, PAISCESSIONARIA, ");
                sql.Append("SETOR, ENDERECOCESSIONARIA, NATUREZADOCUMENTO, MOEDADEPAGAMENTO, VALOR, PAGAMENTO, PRAZO, SERVISENTOSDEAVERBACAO, ");
                sql.Append("CRIADOR, LINGUAGEM, CAMPOAPLICACAO, TIPODEPROGRAMA, DATADACRIACAO, REGIMEDEGUARDA, REQUERENTE, REDACAO, DATAPRORROGACAO, ");
                sql.Append("CLASSIFICACAONACIONAL) ");
                sql.Append("VALUES (");
                sql.Append(String.Concat(processoDaRevistaDePatente.IdRevistaPatente.Value.ToString(), ", "));
                sql.Append(String.Concat(processoDaRevistaDePatente.NumeroRevistaPatente, ", "));

                sql.Append(processoDaRevistaDePatente.DataPublicacao.HasValue
                          ? String.Concat(processoDaRevistaDePatente.DataPublicacao.Value.ToString("yyyyMMdd"), ", ") : "NULL, ");

                sql.Append(processoDaRevistaDePatente.DataProcessamento.HasValue
                           ? String.Concat(processoDaRevistaDePatente.DataProcessamento.Value.ToString("yyyyMMdd"), ", ") : "NULL, ");

                sql.Append(processoDaRevistaDePatente.Processada ? "1, " : "0, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.ExtensaoArquivo)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.ExtensaoArquivo), "', ") : "NULL, ");

                sql.Append(String.Concat("'", processoDaRevistaDePatente.NumeroProcessoDaPatente, "', "));

                sql.Append(processoDaRevistaDePatente.DataDeDeposito.HasValue
                           ? String.Concat(processoDaRevistaDePatente.DataDeDeposito.Value.ToString("yyyyMMdd"), ", ") : "NULL, ");

                sql.Append(String.Concat(processoDaRevistaDePatente.NumeroDoPedido, ", "));

                sql.Append(processoDaRevistaDePatente.DataDaPublicacaoDoPedido.HasValue
                           ? String.Concat(processoDaRevistaDePatente.DataDaPublicacaoDoPedido.Value.ToString("yyyyMMdd"), ", ") : "NULL, ");

                sql.Append(processoDaRevistaDePatente.DataDeConcessao.HasValue
                           ? String.Concat(processoDaRevistaDePatente.DataDeConcessao.Value.ToString("yyyyMMdd"), ", ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.PrioridadeUnionista)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.PrioridadeUnionista), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.ClassificacaoInternacional)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.ClassificacaoInternacional), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Titulo)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Titulo), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Resumo)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Resumo), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.DadosDoPedidoDaPatente)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.DadosDoPedidoDaPatente), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.DadosDoPedidoOriginal)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.DadosDoPedidoOriginal), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.PrioridadeInterna)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.PrioridadeInterna), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Depositante)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Depositante), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Inventor)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Inventor), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Titular)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Titular), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.UFTitular)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.UFTitular), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.PaisTitular)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.PaisTitular), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Procurador)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Procurador), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.PaisesDesignados)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.PaisesDesignados), "', ") : "NULL, ");

                sql.Append(processoDaRevistaDePatente.DataInicioFaseNacional.HasValue
                           ? String.Concat(processoDaRevistaDePatente.DataInicioFaseNacional.Value.ToString("yyyyMMdd"), ", ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.DadosDepositoInternacional)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.DadosDepositoInternacional), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.DadosPublicacaoInternacional)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.DadosPublicacaoInternacional), "', ") : "NULL, ");

                sql.Append(processoDaRevistaDePatente.CodigoDoDespachoAnterior != null ? String.Concat("'" + processoDaRevistaDePatente.CodigoDoDespachoAnterior, "', ")
                           : "NULL, ");

                sql.Append(processoDaRevistaDePatente.CodigoDoDespacho != null ? String.Concat("'" + processoDaRevistaDePatente.CodigoDoDespacho, "', ")
                           : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.ResponsavelPagamentoImpostoDeRenda)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.ResponsavelPagamentoImpostoDeRenda), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Complemento)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Complemento), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Decisao)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Decisao), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Recorrente)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Recorrente), "', ") : "NULL, ");

                sql.Append(String.Concat(processoDaRevistaDePatente.NumeroDoProcesso, ", "));

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Cedente)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Cedente), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Cessionaria)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Cessionaria), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Observacao)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Observacao), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.UltimaInformacao)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.UltimaInformacao), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.CertificadoDeAverbacao)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.CertificadoDeAverbacao), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.PaisCedente)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.PaisCedente), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.PaisDaCessionaria)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.PaisDaCessionaria), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Setor)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Setor), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.EnderecoDaCessionaria)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.EnderecoDaCessionaria), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.NaturezaDoDocumento)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.NaturezaDoDocumento), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.MoedaDePagamento)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.MoedaDePagamento), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Valor)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Valor), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Pagamento)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Pagamento), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Prazo)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Prazo), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.ServicosIsentosDeAverbacao)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.ServicosIsentosDeAverbacao), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Criador)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Criador), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Linguagem)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Linguagem), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.CampoDeAplicacao)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.CampoDeAplicacao), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.TipoDePrograma)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.TipoDePrograma), "', ") : "NULL, ");

                sql.Append(processoDaRevistaDePatente.DataDaCriacao.HasValue
                           ? String.Concat(processoDaRevistaDePatente.DataDaCriacao.Value.ToString("yyyyMMdd"), ", ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.RegimeDeGuarda)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.RegimeDeGuarda), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Requerente)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Requerente), "', ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.Redacao)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.Redacao), "', ") : "NULL, ");

                sql.Append(processoDaRevistaDePatente.DataDaProrrogacao.HasValue
                           ? String.Concat(processoDaRevistaDePatente.DataDaProrrogacao.Value.ToString("yyyyMMdd"), ", ") : "NULL, ");

                sql.Append(!string.IsNullOrEmpty(processoDaRevistaDePatente.ClassificacaoNacional)
                           ? String.Concat("'" + UtilidadesDePersistencia.FiltraApostrofe(processoDaRevistaDePatente.ClassificacaoNacional), "')") : "NULL)");

                DBHelper.ExecuteNonQuery(sql.ToString());
            }
        }

        public void Modificar(IRevistaDePatente revistaDePatentes)
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
    }
}
