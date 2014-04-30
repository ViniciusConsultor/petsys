using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;

namespace FN.Interfaces.Servicos
{
    public interface IServicoDeItemFinanceiroRecebidoComBoleto : IServico
    {
        void Insira(long idItemFinanRecebimento, long idBoleto);
        long ObtenhaItemFinanRecebimentoPorIdBoleto(long idBoleto);
        long ObtenhaBoletoPorIdItemFinanRecebimento(long idItemFinanRecebimento);
        void Excluir(long idItemFinanRecebimento);
    }
}
