using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class Periodo
    {
        private int _codigo;
        private string _descricao;

        public static Periodo Diario = new Periodo(1, "Diário");
        public static Periodo Semanal = new Periodo(2, "Semanal");
        public static Periodo Mensal = new Periodo(3, "Mensal");
        public static Periodo Trimestral = new Periodo(4, "Trimestral");
        public static Periodo Semestral = new Periodo(5, "Semestral");
        public static Periodo Anual = new Periodo(6, "Anual");

        private static IList<Periodo> periodos = new List<Periodo>() { Diario, Semanal, Mensal, Trimestral, Semestral, Anual };

        private Periodo(int codigo, string descricao)
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

        public static IList<Periodo> ObtenhaTodas()
        {
            return periodos;
        }

        public static Periodo ObtenhaPorCodigo(int codigo)
        {
            return periodos.FirstOrDefault(natureza => natureza.Codigo.Equals(codigo));
        }

        public static Periodo ObtenhaPorNome(string descricao)
        {
            return periodos.FirstOrDefault(periodo => periodo.Descricao.Equals(descricao, StringComparison.InvariantCultureIgnoreCase));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as Periodo;

            return objeto != null && objeto.Codigo == Codigo;
        }

        public override int GetHashCode()
        {
            return _codigo.GetHashCode();
        }
    }
}
