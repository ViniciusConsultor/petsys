﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeProcurador
    {
        void Inserir(IProcurador procurador);
        void Remover(long idProcurador);
        void Atualizar(IProcurador procurador);
        IList<IProcurador> ObtenhaProcuradorPeloNome(string nomeDoProcurador, int quantidadeMaximaDeRegistros);
        IProcurador ObtenhaProcurador(IPessoa pessoa);
        IProcurador ObtenhaProcurador(long id);
    }
}