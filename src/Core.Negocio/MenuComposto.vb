Imports Core.Interfaces.Negocio

<Serializable()> _
Public Class MenuComposto
    Inherits MenuAbstrato
    Implements IMenuComposto

    Private _Itens As IList(Of IMenuAbstrato)

    Public Sub New()
        _Itens = New List(Of IMenuAbstrato)
    End Sub

    Public Sub AdicioneItem(ByVal Item As IMenuAbstrato) Implements IMenuComposto.AdicioneItem
        If Not _Itens.Contains(Item) Then _Itens.Add(Item)
    End Sub

    Public Function ObtenhaItens() As IList(Of IMenuAbstrato) Implements IMenuComposto.ObtenhaItens
        Return _Itens
    End Function

End Class