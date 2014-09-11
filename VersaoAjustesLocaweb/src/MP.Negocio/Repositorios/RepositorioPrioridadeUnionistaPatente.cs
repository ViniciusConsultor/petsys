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
    public class RepositorioPrioridadeUnionistaPatente
    {

        private IDictionary<long, IPrioridadeUnionistaPatente> cache;
        private const string NOME_CALLCONTEXT = "RepositorioPrioridadeUnionistaPatente";

        private RepositorioPrioridadeUnionistaPatente()
        {
            cache = new Dictionary<long, IPrioridadeUnionistaPatente>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioPrioridadeUnionistaPatente obtenhaInstancia()
        {
            var instancia = (RepositorioPrioridadeUnionistaPatente)ChamadaPorContexto.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioPrioridadeUnionistaPatente();
                ChamadaPorContexto.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }

        public IPrioridadeUnionistaPatente ObtenhaPrioridadeUnionista(long id)
        {
            if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                var prioridade = servico.ObtenhaPrioridadeUnionista(id);
                cache.Add(id, prioridade);
                return prioridade;
            }
        }
    }
}
