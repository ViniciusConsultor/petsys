using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    public class Patente : IPatente
    {
        public long Identificador { get; set; }

        public long NumeroDoProtrocolo { get; set; }

        public string NumeroDoProcesso { get; set; }

        public string TituloPatente { get; set; }

        public DateTime? DataEntrada { get; set; }

        public bool ProcessoDeTerceiro { get; set; }

        public DateTime? DataConcessaoRegistro { get; set; }

        public ITipoDePatente TipoDePatente { get; set; }

        public IProcurador Procurador { get; set; }

        public IDespachoDeMarcas DespachoDeMarcas { get; set; }

        public string LinkINPI { get; set; }

        public bool ObrigacaoGerada { get; set; }

        public DateTime? DataCadastro { get; set; }

        public string Observacao { get; set; }

        public string Resumo { get; set; }

        public bool Estrangeiro { get; set; }

        public bool PCT { get; set; }

        public string NumeroPCT { get; set; }

        public string NumeroWO { get; set; }

        public DateTime? DataDepositoPCT { get; set; }

        public DateTime? DataPublicacaoPCT { get; set; }

        public bool Ativo { get; set; }

        public int QuantidadeReivindicacao { get; set; }

        public IList<IAnuidadePatente> Anuidades { get; set; }

        public IList<IClassificacaoPatente> Classificacoes { get; set; }

        public IList<IPrioridadeUnionistaPatente> PrioridadesUnionista { get; set; }

        public IList<ITitularPatente> Titulares { get; set; }
    }
}
