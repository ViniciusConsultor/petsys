Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public MustInherit Class Documento
    Implements IDocumento

    Private _Numero As String

    Protected Sub New(ByVal Numero As String)
        _Numero = Numero
    End Sub

    Public MustOverride Function EhValido() As Boolean Implements IDocumento.EhValido

    Public Property Numero() As String Implements IDocumento.Numero
        Get
            Return _Numero
        End Get
        Set(ByVal value As String)
            _Numero = value
        End Set
    End Property

    Public MustOverride ReadOnly Property Tipo() As TipoDeDocumento Implements IDocumento.Tipo
    Public MustOverride Overrides Function ToString() As String Implements IDocumento.ToString

End Class