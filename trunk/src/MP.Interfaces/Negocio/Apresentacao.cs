using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class Apresentacao
    {
        private int _codigo;
        private string _nome;
        private bool _possuiFigura;
        private bool _possuiDescricao;

        public static Apresentacao Nominativa = new Apresentacao(1, "Nominativa", false, true);
        public static Apresentacao Mista = new Apresentacao(2, "Mista",true, true);
        public static Apresentacao Figurativa = new Apresentacao(3,"Figurativa",true, false);
        public static Apresentacao Tridimensional = new Apresentacao(4, "Tridimensional", true, true);

        private static IList<Apresentacao> apresentacoes = new List<Apresentacao>() { Nominativa, Mista, Figurativa, Tridimensional };

        private Apresentacao(int codigo, string nome, bool possuiFigura, bool possuiDescricao)
        {
            Codigo = codigo;
            Nome = nome;
            PossuiFigura = possuiFigura;
            PossuiDescricao = possuiDescricao;
        }

        public int Codigo
        {
            get { return _codigo; }
            private set { _codigo = value; }
        }

        public string Nome
        {
            get { return _nome; }
            private set { _nome = value; }
        }

        public bool PossuiFigura
        {
            get { return _possuiFigura; }
            private set { _possuiFigura = value; }
        }

        public bool PossuiDescricao
        {
            get { return _possuiDescricao; }
            private set { _possuiDescricao = value; }
        }

        public static IList<Apresentacao> ObtenhaTodas()
        {
            return apresentacoes;
        }

        public static Apresentacao ObtenhaPorCodigo(int codigo)
        {
            return apresentacoes.FirstOrDefault(apresentacao => apresentacao.Codigo.Equals(codigo));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as Apresentacao;

            return objeto != null && objeto.Codigo == Codigo;
        }

        public override int GetHashCode()
        {
            return _codigo.GetHashCode();
        }
    }
}
