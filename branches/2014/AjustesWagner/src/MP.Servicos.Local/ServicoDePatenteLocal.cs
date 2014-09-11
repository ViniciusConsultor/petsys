using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDePatenteLocal : Servico, IServicoDePatente
    {
        public ServicoDePatenteLocal(ICredencial Credencial) : base(Credencial)
        {
        }
        
        public IAnuidadePatente ObtenhaAnuidade(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorDePatente.ObtenhaAnuidade(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IClassificacaoPatente ObtenhaClassificacao(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorDePatente.ObtenhaClassificacao(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IPrioridadeUnionistaPatente ObtenhaPrioridadeUnionista(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorDePatente.ObtenhaPrioridadeUnionista(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IInventor ObtenhaInventor(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorDePatente.ObtenhaInventor(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IPatente ObtenhaPatente(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorDePatente.ObtenhaPatente(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }


        public IList<IPatente> ObtenhaPatentesPeloTitulo(string titulo, int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorDePatente.ObtenhaPatentesPeloTitulo(titulo, quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IList<IAnuidadePatente> CalculeAnuidadesPatentesDeNaturezaPIeMU(DateTime dataDeDeposito)
        {
            IList<IAnuidadePatente> ListaDeAnuidadeDaPatente = new List<IAnuidadePatente>();

            DateTime? dataDoUltimoLancamento = null;

            for (int i = 0; i < 18; i++)
            {
                var anuidadeDaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IAnuidadePatente>();

                anuidadeDaPatente.DescricaoAnuidade = i + 3 + "ª ANUIDADE";
                anuidadeDaPatente.DataLancamento = i == 0 ? dataDeDeposito.AddYears(2) : dataDoUltimoLancamento.Value.AddYears(1);
                dataDoUltimoLancamento = anuidadeDaPatente.DataLancamento;
                anuidadeDaPatente.DataVencimentoSemMulta = anuidadeDaPatente.DataLancamento.Value.AddMonths(3);
                anuidadeDaPatente.DataVencimentoComMulta = anuidadeDaPatente.DataVencimentoSemMulta.Value.AddMonths(6);
                anuidadeDaPatente.DataPagamento = null;
                anuidadeDaPatente.ValorPagamento = 0;
                ListaDeAnuidadeDaPatente.Add(anuidadeDaPatente);
            }

            return ListaDeAnuidadeDaPatente;
        }

        public IList<IAnuidadePatente> CalculeAnuidadesPatentesDeNaturezaDI(DateTime dataDeDeposito)
        {
            var ListaDeAnuidadeDaPatente = new List<IAnuidadePatente>();

            DateTime? dataDoUltimoLancamento = null;
            
            for (int i = 1; i <= 4; i++)
            {
                var anuidadeDaPatente = FabricaGenerica.GetInstancia().CrieObjeto<IAnuidadePatente>();

                anuidadeDaPatente.DescricaoAnuidade = i + 1 + "º QUIQUÊNIO";
                anuidadeDaPatente.DataLancamento = i == 1 ? dataDeDeposito.AddYears(4) : dataDoUltimoLancamento.Value.AddYears(5);
                dataDoUltimoLancamento = anuidadeDaPatente.DataLancamento;
                anuidadeDaPatente.DataVencimentoSemMulta = anuidadeDaPatente.DataLancamento.Value.AddYears(1).AddDays(-1);
                anuidadeDaPatente.DataVencimentoComMulta = anuidadeDaPatente.DataVencimentoSemMulta.Value.AddMonths(6);
                anuidadeDaPatente.DataPagamento = null;
                anuidadeDaPatente.ValorPagamento = 0;

                ListaDeAnuidadeDaPatente.Add(anuidadeDaPatente);

                if (i == 2 || i == 3 || i == 4)
                {
                    var anuidadeDaPatenteProrrogacao = FabricaGenerica.GetInstancia().CrieObjeto<IAnuidadePatente>();

                    anuidadeDaPatenteProrrogacao.DescricaoAnuidade = i - 1 + "ª PRORROGAÇÃO";
                    anuidadeDaPatenteProrrogacao.DataLancamento = anuidadeDaPatente.DataLancamento;
                    anuidadeDaPatenteProrrogacao.DataVencimentoSemMulta = anuidadeDaPatente.DataVencimentoSemMulta;
                    anuidadeDaPatenteProrrogacao.DataVencimentoComMulta = anuidadeDaPatente.DataVencimentoComMulta;
                    anuidadeDaPatenteProrrogacao.DataPagamento = anuidadeDaPatente.DataPagamento;
                    anuidadeDaPatenteProrrogacao.ValorPagamento = anuidadeDaPatente.ValorPagamento;
                    ListaDeAnuidadeDaPatente.Add(anuidadeDaPatenteProrrogacao);
                }
            }

            return ListaDeAnuidadeDaPatente;
        }

        public IList<IPatente> ObtenhaPatentesDoCliente(string titulo, long idCliente, int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorDePatente.ObtenhaPatentesDoCliente(titulo, idCliente, quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public ITitular ObtenhaTitular(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorDePatente = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorDePatente.ObtenhaTitular(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }
    }
}
