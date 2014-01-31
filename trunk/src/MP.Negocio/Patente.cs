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
        public Patente()
        {
            Clientes = new List<ICliente>();
            Inventores = new List<IInventor>();
            Anuidades = new List<IAnuidadePatente>();
            PrioridadesUnionista = new List<IPrioridadeUnionistaPatente>();
            Radicais = new List<IRadicalPatente>();
            Classificacoes = new List<IClassificacaoPatente>();
            Titulares = new List<ITitular>();
        }

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

        public IList<ITitular> Titulares { get; set; }

        public string ClassificacoesConcatenadas { 
            get 
            { 
                string classificacoesConcatenadas = string.Empty;

                foreach (IClassificacaoPatente classificacaoPatente in Classificacoes)
                    classificacoesConcatenadas += classificacaoPatente.Classificacao + ", ";

                if (string.IsNullOrEmpty(classificacoesConcatenadas))
                    return classificacoesConcatenadas;

                return classificacoesConcatenadas.Substring(0, classificacoesConcatenadas.Length - 2);
            }
        }

        public bool PagaManutencao { get; set; }

        public Periodo Periodo { get; set; }

        public string FormaDeCobranca { get; set; }

        public double ValorDeCobranca { get; set; }

        public string Mes
        { get; set; }
    }
}
