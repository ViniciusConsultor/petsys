using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Repositorios;
using MP.Interfaces.Servicos;

namespace MP.Negocio.Repositorios
{
    public class RepositorioDeAnuidadePatente : IRepositorioDeAnuidadePatente
    {
        private IDictionary<long, IAnuidadePatente> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeAnuidadePatente";
        
        private RepositorioDeAnuidadePatente()
        {
            cache = new Dictionary<long, IAnuidadePatente>();
        }
        
        public static IRepositorioDeAnuidadePatente obtenhaInstancia()
        {
            var instancia = (IRepositorioDeAnuidadePatente) CallContext.GetData(NOME_CALLCONTEXT) ??
                                                      new RepositorioDeAnuidadePatente();

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