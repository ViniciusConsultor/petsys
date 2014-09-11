Imports Diary.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class DespachoAgenda
    Inherits Despacho
    Implements IDespachoAgenda

    Public Overrides ReadOnly Property TipoDestino() As TipoDestinoDespacho
        Get
            Return TipoDestinoDespacho.Compromisso
        End Get
    End Property

    Private _Compromisso As ICompromisso
    Public Property Compromisso() As ICompromisso Implements IDespachoAgenda.Compromisso
        Get
            Return _Compromisso
        End Get
        Set(ByVal value As ICompromisso)
            _Compromisso = value
        End Set
    End Property

End Class
