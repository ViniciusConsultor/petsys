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

        public static LayoutRevistaPatente LayoutRevistaPatente1 = new LayoutRevistaPatente("(11)", "Número da Patente");
        public static LayoutRevistaPatente LayoutRevistaPatente2 = new LayoutRevistaPatente("(21)", "Número do Pedido");
        public static LayoutRevistaPatente LayoutRevistaPatente3 = new LayoutRevistaPatente("(22)", "Data do Depósito");
        public static LayoutRevistaPatente LayoutRevistaPatente4 = new LayoutRevistaPatente("(30)", "Dados da Prioridade Unionista (data de depósito, país, número)");
        public static LayoutRevistaPatente LayoutRevistaPatente5 = new LayoutRevistaPatente("(43)", "Data da Publicação do Pedido");
        public static LayoutRevistaPatente LayoutRevistaPatente6 = new LayoutRevistaPatente("(45)", "Data da Concessão da Patente/Certificado de Adição de Invenção");
        public static LayoutRevistaPatente LayoutRevistaPatente7 = new LayoutRevistaPatente("(51)", "Classificação Internacional");
        public static LayoutRevistaPatente LayoutRevistaPatente8 = new LayoutRevistaPatente("(54)", "Título");
        public static LayoutRevistaPatente LayoutRevistaPatente9 = new LayoutRevistaPatente("(57)", "Resumo");
        public static LayoutRevistaPatente LayoutRevistaPatente10 = new LayoutRevistaPatente("(61)", "Dados do Pedido ou patente principal do qual o presente é uma adição (número e data de depósito)");
        public static LayoutRevistaPatente LayoutRevistaPatente11 = new LayoutRevistaPatente("(62)", "Dados do pedido original do qual o presente é uma divisão (número e dadta de depósito)");
        public static LayoutRevistaPatente LayoutRevistaPatente12 = new LayoutRevistaPatente("(66)", "Dados da Prioridade Interna (número e data de depósito)");
        public static LayoutRevistaPatente LayoutRevistaPatente13 = new LayoutRevistaPatente("(71)", "Nome do Depositante");
        public static LayoutRevistaPatente LayoutRevistaPatente14 = new LayoutRevistaPatente("(72)", "Nome do Inventor");
        public static LayoutRevistaPatente LayoutRevistaPatente15 = new LayoutRevistaPatente("(73)", "Nome do Titular");
        public static LayoutRevistaPatente LayoutRevistaPatente16 = new LayoutRevistaPatente("(74)", "Nome do Procurador");
        public static LayoutRevistaPatente LayoutRevistaPatente17 = new LayoutRevistaPatente("(81)", "Países Designados");
        public static LayoutRevistaPatente LayoutRevistaPatente18 = new LayoutRevistaPatente("(85)", "Data do Início da Fase Nacional");
        public static LayoutRevistaPatente LayoutRevistaPatente19 = new LayoutRevistaPatente("(86)", "Número, Idioma e Data do Depósito Internacional");
        public static LayoutRevistaPatente LayoutRevistaPatente20 = new LayoutRevistaPatente("(87)", "Número, Idioma e Data da Publicação Internacional"); 

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
