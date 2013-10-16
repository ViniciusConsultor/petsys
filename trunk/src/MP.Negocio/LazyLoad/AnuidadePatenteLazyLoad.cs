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
    public class AnuidadePatenteLazyLoad : IAnuidadePatenteLazyLoad
    {
        private IAnuidadePatente _ObjetoReal;
        private long _ID;

        public AnuidadePatenteLazyLoad(long Id)
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
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
                _ObjetoReal = servico.ObtenhaAnuidade(_ID);
        }

        public long Identificador
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.Identificador;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.Identificador = value;
            }
        }

        public string DescricaoAnuidade
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DescricaoAnuidade;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DescricaoAnuidade = value;
            }
        }

        public DateTime? DataLancamento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DataLancamento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DataLancamento = value;
            }
        }

        public DateTime? DataVencimento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DataVencimento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DataVencimento = value;
            }
        }

        public DateTime? DataPagamento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DataPagamento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DataPagamento = value;
            }
        }

        public double ValorPagamento
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.ValorPagamento;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.ValorPagamento = value;
            }
        }

        public bool AnuidadePaga
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.AnuidadePaga;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.AnuidadePaga = value;
            }
        }

        public bool PedidoExame
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.PedidoExame;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.PedidoExame = value;
            }
        }

        public DateTime? DataVencimentoSemMulta
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DataVencimentoSemMulta;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DataVencimentoSemMulta = value;
            }
        }

        public DateTime? DataVencimentoComMulta
        {
            get
            {
                VerifiqueSeObjetoEstaCarregado();
                return _ObjetoReal.DataVencimentoComMulta;
            }
            set
            {
                VerifiqueSeObjetoEstaCarregado();
                _ObjetoReal.DataVencimentoComMulta = value;
            }
        }
    }
}