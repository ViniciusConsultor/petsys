﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDePatente : IDisposable
    {
        void Insira(IPatente patente);
        void Modificar(IPatente patente);
        void Exluir(long codigoPatente);
        IAnuidadePatente ObtenhaAnuidade(long id);
        IClassificacaoPatente ObtenhaClassificacao(long id);
        IPrioridadeUnionistaPatente ObtenhaPrioridadeUnionista(long id);
        IInventor ObtenhaInventor(long id);
        IPatente ObtenhaPatente(long id);
        IList<IPatente> ObtenhaPatentesPeloTitulo(string titulo, int quantidadeDeRegistros);
    }
}