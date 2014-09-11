using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FN.Interfaces.Mapeadores
{
    public interface IMapeadorDeItemFinanceiroRecebidoComBoleto
    {
        void Insira(long idItemFinanRecebimento, long idBoleto);
        long ObtenhaItemFinanRecebimentoPorIdBoleto(long idBoleto);
        long ObtenhaBoletoPorIdItemFinanRecebimento(long idItemFinanRecebimento);
        void Excluir(long idItemFinanRecebimento);
    }
}
