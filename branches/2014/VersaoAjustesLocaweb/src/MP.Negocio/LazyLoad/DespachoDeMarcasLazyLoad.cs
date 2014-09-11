﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;
using MP.Negocio.Repositorios;

namespace MP.Negocio.LazyLoad
{
    [Serializable]
    public class DespachoDeMarcasLazyLoad : IDespachoDeMarcasLazyLoad
    {
        private IDespachoDeMarcas _ObjetoReal;
        private long _ID;

        public DespachoDeMarcasLazyLoad(long Id)
        {
            _ID = Id;
        }

        private void VerifiqueSeObjetoEstaCarregado()
        {
            if (_ObjetoReal == null)
                CarregueObjetoReal();
        }

        public void CarregueObjetoReal()
        {
            _ObjetoReal = RepositorioDeDespachodeMarcas.obtenhaInstancia().obtenhaDespachoDeMarcasPeloId(_ID);
        }

        public long? IdDespacho
        {
            get { return _ID; }
            set { _ID = value.Value; }
        }

        public string CodigoDespacho
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.CodigoDespacho;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.CodigoDespacho = value;
            }
        }

        public string DescricaoDespacho
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DescricaoDespacho;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DescricaoDespacho = value;
            }
        }

        public string SituacaoProcesso
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.SituacaoProcesso;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.SituacaoProcesso = value;
            }
        }

        public int PrazoParaProvidenciaEmDias
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.PrazoParaProvidenciaEmDias;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.PrazoParaProvidenciaEmDias = value;
            }
        }

        public string Providencia
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.Providencia;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.Providencia = value;
            }
        }

        public bool DesativaProcesso
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DesativaProcesso;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DesativaProcesso = value; 
            }
        }

        public bool DesativaPesquisaDeColidencia
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DesativaPesquisaDeColidencia;
            }
            set { VerifiqueSeObjetoEstaCarregado();
            _ObjetoReal.DesativaPesquisaDeColidencia = value;
            }
        }

        public ITemplateDeEmail TemplateDeEmail
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.TemplateDeEmail;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.TemplateDeEmail = value;
            }
        }
    }
}
