// -----------------------------------------------------------------------
// <copyright file="MapeadorDeEventosDeMarca.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using Compartilhados;
using Compartilhados.DBHelper;
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
    public class MapeadorDeEventosDeMarca : IMapeadorDeEventosDeMarca
    {
        public IList<IEvento> ObtenhaEventos(long idDaMarca)
        {
            var sql = new StringBuilder();
            var eventos = new List<IEvento>();

            sql.Append("SELECT IDDAMARCA, DATA, DESCRICAO FROM MP_MARCAEVENTO WHERE IDDAMARCA = " + idDaMarca);
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

        public void AtualizeEventos(IMarcas marca)
        {
            RemovaEventos(marca.IdMarca.Value);

            var sql = new StringBuilder();
            var DBHelper = ServerUtils.getDBHelper();

            if (marca.Eventos != null)
                foreach (var evento in marca.Eventos)
                {
                    sql.Clear();
                    sql.Append("INSERT INTO MP_MARCAEVENTO (IDDAMARCA, DATA, DESCRICAO) VALUES (");
                    sql.Append(marca.IdMarca.Value + ", ");
                    sql.Append(evento.Data.ToString("yyyyMMdd") + ", ");
                    sql.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(evento.Descricao) + "')");
                    DBHelper.ExecuteNonQuery(sql.ToString());
                }

        }

        public void RemovaEventos(long idDaMarca)
        {
            var sql = new StringBuilder();
            var DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_MARCAEVENTO WHERE IDDAMARCA = " + idDaMarca);

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
