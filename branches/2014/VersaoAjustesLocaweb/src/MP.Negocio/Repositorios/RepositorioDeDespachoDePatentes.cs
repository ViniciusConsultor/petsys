using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Negocio.Repositorios
{
    public class RepositorioDeDespachoDePatentes
    {
        private IDictionary<long, IDespachoDePatentes> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeDespachoDePatentes";

        private RepositorioDeDespachoDePatentes()
        {
            cache = new Dictionary<long, IDespachoDePatentes>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeDespachoDePatentes obtenhaInstancia()
        {
            var instancia = (RepositorioDeDespachoDePatentes)ChamadaPorContexto.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeDespachoDePatentes();
                ChamadaPorContexto.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }

        public IDespachoDePatentes obtenhaDespachoDePatentesPeloId(long id)
        {
            if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDePatentes>())
            {
                var despacho = servico.obtenhaDespachoDePatentesPeloId(id);

                cache.Add(id, despacho);
                return despacho;
            }
        }
    }
}
