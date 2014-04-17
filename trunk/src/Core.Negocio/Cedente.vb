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

End Class
