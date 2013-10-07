﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeProcurador : IServico
    {
        void Inserir(IProcurador procurador);
        void Remover(IProcurador procurador);
        void Atualizar(IProcurador procurador);
        List<IProcurador> ObtenhaTodosProcuradores();
        IProcurador ObtenhaProcurador(IProcurador procurador);
    }
}