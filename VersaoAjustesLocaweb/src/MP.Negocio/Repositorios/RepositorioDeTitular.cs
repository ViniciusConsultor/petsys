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
    public class RepositorioDeTitular
    {
        private IDictionary<long, ITitular> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeTitular";

        private RepositorioDeTitular()
        {
            cache = new Dictionary<long, ITitular>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeTitular obtenhaInstancia()
        {
            var instancia = (RepositorioDeTitular)ChamadaPorContexto.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeTitular();
                ChamadaPorContexto.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }


        public ITitular Obtenha(long id)
        {
            if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeTitular>())
            {
                var titular = servico.Obtenha(id);

                cache.Add(id, titular);
                return titular;
            }
        }
    }
}
