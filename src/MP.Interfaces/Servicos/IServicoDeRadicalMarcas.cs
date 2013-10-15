using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeRadicalMarcas : IServico
    {
        IList<IRadicalMarcas> obtenhaRadicalMarcasPelaDescricaoComoFiltro(string descricaoDoRadicalMarcas, int quantidadeMaximaDeRegistros);
        IRadicalMarcas obtenhaRadicalMarcasPeloId(long idRadicalMarcas);
        void Inserir(IRadicalMarcas radicalMarcas);
        void Modificar(IRadicalMarcas radicalMarcas);
        void Excluir(long idRadicalMarcas);
        IList<IRadicalMarcas> obtenhaRadicalMarcasPeloIdDaMarcaComoFiltro(long idMarca, int quantidadeMaximaDeRegistros);
    }
}
