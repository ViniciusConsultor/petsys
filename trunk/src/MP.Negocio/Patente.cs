using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class Patente : IPatente
    {
        public long Identificador { get; set; }

        public string TituloPatente { get; set; }

        public INaturezaPatente NaturezaPatente { get; set; }

        public bool ObrigacaoGerada { get; set; }

        public DateTime? DataCadastro { get; set; }

        public string Observacao { get; set; }

        public string Resumo { get; set; }

        public int QuantidadeReivindicacao { get; set; }

        public IList<IAnuidadePatente> Anuidades { get; set; }

        public IList<IClassificacaoPatente> Classificacoes { get; set; }

        public IList<IPrioridadeUnionistaPatente> PrioridadesUnionista { get; set; }

        public IList<IInventor> Inventores { get; set; }

        public IList<ICliente> Clientes { get; set; }

        public IList<IRadicalPatente> Radicais { get; set; }
    }
}
