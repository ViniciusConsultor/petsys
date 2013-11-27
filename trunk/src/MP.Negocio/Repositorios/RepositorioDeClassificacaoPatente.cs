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
    public class RepositorioDeClassificacaoPatente 
    {
        private IDictionary<long, IClassificacaoPatente> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeClassificacaoPatente";

        private RepositorioDeClassificacaoPatente()
        {
            cache = new Dictionary<long, IClassificacaoPatente>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeClassificacaoPatente obtenhaInstancia()
        {
            var instancia = (RepositorioDeClassificacaoPatente)CallContext.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeClassificacaoPatente();
                CallContext.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }

        
        public IClassificacaoPatente ObtenhaClassificacao(long id)
        {
            if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePatente>())
            {
                var classificacao = servico.ObtenhaClassificacao(id);
                cache.Add(id, classificacao);
                return classificacao;
            }
        }
    }
}