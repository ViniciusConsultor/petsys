﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;
using Compartilhados;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeProcessoDePatente : IServico
    {
        void Inserir(IProcessoDePatente processoDePatente);
        void Modificar(IProcessoDePatente processoDePatente);
        void AtualizeProcessoAposLeituraDaRevista(IProcessoDePatente processoDePatente);
        void Excluir(IProcessoDePatente processoDePatente);
        IList<IProcessoDePatente> ObtenhaProcessosDePatentes(IFiltro filtro, int quantidadeDeRegistros, int offSet);
        IProcessoDePatente Obtenha(long ID);
        int ObtenhaQuantidadeDeProcessosCadastrados(IFiltro filtro);
        void AtivaDesativaProcessoDePatente(long idProcessoDePatente, bool ativo);
        IList<string> ObtenhaTodosNumerosDeProcessosCadastrados();
        IProcessoDePatente ObtenhaPeloNumeroDoProcesso(string numeroDoProcesso);
        IList<IProcessoDePatente> obtenhaProcessosAtivosComPatenteQueContemRadicalCadastrado();
        IList<IProcessoDePatente> ObtenhaTodosProcessosAtivos();
    }
}