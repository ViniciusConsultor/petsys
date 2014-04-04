using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Patentes;

namespace MP.Negocio.Filtros.Patentes
{
    public class FiltroRelatorioGeralPatente : Filtro, IFiltroRelatorioGeralPatente
    {
        public string TipoNatureza { get; set; }
        public TipoClassificacaoPatente ClassificacaoPatente { get; set; }
        public string TipoDeOrigem { get; set; }
        public string StatusDoProcesso { get; set; }
        public ICliente Cliente { get; set; }
        public ITitular Titular { get; set; }
        public IInventor Inventor { get; set; }

        public override string ObtenhaQuery()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT MP_PROCESSOPATENTE.IDPROCESSOPATENTE, MP_PROCESSOPATENTE.IDPATENTE IDDAPATENTE, MP_PROCESSOPATENTE.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADEPUBLICACAO, MP_PROCESSOPATENTE.DATADEDEPOSITO, MP_PROCESSOPATENTE.DATADECONCESSAO, MP_PROCESSOPATENTE.DATADEEXAME, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADECADASTRO, MP_PROCESSOPATENTE.PROCESSODETERCEIRO, MP_PROCESSOPATENTE.IDPROCURADOR, ");
            sql.AppendLine("MP_PROCESSOPATENTE.EHESTRANGEIRO, MP_PROCESSOPATENTE.NUMEROPCT, MP_PROCESSOPATENTE.NUMEROWO, MP_PROCESSOPATENTE.DATAPUBLICACAOPCT, ");
            sql.AppendLine("MP_PROCESSOPATENTE.DATADEPOSITOPCT, MP_PROCESSOPATENTE.IDDESPACHO, MP_PROCESSOPATENTE.ATIVO, MP_PROCESSOPATENTE.IDPASTA, MP_PASTA.NOME NOMEPASTA, MP_PASTA.CODIGO CODIGOPASTA, ");
            sql.AppendLine("MP_PATENTE.IDPATENTE, MP_PROCESSOPATENTE.PAIS ");
            sql.AppendLine(" FROM MP_PROCESSOPATENTE");
            sql.AppendLine(" INNER JOIN MP_PATENTE ON MP_PATENTE.IDPATENTE = MP_PROCESSOPATENTE.IDPATENTE");
            sql.AppendLine(" INNER JOIN MP_NATUREZA_PATENTE ON MP_PATENTE.IDNATUREZAPATENTE = MP_NATUREZA_PATENTE.IDNATUREZA_PATENTE");
            sql.AppendLine(" LEFT JOIN MP_PASTA ON MP_PASTA.ID = MP_PROCESSOPATENTE.IDPASTA");
            sql.AppendLine(" LEFT JOIN MP_PATENTEINVENTOR ON MP_PATENTEINVENTOR.IDPATENTE = MP_PATENTE.IDPATENTE");
            sql.AppendLine(" INNER JOIN MP_INVENTOR ON MP_INVENTOR.IDPESSOA = MP_PATENTEINVENTOR.IDINVENTOR");
            sql.AppendLine(" LEFT JOIN MP_PATENTECLIENTE ON MP_PATENTECLIENTE.IDPATENTE = MP_PATENTE.IDPATENTE");
            sql.AppendLine(" INNER JOIN NCL_CLIENTE ON NCL_CLIENTE.IDPESSOA = MP_PATENTECLIENTE.IDCLIENTE");
            sql.AppendLine(" LEFT JOIN MP_PATENTETITULAR ON MP_PATENTETITULAR.IDPATENTE = MP_PATENTE.IDPATENTE");
            sql.AppendLine(" INNER JOIN MP_TITULAR ON MP_TITULAR.IDPESSOA = MP_PATENTETITULAR.IDTITULAR");
            sql.AppendLine(" WHERE ");  
            sql.AppendLine(ObtenhaCondicaoNatureza());

            if(ClassificacaoPatente != null)
                sql.AppendLine(" AND " + ObtenhaCondicaoClassificacaoPatente());

            if (!string.IsNullOrEmpty(TipoDeOrigem))
                sql.AppendLine(" AND " + ObtenhaCondicaoTipoDeOrigem());

            if (!string.IsNullOrEmpty(StatusDoProcesso))
                sql.AppendLine(" AND " + ObtenhaCondicaoStatusDoProcesso());

            if(Cliente != null && Cliente.Pessoa != null)
                sql.AppendLine(" AND NCL_CLIENTE.IDPESSOA = " + Cliente.Pessoa.ID);

            if (Titular != null && Titular.Pessoa != null)
                sql.AppendLine(" AND MP_TITULAR.IDPESSOA = " + Titular.Pessoa.ID);

            if (Inventor != null && Inventor.Pessoa != null)
                sql.AppendLine(" AND MP_INVENTOR.IDPESSOA = " + Inventor.Pessoa.ID);

            return sql.ToString();
        }

        public string ObtenhaCondicaoNatureza()
        {
            if (TipoNatureza.Equals("PATENTE"))
                return " MP_NATUREZA_PATENTE.SIGLA_NATUREZA NOT IN('DI', 'MI', '30', '31', '32') ";

            return " MP_NATUREZA_PATENTE.SIGLA_NATUREZA IN('DI', 'MI', '30', '31', '32') ";
        }

        public string ObtenhaCondicaoClassificacaoPatente()
        {
            if (ClassificacaoPatente == TipoClassificacaoPatente.Nacional)
                return " MP_PROCESSOPATENTE.EHESTRANGEIRO = 0 ";

            return " MP_PROCESSOPATENTE.EHESTRANGEIRO = 1 ";
        }

        public string ObtenhaCondicaoTipoDeOrigem()
        {
            if (TipoDeOrigem.Equals("PROPRIO"))
                return " MP_PROCESSOPATENTE.PROCESSODETERCEIRO = 0 ";

            return " MP_PROCESSOPATENTE.PROCESSODETERCEIRO = 1 ";
        }

        public string ObtenhaCondicaoStatusDoProcesso()
        {
            if (StatusDoProcesso.Equals("ATIVOS"))
                return " MP_PROCESSOPATENTE.ATIVO = 1 ";

            return " MP_PROCESSOPATENTE.ATIVO = 0 ";
        }
    }
}
