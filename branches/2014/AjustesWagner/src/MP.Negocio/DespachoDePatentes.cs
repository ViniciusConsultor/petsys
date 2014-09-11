using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class DespachoDePatentes : IDespachoDePatentes
    {
        public long? IdDespachoDePatente { get; set; }
        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public string Situacao { get; set; }
        public int? PrazoProvidencia { get; set; }
        public string TipoProvidencia { get; set; }
        public bool DesativaProcesso { get; set; }
        public bool AgendarPagamento { get; set; }
        public string Descricao { get; set; }
        public ITemplateDeEmail TemplateDeEmail { get; set; }

        public override bool Equals(object obj)
        {
            if (IdDespachoDePatente.HasValue)
            {
                var objAComparar = obj as IDespachoDePatentes;

                if (!objAComparar.IdDespachoDePatente.HasValue) return false;

                return objAComparar.IdDespachoDePatente.Value.Equals(IdDespachoDePatente.Value);
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return IdDespachoDePatente.HasValue ? IdDespachoDePatente.Value.GetHashCode() : base.GetHashCode();
        }
    }
}