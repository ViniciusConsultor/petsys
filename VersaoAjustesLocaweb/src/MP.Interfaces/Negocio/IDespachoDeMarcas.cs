using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    public interface IDespachoDeMarcas
    {
        long? IdDespacho { get; set; }
        string CodigoDespacho { get; set; }
        string DescricaoDespacho { get; set; }
        string SituacaoProcesso { get; set; }
        int PrazoParaProvidenciaEmDias { get; set; }
        string Providencia { get; set; }
        bool DesativaProcesso { get; set; }
        bool DesativaPesquisaDeColidencia { get; set; }
        ITemplateDeEmail TemplateDeEmail { get; set; }
    }
}
