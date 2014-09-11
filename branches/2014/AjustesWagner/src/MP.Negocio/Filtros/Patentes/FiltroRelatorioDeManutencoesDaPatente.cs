using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    public class FiltroRelatorioDeManutencoesDaPatente : Filtro, IFiltroRelatorioDeManutencoesDaPatente
    {
        public IList<ICliente> ClientesSelecionados { get; set; }
        public DateTime? DataDeInicio { get; set; }
        public DateTime? DataTermino { get; set; }

        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT MP_PROCESSOPATENTE.IDPROCESSOPATENTE, MP_PROCESSOPATENTE.IDPATENTE IDDAPATENTE, MP_PROCESSOPATENTE.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADEPUBLICACAO, MP_PROCESSOPATENTE.DATADEDEPOSITO, MP_PROCESSOPATENTE.DATADECONCESSAO, MP_PROCESSOPATENTE.DATADEEXAME, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADECADASTRO, MP_PROCESSOPATENTE.PROCESSODETERCEIRO, MP_PROCESSOPATENTE.IDPROCURADOR, ");
            sql.AppendLine("MP_PROCESSOPATENTE.EHESTRANGEIRO, MP_PROCESSOPATENTE.NUMEROPCT, MP_PROCESSOPATENTE.NUMEROWO, MP_PROCESSOPATENTE.DATAPUBLICACAOPCT, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADEPOSITOPCT, MP_PROCESSOPATENTE.IDDESPACHO, MP_PROCESSOPATENTE.ATIVO, MP_PROCESSOPATENTE.IDPASTA, ");
            sql.AppendLine("MP_PROCESSOPATENTE.PAIS, MP_PASTA.NOME NOMEPASTA,  MP_PASTA.CODIGO CODIGOPASTA, MP_PATENTE.IDPATENTE ");
            sql.AppendLine(" FROM MP_PROCESSOPATENTE");
            sql.AppendLine(" INNER JOIN MP_PATENTE ON MP_PATENTE.IDPATENTE = MP_PROCESSOPATENTE.IDPATENTE");
            sql.AppendLine(" AND MP_PATENTE.PAGAMANUTENCAO = 1 ");
                
            if(DataDeInicio.HasValue && DataTermino.HasValue)
                sql.AppendLine(" AND MP_PATENTE.DATAPROXIMAMANUTENCAO BETWEEN " + string.Concat(DataDeInicio.Value.ToString("yyyyMMdd"), " AND ",
                    DataTermino.Value.ToString("yyyyMMdd")));

            if(ClientesSelecionados != null && ClientesSelecionados.Count > 0)
            {
                string condicao = ClientesSelecionados.Aggregate(string.Empty, (current, cliente) => current + (cliente.Pessoa.ID + ","));

                sql.AppendLine(" INNER JOIN MP_PATENTECLIENTE ON MP_PATENTECLIENTE.IDPATENTE = MP_PROCESSOPATENTE.IDPATENTE ");
                sql.AppendLine(" INNER JOIN NCL_CLIENTE ON NCL_CLIENTE.IDPESSOA = MP_PATENTECLIENTE.IDCLIENTE ");
                sql.AppendLine(" AND MP_PATENTECLIENTE.IDCLIENTE IN(" + condicao.Remove(condicao.Length - 1) + ") ");
            }

            sql.AppendLine(" LEFT JOIN MP_PASTA ON MP_PASTA.ID = MP_PROCESSOPATENTE.IDPASTA");

            return sql.ToString();
        }
    }
}
