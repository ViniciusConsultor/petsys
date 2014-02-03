using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeBoletosGerados
    {
        IBoletosGerados obtenhaBoletoPeloId(long idBoleto);
        IBoletosGerados obtenhaBoletoPeloNumero(long numero);

        void Inserir(IBoletosGerados boletoGerado);
        void Excluir(long idBoleto);
    }
}
