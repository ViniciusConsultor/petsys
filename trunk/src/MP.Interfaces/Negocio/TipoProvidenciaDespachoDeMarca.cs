using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public  class TipoProvidenciaDespachoDeMarca
    {

        public TipoProvidenciaDespachoDeMarca AcompanharOPedidoEmQuestaoNaRPI = new TipoProvidenciaDespachoDeMarca(1,"Acompanhar o pedido em questão na RPI");
        public TipoProvidenciaDespachoDeMarca AcompanharORegistro = new TipoProvidenciaDespachoDeMarca(2, "Acompanhar o registro");
        public TipoProvidenciaDespachoDeMarca AcompanharORegistroDuranteDecenio = new TipoProvidenciaDespachoDeMarca(3, "Acompanhar o registro durante decênio");
        public TipoProvidenciaDespachoDeMarca AcompanharPedidoDeNotoriedade = new TipoProvidenciaDespachoDeMarca(4, "Acompanhar pedido de notoriedade");
        

        private TipoProvidenciaDespachoDeMarca(int codigo, string descricao)
        {
            
        }
    }
}
