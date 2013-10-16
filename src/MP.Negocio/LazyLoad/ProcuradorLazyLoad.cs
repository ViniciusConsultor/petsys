using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;

namespace MP.Negocio.LazyLoad
{
    [Serializable]
    public class ProcuradorLazyLoad : IProcuradorLazyLoad
    {
        private IProcurador _ObjetoReal;
        private long _ID;

        public ProcuradorLazyLoad(long Id)
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
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
                _ObjetoReal = servico.ObtenhaProcurador(_ID);
        }

        public string MatriculaAPI
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.MatriculaAPI;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.MatriculaAPI = value;
            }
        }

        public string SiglaOrgaoProfissional
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.SiglaOrgaoProfissional;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.SiglaOrgaoProfissional = value;
            }
        }

        public string NumeroRegistroProfissional
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.NumeroRegistroProfissional;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.NumeroRegistroProfissional = value;
            }
        }

        public DateTime? DataRegistroProfissional
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DataRegistroProfissional;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DataRegistroProfissional = value;
            }
        }

        public string ObservacaoContato
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.ObservacaoContato;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.ObservacaoContato = value;
            }
        }

        public IPessoa Pessoa
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.Pessoa;
            }
        }
    }
}
