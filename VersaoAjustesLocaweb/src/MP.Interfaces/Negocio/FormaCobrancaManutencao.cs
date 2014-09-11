using System;
using System.Collections.Generic;
using System.Linq;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class FormaCobrancaManutencao
    {
        private string _codigo;
        private string _descricao;

        public static FormaCobrancaManutencao SalarioMinimo = new FormaCobrancaManutencao("S", "Salário mínimo");
        public static FormaCobrancaManutencao ValorFixo = new FormaCobrancaManutencao("R", "Valor fixo");
        
        private static IList<FormaCobrancaManutencao> formasDePagamento = new List<FormaCobrancaManutencao>() { ValorFixo, SalarioMinimo};

        private FormaCobrancaManutencao(string codigo, string descricao)
        {
            Codigo = codigo;
            Descricao = descricao;
        }

        public string Codigo
        {
            get { return _codigo; }
            private set { _codigo = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            private set { _descricao = value; }
        }

        public static IList<FormaCobrancaManutencao> ObtenhaTodas()
        {
            return formasDePagamento;
        }

        public static FormaCobrancaManutencao ObtenhaPorCodigo(string codigo)
        {
            return formasDePagamento.FirstOrDefault(forma => forma.Codigo.Equals(codigo));
        }

        public static FormaCobrancaManutencao ObtenhaPorNome(string descricao)
        {
            return formasDePagamento.FirstOrDefault(forma => forma.Descricao.Equals(descricao, StringComparison.InvariantCultureIgnoreCase));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as FormaCobrancaManutencao;

            return objeto != null && objeto.Codigo == Codigo;
        }

        public override int GetHashCode()
        {
            return _codigo.GetHashCode();
        }

    }
}
