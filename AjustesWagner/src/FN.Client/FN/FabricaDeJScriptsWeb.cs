using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FN.Client.FN
{
    public class FabricaDeJScriptsWeb : Singleton<FabricaDeJScriptsWeb>
    {
        public GeradorDePopupsWeb obtenhaGeradorDePopupsWeb()
        {
            return GeradorDePopupsWeb.Instancia;
        }
    }
}