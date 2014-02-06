using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BoletoNet;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Servicos;
using MP.Client.Relatorios;
using MP.Interfaces.Negocio;
using MP.Interfaces.Servicos;
using Telerik.Web.UI;

namespace MP.Client.MP
{
    public partial class frmBoletoBancarioCEF : SuperPagina
    {
        public const string carteira = "SR";
        public const int codigoDoBanco = 104;
        public const string CHAVE_CLIENTE_SELECIONADO = "CHAVE_CLIENTE_SELECIONADO";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlCliente.ClienteFoiSelecionado +=ctrlCliente_ClienteFoiSelecionado;

            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ctrlCliente_ClienteFoiSelecionado(ICliente cliente)
        {
            Session.Add("CHAVE_CLIENTE_SELECIONADO", cliente);

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

            if (ExisteErroDePreenchimento())
            {
                return;
            }

            var vencimento = txtVencimento.SelectedDate.Value.ToString("dd/MM/yyyy");
            var valorBoleto = txtValor.Text;

            IBoletosGeradosAux dadosAuxiliares = null;

            // Busca dados auxiliares no banco, se for a primeira vez, efetua o insert

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoletosGeradosAux>())
            {
                dadosAuxiliares = servico.obtenhaProximasInformacoesParaGeracaoDoBoleto();

                if (!dadosAuxiliares.ID.HasValue)
                {
                    var boletosGeradosAux = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGeradosAux>();

                    boletosGeradosAux.ID = GeradorDeID.getInstancia().getProximoID();
                    boletosGeradosAux.ProximoNossoNumero = 8210001001;
                    boletosGeradosAux.ProximoNumeroBoleto = 1;

                    servico.InserirPrimeiraVez(boletosGeradosAux);

                    dadosAuxiliares = servico.obtenhaProximasInformacoesParaGeracaoDoBoleto();
                }
            }

            var numeroDocumento = dadosAuxiliares.ProximoNumeroBoleto.Value;

            // Numero do documento - numero de controle interno, não afeta o calculo da linha digitavel e nem o codigo de barras
            // mais pode ser útil, exemplo: quando o cliente ligar, vc pode consultar por este número e ver 
            // se já foi efetuado o pagamento

            // cedente

            var cedenteNossoNumeroBoleto = dadosAuxiliares.ProximoNossoNumero.Value; // o final do nosso número é incrementado ao final


            // obtendo a configuração do cedente
            IConfiguracaoDeModulo configuracaoDoCedente;
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracoesDoModulo>())
            {
                configuracaoDoCedente = servico.ObtenhaConfiguracao();
            }

            var cedenteCpfCnpj = string.Empty;
            var cedenteNome = string.Empty;
            var cedenteAgencia = string.Empty;
            var cedenteConta = string.Empty;
            var cedenteDigitoConta = string.Empty;
            var cedenteOperacaoConta = string.Empty;
            var cedenteCodigo = string.Empty;
            var imagemDoRecibo = string.Empty;

            if(configuracaoDoCedente != null && configuracaoDoCedente.ConfiguracaoDeBoletoBancario != null &&
                configuracaoDoCedente.ConfiguracaoDeBoletoBancario.Cedente != null && 
                configuracaoDoCedente.ConfiguracaoDeBoletoBancario.Cedente.Pessoa != null)
            {
                imagemDoRecibo = configuracaoDoCedente.ConfiguracaoDeBoletoBancario.ImagemDeCabecalhoDoReciboDoSacado;

                var cedentePessoa = configuracaoDoCedente.ConfiguracaoDeBoletoBancario.Cedente.Pessoa;

                cedenteNome = cedentePessoa.Nome;

                if (cedentePessoa.DadoBancario != null)
                {
                    cedenteAgencia = cedentePessoa.DadoBancario.Agencia.Numero;
                    cedenteConta = cedentePessoa.DadoBancario.Conta.Numero;
                    cedenteOperacaoConta = cedentePessoa.DadoBancario.Conta.Tipo.ToString();

                    if(!string.IsNullOrEmpty(cedenteConta))
                    {
                        cedenteDigitoConta = cedenteConta.Substring(cedenteConta.Length -1, 1);
                    }
                }

                if (cedentePessoa.Tipo == TipoDePessoa.Fisica)
                {
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                    {
                        var pessoaFisica = servico.ObtenhaPessoa(cedentePessoa.ID.Value);

                        if (pessoaFisica != null)
                        {
                            var cpf = pessoaFisica.ObtenhaDocumento(TipoDeDocumento.CPF);

                            if (cpf != null)
                                cedenteCpfCnpj = cpf.ToString();
                        }
                    }
                }
                else
                {
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaJuridica>())
                    {
                        var pessoaJuridica = servico.ObtenhaPessoa(cedentePessoa.ID.Value);

                        if (pessoaJuridica != null)
                        {
                            var cnpj = pessoaJuridica.ObtenhaDocumento(TipoDeDocumento.CNPJ);

                            if (cnpj != null)
                                cedenteCpfCnpj = cnpj.ToString();
                        }
                    }
                }

            }

            // formata código do cedente
            if(!string.IsNullOrEmpty(cedenteOperacaoConta) && !string.IsNullOrEmpty(cedenteConta))
            {
                string operacao;
                switch (cedenteOperacaoConta.Length)
                {
                    case 1:
                        operacao = "00" + cedenteOperacaoConta;
                        break;
                    case 2:
                        operacao = "0" + cedenteOperacaoConta;
                        break;
                    default:
                        operacao = cedenteOperacaoConta;
                        break;
                }

                string conta;
                var contaSemDigito = cedenteConta.Substring(0, cedenteConta.Length - 1);
                switch (contaSemDigito.Length)
                {
                    case 6:
                        conta = "00" + contaSemDigito;
                        break;
                    case 7:
                        conta = "0" + contaSemDigito;
                        break;
                    default:
                        conta = contaSemDigito;
                        break;
                }

                cedenteCodigo = operacao + conta;
            }

            var cedente = new Cedente(cedenteCpfCnpj, cedenteNome, cedenteAgencia, cedenteConta, cedenteDigitoConta) { Codigo = cedenteCodigo };

            var boleto = new Boleto(Convert.ToDateTime(vencimento),
                                    (decimal)Convert.ToDouble(valorBoleto), carteira, cedenteNossoNumeroBoleto.ToString(),
                                    cedente) { NumeroDocumento = numeroDocumento.ToString() };

            //sacado         
            var sacadoCpfCnpj = txtCNPJCPF.Text;
            var sacadoNome = txtNome.Text;
            var sacadoEndereco = txtEndereco.Text;
            var sacadoObservacao = txtFinalidadeBoleto.Text;

            var sacado = new Sacado(sacadoCpfCnpj, sacadoNome);
            boleto.Sacado = sacado;
            boleto.Sacado.Endereco.End = sacadoEndereco;
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

            //var urlImagemLogo = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + UtilidadesWeb.PASTA_LOADS + "/Imagens/Marcas/logoReciboBoleto.jpg";
            
            var boletoBancario = new BoletoBancario(imagemDoRecibo);
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

            // Salvar dados do Boleto gerado

            var boletoGerado = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGerados>();

            if (Session["CHAVE_CLIENTE_SELECIONADO"] != null)
            {
                boletoGerado.Cliente = (ICliente)Session["CHAVE_CLIENTE_SELECIONADO"];
            }

            boletoGerado.DataGeracao = boleto.DataProcessamento;
            boletoGerado.DataVencimento = txtVencimento.SelectedDate.Value;
            boletoGerado.NossoNumero = cedenteNossoNumeroBoleto;
            boletoGerado.NumeroBoleto = numeroDocumento;
            boletoGerado.NumeroProcesso = null; // verificar número do processo
            boletoGerado.Observacao = sacadoObservacao;
            boletoGerado.Valor = Convert.ToDouble(valorBoleto);

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoletosGerados>())
            {
                servico.Inserir(boletoGerado);
            }

            // incrementar o nosso numero e o numero do documento e atualizar no banco.

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoletosGeradosAux>())
            {
                dadosAuxiliares.ProximoNossoNumero = dadosAuxiliares.ProximoNossoNumero + 1;
                dadosAuxiliares.ProximoNumeroBoleto = dadosAuxiliares.ProximoNumeroBoleto + 1;

                servico.AtualizarProximasInformacoes(dadosAuxiliares);
            }
        }

        private bool ExisteErroDePreenchimento()
        {
            if(!txtVencimento.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Selecione uma data para vencimento do boleto."),
                                                    false);

                return true;
            }
            if(string.IsNullOrEmpty(txtValor.Text))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Informe o valor do boleto para cobrança."),
                                                    false);

                return true;
            }

            return false;
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