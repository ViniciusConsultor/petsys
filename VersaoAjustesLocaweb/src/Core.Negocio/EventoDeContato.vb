Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class EventoDeContato
    Implements IEventoDeContato

    Private _Data As Date
    Public Property Data As Date Implements IEventoDeContato.Data
        Get
            Return _Data
        End Get
        Set(ByVal value As Date)
            _Data = value
        End Set
    End Property

    Private _Descricao As String
    Public Property Descricao As String Implements IEventoDeContato.Descricao
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

End Class
