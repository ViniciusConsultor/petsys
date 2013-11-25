using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using Compartilhados.Fabricas;
using MP.Interfaces.Negocio;
using MP.Interfaces.Negocio.Repositorios;
using MP.Interfaces.Servicos;

namespace MP.Negocio.Repositorios
{
    public class RepositorioDeClassificacaoPatente : IRepositorioDeClassificacaoPatente
    {
        private IDictionary<long, IClassificacaoPatente> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeClassificacaoPatente";

        private RepositorioDeClassificacaoPatente()
        {
            cache = new Dictionary<long, IClassificacaoPatente>();
        }

        public static IRepositorioDeClassificacaoPatente obtenhaInstancia()
        {
            var instancia = (IRepositorioDeClassificacaoPatente)CallContext.GetData(NOME_CALLCONTEXT) ??
                                                                new RepositorioDeClassificacaoPatente();

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