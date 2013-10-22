﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MP.Interfaces.Negocio;

namespace MP.Negocio
{
    [Serializable]
    public class ProcessoDeMarca : IProcessoDeMarca
    {
        public long? IdProcessoDeMarca
        {
            get; set; }

        public IMarcas Marca
        {
            get; set; }

        public long Processo
        {
            get; set; }

        public DateTime DataDeEntrada
        {
            get; set; }

        public DateTime? DataDeConcessao
        {
            get; set; }

        public bool ProcessoEhDeTerceiro
        {
            get; set; }

        public IDespachoDeMarcas Despacho
        {
            get; set; }


        public DateTime? DataDeProrrogacao
        {
            get { return DataDeConcessao.Value.AddYears(10);  }
        }

        public IProcurador Procurador
        {
            get; set; }

        public SituacaoDoProcessoDeMarca SituacaoDoProcesso
        {
            get; set; }
    }
}