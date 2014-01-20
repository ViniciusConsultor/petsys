Imports Compartilhados.Fabricas

Namespace Negocio

    Public Class FabricaDeJogo

        Public Shared Function CrieObjeto() As IJogo

            Return FabricaGenerica.GetInstancia.CrieObjeto(Of IJogo)()
        End Function

        Public Shared Function CrieObjeto(ByVal Dezenas As IList(Of IDezena)) As IJogo
            Dim Parametros() As Object = New Object() {Dezenas}

            Return FabricaGenerica.GetInstancia.CrieObjeto(Of IJogo)(Parametros)
        End Function

    End Class

End Namespace