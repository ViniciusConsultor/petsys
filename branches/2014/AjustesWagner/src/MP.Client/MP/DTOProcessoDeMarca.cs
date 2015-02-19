using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Compartilhados.Interfaces.Core.Negocio;
using MP.Interfaces.Negocio;

namespace MP.Client.MP
{
    [Serializable]
    public class DTOProcessoDeMarca
    {

        public string IdProcesso { get; set; }
        public string NumeroProcesso { get; set; }
        public string DescricaoMarca { get; set; }
        public string Classe { get; set; }
        public string DataDeposito { get; set; }
        public string Apresentacao { get; set; }
        public string Natureza { get; set; }
        public string CPFCNPJ { get; set; }
        public string Cliente { get; set; }
        public string DataDoCadastro { get; set; }
        public string IdCliente { get; set; }
        public string Status { get; set; }


        public static IList<DTOProcessoDeMarca> ConvertaProcessoParaDTO(IList<IProcessoDeMarca> processosDeMarca)
        {
            var processos = new List<DTOProcessoDeMarca>();

            foreach (var processo in processosDeMarca)
            {
                var dto = new DTOProcessoDeMarca();

                if (processo.Marca.Apresentacao != null)
                    dto.Apresentacao = processo.Marca.Apresentacao.Nome;

                if (processo.Marca.Cliente.Pessoa.Tipo == TipoDePessoa.Fisica)
                {
                    var cpf = processo.Marca.Cliente.Pessoa.ObtenhaDocumento(TipoDeDocumento.CPF);

                    if (cpf != null)
                        dto.CPFCNPJ = cpf.ToString();

                }
                else
                {
                    var cnpj = processo.Marca.Cliente.Pessoa.ObtenhaDocumento(TipoDeDocumento.CNPJ);

                    if (cnpj != null)
                        dto.CPFCNPJ = cnpj.ToString();
                }

                if (processo.Marca.NCL != null)
                    dto.Classe = processo.Marca.NCL.Codigo;

                dto.Cliente = processo.Marca.Cliente.Pessoa.Nome;
                dto.IdCliente = processo.Marca.Cliente.Pessoa.ID.Value.ToString();

                if (processo.DataDoDeposito != null)
                    dto.DataDeposito = processo.DataDoDeposito.Value.ToString("dd/MM/yyyy");

                dto.DataDoCadastro = processo.DataDoCadastro.ToString("dd/MM/yyyy");
                dto.DescricaoMarca = processo.Marca.DescricaoDaMarca;

                dto.IdProcesso = processo.IdProcessoDeMarca.Value.ToString();

                dto.Natureza = processo.Marca.Natureza.Nome;

                dto.NumeroProcesso = processo.Processo.ToString();

                dto.Status = processo.Ativo ? "Ativo" : "Inativo";

                processos.Add(dto);

            }

            return processos;
        }

    }
}