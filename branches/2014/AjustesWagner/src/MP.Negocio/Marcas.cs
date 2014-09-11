using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class Marcas : IMarcas
    {
        public long? IdMarca { get; set; }
        public NCL NCL { get; set; }
        public Apresentacao Apresentacao { get; set; }
        public ICliente Cliente { get; set; }
        public NaturezaDeMarca Natureza { get; set; }
        public string DescricaoDaMarca { get; set; }
        public string EspecificacaoDeProdutosEServicos { get; set; }
        public string ImagemDaMarca { get; set; }
        public string ObservacaoDaMarca { get; set; }
        public Nullable<int> CodigoDaClasse { get; set; }
        public Nullable<int> CodigoDaSubClasse1 { get; set; }
        public Nullable<int> CodigoDaSubClasse2 { get; set; }
        public Nullable<int> CodigoDaSubClasse3 { get; set; }

        public IList<IRadicalMarcas> RadicalMarcas
        { get; set; }

        public IManutencao Manutencao
        {
            get; set;
        }
        
        public void AdicioneRadicalMarcas(IRadicalMarcas radicalMarcas)
        {
            RadicalMarcas.Add(radicalMarcas);
        }

        public void AdicioneRadicaisMarcas(IList<IRadicalMarcas> listaRadicalMarcas)
        {
            ((List<IRadicalMarcas>)RadicalMarcas).AddRange(listaRadicalMarcas);
        }

        public Marcas()
        {
            RadicalMarcas = new List<IRadicalMarcas>();
        }

    }
}
