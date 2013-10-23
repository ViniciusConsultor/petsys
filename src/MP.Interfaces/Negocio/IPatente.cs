using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IPatente
    {
        long Identificador { get; set; }
        string TituloPatente { get; set; }
        INaturezaPatente NaturezaPatente { get; set; }
        bool ObrigacaoGerada { get; set; }
        DateTime? DataCadastro { get; set; }
        string Observacao { get; set; }
        string Resumo { get; set; }
        int QuantidadeReivindicacao { get; set; }
        IList<IAnuidadePatente> Anuidades { get; set; }
        IList<IClassificacaoPatente> Classificacoes { get; set; }
        IList<IPrioridadeUnionistaPatente> PrioridadesUnionista { get; set; }
        IList<ITitularPatente> Titulares { get; set; }
    }
}
