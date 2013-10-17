using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeProcessoDePatenteLocal : Servico, IServicoDeProcessoDePatente
    {
        public ServicoDeProcessoDePatenteLocal(ICredencial Credencial) : base(Credencial)
        {
        }

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
            return new List<IProcessoDePatente>();
        }

        public IProcessoDePatente Obtenha(long ID)
        {
            throw new NotImplementedException();
        }

        public int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro)
        {
            return 0;
        }
    }
}
