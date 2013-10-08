﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Interfaces.Mapeadores
{
    public interface IMapeadorTipoDePatente
    {
        IList<ITipoDePatente> obtenhaTodosTiposDePatentes();
        ITipoDePatente obtenhaTipoDePatentePeloId(long idTipoPatente);
        ITipoDePatente obtenhaTipoDePatentePelaDescricaoOuSigla(string descricaoTipoDePatente, string siglaTipo);
        void Inserir(ITipoDePatente tipoPatente);
        void Modificar(ITipoDePatente tipoPatente);
        void Excluir(long idTipoPatente);
    }
}
