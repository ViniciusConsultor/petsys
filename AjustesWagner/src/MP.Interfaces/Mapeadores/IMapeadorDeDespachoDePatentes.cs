using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeDespachoDePatentes
    {
        IDespachoDePatentes obtenhaDespachoDePatentesPeloId(long idDespachoDePatentes);
        IList<IDespachoDePatentes> ObtenhaPorDescricao(string descricaoParcial, int quantidadeMaximaDeRegistros);
        void Inserir(IDespachoDePatentes despachoDePatentes);
        void Modificar(IDespachoDePatentes despachoDePatentes);
        void Excluir(long idDespachoDePatentes);
        IDespachoDePatentes ObtenhaDespachoPeloCodigo(string codigo);
    }
}
