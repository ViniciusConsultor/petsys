using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Negocio.Repositorios
{
    public class RepositorioDeAnuidadePatente 
    {
        private IDictionary<long, IAnuidadePatente> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeAnuidadePatente";
        
        private RepositorioDeAnuidadePatente()
        {
            cache = new Dictionary<long, IAnuidadePatente>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeAnuidadePatente obtenhaInstancia()
        {
            var instancia = (RepositorioDeAnuidadePatente)ChamadaPorContexto.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeAnuidadePatente();
                ChamadaPorContexto.SetData(NOME_CALLCONTEXT, instancia);
            }
                
            return instancia;
        }


        public IAnuidadePatente ObtenhaAnuidade(long id)
        {
            if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                var anuidadeDePatente = servico.ObtenhaAnuidade(id);

                cache.Add(id, anuidadeDePatente);
                return anuidadeDePatente;
            }
        }
    }
}