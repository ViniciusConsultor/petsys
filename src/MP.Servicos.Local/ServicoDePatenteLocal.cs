using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compartilhados;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;

namespace MP.Servicos.Local
{
    public class ServicoDePatenteLocal : Servico, IServicoDePatente
    {
        public ServicoDePatenteLocal(ICredencial Credencial) : base(Credencial)
        {
        }

        public void Insira(IPatente patente)
        {
        }

        public void Modificar(IPatente patente)
        {
        }

        public void Exluir(int codigopatente)
        {
        }
    }
}
