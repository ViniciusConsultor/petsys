using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class Patente : IPatente
    {
        public long Identificador { get; set; }

        public string TituloPatente { get; set; }

        public ITipoDePatente TipoDePatente { get; set; }

        public IDespachoDeMarcas DespachoDeMarcas { get; set; }

        public bool ObrigacaoGerada { get; set; }

        public DateTime? DataCadastro { get; set; }

        public string Observacao { get; set; }

        public string Resumo { get; set; }

        public int QuantidadeReivindicacao { get; set; }

        public IList<IAnuidadePatente> Anuidades { get; set; }

        public IList<IClassificacaoPatente> Classificacoes { get; set; }

        public IList<IPrioridadeUnionistaPatente> PrioridadesUnionista { get; set; }

        public IList<ITitularPatente> Titulares { get; set; }
    }
}
