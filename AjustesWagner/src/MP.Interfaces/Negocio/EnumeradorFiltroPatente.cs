using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class EnumeradorFiltroPatente
    {
        private int _id;
        private string _descricao;

        public static EnumeradorFiltroPatente NumeroDoProcesso = new EnumeradorFiltroPatente(1, "Número do Processo");
        public static EnumeradorFiltroPatente NumeroDaPatente = new EnumeradorFiltroPatente(2, "Número da Patente");
        public static EnumeradorFiltroPatente NumeroDoPedido = new EnumeradorFiltroPatente(3, "Número do Pedido");
        public static EnumeradorFiltroPatente DataDoDeposito = new EnumeradorFiltroPatente(4, "Data do Depósito");
        public static EnumeradorFiltroPatente PrioridadeUnionista = new EnumeradorFiltroPatente(5, "Prioridade Unionista");
        public static EnumeradorFiltroPatente DataDaPublicacao = new EnumeradorFiltroPatente(6, "Data da Publicação");
        public static EnumeradorFiltroPatente DataDeConcenssao = new EnumeradorFiltroPatente(7, "Data da Concessão");
        public static EnumeradorFiltroPatente ClassificacaoInternacional = new EnumeradorFiltroPatente(8, "Classificação Internacional");
        public static EnumeradorFiltroPatente Titulo = new EnumeradorFiltroPatente(9, "Título");
        public static EnumeradorFiltroPatente Resumo = new EnumeradorFiltroPatente(10, "Resumo");
        public static EnumeradorFiltroPatente PatentePrincipalAdicao = new EnumeradorFiltroPatente(11, "Patente Principal Adição");
        public static EnumeradorFiltroPatente PatentePrincipalDivisao = new EnumeradorFiltroPatente(12, "Patente Principal Divisão");
        public static EnumeradorFiltroPatente PrioridadeInterna = new EnumeradorFiltroPatente(13, "Prioridade Interna");
        public static EnumeradorFiltroPatente NomeDoDepositante = new EnumeradorFiltroPatente(14, "Nome do Depositante");
        public static EnumeradorFiltroPatente NomeDoInventor = new EnumeradorFiltroPatente(15, "Nome do Inventor");
        public static EnumeradorFiltroPatente NomeDoTitular = new EnumeradorFiltroPatente(16, "Nome do Titular");
        public static EnumeradorFiltroPatente PaisesDesignados = new EnumeradorFiltroPatente(17, "Países Designados");
        public static EnumeradorFiltroPatente DataDeInicioFaseNacional = new EnumeradorFiltroPatente(18, "Dt. Início Fase Nacional");
        public static EnumeradorFiltroPatente NumeroIdiomaDataDepositoInternacional = new EnumeradorFiltroPatente(19, "Nº Idioma e Dt. Dep. Int.");
        public static EnumeradorFiltroPatente NumeroIdiomaDataPublicacaoInternacional = new EnumeradorFiltroPatente(20, "Nº Idioma e Dt. Publ. Int.");
        public static EnumeradorFiltroPatente Rp = new EnumeradorFiltroPatente(21, "Rp");
        public static EnumeradorFiltroPatente Estado = new EnumeradorFiltroPatente(22, "Estado");
        public static EnumeradorFiltroPatente NomeDoProcurador = new EnumeradorFiltroPatente(23, "Nome do Procurador");
        public static EnumeradorFiltroPatente CodigoDoRegistro = new EnumeradorFiltroPatente(24, "Código Registro");


        public EnumeradorFiltroPatente(int ID, string Descricao)
        {
            _id = ID;
            _descricao = Descricao;
        }

        private static IList<EnumeradorFiltroPatente> Lista = new List<EnumeradorFiltroPatente>
                                                                  {
                                                                      NumeroDoProcesso,
                                                                      NumeroDaPatente,
                                                                      NumeroDoPedido,
                                                                      DataDoDeposito,
                                                                      PrioridadeUnionista,
                                                                      DataDaPublicacao,
                                                                      DataDeConcenssao,
                                                                      ClassificacaoInternacional,
                                                                      Titulo,
                                                                      Resumo,
                                                                      PatentePrincipalAdicao,
                                                                      PatentePrincipalDivisao,
                                                                      PrioridadeInterna,
                                                                      NomeDoDepositante,
                                                                      NomeDoTitular,
                                                                      NomeDoProcurador,
                                                                      PaisesDesignados,
                                                                      DataDeInicioFaseNacional,
                                                                      NumeroIdiomaDataDepositoInternacional,
                                                                      NumeroIdiomaDataPublicacaoInternacional,
                                                                      Rp,
                                                                  };
        public int Id { get { return _id; } }
        public string Descricao { get { return _descricao; } }

        public static EnumeradorFiltroPatente Obtenha(int ID)
        {
            return Lista.FirstOrDefault(enumerador => enumerador.Id == ID);
        }

        public static IList<EnumeradorFiltroPatente> ObtenhaTodos()
        {
            return Lista;
        }
    }
}
