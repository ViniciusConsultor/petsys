using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;
using MP.Negocio.Repositorios;

namespace MP.Negocio.LazyLoad
{
    [Serializable]
    public class PrioridadeUnionistaPatenteLazyLoad : IPrioridadeUnionistaPatenteLazyLoad
    {
        private IPrioridadeUnionistaPatente _ObjetoReal;
        private long _ID;

        public PrioridadeUnionistaPatenteLazyLoad(long Id)
        {
            _ID = Id;
        }

        public void CarregueObjetoReal()
        {
            _ObjetoReal = RepositorioPrioridadeUnionistaPatente.obtenhaInstancia().ObtenhaPrioridadeUnionista(_ID);
        }

        public long Identificador
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public DateTime? DataPrioridade
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.DataPrioridade;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.DataPrioridade = value;
            }
        }

        public string NumeroPrioridade
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.NumeroPrioridade;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.NumeroPrioridade = value;
            }
        }

        public IPais Pais
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.Pais;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.Pais = value;
            }
        }
    }
}
