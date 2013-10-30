using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class RevistaDeMarcas : IRevistaDeMarcas
    {
        public long? IdRevistaMarcas { get; set; }

        public int NumeroRevistaMarcas
        { get; set; }

        public DateTime DataPublicacao
        { get; set; }

        public DateTime DataProcessamento
        { get; set; }

        public long NumeroProcessoDeMarca
        { get; set; }

        public string CodigoDespachoAnterior
        { get; set; }

        public string CodigoDespachoAtual
        { get; set; }

        public string Apostila
        { get; set; }

        public string TextoDoDespacho
        { get; set; }

        public bool Processada
        { get; set; }

        public string ExtensaoArquivo
        { get; set; }
    }
}
