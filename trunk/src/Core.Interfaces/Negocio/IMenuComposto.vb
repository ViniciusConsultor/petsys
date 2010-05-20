Namespace Negocio

    Public Interface IMenuComposto
        Inherits IMenuAbstrato

        Function ObtenhaItens() As IList(Of IMenuAbstrato)
        Sub AdicioneItem(ByVal Item As IMenuAbstrato)

    End Interface

End Namespace