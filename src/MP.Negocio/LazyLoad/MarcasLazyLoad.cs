using System;
using System.Collections.Generic;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.LazyLoad;
using MP.Interfaces.Servicos;

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
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeMarcas>())
            {
                _marcasReal = servico.obtenhaMarcasPeloId(IdMarca.Value);
            }
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

        public Natureza Natureza
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

        public IList<IRadicalMarcas> RadicalMarcas
        {
            get
            {
                if (_marcasReal == null) CarregueObjetoReal();
                return _marcasReal.RadicalMarcas;
            }
            set
            {
                if (_marcasReal == null) CarregueObjetoReal();
                _marcasReal.RadicalMarcas = value;
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

        public IList<IRadicalMarcas> ObtenhaRadicaisMarcas()
        {
            if (_marcasReal == null) CarregueObjetoReal();
            return _marcasReal.ObtenhaRadicaisMarcas();
        }
    }
}
