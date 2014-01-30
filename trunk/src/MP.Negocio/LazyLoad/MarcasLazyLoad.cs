using System;
using System.Collections.Generic;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;
using MP.Negocio.Repositorios;

namespace MP.Negocio.LazyLoad
{
    [Serializable]
    public class MarcasLazyLoad : IMarcasLazyLoad
    {
        private IMarcas _marcasReal;

        public MarcasLazyLoad(long ID)
        {
            IdMarca = ID;
        }

        public void CarregueObjetoReal()
        {
            _marcasReal = RepositorioDeMarcas.obtenhaInstancia().obtenhaMarcasPeloId(IdMarca.Value);
        }

        private Nullable<long> _idMarca;
        public long? IdMarca
        {
            get { return _idMarca; }
            set { _idMarca = value; }
        }

        public NCL NCL
        {
            get
            {   if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.NCL;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.NCL = value;
            }
        }

        public Apresentacao Apresentacao
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.Apresentacao;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.Apresentacao = value;
            }
        }

        public ICliente Cliente
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.Cliente;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.Cliente = value;
            }
        }

        public NaturezaDeMarca Natureza
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.Natureza;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.Natureza = value;
            }
        }

        public string DescricaoDaMarca
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.DescricaoDaMarca;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.DescricaoDaMarca = value;
            }
        }

        public string EspecificacaoDeProdutosEServicos
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.EspecificacaoDeProdutosEServicos;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.EspecificacaoDeProdutosEServicos = value;
            }
        }

        public string ImagemDaMarca
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.ImagemDaMarca;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.ImagemDaMarca = value;
            }
        }

        public string ObservacaoDaMarca
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.ObservacaoDaMarca;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.ObservacaoDaMarca = value;
            }
        }

        public Nullable<int> CodigoDaClasse
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.CodigoDaClasse;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                 _marcasReal.CodigoDaClasse = value;
            }
        }

        public Nullable<int> CodigoDaSubClasse1
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.CodigoDaSubClasse1;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.CodigoDaSubClasse1 = value;
            }
        }

        public Nullable<int> CodigoDaSubClasse2
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.CodigoDaSubClasse2;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.CodigoDaSubClasse2 = value;
            }
        }

        public Nullable<int> CodigoDaSubClasse3
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.CodigoDaSubClasse3;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.CodigoDaSubClasse3 = value;
            }
        }

        public IList<IRadicalMarcas> RadicalMarcas { get; set; }

        public bool PagaManutencao
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.PagaManutencao;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.PagaManutencao = value;
            }
            
        }

        public string Periodo
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.Periodo;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.Periodo = value;
            }
        }

        public string FormaDeCobranca
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.FormaDeCobranca;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.FormaDeCobranca = value;
            }
        }

        public double ValorDeCobranca
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.ValorDeCobranca;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.ValorDeCobranca = value;
            }
        }

        public string Mes
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.Mes;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.Mes = value;
            }
        }

        public void AdicioneRadicalMarcas(IRadicalMarcas radicalMarcas)
        {
            if (_marcasReal == null) CarregueObjetoReal();
            _marcasReal.AdicioneRadicalMarcas(radicalMarcas);
        }

        public void AdicioneRadicaisMarcas(IList<IRadicalMarcas> listaRadicalMarcas)
        {
            if (_marcasReal == null) CarregueObjetoReal();
            _marcasReal.AdicioneRadicaisMarcas(listaRadicalMarcas);
        }

    }
}
