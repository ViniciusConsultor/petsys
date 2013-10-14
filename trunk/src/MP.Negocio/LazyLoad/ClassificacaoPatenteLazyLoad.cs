using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;

namespace MP.Negocio.LazyLoad
{
    public class ClassificacaoPatenteLazyLoad : IClassificacaoPatenteLazyLoad
    {
        private IClassificacaoPatente _ObjetoReal;
        private long _ID;

        public ClassificacaoPatenteLazyLoad(long Id)
        {
            _ID = Id;
        }

        public void CarregueObjetoReal()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
                _ObjetoReal = servico.ObtenhaClassificacao(_ID);
        }

        public long Identificador
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.Identificador;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.Identificador = value;
            }
        }

        public string Classificacao
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.Classificacao;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.Classificacao = value;
            }
        }

        public string DescricaoClassificacao
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.DescricaoClassificacao;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.DescricaoClassificacao = value;
            }
        }

        public TipoClassificacaoPatente TipoClassificacao
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.TipoClassificacao;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.TipoClassificacao = value;
            }
        }
    }
}
