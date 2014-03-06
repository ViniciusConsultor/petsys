﻿using System;
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
using Compartilhados.Interfaces.FN.Negocio;
using FN.Interfaces.Mapeadores;

namespace FN.Mapeadores
{
    public class MapeadorDeItensFinanceirosDeRecebimento : IMapeadorDeItensFinanceirosDeRecebimento
    {
        public void Insira(IItemLancamentoFinanceiroRecebimento Item)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            Item.ID = GeradorDeID.ProximoID();

            sql.Append("INSERT INTO FN_ITEMFINANREC (");
            sql.Append("ID, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, SITUACAO, TIPOLANCAMENTO) ");
            sql.Append("VALUES (");

            sql.Append(Item.ID.Value + ", ");
            sql.Append(Item.Cliente.Pessoa.ID.Value + ", ");
            sql.Append(UtilidadesDePersistencia.TPVd(Item.Valor) + ", ");
            sql.Append(string.IsNullOrEmpty(Item.Observacao)
                           ? "NULL, "
                           : "'" + UtilidadesDePersistencia.FiltraApostrofe(Item.Observacao) + "', ");
            sql.Append(Item.DataDoLancamento.ToString("yyyyMMdd") + ", ");
            sql.Append(Item.Situacao.ID + ", ");
            sql.Append(Item.TipoLacamento.ID + ")");

            DBHelper.ExecuteNonQuery(sql.ToString());

        }

        public void Modifique(IItemLancamentoFinanceiroRecebimento Item)
        {

        }

        public int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro)
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

        private IItemLancamentoFinanceiroRecebimento MontaItemDeRecebimento(IDataReader leitor)
        {
            var item = FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();

            item.Cliente = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IClienteLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDCLIENTE"));
            item.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
            item.DataDoLancamento = UtilidadesDePersistencia.getValorDate(leitor, "DATALACAMENTO").Value;
            item.Situacao = Situacao.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "SITUACAO"));
            item.Valor = UtilidadesDePersistencia.getValorDouble(leitor, "VALOR");

            item.TipoLacamento =
                TipoLacamentoFinanceiroRecebimento.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "TIPOLANCAMENTO"));

            if (!UtilidadesDePersistencia.EhNulo(leitor, "OBSERVACAO"))
                item.Observacao = UtilidadesDePersistencia.GetValorString(leitor, "OBSERVACAO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "DATARECEBIMENTO"))
                item.DataDoRecebimento = UtilidadesDePersistencia.getValorDate(leitor, "DATARECEBIMENTO");

            return item;
        }

        public IList<IItemLancamentoFinanceiroRecebimento> ObtenhaProcessosDeMarcas(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQuery());

            sql.AppendLine(" ORDER BY DATALACAMENTO DESC");

            var itens = new List<IItemLancamentoFinanceiroRecebimento>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros, offSet))
                try
                {
                    while (leitor.Read())
                        itens.Add(MontaItemDeRecebimento(leitor));
                }
                finally
                {
                    leitor.Close();
                }

            return itens;
        }
    }
}
