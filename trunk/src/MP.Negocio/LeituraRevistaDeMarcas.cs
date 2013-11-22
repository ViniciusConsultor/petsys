using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class LeituraRevistaDeMarcas : ILeituraRevistaDeMarcas
    {
        public long? IdLeitura
        { get; set; }

        public string NumeroDoProcesso { get; set; }

        public string DataDeDeposito
        { get; set; }

        public string DataDeConcessao
        { get; set; }

        public string DataDeVigencia
        { get; set; }

        public string CodigoDoDespacho
        { get; set; }

        public string TextoDoDespacho
        { get; set; }

        public string Titular
        { get; set; }

        public string Pais
        { get; set; }

        public string Uf
        { get; set; }

        public string Marca
        { get; set; }

        public string Apresentacao
        { get; set; }

        public string Natureza
        { get; set; }

        public string TraducaoDaMarca
        { get; set; }

        public string NCL
        { get; set; }

        public string EdicaoNCL
        { get; set; }

        public string EspecificacaoNCL
        { get; set; }

        public IClasseViena ClasseViena
        { get; set; }

        public IClasseNacional ClasseNacional
        { get; set; }

        public string Apostila
        { get; set; }

        public string Procurador
        { get; set; }

        public string DataPrioridadeUnionista
        { get; set; }

        public string NumeroPrioridadeUnionista
        { get; set; }

        public string PaisPrioridadeUnionista
        { get; set; }

        public IDictionary<string, string> DicionarioSobrestadores
        { get; set; }

        public string Radical
        { get; set; }

        public string RadicalNCL
        { get; set; }

        public string NumeroProtocoloDespacho
        { get; set; }

        public string DataProtocoloDespacho
        { get; set; }

        public string CodigoServicoProtocoloDespacho
        { get; set; }

        public string RazaoSocialRequerenteProtocoloDespacho
        { get; set; }

        public string PaisRequerenteProtocoloDespacho
        { get; set; }

        public string EstadoRequerenteProtocoloDespacho
        { get; set; }

        public string ProcuradorProtocoloDespacho
        { get; set; }

        public override int GetHashCode()
        {
            if (!IdLeitura.HasValue) return base.GetHashCode();

            return IdLeitura.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var objAComparar = obj as ILeituraRevistaDeMarcas;

            return objAComparar.IdLeitura.Equals(IdLeitura);
        }
    }
}
