﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDeProcessoDeMarca
    {
        void Inserir(IProcessoDeMarca processoDeMarca);
        void Modificar(IProcessoDeMarca processoDeMarca);
        void Excluir(long ID);
        IList<IProcessoDeMarca> ObtenhaProcessosDeMarcas(int quantidadeDeRegistros, int offSet);
        IProcessoDeMarca Obtenha(long ID);
    }
}
