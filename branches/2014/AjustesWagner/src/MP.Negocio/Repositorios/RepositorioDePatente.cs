using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Negocio.Repositorios
{
    public class RepositorioDePatente
    {
        private IDictionary<long, IPatente> cache;
        private const string NOME_CALLCONTEXT = "RepositorioDePatente";

        private RepositorioDePatente()
        {
            cache = new Dictionary<long, IPatente>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDePatente obtenhaInstancia()
        {
            var instancia = (RepositorioDePatente)CallContext.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDePatente();
                CallContext.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }

        public IPatente ObtenhaPatente(long id)
        {
             if (cache.ContainsKey(id)) return cache[id];

             using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                var patente = servico.ObtenhaPatente(id);

                cache.Add(id, patente);
                return patente;
            }    
        }
    }
}
