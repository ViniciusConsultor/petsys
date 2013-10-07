using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeSituacaoDoProcesso : IMapeadorDeSituacaoDoProcesso
    {
        public IList<ISituacaoDoProcesso> obtenhaTodasSituacoesDoProcesso()
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDSITUACAO_PROCESSO as IdSituacaoPeocesso, DESCRICAO_SITUACAO as DescricaoSituacao ");
            sql.Append("FROM MP_SITUACAO_PROCESSO");

            var DBHelper = ServerUtils.criarNovoDbHelper();

            IList<ISituacaoDoProcesso> listaDeSituacaoDoProcesso = new List<ISituacaoDoProcesso>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read())
                {
                    var situacaoDoProcesso = FabricaGenerica.GetInstancia().CrieObjeto<ISituacaoDoProcesso>();
                    situacaoDoProcesso.IdSituacaoProcesso = UtilidadesDePersistencia.GetValorLong(leitor, "IdSituacaoPeocesso");
                    situacaoDoProcesso.DescricaoSituacao = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoSituacao");

                    listaDeSituacaoDoProcesso.Add(situacaoDoProcesso);
                }
            }

            return listaDeSituacaoDoProcesso;
        }

        public ISituacaoDoProcesso obtenhaSituacaoDoProcessoPeloId(long idSituacaoDoProcesso)
        {
            var sql = new StringBuilder();

            sql.Append("SELECT IDSITUACAO_PROCESSO as IdSituacaoPeocesso, DESCRICAO_SITUACAO as DescricaoSituacao ");
            sql.Append("FROM MP_SITUACAO_PROCESSO ");
            sql.Append("WHERE IDSITUACAO_PROCESSO = " + idSituacaoDoProcesso);

            ISituacaoDoProcesso situacaoDoProcesso = null;

            IList<ISituacaoDoProcesso> listaDeSituacaoDoProcesso = new List<ISituacaoDoProcesso>();

            listaDeSituacaoDoProcesso = obtenhaSituacaoDoProcesso(sql, int.MaxValue);

            if (listaDeSituacaoDoProcesso.Count > 0)
                situacaoDoProcesso = listaDeSituacaoDoProcesso[0];

            return situacaoDoProcesso;
        }

        private IList<ISituacaoDoProcesso> obtenhaSituacaoDoProcesso(StringBuilder sql, int quantidadeMaximaRegistros)
        {
            var DBHelper = ServerUtils.criarNovoDbHelper();
            IList<ISituacaoDoProcesso> listaDeSituacaoDoProcesso = new List<ISituacaoDoProcesso>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
            {
                while (leitor.Read() && listaDeSituacaoDoProcesso.Count < quantidadeMaximaRegistros)
                {
                    var situacaoDoProcesso = FabricaGenerica.GetInstancia().CrieObjeto<ISituacaoDoProcesso>();
                    situacaoDoProcesso.IdSituacaoProcesso = UtilidadesDePersistencia.GetValorLong(leitor, "IdSituacaoPeocesso");
                    situacaoDoProcesso.DescricaoSituacao = UtilidadesDePersistencia.GetValorString(leitor, "DescricaoSituacao");

                    listaDeSituacaoDoProcesso.Add(situacaoDoProcesso);
                }
            }

            return listaDeSituacaoDoProcesso;
        }

        public ISituacaoDoProcesso obtenhaSituacaoDoProcessoPelaDescricao(string descricaoSituacaoDoProcesso)
        {
            throw new NotImplementedException();
        }

        public IList<ISituacaoDoProcesso> ObtenhaPorDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros)
        {
            throw new NotImplementedException();
        }

        public void Inserir(ISituacaoDoProcesso situacaoDoProcesso)
        {
            throw new NotImplementedException();
        }

        public void Modificar(ISituacaoDoProcesso situacaoDoProcesso)
        {
            throw new NotImplementedException();
        }

        public void Excluir(long idSituacaoDoProcesso)
        {
            throw new NotImplementedException();
        }
    }
}
