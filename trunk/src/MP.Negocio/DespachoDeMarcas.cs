using System;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class DespachoDeMarcas : IDespachoDeMarcas
    {
        public long? IdDespacho{get;set;}
        public string CodigoDespacho {get; set; }
        public string DescricaoDespacho { get; set; }
        public string SituacaoProcesso { get; set; }
        public int PrazoParaProvidenciaEmDias { get; set; }
        public string Providencia { get; set; }
        public bool DesativaProcesso { get; set; }
        public bool DesativaPesquisaDeColidencia {get;set;}
    }
}
