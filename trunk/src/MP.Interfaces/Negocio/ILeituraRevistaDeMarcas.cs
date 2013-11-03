using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface ILeituraRevistaDeMarcas
    {
        string NumeroDoProcesso { get; set; }
        string DataDeDeposito { get; set; }
        string DataDeConcessao { get; set; }
        string DataDeVigencia { get; set; }
        string CodigoDoDespacho { get; set; }
        string TextoDoDespacho { get; set; }
        string Titular { get; set; }
        string Pais { get; set; }
        string Uf { get; set; }
        string Marca { get; set; }
        string Apresentacao { get; set; }
        string Natureza { get; set; }
        string TraducaoDaMarca { get; set; }
        string NCL { get; set; }
        string EdicaoNCL { get; set; }
        string EspecificacaoNCL { get; set; }
        IClasseViena ClasseViena { get; set; }
        IClasseNacional ClasseNacional { get; set; }
        string Apostila { get; set; }
        string Procurador { get; set; }
        string DataPrioridadeUnionista { get; set; }
        string NumeroPrioridadeUnionista { get; set; }
        string PaisPrioridadeUnionista { get; set; }
        IDictionary<string, string> DicionarioSobrestadores { get; set; }
    }
}
