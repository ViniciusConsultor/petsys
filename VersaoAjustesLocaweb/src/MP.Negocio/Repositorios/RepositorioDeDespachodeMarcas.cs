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
    public class RepositorioDeDespachodeMarcas
    {
        private IDictionary<long, IDespachoDeMarcas> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeDespachodeMarcas";

        private RepositorioDeDespachodeMarcas()
        {
            cache = new Dictionary<long, IDespachoDeMarcas>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeDespachodeMarcas obtenhaInstancia()
        {
            var instancia = (RepositorioDeDespachodeMarcas)ChamadaPorContexto.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeDespachodeMarcas();
                ChamadaPorContexto.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }

        public IDespachoDeMarcas obtenhaDespachoDeMarcasPeloId(long id)
        {
            if (cache.ContainsKey(id)) return cache[id];

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeDespachoDeMarcas>())
            {
                var despacho = servico.obtenhaDespachoDeMarcasPeloId(id);

                cache.Add(id, despacho);
                return despacho;
            }
        }
    }
}
