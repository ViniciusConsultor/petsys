using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IRevistaDePatente
    {
        long? IdRevistaPatente { get; set; }
        int NumeroRevistaPatente { get; set; }
        DateTime DataPublicacao { get; set; }
        DateTime DataProcessamento { get; set; }
        long NumeroProcessoDePatente { get; set; }
        string CodigoDespachoAnterior { get; set; }
        string CodigoDespachoAtual { get; set; }
        string Apostila { get; set; }
        string TextoDoDespacho { get; set; }
        bool Processada { get; set; }
        string ExtensaoArquivo { get; set; }
        DateTime DataDeConcessao { get; set; }
        DateTime DataDeDeposito { get; set; }
    }
}
