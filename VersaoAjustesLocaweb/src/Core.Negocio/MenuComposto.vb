Imports Core.Interfaces.Negocio

<Serializable()> _
Public Class MenuComposto
    Inherits MenuAbstrato
    Implements IMenuComposto

    Private _Itens As List(Of IMenuAbstrato)
    Private _Agrupador As IDictionary(Of String, IMenuComposto)

    Public Sub New()
        _Itens = New List(Of IMenuAbstrato)
        _Agrupador = New Dictionary(Of String, IMenuComposto)()
    End Sub

    Public Sub AdicioneItem(ByVal Item As IMenuAbstrato) Implements IMenuComposto.AdicioneItem
        If Not _Itens.Contains(Item) Then _Itens.Add(Item)
    End Sub

    Public Property Agrupador() As IDictionary(Of String,IMenuComposto) Implements IMenuComposto.Agrupador
        Get
            Return _Agrupador
        End Get
        Set (ByVal value As IDictionary(Of String,IMenuComposto))
            _Agrupador = value
        End Set
    End Property

    Public Function ObtenhaItens() As IList(Of IMenuAbstrato) Implements IMenuComposto.ObtenhaItens
        _Itens.Sort(New ComparadorDeMenu())

        Return _Itens
    End Function

    Private Class ComparadorDeMenu
        Implements IComparer(Of IMenuAbstrato)

        Public Function Compare(ByVal x As IMenuAbstrato, ByVal y As IMenuAbstrato) As Integer Implements IComparer(Of IMenuAbstrato).Compare
            If x Is Nothing Then
                If y Is Nothing Then
                    Return 0
                End If

                Return -1
            End If

            If y Is Nothing Then
                Return 1
            End If

            Return String.Compare(x.Nome, y.Nome)
        End Function
    End Class

End Class