using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeNaturezaPatente : IDisposable
    {
        IList<INaturezaPatente> obtenhaNaturezaPatentePelaDescricaoComoFiltro(string descricao, int quantidadeMaximaDeRegistros);
        INaturezaPatente obtenhaNaturezaPatentePeloId(long idNaturezaPatente);
        INaturezaPatente obtenhaNaturezaPatentePelaDescricaoOuSigla(string descricaoNaturezaPatente, string siglaNatureza);
        void Inserir(INaturezaPatente naturezaPatente);
        void Modificar(INaturezaPatente naturezaPatente);
        void Excluir(long idNaturezaPatente);
    }
}
