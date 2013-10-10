using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IMarcas
    {
        long? IdMarca { get; set; }
        int CodigoNCL { get; set; }
        int CodigoApresentacao { get; set; }
        //Cliente Cliente { get; set; }
        int CodigoNatureza { get; set; }
        string DescricaoDaMarca { get; set; }
        string EspecificacaoDeProdutosEServicos { get; set; }
        string ImagemDaMarca { get; set; }
        string ObservacaoDaMarca { get; set; }
        int CodigoDaClasse { get; set; }
        int CodigoDaSubClasse1 { get; set; }
        int CodigoDaSubClasse2 { get; set; }
        int CodigoDaSubClasse3 { get; set; }
    }
}
