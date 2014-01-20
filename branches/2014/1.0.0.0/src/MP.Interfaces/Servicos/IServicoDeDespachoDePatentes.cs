using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeDespachoDePatentes : IServico
    {
        IDespachoDePatentes obtenhaDespachoDePatentesPeloId(long idDespachoDePatentes);
        IList<IDespachoDePatentes> ObtenhaPorCodigoDoDespachoComoFiltro(string codigo, int quantidadeMaximaDeRegistros);
        void Inserir(IDespachoDePatentes despachoDePatentes);
        void Modificar(IDespachoDePatentes despachoDePatentes);
        void Excluir(long idDespachoDePatentes);
        IDespachoDePatentes ObtenhaDespachoPeloCodigo(string codigo, int quantidadeMaximaDeRegistros);
    }
}
