using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeLayoutRevistaPatente : IServico
    {
        void Inserir(ILayoutRevistaPatente layoutRevistaPatente);
        void Excluir(long codigo);
        void Modificar(ILayoutRevistaPatente layoutRevistaPatente);
        IList<ILayoutRevistaPatente> ObtenhaTodos();
        IList<ILayoutRevistaPatente> SelecioneLayoutPeloNomeDoCampo(string nomeDoCampo, int quantidadeMaximaDeRegistros);
    }
}
