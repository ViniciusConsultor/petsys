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
                return _ID;
            }
            set
            {
                _ID = value;
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


        public IInventor Iventor
        {
            get
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                return _ObjetoReal.Iventor;
            }
            set
            {
                if (_ObjetoReal == null) CarregueObjetoReal();
                _ObjetoReal.Iventor = value;
            }
        }
    }
}