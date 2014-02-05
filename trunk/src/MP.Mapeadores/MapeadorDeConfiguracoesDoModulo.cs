using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.LazyLoad;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeConfiguracoesDoModulo : IMapeadorDeConfiguracoesDoModulo
    {
        public IConfiguracaoDeModulo ObtenhaConfiguracao()
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            IConfiguracaoDeModulo configuracaoDeModulo = null;
            DBHelper = ServerUtils.criarNovoDbHelper();

            sql.Append("SELECT NCL_PESSOA.ID, NCL_PESSOA.TIPO, ");
            sql.Append("MP_CNFMODGRL.IDCEDENTE, MP_CNFMODGRL.TIPOPESSOA, MP_CNFMODGRL.VALORSALARIOMINIO, MP_CNFMODGRL.IMAGEMBOLETO ");
            sql.Append("FROM MP_CNFMODGRL ");
            sql.Append("LEFT JOIN NCL_PESSOA ON MP_CNFMODGRL.IDCEDENTE = NCL_PESSOA.ID ");
            sql.Append("AND MP_CNFMODGRL.TIPOPESSOA = NCL_PESSOA.TIPO ");
            
            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), int.MaxValue))
            {
                try
                {
                    if (leitor.Read())
                    {
                        configuracaoDeModulo = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeModulo>();

                        ICedente cedente = null;

                        if (!UtilidadesDePersistencia.EhNulo(leitor, "IDCEDENTE"))
                        {
                            TipoDePessoa tipoDePessoa = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(leitor, "TIPO"));
                            IPessoa pessoa;

                            if (tipoDePessoa.Equals(TipoDePessoa.Fisica))
                                pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaFisicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "ID"));
                            else
                                pessoa = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IPessoaJuridicaLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "ID"));

                            cedente = FabricaGenerica.GetInstancia().CrieObjeto<ICedente>(new object[] {pessoa});

                        }

                        IConfiguracaoDeBoletoBancario configuracaoBoleto  = FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeBoletoBancario>();
                        configuracaoBoleto.Cedente = cedente;

                        if (!UtilidadesDePersistencia.EhNulo(leitor, "IMAGEMBOLETO"))
                            configuracaoBoleto.ImagemDeCabecalhoDoReciboDoSacado =
                                UtilidadesDePersistencia.GetValorString(leitor, "IMAGEMBOLETO");

                        configuracaoDeModulo.ConfiguracaoDeBoletoBancario = configuracaoBoleto;

                        IConfiguracaoDeIndicesFinanceiros configuracaoDeIndicesFinanceiros =
                            FabricaGenerica.GetInstancia().CrieObjeto<IConfiguracaoDeIndicesFinanceiros>();

                        if (!UtilidadesDePersistencia.EhNulo(leitor, "VALORSALARIOMINIO"))
                            configuracaoDeIndicesFinanceiros.ValorDoSalarioMinimo =
                                UtilidadesDePersistencia.getValorDouble(leitor, "VALORSALARIOMINIO");

                        configuracaoDeModulo.ConfiguracaoDeIndicesFinanceiros = configuracaoDeIndicesFinanceiros;
                    }
                }
                finally
                {
                    leitor.Close();
                }
            }

            return configuracaoDeModulo;
        }

        public void Salve(IConfiguracaoDeModulo configuracao)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            DBHelper.ExecuteNonQuery("DELETE FROM MP_CNFMODGRL");

            if (configuracao == null) return;

            sql.Append("INSERT INTO MP_CNFMODGRL (");
            sql.Append("VALORSALARIOMINIO, IMAGEMBOLETO, IDCEDENTE, TIPOPESSOA)");
            sql.Append("VALUES (");

            if (configuracao.ConfiguracaoDeIndicesFinanceiros == null || configuracao.ConfiguracaoDeIndicesFinanceiros.ValorDoSalarioMinimo == null)
                sql.Append("NULL, ");
            else
                sql.Append(string.Concat(UtilidadesDePersistencia.TPVd(configuracao.ConfiguracaoDeIndicesFinanceiros.ValorDoSalarioMinimo.Value), ", "));

            if (configuracao.ConfiguracaoDeBoletoBancario == null || string.IsNullOrEmpty(configuracao.ConfiguracaoDeBoletoBancario.ImagemDeCabecalhoDoReciboDoSacado))
                sql.Append("NULL, ");
            else
                sql.Append(string.Concat("'",UtilidadesDePersistencia.FiltraApostrofe(configuracao.ConfiguracaoDeBoletoBancario.ImagemDeCabecalhoDoReciboDoSacado), "', "));


            if (configuracao.ConfiguracaoDeBoletoBancario == null || configuracao.ConfiguracaoDeBoletoBancario.Cedente == null)
                sql.Append("NULL, NULL) ");
            else
                sql.Append(string.Concat(configuracao.ConfiguracaoDeBoletoBancario.Cedente.Pessoa.ID.Value, ", ", configuracao.ConfiguracaoDeBoletoBancario.Cedente.Pessoa.Tipo.ID, ")"));
            

            DBHelper.ExecuteNonQuery(sql.ToString());
        }
    }
}
