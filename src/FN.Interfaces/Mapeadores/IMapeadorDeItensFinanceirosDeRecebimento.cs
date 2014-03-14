﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.FN.Negocio;

namespace FN.Interfaces.Mapeadores
{
    public interface IMapeadorDeItensFinanceirosDeRecebimento
    {
        void Insira(IItemLancamentoFinanceiroRecebimento Item);
        void Modifique(IItemLancamentoFinanceiroRecebimento Item);

        int ObtenhaQuantidadeDeItensFinanceiros(IFiltro filtro);

        IList<IItemLancamentoFinanceiroRecebimento> ObtenhaItensFinanceiros(IFiltro filtro, int quantidadeDeRegistros, int offSet);

    }
}
