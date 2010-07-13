Imports Compartilhados.Fabricas

Namespace Core.Negocio

    Public Class FabricaDePessoaFisicaLazyLoad

        Public Shared Function Crie(ByVal ID As Long) As IPessoaFisica
            Return FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisicaLazyLoad)(New Object() {ID})
        End Function

    End Class

End Namespace