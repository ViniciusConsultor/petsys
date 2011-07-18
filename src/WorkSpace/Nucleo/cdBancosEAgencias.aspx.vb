Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Telerik.Web.UI
Imports Compartilhados

Partial Public Class cdBancosEAgencias
    Inherits SuperPagina

    Private CHAVE_ESTADO_CD_BANCO As String = "CHAVE_ESTADO_CD_BANCO"
    Private CHAVE_ESTADO_CD_AGENCIA As String = "CHAVE_ESTADO_CD_AGENCIA"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler ctrlPessoa1.PessoaFoiSelecionada, AddressOf ObtenhaBanco
        AddHandler ctrlPessoa2.PessoaFoiSelecionada, AddressOf ObtenhaAgencia

        If Not IsPostBack Then
            ExibaTelaInicialBanco()
        End If
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return Me.rtbToolBarBancos
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.NCL.009"
    End Function

    Private Enum Estado As Byte
        Inicial = 1
        Novo
        Consulta
        Modifica
        Remove
    End Enum

    Private Sub ExibaTelaInicialBanco()
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoBanco, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoBanco, Control), False)
        ctrlPessoa1.Inicializa()
        ctrlPessoa1.BotaoDetalharEhVisivel = False
        ctrlPessoa1.BotaoNovoEhVisivel = True
        'Com isso garantimos que banco será apenas pessoa juridica
        ctrlPessoa1.OpcaoTipoDaPessoaEhVisivel = False
        ctrlPessoa1.SetaTipoDePessoaPadrao(TipoDePessoa.Juridica)

        pnlAgencias.Visible = False
        ViewState(CHAVE_ESTADO_CD_BANCO) = Estado.Inicial
    End Sub

    Private Sub ExibaTelaInicialAgencia()
        pnlAgencias.Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.LimparComponente(CType(pnlDadosDoAgencia, Control))
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAgencia, Control), False)
        ctrlPessoa2.Inicializa()
        ctrlPessoa2.BotaoDetalharEhVisivel = False
        ctrlPessoa2.BotaoNovoEhVisivel = True
        'Com isso garantimos que banco será apenas pessoa juridica para agencias
        ctrlPessoa2.OpcaoTipoDaPessoaEhVisivel = False
        ctrlPessoa2.SetaTipoDePessoaPadrao(TipoDePessoa.Juridica)
        ViewState(CHAVE_ESTADO_CD_AGENCIA) = Estado.Inicial
    End Sub

    Protected Sub btnNovo_Click()
        ExibaTelaNovoBanco()
    End Sub

    Protected Sub btnNovaAgencia_Click()
        ExibaTelaNovaAgencia()
    End Sub

    Private Sub ExibaTelaNovoBanco()
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBarBancos.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_BANCO) = Estado.Novo
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoBanco, Control), True)
    End Sub

    Private Sub ExibaTelaNovaAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_AGENCIA) = Estado.Novo
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAgencia, Control), True)
    End Sub

    Private Sub ExibaTelaModificarBanco()
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBarBancos.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_BANCO) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoBanco, Control), True)
    End Sub

    Private Sub ExibaTelaModificarAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        ViewState(CHAVE_ESTADO_CD_AGENCIA) = Estado.Modifica
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAgencia, Control), True)
    End Sub

    Private Sub ExibaTelaExcluirBanco()
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_BANCO) = Estado.Remove
    End Sub

    Private Sub ExibaTelaExcluirAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = True
        ViewState(CHAVE_ESTADO_CD_AGENCIA) = Estado.Remove
    End Sub

    Private Sub ExibaTelaConsultarBanco()
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBarBancos.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarBancos.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarBancos.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoBanco, Control), False)
        ExibaTelaInicialAgencia()
    End Sub

    Private Sub ExibaTelaConsultarAgencia()
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnModificar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnExcluir"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSalvar"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnCancelar"), RadToolBarButton).Visible = True
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnSim"), RadToolBarButton).Visible = False
        CType(rtbToolBarAgencias.FindButtonByCommandName("btnNao"), RadToolBarButton).Visible = False
        UtilidadesWeb.HabilitaComponentes(CType(pnlDadosDoAgencia, Control), False)
    End Sub

    Protected Sub btnCancela_Click()
        ExibaTelaInicialBanco()
    End Sub

    Protected Sub btnCancelaAgencia_Click()
        ExibaTelaInicialAgencia()
    End Sub

    Private Function MontaObjetoBanco() As IBanco
        Dim Banco As IBanco
        Dim Pessoa As IPessoa

        Pessoa = ctrlPessoa1.PessoaSelecionada
        Banco = FabricaGenerica.GetInstancia.CrieObjeto(Of IBanco)((New Object() {Pessoa}))
        Banco.Numero = CInt(txtNumeroDoBanco.Value)

        Return Banco
    End Function

    Private Function MontaObjetoAgencia() As IAgencia
        Dim Banco As IBanco
        Dim Agencia As IAgencia
        Dim Pessoa As IPessoa

        Pessoa = ctrlPessoa2.PessoaSelecionada
        Banco = MontaObjetoBanco()
        Agencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IAgencia)((New Object() {Pessoa}))
        Agencia.Numero = txtNumeroDaAgencia.Text
        Agencia.Banco = Banco

        Return Agencia
    End Function

    Private Function ConsisteDadosDoBanco() As String
        If Not txtNumeroDoBanco.Value.HasValue Then Return "O número do banco deve ser informado."
        Return Nothing
    End Function

    Private Sub btnSalva_Click()
        Dim Mensagem As String

        Dim Inconsistencia As String

        Inconsistencia = ConsisteDadosDoBanco()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If

        Dim Banco As IBanco = MontaObjetoBanco()

        Try
            Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
                If CByte(ViewState(CHAVE_ESTADO_CD_BANCO)) = Estado.Novo Then
                    Servico.InsiraBanco(Banco)
                    Mensagem = "Banco cadastrado com sucesso."
                Else
                    Servico.ModifiqueBanco(Banco)
                    Mensagem = "Banco modificado com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicialBanco()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Function ConsisteDadosDaAgencia() As String
        If String.IsNullOrEmpty(txtNumeroDaAgencia.Text) Then Return "O número da agência deve ser informado."
        Return Nothing
    End Function

    Private Sub btnSalvaAgencia_Click()
        Dim Mensagem As String
        Dim Inconsistencia As String

        Inconsistencia = ConsisteDadosDaAgencia()

        If Not String.IsNullOrEmpty(Inconsistencia) Then
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(Inconsistencia), False)
            Exit Sub
        End If
        Dim Agencia As IAgencia = MontaObjetoAgencia()

        Try
            Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
                If CByte(ViewState(CHAVE_ESTADO_CD_AGENCIA)) = Estado.Novo Then
                    Servico.InsiraAgencia(Agencia)
                    Mensagem = "Agência cadastrada com sucesso."
                Else
                    Servico.ModifiqueAgencia(Agencia)
                    Mensagem = "Agência modificada com sucesso."
                End If

            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao(Mensagem), False)
            ExibaTelaInicialAgencia()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub btnModificar_Click()
        ExibaTelaModificarBanco()
    End Sub

    Private Sub btnModificarAgencia_Click()
        ExibaTelaModificarAgencia()
    End Sub

    Private Sub btnExclui_Click()
        ExibaTelaExcluirBanco()
    End Sub

    Private Sub btnExcluiAgencia_Click()
        ExibaTelaExcluirAgencia()
    End Sub

    Private Sub btnNao_Click()
        Me.ExibaTelaInicialBanco()
    End Sub

    Private Sub btnNaoAgencia_Click()
        Me.ExibaTelaInicialAgencia()
    End Sub

    Private Sub btnSim_Click()
        Try
            Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
                Servico.RemovaBanco(ctrlPessoa1.PessoaSelecionada.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Banco excluido com sucesso."), False)
            ExibaTelaInicialBanco()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub btnSimAgencia_Click()
        Try
            Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
                Servico.RemovaAgencia(ctrlPessoa2.PessoaSelecionada.ID.Value)
            End Using

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), New Guid().ToString, UtilidadesWeb.MostraMensagemDeInformacao("Agência excluida com sucesso."), False)
            ExibaTelaInicialAgencia()

        Catch ex As BussinesException
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, New Guid().ToString, UtilidadesWeb.MostraMensagemDeInconsitencia(ex.Message), False)
        End Try
    End Sub

    Private Sub rtbToolBarBancos_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBarBancos.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovo_Click()
            Case "btnModificar"
                Call btnModificar_Click()
            Case "btnExcluir"
                Call btnExclui_Click()
            Case "btnSalvar"
                Call btnSalva_Click()
            Case "btnCancelar"
                Call btnCancela_Click()
            Case "btnSim"
                Call btnSim_Click()
            Case "btnNao"
                Call btnNao_Click()
        End Select
    End Sub

    Private Sub ObtenhaBanco(ByVal Pessoa As IPessoa)
        Dim Banco As IBanco

        ctrlPessoa1.BotaoDetalharEhVisivel = True

        Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
            Banco = Servico.ObtenhaBanco(Pessoa)
        End Using

        If Banco Is Nothing Then
            CType(rtbToolBarBancos.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
            Exit Sub
        End If

        MostreBanco(Banco)
        ExibaTelaConsultarBanco()
    End Sub

    Private Sub ObtenhaAgencia(ByVal Pessoa As IPessoa)
        Dim Agencia As IAgencia

        ctrlPessoa1.BotaoDetalharEhVisivel = True

        Using Servico As IServicoDeBancosEAgencias = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeBancosEAgencias)()
            Agencia = Servico.ObtenhaAgencia(ctrlPessoa1.PessoaSelecionada.ID.Value, Pessoa.ID.Value)
        End Using

        If Agencia Is Nothing Then
            CType(rtbToolBarAgencias.FindButtonByCommandName("btnNovo"), RadToolBarButton).Visible = True
            Exit Sub
        End If

        MostreAgencia(Agencia)
        ExibaTelaConsultarAgencia()
    End Sub

    Private Sub MostreBanco(ByVal Banco As IBanco)
        txtNumeroDoBanco.Value = Banco.Numero
    End Sub

    Private Sub MostreAgencia(ByVal Agencia As IAgencia)
        txtNumeroDaAgencia.Text = Agencia.Numero
    End Sub

    Private Sub rtbToolBarAgencias_ButtonClick(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadToolBarEventArgs) Handles rtbToolBarAgencias.ButtonClick
        Select Case CType(e.Item, RadToolBarButton).CommandName
            Case "btnNovo"
                Call btnNovaAgencia_Click()
            Case "btnModificar"
                Call btnModificarAgencia_Click()
            Case "btnExcluir"
                Call btnExcluiAgencia_Click()
            Case "btnSalvar"
                Call btnSalvaAgencia_Click()
            Case "btnCancelar"
                Call btnCancelaAgencia_Click()
            Case "btnSim"
                Call btnSimAgencia_Click()
            Case "btnNao"
                Call btnNaoAgencia_Click()
        End Select
    End Sub

End Class