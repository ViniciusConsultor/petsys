using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Core.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;

namespace MP.Negocio.Filtros.Marcas
{
    public class FiltroRelatorioDeManutencoesDaMarca : Filtro, IFiltroRelatorioDeManutencoesDaMarca
    {
        public IList<ICliente> ClientesSelecionados { get; set; }
        public DateTime? DataDeInicio { get; set; }
        public DateTime? DataTermino { get; set; }

        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT MP_PROCESSOMARCA.IDPROCESSO, MP_PROCESSOMARCA.IDMARCA IDDAMARCA, MP_PROCESSOMARCA.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOMARCA.DATADECADASTRO, MP_PROCESSOMARCA.DATACONCESSAO, MP_PROCESSOMARCA.PROCESSOEHTERCEIRO, MP_PROCESSOMARCA.IDDESPACHO,");
            sql.AppendLine("MP_PROCESSOMARCA.IDPROCURADOR, MP_PROCESSOMARCA.DATADODEPOSITO, MP_PROCESSOMARCA.TXTCOMPLDESPACHO, ");
            sql.AppendLine("MP_PROCESSOMARCA.APOSTILA, MP_PROCESSOMARCA.ATIVO, ");
            sql.AppendLine("MP_MARCAS.IDMARCA, MP_MARCAS.CODIGONCL, MP_MARCAS.CODIGOAPRESENTACAO, MP_MARCAS.IDCLIENTE, MP_MARCAS.CODIGONATUREZA");
            sql.AppendLine(" FROM MP_PROCESSOMARCA, MP_MARCAS");

            if (ClientesSelecionados != null && ClientesSelecionados.Count > 0)
            {
                string condicao = ClientesSelecionados.Aggregate(string.Empty, (current, cliente) => current + (cliente.Pessoa.ID + ","));

                sql.AppendLine(" INNER JOIN NCL_CLIENTE ON NCL_CLIENTE.IDPESSOA = MP_MARCAS.IDCLIENTE ");
                sql.AppendLine(" AND MP_MARCAS.IDCLIENTE IN(" + condicao.Remove(condicao.Length - 1) + ") ");
            }

            sql.AppendLine(" WHERE MP_PROCESSOMARCA.IDMARCA = MP_MARCAS.IDMARCA ");
            sql.AppendLine(" AND MP_MARCAS.PAGAMANUTENCAO = 1 ");

            if (DataDeInicio.HasValue && DataTermino.HasValue)
                sql.AppendLine(" AND MP_MARCAS.DATAPROXIMAMANUTENCAO BETWEEN " + string.Concat(DataDeInicio.Value.ToString("yyyyMMdd"), " AND ",
                    DataTermino.Value.ToString("yyyyMMdd")));

            return sql.ToString();
        }
    }
}
