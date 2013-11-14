using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IRevistaDeMarcas
    {
        long? IdRevistaMarcas { get; set; }
        int NumeroRevistaMarcas { get; set; }
        Nullable<DateTime> DataPublicacao { get; set; }
        Nullable<DateTime> DataProcessamento { get; set; }
        long NumeroProcessoDeMarca { get; set; }
        string CodigoDespachoAnterior { get; set; }
        string CodigoDespachoAtual { get; set; }
        string Apostila { get; set; }
        string TextoDoDespacho { get; set; }
        bool Processada { get; set; }
        string ExtensaoArquivo { get; set; }
        Nullable<DateTime> DataDeConcessao { get; set; }
        Nullable<DateTime> DataDeDeposito { get; set; }
    }
}
