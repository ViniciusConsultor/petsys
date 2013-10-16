﻿using System;
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

        public void Insira(IPatente patente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                mapeadorProcurador.Insira(patente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public void Modificar(IPatente patente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                mapeadorProcurador.Modificar(patente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public void Exluir(int codigoPatente)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                mapeadorProcurador.Exluir(codigoPatente);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IAnuidadePatente ObtenhaAnuidade(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaAnuidade(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IClassificacaoPatente ObtenhaClassificacao(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaClassificacao(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IPrioridadeUnionistaPatente ObtenhaPrioridadeUnionista(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaPrioridadeUnionista(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public ITitularPatente ObtenhaTitular(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaTitular(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }

        public IPatente ObtenhaPatente(long id)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaPatente(id);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }


        public IList<IPatente> ObtenhaPatentesPeloTitulo(string titulo, int quantidadeDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);
            var mapeadorProcurador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDePatente>();

            try
            {
                return mapeadorProcurador.ObtenhaPatentesPeloTitulo(titulo, quantidadeDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }            
        }
    }
}
