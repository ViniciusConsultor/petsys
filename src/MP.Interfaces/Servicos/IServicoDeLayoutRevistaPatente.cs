using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeLayoutRevistaPatente
    {
        void Inserir(ILayoutRevistaPatente layoutRevistaPatente);
        void Excluir(long codigo);
        void Modificar(ILayoutRevistaPatente layoutRevistaPatente);
    }
}
