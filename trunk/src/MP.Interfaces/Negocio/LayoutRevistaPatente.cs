using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class LayoutRevistaPatente
    {
        private string identificadorCampo;
        private string descricaoCampo;

        public static LayoutRevistaPatente Layout11 = new LayoutRevistaPatente("(11)", "Número da Patente");
        public static LayoutRevistaPatente Layout21 = new LayoutRevistaPatente("(21)", "Número do Pedido");
        public static LayoutRevistaPatente Layout22 = new LayoutRevistaPatente("(22)", "Data do Depósito");
        public static LayoutRevistaPatente Layout30 = new LayoutRevistaPatente("(30)", "Dados da Prioridade Unionista (data de depósito, país, número)");
        public static LayoutRevistaPatente Layout43 = new LayoutRevistaPatente("(43)", "Data da Publicação do Pedido");
        public static LayoutRevistaPatente Layout45 = new LayoutRevistaPatente("(45)", "Data da Concessão da Patente/Certificado de Adição de Invenção");
        public static LayoutRevistaPatente Layout51 = new LayoutRevistaPatente("(51)", "Classificação Internacional");
        public static LayoutRevistaPatente Layout54 = new LayoutRevistaPatente("(54)", "Título");
        public static LayoutRevistaPatente Layout57 = new LayoutRevistaPatente("(57)", "Resumo");
        public static LayoutRevistaPatente Layout61 = new LayoutRevistaPatente("(61)", "Dados do Pedido ou patente principal do qual o presente é uma adição (número e data de depósito)");
        public static LayoutRevistaPatente Layout62 = new LayoutRevistaPatente("(62)", "Dados do pedido original do qual o presente é uma divisão (número e dadta de depósito)");
        public static LayoutRevistaPatente Layout66 = new LayoutRevistaPatente("(66)", "Dados da Prioridade Interna (número e data de depósito)");
        public static LayoutRevistaPatente Layout71 = new LayoutRevistaPatente("(71)", "Nome do Depositante");
        public static LayoutRevistaPatente Layout72 = new LayoutRevistaPatente("(72)", "Nome do Inventor");
        public static LayoutRevistaPatente Layout73 = new LayoutRevistaPatente("(73)", "Nome do Titular");
        public static LayoutRevistaPatente Layout74 = new LayoutRevistaPatente("(74)", "Nome do Procurador");
        public static LayoutRevistaPatente Layout81 = new LayoutRevistaPatente("(81)", "Países Designados");
        public static LayoutRevistaPatente Layout85 = new LayoutRevistaPatente("(85)", "Data do Início da Fase Nacional");
        public static LayoutRevistaPatente Layout86 = new LayoutRevistaPatente("(86)", "Número, Idioma e Data do Depósito Internacional");
        public static LayoutRevistaPatente Layout87 = new LayoutRevistaPatente("(87)", "Número, Idioma e Data da Publicação Internacional"); 

        private static IList<LayoutRevistaPatente> layoutsRevistaPatente = new List<LayoutRevistaPatente>();

        private LayoutRevistaPatente(string identificador, string descricao)
        {
            identificadorCampo = identificador;
            descricaoCampo = descricao;
        }

        public string IdentificadorCampo
        {
            get { return identificadorCampo; }
            set { identificadorCampo = value; }
        }

        public string DescricaoCampo
        {
            get { return descricaoCampo; }
            set { descricaoCampo = value; }
        }

        public static IList<LayoutRevistaPatente> ObtenhaTodas()
        {
            return layoutsRevistaPatente;
        }

        public static LayoutRevistaPatente ObtenhaPorIdentificador(int identificador)
        {
            return ObtenhaTodas().FirstOrDefault(layout => layout.IdentificadorCampo.Equals(identificador));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as LayoutRevistaPatente;
            return objeto != null && objeto.IdentificadorCampo == IdentificadorCampo;
        }

        public override int GetHashCode()
        {
            return identificadorCampo.GetHashCode();
        }
    }
}
