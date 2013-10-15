using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeTipoDePatente : IServico
    {
        IList<ITipoDePatente> obtenhaTipoDePatentePelaDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros);
        ITipoDePatente obtenhaTipoDePatentePeloId(long idTipoPatente);
        ITipoDePatente obtenhaTipoDePatentePelaDescricaoOuSigla(string descricaoTipoDePatente, string siglaTipo);
        void Inserir(ITipoDePatente tipoPatente);
        void Modificar(ITipoDePatente tipoPatente);
        void Excluir(long idTipoPatente);
    }
}
