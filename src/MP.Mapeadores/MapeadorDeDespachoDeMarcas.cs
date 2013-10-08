using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeDespachoDeMarcas : IMapeadorDeDespachoDeMarcas
    {
        public IList<IDespachoDeMarcas> obtenhaTodosDespachoDeMarcas()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHO as IdDespacho, CODIGO_DESPACHO as CodigoDespacho, DETALHE_DESPACHO as DetalheDespacho, ");
            sql.Append("IDSITUACAO_PROCESSO as IdSituacaoPeocesso, REGISTRO as Registro ");
            sql.Append("FROM MP_DESPACHO");

            var DBHelper = ServerUtils.criarNovoDbHelper();

            IList<IDespachoDeMarcas> listaDeDespachoDeMarcas = new List<IDespachoDeMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read())
                {
                    var despachoDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();
                    despachoDeMarcas.IdDespacho = UtilidadesDePersistencia.getValorInteger(leitor, "IdDespacho");
                    despachoDeMarcas.CodigoDespacho = UtilidadesDePersistencia.getValorInteger(leitor, "CodigoDespacho");
                    despachoDeMarcas.DetalheDespacho = UtilidadesDePersistencia.GetValorString(leitor, "DetalheDespacho");
                    despachoDeMarcas.IdSituacaoProcesso = UtilidadesDePersistencia.getValorInteger(leitor, "IdSituacaoPeocesso");
                    despachoDeMarcas.Registro = UtilidadesDePersistencia.GetValorBooleano(leitor, "Registro");

                    listaDeDespachoDeMarcas.Add(despachoDeMarcas);
                }
            }

            return listaDeDespachoDeMarcas;
        }

        public IDespachoDeMarcas obtenhaDespachoDeMarcasPeloId(long idDespachoDeMarcas)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHO as IdDespacho, CODIGO_DESPACHO as CodigoDespacho, DETALHE_DESPACHO as DetalheDespacho, ");
            sql.Append("IDSITUACAO_PROCESSO as IdSituacaoPeocesso, REGISTRO as Registro ");
            sql.Append("FROM MP_DESPACHO ");
            sql.Append("WHERE IDDESPACHO = " + idDespachoDeMarcas);

            IDespachoDeMarcas despachoDeMarcas = null;

            IList<IDespachoDeMarcas> listaDeDespachoDeMarcas = new List<IDespachoDeMarcas>();

            listaDeDespachoDeMarcas = obtenhaDespachoDeMarcas(sql, int.MaxValue);

            if (listaDeDespachoDeMarcas.Count > 0)
                despachoDeMarcas = listaDeDespachoDeMarcas[0];

            return despachoDeMarcas;
        }

        private IList<IDespachoDeMarcas> obtenhaDespachoDeMarcas(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<IDespachoDeMarcas> listaDeDespachoDeMarcas = new List<IDespachoDeMarcas>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read() && listaDeDespachoDeMarcas.Count < quantidadeMaximaRegistros)
                {
                    var despachoDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDeMarcas>();
                    despachoDeMarcas.IdDespacho = UtilidadesDePersistencia.getValorInteger(leitor, "IdDespacho");
                    despachoDeMarcas.CodigoDespacho = UtilidadesDePersistencia.getValorInteger(leitor, "CodigoDespacho");
                    despachoDeMarcas.DetalheDespacho = UtilidadesDePersistencia.GetValorString(leitor, "DetalheDespacho");
                    despachoDeMarcas.IdSituacaoProcesso = UtilidadesDePersistencia.getValorInteger(leitor, "IdSituacaoPeocesso");
                    despachoDeMarcas.Registro = UtilidadesDePersistencia.GetValorBooleano(leitor, "Registro");

                    listaDeDespachoDeMarcas.Add(despachoDeMarcas);
                }
            }

            return listaDeDespachoDeMarcas;
        }

        public IList<IDespachoDeMarcas> ObtenhaPorCodigoDoDespachoComoFiltro(string codigo, int quantidadeMaximaDeRegistros)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHO as IdDespacho, CODIGO_DESPACHO as CodigoDespacho, DETALHE_DESPACHO as DetalheDespacho, ");
            sql.Append("IDSITUACAO_PROCESSO as IdSituacaoPeocesso, REGISTRO as Registro ");
            sql.Append("FROM MP_DESPACHO ");

            if (!string.IsNullOrEmpty(codigo))
            {
                sql.Append(string.Concat("WHERE CODIGO_DESPACHO = '", codigo, "'"));
            }
            
            IList<IDespachoDeMarcas> listaDeDespachoDeMarcas = new List<IDespachoDeMarcas>();

            listaDeDespachoDeMarcas = obtenhaDespachoDeMarcas(sql, int.MaxValue);
            
            return listaDeDespachoDeMarcas;
        }

        public void Inserir(IDespachoDeMarcas despachoDeMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            despachoDeMarcas.IdDespacho = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_DESPACHO (");
            sql.Append("IDDESPACHO, CODIGO_DESPACHO, DETALHE_DESPACHO, IDSITUACAO_PROCESSO, REGISTRO) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(despachoDeMarcas.IdDespacho.Value.ToString(), ", "));
            sql.Append(String.Concat("'", despachoDeMarcas.CodigoDespacho, "', "));
            sql.Append(String.Concat("'", despachoDeMarcas.DetalheDespacho, "', "));
            sql.Append(String.Concat("'", despachoDeMarcas.IdSituacaoProcesso, "', "));
            sql.Append(String.Concat("'", despachoDeMarcas.Registro.ToString(), "') "));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IDespachoDeMarcas despachoDeMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_DESPACHO SET ");
            sql.Append(String.Concat("CODIGO_DESPACHO = ", despachoDeMarcas.CodigoDespacho, ", "));
            sql.Append(String.Concat("DETALHE_DESPACHO = '", despachoDeMarcas.DetalheDespacho, "', "));
            sql.Append(String.Concat("IDSITUACAO_PROCESSO = '", despachoDeMarcas.IdSituacaoProcesso.Value.ToString(), "', "));
            sql.Append(String.Concat("REGISTRO = '", despachoDeMarcas.Registro.ToString(), "' "));
            sql.Append(String.Concat("WHERE IDDESPACHO = ", despachoDeMarcas.IdDespacho.Value.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idDespachoDeMarcas)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("delete from MP_DESPACHO");
            sql.Append(string.Concat(" where IDDESPACHO = ", idDespachoDeMarcas.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
