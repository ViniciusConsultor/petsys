﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Servicos
{
    public interface IServicoDeDespachoDeMarcas : IServico
    {
        IList<IDespachoDeMarcas> obtenhaTodosDespachoDeMarcas();
        IDespachoDeMarcas obtenhaDespachoDeMarcasPeloId(long idDespachoDeMarcas);
        IDespachoDeMarcas obtenhaDespachoDeMarcasPelaDescricao(string descricaoDespachoDeMarcas);
        IList<IDespachoDeMarcas> ObtenhaPorCodigoDoDespachoComoFiltro(string codigo, int quantidadeMaximaDeRegistros);
        void Inserir(IDespachoDeMarcas despachoDeMarcas);
        void Modificar(IDespachoDeMarcas despachoDeMarcas);
        void Excluir(long idDespachoDeMarcas);
    }
}
