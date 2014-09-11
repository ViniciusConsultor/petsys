using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Fabricas;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeMarcasLocal : Servico, IServicoDeMarcas
    {
        public ServicoDeMarcasLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }


        public IMarcas obtenhaMarcasPeloId(long idMarca)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                return mapeador.obtenhaMarcasPeloId(idMarca);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IMarcas> obtenhaMarcasPelaDescricaoComoFiltro(string descricaoDaMarca,
                                                                   int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeMarcas>();

            try
            {
                return mapeador.obtenhaMarcasPelaDescricaoComoFiltro(descricaoDaMarca, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
     
    }

}
