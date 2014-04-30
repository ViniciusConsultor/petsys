Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Cedente
    Inherits PapelPessoa
    Implements ICedente

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

    Private _imagemDeCabecalhoDoReciboDoSacado As String
    Public Property ImagemDeCabecalhoDoReciboDoSacado() As String Implements ICedente.ImagemDeCabecalhoDoReciboDoSacado
        Get
            Return _imagemDeCabecalhoDoReciboDoSacado
        End Get
        Set (ByVal value As String)
            _imagemDeCabecalhoDoReciboDoSacado = value
        End Set
    End Property

    Private _tipoDeCarteira As TipoDeCarteira
    Public Property ICedente_TipoDeCarteira() As TipoDeCarteira Implements ICedente.TipoDeCarteira
        Get
            Return _tipoDeCarteira
        End Get
        Set(ByVal value As TipoDeCarteira)
            _tipoDeCarteira = value
        End Set
    End Property

    Private _inicioNossoNumero As Long
    Public Property InicioNossoNumero() As Long Implements ICedente.InicioNossoNumero
        Get
            Return _inicioNossoNumero
        End Get
        Set(ByVal value As Long)
            _inicioNossoNumero = value
        End Set
    End Property

    Private _numeroDaAgencia As String
    Public Property NumeroDaAgencia() As String Implements ICedente.NumeroDaAgencia
        Get
            Return _numeroDaAgencia
        End Get
        Set (ByVal value As String)
            _numeroDaAgencia = value
        End Set
    End Property

    Private _numeroDaConta As String
    Public Property NumeroDaConta() As String Implements ICedente.NumeroDaConta
        Get
            Return _numeroDaConta
        End Get
        Set (ByVal value As String)
            _numeroDaConta = value
        End Set
    End Property

    Private _tipoDaConta As Integer
    Public Property TipoDaConta() As Integer Implements ICedente.TipoDaConta
        Get
            Return _tipoDaConta
        End Get
        Set (ByVal value As Integer)
            _tipoDaConta = value
        End Set
    End Property

    Private _padrao As Boolean
    Public Property Padrao() As Boolean Implements ICedente.Padrao
        Get
            Return _padrao
        End Get
        Set (ByVal value As Boolean)
            _padrao = value
        End Set
    End Property

    Private _numeroDoBanco As String
    Public Property NumeroDoBanco() As String Implements ICedente.NumeroDoBanco
        Get
            Return _numeroDoBanco
        End Get
        Set(ByVal value As String)
            _numeroDoBanco = value
        End Set
    End Property
End Class
