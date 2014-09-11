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
    public class RepositorioDeMarcas
    {

        private IDictionary<long, IMarcas> cache;
        private const string NOME_CALLCONTEXT = "RepositorioDeMarcas";

        private RepositorioDeMarcas()
        {
            cache = new Dictionary<long, IMarcas>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeMarcas obtenhaInstancia()
        {
            var instancia = (RepositorioDeMarcas)ChamadaPorContexto.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeMarcas();
                ChamadaPorContexto.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }

        public IMarcas obtenhaMarcasPeloId(long id)
        {
             if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeMarcas>())
            {
                var marca = servico.obtenhaMarcasPeloId(id);

                cache.Add(id, marca);
                return marca;
            }    
        }
    }
}
