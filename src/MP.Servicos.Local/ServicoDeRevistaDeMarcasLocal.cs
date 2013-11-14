using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Compartilhados;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Filtros.Marcas;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeRevistaDeMarcasLocal : Servico, IServicoDeRevistaDeMarcas
    {
        public ServicoDeRevistaDeMarcasLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }

        public void Inserir(IList<IRevistaDeMarcas> listaDeObjetoRevistaDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.InserirDadosRevistaXml(listaDeObjetoRevistaDeMarcas);
                ServerUtils.CommitTransaction();
            }
            catch
            {
                ServerUtils.RollbackTransaction();
                throw;
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IRevistaDeMarcas> ObtenhaProcessosExistentesDeAcordoComARevistaXml(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml)
        {
            IList<IRevistaDeMarcas> listaDeProcessosExistentesNaRevista = new List<IRevistaDeMarcas>();
            listaDeProcessosExistentesNaRevista = LerRevistaXMLParaProcessosExistentes(revistaDeMarcas, revistaXml);
            return listaDeProcessosExistentesNaRevista;
        }

        public IList<ILeituraRevistaDeMarcas> ObtenhaResultadoDaConsultaPorFiltroXML(XmlDocument revistaXml, IFiltroLeituraDeRevistaDeMarcas filtro)
        {
            IList<ILeituraRevistaDeMarcas> listaDeTodosProcessosDaRevista = LerTodaRevistaXML(revistaXml);
            IList<ILeituraRevistaDeMarcas> listaResultadoPorFiltro = new List<ILeituraRevistaDeMarcas>();

            if (listaDeTodosProcessosDaRevista.Count > 0)
            {
                foreach (var processoDaRevista in listaDeTodosProcessosDaRevista)
                {
                    if (!string.IsNullOrEmpty(filtro.NumeroDoProcesso))
                    {
                        if (processoDaRevista.NumeroDoProcesso == filtro.NumeroDoProcesso)
                        {
                            listaResultadoPorFiltro.Add(processoDaRevista);
                            return listaResultadoPorFiltro;
                        }
                    }
                    else if (!string.IsNullOrEmpty(filtro.UF) && filtro.Procurador == null && filtro.Despacho == null)
                    {
                        // filtro por estado
                        if (processoDaRevista.Uf != null && processoDaRevista.Uf.ToUpper() == filtro.UF.ToUpper())
                            listaResultadoPorFiltro.Add(processoDaRevista);
                    }
                    else if (!string.IsNullOrEmpty(filtro.UF) && filtro.Procurador != null && filtro.Despacho == null)
                    {
                        // filtro por estado, procurador
                        if (processoDaRevista.Uf != null && processoDaRevista.Uf.ToUpper() == filtro.UF.ToUpper()
                            && processoDaRevista.Procurador != null && filtro.Procurador.Pessoa.Nome.ToUpper().Equals(processoDaRevista.Procurador.ToUpper()))
                            listaResultadoPorFiltro.Add(processoDaRevista);
                    }
                    else if (!string.IsNullOrEmpty(filtro.UF) && filtro.Procurador == null && filtro.Despacho != null)
                    {
                        // filtro por estado, despacho
                        if (processoDaRevista.Uf != null && processoDaRevista.Uf.ToUpper() == filtro.UF.ToUpper()
                            && filtro.Despacho.CodigoDespacho == processoDaRevista.CodigoDoDespacho)
                            listaResultadoPorFiltro.Add(processoDaRevista);
                    }
                    else if (!string.IsNullOrEmpty(filtro.UF) && filtro.Procurador != null && filtro.Despacho != null)
                    {
                        // filtro por estado, procurador , despacho
                        if (processoDaRevista.Uf != null && processoDaRevista.Uf.ToUpper() == filtro.UF.ToUpper()
                            && processoDaRevista.Procurador != null && filtro.Procurador.Pessoa.Nome.ToUpper().Equals(processoDaRevista.Procurador.ToUpper())
                            && filtro.Despacho.CodigoDespacho == processoDaRevista.CodigoDoDespacho)
                            listaResultadoPorFiltro.Add(processoDaRevista);
                    }
                    else if (string.IsNullOrEmpty(filtro.UF) && filtro.Procurador != null && filtro.Despacho == null)
                    {
                        // filtro por procurador
                        if (processoDaRevista.Procurador != null && filtro.Procurador.Pessoa.Nome.ToUpper().Equals(processoDaRevista.Procurador.ToUpper()))
                            listaResultadoPorFiltro.Add(processoDaRevista);
                    }
                    else if (string.IsNullOrEmpty(filtro.UF) && filtro.Procurador != null && filtro.Despacho != null)
                    {
                        // filtro por procurador, despacho
                        if (processoDaRevista.Procurador != null && filtro.Procurador.Pessoa.Nome.ToUpper().Equals(processoDaRevista.Procurador.ToUpper())
                            && filtro.Despacho.CodigoDespacho == processoDaRevista.CodigoDoDespacho)
                            listaResultadoPorFiltro.Add(processoDaRevista);
                    }
                    else if (string.IsNullOrEmpty(filtro.UF) && filtro.Procurador == null && filtro.Despacho != null)
                    {
                        // filtro por despacho
                        if (filtro.Despacho.CodigoDespacho == processoDaRevista.CodigoDoDespacho)
                            listaResultadoPorFiltro.Add(processoDaRevista);
                    }
                    else
                    {
                        new List<ILeituraRevistaDeMarcas>();
                    }
                }

            }

            return listaResultadoPorFiltro;
        }

        public IList<ILeituraRevistaDeMarcas> ObtenhaObjetoDeLeituraRevistaDeMarcas(IList<IRevistaDeMarcas> listaDeProcessosExistentes)
        {
            IList<ILeituraRevistaDeMarcas> listaObjetosLeituraRevistaDeMarcas = new List<ILeituraRevistaDeMarcas>();
            var objetoLeituraRevistaDeMarcas = FabricaGenerica.GetInstancia().CrieObjeto<ILeituraRevistaDeMarcas>();
            return listaObjetosLeituraRevistaDeMarcas;
        }

        //public IList<ILeituraRevistaDeMarcas> ObtenhaListaDeMarcasDeClientes(XmlDocument revistaXml)
        //{
        //    IList<ILeituraRevistaDeMarcas> listaDeProcessosDaRevista = new List<ILeituraRevistaDeMarcas>();
        //    IList<IProcessoDeMarca> listaDeProcessosComMarcaQueContemRadicalDadastrado = new List<IProcessoDeMarca>();

        //    listaDeProcessosDaRevista = LerTodaRevistaXML(revistaXml);

        //    listaDeProcessosComMarcaQueContemRadicalDadastrado = obtenhaProcessosComMarcaQueContemRadicalDadastrado();


        //}

        //private IList<IProcessoDeMarca> obtenhaProcessosComMarcaQueContemRadicalDadastrado()
        //{
        //    ServerUtils.setCredencial(_Credencial);

        //    var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeProcessoDeMarca>();

        //    try
        //    {
        //        return mapeador.obtenhaProcessosComMarcaQueContemRadicalDadastrado();
        //    }
        //    finally
        //    {
        //        ServerUtils.libereRecursos();
        //    }
        //}

        public IList<ILeituraRevistaDeMarcas> obtenhaTodosOsProcessosDaRevistaDeMarcasXML(XmlDocument revistaXml)
        {
            return LerTodaRevistaXML(revistaXml);
        }

        private IList<ILeituraRevistaDeMarcas> LerTodaRevistaXML(XmlDocument revistaXml)
        {
            IList<ILeituraRevistaDeMarcas> listaDeProcessos = new List<ILeituraRevistaDeMarcas>();

            XmlNodeList xmlprocessos = revistaXml.GetElementsByTagName("processo");

            foreach (XmlNode processo in xmlprocessos)
            {
                var processoLidoDaRevistaDeMarca = FabricaGenerica.GetInstancia().CrieObjeto<ILeituraRevistaDeMarcas>();
                processoLidoDaRevistaDeMarca.NumeroDoProcesso = processo.Attributes.GetNamedItem("numero").Value;

                if (processo.Attributes.GetNamedItem("data-deposito") != null)
                    processoLidoDaRevistaDeMarca.DataDeDeposito = processo.Attributes.GetNamedItem("data-deposito").Value;

                if (processo.Attributes.GetNamedItem("data-concessao") != null)
                    processoLidoDaRevistaDeMarca.DataDeConcessao = processo.Attributes.GetNamedItem("data-concessao").Value;

                if (processo.Attributes.GetNamedItem("data-vigencia") != null)
                    processoLidoDaRevistaDeMarca.DataDeVigencia = processo.Attributes.GetNamedItem("data-vigencia").Value;

                var despachos = processo["despachos"];

                if (despachos != null)
                {
                    var despacho = despachos["despacho"];

                    if (despacho != null)
                    {
                        processoLidoDaRevistaDeMarca.CodigoDoDespacho = despachos["despacho"].Attributes.GetNamedItem("codigo").Value;
                        var textoDoDespacho = despacho["texto-complementar"];
                        processoLidoDaRevistaDeMarca.TextoDoDespacho = textoDoDespacho != null ? textoDoDespacho.InnerText : null;
                    }
                }

                var titulares = processo["titulares"];

                if (titulares != null)
                {
                    var titular = titulares["titular"];

                    if (titular != null)
                    {
                        if (titular.Attributes.GetNamedItem("nome-razao-social") != null)
                            processoLidoDaRevistaDeMarca.Titular = titular.Attributes.GetNamedItem("nome-razao-social").Value;

                        if (titular.Attributes.GetNamedItem("pais") != null)
                            processoLidoDaRevistaDeMarca.Pais = titular.Attributes.GetNamedItem("pais").Value;

                        if (titular.Attributes.GetNamedItem("uf") != null)
                            processoLidoDaRevistaDeMarca.Uf = titular.Attributes.GetNamedItem("uf").Value;
                    }
                }

                var marca = processo["marca"];

                if (marca != null)
                {
                    if (marca.Attributes.GetNamedItem("apresentacao") != null)
                        processoLidoDaRevistaDeMarca.Apresentacao = marca.Attributes.GetNamedItem("apresentacao").Value;

                    if (marca.Attributes.GetNamedItem("natureza") != null)
                        processoLidoDaRevistaDeMarca.Natureza = marca.Attributes.GetNamedItem("natureza").Value;

                    var descricaoMarca = marca["nome"];
                    if (descricaoMarca != null) processoLidoDaRevistaDeMarca.Marca = UtilidadesDeString.RemoveAcentos(descricaoMarca.InnerText);

                    var traducaoMarca = marca["traducao"];
                    if (traducaoMarca != null) processoLidoDaRevistaDeMarca.TraducaoDaMarca = traducaoMarca.InnerText;
                }

                var classesViena = processo["classes-vienna"];

                if (classesViena != null)
                {
                    processoLidoDaRevistaDeMarca.ClasseViena = FabricaGenerica.GetInstancia().CrieObjeto<IClasseViena>();

                    if (classesViena.Attributes.GetNamedItem("edicao") != null)
                        processoLidoDaRevistaDeMarca.ClasseViena.EdicaoClasseViena = classesViena.Attributes.GetNamedItem("edicao").Value;

                    processoLidoDaRevistaDeMarca.ClasseViena.ListaDeCodigosClasseViena = new List<string>();

                    foreach (XmlNode classe in classesViena)
                    {
                        if (classe.Attributes != null)
                            processoLidoDaRevistaDeMarca.ClasseViena.ListaDeCodigosClasseViena.Add(
                                classe.Attributes.GetNamedItem("codigo").Value);
                    }
                }

                var classesNice = processo["classe-nice"];

                if (classesNice != null)
                {
                    if (classesNice.Attributes.GetNamedItem("codigo") != null)
                        processoLidoDaRevistaDeMarca.NCL = classesNice.Attributes.GetNamedItem("codigo").Value;

                    if (classesNice.Attributes.GetNamedItem("edicao") != null)
                        processoLidoDaRevistaDeMarca.EdicaoNCL = classesNice.Attributes.GetNamedItem("edicao").Value;

                    var especificacao = classesNice["especificacao"];

                    if (especificacao != null)
                        processoLidoDaRevistaDeMarca.EspecificacaoNCL = especificacao.InnerText;
                }

                var classeNacional = processo["classe-nacional"];

                if (classeNacional != null)
                {
                    processoLidoDaRevistaDeMarca.ClasseNacional = FabricaGenerica.GetInstancia().CrieObjeto<IClasseNacional>();

                    if (classeNacional.Attributes.GetNamedItem("codigo") != null)
                        processoLidoDaRevistaDeMarca.ClasseNacional.CodigoClasseNacional = classeNacional.Attributes.GetNamedItem("codigo").Value;

                    var especificacaoClasseNacional = classeNacional["especificacao"];

                    if (especificacaoClasseNacional != null)
                        processoLidoDaRevistaDeMarca.ClasseNacional.EspecificacaoClasseNacional = especificacaoClasseNacional.InnerText;

                    var subClassesNacional = classeNacional["sub-classes-nacional"];

                    if (subClassesNacional != null)
                    {
                        processoLidoDaRevistaDeMarca.ClasseNacional.listaDeCodigosDeSubClasse = new List<string>();

                        foreach (XmlNode subClasseNacional in subClassesNacional)
                            if (subClasseNacional.Attributes != null)
                                processoLidoDaRevistaDeMarca.ClasseNacional.listaDeCodigosDeSubClasse.Add(
                                    subClasseNacional.Attributes.GetNamedItem("codigo").Value);
                    }
                }

                var apostila = processo["apostila"];

                if (apostila != null)
                    processoLidoDaRevistaDeMarca.Apostila = apostila.InnerText;

                var prioridadeUnionista = processo["prioridade-unionista"];

                if (prioridadeUnionista != null)
                {
                    var prioridade = prioridadeUnionista["prioridade"];

                    if (prioridade != null)
                    {
                        if (prioridade.Attributes.GetNamedItem("data") != null)
                            processoLidoDaRevistaDeMarca.DataPrioridadeUnionista = prioridade.Attributes.GetNamedItem("data").Value;

                        if (prioridade.Attributes.GetNamedItem("numero") != null)
                            processoLidoDaRevistaDeMarca.NumeroPrioridadeUnionista = prioridade.Attributes.GetNamedItem("numero").Value;

                        if (prioridade.Attributes.GetNamedItem("pais") != null)
                            processoLidoDaRevistaDeMarca.PaisPrioridadeUnionista = prioridade.Attributes.GetNamedItem("pais").Value;
                    }
                }

                var procurador = processo["procurador"];

                if (procurador != null)
                    processoLidoDaRevistaDeMarca.Procurador = procurador.InnerText;

                var sobrestadores = processo["sobrestadores"];

                if (sobrestadores != null)
                {
                    processoLidoDaRevistaDeMarca.DicionarioSobrestadores = new Dictionary<string, string>();

                    foreach (XmlNode sobrestador in sobrestadores)
                        if (sobrestador.Attributes != null)
                            processoLidoDaRevistaDeMarca.DicionarioSobrestadores.Add(sobrestador.Attributes.GetNamedItem("processo").Value,
                                sobrestador.Attributes.GetNamedItem("marca").Value);
                }

                listaDeProcessos.Add(processoLidoDaRevistaDeMarca);
            }

            return listaDeProcessos;
        }

        private void AtualizeDespachoNoProcesso(string codigoDoDespacho, IProcessoDeMarca processoDeMarca)
        {
            if (string.IsNullOrEmpty(codigoDoDespacho)) return;

            IDespachoDeMarcas despacho;

            using (var servicoDespacho = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
                despacho = servicoDespacho.ObtenhaDespachoPorCodigo(codigoDoDespacho);

            if (despacho != null)
                processoDeMarca.Despacho = despacho;
        }

        private IList<IRevistaDeMarcas> LerRevistaXMLParaProcessosExistentes(IRevistaDeMarcas revistaDeMarcas, XmlDocument revistaXml)
        {

            var numeroRevista = string.Empty;
            var dataRevista = string.Empty;

            IList<IRevistaDeMarcas> listaDeProcessosDaRevistaDeMarcas = new List<IRevistaDeMarcas>();

            XmlNodeList dadosRevista = revistaXml.GetElementsByTagName("revista");

            for (int i = 0; i < dadosRevista.Count; i++)
            {
                numeroRevista = dadosRevista[i].Attributes.GetNamedItem("numero").Value;
                dataRevista = dadosRevista[i].Attributes.GetNamedItem("data").Value;
            }

            XmlNodeList xmlprocessos = revistaXml.GetElementsByTagName("processo");

            foreach (XmlNode processo in xmlprocessos)
            {
                var objetoRevista = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();

                objetoRevista.NumeroProcessoDeMarca = Convert.ToInt64(processo.Attributes.GetNamedItem("numero").Value);

                if (processo.Attributes.GetNamedItem("data-deposito") != null) {}
                    objetoRevista.DataDeDeposito = Convert.ToDateTime(processo.Attributes.GetNamedItem("data-deposito").Value);

                if (processo.Attributes.GetNamedItem("data-concessao") != null)
                    objetoRevista.DataDeConcessao = Convert.ToDateTime(processo.Attributes.GetNamedItem("data-concessao").Value);

                var despachos = processo["despachos"];

                if (despachos != null)
                {
                    var despacho = despachos["despacho"];

                    if (despacho != null)
                    {
                        objetoRevista.CodigoDespachoAtual = despacho.Attributes.GetNamedItem("codigo").Value;

                        var textoDoDespacho = despacho["texto-complementar"];
                        objetoRevista.TextoDoDespacho = textoDoDespacho != null ? textoDoDespacho.InnerText : null;
                    }

                }

                var apostila = processo["apostila"];
                objetoRevista.Apostila = apostila != null ? apostila.InnerText : null;

                objetoRevista.NumeroRevistaMarcas = Convert.ToInt32(numeroRevista);
                objetoRevista.DataPublicacao = Convert.ToDateTime(dataRevista);
                objetoRevista.DataProcessamento = DateTime.Now;
                objetoRevista.Processada = revistaDeMarcas.Processada;
                objetoRevista.ExtensaoArquivo = revistaDeMarcas.ExtensaoArquivo;
                listaDeProcessosDaRevistaDeMarcas.Add(objetoRevista);
            }

            var codigoDeDespachoDoProcessoDaRevista = string.Empty;

            IList<IRevistaDeMarcas> listaDeDadosDaRevistaASerSalvo = new List<IRevistaDeMarcas>();

            if (listaDeProcessosDaRevistaDeMarcas.Count > 0)
            {
                // verifica se o processo está cadastrado na base

                IList<long> listaDeNumerosDeProcessosCadastrados = new List<long>();

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcessoDeMarca>())
                {
                    listaDeNumerosDeProcessosCadastrados = servico.ObtenhaTodosNumerosDeProcessosCadastrados();

                    foreach (var processo in listaDeProcessosDaRevistaDeMarcas)
                    {
                        if (listaDeNumerosDeProcessosCadastrados.Contains(processo.NumeroProcessoDeMarca))
                        {
                            var objetoRevistaASerSalvo = FabricaGenerica.GetInstancia().CrieObjeto<IRevistaDeMarcas>();
                            var processoDeMarcaExistente = servico.ObtenhaProcessoDeMarcaPeloNumero(processo.NumeroProcessoDeMarca);

                            if (processoDeMarcaExistente.IdProcessoDeMarca != null)
                            {
                                objetoRevistaASerSalvo.IdRevistaMarcas = GeradorDeID.getInstancia().getProximoID();

                                codigoDeDespachoDoProcessoDaRevista = processo.CodigoDespachoAtual;

                                objetoRevistaASerSalvo.Apostila = processo.Apostila;

                                if (!string.IsNullOrEmpty(codigoDeDespachoDoProcessoDaRevista))
                                {
                                    var codigoDoDespachoAnterior = processoDeMarcaExistente.Despacho == null
                                                                       ? null
                                                                       : processoDeMarcaExistente.Despacho.
                                                                             CodigoDespacho;

                                    objetoRevistaASerSalvo.CodigoDespachoAnterior = codigoDoDespachoAnterior;
                                    AtualizeDespachoNoProcesso(codigoDeDespachoDoProcessoDaRevista, processoDeMarcaExistente);
                                }

                                if (!string.IsNullOrEmpty(processo.Apostila))
                                {
                                    processoDeMarcaExistente.Apostila = processo.Apostila;
                                }

                                if (processo.DataDeDeposito != null)
                                {
                                    processoDeMarcaExistente.DataDoDeposito = processo.DataDeDeposito;
                                }

                                if (processo.DataDeConcessao != null)
                                {
                                    processoDeMarcaExistente.DataDeConcessao = processo.DataDeConcessao;
                                }

                                if (!string.IsNullOrEmpty(processo.TextoDoDespacho))
                                {
                                    processoDeMarcaExistente.TextoComplementarDoDespacho = processo.TextoDoDespacho;
                                }

                                objetoRevistaASerSalvo.CodigoDespachoAtual = processo.CodigoDespachoAtual;
                                objetoRevistaASerSalvo.DataProcessamento = processo.DataProcessamento;
                                objetoRevistaASerSalvo.DataPublicacao = processo.DataPublicacao;
                                objetoRevistaASerSalvo.ExtensaoArquivo = processo.ExtensaoArquivo;
                                objetoRevistaASerSalvo.NumeroProcessoDeMarca = processo.NumeroProcessoDeMarca;
                                objetoRevistaASerSalvo.NumeroRevistaMarcas = processo.NumeroRevistaMarcas;
                                objetoRevistaASerSalvo.Processada = processo.Processada;
                                objetoRevistaASerSalvo.TextoDoDespacho = processo.TextoDoDespacho;

                                listaDeDadosDaRevistaASerSalvo.Add(objetoRevistaASerSalvo);

                                servico.Modificar(processoDeMarcaExistente);
                            }
                        }
                    }

                }
            }

            return listaDeDadosDaRevistaASerSalvo;
        }

       

        public IList<IRevistaDeMarcas> ObtenhaRevistasAProcessar(int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                return mapeador.ObtenhaRevistasAProcessar(quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IRevistaDeMarcas> ObtenhaRevistasJaProcessadas(int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                return mapeador.ObtenhaRevistasJaProcessadas(quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IDictionary<IList<ILeituraRevistaDeMarcas>, IList<ILeituraRevistaDeMarcas>> obtenhaListaDasMarcasColidentesEClientes
            (IList<ILeituraRevistaDeMarcas> listaDeProcessosDaRevistaComMarcaExistente, IList<IProcessoDeMarca> listaDeProcessosDeMarcasComRadicalCadastrado)
        {
            var listaDeMarcasDeClientes = new HashSet<ILeituraRevistaDeMarcas>();
            IList<ILeituraRevistaDeMarcas> listaDeMarcasDeColidentes = new List<ILeituraRevistaDeMarcas>();

            IDictionary<IList<ILeituraRevistaDeMarcas>, IList<ILeituraRevistaDeMarcas>>
                                        dicionarioDeMarcasColidentesEClientes =
                                            new Dictionary
                                                <IList<ILeituraRevistaDeMarcas>, IList<ILeituraRevistaDeMarcas>>();

            var rdn = new Random();

            foreach (var processoDeMarcaDoCliente in listaDeProcessosDeMarcasComRadicalCadastrado)
            {
                var marcaDeCliente = FabricaGenerica.GetInstancia().CrieObjeto<ILeituraRevistaDeMarcas>();
                marcaDeCliente.IdLeitura = rdn.Next(0, int.MaxValue);
                
                foreach (var radicalDaMarca in processoDeMarcaDoCliente.Marca.RadicalMarcas)
                    foreach (var processoDaRevista in listaDeProcessosDaRevistaComMarcaExistente)
                        if (radicalDaMarca.NCL == null ||
                            processoDaRevista.NCL == radicalDaMarca.NCL.Codigo)
                        {

                            if (processoDaRevista.Marca.ToUpper().Contains(radicalDaMarca.DescricaoRadical.ToUpper()))
                            {
                                marcaDeCliente.Marca = processoDeMarcaDoCliente.Marca.DescricaoDaMarca;
                                marcaDeCliente.NCL = processoDeMarcaDoCliente.Marca.NCL.Codigo;
                                marcaDeCliente.NumeroDoProcesso = processoDeMarcaDoCliente.Processo.ToString();
                                marcaDeCliente.Radical = radicalDaMarca.DescricaoRadical;
                                marcaDeCliente.RadicalNCL = radicalDaMarca.NCL == null
                                                                ? "Todos"
                                                                : radicalDaMarca.NCL.Codigo;

                                if (!listaDeMarcasDeClientes.Contains(marcaDeCliente))
                                    listaDeMarcasDeClientes.Add(marcaDeCliente);


                                var marcaColidente = FabricaGenerica.GetInstancia().CrieObjeto<ILeituraRevistaDeMarcas>();

                                marcaColidente.IdLeitura = marcaDeCliente.IdLeitura;
                                marcaColidente.NCL = processoDaRevista.NCL;
                                marcaColidente.Marca = processoDaRevista.Marca;
                                marcaColidente.NumeroDoProcesso = processoDaRevista.NumeroDoProcesso;
                                marcaColidente.CodigoDoDespacho =
                                    !string.IsNullOrEmpty(processoDaRevista.CodigoDoDespacho)
                                        ? processoDaRevista.CodigoDoDespacho
                                        : null;

                                listaDeMarcasDeColidentes.Add(marcaColidente);
                            }
                        }
            }

            dicionarioDeMarcasColidentesEClientes.Add(listaDeMarcasDeColidentes, listaDeMarcasDeClientes.ToList());
            return dicionarioDeMarcasColidentesEClientes;
        }

        public void Excluir(int numeroDaRevistaDeMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRevistaDeMarcas>();

            try
            {
                ServerUtils.BeginTransaction();
                mapeador.Excluir(numeroDaRevistaDeMarcas);
                ServerUtils.CommitTransaction();
            }
            catch
            {
                ServerUtils.RollbackTransaction();
                throw;
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
