using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;

namespace MP.Interfaces.Negocio
{
    public interface IMarcas
    {
        long? IdMarca { get; set; }
        NCL NCL { get; set; }
        Apresentacao Apresentacao { get; set; }
        ICliente Cliente { get; set; }
        NaturezaDeMarca Natureza { get; set; }
        string DescricaoDaMarca { get; set; }
        string EspecificacaoDeProdutosEServicos { get; set; }
        string ImagemDaMarca { get; set; }
        string ObservacaoDaMarca { get; set; }
        Nullable<int> CodigoDaClasse { get; set; }
        Nullable<int> CodigoDaSubClasse1 { get; set; }
        Nullable<int> CodigoDaSubClasse2 { get; set; }
        Nullable<int> CodigoDaSubClasse3 { get; set; }
        IList<IRadicalMarcas> RadicalMarcas { get; set; }
        bool PagaManutencao { get; set; }
        string Periodo { get; set; }
        string FormaDeCobranca { get; set; }
        double ValorDeCobranca { get; set; }
        string Mes { get; set; }


        void AdicioneRadicalMarcas(IRadicalMarcas radicalMarcas);
        void AdicioneRadicaisMarcas(IList<IRadicalMarcas> listaRadicalMarcas);
    }
}
