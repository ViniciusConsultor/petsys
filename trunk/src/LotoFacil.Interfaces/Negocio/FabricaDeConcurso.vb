Imports Compartilhados.Fabricas

Namespace Negocio

    Public Class FabricaDeConcurso

        Public Shared Function CrieObjeto(ByVal Numero As Integer, ByVal Data As Date) As IConcurso
            Dim Parametros() As Object = New Object() {Numero, Data}

            Return FabricaGenerica.GetInstancia.CrieObjeto(Of IConcurso)(Parametros)
        End Function
    End Class

End Namespace