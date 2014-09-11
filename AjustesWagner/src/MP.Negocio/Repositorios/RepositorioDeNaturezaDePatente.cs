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
    public class RepositorioDeNaturezaDePatente
    {
        private IDictionary<long, INaturezaPatente> cache;
        private const string NOME_CALLCONTEXT = "RepositorioDeNaturezaDePatente";

        private RepositorioDeNaturezaDePatente()
        {
            cache = new Dictionary<long, INaturezaPatente>();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static RepositorioDeNaturezaDePatente obtenhaInstancia()
        {
            var instancia = (RepositorioDeNaturezaDePatente)CallContext.GetData(NOME_CALLCONTEXT);

            if (instancia == null)
            {
                instancia = new RepositorioDeNaturezaDePatente();
                CallContext.SetData(NOME_CALLCONTEXT, instancia);
            }

            return instancia;
        }

        public INaturezaPatente obtenhaDespachoDeMarcasPeloId(long id)
        {
            if (cache.ContainsKey(id)) return cache[id];

              using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeNaturezaPatente>()) {
                var natureza = servico.obtenhaNaturezaPatentePeloId(id);

                cache.Add(id, natureza);
                return natureza;
            }
        }
    }
}
