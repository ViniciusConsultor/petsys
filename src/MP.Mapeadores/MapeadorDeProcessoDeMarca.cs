using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Compartilhados;
using Compartilhados.DBHelper;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Negocio.LazyLoad;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Negocio.LazyLoad;

namespace MP.Mapeadores
{
    public class MapeadorDeProcessoDeMarca : IMapeadorDeProcessoDeMarca
    {
        public void Inserir(IProcessoDeMarca processoDeMarca)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;
            DBHelper = ServerUtils.getDBHelper();

            processoDeMarca.IdProcessoDeMarca = GeradorDeID.getInstancia().getProximoID();

            sql.Append("INSERT INTO MP_PROCESSOMARCA (");
            sql.Append("IDPROCESSO, IDMARCA, PROCESSO,");
            sql.Append("DATADECADASTRO, DATADODEPOSITO,  DATACONCESSAO, PROCESSOEHTERCEIRO, IDDESPACHO,");
            sql.Append("TXTCOMPLDESPACHO, APOSTILA, ATIVO, IDPROCURADOR)");
            sql.Append("VALUES (");
            sql.Append(String.Concat(processoDeMarca.IdProcessoDeMarca.Value, ", "));
            sql.Append(String.Concat(processoDeMarca.Marca.IdMarca.Value, ", "));
            sql.Append(String.Concat(processoDeMarca.Processo, ", "));
            sql.Append(String.Concat(processoDeMarca.DataDoCadastro.ToString("yyyyMMdd"), ", "));
            sql.Append(processoDeMarca.DataDoDeposito.HasValue
                           ? String.Concat(processoDeMarca.DataDoDeposito.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");
            sql.Append(processoDeMarca.DataDeConcessao.HasValue
                           ? String.Concat(processoDeMarca.DataDeConcessao.Value.ToString("yyyyMMdd"), ", ")
                           : "NULL, ");
            sql.Append(processoDeMarca.ProcessoEhDeTerceiro ? "'1', " : "'0', ");

            sql.Append(processoDeMarca.Despacho != null
                           ? String.Concat(processoDeMarca.Despacho.IdDespacho, ", ")
                           : "NULL, ");
            sql.Append(!String.IsNullOrEmpty(processoDeMarca.TextoComplementarDoDespacho)
                          ? String.Concat("'", processoDeMarca.TextoComplementarDoDespacho, "', ")
                          : "NULL, ");
            sql.Append(!String.IsNullOrEmpty(processoDeMarca.Apostila)
                         ? String.Concat("'", processoDeMarca.Apostila, "', ")
                         : "NULL, ");

            sql.Append(processoDeMarca.Ativo ? "'1', " : "'0', ");

            sql.Append(String.Concat(processoDeMarca.Procurador != null ? processoDeMarca.Procurador.Pessoa.ID.Value.ToString() + ")" : "NULL)"));

            DBHelper.ExecuteNonQuery(sql.ToString());
        }

        public void Modificar(IProcessoDeMarca processoDeMarca)
        {
            var sql = new StringBuilder();
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();

            sql.Append("UPDATE MP_PROCESSOMARCA ");
            sql.Append("SET IDMARCA = " + processoDeMarca.Marca.IdMarca + ", ");
            sql.Append(String.Concat("PROCESSO = ", processoDeMarca.Processo, ", "));
            sql.Append(processoDeMarca.DataDeConcessao.HasValue
                           ? String.Concat("DATACONCESSAO = ", processoDeMarca.DataDeConcessao.Value.ToString("yyyyMMdd"), ", ")
                           : "DATACONCESSAO = NULL, ");
            sql.Append(processoDeMarca.DataDoDeposito.HasValue
                           ? String.Concat("DATADODEPOSITO = ", processoDeMarca.DataDoDeposito.Value.ToString("yyyyMMdd"), ", ")
                           : "DATADODEPOSITO = NULL, ");

            sql.Append("PROCESSOEHTERCEIRO = " + (processoDeMarca.ProcessoEhDeTerceiro ? "1, " : "0, "));

            sql.Append(processoDeMarca.Despacho != null
                           ? String.Concat("IDDESPACHO = ", processoDeMarca.Despacho.IdDespacho, ", ")
                           : "IDDESPACHO = NULL, ");

            sql.Append(!String.IsNullOrEmpty(processoDeMarca.TextoComplementarDoDespacho)
                           ? String.Concat("TXTCOMPLDESPACHO = '", processoDeMarca.TextoComplementarDoDespacho, "', ")
                           : "TXTCOMPLDESPACHO = NULL, ");
            sql.Append(!String.IsNullOrEmpty(processoDeMarca.Apostila)
                         ? String.Concat("APOSTILA = '", processoDeMarca.Apostila, "', ")
                         : "APOSTILA = NULL, ");

            sql.Append(processoDeMarca.Ativo ? "ATIVO = '1', " : "ATIVO = '0', ");

            sql.Append(String.Concat("IDPROCURADOR = ", processoDeMarca.Procurador != null ? processoDeMarca.Procurador.Pessoa.ID.Value.ToString() : "NULL"));

            sql.Append(" WHERE IDPROCESSO = " + processoDeMarca.IdProcessoDeMarca);

            DBHelper.ExecuteNonQuery(sql.ToString());

        }

        public void Excluir(long ID)
        {
            IDBHelper DBHelper;

            DBHelper = ServerUtils.getDBHelper();
            DBHelper.ExecuteNonQuery("DELETE FROM MP_PROCESSOMARCA WHERE IDPROCESSO=" + ID.ToString());
        }

        public IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var sql = new StringBuilder();

            sql.Append(filtro.ObtenhaQuery());

            sql.AppendLine(" ORDER BY DATADECADASTRO DESC");

            var processos = new List<IProcessoDeMarca>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString(), quantidadeDeRegistros, offSet))
                try
                {
                    while (leitor.Read())
                        processos.Add(MontaProcessoDeMarca(leitor));
                }
                finally
                {
                    leitor.Close();
                }

            return processos;
        }

        private IProcessoDeMarca MontaProcessoDeMarca(IDataReader leitor)
        {
            var processoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDeMarca>();

            processoDeMarca.Marca = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IMarcasLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDMARCA"));
            processoDeMarca.IdProcessoDeMarca = UtilidadesDePersistencia.GetValorLong(leitor, "IDPROCESSO");
            processoDeMarca.Processo = UtilidadesDePersistencia.GetValorLong(leitor, "PROCESSO");
            processoDeMarca.DataDoCadastro = UtilidadesDePersistencia.getValorDate(leitor, "DATADECADASTRO").Value;
            processoDeMarca.DataDoDeposito = UtilidadesDePersistencia.getValorDate(leitor, "DATADODEPOSITO");
            processoDeMarca.DataDeConcessao = UtilidadesDePersistencia.getValorDate(leitor, "DATACONCESSAO");
            processoDeMarca.ProcessoEhDeTerceiro = UtilidadesDePersistencia.GetValorBooleano(leitor, "PROCESSOEHTERCEIRO");

            if (!UtilidadesDePersistencia.EhNulo(leitor, "IDPROCURADOR"))
                processoDeMarca.Procurador = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IProcuradorLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDPROCURADOR"));

            if (!UtilidadesDePersistencia.EhNulo(leitor, "IDDESPACHO"))
                processoDeMarca.Despacho = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IDespachoDeMarcasLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "IDDESPACHO"));

            if (!UtilidadesDePersistencia.EhNulo(leitor, "TXTCOMPLDESPACHO"))
                processoDeMarca.TextoComplementarDoDespacho = UtilidadesDePersistencia.GetValorString(leitor,
                                                                                                      "TXTCOMPLDESPACHO");
            if (!UtilidadesDePersistencia.EhNulo(leitor, "APOSTILA"))
                processoDeMarca.Apostila = UtilidadesDePersistencia.GetValorString(leitor, "APOSTILA");
            processoDeMarca.Ativo = UtilidadesDePersistencia.GetValorBooleano(leitor, "ATIVO");
            return processoDeMarca;
        }

        public IProcessoDeMarca Obtenha(long ID)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorID>();
            filtro.Operacao = OperacaoDeFiltro.IgualA;
            filtro.ValorDoFiltro = ID.ToString();

            var processos = new List<IProcessoDeMarca>();

            using (var leitor = DBHelper.obtenhaReader(filtro.ObtenhaQuery()))
                try
                {
                    if (leitor.Read())
                        return MontaProcessoDeMarca(leitor);

                }
                finally
                {
                    leitor.Close();
                }

            return null;
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

        public IProcessoDeMarca ObtenhaProcessoDeMarcaPeloNumero(long numeroDoProcesso)
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var filtro = FabricaGenerica.GetInstancia().CrieObjeto<IFiltroMarcaPorProcesso>();
            filtro.Operacao = OperacaoDeFiltro.IgualA;
            filtro.ValorDoFiltro = numeroDoProcesso.ToString();

            var processos = new List<IProcessoDeMarca>();

            using (var leitor = DBHelper.obtenhaReader(filtro.ObtenhaQuery()))
                try
                {
                    if (leitor.Read())
                        return MontaProcessoDeMarca(leitor);

                }
                finally
                {
                    leitor.Close();
                }

            return null;
        }

        public IList<long> ObtenhaTodosNumerosDeProcessosCadastrados()
        {
            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            IList<long> listaDeProcessos = new List<long>();

            using (var leitor = DBHelper.obtenhaReader("SELECT PROCESSO FROM MP_PROCESSOMARCA ORDER BY PROCESSO"))
                try
                {
                    while (leitor.Read())
                        listaDeProcessos.Add(UtilidadesDePersistencia.GetValorLong(leitor, "PROCESSO"));
                }
                finally
                {
                    leitor.Close();
                }

            return listaDeProcessos;
        }

        public IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(long? IDCliente, long? IDGrupoDeAtividade, IList<string> IDsDosDespachos)
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT MP_PROCESSOMARCA.IDPROCESSO, MP_PROCESSOMARCA.IDMARCA IDDAMARCA, MP_PROCESSOMARCA.PROCESSO, ");
            sql.AppendLine("MP_PROCESSOMARCA.DATADECADASTRO, MP_PROCESSOMARCA.DATACONCESSAO, MP_PROCESSOMARCA.PROCESSOEHTERCEIRO, MP_PROCESSOMARCA.IDDESPACHO,");
            sql.AppendLine("MP_PROCESSOMARCA.IDPROCURADOR, MP_PROCESSOMARCA.DATADODEPOSITO, MP_PROCESSOMARCA.TXTCOMPLDESPACHO, ");
            sql.AppendLine("MP_PROCESSOMARCA.APOSTILA, MP_PROCESSOMARCA.ATIVO, ");
            sql.AppendLine("MP_MARCAS.IDMARCA, MP_MARCAS.CODIGONCL, MP_MARCAS.CODIGOAPRESENTACAO, MP_MARCAS.IDCLIENTE, MP_MARCAS.CODIGONATUREZA");
            sql.AppendLine(" FROM MP_PROCESSOMARCA, MP_MARCAS");
            sql.AppendLine(" WHERE MP_PROCESSOMARCA.IDMARCA = MP_MARCAS.IDMARCA ");

            if (IDCliente.HasValue)
            {
                sql.AppendLine(" AND MP_MARCAS.IDCLIENTE = " + IDCliente.Value +  " ");
            }

            if (IDsDosDespachos != null && IDsDosDespachos.Count > 0)
            {
                sql.AppendLine(" AND" +  UtilidadesDePersistencia.MontaFiltro<string>("MP_PROCESSOMARCA.IDDESPACHO", IDsDosDespachos, "OR",
                                                                    false) +  " ");
            }

            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var processos = new List<IProcessoDeMarca>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
                try
                {
                    while (leitor.Read())
                        processos.Add(MontaProcessoDeMarca(leitor));
                }
                finally
                {
                    leitor.Close();
                }

            return processos;

        }

        public IList<IProcessoDeMarca> obtenhaProcessosComMarcaQueContemRadicalDadastrado()
        {
            var sql = new StringBuilder();

            sql.AppendLine("SELECT DISTINCT(MP_PROCESSOMARCA.IDPROCESSO) IDPROCESSO, MP_PROCESSOMARCA.IDMARCA MARCA, ");
            sql.AppendLine("MP_PROCESSOMARCA.PROCESSO NUMEROPROCESSO");
            sql.AppendLine(" FROM MP_PROCESSOMARCA, MP_MARCAS, MP_RADICAL_MARCA");
            sql.AppendLine(" WHERE MP_PROCESSOMARCA.IDMARCA = MP_MARCAS.IDMARCA AND MP_RADICAL_MARCA.IDMARCA = MP_MARCAS.IDMARCA");
            sql.AppendLine(" AND MP_PROCESSOMARCA.ATIVO = 1");

            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            var processos = new List<IProcessoDeMarca>();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
                try
                {
                    while (leitor.Read())
                        processos.Add(MontaProcessoDeMarcaParaConsultaDeRadicais(leitor));
                }
                finally
                {
                    leitor.Close();
                }
            
            return processos;
        }

        public IList<IProcessoDeMarca> ObtenhaProcessoComRadicailAdicionadoNaMarca(IList<IProcessoDeMarca> processos)
        {
            IDictionary<long, List<IRadicalMarcas>> dicionarioDeRadicais = new Dictionary<long, List<IRadicalMarcas>>();

            var sql = new StringBuilder();

            sql.AppendLine("SELECT MP_MARCAS.IDMARCA IDMARCA, MP_RADICAL_MARCA.DESCRICAORADICAL RADICAL, ");
            sql.AppendLine("MP_RADICAL_MARCA.CODIGONCL RADICALNCL");
            sql.AppendLine(" FROM MP_MARCAS, MP_RADICAL_MARCA ");
            sql.AppendLine(" WHERE MP_RADICAL_MARCA.IDMARCA = MP_MARCAS.IDMARCA");

            IDBHelper DBHelper;
            DBHelper = ServerUtils.criarNovoDbHelper();

            using (var leitor = DBHelper.obtenhaReader(sql.ToString()))
                try
                {
                    while (leitor.Read())
                    {
                        var idMarca = UtilidadesDePersistencia.GetValorLong(leitor, "IDMARCA");

                        var radical = FabricaGenerica.GetInstancia().CrieObjeto<IRadicalMarcas>();

                        var listaDeRadicais = new List<IRadicalMarcas>();
                        
                        if(dicionarioDeRadicais.ContainsKey(idMarca))
                        {
                            radical.DescricaoRadical = UtilidadesDePersistencia.GetValorString(leitor, "RADICAL");

                            radical.NCL = !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValorString(leitor, "RADICALNCL")) ? NCL.ObtenhaPorCodigo(UtilidadesDePersistencia.GetValorString(leitor, "RADICALNCL")) : null;
                            
                            radical.IdMarca = idMarca;

                            dicionarioDeRadicais[idMarca].Add(radical);
                        }
                        else
                        {
                            radical.DescricaoRadical = UtilidadesDePersistencia.GetValorString(leitor, "RADICAL");
                            radical.NCL = !string.IsNullOrEmpty(UtilidadesDePersistencia.GetValorString(leitor, "RADICALNCL")) ? NCL.ObtenhaPorCodigo(UtilidadesDePersistencia.GetValorString(leitor, "RADICALNCL")) : null;
                            radical.IdMarca = idMarca;

                            listaDeRadicais.Add(radical);

                            dicionarioDeRadicais.Add(idMarca, listaDeRadicais);
                        }
                    }
                }
                finally
                {
                    leitor.Close();
                }

            foreach (var processo in processos)
            {
                foreach (var idMarca in dicionarioDeRadicais.Keys)
                {
                    if(processo.Marca.IdMarca != null && processo.Marca.IdMarca.Value.Equals(idMarca))
                    {
                        processo.Marca.RadicalMarcas = dicionarioDeRadicais[idMarca];
                    }
                }
            }

            return processos;
        }

        private IProcessoDeMarca MontaProcessoDeMarcaParaConsultaDeRadicais(IDataReader leitor)
        {
            var processoDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<IProcessoDeMarca>();

            processoDeMarca.Marca = FabricaDeObjetoLazyLoad.CrieObjetoLazyLoad<IMarcasLazyLoad>(UtilidadesDePersistencia.GetValorLong(leitor, "MARCA"));
            processoDeMarca.IdProcessoDeMarca = UtilidadesDePersistencia.GetValorLong(leitor, "IDPROCESSO");
            processoDeMarca.Processo = UtilidadesDePersistencia.GetValorLong(leitor, "NUMEROPROCESSO");
            processoDeMarca.DataDoCadastro = DateTime.Now;
            processoDeMarca.DataDoDeposito = null;
            processoDeMarca.DataDeConcessao = null;
            processoDeMarca.ProcessoEhDeTerceiro = false;
            processoDeMarca.Despacho = null;
            processoDeMarca.TextoComplementarDoDespacho = null;
            processoDeMarca.Procurador = null;
            processoDeMarca.Apostila = null;
            processoDeMarca.Ativo = true;

            return processoDeMarca;
        }
    }
}
