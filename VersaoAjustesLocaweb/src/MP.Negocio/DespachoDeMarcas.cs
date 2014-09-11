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

        public ITemplateDeEmail TemplateDeEmail {get; set; }

        public override bool Equals(object obj)
        {
            if (IdDespacho.HasValue)
            {
                var objAComparar = obj as IDespachoDeMarcas;

                if (!objAComparar.IdDespacho.HasValue) return false;

                return objAComparar.IdDespacho.Value.Equals(IdDespacho.Value);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return IdDespacho.HasValue ? IdDespacho.Value.GetHashCode() : base.GetHashCode();
        }
    }
}
