using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeBoletosGeradosAux : IMapeadorDeBoletosGeradosAux
    {
        public IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT ID, PROXNOSSONUMERO, PROXNUMEROBOLETO ");
            sql.Append("FROM MP_BOLETOS_GERADOS_AUX ");

            var dadosAuxiliares = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGeradosAux>();

            var dbHelper = ServerUtils.criarNovoDbHelper();

             using (var leitor = dbHelper.obtenhaReader(sql.ToString()))
             {
                 while (leitor.Read())
                 {
                     dadosAuxiliares.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
                     dadosAuxiliares.ProximoNossoNumero = UtilidadesDePersistencia.GetValorLong(leitor, "PROXNOSSONUMERO");
                     dadosAuxiliares.ProximoNumeroBoleto = UtilidadesDePersistencia.GetValorLong(leitor, "PROXNUMEROBOLETO");
                 }
             }

            return dadosAuxiliares;
        }

        public void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto)
        {
            var sql = new StringBuilder();

            var dbHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_BOLETOS_GERADOS_AUX ");
            sql.Append("SET PROXNOSSONUMERO = " + dadosAuxBoleto.ProximoNossoNumero.Value + ", ");
            sql.Append("PROXNUMEROBOLETO = " + dadosAuxBoleto.ProximoNumeroBoleto.Value + " ");
            sql.Append(" WHERE ID = " + dadosAuxBoleto.ID.Value);

            dbHelper.ExecuteNonQuery(sql.ToString());
        }

        public void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto)
        {
            var sql = new StringBuilder();

            var dbHelper = ServerUtils.getDBHelper();

            sql.Append("INSERT INTO MP_BOLETOS_GERADOS_AUX(ID, PROXNOSSONUMERO, PROXNUMEROBOLETO) ");
            sql.Append("VALUES( ");
            sql.Append(String.Concat(dadosAuxBoleto.ID.Value, ", "));
            sql.Append(String.Concat(dadosAuxBoleto.ProximoNossoNumero.Value, ", "));
            sql.Append(String.Concat(dadosAuxBoleto.ProximoNumeroBoleto.Value, ") "));

            dbHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
