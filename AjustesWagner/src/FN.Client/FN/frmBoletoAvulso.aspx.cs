using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using BoletoNet;
using Compartilhados;
using Compartilhados.Componentes.Web;
using Compartilhados.Fabricas;
using Compartilhados.Interfaces;
using Compartilhados.Interfaces.Core.Negocio;
using Compartilhados.Interfaces.Core.Servicos;
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using FN.ImagePDF;
using FN.Interfaces;
using FN.Interfaces.Negocio;
using FN.Interfaces.Negocio.Filtros.BoletosGerados;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using Image = System.Web.UI.WebControls.Image;

namespace FN.Client.FN
{
    public partial class frmBoletoAvulso : SuperPagina
    {
        //Todo verificar carregamento do tipo de carteira
        //public const string carteira = "SR";
        public const string CHAVE_CEDENTE_BOLETOGERADO = "CHAVE_CEDENTE_BOLETOGERADO";
        public const string CHAVE_LISTA_ITEM_FINANCEIRO = "CHAVE_LISTA_ITEM_FINANCEIRO";
       
        public IBoletosGerados BoletoGerado { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ctrlCedente.CedenteFoiSelecionado += ctrlCedente_CedenteFoiSelecionado;
            ctrlCliente.ClienteFoiSelecionado += ctrlCliente_ClienteFoiSelecionado;

            if (!IsPostBack)
            {
                long? id = null;

                if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
                    id = Convert.ToInt64(Request.QueryString["Id"]);

                string itensFinanceiros = null;

                if (!String.IsNullOrEmpty(Request.QueryString["ItensFinanceiros"]))
                    itensFinanceiros = Request.QueryString["ItensFinanceiros"];

                if (id == null && itensFinanceiros == null)
                {
                    ExibaTelaInicial();

                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeCedente>())
                    {
                        var idCedente = servico.ObtenhaCedentePadrao();

                        if(idCedente > 0)
                        {
                            var cedentePadrao = servico.Obtenha(idCedente);

                            ctrlCedente.CedenteSelecionado = cedentePadrao;
                            PreenchaDadosDoCedente(cedentePadrao);
                        }
                    }
                }
                    
                else
                    if (itensFinanceiros == null)
                        ExibaTelaBoletoGerado(id.Value);
                    else
                    {
                        ExibaItensFinanceiros(itensFinanceiros);
                        return;
                    }
                        

                CarregueInstrucoesDoBoleto();
            }
        }

        private bool BoletoGeraItemFinanceiroDeRecebimento
        {
            get { return (bool)ViewState["GERAITENFINANCEIRORECEBIMENTO"]; }
            set { ViewState["GERAITENFINANCEIRORECEBIMENTO"] = value; }
        }

        private void ExibaItensFinanceiros(string itens)
        {
            ExibaTelaInicial();

            BoletoGeraItemFinanceiroDeRecebimento = false;
            var ids = new List<string>(itens.Split('|'));

            IList<IItemLancamentoFinanceiroRecebimento> listaDeItensFinanceiros =
                new List<IItemLancamentoFinanceiroRecebimento>();

            foreach (var idItemFinanceiro in ids)
            {
                IItemLancamentoFinanceiroRecebimento itemLancamento;

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                    itemLancamento = servico.Obtenha(Convert.ToInt64(idItemFinanceiro));

                if (itemLancamento != null)
                    listaDeItensFinanceiros.Add(itemLancamento);
            }

            if (listaDeItensFinanceiros.Count > 0)
            {
                double valor = 0;

                valor = listaDeItensFinanceiros.Count > 1 ? listaDeItensFinanceiros.Aggregate(valor, (current, itemLancamentoFinanceiroRecebimento) => current + itemLancamentoFinanceiroRecebimento.Valor) : listaDeItensFinanceiros[0].Valor;

                var itemLancamentoFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IItemLancamentoFinanceiroRecebimento>();

                itemLancamentoFinanceiro.Cliente = listaDeItensFinanceiros[0].Cliente;
                itemLancamentoFinanceiro.Valor = valor;
                itemLancamentoFinanceiro.DataDoVencimento = listaDeItensFinanceiros[0].DataDoVencimento;

                ViewState["CHAVE_ITEM_FINANCEIRO_SELECIONADO"] = itemLancamentoFinanceiro;
                ViewState["CHAVE_LISTA_ITEM_FINANCEIRO"] = listaDeItensFinanceiros;

                    foreach (var itemLancamentoFinanceiroRecebimento in listaDeItensFinanceiros)
                    {
                        long idboletoGerado;

                        using (var servicoFinanceiroComBoleto = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItemFinanceiroRecebidoComBoleto>())
                        {
                            idboletoGerado =
                                servicoFinanceiroComBoleto.ObtenhaBoletoPorIdItemFinanRecebimento(
                                    itemLancamentoFinanceiroRecebimento.ID.Value);
                        }

                        if (idboletoGerado > 0)
                        {
                            IBoletosGerados boletoGeradoParaItemFinanceiro;

                            using (var servicoBoletoGeradoParaItemFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                            {
                                boletoGeradoParaItemFinanceiro =
                                    servicoBoletoGeradoParaItemFinanceiro.obtenhaBoletoPeloId(
                                        idboletoGerado);
                            }

                            txtNumeroDoBoleto.Text = boletoGeradoParaItemFinanceiro.NumeroBoleto;
                            txtInstrucoes.Text = boletoGeradoParaItemFinanceiro.Instrucoes;
                            txtFinalidadeBoleto.Text = boletoGeradoParaItemFinanceiro.Observacao;

                            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeCedente>())
                            {
                                var cedenteDoBoleto = servico.Obtenha(boletoGeradoParaItemFinanceiro.Cedente.Pessoa.ID.Value);

                                ctrlCedente.CedenteSelecionado = cedenteDoBoleto;
                                PreenchaDadosDoCedente(cedenteDoBoleto);

                                DesabilitaCamposParaEdicao();

                                return;
                            }
                        }

                        CarregueInstrucoesDoBoleto();

                        break;
                    }
            }

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeCedente>())
            {
                var idCedente = servico.ObtenhaCedentePadrao();

                if (idCedente > 0)
                {
                    var cedentePadrao = servico.Obtenha(idCedente);

                    ctrlCedente.CedenteSelecionado = cedentePadrao;
                    PreenchaDadosDoCedente(cedentePadrao);
                }
            }

            DesabilitaCamposParaEdicaoComCedenteHabilitado();
        }

        private void ExibaTelaBoletoGerado(long idBoleto)
        {
            ExibaTelaInicial();

            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
            {
                var boleto = servico.obtenhaBoletoPeloId(idBoleto);

                if (boleto != null)
                {
                    ViewState.Add(CHAVE_CEDENTE_BOLETOGERADO, boleto);
                    //BoletoGerado = boleto;

                    long idCedente = boleto.Cedente.Pessoa.ID.Value;

                    using (var servicoCedente = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeCedente>())
                    {
                        if (idCedente > 0)
                        {
                            var cedentePadrao = servicoCedente.Obtenha(idCedente);

                            ctrlCedente.CedenteSelecionado = cedentePadrao;
                            PreenchaDadosDoCedente(cedentePadrao);
                        }
                    }

                    if (ctrlCedente.CedenteSelecionado == null && boleto.Cedente != null)
                    {
                        this.ctrlCedente.CedenteSelecionado = boleto.Cedente;
                        PreenchaDadosDoCedente(boleto.Cedente);
                    } 

                    this.ctrlCliente.ClienteSelecionado = boleto.Cliente;
                    PreenchaDadosDoClienteSelecionado(boleto.Cliente);

                    PreenchaInformacoesDoBoleto(boleto);

                    if(boleto.StatusBoleto.ToUpper() == StatusBoleto.Status.Pago.ToString().ToUpper())
                    {
                        DesabilitaCamposParaEdicaoBoletoPago();
                    }
                    else
                    {
                        DesabilitaCamposParaEdicao();
                    }
                }
            }
        }

        private void DesabilitaCamposParaEdicao()
        {
            this.ctrlCliente.DesabilitaComboParaEdicao();
            this.ctrlCedente.DesabilitaComboParaEdicao();
            txtCNPJCPF.Enabled = false;
            txtCep.Enabled = false;
            txtCidade.Enabled = false;
            txtEndereco.Enabled = false;
            txtEstado.Enabled = false;
            txtNome.Enabled = false;
            txtBairro.Enabled = false;
        }

        private void DesabilitaCamposParaEdicaoComCedenteHabilitado()
        {
            this.ctrlCliente.DesabilitaComboParaEdicao();
            txtCNPJCPF.Enabled = false;
            txtCep.Enabled = false;
            txtCidade.Enabled = false;
            txtEndereco.Enabled = false;
            txtEstado.Enabled = false;
            txtNome.Enabled = false;
            txtBairro.Enabled = false;
        }

        private void DesabilitaCamposParaEdicaoBoletoPago()
        {
            this.ctrlCliente.DesabilitaComboParaEdicao();
            this.ctrlCedente.DesabilitaComboParaEdicao();
            txtCNPJCPF.Enabled = false;
            txtCep.Enabled = false;
            txtCidade.Enabled = false;
            txtEndereco.Enabled = false;
            txtEstado.Enabled = false;
            txtNome.Enabled = false;
            txtBairro.Enabled = false;
            txtVencimento.Enabled = false;
            txtNumeroDoBoleto.Enabled = false;
            txtInstrucoes.Enabled = false;
            txtFinalidadeBoleto.Enabled = false;
            txtValor.Enabled = false;
            //btnGerarBoleto.Enabled = false;
        }

        private void ctrlCedente_CedenteFoiSelecionado(ICedente cedente)
        {
            ctrlCedente.CedenteSelecionado = cedente;
            PreenchaDadosDoCedente(cedente);
        }

        private void PreenchaDadosDoCedente(ICedente cedente)
        {
            if (cedente == null)
            {
                ExibaTelaInicial();
                return;
            }

            ctrlCliente.Visible = true;
            ctrlCliente.BotaoNovoEhVisivel = true;
            lblCliente.Visible = true;

            if (ViewState["CHAVE_ITEM_FINANCEIRO_SELECIONADO"] != null)
                PreenchaDadosDosItensFinanceiros(
                    (IItemLancamentoFinanceiroRecebimento)ViewState["CHAVE_ITEM_FINANCEIRO_SELECIONADO"]);
        }

        private void PreenchaDadosDosItensFinanceiros(IItemLancamentoFinanceiroRecebimento itemLancamentoFinanceiroRecebimento)
        {
            ctrlCliente.ClienteSelecionado = itemLancamentoFinanceiroRecebimento.Cliente;
            PreenchaDadosDoClienteSelecionado(itemLancamentoFinanceiroRecebimento.Cliente);
            txtValor.Value = itemLancamentoFinanceiroRecebimento.Valor;
            txtVencimento.SelectedDate = itemLancamentoFinanceiroRecebimento.DataDoVencimento;

            //ViewState["CHAVE_ITEM_FINANCEIRO_SELECIONADO"] = null;
        }

        private void ctrlCliente_ClienteFoiSelecionado(ICliente cliente)
        {
            ctrlCliente.ClienteSelecionado = cliente;
            PreenchaDadosDoClienteSelecionado(cliente);
        }

        private string OtenhaNumeroCPFOuCNPJ(IPessoa pessoa)
        {
            if (pessoa.Tipo.Equals(TipoDePessoa.Fisica))
            {
                var cpf = pessoa.ObtenhaDocumento(TipoDeDocumento.CPF);

                return cpf != null ? cpf.ToString() : "";
            }

            var cnpj = pessoa.ObtenhaDocumento(TipoDeDocumento.CNPJ);

            return cnpj != null ? cnpj.ToString() : "";
        }

        private void PreenchaDadosDoClienteSelecionado(ICliente cliente)
        {
            try
            {
               pnlDados.Visible = true;

                if (cliente.Pessoa != null)
                {
                    txtNome.Text = cliente.Pessoa.Nome;
                    txtCNPJCPF.Text = OtenhaNumeroCPFOuCNPJ(cliente.Pessoa);

                    if (cliente.Pessoa.Enderecos != null && cliente.Pessoa.Enderecos.Count > 0)
                    {
                        foreach (var endereco in cliente.Pessoa.Enderecos)
                        {
                            if(endereco.TipoDeEndereco != null)
                            {
                                if(endereco.TipoDeEndereco.Nome.ToUpper().Equals("COBRANÇA".ToUpper()) ||
                                    endereco.TipoDeEndereco.Nome.ToUpper().Equals("COBRANCA".ToUpper()))
                                {
                                    txtEndereco.Text = endereco.Logradouro + " - " + endereco.Complemento;
                                    txtBairro.Text = endereco.Bairro;

                                    if (endereco.Municipio != null)
                                    {
                                        txtCidade.Text = endereco.Municipio.Nome;

                                        if (endereco.Municipio.UF != null)
                                            txtEstado.Text = endereco.Municipio.UF.Sigla;
                                    }

                                    if (endereco.CEP != null && endereco.CEP.Numero.HasValue)
                                        txtCep.Text = endereco.CEP.Numero.Value.ToString();

                                    break;
                                }
                            }
                        }

                        if (string.IsNullOrEmpty(txtEndereco.Text))
                        {
                            txtEndereco.Text = cliente.Pessoa.Enderecos[0].Logradouro + " - " + cliente.Pessoa.Enderecos[0].Complemento;
                            txtBairro.Text = cliente.Pessoa.Enderecos[0].Bairro;

                            if (cliente.Pessoa.Enderecos[0].Municipio != null)
                            {
                                txtCidade.Text = cliente.Pessoa.Enderecos[0].Municipio.Nome;

                                if (cliente.Pessoa.Enderecos[0].Municipio.UF != null)
                                    txtEstado.Text = cliente.Pessoa.Enderecos[0].Municipio.UF.Sigla;
                            }

                            if (cliente.Pessoa.Enderecos[0].CEP != null && cliente.Pessoa.Enderecos[0].CEP.Numero.HasValue)
                                txtCep.Text = cliente.Pessoa.Enderecos[0].CEP.Numero.Value.ToString();
                        }
                    }
                }
            }
            catch (BussinesException ex)
            {
                Logger.GetInstancia().Erro("Erro ao carregar informações do cliente selecionado, exceção: ", ex);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia(
                                                            "Erro ao carregar informações do cliente selecionado, exceção: " +
                                                            ex.Message), false);
            }
        }

        private void ExibaTelaInicial()
        {
            BoletoGeraItemFinanceiroDeRecebimento = true;
            ctrlCedente.Inicializa();
            ctrlCliente.Inicializa();
            ctrlCliente.BotaoNovoEhVisivel = true;
            ctrlCedente.BotaoNovoEhVisivel = true;
            pnlDados.Visible = false;
            ctrlCliente.Visible = false;
            lblCliente.Visible = false;
        }

        private void PreenchaInformacoesDoBoleto(IBoletosGerados boleto)
        {
            txtVencimento.SelectedDate = boleto.DataVencimento;
            txtValor.Text = boleto.Valor.ToString();
            txtNumeroDoBoleto.Text = boleto.NumeroBoleto;
            txtFinalidadeBoleto.Text = boleto.Observacao;
            txtInstrucoes.Text = boleto.Instrucoes;
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
                var valorBoleto = txtValor.Value.Value;

                IBoletosGeradosAux dadosAuxiliares = null;

                // Busca dados auxiliares no banco, se for a primeira vez, efetua o insert

                long cedenteNossoNumeroBoleto = 0;

                if (ViewState["CHAVE_CEDENTE_BOLETOGERADO"] != null)
                {
                    // Numero do documento - numero de controle interno, não afeta o calculo da linha digitavel e nem o codigo de barras
                    // mais pode ser útil, exemplo: quando o cliente ligar, vc pode consultar por este número e ver 
                    // se já foi efetuado o pagamento

                    // cedente

                    BoletoGerado = (IBoletosGerados)ViewState["CHAVE_CEDENTE_BOLETOGERADO"];

                    cedenteNossoNumeroBoleto = BoletoGerado.NossoNumero.Value; // o final do nosso número é incrementado ao final

                }
                else if (ViewState["CHAVE_LISTA_ITEM_FINANCEIRO"] != null)
                {
                    var listaDeItensFinanceiros =
                                            (List<IItemLancamentoFinanceiroRecebimento>)ViewState["CHAVE_LISTA_ITEM_FINANCEIRO"];

                    if (listaDeItensFinanceiros.Count > 0)
                    {
                        foreach (var itemLancamentoFinanceiroRecebimento in listaDeItensFinanceiros)
                        {
                            long idboleto;

                            using (var servicoFinanceiroComBoleto = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItemFinanceiroRecebidoComBoleto>())
                                idboleto =
                                    servicoFinanceiroComBoleto.ObtenhaBoletoPorIdItemFinanRecebimento(
                                        itemLancamentoFinanceiroRecebimento.ID.Value);

                            if (idboleto > 0)
                            {
                                IBoletosGerados boletoGeradoParaItemFinanceiro;

                                using (var servicoBoletoGeradoParaItemFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                                    boletoGeradoParaItemFinanceiro =
                                        servicoBoletoGeradoParaItemFinanceiro.obtenhaBoletoPeloId(
                                            idboleto);

                                if (boletoGeradoParaItemFinanceiro != null)
                                    cedenteNossoNumeroBoleto = boletoGeradoParaItemFinanceiro.NossoNumero.Value;
                            }
                            else
                            {
                                 using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                                     dadosAuxiliares = 
                                         servico.obtenhaProximasInformacoesParaGeracaoDoBoleto(ctrlCedente.CedenteSelecionado);

                                 cedenteNossoNumeroBoleto = dadosAuxiliares.ProximoNossoNumero.Value;
                            }

                            break;
                        }
                    }
                }
                else
                {
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                        dadosAuxiliares = servico.obtenhaProximasInformacoesParaGeracaoDoBoleto(ctrlCedente.CedenteSelecionado);

                    // Numero do documento - numero de controle interno, não afeta o calculo da linha digitavel e nem o codigo de barras
                    // mais pode ser útil, exemplo: quando o cliente ligar, vc pode consultar por este número e ver 
                    // se já foi efetuado o pagamento

                    // cedente

                    cedenteNossoNumeroBoleto = dadosAuxiliares.ProximoNossoNumero.Value; // o final do nosso número é incrementado ao final

                }

                // obtendo a configuração do cedente

                var cedenteSelecionado = ctrlCedente.CedenteSelecionado;

                var cedenteCpfCnpj = string.Empty;
                var cedenteNome = string.Empty;
                var cedenteAgencia = string.Empty;
                var cedenteConta = string.Empty;
                var cedenteDigitoConta = string.Empty;
                var cedenteOperacaoConta = string.Empty;
                var cedenteCodigo = string.Empty;
                var imagemDoRecibo = string.Empty;
                var codigoDoBanco = 0;
                var carteira = string.Empty;

                if (cedenteSelecionado != null)
                {
                    // pegar imagem do cedente para o boleto
                    //imagemDoRecibo = configuracaoDoBoleto.ImagemDeCabecalhoDoReciboDoSacado; 

                    if (!string.IsNullOrEmpty(cedenteSelecionado.ImagemDeCabecalhoDoReciboDoSacado))
                        imagemDoRecibo = cedenteSelecionado.ImagemDeCabecalhoDoReciboDoSacado;

                    var cedentePessoa = cedenteSelecionado.Pessoa;

                    cedenteNome = cedentePessoa.Nome;

                    codigoDoBanco = Convert.ToInt32(cedenteSelecionado.NumeroDoBanco);
                    cedenteAgencia = cedenteSelecionado.NumeroDaAgencia;
                    cedenteConta = cedenteSelecionado.NumeroDaConta;
                    cedenteOperacaoConta = cedenteSelecionado.TipoDaConta.ToString("000");

                    if (!string.IsNullOrEmpty(cedenteConta))
                        cedenteDigitoConta = cedenteConta.Substring(cedenteConta.Length - 1, 1);

                    //if (cedentePessoa.DadoBancario != null)
                    //{
                    //    codigoDoBanco = cedentePessoa.DadoBancario.Agencia.Banco.Numero;
                    //    cedenteAgencia = cedentePessoa.DadoBancario.Agencia.Numero;
                    //    cedenteConta = cedentePessoa.DadoBancario.Conta.Numero;
                    //    cedenteOperacaoConta = cedentePessoa.DadoBancario.Conta.Tipo.Value.ToString("000");

                    //    if (!string.IsNullOrEmpty(cedenteConta))
                    //        cedenteDigitoConta = cedenteConta.Substring(cedenteConta.Length - 1, 1);
                    //}

                    cedenteCpfCnpj = OtenhaNumeroCPFOuCNPJ(cedentePessoa);
                    carteira = cedenteSelecionado.TipoDeCarteira.Sigla;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInformacao("Configurações do cedente ou boleto bancário inválidas, favor verificar."),
                                                        false);

                    return;
                }

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
                                        (decimal)valorBoleto, carteira, cedenteNossoNumeroBoleto.ToString(),
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
                Session.Add("HabilitarBotaoImprimir", true);

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracaoGeralFinanceiro>())
                {
                    var configuracaoGeral = servico.Obtenha();

                    if (configuracaoGeral != null)
                        Session.Add("HabilitarBotaoImprimir", configuracaoGeral.HabilitarBotaoImprimir);
                }

                MostraBoleto(boletoBancario);

                var url = String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmVisualizarBoletoGerado.aspx",
                                                "?Id=", chaveDoBoleto);
                ScriptManager.RegisterStartupScript(this, this.GetType(), Guid.NewGuid().ToString(),
                                                    UtilidadesWeb.ExibeJanela(url,
                                                                                   "Visualizar boleto gerado",
                                                                                   800, 600, "FN_frmVisualizarBoletoGerado_aspx"), false);

                // Salvar dados do Boleto gerado

                var boletoGerado = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGerados>();
                boletoGerado.Cedente = ctrlCedente.CedenteSelecionado;

                boletoGerado.Cliente = ctrlCliente.ClienteSelecionado;

                boletoGerado.DataGeracao = boleto.DataProcessamento;
                boletoGerado.DataVencimento = txtVencimento.SelectedDate.Value;
                boletoGerado.NossoNumero = cedenteNossoNumeroBoleto;
                boletoGerado.NumeroBoleto = txtNumeroDoBoleto.Text;
                boletoGerado.Observacao = sacadoObservacao;
                boletoGerado.Valor = valorBoleto;
                boletoGerado.Instrucoes = txtInstrucoes.Text;

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                {

                    if (BoletoGerado != null)
                    {
                        boletoGerado.ID = BoletoGerado.ID.Value;
                        boletoGerado.StatusBoleto = BoletoGerado.StatusBoleto;
                        servico.AtualizarBoletoGerado(boletoGerado);

                        IItemLancamentoFinanceiroRecebimento itemLancamento;
                        long idItemFinanceiroRecebido;

                        using (var servicoDeItemFinanceiroRecebidoComBoleto = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItemFinanceiroRecebidoComBoleto>())
                        {
                            idItemFinanceiroRecebido = servicoDeItemFinanceiroRecebidoComBoleto.ObtenhaItemFinanRecebimentoPorIdBoleto(boletoGerado.ID.Value);
                        }

                        if(idItemFinanceiroRecebido > 0)
                        {
                            using (var servicoFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                            {
                                itemLancamento = servicoFinanceiro.Obtenha(idItemFinanceiroRecebido);

                                if (itemLancamento != null)
                                {
                                    itemLancamento.Cliente = boletoGerado.Cliente;

                                    if (boletoGerado.DataVencimento != null)
                                        itemLancamento.DataDoVencimento = boletoGerado.DataVencimento.Value;

                                    itemLancamento.Valor = boletoGerado.Valor;
                                    
                                    if (boletoGerado.NossoNumero != null)
                                        itemLancamento.NumeroBoletoGerado = boletoGerado.NossoNumero.Value.ToString();

                                    itemLancamento.BoletoFoiGeradoColetivamente = false;
                                    servicoFinanceiro.Modifique(itemLancamento);
                                }
                            }
                        }
                    }
                    else if (ViewState["CHAVE_LISTA_ITEM_FINANCEIRO"] != null)
                    {
                        var listaDeItensFinanceiros =
                            (List<IItemLancamentoFinanceiroRecebimento>) ViewState["CHAVE_LISTA_ITEM_FINANCEIRO"];
                        
                        if (listaDeItensFinanceiros.Count > 0)
                        {
                            // mais de 1 item financeiro selecionado
                            if(listaDeItensFinanceiros.Count > 1)
                            {
                                long idBoletoExistente = 0;

                                foreach (var itemLancamentoFinanceiroRecebimento in listaDeItensFinanceiros)
                                {
                                    long idboleto = 0;

                                    using (var servicoFinanceiroComBoleto = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItemFinanceiroRecebidoComBoleto>())
                                    {
                                        idboleto =
                                            servicoFinanceiroComBoleto.ObtenhaBoletoPorIdItemFinanRecebimento(
                                                itemLancamentoFinanceiroRecebimento.ID.Value);

                                        if (idboleto > 0)
                                        {
                                            idBoletoExistente = idboleto;
                                            //servico.Excluir(idboleto);
                                        }//servicoFinanceiroComBoleto.Excluir(itemLancamentoFinanceiroRecebimento.ID.Value);
                                    }

                                    break;
                                    //using (var servicoFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                                    //{
                                    //    itemLancamentoFinanceiroRecebimento.Situacao = Situacao.Cancelada;
                                    //    itemLancamentoFinanceiroRecebimento.Observacao =
                                    //        "Item de contas a receber foi cancelado, para a geração de um novo item.";

                                    //    servicoFinanceiro.Modifique(itemLancamentoFinanceiroRecebimento);
                                    //}
                                }

                                if (idBoletoExistente > 0)
                                {
                                    // Este caso vai acontecer quando existe boleto gerado(mesmo boleto) para as contas a receber
                                    // ou seja, os dados do boleto vai atualizar

                                    using (var servicoBoletoGeradoParaItemFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                                    {
                                        var boletoGeradoParaItemFinanceiro =
                                            servicoBoletoGeradoParaItemFinanceiro.obtenhaBoletoPeloId(
                                                idBoletoExistente);

                                        boletoGeradoParaItemFinanceiro.NumeroBoleto = boletoGerado.NumeroBoleto;
                                        boletoGeradoParaItemFinanceiro.Valor = boletoGerado.Valor;
                                        boletoGeradoParaItemFinanceiro.DataGeracao = boletoGerado.DataGeracao;
                                        boletoGeradoParaItemFinanceiro.DataVencimento = boletoGerado.DataVencimento;
                                        boletoGeradoParaItemFinanceiro.Observacao = boletoGerado.Observacao;
                                        boletoGeradoParaItemFinanceiro.Instrucoes = boletoGerado.Instrucoes;

                                        servico.AtualizarBoletoGerado(boletoGeradoParaItemFinanceiro);
                                    }
                                }
                                else
                                {
                                    // Este caso vai acontecer quando existe mais de uma conta a receber, e está gerando um novo boleto,
                                    // ou seja, não existe boleto gerado para as contas
                                    boletoGerado.StatusBoleto = StatusBoleto.Status.Aberto.ToString();
                                    boletoGerado.EhBoletoAvulso = false;
                                    servico.Inserir(boletoGerado, false, TipoLacamentoFinanceiroRecebimento.RecebimentoDeManutencao);
                                    // incrementar o nosso numero e o numero do documento e atualizar no banco.
                                    dadosAuxiliares.ProximoNossoNumero = dadosAuxiliares.ProximoNossoNumero + 1;
                                    servico.AtualizarProximasInformacoes(dadosAuxiliares);

                                    foreach (var itemLancamentoFinanceiroRecebimento in listaDeItensFinanceiros)
                                    {
                                        using (var servicoFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                                        {
                                            itemLancamentoFinanceiroRecebimento.Situacao = Situacao.CobrancaGerada;
                                            itemLancamentoFinanceiroRecebimento.NumeroBoletoGerado =
                                                boletoGerado.NossoNumero.Value.ToString();

                                            itemLancamentoFinanceiroRecebimento.BoletoFoiGeradoColetivamente = true;
                                            servicoFinanceiro.Modifique(itemLancamentoFinanceiroRecebimento);
                                        }

                                        using (var servicoFinanceiroComBoleto = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItemFinanceiroRecebidoComBoleto>())
                                        {
                                            servicoFinanceiroComBoleto.Insira(itemLancamentoFinanceiroRecebimento.ID.Value, boletoGerado.ID.Value);
                                        }
                                    }
                                }
                                
                            }
                            else
                            {
                                foreach (var itemLancamentoFinanceiroRecebimento in listaDeItensFinanceiros)
                                {
                                    long idboleto;

                                    using (var servicoFinanceiroComBoleto = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItemFinanceiroRecebidoComBoleto>())
                                    {
                                        idboleto =
                                            servicoFinanceiroComBoleto.ObtenhaBoletoPorIdItemFinanRecebimento(
                                                itemLancamentoFinanceiroRecebimento.ID.Value);
                                    }

                                    if (idboleto > 0)
                                    {
                                        boletoGerado.ID = idboleto;
                                        servico.AtualizarBoletoGerado(boletoGerado);

                                        using (var servicoFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                                        {
                                            itemLancamentoFinanceiroRecebimento.Cliente = boletoGerado.Cliente;

                                            if (boletoGerado.DataVencimento != null)
                                                itemLancamentoFinanceiroRecebimento.DataDoVencimento = boletoGerado.DataVencimento.Value;

                                            itemLancamentoFinanceiroRecebimento.Valor = boletoGerado.Valor;

                                            if (boletoGerado.NossoNumero != null)
                                                itemLancamentoFinanceiroRecebimento.NumeroBoletoGerado = boletoGerado.NossoNumero.Value.ToString();

                                            itemLancamentoFinanceiroRecebimento.Situacao = Situacao.CobrancaGerada;
                                            itemLancamentoFinanceiroRecebimento.BoletoFoiGeradoColetivamente = false;

                                            servicoFinanceiro.Modifique(itemLancamentoFinanceiroRecebimento);
                                        }
                                    }
                                    else
                                    {
                                        boletoGerado.StatusBoleto = StatusBoleto.Status.Aberto.ToString();
                                        boletoGerado.EhBoletoAvulso = false;
                                        servico.Inserir(boletoGerado, false, TipoLacamentoFinanceiroRecebimento.RecebimentoDeManutencao);

                                        // incrementar o nosso numero e o numero do documento e atualizar no banco.
                                        dadosAuxiliares.ProximoNossoNumero = dadosAuxiliares.ProximoNossoNumero + 1;
                                        servico.AtualizarProximasInformacoes(dadosAuxiliares);

                                        using (var servicoFinanceiro = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItensFinanceirosDeRecebimento>())
                                        {
                                            itemLancamentoFinanceiroRecebimento.Cliente = boletoGerado.Cliente;

                                            if (boletoGerado.DataVencimento != null)
                                                itemLancamentoFinanceiroRecebimento.DataDoVencimento = boletoGerado.DataVencimento.Value;

                                            itemLancamentoFinanceiroRecebimento.Valor = boletoGerado.Valor;

                                            if (boletoGerado.NossoNumero != null)
                                                itemLancamentoFinanceiroRecebimento.NumeroBoletoGerado = boletoGerado.NossoNumero.Value.ToString();

                                            itemLancamentoFinanceiroRecebimento.Situacao = Situacao.CobrancaGerada;
                                            itemLancamentoFinanceiroRecebimento.BoletoFoiGeradoColetivamente = false;

                                            servicoFinanceiro.Modifique(itemLancamentoFinanceiroRecebimento);
                                        }

                                        using (var servicoFinanceiroComBoleto = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeItemFinanceiroRecebidoComBoleto>())
                                        {
                                            servicoFinanceiroComBoleto.Insira(itemLancamentoFinanceiroRecebimento.ID.Value, boletoGerado.ID.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        boletoGerado.StatusBoleto = StatusBoleto.Status.Aberto.ToString();
                        boletoGerado.EhBoletoAvulso = true;
                        servico.Inserir(boletoGerado, BoletoGeraItemFinanceiroDeRecebimento, TipoLacamentoFinanceiroRecebimento.BoletoAvulso);
                        // incrementar o nosso numero e o numero do documento e atualizar no banco.
                        dadosAuxiliares.ProximoNossoNumero = dadosAuxiliares.ProximoNossoNumero + 1;
                        servico.AtualizarProximasInformacoes(dadosAuxiliares);
                    }
                }

            }
            catch (BussinesException ex)
            {
                Logger.GetInstancia().Erro("Erro ao gerar boleto, exceção: ", ex);
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                        UtilidadesWeb.MostraMensagemDeInconsitencia("Erro ao gerar boleto, exceção: " + ex.Message), false);
            }

            //Garante que a tela de contas a receber será atualizada.
            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.AtualizaJanela(String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmContasAReceber.aspx"), "FN_frmContasAReceber_aspx"), false);

            //Garante que a tela de historico de boletos seja atualizada
            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), UtilidadesWeb.AtualizaJanela(String.Concat(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual(), "FN/frmBoletosGerados.aspx"), "FN_frmBoletosGerados_aspx"), false);

        }

        private void MostraBoleto(BoletoBancario bb)
        {
            var html = new StringBuilder();

            html.Append(bb.MontaHtml());

            var html_st = html.ToString();

            //var boletoPathHTML = Path.Combine(Path.GetTempPath(), "Boleto.html");
            var pastaDeDestino = Server.MapPath(Util.URL_IMAGEM_CABECALHO_BOLETO);
            Compartilhados.Util.CrieDiretorio(pastaDeDestino);

            var boletoPathHTML = Server.MapPath(Util.URL_IMAGEM_CABECALHO_BOLETO + "Boleto.html");

            var f = new FileStream(boletoPathHTML, FileMode.Create);

            using (var w = new StreamWriter(f, Encoding.Default))
            {
                w.Write(html_st);
                w.Close();
                f.Close();
                //System.Diagnostics.Process.Start(boletoPathHTML); Exibe o boleto na página
            }

            //var boletoPathPDF = Path.Combine(Path.GetTempPath(), "Boleto.pdf");
            var boletoPathPDF = Server.MapPath(Util.URL_IMAGEM_CABECALHO_BOLETO + "Boleto.pdf");
            var imagePath = GerarImagem(boletoPathHTML);
            var doc2 = new Document(PageSize.A4, 46, 0, 40, 0);

            PdfWriter.GetInstance(doc2, new FileStream(boletoPathPDF, FileMode.Create));
            doc2.Open();

            var gif = iTextSharp.text.Image.GetInstance(imagePath);
            gif.ScaleAbsolute(494.0F, 785.0F);
            doc2.Add(gif);
            doc2.Close();

            //System.Diagnostics.Process.Start(@"C:\Windows\Temp\Boleto.pdf");
            AbraPdf(boletoPathPDF);
        }

        private void AbraPdf(string boletoPathPDF)
        {
            var pastaDeDestinoTemp = Server.MapPath(Util.URL_IMAGEM_CABECALHO_BOLETO + "/temp/");

            if (!Directory.Exists(pastaDeDestinoTemp))
            Directory.CreateDirectory(pastaDeDestinoTemp);

            var caminhoArquivoNovo = Path.Combine(pastaDeDestinoTemp, "Boleto.pdf");

            if (File.Exists(caminhoArquivoNovo))
            {
                File.Delete(caminhoArquivoNovo);
            }

            File.Move(boletoPathPDF, caminhoArquivoNovo);
        }

        private string GerarImagem(string _boletoPathHtml)
        {
            const int width = 680;
            const int height = 1096;
            const int webBrowserWidth = 680;
            const int webBrowserHeight = 1096;
            var bmp = WebsiteThumbnailImageGenerator.GetWebSiteThumbnail(_boletoPathHtml, webBrowserWidth, webBrowserHeight, width, height);
            var boletoPathBMP = Path.Combine(Path.GetTempPath(), "Boleto.bmp");
            bmp.Save(boletoPathBMP);
            return boletoPathBMP;
        }

        //private string LerHtml(string _boletoPathHTML)
        //{
        //    bool fileExists = false;
        //    fileExists = File.Exists(_boletoPathHTML);
        //    string fileContents = " ";

        //    if (fileExists)
        //    {
        //        fileContents = File.ReadAllText(_boletoPathHTML);
        //    }

        //    return fileContents;
        //}

        private bool ExisteErroDePreenchimento()
        {

            if(!string.IsNullOrEmpty(txtFinalidadeBoleto.Text))
            {
                string[] linhasDaIntrucao = txtFinalidadeBoleto.Text.Split('\n');
                IList<string> listaDeLinhas = linhasDaIntrucao.ToList();

                const int quantidadeMaxDeLinhas = 30;

                if (listaDeLinhas.Count > quantidadeMaxDeLinhas)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInformacao("Quantidade de linhas das informações do recibo do sacado, excedeu o tamanho limite de " + quantidadeMaxDeLinhas + " linhas."),
                                                   false);
                    return true;
                }
            }

            if (!string.IsNullOrEmpty(txtInstrucoes.Text))
            {
                string[] linhasDaIntrucao = txtInstrucoes.Text.Split('\n');
                IList<string> listaDeLinhas = linhasDaIntrucao.ToList();

                const int quantidadeMaxDeLinhas = 10;

                if (listaDeLinhas.Count > quantidadeMaxDeLinhas)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInformacao("Quantidade de linhas das instruções do agente financeiro, excedeu o tamanho limite de " + quantidadeMaxDeLinhas + " linhas."),
                                                   false);
                    return true;
                }
            }

            if (string.IsNullOrEmpty(ctrlCedente.CedenteSelecionado.NumeroDaAgencia))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInformacao("O cedente informado não possui uma agência cadastrada."),
                                                   false);
                return true;
            }

            if (string.IsNullOrEmpty(ctrlCedente.CedenteSelecionado.NumeroDaConta))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInformacao("O cedente informado não possui uma conta cadastrada."),
                                                   false);
                return true;
            }

            if (string.IsNullOrEmpty(ctrlCedente.CedenteSelecionado.NumeroDoBanco))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInformacao("O cedente informado não possui um banco cadastrado."),
                                                   false);
                return true;
            }

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

        private void CarregueInstrucoesDoBoleto()
        {
            using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeConfiguracaoGeralFinanceiro>())
            {
                var configuracaoGeral = servico.Obtenha();

                if(configuracaoGeral == null || string.IsNullOrEmpty(configuracaoGeral.InstrucoesDoBoleto))
                    return;

                txtInstrucoes.Text = configuracaoGeral.InstrucoesDoBoleto;
            }
        }
    }
}