using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeProcessoDePatente
    {
        void Inserir(IProcessoDePatente processoDePatente);
        void Modificar(IProcessoDePatente processoDePatente);
        void Excluir(long ID);
        IList<IProcessoDePatente> ObtenhaProcessosDePatentes(IFiltro filtro, int quantidadeDeRegistros, int offSet, bool considerarNaoAtivos);
        IProcessoDePatente Obtenha(long ID);
        int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro, bool considerarNaoAtivos);
        DateTime? ObtenhaDataDepositoDoProcessoVinvuladoAPatente(long idPatente);
        IProcessoDePatente ObtenhaPeloNumeroDoProcesso(string numeroDoProcesso);
        IList<IProcessoDePatente> obtenhaProcessosAtivosComPatenteQueContemRadicalCadastrado();
        IList<IProcessoDePatente> ObtenhaTodosProcessosAtivos();
    }
}
