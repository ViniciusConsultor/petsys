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
    public class ServicoDeRadicalMarcasLocal : Servico , IServicoDeRadicalMarcas
    {
        public ServicoDeRadicalMarcasLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public IList<IRadicalMarcas> obtenhaRadicalMarcasPelaDescricaoComoFiltro(string descricaoDoRadicalMarcas, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRadicalMarcas>();

            try
            {
                return mapeador.obtenhaRadicalMarcasPelaDescricaoComoFiltro(descricaoDoRadicalMarcas, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IRadicalMarcas obtenhaRadicalMarcasPeloId(long idRadicalMarcas)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRadicalMarcas>();

            try
            {
                return mapeador.obtenhaRadicalMarcasPeloId(idRadicalMarcas);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }

        public IList<IRadicalMarcas> obtenhaRadicalMarcasPeloIdDaMarcaComoFiltro(long idMarca, int quantidadeMaximaDeRegistros)
        {
            ServerUtils.setCredencial(_Credencial);

            var mapeador = FabricaGenerica.GetInstancia().CrieObjeto<IMapeadorDeRadicalMarcas>();

            try
            {
                return mapeador.obtenhaRadicalMarcasPeloIdDaMarcaComoFiltro(idMarca, quantidadeMaximaDeRegistros);
            }
            finally
            {
                ServerUtils.libereRecursos();
            }
        }
    }
}
