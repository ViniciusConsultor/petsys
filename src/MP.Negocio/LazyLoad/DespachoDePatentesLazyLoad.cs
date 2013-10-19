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
    public class DespachoDePatentesLazyLoad : IDespachoDePatentesLazyLoad
    {
        private IDespachoDePatentes _ObjetoReal;
        private long _ID;

        public DespachoDePatentesLazyLoad(long Id)
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
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
                _ObjetoReal = servico.obtenhaDespachoDePatentesPeloId(_ID);
        }

        public long? IdDespachoDePatente
        {
            get { return _ID; }
            set { _ID = value.Value; }
        }

        public string CodigoDespachoDePatente
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.CodigoDespachoDePatente;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.CodigoDespachoDePatente = value;
            }
        }

        public string DetalheDespachoDePatente
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DetalheDespachoDePatente;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DetalheDespachoDePatente = value;
            }
        }

        public string DescricaoDespachoDePatente
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DescricaoDespachoDePatente;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DescricaoDespachoDePatente = value;
            }
        }

        public SituacaoDoProcessoDePatente SituacaoDoProcessoDePatente
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.SituacaoDoProcessoDePatente;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.SituacaoDoProcessoDePatente = value;
            }
        }
    }
}
