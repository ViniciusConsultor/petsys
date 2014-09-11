using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Negocio;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

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
                    else
                        TrateNovaManutencaoParaProcessoDePatente(idDoConceito.Value);

                    Exclua(itemLancamentoFinanceiro.ID.Value);
                }
            }
        }

        private void TrateNovaManutencaoParaMarca(long idProcessoDeMarca)
        {
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

            var processoDeMarca = mapeador.Obtenha(idProcessoDeMarca);

            if (processoDeMarca != null)
            {
                var marca = processoDeMarca.Marca;

                if (marca.Manutencao != null)
                {
                    marca.Manutencao.DataDaProximaManutencao = marca.Manutencao.ObtenhaProximaDataDeManutencao();

                    var mapeadorDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();
                    mapeadorDeMarca.Modificar(marca);
                }
            }

        }

        private void TrateNovaManutencaoParaProcessoDePatente(long idProcessoDePatente)
        {
            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDePatente>();
            var processoDePatente = mapeador.Obtenha(idProcessoDePatente);

            if (processoDePatente != null)
            {
                var patente = processoDePatente.Patente;

                if (patente.Manutencao != null)
                {
                    patente.Manutencao.DataDaProximaManutencao = patente.Manutencao.ObtenhaProximaDataDeManutencao();
                    var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

                    mapeadorDePatente.Modificar(patente);    
                }
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
