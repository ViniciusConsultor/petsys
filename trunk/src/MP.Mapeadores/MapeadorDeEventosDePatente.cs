// -----------------------------------------------------------------------
// <copyright file="MapeadorDeEventosDePatente.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MapeadorDeEventosDePatente : IMapeadorDeEventosDePatente
    {
        public IList<IEvento> ObtenhaEventos(long idDaPatente)
        {
            var sql = new StringBuilder();
            var eventos = new List<IEvento>();

            sql.Append("SELECT IDDAPATENTE, DATA, DESCRICAO FROM MP_PATENTEEVENTO WHERE IDDAPATENTE = " + idDaPatente);
            var DBHelper = ServerUtils.criarNovoDbHelper();

            using (var reader = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (reader.Read())
                {
                    var evento = FabricaGenerica.GetInstancia().CrieObjeto<IEvento>();

                    evento.Data = UtilidadesDePersistencia.getValorDate(reader, "DATA").Value;
                    evento.Descricao = UtilidadesDePersistencia.GetValorString(reader, "DESCRICAO");
                    eventos.Add(evento);
                }
            }

            return eventos;
        }

        public void AtualizeEventos(IPatente patente)
        {
            RemovaEventos(patente.Identificador);

            var sql = new StringBuilder();
            var DBHelper = ServerUtils.getDBHelper();

            if (patente.Eventos != null)
                foreach (var evento in patente.Eventos)
                {
                    sql.Clear();
                    sql.Append("INSERT INTO MP_PATENTEEVENTO (IDDAPATENTE, DATA, DESCRICAO) VALUES (");
                    sql.Append(patente.Identificador + ", ");
                    sql.Append(evento.Data.ToString("yyyyMMdd") + ", ");
                    sql.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(evento.Descricao) + "')");
                    DBHelper.ExecuteNonQuery(sql.ToString());
                }

        }

        public void RemovaEventos(long idDaPatente)
        {
            var sql = new StringBuilder();
            var DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_PATENTEEVENTO WHERE IDDAPATENTE = " + idDaPatente);

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
