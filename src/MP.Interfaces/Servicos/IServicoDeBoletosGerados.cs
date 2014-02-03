using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeBoletosGerados : IServico
    {
        IBoletosGerados obtenhaBoletoPeloId(long idBoleto);
        IBoletosGerados obtenhaBoletoPeloNumero(long numero);

        void Inserir(IBoletosGerados boletoGerado);
        void Excluir(long idBoleto);
    }
}
