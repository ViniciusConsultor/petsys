Namespace Negocio

    Public Interface IMenuComposto
        Inherits IMenuAbstrato
        
        Property Agrupador As IDictionary(Of String, IMenuComposto)
        Function ObtenhaItens() As IList(Of IMenuAbstrato)
        Sub AdicioneItem(ByVal Item As IMenuAbstrato)
        
    End Interface

End Namespace