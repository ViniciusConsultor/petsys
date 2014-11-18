using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

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
        IList<IInventor> Inventores { get; set; }
        IList<ICliente> Clientes { get; set; }
        IList<IRadicalPatente> Radicais { get; set; }
        IList<ITitular> Titulares { get; set; }
        string ClassificacoesConcatenadas { get; }
        IManutencao Manutencao { get; set; }
        bool PatenteEhDeDesenhoIndutrial();
        string Imagem { get; set; }
        IList<IEvento> Eventos { get; set; } 
    
    }
}
