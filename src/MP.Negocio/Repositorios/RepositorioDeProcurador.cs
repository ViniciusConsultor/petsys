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
    public class RepositorioDeProcurador
    {

        private IDictionary<long, IProcurador> cache;
        private const string NOME_CALLCONTEXT = "RepositorioDeProcurador";

        private RepositorioDeProcurador()
        {
            cache = new Dictionary<long, IProcurador>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeProcurador obtenhaInstancia()
        {
            var instancia = (RepositorioDeProcurador)CallContext.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeProcurador();
                CallContext.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }
        
        public IProcurador ObtenhaProcurador(long id)
        {

            if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeProcurador>())
            {
                var classificacao = servico.ObtenhaProcurador(id);
                cache.Add(id, classificacao);
                return classificacao;
            }
        }
    }
}