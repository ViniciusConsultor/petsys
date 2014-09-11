Imports Diary.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class DespachoLembrete
    Inherits Despacho
    Implements IDespachoLembrete

    Private _Lembrete As ILembrete
    Public Property Lembrete() As ILembrete Implements IDespachoLembrete.Lembrete
        Get
            Return _Lembrete
        End Get
        Set(ByVal value As ILembrete)
            _Lembrete = value
        End Set
    End Property

    Public Overrides ReadOnly Property TipoDestino() As TipoDestinoDespacho
        Get
            Return TipoDestinoDespacho.Lembrete
        End Get
    End Property

End Class
