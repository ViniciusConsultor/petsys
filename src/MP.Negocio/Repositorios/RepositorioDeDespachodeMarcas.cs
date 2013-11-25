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
    public class RepositorioDeDespachodeMarcas : IRepositorioDeDespachodeMarcas
    {
        private IDictionary<long, IDespachoDeMarcas> cache;
        private const string NOME_CALLCONTEXT = "IRepositorioDeDespachodeMarcas";

        private RepositorioDeDespachodeMarcas()
        {
            cache = new Dictionary<long, IDespachoDeMarcas>();
        }

        public static IRepositorioDeDespachodeMarcas obtenhaInstancia()
        {
            var instancia = (IRepositorioDeDespachodeMarcas)CallContext.GetData(NOME_CALLCONTEXT) ??
                                                      new RepositorioDeDespachodeMarcas();

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
