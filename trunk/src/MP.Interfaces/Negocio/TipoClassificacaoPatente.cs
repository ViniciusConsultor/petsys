using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class TipoClassificacaoPatente
    {
        private int codigo;
        private string descricao;

        public static TipoClassificacaoPatente TipoClassificacaoPatente1 = new TipoClassificacaoPatente(1, "Internacional");
        public static TipoClassificacaoPatente TipoClassificacaoPatente2 = new TipoClassificacaoPatente(2, "Nacional");

        private TipoClassificacaoPatente(int codigo, string descricao)
        {

        }

        public int Codigo
        {
            get { return codigo; }
            private set { codigo = value; }
        }

        public string Descricao
        {
            get { return descricao; }
            private set { descricao = value; }
        }

        private static IList<TipoClassificacaoPatente> todosTiposClassificacaoPatente = new List<TipoClassificacaoPatente>()
                                                                                            {
                                                                                                TipoClassificacaoPatente1,
                                                                                                TipoClassificacaoPatente2
                                                                                            };
        public static IList<TipoClassificacaoPatente> ObtenhaTodas()
        {
            return todosTiposClassificacaoPatente;
        }

        public static TipoClassificacaoPatente ObtenhaPorCodigo(int codigo)
        {
            return ObtenhaTodas().FirstOrDefault(tipoClassificacaoPatente => tipoClassificacaoPatente.Codigo.Equals(codigo));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as NCL;

            return objeto != null && objeto.Codigo == Codigo;
        }

        public override int GetHashCode()
        {
            return codigo.GetHashCode();
        }

    }
}
