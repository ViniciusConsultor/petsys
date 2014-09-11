Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Runtime.CompilerServices
Imports System.Runtime.Remoting.Messaging
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Namespace Repositorios

    Public Class RepositorioDePais

        Private Const NOME_CALLCONTEXT As String = "RepositorioDePais"
        Private cache As IDictionary(Of Long, IPais)

        Private Sub New()
            cache = New Dictionary(Of Long, IPais)()
        End Sub

        <MethodImpl(MethodImplOptions.Synchronized)> _
        Public Shared Function ObtenhaInstancia() As RepositorioDePais
            Dim instancia = CType(ChamadaPorContexto.GetData(NOME_CALLCONTEXT), RepositorioDePais)

            If instancia Is Nothing Then
                instancia = New RepositorioDePais()
                ChamadaPorContexto.SetData(NOME_CALLCONTEXT, instancia)
            End If

            Return instancia
        End Function

        Public Function ObtenhaPais(ByVal id As Long) As IPais
            If cache.ContainsKey(id) Then Return cache(id)

            Using Servico As IServicoDePais = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePais)()
                Dim pais = Servico.ObtenhaPais(id)

                cache.Add(id, pais)
                Return pais
            End Using
        End Function

    End Class

End Namespace