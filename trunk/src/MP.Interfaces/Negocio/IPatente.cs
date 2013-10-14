using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IPatente
    {
        long Identificador { get; set; }
        long NumeroDoProtrocolo { get; set; }
        string NumeroDoProcesso { get; set; }
        string TituloPatente { get; set; }
        DateTime? DataEntrada { get; set; }
        bool ProcessoDeTerceiro { get; set; }
        DateTime? DataConcessaoRegistro { get; set; }
        ITipoDePatente TipoDePatente { get; set; }
        IProcurador Procurador { get; set; }
        IDespachoDeMarcas DespachoDeMarcas { get; set; }
        string LinkINPI { get; set; }
        bool ObrigacaoGerada { get; set; }
        DateTime? DataCadastro { get; set; }
        string Observacao { get; set; }
        string Resumo { get; set; }
        bool Estrangeiro { get; set; }
        bool PCT { get; set; }
        string NumeroPCT { get; set; }
        string NumeroWO { get; set; }
        DateTime? DataDepositoPCT { get; set; }
        DateTime? DataPublicacaoPCT { get; set; }
        bool Ativo { get; set; }
        int QuantidadeReivindicacao { get; set; }
        IList<IAnuidadePatente> Anuidades { get; set; }
        IList<IClassificacaoPatente> Classificacoes { get; set; }
        IList<IPrioridadeUnionistaPatente> PrioridadesUnionista { get; set; }
        IList<ITitularPatente> Titulares { get; set; }
    }
}
