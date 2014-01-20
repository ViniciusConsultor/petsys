Imports Diary.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class DespachoTarefa
    Inherits Despacho
    Implements IDespachoTarefa

    Public Overrides ReadOnly Property TipoDestino() As TipoDestinoDespacho
        Get
            Return TipoDestinoDespacho.Tarefa
        End Get
    End Property

    Private _Tarefa As ITarefa
    Public Property Tarefa() As ITarefa Implements IDespachoTarefa.Tarefa
        Get
            Return _Tarefa
        End Get
        Set(ByVal value As ITarefa)
            _Tarefa = value
        End Set
    End Property
End Class
