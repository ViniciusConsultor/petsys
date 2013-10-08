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

            layoutRevistaPatente.Codigo = UtilidadesDePersistencia.GetValorLong(reader, "IDCODIGOREVISTA");
            layoutRevistaPatente.NomeDoCampo = UtilidadesDePersistencia.GetValorString(reader, "NOMEDOCAMPO");
            layoutRevistaPatente.DescricaoDoCampo = UtilidadesDePersistencia.GetValorString(reader, "DESCRICAO_CAMPO");
            layoutRevistaPatente.DescricaoResumida = UtilidadesDePersistencia.GetValorString(reader, "DESCRICAO_RESUMIDA");
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

            string delimitadorDoRegistro = layoutRevistaPatente.CampoDelimitadorDoRegistro ? "1" : "0";
            string campoIdentificadorDoProcesso = layoutRevistaPatente.CampoIdentificadorDoProcesso ? "1" : "0";
            string campoIdentificadorDeColidencia = layoutRevistaPatente.CampoIdentificadorDeColidencia ? "1" : "0";

            comandoSQL.Append("INSERT INTO MP_LAYOUT_REVISTA_PATENTE(IDCODIGOREVISTA, NOMEDOCAMPO, DESCRICAO_CAMPO, DESCRICAO_RESUMIDA, ");
            comandoSQL.Append("TAMANHO_CAMPO, CAMPO_DELIMITADOR_REGISTRO, CAMPO_IDENTIFICADOR_PROCESSO, CAMPO_IDENTIFICADOR_COLIDENCIA) VALUES(");
            comandoSQL.Append(layoutRevistaPatente.Codigo + ", ");
            comandoSQL.Append("'" + layoutRevistaPatente.NomeDoCampo + "', ");
            comandoSQL.Append("'" + layoutRevistaPatente.DescricaoDoCampo + "', ");
            comandoSQL.Append("'" + layoutRevistaPatente.DescricaoResumida + "', ");
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
            comandoSQL.Append(" DESCRICAO_CAMPO = '" + layoutRevistaPatente.DescricaoDoCampo + "', ");
            comandoSQL.Append(" DESCRICAO_RESUMIDA = '" + layoutRevistaPatente.DescricaoResumida + "', ");
            comandoSQL.Append(" TAMANHO_CAMPO = " + layoutRevistaPatente.TamanhoDoCampo + "', ");
            comandoSQL.Append(" CAMPO_DELIMITADOR_REGISTRO = '" + delimitadorDoRegistro + "', ");
            comandoSQL.Append(" CAMPO_IDENTIFICADOR_PROCESSO = '" + campoIdentificadorDoProcesso + "', ");
            comandoSQL.Append(" CAMPO_IDENTIFICADOR_COLIDENCIA = '" + campoIdentificadorDeColidencia + "')");

            DBHelper.ExecuteNonQuery(comandoSQL.ToString());            
        }


        public List<ILayoutRevistaPatente> ObtenhaTodos()
        {
            var listaDeLayouts = new List<ILayoutRevistaPatente>();
            var comandoSQL = new StringBuilder();
            IDBHelper DBHelper = ServerUtils.getDBHelper();

            comandoSQL.Append("SELECT IDCODIGOREVISTA, NOMEDOCAMPO, DESCRICAO_CAMPO, DESCRICAO_RESUMIDA, TAMANHO_CAMPO, CAMPO_DELIMITADOR_REGISTRO, ");
            comandoSQL.Append("CAMPO_IDENTIFICADOR_PROCESSO, CAMPO_IDENTIFICADOR_COLIDENCIA FROM MP_LAYOUT_REVISTA_PATENTE");

            using (var reader = DBHelper.obtenhaReader(comandoSQL.ToString()))
                while (reader.Read())
                    listaDeLayouts.Add(MapeieObjetoLayoutRevistaPatente(reader));

            return listaDeLayouts;
        }
    }
}
