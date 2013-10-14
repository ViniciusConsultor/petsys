using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class Natureza
    {
        private int _codigo;
        private string _nome;

        public static Natureza DeProduto = new Natureza(1, "De produto");
        public static Natureza DeServico = new Natureza(2, "De serviço");
        public static Natureza Coletiva = new Natureza(3,"Coletiva");
        public static Natureza Certificacao = new Natureza(4, "Certificação");

        private static IList<Natureza> naturezas = new List<Natureza>() {DeProduto, DeServico, Coletiva, Certificacao};
        
        private Natureza(int codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
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

        public static IList<Natureza> ObtenhaTodas()
        {
            return naturezas;
        }

        public static Natureza ObtenhaPorCodigo(string codigo)
        {
            return naturezas.FirstOrDefault(natureza => natureza.Codigo.Equals(codigo));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as Natureza;

            return objeto != null && objeto.Codigo == Codigo;
        }

        public override int GetHashCode()
        {
            return _codigo.GetHashCode();
        }
    }
}