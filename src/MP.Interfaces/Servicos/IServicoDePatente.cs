﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDePatente : IDisposable
    {
        IAnuidadePatente ObtenhaAnuidade(long id);
        IClassificacaoPatente ObtenhaClassificacao(long id);
        IPrioridadeUnionistaPatente ObtenhaPrioridadeUnionista(long id);
        IInventor ObtenhaInventor(long id);
        ITitular ObtenhaTitular(long id);
        IPatente ObtenhaPatente(long id);
        IList<IPatente> ObtenhaPatentesPeloTitulo(string titulo, int quantidadeDeRegistros);
        IList<IAnuidadePatente> CalculeAnuidadesPatentesDeNaturezaPIeMU(DateTime dataDeDeposito);
        IList<IAnuidadePatente> CalculeAnuidadesPatentesDeNaturezaDI(DateTime dataDeDeposito);
        IList<IPatente> ObtenhaPatentesDoCliente(string titulo, long idCliente, int quantidadeDeRegistros);
    }
}