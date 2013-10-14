﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorDePatente
    {
        void Insira(IPatente patente);
        void Modificar(IPatente patente);
        void Exluir(int codigopatente);
    }
}