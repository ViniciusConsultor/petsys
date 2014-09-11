Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Runtime.CompilerServices
Imports System.Runtime.Remoting.Messaging
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Namespace Repositorios

    Public Class RepositorioDePessoa
        
        Private Const NOME_CALLCONTEXT As String = "RepositorioDePessoa"
        Private cache As IDictionary(Of Long, IPessoa)

        Private Sub New()
            cache = New Dictionary(Of Long, IPessoa)()
        End Sub

        <MethodImpl(MethodImplOptions.Synchronized)> _
        Public Shared Function ObtenhaInstancia() As RepositorioDePessoa
            Dim instancia = CType(ChamadaPorContexto.GetData(NOME_CALLCONTEXT), RepositorioDePessoa)

            If instancia Is Nothing Then
                instancia = New RepositorioDePessoa()
                ChamadaPorContexto.SetData(NOME_CALLCONTEXT, instancia)
            End If

            Return instancia
        End Function

        Public Function ObtenhaPessoa(ByVal id As Long, ByVal tipo As TipoDePessoa) As IPessoa
            If cache.ContainsKey(id) Then Return cache(id)

            Dim pessoa As IPessoa

            If tipo.Equals(TipoDePessoa.Fisica) Then
                pessoa = ObtenhaPessoaFisica(id)
            Else
                pessoa = ObtenhaPessoaJuridica(id)
            End If

            cache.Add(id, pessoa)

            Return pessoa

        End Function

        Private Function ObtenhaPessoaFisica(ByVal id As Long) As IPessoa
            Using Servico As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
                Return Servico.ObtenhaPessoa(id)
            End Using
        End Function

        Private Function ObtenhaPessoaJuridica(ByVal id As Long) As IPessoa
            Using Servico As IServicoDePessoaJuridica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaJuridica)()
                Return Servico.ObtenhaPessoa(id)
            End Using
        End Function

    End Class

End Namespace