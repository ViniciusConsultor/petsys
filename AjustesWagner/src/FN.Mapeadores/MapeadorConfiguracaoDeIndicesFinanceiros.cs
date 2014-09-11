using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.FN.Negocio;
using FN.Interfaces.Mapeadores;

namespace FN.Mapeadores
{
    public class MapeadorConfiguracaoDeIndicesFinanceiros : IMapeadorConfiguracaoDeIndicesFinanceiros
    {
        public IConfiguracaoDeIndicesFinanceiros ObtenhaConfiguracao()
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            IConfiguracaoDeIndicesFinanceiros configuracao = null;
            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT VALORSALMIN ");
            sql.Append("FROM FN_CNFINDFINAN ");

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                try
                {
                    if (leitor.Read())
                    {
                        configuracao = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeIndicesFinanceiros>();

                        if (!UtilidadesDePersistencia.EhNulo(leitor, "VALORSALMIN"))
                            configuracao.ValorDoSalarioMinimo = UtilidadesDePersistencia.getValorDouble(leitor, "VALORSALMIN");
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return configuracao;       
        }

        public void Salve(IConfiguracaoDeIndicesFinanceiros configuracao)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            DBHelper.ExecuteNonQuery("DELETE FROM FN_CNFINDFINAN");

            if (configuracao == null) return;

            sql.Append("INSERT INTO FN_CNFINDFINAN (");
            sql.Append("VALORSALMIN)");
            sql.Append("VALUES (");

            if (configuracao == null ||  !configuracao.ValorDoSalarioMinimo.HasValue )
                sql.Append("NULL) ");
            else
                sql.Append(string.Concat(UtilidadesDePersistencia.TPVd(configuracao.ValorDoSalarioMinimo.Value) , ") "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
