using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class Mes
    {
        private int _codigo;
        private string _descricao;

        public static Mes Janeiro = new Mes(1, "Janeiro");
        public static Mes Fevereiro = new Mes(2, "Fevereiro");
        public static Mes Marco = new Mes(3, "Marco");
        public static Mes Abril = new Mes(4, "Abril");
        public static Mes Maio = new Mes(5, "Maio");
        public static Mes Junho = new Mes(6, "Junho");
        public static Mes Julho = new Mes(7, "Julho");
        public static Mes Agosto = new Mes(8, "Agosto");
        public static Mes Setembro = new Mes(9, "Setembro");
        public static Mes Outubro = new Mes(10, "Outubro");
        public static Mes Novembro = new Mes(11, "Novembro");
        public static Mes Dezembro = new Mes(12, "Dezembro");

        private static IList<Mes> meses = new List<Mes>() { Janeiro, Fevereiro, Marco, Abril, Maio, 
            Junho, Julho, Agosto, Setembro, Outubro, Novembro, Dezembro};

        private Mes(int codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public int Codigo
        {
            get { return _codigo; }
            private set { _codigo = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            private set { _descricao = value; }
        }

        public static IList<Mes> ObtenhaTodas()
        {
            return meses;
        }

        public static Mes ObtenhaPorCodigo(int codigo)
        {
            return meses.FirstOrDefault(mes => mes.Codigo.Equals(codigo));
        }

        public static Mes ObtenhaPorNome(string descricao)
        {
            return meses.FirstOrDefault(mes => mes.Descricao.Equals(descricao, StringComparison.InvariantCultureIgnoreCase));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as Mes;

            return objeto != null && objeto.Codigo == Codigo;
        }

        public override int GetHashCode()
        {
            return _codigo.GetHashCode();
        }
    }
}
