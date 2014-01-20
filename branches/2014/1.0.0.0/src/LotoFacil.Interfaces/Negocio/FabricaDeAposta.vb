Imports Compartilhados.Fabricas

Namespace Negocio

    Public Class FabricaDeAposta

        Public Shared Function CrieObjeto(ByVal Concurso As IConcurso) As IAposta
            Dim Parametros() As Object = New Object() {Concurso}

            Return FabricaGenerica.GetInstancia.CrieObjeto(Of IAposta)(Parametros)
        End Function

        Public Shared Function CrieObjeto(ByVal Concurso As IConcurso, _
                                          ByVal Jogos As IList(Of IJogo)) As IAposta
            Dim Parametros() As Object = New Object() {Concurso, Jogos}

            Return FabricaGenerica.GetInstancia.CrieObjeto(Of IAposta)(Parametros)
        End Function

    End Class

End Namespace