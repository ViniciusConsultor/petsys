using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorLayoutRevistaPatente : IMapeadorLayoutRevistaPatente
    {
        private ILayoutRevistaPatente MapeieObjetoLayoutRevistaPatente(IDataReader reader)
        {
            var layoutRevistaPatente = FabricaGenerica.GetInstancia().CrieObjeto<ILayoutRevistaPatente>();

            layoutRevistaPatente.Codigo = UtilidadesDePersistencia.GetValorLong(reader, "IDCODIGOIDENTIFICACAOREVISTA");
            layoutRevistaPatente.Identificador = UtilidadesDePersistencia.GetValorString(reader, "IDENTIFICADOR");
            layoutRevistaPatente.DescricaoIdentificador = UtilidadesDePersistencia.GetValorString(reader, "DESCRICAO_IDENTIFICADOR");
            layoutRevistaPatente.DescricaoResumida = UtilidadesDePersistencia.GetValorString(reader, "DESCRICAO_RESUMIDA");
            layoutRevistaPatente.TipoDeIdentificador = UtilidadesDePersistencia.getValorInteger(reader, "TIPO_IDENTIFICADOR");
            layoutRevistaPatente.TamanhoDoCampo = UtilidadesDePersistencia.getValorInteger(reader, "TAMANHO_CAMPO");
            layoutRevistaPatente.CampoDelimitadorDoRegistro = UtilidadesDePersistencia.GetValorBooleano(reader, "CAMPO_DELIMITADOR_REGISTRO");
            layoutRevistaPatente.CampoIdentificadorDoProcesso = UtilidadesDePersistencia.GetValorBooleano(reader, "CAMPO_IDENTIFICADOR_PROCESSO");
            layoutRevistaPatente.CampoIdentificadorDeColidencia = UtilidadesDePersistencia.GetValorBooleano(reader, "CAMPO_IDENTIFICADOR_COLIDENCIA");

            return layoutRevistaPatente;
        }

        public void Inserir(ILayoutRevistaPatente layoutRevistaPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            string delimitadorDoRegistro = layoutRevistaPatente.CampoDelimitadorDoRegistro ? "Y" : "N";
            string campoIdentificadorDoProcesso = layoutRevistaPatente.CampoIdentificadorDoProcesso ? "Y" : "N";
            string campoIdentificadorDeColidencia = layoutRevistaPatente.CampoIdentificadorDeColidencia ? "Y" : "N";

            comandoSQL.Append("INSERT INTO MP_LAYOUT_REVISTA_PATENTE(IDCODIGOIDENTIFICACAOREVISTA, IDENTIFICADOR, DESCRICAO_IDENTIFICADOR, DESCRICAO_RESUMIDA,");
            comandoSQL.Append("TIPO_IDENTIFICADOR, TAMANHO_CAMPO, CAMPO_DELIMITADOR_REGISTRO, CAMPO_IDENTIFICADOR_PROCESSO, CAMPO_IDENTIFICADOR_COLIDENCIA) VALUES(");
            comandoSQL.Append(layoutRevistaPatente.Codigo + ", ");
            comandoSQL.Append("'" + layoutRevistaPatente.Identificador + "', ");
            comandoSQL.Append("'" + layoutRevistaPatente.DescricaoIdentificador + "', ");
            comandoSQL.Append("'" + layoutRevistaPatente.DescricaoResumida + "', ");
            comandoSQL.Append(layoutRevistaPatente.TipoDeIdentificador + "', ");
            comandoSQL.Append(layoutRevistaPatente.TamanhoDoCampo + "', ");
            comandoSQL.Append("'" + delimitadorDoRegistro + "', ");
            comandoSQL.Append("'" + campoIdentificadorDoProcesso + "', ");
            comandoSQL.Append("'" + campoIdentificadorDeColidencia + "')");

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Excluir(long codigo)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("DELETE FROM MP_LAYOUT_REVISTA_PATENTE WHERE IDCODIGOIDENTIFICACAOREVISTA = " + codigo);

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());
        }

        public void Modificar(ILayoutRevistaPatente layoutRevistaPatente)
        {
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            string delimitadorDoRegistro = layoutRevistaPatente.CampoDelimitadorDoRegistro ? "Y" : "N";
            string campoIdentificadorDoProcesso = layoutRevistaPatente.CampoIdentificadorDoProcesso ? "Y" : "N";
            string campoIdentificadorDeColidencia = layoutRevistaPatente.CampoIdentificadorDeColidencia ? "Y" : "N";

            comandoSQL.Append("UPDATE MP_LAYOUT_REVISTA_PATENTE SET ");
            comandoSQL.Append(" DESCRICAO_IDENTIFICADOR = '" + layoutRevistaPatente.DescricaoIdentificador + "', ");
            comandoSQL.Append(" DESCRICAO_RESUMIDA = '" + layoutRevistaPatente.DescricaoResumida + "', ");
            comandoSQL.Append(" TIPO_IDENTIFICADOR = " + layoutRevistaPatente.TipoDeIdentificador + "', ");
            comandoSQL.Append(" TAMANHO_CAMPO = " + layoutRevistaPatente.TamanhoDoCampo + "', ");
            comandoSQL.Append(" CAMPO_DELIMITADOR_REGISTRO = '" + delimitadorDoRegistro + "', ");
            comandoSQL.Append(" CAMPO_IDENTIFICADOR_PROCESSO = '" + campoIdentificadorDoProcesso + "', ");
            comandoSQL.Append(" CAMPO_IDENTIFICADOR_COLIDENCIA = '" + campoIdentificadorDeColidencia + "')");

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());            
        }
    }
}
