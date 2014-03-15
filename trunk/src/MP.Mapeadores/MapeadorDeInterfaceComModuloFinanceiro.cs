using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.FN.Negocio;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeInterfaceComModuloFinanceiro : IMapeadorDeInterfaceComModuloFinanceiro
    {
        public void ItemDeRecebimentoFoiModificado(IItemLancamentoFinanceiroRecebimento itemLancamentoFinanceiro)
        {
            if (itemLancamentoFinanceiro.Situacao.Equals(Situacao.Cancelada) || itemLancamentoFinanceiro.Situacao.Equals(Situacao.Paga))
            {
                string conceito = null;
                long? idDoConceito = null;

                var sql = "SELECT CONCEITO, IDCONCEITO FROM MP_INTERFACEFN WHERE IDITEMRECEBIMENTO = " +
                             itemLancamentoFinanceiro.ID.Value;

                IDBHelper DBHelper;

                DBHelper = ServerUtils.criarNovoDbHelper();

                using (var leitor = DBHelper.obtenhaReader(sql))
                {
                    if (leitor.Read())
                    {
                        conceito = UtilidadesDePersistencia.GetValorString(leitor, "CONCEITO");
                        idDoConceito = UtilidadesDePersistencia.GetValorLong(leitor, "IDCONCEITO");
                    }
                }

                if (!string.IsNullOrEmpty(conceito))
                {
                    if (conceito.Equals("MARCA", StringComparison.InvariantCultureIgnoreCase))
                        TrateNovaManutencaoParaMarca(idDoConceito.Value);


                    Exclua(itemLancamentoFinanceiro.ID.Value);
                }
            }
        }

        private void TrateNovaManutencaoParaMarca(long idMarca)
        {
            IMarcas marca = null;

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            marca = mapeador.obtenhaMarcasPeloId(idMarca);

            if (marca.Manutencao != null)
            {
                marca.Manutencao.DataDaProximaManutencao = marca.Manutencao.ObtenhaProximaDataDeManutencao();
                mapeador.Modificar(marca);
            }

        }

        private void Exclua(long idItemRecebimento)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            DBHelper.ExecuteNonQuery("DELETE FROM MP_INTERFACEFN WHERE IDITEMRECEBIMENTO = " + idItemRecebimento);
        }

        public void Insira(long idItemRecebimento, string Conceito, long idConceito, DateTime dataVencimento)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("INSERT INTO MP_INTERFACEFN (");
            sql.Append("IDITEMRECEBIMENTO, CONCEITO, IDCONCEITO, DATAVENCIMENTO)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(idItemRecebimento, ", "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Conceito), "', "));
            sql.Append(String.Concat(idConceito, ", "));
            sql.Append(String.Concat(dataVencimento.ToString("yyyyMMdd"), ") "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
