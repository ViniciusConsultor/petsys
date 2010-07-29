Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

<Serializable()> _
Public Class Compromisso
    Implements ICompromisso

    Private _Assunto As String
    Public Property Assunto() As String Implements ICompromisso.Assunto
        Get
            Return _Assunto
        End Get
        Set(ByVal value As String)
            _Assunto = value
        End Set
    End Property

    Private _Descricao As String
    Public Property Descricao() As String Implements ICompromisso.Descricao
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Private _Fim As Date
    Public Property Fim() As Date Implements ICompromisso.Fim
        Get
            Return _Fim
        End Get
        Set(ByVal value As Date)
            _Fim = value
        End Set
    End Property

    Private _Inicio As Date
    Public Property Inicio() As Date Implements ICompromisso.Inicio
        Get
            Return _Inicio
        End Get
        Set(ByVal value As Date)
            _Inicio = value
        End Set
    End Property

    Private _Local As String
    Public Property Local() As String Implements ICompromisso.Local
        Get
            Return _Local
        End Get
        Set(ByVal value As String)
            _Local = value
        End Set
    End Property

    Private _Proprietario As IPessoaFisica
    Public Property Proprietario() As IPessoaFisica Implements ICompromisso.Proprietario
        Get
            Return _Proprietario
        End Get
        Set(ByVal value As IPessoaFisica)
            _Proprietario = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements ICompromisso.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Sub EstaConsistente() Implements ICompromisso.EstaConsistente
        VerificaSeDataDeIncioEhMaiorQueDataAtual()
        VerificaSeDataFinalEhMenorQueDataDeInicio()
    End Sub

    Private Sub VerificaSeDataDeIncioEhMaiorQueDataAtual()
        If CLng(Me.Inicio.ToString("yyyyMMddHHmm")) <= CLng(Now.ToString("yyyyMMddHHmm")) Then
            Throw New BussinesException("Data de início deve ser maior igual a data atual.")
        End If
    End Sub

    Private Sub VerificaSeDataFinalEhMenorQueDataDeInicio()
        If CLng(Me.Inicio.ToString("yyyyMMddHHmm")) > CLng(Me.Fim.ToString("yyyyMMddHHmm")) Then
            Throw New BussinesException("Data de início deve ser menor igual a data final.")
        End If
    End Sub

    Private _Status As StatusDoCompromisso
    Public Property Status() As StatusDoCompromisso Implements ICompromisso.Status
        Get
            Return _Status
        End Get
        Set(ByVal value As StatusDoCompromisso)
            _Status = value
        End Set
    End Property

End Class