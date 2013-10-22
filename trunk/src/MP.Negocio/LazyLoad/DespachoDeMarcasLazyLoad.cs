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
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
                _ObjetoReal = servico.obtenhaDespachoDeMarcasPeloId(_ID);
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

        public string DetalheDespacho
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DetalheDespacho;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DetalheDespacho = value;
            }
        }

        public SituacaoDoProcessoDeMarca SituacaoProcesso
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

        public bool Registro
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.Registro;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.Registro = value;
            }
        }
    }
}
