using System;
using System.Collections.Generic;
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
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmBoletoAvulso : SuperPagina
    {
        //Todo verificar carregamento do tipo de carteira
        public const string carteira = "SR";

        public const string CHAVE_CLIENTE_SELECIONADO = "CHAVE_CLIENTE_SELECIONADO";
        public const string CHAVE_CEDENTE_SELECIONADO = "CHAVE_CEDENTE_SELECIONADO";

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlCedente.CedenteFoiSelecionado += ctrlCedente_CedenteFoiSelecionado;
            ctrlCliente.ClienteFoiSelecionado += ctrlCliente_ClienteFoiSelecionado;

            if (!IsPostBack)
                ExibaTelaInicial();
        }

        private void ctrlCedente_CedenteFoiSelecionado(ICedente cliente)
        {
            if (cliente == null)
            {
                ExibaTelaInicial();
                Session["CHAVE_CEDENTE_SELECIONADO"] = null;
                return;
            }

            Session.Add("CHAVE_CEDENTE_SELECIONADO", cliente);
            ctrlCliente.Visible = true;
            ctrlCliente.BotaoNovoEhVisivel = true;
            lblCliente.Visible = true;
        }

        private void ctrlCliente_ClienteFoiSelecionado(ICliente cliente)
        {
            try
            {
                Session.Add("CHAVE_CLIENTE_SELECIONADO", cliente);

                pnlDados.Visible = true;

                if (cliente.Pessoa != null)
                {
                    txtNome.Text = cliente.Pessoa.Nome;

                    IPessoa pessoa = null;

                    if (cliente.Pessoa.Tipo == TipoDePessoa.Fisica)
                    {
                        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                        {
                            var pessoaFisica = servico.ObtenhaPessoa(cliente.Pessoa.ID.Value);

                            if (pessoaFisica != null)
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
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar informações do cliente selecionado, exceção: ", ex);
            }
        }

        private void ExibaTelaInicial()
        {
            ctrlCedente.Inicializa();
            ctrlCliente.Inicializa();
            //ctrlCliente.BotaoNovoEhVisivel = true;
            pnlDados.Visible = false;
            ctrlCliente.Visible = false;
            lblCliente.Visible = false;
        }

        protected void btnGerarBoleto_ButtonClick(object sender, EventArgs e)
        {
            // Boleto para CEF, com carteira SR e nosso número de 11 dígitos.

            if (ExisteErroDePreenchimento())
            {
                return;
            }

            try
            {
                var vencimento = txtVencimento.SelectedDate.Value.ToString("dd/MM/yyyy");
                var valorBoleto = txtValor.Text;

                IBoletosGeradosAux dadosAuxiliares;

                // Busca dados auxiliares no banco, se for a primeira vez, efetua o insert

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                {
                    dadosAuxiliares = servico.obtenhaProximasInformacoesParaGeracaoDoBoleto();

                    if (!dadosAuxiliares.ID.HasValue)
                    {
                        var boletosGeradosAux = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGeradosAux>();

                        boletosGeradosAux.ID = GeradorDeID.getInstancia().getProximoID();
                        boletosGeradosAux.ProximoNossoNumero = 8210001001;

                        servico.InserirPrimeiraVez(boletosGeradosAux);

                        dadosAuxiliares = servico.obtenhaProximasInformacoesParaGeracaoDoBoleto();
                    }
                }

                // Numero do documento - numero de controle interno, não afeta o calculo da linha digitavel e nem o codigo de barras
                // mais pode ser útil, exemplo: quando o cliente ligar, vc pode consultar por este número e ver 
                // se já foi efetuado o pagamento

                // cedente

                var cedenteNossoNumeroBoleto = dadosAuxiliares.ProximoNossoNumero.Value; // o final do nosso número é incrementado ao final


                // obtendo a configuração do cedente
                //IConfiguracaoDeBoletoBancario configuracaoDoBoleto;

                //using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                //{
                //    configuracaoDoBoleto = servico.ObtenhaConfiguracao();
                //}

                var cedenteSelecionado = (ICedente)Session["CHAVE_CEDENTE_SELECIONADO"];

                var cedenteCpfCnpj = string.Empty;
                var cedenteNome = string.Empty;
                var cedenteAgencia = string.Empty;
                var cedenteConta = string.Empty;
                var cedenteDigitoConta = string.Empty;
                var cedenteOperacaoConta = string.Empty;
                var cedenteCodigo = string.Empty;
                var imagemDoRecibo = string.Empty;
                var codigoDoBanco = 0;

                if (cedenteSelecionado != null)
                {
                    // pegar imagem do cedente para o boleto
                    //imagemDoRecibo = configuracaoDoBoleto.ImagemDeCabecalhoDoReciboDoSacado; 

                    if(!string.IsNullOrEmpty(cedenteSelecionado.ImagemDeCabecalhoDoReciboDoSacado))
                    {
                        imagemDoRecibo = cedenteSelecionado.ImagemDeCabecalhoDoReciboDoSacado; 
                    }

                    var cedentePessoa = cedenteSelecionado.Pessoa;

                    cedenteNome = cedentePessoa.Nome;

                    if (cedentePessoa.DadoBancario != null)
                    {
                        codigoDoBanco = cedentePessoa.DadoBancario.Agencia.Banco.Numero;
                        cedenteAgencia = cedentePessoa.DadoBancario.Agencia.Numero;
                        cedenteConta = cedentePessoa.DadoBancario.Conta.Numero;
                        cedenteOperacaoConta = cedentePessoa.DadoBancario.Conta.Tipo.Value.ToString("000");

                        if (!string.IsNullOrEmpty(cedenteConta))
                        {
                            cedenteDigitoConta = cedenteConta.Substring(cedenteConta.Length - 1, 1);
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
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Configurações do cedente ou boleto bancário inválidas, favor verificar."),
                                                        false);

                    return;
                }

                //if (configuracaoDoBoleto != null)
                //{
                //    imagemDoRecibo = configuracaoDoBoleto.ImagemDeCabecalhoDoReciboDoSacado;

                //    var cedentePessoa = configuracaoDoBoleto.Cedente.Pessoa;

                //    cedenteNome = cedentePessoa.Nome;

                //    if (cedentePessoa.DadoBancario != null)
                //    {
                //        codigoDoBanco = cedentePessoa.DadoBancario.Agencia.Banco.Numero;
                //        cedenteAgencia = cedentePessoa.DadoBancario.Agencia.Numero;
                //        cedenteConta = cedentePessoa.DadoBancario.Conta.Numero;
                //        cedenteOperacaoConta = cedentePessoa.DadoBancario.Conta.Tipo.Value.ToString("000");

                //        if (!string.IsNullOrEmpty(cedenteConta))
                //        {
                //            cedenteDigitoConta = cedenteConta.Substring(cedenteConta.Length - 1, 1);
                //        }
                //    }

                //    if (cedentePessoa.Tipo == TipoDePessoa.Fisica)
                //    {
                //        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaFisica>())
                //        {
                //            var pessoaFisica = servico.ObtenhaPessoa(cedentePessoa.ID.Value);

                //            if (pessoaFisica != null)
                //            {
                //                var cpf = pessoaFisica.ObtenhaDocumento(TipoDeDocumento.CPF);

                //                if (cpf != null)
                //                    cedenteCpfCnpj = cpf.ToString();
                //            }
                //        }
                //    }
                //    else
                //    {
                //        using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDePessoaJuridica>())
                //        {
                //            var pessoaJuridica = servico.ObtenhaPessoa(cedentePessoa.ID.Value);

                //            if (pessoaJuridica != null)
                //            {
                //                var cnpj = pessoaJuridica.ObtenhaDocumento(TipoDeDocumento.CNPJ);

                //                if (cnpj != null)
                //                    cedenteCpfCnpj = cnpj.ToString();
                //            }
                //        }
                //    }
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                //                                        UtilidadesWeb.MostraMensagemDeInformacao("Configurações do cedente ou boleto bancário inválidas, favor verificar."),
                //                                        false);

                //    return;
                //}

                // formata código do cedente
                if (!string.IsNullOrEmpty(cedenteOperacaoConta) && !string.IsNullOrEmpty(cedenteConta))
                {
                    string conta;
                    var contaSemDigito = Convert.ToInt32(cedenteConta.Substring(0, cedenteConta.Length - 1));

                    conta = contaSemDigito.ToString("00000000");
                    cedenteCodigo = cedenteOperacaoConta + conta;
                }

                var cedente = new Cedente(cedenteCpfCnpj, cedenteNome, cedenteAgencia, cedenteConta, cedenteDigitoConta) { Codigo = cedenteCodigo };

                var boleto = new Boleto(Convert.ToDateTime(vencimento),
                                        (decimal)Convert.ToDouble(valorBoleto), carteira, cedenteNossoNumeroBoleto.ToString(),
                                        cedente) { NumeroDocumento = txtNumeroDoBoleto.Text };

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

                var instrucaoCaixa = new Instrucao_Caixa { Descricao = txtInstrucoes.Text };
                instrucaoCaixa.DescricaoReciboDoSacado = sacadoObservacao;
                boleto.Instrucoes.Add(instrucaoCaixa);

                var especificacao = new EspecieDocumento_Caixa();
                boleto.EspecieDocumento = new EspecieDocumento_Caixa(especificacao.getCodigoEspecieByEnum(EnumEspecieDocumento_Caixa.Recibo));

                boleto.DataProcessamento = DateTime.Now;
                boleto.DataDocumento = DateTime.Now;

                BoletoBancario boletoBancario;

                if (string.IsNullOrEmpty(imagemDoRecibo))
                    boletoBancario = new BoletoBancario();
                else
                {
                    var urlImagemLogo = UtilidadesWeb.ObtenhaURLHostDiretorioVirtual() + imagemDoRecibo.Remove(0, 1);
                    boletoBancario = new BoletoBancario(urlImagemLogo);
                }

                boletoBancario.CodigoBanco = (short)codigoDoBanco;
                boletoBancario.Boleto = boleto;
                boletoBancario.MostrarCodigoCarteira = true;
                boletoBancario.Boleto.Valida();
                boletoBancario.MostrarComprovanteEntrega = false;

                var chaveDoBoleto = Guid.NewGuid().ToString();

                Session.Add(chaveDoBoleto, boletoBancario);

                var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmVisualizarBoletoGerado.aspx",
                                                "?Id=", chaveDoBoleto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.ExibeJanela(url,
                                                                                   "Visualizar boleto gerado",
                                                                                   800, 600, "frmVisualizarBoletoGerado_aspx"), false);

                // Salvar dados do Boleto gerado

                var boletoGerado = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGerados>();

                if (Session["CHAVE_CLIENTE_SELECIONADO"] != null)
                {
                    boletoGerado.Cliente = (ICliente)Session["CHAVE_CLIENTE_SELECIONADO"];
                }

                boletoGerado.DataGeracao = boleto.DataProcessamento;
                boletoGerado.DataVencimento = txtVencimento.SelectedDate.Value;
                boletoGerado.NossoNumero = cedenteNossoNumeroBoleto;
                boletoGerado.NumeroBoleto = txtNumeroDoBoleto.Text;
                boletoGerado.Observacao = sacadoObservacao;
                boletoGerado.Valor = Convert.ToDouble(valorBoleto);

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                {
                    servico.Inserir(boletoGerado);
                }

                // incrementar o nosso numero e o numero do documento e atualizar no banco.

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                {
                    dadosAuxiliares.ProximoNossoNumero = dadosAuxiliares.ProximoNossoNumero + 1;

                    servico.AtualizarProximasInformacoes(dadosAuxiliares);
                }
            }
            catch (Exception ex)
            {
                Logger.GetInstancia().Erro("Erro ao gerar boleto, exceção: ", ex);
            }
        }

        private bool ExisteErroDePreenchimento()
        {
            if (!txtVencimento.SelectedDate.HasValue)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.MostraMensagemDeInformacao("Selecione uma data para vencimento do boleto."),
                                                    false);

                return true;
            }
            if (string.IsNullOrEmpty(txtValor.Text))
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
            return "FUN.FN.002";
        }

        protected override RadToolBar ObtenhaBarraDeFerramentas()
        {
            return null;
        }
    }
}