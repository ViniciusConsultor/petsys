﻿using System;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class DespachoDeMarcas : IDespachoDeMarcas
    {
        public long? IdDespacho
        {
            get;
            set;
        }

        public string CodigoDespacho
        {
            get; set; }

        public string DetalheDespacho
        {
            get; set; }

        public SituacaoDoProcessoDeMarca SituacaoProcesso
        {
            get; set; }

        public bool Registro
        {
            get; set; }
    }
}