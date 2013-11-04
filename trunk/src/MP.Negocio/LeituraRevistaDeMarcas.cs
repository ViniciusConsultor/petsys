using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class LeituraRevistaDeMarcas : ILeituraRevistaDeMarcas
    {
        public long? IdLeitura
        { get; set; }

        public string NumeroDoProcesso { get; set; }

        public string DataDeDeposito
        { get; set; }

        public string DataDeConcessao
        { get; set; }

        public string DataDeVigencia
        { get; set; }

        public string CodigoDoDespacho
        { get; set; }

        public string TextoDoDespacho
        { get; set; }

        public string Titular
        { get; set; }

        public string Pais
        { get; set; }

        public string Uf
        { get; set; }

        public string Marca
        { get; set; }

        public string Apresentacao
        { get; set; }

        public string Natureza
        { get; set; }

        public string TraducaoDaMarca
        { get; set; }

        public string NCL
        { get; set; }

        public string EdicaoNCL
        { get; set; }

        public string EspecificacaoNCL
        { get; set; }

        public IClasseViena ClasseViena
        { get; set; }

        public IClasseNacional ClasseNacional
        { get; set; }

        public string Apostila
        { get; set; }

        public string Procurador
        { get; set; }

        public string DataPrioridadeUnionista
        { get; set; }

        public string NumeroPrioridadeUnionista
        { get; set; }

        public string PaisPrioridadeUnionista
        { get; set; }

        public IDictionary<string, string> DicionarioSobrestadores
        { get; set; }
    }
}
