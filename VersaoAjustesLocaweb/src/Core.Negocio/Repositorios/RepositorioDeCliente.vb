Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Runtime.Remoting.Messaging
Imports System.Runtime.CompilerServices
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Namespace Repositorios

    Public Class RepositorioDeCliente
        
        Private Const NOME_CALLCONTEXT As String = "RepositorioDeCliente"
        Private cache As IDictionary(Of Long, ICliente)

        Private Sub New()
            cache = New Dictionary(Of Long, ICliente)()
        End Sub

        <MethodImpl(MethodImplOptions.Synchronized)> _
        Public Shared Function ObtenhaInstancia() As RepositorioDeCliente
            Dim instancia = CType(CallContext.GetData(NOME_CALLCONTEXT), RepositorioDeCliente)

            If instancia Is Nothing Then
                instancia = New RepositorioDeCliente()
                CallContext.SetData(NOME_CALLCONTEXT, instancia)
            End If

            Return instancia
        End Function

        Public Function Obtenha(ByVal id As Long) As ICliente 
            If cache.ContainsKey(id) Then Return cache(id)

            Using Servico As IServicoDeCliente = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeCliente)()
                Dim cliente = Servico.Obtenha(id)

                cache.Add(id, cliente)
                Return cliente
            End Using
        End Function

    End Class

End Namespace