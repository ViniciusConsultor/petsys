using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public  class NaturezaDeMarca
    {
         private int _codigo;
        private string _nome;

        public static NaturezaDeMarca DeProduto = new NaturezaDeMarca(1, "De produto");
        public static NaturezaDeMarca DeServico = new NaturezaDeMarca(2, "De serviço");
        public static NaturezaDeMarca Coletiva = new NaturezaDeMarca(3, "Coletiva");
        public static NaturezaDeMarca Certificacao = new NaturezaDeMarca(4, "Certificação");

        private static IList<NaturezaDeMarca> naturezas = new List<NaturezaDeMarca>() { DeProduto, DeServico, Coletiva, Certificacao };

        private NaturezaDeMarca(int codigo, string nome)
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

        public static IList<NaturezaDeMarca> ObtenhaTodas()
        {
            return naturezas;
        }

        public static NaturezaDeMarca ObtenhaPorCodigo(int codigo)
        {
            return naturezas.FirstOrDefault(natureza => natureza.Codigo.Equals(codigo));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as NaturezaDeMarca;

            return objeto != null && objeto.Codigo == Codigo;
        }

        public override int GetHashCode()
        {
            return _codigo.GetHashCode();
        }
    }
}
