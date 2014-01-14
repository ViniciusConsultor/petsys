using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Negocio.Repositorios;

namespace MP.Negocio.LazyLoad
{
    [Serializable]
    public class TitularLazyLoad : ITitularLazyLoad
    {
        private ITitular _ObjetoReal;
        private long _ID;

        public TitularLazyLoad(long ID)
        {
            _ID = ID;
        }

        public void CarregueObjetoReal()
        {
            _ObjetoReal = RepositorioDeTitular.obtenhaInstancia().Obtenha(_ID);
        }

        public DateTime? DataDoCadastro
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.DataDoCadastro;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.DataDoCadastro = value;
            }
        }

        public string InformacoesAdicionais
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.InformacoesAdicionais;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.InformacoesAdicionais = value;
            }
        }

        public Compartilhados.Interfaces.Core.Negocio.IPessoa Pessoa
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.Pessoa;
            }
        }
    }
}