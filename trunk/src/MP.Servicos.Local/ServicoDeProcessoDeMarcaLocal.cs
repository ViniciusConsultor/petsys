using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDeProcessoDeMarcaLocal : Servico, IServicoDeProcessoDeMarca
    {
        public ServicoDeProcessoDeMarcaLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Inserir(IProcessoDeMarca processoDeMarca)
        {
            
        }

        public void Modificar(IProcessoDeMarca processoDeMarca)
        {
            
        }

        public void Excluir(long ID)
        {
            
        }

        public IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(int quantidadeDeRegistros, int offSet)
        {
            throw new NotImplementedException();
        }

        public IProcessoDeMarca Obtenha(long ID)
        {
            throw new NotImplementedException();
        }
    }
}
