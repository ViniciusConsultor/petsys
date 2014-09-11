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
    public class RepositorioDeInventor 
    {

        private IDictionary<long, IInventor> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeInventor";

        private RepositorioDeInventor()
        {
            cache = new Dictionary<long, IInventor>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeInventor obtenhaInstancia()
        {
            var instancia = (RepositorioDeInventor)CallContext.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeInventor();
                CallContext.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }

        
        public IInventor Obtenha(long id)
        {
            if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeInventor>())
            {
                var inventor = servico.Obtenha(id);

                cache.Add(id, inventor);
                return inventor;
            }    
        }
    }
}
