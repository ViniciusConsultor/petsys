using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Fabricas;
using MP.Interfaces.Servicos;

namespace MP.Interfaces.Negocio.LazyLoad
{
    public class TitularPatenteLazyLoad : ITitularPatenteLazyLoad
    {
        private ITitularPatente _ObjetoReal;
        private long _ID;

        public TitularPatenteLazyLoad(long Id)
        {
            _ID = Id;
        }

        public void CarregueObjetoReal()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
                _ObjetoReal = servico.ObtenhaTitular(_ID);   
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

        public IProcurador Procurador
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.Procurador;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.Procurador = value;
            }
        }

        public string ContatoTitular
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.ContatoTitular;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.ContatoTitular = value;
            }
        }
    }
}