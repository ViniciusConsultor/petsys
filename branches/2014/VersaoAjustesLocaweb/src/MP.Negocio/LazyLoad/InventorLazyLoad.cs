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
    public class InventorLazyLoad : IInventorLazyLoad
    {
        private IInventor _ObjetoReal;
        private long _ID;

        public InventorLazyLoad(long ID)
        {
            _ID = ID;
        }

        public void CarregueObjetoReal()
        {
            _ObjetoReal = RepositorioDeInventor.obtenhaInstancia().Obtenha(_ID);
        }

        public IPessoa Pessoa
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.Pessoa;
            }
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
    }
}
