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
using Compartilhados.Interfaces.FN.Negocio;
using Compartilhados.Interfaces.FN.Servicos;
using FN.Interfaces.Negocio;
using FN.Interfaces.Servicos;
using Telerik.Web.UI;

namespace FN.Client.FN
{
    public partial class frmBoletoAvulso : SuperPagina
    {
        //Todo verificar carregamento do tipo de carteira
        //public const string carteira = "SR";
        public const string CHAVE_CEDENTE_BOLETOGERADO = "CHAVE_CEDENTE_BOLETOGERADO";
       
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
                    ExibaTelaInicial();
                else
                    if (itensFinanceiros == null)
                        ExibaTelaBoletoGerado(id.Value);
                    else
                        ExibaItensFinanceiros(itensFinanceiros);

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

                ViewState["CHAVE_ITEM_FINANCEIRO_SELECIONADO"] = itemLancamentoFinanceiro;
            }

            //Aqui para cada id invoca o servico para obter o item financeiro de recebimento
            //depois pegar os dados e formar um boleto
            //OBS: no momento da geracao do boleto passar false como o parametro se é para gerar um item de recebimento.
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

                    if (boleto.Cedente != null)
                    {
                        this.ctrlCedente.CedenteSelecionado = boleto.Cedente;
                        PreenchaDadosDoCedente(boleto.Cedente);
                    }

                    this.ctrlCliente.ClienteSelecionado = boleto.Cliente;
                    PreenchaDadosDoClienteSelecionado(boleto.Cliente);

                    PreenchaInformacoesDoBoleto(boleto);

                    DesabilitaCamposParaEdicao();
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
            txtValor.Text = itemLancamentoFinanceiroRecebimento.Valor.ToString();

            ViewState["CHAVE_ITEM_FINANCEIRO_SELECIONADO"] = null;
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
                var valorBoleto = txtValor.Text;

                IBoletosGeradosAux dadosAuxiliares = null;

                // Busca dados auxiliares no banco, se for a primeira vez, efetua o insert

                long cedenteNossoNumeroBoleto;

                if (ViewState["CHAVE_CEDENTE_BOLETOGERADO"] != null)
                {
                    // Numero do documento - numero de controle interno, não afeta o calculo da linha digitavel e nem o codigo de barras
                    // mais pode ser útil, exemplo: quando o cliente ligar, vc pode consultar por este número e ver 
                    // se já foi efetuado o pagamento

                    // cedente

                    BoletoGerado = (IBoletosGerados)ViewState["CHAVE_CEDENTE_BOLETOGERADO"];

                    cedenteNossoNumeroBoleto = BoletoGerado.NossoNumero.Value; // o final do nosso número é incrementado ao final

                }
                else
                {
                    using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                    {
                        dadosAuxiliares = servico.obtenhaProximasInformacoesParaGeracaoDoBoleto();

                        if (!dadosAuxiliares.ID.HasValue)
                        {
                            var boletosGeradosAux = FabricaGenerica.GetInstancia().CrieObjeto<IBoletosGeradosAux>();

                            boletosGeradosAux.ID = GeradorDeID.getInstancia().getProximoID();

                            // verificar se o codigo do banco é da caixa e acrescentar o 82
                            if(ctrlCedente.CedenteSelecionado != null)
                            {
                                if (ctrlCedente.CedenteSelecionado.InicioNossoNumero > 0)
                                {
                                    boletosGeradosAux.ProximoNossoNumero = Convert.ToInt64("82" + ctrlCedente.CedenteSelecionado.InicioNossoNumero);
                                }
                                else
                                {
                                    boletosGeradosAux.ProximoNossoNumero = 8210001001;
                                }
                            }

                            servico.InserirPrimeiraVez(boletosGeradosAux);

                            dadosAuxiliares = servico.obtenhaProximasInformacoesParaGeracaoDoBoleto();
                        }
                    }

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

                    if (cedentePessoa.DadoBancario != null)
                    {
                        codigoDoBanco = Convert.ToInt32(cedentePessoa.DadoBancario.Agencia.Banco.ID);
                        cedenteAgencia = cedentePessoa.DadoBancario.Agencia.Numero;
                        cedenteConta = cedentePessoa.DadoBancario.Conta.Numero;
                        cedenteOperacaoConta = cedentePessoa.DadoBancario.Conta.Tipo.Value.ToString("000");

                        if (!string.IsNullOrEmpty(cedenteConta))
                            cedenteDigitoConta = cedenteConta.Substring(cedenteConta.Length - 1, 1);
                    }

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
                boletoGerado.Cedente = ctrlCedente.CedenteSelecionado;

                boletoGerado.Cliente = ctrlCliente.ClienteSelecionado;

                boletoGerado.DataGeracao = boleto.DataProcessamento;
                boletoGerado.DataVencimento = txtVencimento.SelectedDate.Value;
                boletoGerado.NossoNumero = cedenteNossoNumeroBoleto;
                boletoGerado.NumeroBoleto = txtNumeroDoBoleto.Text;
                boletoGerado.Observacao = sacadoObservacao;
                boletoGerado.Valor = Convert.ToDouble(valorBoleto);
                boletoGerado.Instrucoes = txtInstrucoes.Text;

                using (var servico = FabricaGenerica.GetInstancia().CrieObjeto<IServicoDeBoleto>())
                {

                    if (BoletoGerado != null)
                    {
                        boletoGerado.ID = BoletoGerado.ID.Value;
                        servico.AtualizarBoletoGerado(boletoGerado);

                    }
                    else
                    {
                        servico.Inserir(boletoGerado, BoletoGeraItemFinanceiroDeRecebimento);
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
        }

        private bool ExisteErroDePreenchimento()
        {

            if(!string.IsNullOrEmpty(txtFinalidadeBoleto.Text))
            {
                string[] linhasDaIntrucao = txtFinalidadeBoleto.Text.Split('\n');
                IList<string> listaDeLinhas = linhasDaIntrucao.ToList();

                if(listaDeLinhas.Count > 10)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInformacao("Quantidade de linhas das informações do recibo do sacado, excedeu o tamanho limite de 15 linhas."),
                                                   false);
                    return true;
                }
            }

            if (ctrlCedente.CedenteSelecionado.Pessoa.DadoBancario == null)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(),
                                                   UtilidadesWeb.MostraMensagemDeInformacao("O cedente informado não possui um banco e uma agência cadastrado."),
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
    }
}