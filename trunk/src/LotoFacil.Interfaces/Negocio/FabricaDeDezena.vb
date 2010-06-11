Imports Compartilhados.Fabricas

Namespace Negocio

    Public Class FabricaDeDezena

        Public Shared Function CrieObjeto(ByVal Numero As Byte) As IDezena
            Dim Parametros() As Object = New Object() {Numero}

            Return FabricaGenerica.GetInstancia.CrieObjeto(Of IDezena)(Parametros)
        End Function
    End Class

End Namespace
