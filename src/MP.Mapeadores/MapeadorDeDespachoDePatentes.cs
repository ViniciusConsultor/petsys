﻿using System;
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
    public class MapeadorDeDespachoDePatentes : IMapeadorDeDespachoDePatentes
    {
        public IDespachoDePatentes obtenhaDespachoDePatentesPeloId(long idDespachoDePatentes)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHOPATENTE, CODIGO, ");
            sql.Append("TITULO, DESCRICAO, SITUACAO, PRAZO, PROVIDENCIA, DESATIVAPROCESSO, AGENDAPAGAMENTO, TEMPLATEEMAIL ");
            sql.Append("FROM MP_DESPACHO_PATENTE ");
            sql.Append("WHERE IDDESPACHOPATENTE = " + idDespachoDePatentes);

            IDespachoDePatentes despachoDePatentes = null;

            IList<IDespachoDePatentes> listaDeDespachoDePatentes = new List<IDespachoDePatentes>();

            listaDeDespachoDePatentes = obtenhaDespachoDePatentes(sql, int.MaxValue);

            if (listaDeDespachoDePatentes.Count > 0)
                despachoDePatentes = listaDeDespachoDePatentes[0];

            return despachoDePatentes;
        }

        private IList<IDespachoDePatentes> obtenhaDespachoDePatentes(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<IDespachoDePatentes> listaDeDespachoDePatentes = new List<IDespachoDePatentes>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeMaximaRegistros))
            {
                while (leitor.Read())
                {
                    var despachoDePatentes = FabricaGenerica.GetInstancia().CrieObjeto<IDespachoDePatentes>();
                    despachoDePatentes.IdDespachoDePatente = UtilidadesDePersistencia.getValorInteger(leitor, "IDDESPACHOPATENTE");
                    despachoDePatentes.Codigo = UtilidadesDePersistencia.GetValorString(leitor, "CODIGO");
                    despachoDePatentes.Titulo = UtilidadesDePersistencia.GetValorString(leitor, "TITULO");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "DESCRICAO"))
                        despachoDePatentes.Descricao = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "SITUACAO"))
                        despachoDePatentes.Situacao = UtilidadesDePersistencia.GetValorString(leitor, "SITUACAO");

                    if (!UtilidadesDePersistencia.EhNulo(leitor, "PROVIDENCIA"))
                        despachoDePatentes.TipoProvidencia = UtilidadesDePersistencia.GetValorString(leitor, "PROVIDENCIA");


                    if (!UtilidadesDePersistencia.EhNulo(leitor, "PRAZO"))
                        despachoDePatentes.PrazoProvidencia = UtilidadesDePersistencia.getValorInteger(leitor, "PRAZO");

                    despachoDePatentes.DesativaProcesso = UtilidadesDePersistencia.GetValorBooleano(leitor, "DESATIVAPROCESSO");
                    despachoDePatentes.AgendarPagamento = UtilidadesDePersistencia.GetValorBooleano(leitor, "AGENDAPAGAMENTO");


                    if (!UtilidadesDePersistencia.EhNulo(leitor, "TEMPLATEEMAIL"))
                    {
                        despachoDePatentes.TemplateDeEmail =
                            FabricaGenerica.GetInstancia().CrieObjeto<ITemplateDeEmail>();
                        despachoDePatentes.TemplateDeEmail.Template = UtilidadesDePersistencia.GetValorString(leitor,
                                                                                                     "TEMPLATEEMAIL");
                    }
                    

                    listaDeDespachoDePatentes.Add(despachoDePatentes);
                }
            }

            return listaDeDespachoDePatentes;
        }

        public IList<IDespachoDePatentes> ObtenhaPorDescricao(string descricaoParcial, int quantidadeMaximaDeRegistros)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHOPATENTE, CODIGO, ");
            sql.Append("TITULO, DESCRICAO, SITUACAO, PRAZO, PROVIDENCIA, DESATIVAPROCESSO, AGENDAPAGAMENTO, TEMPLATEEMAIL ");
            sql.Append("FROM MP_DESPACHO_PATENTE ");

            if (!string.IsNullOrEmpty(descricaoParcial))
            {
                sql.Append(string.Concat("WHERE DESCRICAO LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(descricaoParcial.ToUpper()), "%'"));
            }

            sql.Append(" ORDER BY CODIGO");

            IList<IDespachoDePatentes> listaDeDespachoDePatentes = new List<IDespachoDePatentes>();

            listaDeDespachoDePatentes = obtenhaDespachoDePatentes(sql, quantidadeMaximaDeRegistros);

            return listaDeDespachoDePatentes;
        }

        public void Inserir(IDespachoDePatentes despachoDePatentes)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            despachoDePatentes.IdDespachoDePatente = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_DESPACHO_PATENTE (");
            sql.Append("IDDESPACHOPATENTE, CODIGO, DESCRICAO, TITULO, SITUACAO, PRAZO, PROVIDENCIA, DESATIVAPROCESSO, AGENDAPAGAMENTO) ");
            sql.Append("VALUES (");
            sql.Append(String.Concat(despachoDePatentes.IdDespachoDePatente.Value.ToString(), ", "));
            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.Codigo), "', "));

            sql.Append(string.IsNullOrEmpty(despachoDePatentes.Descricao)
                           ? "NULL, "
                           : String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.Descricao),
                                           "', "));

            sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.Titulo), "', "));


            sql.Append(string.IsNullOrEmpty(despachoDePatentes.Situacao)
                           ? "NULL, "
                           : String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.Situacao),
                                           "', "));


            sql.Append(!despachoDePatentes.PrazoProvidencia.HasValue
                           ? "NULL, "
                           : String.Concat(despachoDePatentes.PrazoProvidencia.Value.ToString(),
                                           ", "));

            sql.Append(string.IsNullOrEmpty(despachoDePatentes.TipoProvidencia)
                           ? "NULL, "
                           : String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.TipoProvidencia),
                                           "', "));

            sql.Append(despachoDePatentes.DesativaProcesso ? String.Concat("'", 1, "', ") : String.Concat("'", 0, "', "));
            sql.Append(despachoDePatentes.AgendarPagamento ? String.Concat("'", 1, "') ") : String.Concat("'", 0, "') "));
            
            DBHelper.ExecuteNonQuery(sql.ToString());

            if (despachoDePatentes.TemplateDeEmail != null)
                ModificarTemplate(despachoDePatentes);
        }

        public void ModificarTemplate(IDespachoDePatentes despacho)
        {
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            var sql = new StringBuilder();

            sql.Append("UPDATE MP_DESPACHO_PATENTE SET ");

            if (despacho.TemplateDeEmail == null || string.IsNullOrWhiteSpace(despacho.TemplateDeEmail.Template))
                sql.Append("TEMPLATEEMAIL = NULL");
            else
                sql.Append(String.Concat("TEMPLATEEMAIL = '",
                                         UtilidadesDePersistencia.FiltraApostrofe(despacho.TemplateDeEmail.Template), "'"));

            sql.Append(String.Concat(" WHERE IDDESPACHOPATENTE = ", despacho.IdDespachoDePatente.Value.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString(), false);
        }

        public void Modificar(IDespachoDePatentes despachoDePatentes)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_DESPACHO_PATENTE SET ");
            sql.Append(String.Concat("CODIGO = '", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.Codigo), "', "));

            sql.Append(string.IsNullOrEmpty(despachoDePatentes.Descricao)
                           ? "DESCRICAO = NULL, "
                           : String.Concat("DESCRICAO = '", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.Descricao),
                                           "', "));

            sql.Append(String.Concat("TITULO = '", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.Titulo), "', "));

            sql.Append(string.IsNullOrEmpty(despachoDePatentes.Situacao)
                          ? "SITUACAO = NULL, "
                          : String.Concat("SITUACAO= '", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.Situacao),
                                          "', "));

            sql.Append(string.IsNullOrEmpty(despachoDePatentes.TipoProvidencia)
                           ? "PROVIDENCIA = NULL, "
                           : String.Concat("PROVIDENCIA= '", UtilidadesDePersistencia.FiltraApostrofe(despachoDePatentes.TipoProvidencia),
                                           "', "));


            sql.Append(!despachoDePatentes.PrazoProvidencia.HasValue
                           ? "PRAZO = NULL, "
                           : String.Concat("PRAZO = ", despachoDePatentes.PrazoProvidencia.Value.ToString(),
                                           ", "));


            sql.Append(despachoDePatentes.DesativaProcesso ? String.Concat("DESATIVAPROCESSO = '", 1, "', ") : String.Concat("DESATIVAPROCESSO = '", 0, "', "));
            sql.Append(despachoDePatentes.AgendarPagamento ? String.Concat("AGENDAPAGAMENTO = '", 1, "', ") : String.Concat("AGENDAPAGAMENTO = '", 0, "' "));
            sql.Append(String.Concat("WHERE IDDESPACHOPATENTE = ", despachoDePatentes.IdDespachoDePatente.Value.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());

            ModificarTemplate(despachoDePatentes);
        }

        public void Excluir(long idDespachoDePatentes)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM MP_DESPACHO_PATENTE");
            sql.Append(string.Concat(" WHERE IDDESPACHOPATENTE = ", idDespachoDePatentes.ToString()));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }


        public IDespachoDePatentes ObtenhaDespachoPeloCodigo(string codigo)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDDESPACHOPATENTE, CODIGO, ");
            sql.Append("TITULO, DESCRICAO, SITUACAO, PRAZO, PROVIDENCIA, DESATIVAPROCESSO, AGENDAPAGAMENTO, TEMPLATEEMAIL ");
            sql.Append("FROM MP_DESPACHO_PATENTE ");
            sql.Append("WHERE CODIGO = '" + codigo + "'");
            sql.Append(" ORDER BY CODIGO");

            IList<IDespachoDePatentes> listaDeDespachoDePatentes = new List<IDespachoDePatentes>();

            listaDeDespachoDePatentes = obtenhaDespachoDePatentes(sql, 1);

            if (listaDeDespachoDePatentes.Count == 0)
                return null;

            return listaDeDespachoDePatentes[0];
        }
    }
}
