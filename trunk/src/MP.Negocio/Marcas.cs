using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class Marcas : IMarcas
    {
        public long? IdMarca { get; set; }
        public NCL NCL { get; set; }
        public Apresentacao Apresentacao { get; set; }
        //public Cliente Cliente { get; set; }
        public Natureza Natureza { get; set; }
        public string DescricaoDaMarca { get; set; }
        public string EspecificacaoDeProdutosEServicos { get; set; }
        public string ImagemDaMarca { get; set; }
        public string ObservacaoDaMarca { get; set; }
        public int CodigoDaClasse { get; set; }
        public int CodigoDaSubClasse1 { get; set; }
        public int CodigoDaSubClasse2 { get; set; }
        public int CodigoDaSubClasse3 { get; set; }
    }
}
