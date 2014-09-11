Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Fabricas

Namespace Core.Negocio.LazyLoad

    Public Class FabricaDeObjetoLazyLoad

        Public Shared Function CrieObjetoLazyLoad(Of T)(ByVal ID As Long) As T
            Return FabricaGenerica.GetInstancia.CrieObjeto(Of T)(New Object() {ID})
        End Function

    End Class

End Namespace