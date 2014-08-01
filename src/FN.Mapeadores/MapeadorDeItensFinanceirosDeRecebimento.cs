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
using Compartilhados.Interfaces.FN.Mapeadores;
using Compartilhados.Interfaces.FN.Negocio;
using FN.Interfaces.Negocio.Filtros.ContasAReceber;

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
            sql.Append("ID, IDCLIENTE, VALOR, OBSERVACAO, DATALACAMENTO, DATAVENCIMENTO, DESCRICAO, FORMARECEBIMENTO, DATARECEBIMENTO, SITUACAO, NUMEROBOLETOGERADO, TIPOLANCAMENTO) ");
            sql.Append("VALUES (");

            sql.Append(Item.ID.Value + ", ");
            sql.Append(Item.Cliente.Pessoa.ID.Value + ", ");
            sql.Append(UtilidadesDePersistencia.TPVd(Item.Valor) + ", ");
            sql.Append(string.IsNullOrEmpty(Item.Observacao)
                           ? "NULL, "
                           : "'" + UtilidadesDePersistencia.FiltraApostrofe(Item.Observacao) + "', ");
            sql.Append(Item.DataDoLancamento.ToString("yyyyMMdd") + ", ");
            sql.Append(Item.DataDoVencimento.ToString("yyyyMMdd") + ", ");
            
            sql.Append(string.IsNullOrEmpty(Item.Descricao)
                         ? "NULL, "
                         : "'" + UtilidadesDePersistencia.FiltraApostrofe(Item.Descricao) + "', ");

            sql.Append(Item.FormaDeRecebimento == null
                         ? "NULL, "
                         : Item.FormaDeRecebimento.ID  + ", ");

            sql.Append(!Item.DataDoRecebimento.HasValue
                         ? "NULL, "
                         : Item.DataDoRecebimento.Value.ToString("yyyyMMdd") + ", ");

            sql.Append(Item.Situacao.ID + ", ");

            if (string.IsNullOrEmpty(Item.NumeroBoletoGerado))
                sql.Append("NULL, ");
            else
                sql.Append("'" + UtilidadesDePersistencia.FiltraApostrofe(Item.NumeroBoletoGerado) + "', ");
            
            sql.Append(Item.TipoLacamento.ID + ") ");

            DBHelper.ExecuteNonQuery(sql.ToString());

        }

        public void Modifique(IItemLancamentoFinanceiroRecebimento Item)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE FN_ITEMFINANREC SET ");
            sql.Append("VALOR = " + UtilidadesDePersistencia.TPVd(Item.Valor) + ", ");

            sql.Append(string.IsNullOrEmpty(Item.Observacao)
                           ? "OBSERVACAO = NULL, "
                           : "OBSERVACAO = '" + UtilidadesDePersistencia.FiltraApostrofe(Item.Observacao) + "', ");

            sql.Append("DATALACAMENTO = " + Item.DataDoLancamento.ToString("yyyyMMdd") + ", ");
            sql.Append("DATAVENCIMENTO = " + Item.DataDoVencimento.ToString("yyyyMMdd") + ", ");

            sql.Append(!Item.DataDoRecebimento.HasValue
                           ? "DATARECEBIMENTO = NULL, "
                           : "DATARECEBIMENTO = " + Item.DataDoRecebimento.Value.ToString("yyyyMMdd") + ", ");
            
            sql.Append(string.IsNullOrEmpty(Item.Descricao)
                           ? "DESCRICAO = NULL, "
                           : "DESCRICAO = '" + UtilidadesDePersistencia.FiltraApostrofe(Item.Descricao) + "', ");

            sql.Append(Item.FormaDeRecebimento == null
                           ? "FORMARECEBIMENTO = NULL, "
                           : "FORMARECEBIMENTO = " + Item.FormaDeRecebimento.ID + ", ");

            if (!string.IsNullOrEmpty(Item.NumeroBoletoGerado))
                sql.Append("NUMEROBOLETOGERADO = '" + UtilidadesDePersistencia.FiltraApostrofe(Item.NumeroBoletoGerado) + "', ");
            else
                sql.Append("NUMEROBOLETOGERADO = NULL, ");

            sql.Append("SITUACAO = " + Item.Situacao.ID + " ");

            sql.Append(" WHERE ID = " + Item.ID.Value);

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public int ObtenhaQuantidadeDeItensFinanceiros(IFiltro filtro)
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

        public IList<IItemLancamentoFinanceiroRecebimento> ObtenhaItensFinanceiros(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQuery());

            sql.AppendLine(" ORDER BY NOME, DATALACAMENTO DESC");

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

        public IItemLancamentoFinanceiroRecebimento Obtenha(long ID)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroContaAReceberPorID>();
            filtro.Operacao = OperacaoDeFiltro.IgualA;
            filtro.ValorDoFiltro = ID.ToString();
            
            using (var leitor = DBHelper.obtenhaReader(filtro.ObtenhaQuery()))
                try
                {
                    if (leitor.Read())
                        return MontaItemDeRecebimento(leitor);

                }
                finally
                {
                    leitor.Close();
                }

            return null;
        }

        public void Excluir(long IdItemLancamentoFinanceiroRecebimento)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("DELETE FROM FN_ITEMFINANREC");
            sql.Append(string.Concat(" WHERE ID = ", IdItemLancamentoFinanceiroRecebimento));

            DBHelper.ExecuteNonQuery(sql.ToString());

        }

        private IItemLancamentoFinanceiroRecebimento MontaItemDeRecebimento(IDataReader leitor)
        {
            var item = FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();

            item.Cliente = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IClienteLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDCLIENTE"));
            item.ID = UtilidadesDePersistencia.GetValorLong(leitor, "ID");
            item.DataDoLancamento = UtilidadesDePersistencia.getValorDate(leitor, "DATALACAMENTO").Value;
            item.DataDoVencimento = UtilidadesDePersistencia.getValorDate(leitor, "DATAVENCIMENTO").Value;  
            item.Situacao = Situacao.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "SITUACAO"));
            item.Valor = UtilidadesDePersistencia.getValorDouble(leitor, "VALOR");

            item.TipoLacamento =
                TipoLacamentoFinanceiroRecebimento.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "TIPOLANCAMENTO"));

            if (!UtilidadesDePersistencia.EhNulo(leitor, "OBSERVACAO"))
                item.Observacao = UtilidadesDePersistencia.GetValorString(leitor, "OBSERVACAO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "DATARECEBIMENTO"))
                item.DataDoRecebimento = UtilidadesDePersistencia.getValorDate(leitor, "DATARECEBIMENTO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "FORMARECEBIMENTO"))
                item.FormaDeRecebimento = FormaDeRecebimento.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "FORMARECEBIMENTO"));

            if (!UtilidadesDePersistencia.EhNulo(leitor, "DESCRICAO"))
                item.Descricao = UtilidadesDePersistencia.GetValorString(leitor, "DESCRICAO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "NUMEROBOLETOGERADO"))
                item.NumeroBoletoGerado = UtilidadesDePersistencia.GetValorString(leitor, "NUMEROBOLETOGERADO");

            return item;
        }


    }
}
