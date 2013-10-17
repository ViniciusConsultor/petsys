using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Mapeadores;
using MP.Interfaces.Negocio;

namespace MP.Mapeadores
{
    public class MapeadorDeProcessoDePatente : IMapeadorDeProcessoDePatente
    {
        public void Inserir(IProcessoDePatente processoDePatente)
        {
            throw new NotImplementedException();
        }

        public void Modificar(IProcessoDePatente processoDePatente)
        {
            throw new NotImplementedException();
        }

        public void Excluir(long ID)
        {
            throw new NotImplementedException();
        }

        public IList<IProcessoDePatente> ObtenhaProcessosDePatentes(IFiltro filtro, int quantidadeDeRegistros, int offSet)
        {
            throw new NotImplementedException();
        }

        public IProcessoDePatente Obtenha(long ID)
        {
            throw new NotImplementedException();
        }

        public int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro)
        {
            throw new NotImplementedException();
        }
    }
}
