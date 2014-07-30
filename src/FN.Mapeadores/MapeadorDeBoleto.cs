using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.LazyLoad;
using FN.Interfaces.Mapeadores;
using FN.Interfaces.Negocio;

namespace FN.Mapeadores
{
    public class MapeadorDeBoleto : IMapeadorDeBoleto
    {

        public IBoletosGerados obtenhaBoletoPeloId(long idBoleto)
        {
            var sql = retornaSQLSeleciona();
            sql.Append("WHERE ID = " + idBoleto);

            IBoletosGerados boletoGerado = null;

            var listaDeBoleto = obtenhaBoleto(sql);

            if (listaDeBoleto.Count > 0)
                boletoGerado = listaDeBoleto[0];

            return boletoGerado;
        }

        private IList<IBoletosGerados> obtenhaBoleto(StringBuilder sql)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();

            IList<IBoletosGerados> listaDeBoletos = new List<IBoletosGerados>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read())
                {
                    var boletoGerado = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGerados>();

                    boletoGerado.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
                    boletoGerado.NumeroBoleto = UtilidadesDePersistencia.GetValorString(leitor, "NUMEROBOLETO");
                    boletoGerado.NossoNumero = UtilidadesDePersistencia.GetValorLong(leitor, "NOSSONUMERO");

                    var cliente =
                        FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IClienteLazyLoad>(
                            UtilidadesDePersistencia.GetValorLong(leitor, "IDCLIENTE"));

                    boletoGerado.Cliente = cliente;

                    boletoGerado.Valor = UtilidadesDePersistencia.getValorDouble(leitor, "VALOR");
                    boletoGerado.DataGeracao = UtilidadesDePersistencia.getValorDate(leitor, "DATAGERACAO");
                    boletoGerado.DataVencimento = UtilidadesDePersistencia.getValorDate(leitor, "DATAVENCIMENTO");
                    boletoGerado.Observacao = UtilidadesDePersistencia.GetValorString(leitor, "OBSERVACAO");

                    var cedente = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<ICedenteLazyLoad>(
                            UtilidadesDePersistencia.GetValorLong(leitor, "IDCEDENTE"));

                    boletoGerado.Cedente = cedente;

                    boletoGerado.Instrucoes = UtilidadesDePersistencia.GetValorString(leitor, "INSTRUCOES");

                    listaDeBoletos.Add(boletoGerado);
                }
            }

            return listaDeBoletos;
        }

        private IList<IBoletosGerados> obtenhaBoleto(StringBuilder sql, int quantidadeMaximaRegistros, int offSet)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();

            IList<IBoletosGerados> listaDeBoletos = new List<IBoletosGerados>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaRegistros, offSet))
            {
                while (leitor.Read())
                {
                    var boletoGerado = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGerados>();

                    boletoGerado.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
                    boletoGerado.NumeroBoleto = UtilidadesDePersistencia.GetValorString(leitor, "NUMEROBOLETO");
                    boletoGerado.NossoNumero = UtilidadesDePersistencia.GetValorLong(leitor, "NOSSONUMERO");

                    var cliente =
                        FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IClienteLazyLoad>(
                            UtilidadesDePersistencia.GetValorLong(leitor, "IDCLIENTE"));

                    boletoGerado.Cliente = cliente;

                    boletoGerado.Valor = UtilidadesDePersistencia.getValorDouble(leitor, "VALOR");
                    boletoGerado.DataGeracao = UtilidadesDePersistencia.getValorDate(leitor, "DATAGERACAO");
                    boletoGerado.DataVencimento = UtilidadesDePersistencia.getValorDate(leitor, "DATAVENCIMENTO");
                    boletoGerado.Observacao = UtilidadesDePersistencia.GetValorString(leitor, "OBSERVACAO");

                    var cedente = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<ICedenteLazyLoad>(
                            UtilidadesDePersistencia.GetValorLong(leitor, "IDCEDENTE"));

                    boletoGerado.Cedente = cedente;

                    boletoGerado.Instrucoes = UtilidadesDePersistencia.GetValorString(leitor, "INSTRUCOES");
                    
                    listaDeBoletos.Add(boletoGerado);
                }
            }

            return listaDeBoletos;
        }

        private StringBuilder retornaSQLSeleciona()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT ID, NUMEROBOLETO, NOSSONUMERO, IDCLIENTE, VALOR, DATAGERACAO, ");
            sql.Append("DATAVENCIMENTO, OBSERVACAO, IDCEDENTE, INSTRUCOES ");
            sql.Append("FROM FN_BOLETOS_GERADOS ");

            return sql;
        }

        public IBoletosGerados obtenhaBoletoPeloNossoNumero(long numero)
        {
            var sql = retornaSQLSeleciona();
            sql.Append("WHERE NOSSONUMERO = " + numero);

            IBoletosGerados boletoGerado = null;

            var listaDeBoleto = obtenhaBoleto(sql);

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

            sql.Append("INSERT INTO FN_BOLETOS_GERADOS (");
            sql.Append("ID, NUMEROBOLETO, NOSSONUMERO, IDCLIENTE, VALOR, DATAGERACAO, DATAVENCIMENTO, ");
            sql.Append("OBSERVACAO, IDCEDENTE, INSTRUCOES)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(boletoGerado.ID.Value, ", "));
            sql.Append(!string.IsNullOrEmpty(boletoGerado.NumeroBoleto) ? String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(boletoGerado.NumeroBoleto), "', ") : "NULL, ");
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

            sql.Append(!string.IsNullOrEmpty(boletoGerado.Observacao) ? String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(boletoGerado.Observacao), "', ") : "NULL, ");

            if (boletoGerado.Cedente != null && boletoGerado.Cedente.Pessoa.ID.HasValue)
                sql.Append(String.Concat(boletoGerado.Cedente.Pessoa.ID.Value, ", "));
            else
            sql.Append("NULL, ");

            sql.Append(!string.IsNullOrEmpty(boletoGerado.Instrucoes) ? String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(boletoGerado.Instrucoes), "') ") : "NULL) ");

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Excluir(long idBoleto)
        {
            var sql = new StringBuilder();
            var dbHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM FN_BOLETOS_GERADOS");
            sql.Append(string.Concat(" WHERE ID = ", idBoleto.ToString()));

            dbHelper.ExecuteNonQuery(sql.ToString());
        }

        public IBoletosGeradosAux obtenhaProximasInformacoesParaGeracaoDoBoleto()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT ID, PROXNOSSONUMERO ");
            sql.Append("FROM FN_BOLETOS_GERADOS_AUX ");

            var dadosAuxiliares = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGeradosAux>();

            var dbHelper = ServerUtils.criarNovoDbHelper();

            using (var leitor = dbHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read())
                {
                    dadosAuxiliares.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
                    dadosAuxiliares.ProximoNossoNumero = UtilidadesDePersistencia.GetValorLong(leitor, "PROXNOSSONUMERO");
                }
            }

            return dadosAuxiliares;
        }

        public void AtualizarProximasInformacoes(IBoletosGeradosAux dadosAuxBoleto)
        {
            var sql = new StringBuilder();

            var dbHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE FN_BOLETOS_GERADOS_AUX ");
            sql.Append("SET PROXNOSSONUMERO = " + dadosAuxBoleto.ProximoNossoNumero.Value + " ");
            sql.Append(" WHERE ID = " + dadosAuxBoleto.ID.Value);

            dbHelper.ExecuteNonQuery(sql.ToString());
        }

        public void InserirPrimeiraVez(IBoletosGeradosAux dadosAuxBoleto)
        {
            var sql = new StringBuilder();

            var dbHelper = ServerUtils.getDBHelper();

            sql.Append("INSERT INTO FN_BOLETOS_GERADOS_AUX(ID, PROXNOSSONUMERO) ");
            sql.Append("VALUES( ");
            sql.Append(String.Concat(dadosAuxBoleto.ID.Value, ", "));
            sql.Append(String.Concat(dadosAuxBoleto.ProximoNossoNumero.Value, ") "));

            dbHelper.ExecuteNonQuery(sql.ToString());
        }

        public IConfiguracaoDeBoletoBancario ObtenhaConfiguracao()
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            IConfiguracaoDeBoletoBancario configuracao = null;
            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.TIPO, ");
            sql.Append("FN_CNFBOLETO.IDCEDENTE, FN_CNFBOLETO.TIPOPESSOA, FN_CNFBOLETO.IMAGEMBOLETO ");
            sql.Append("FROM FN_CNFBOLETO ");
            sql.Append("LEFT JOIN NCL_PESSOA ON FN_CNFBOLETO.IDCEDENTE = NCL_PESSOA.ID ");
            sql.Append("AND FN_CNFBOLETO.TIPOPESSOA = NCL_PESSOA.TIPO ");

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), int.MaxValue))
            {
                try
                {
                    if (leitor.Read())
                    {
                        configuracao = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeBoletoBancario>();

                        ICedente cedente = null;

                        if (!UtilidadesDePersistencia.EhNulo(leitor, "IDCEDENTE"))
                        {
                            TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "TIPO"));
                            IPessoa pessoa;

                            if (tipoDePessoa.Equals(TipoDePessoa.Fisica))
                                pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaFisicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "ID"));
                            else
                                pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaJuridicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "ID"));

                            cedente = FabricaGenerica.GetInstancia().CrieObjeto<ICedente>(new object[] { pessoa });

                        }

                        configuracao.Cedente = cedente;

                        if (!UtilidadesDePersistencia.EhNulo(leitor, "IMAGEMBOLETO"))
                            configuracao.ImagemDeCabecalhoDoReciboDoSacado =
                                UtilidadesDePersistencia.GetValorString(leitor, "IMAGEMBOLETO");

                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return configuracao;
        }

        public void SalveConfiguracao(IConfiguracaoDeBoletoBancario configuracao)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            DBHelper.ExecuteNonQuery("DELETE FROM FN_CNFBOLETO");

            if (configuracao == null) return;

            sql.Append("INSERT INTO FN_CNFBOLETO (");
            sql.Append("IMAGEMBOLETO, IDCEDENTE, TIPOPESSOA)");
            sql.Append("VALUES (");

            if (configuracao == null || string.IsNullOrEmpty(configuracao.ImagemDeCabecalhoDoReciboDoSacado))
                sql.Append("NULL, ");
            else
                sql.Append(string.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(configuracao.ImagemDeCabecalhoDoReciboDoSacado), "', "));


            if (configuracao == null || configuracao.Cedente == null)
                sql.Append("NULL, NULL) ");
            else
                sql.Append(string.Concat(configuracao.Cedente.Pessoa.ID.Value, ", ", configuracao.Cedente.Pessoa.Tipo.ID, ")"));


            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public int ObtenhaQuantidadeDeBoletos(IFiltro filtro)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            using (var leitor = DBHelper.obtenhaReader(filtro.ObtenhaQueryParaQuantidade()))
            {
                try
                {
                    if (leitor.Read())
                        return UtilidadesDePersistencia.getValorInteger(leitor, "QUANTIDADE");
                }
                finally
                {
                    leitor.Close();
                }
            }

            return 0;
        }

        public IList<IBoletosGerados> obtenhaBoletosGerados(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQuery());

            sql.Append("ORDER BY NOSSONUMERO DESC");

            IList<IBoletosGerados> listaDeBoletos = new List<IBoletosGerados>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros, offSet))
                try
                {
                    while (leitor.Read())
                        listaDeBoletos.Add(obtenhaBoletoComFiltro(leitor));
                }
                finally
                {
                    leitor.Close();
                }

            //listaDeBoletos = obtenhaBoleto(sql, quantidadeDeRegistros, offSet);

            return listaDeBoletos;
        }

        private IBoletosGerados obtenhaBoletoComFiltro(IDataReader leitor)
        {
            var boletoGerado = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGerados>();

            boletoGerado.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
            boletoGerado.NumeroBoleto = UtilidadesDePersistencia.GetValorString(leitor, "NUMEROBOLETO");
            boletoGerado.NossoNumero = UtilidadesDePersistencia.GetValorLong(leitor, "NOSSONUMERO");

            var cliente =
                FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IClienteLazyLoad>(
                    UtilidadesDePersistencia.GetValorLong(leitor, "IDCLIENTE"));

            boletoGerado.Cliente = cliente;

            boletoGerado.Valor = UtilidadesDePersistencia.getValorDouble(leitor, "VALOR");
            boletoGerado.DataGeracao = UtilidadesDePersistencia.getValorDate(leitor, "DATAGERACAO");
            boletoGerado.DataVencimento = UtilidadesDePersistencia.getValorDate(leitor, "DATAVENCIMENTO");
            boletoGerado.Observacao = UtilidadesDePersistencia.GetValorString(leitor, "OBSERVACAO");

            var cedente = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<ICedenteLazyLoad>(
                    UtilidadesDePersistencia.GetValorLong(leitor, "IDCEDENTE"));

            boletoGerado.Cedente = cedente;

            boletoGerado.Instrucoes = UtilidadesDePersistencia.GetValorString(leitor, "INSTRUCOES");


            return boletoGerado;
        }

        public void AtualizarBoletoGerado(IBoletosGerados boletoGerado)
        {
            var sql = new StringBuilder();

            var dbHelper = ServerUtils.getDBHelper();
            
            sql.Append("UPDATE FN_BOLETOS_GERADOS SET ");

            //sql.Append("SET NOSSONUMERO = " + boletoGerado.NossoNumero.Value + ", ");

            sql.Append(!String.IsNullOrEmpty(boletoGerado.NumeroBoleto)
                         ? String.Concat("NUMEROBOLETO = '", UtilidadesDePersistencia.FiltraApostrofe(boletoGerado.NumeroBoleto), "', ")
                         : "NUMEROBOLETO = NULL, ");

            sql.Append(String.Concat("IDCLIENTE = ", boletoGerado.Cliente.Pessoa.ID.Value, ", "));

            sql.Append(String.Concat("VALOR = ", boletoGerado.Valor, ", "));

            sql.Append(boletoGerado.DataGeracao.HasValue
                           ? String.Concat("DATAGERACAO = ", boletoGerado.DataGeracao.Value.ToString("yyyyMMdd"), ", ")
                           : "DATAGERACAO = NULL, ");

            sql.Append(boletoGerado.DataVencimento.HasValue
                           ? String.Concat("DATAVENCIMENTO = ", boletoGerado.DataGeracao.Value.ToString("yyyyMMdd"), ", ")
                           : "DATAVENCIMENTO = NULL, ");

            sql.Append(!String.IsNullOrEmpty(boletoGerado.Observacao)
                         ? String.Concat("OBSERVACAO = '", UtilidadesDePersistencia.FiltraApostrofe(boletoGerado.Observacao), "', ")
                         : "OBSERVACAO = NULL, ");

            sql.Append(String.Concat("IDCEDENTE = ", boletoGerado.Cedente.Pessoa.ID.Value, ", "));

            sql.Append(!String.IsNullOrEmpty(boletoGerado.Instrucoes)
                         ? String.Concat("INSTRUCOES = '", UtilidadesDePersistencia.FiltraApostrofe(boletoGerado.Instrucoes), "' ")
                         : "INSTRUCOES = NULL ");

            sql.Append(" WHERE ID = " + boletoGerado.ID.Value);

            dbHelper.ExecuteNonQuery(sql.ToString());

        }
    }
}
