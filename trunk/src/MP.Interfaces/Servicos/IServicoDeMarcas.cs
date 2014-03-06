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
        IMarcas obtenhaMarcasPeloId(long idMarca);
        IList<IMarcas> obtenhaMarcasPelaDescricaoComoFiltro(string descricaoDaMarca, int quantidadeMaximaDeRegistros);
        IList<IMarcas> obtenhaMarcasComDataDeManutencaoAVencer();
    }
}
