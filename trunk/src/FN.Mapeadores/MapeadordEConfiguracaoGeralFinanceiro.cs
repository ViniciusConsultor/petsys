using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.FN.Negocio;
using FN.Interfaces.Mapeadores;
using FN.Interfaces.Negocio;

namespace FN.Mapeadores
{
    public class MapeadorDeConfiguracaoGeralFinanceiro : IMapeadorDeConfiguracaoGeralFinanceiro
    {
        public void Salve(IConfiguracaoGeralFinanceiro configuracaoGeral)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            DBHelper.ExecuteNonQuery("DELETE FROM FN_CONFIGGERAL");

            if (configuracaoGeral == null) return;

            sql.Append("INSERT INTO FN_CONFIGGERAL(INSTRUCOESDOBOLETO) VALUES (");
            sql.Append(string.IsNullOrEmpty(configuracaoGeral.InstrucoesDoBoleto) ? "NULL)" : "'" + configuracaoGeral.InstrucoesDoBoleto + "')");
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public IConfiguracaoGeralFinanceiro Obtenha()
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            IConfiguracaoGeralFinanceiro configuracaoGeral = null;
            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT INSTRUCOESDOBOLETO ");
            sql.Append("FROM FN_CONFIGGERAL ");

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), int.MaxValue))
            {
                try
                {
                    if (leitor.Read())
                    {
                        configuracaoGeral = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoGeralFinanceiro>();

                        if (!UtilidadesDePersistencia.EhNulo(leitor, "INSTRUCOESDOBOLETO"))
                            configuracaoGeral.InstrucoesDoBoleto = UtilidadesDePersistencia.GetValorString(leitor, "INSTRUCOESDOBOLETO");
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return configuracaoGeral;       
        }
    }
}
