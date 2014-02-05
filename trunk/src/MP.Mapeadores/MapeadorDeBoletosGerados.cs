using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio.LazyLoad;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeBoletosGerados : IMapeadorDeBoletosGerados
    {
        public IBoletosGerados obtenhaBoletoPeloId(long idBoleto)
        {
            var sql = retornaSQLSelecionaTodos();
            sql.Append("WHERE ID = " + idBoleto);

            IBoletosGerados boletoGerado = null;

            var listaDeBoleto = obtenhaBoleto(sql, int.MaxValue);

            if (listaDeBoleto.Count > 0)
                boletoGerado = listaDeBoleto[0];

            return boletoGerado;
        }

        private IList<IBoletosGerados> obtenhaBoleto(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();

            IList<IBoletosGerados> listaDeBoletos = new List<IBoletosGerados>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaRegistros))
            {
                while (leitor.Read())
                {
                    var boletoGerado = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGerados>();

                    boletoGerado.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
                    boletoGerado.NumeroBoleto = UtilidadesDePersistencia.GetValorLong(leitor, "NUMEROBOLETO");
                    boletoGerado.NossoNumero = UtilidadesDePersistencia.GetValorLong(leitor, "NOSSONUMERO");

                    var cliente =
                        FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IClienteLazyLoad>(
                            UtilidadesDePersistencia.GetValorLong(leitor, "IDCLIENTE"));

                    boletoGerado.Cliente = cliente;

                    boletoGerado.Valor = UtilidadesDePersistencia.getValorDouble(leitor, "VALOR");
                    boletoGerado.DataGeracao = UtilidadesDePersistencia.getValorDate(leitor, "DATAGERACAO");
                    boletoGerado.DataVencimento = UtilidadesDePersistencia.getValorDate(leitor, "DATAVENCIMENTO");
                    boletoGerado.NumeroProcesso = boletoGerado.NumeroProcesso.HasValue ? UtilidadesDePersistencia.GetValorLong(leitor, "NUMEROPROCESSO") : 0;
                    boletoGerado.Observacao = !string.IsNullOrEmpty(boletoGerado.Observacao) ? UtilidadesDePersistencia.GetValorString(leitor, "OBSERVACAO") : null;

                    listaDeBoletos.Add(boletoGerado);
                }
            }

            return listaDeBoletos;
        }

        private StringBuilder retornaSQLSelecionaTodos()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT ID, NUMEROBOLETO, NOSSONUMERO, IDCLIENTE, VALOR, DATAGERACAO,  ");
            sql.Append("DATAVENCIMENTO, NUMEROPROCESSO, OBSERVACAO ");
            sql.Append("FROM MP_BOLETOS_GERADOS ");

            return sql;
        }

        public IBoletosGerados obtenhaBoletoPeloNumero(long numero)
        {
            var sql = retornaSQLSelecionaTodos();
            sql.Append("WHERE NUMEROBOLETO = " + numero);

            IBoletosGerados boletoGerado = null;

            var listaDeBoleto = obtenhaBoleto(sql, int.MaxValue);

            if (listaDeBoleto.Count > 0)
                boletoGerado = listaDeBoleto[0];

            return boletoGerado;
        }

        public void Inserir(IBoletosGerados boletoGerado)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            boletoGerado.ID = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_BOLETOS_GERADOS (");
            sql.Append("ID, NUMEROBOLETO, NOSSONUMERO, IDCLIENTE, VALOR, DATAGERACAO, DATAVENCIMENTO, ");
            sql.Append("NUMEROPROCESSO, OBSERVACAO)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(boletoGerado.ID.Value, ", "));
            sql.Append(String.Concat(boletoGerado.NumeroBoleto.Value, ", "));
            sql.Append(String.Concat(boletoGerado.NossoNumero.Value, ", "));
            sql.Append(String.Concat(boletoGerado.Cliente.Pessoa.ID.Value, ", "));

            sql.Append(boletoGerado.Valor == 0
                           ? String.Concat(0, ", ")
                           : String.Concat(boletoGerado.Valor.ToString().Replace(",", ".") + ", "));
            
            sql.Append(boletoGerado.DataGeracao.HasValue
                           ? String.Concat(boletoGerado.DataGeracao.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

            sql.Append(boletoGerado.DataVencimento.HasValue
                           ? String.Concat(boletoGerado.DataVencimento.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");

            sql.Append(boletoGerado.NumeroProcesso.HasValue
                           ? String.Concat(boletoGerado.NumeroProcesso.Value, ", ")
                           : String.Concat(0, ", "));

            sql.Append(!string.IsNullOrEmpty(boletoGerado.Observacao) ? String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(boletoGerado.Observacao), "') ") : "NULL) ");
            
            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idBoleto)
        {
            var sql = new StringBuilder();
            var dbHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_BOLETOS_GERADOS");
            sql.Append(string.Concat(" WHERE ID = ", idBoleto.ToString()));

            dbHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
