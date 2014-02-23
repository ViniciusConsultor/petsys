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
End Class
