using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeMarcas : IServico
    {
        IList<IMarcas> obtenhaTodasMarcasCadastradas();
        IMarcas obtenhaMarcasPeloId(long idMarca);
        IList<IMarcas> obtenhaMarcasPelaDescricao(string descricaoDaMarca);
        IList<IMarcas> ObtenhaPorIdDaMarcaComoFiltro(string idMarca, int quantidadeMaximaDeRegistros);
        void Inserir(IMarcas marca);
        void Modificar(IMarcas marca);
        void Excluir(long idMarca);
    }
}
