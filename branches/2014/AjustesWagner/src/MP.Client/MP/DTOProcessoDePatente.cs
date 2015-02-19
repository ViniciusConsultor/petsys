using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MP.Interfaces.Negocio;

namespace MP.Client.MP
{
    public class DTOProcessoDePatente
    {

        public string IdProcessoDePatente { get; set; }
        public string NumeroDoProcessoFormatado { get; set; }
        public string SiglaNatureza { get; set; }
        public string TituloPatente { get; set; }
        public string DataDoDeposito { get; set; }
        public string Status { get; set; }                                
                                            
        public static IList<DTOProcessoDePatente> ConvertaProcessoParaDTO(IList<IProcessoDePatente> processosDePatente)
        {
            var dtos = new List<DTOProcessoDePatente>();

            foreach (var processo in processosDePatente)
            {
                var dto = new DTOProcessoDePatente();

                dto.IdProcessoDePatente = processo.IdProcessoDePatente.Value.ToString();
                dto.NumeroDoProcessoFormatado = processo.NumeroDoProcessoFormatado;
                dto.SiglaNatureza = processo.Patente.NaturezaPatente.SiglaNatureza;
                dto.TituloPatente = processo.Patente.TituloPatente;

                if (processo.DataDoDeposito.HasValue)
                    dto.DataDoDeposito = processo.DataDoDeposito.Value.ToString("dd/MM/yyyy");
                dto.Status = processo.Ativo ? "Ativo" : "Inativo";

                dtos.Add(dto);
            }

            return dtos;
        }
                  
    }
}