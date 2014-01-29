using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoletoNet;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Servicos;
using MP.Client.Relatorios;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmBoletoBancarioCEF : SuperPagina
    {
        public const string carteira = "SR";
        public const int codigoDoBanco = 104;

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlCliente.ClienteFoiSelecionado +=ctrlCliente_ClienteFoiSelecionado;

            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ctrlCliente_ClienteFoiSelecionado(ICliente cliente)
        {
            pnlDados.Visible = true;

            if(cliente.Pessoa != null)
            {
                txtNome.Text = cliente.Pessoa.Nome;

                IPessoa pessoa = null;

                if (cliente.Pessoa.Tipo == TipoDePessoa.Fisica)
                {
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                    {
                        var pessoaFisica = servico.ObtenhaPessoa(cliente.Pessoa.ID.Value);

                        if(pessoaFisica != null)
                        {
                            var cpf = pessoaFisica.ObtenhaDocumento(TipoDeDocumento.CPF);

                            if (cpf != null)
                                txtCNPJCPF.Text = cpf.ToString();

                            pessoa = pessoaFisica;
                        }
                    }
                }
                else
                {
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaJuridica>())
                    {
                        var pessoaJuridica = servico.ObtenhaPessoa(cliente.Pessoa.ID.Value);

                        if (pessoaJuridica != null)
                        {
                            var cnpj = pessoaJuridica.ObtenhaDocumento(TipoDeDocumento.CNPJ);

                            if (cnpj != null)
                                txtCNPJCPF.Text = cnpj.ToString();

                            pessoa = pessoaJuridica;
                        }
                    }
                }

                if (pessoa != null && pessoa.Enderecos != null && pessoa.Enderecos.Count > 0)
                {
                    txtEndereco.Text = pessoa.Enderecos[0].Logradouro;
                    txtBairro.Text = pessoa.Enderecos[0].Bairro;

                    if (pessoa.Enderecos[0].Municipio != null)
                    {
                        txtCidade.Text = pessoa.Enderecos[0].Municipio.Nome;

                        if (pessoa.Enderecos[0].Municipio.UF != null)
                            txtEstado.Text = pessoa.Enderecos[0].Municipio.UF.Sigla;
                    }

                    if (pessoa.Enderecos[0].CEP != null && pessoa.Enderecos[0].CEP.Numero.HasValue)
                    {
                        txtCep.Text = pessoa.Enderecos[0].CEP.Numero.Value.ToString();
                    }
                }
            }
        }

        private void ExibaTelaInicial()
        {
            ctrlCliente.Inicializa();
            ctrlCliente.BotaoNovoEhVisivel = true;
            pnlDados.Visible = false;
        }

        protected void btnGerarBoleto_ButtonClick(object sender, EventArgs e)
        {
            // Boleto para CEF, com carteira SR e nosso número com 11 dígitos.

            //if(ExisteErroDePreenchimento())
            //{
            //    return;
            //}

            var vencimento = txtVencimento.SelectedDate.Value.ToString("dd/MM/yyyy");
            var valorBoleto = txtValor.Text;

            //var numeroDocumento =  busca no banco
            var numeroDocumento = "00001";

            // Numero do documento - numero de controle interno, não afeta o calculo da linha digitavel e nem o codigo de barras
            // mais pode ser útil, exemplo: quando o cliente ligar, vc pode consultar por este número e ver 
            // se já foi efetuado o pagamento

            // cedente

            var proximoNossoNumero = "10000709";// busca no banco

            var cedente_nossoNumeroBoleto = "82" + proximoNossoNumero; // o final do nosso número é incrementado ao final

            var cedente_cpfCnpj = "222.111.111-00"; // busca no banco
            var cedente_nome = "ALTERNATIVA MARCAS E PATENTES"; // busca no banco
            var cedente_agencia = "1394"; // busca no banco
            var cedente_conta = "302864"; // busca no banco
            var cedente_digitoConta = "4"; // busca no banco

            var cedente_codigo = "00100302864"; // operacao + conta - busca no banco

            var cedente = new Cedente(cedente_cpfCnpj, cedente_nome, cedente_agencia, cedente_conta, cedente_digitoConta) { Codigo = cedente_codigo };

            var boleto = new Boleto(Convert.ToDateTime(vencimento),
                                    (decimal)Convert.ToDouble(valorBoleto), carteira, cedente_nossoNumeroBoleto,
                                    cedente) { NumeroDocumento = numeroDocumento };

            //sacado         
            var sacado_cpfCnpj = txtCNPJCPF.Text;
            var sacado_nome = txtNome.Text;
            var sacado_endereco = txtEndereco.Text;

            var sacado = new Sacado(sacado_cpfCnpj, sacado_nome);
            boleto.Sacado = sacado;
            boleto.Sacado.Endereco.End = sacado_endereco;
            boleto.Sacado.Endereco.Bairro = txtBairro.Text;
            boleto.Sacado.Endereco.Cidade = txtCidade.Text;
            boleto.Sacado.Endereco.CEP = txtCep.Text;
            boleto.Sacado.Endereco.UF = txtEstado.Text;

            var instrucaoCaixa = new Instrucao_Caixa { Descricao = "Não Receber após o vencimento" };

            boleto.Instrucoes.Add(instrucaoCaixa);

            var especificacao = new EspecieDocumento_Caixa();
            boleto.EspecieDocumento = new EspecieDocumento_Caixa(especificacao.getCodigoEspecieByEnum(EnumEspecieDocumento_Caixa.Recibo));

            boleto.DataProcessamento = DateTime.Now;
            boleto.DataDocumento = DateTime.Now;

            var urlImagemLogo = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/Imagens/teste.jpg";

            var boletoBancario = new BoletoBancario(urlImagemLogo);
            boletoBancario.CodigoBanco = codigoDoBanco;
            boletoBancario.Boleto = boleto;
            boletoBancario.MostrarCodigoCarteira = true;
            boletoBancario.Boleto.Valida();
            boletoBancario.MostrarComprovanteEntrega = false;

            var chaveDoBoleto = Guid.NewGuid().ToString();

            Session.Add(chaveDoBoleto, boletoBancario);

            var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "MP/frmVisualizarBoletoGerado.aspx",
                                            "?Id=", chaveDoBoleto);
            ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                UtilidadesWeb.ExibeJanela(url,
                                                                               "Visualizar boleto gerado",
                                                                               800, 550), false);
            // incrementar o nosso numero e o numero do documento e atualizar no banco.
        }
        
        protected override string ObtenhaIdFuncao()
        {
            return "FUN.MP.017";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}