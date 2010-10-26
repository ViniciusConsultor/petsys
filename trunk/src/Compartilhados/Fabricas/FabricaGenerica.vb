Imports System.Reflection
Imports System.Runtime.Remoting.Activation
Imports System.Runtime.Remoting
Imports System.Net.Sockets

Namespace Fabricas

    Public Class FabricaGenerica

        Private Shared DicionarioDeAssemblyTypes As IDictionary(Of String, Type())
        Private Shared InstanciaSolitaria As FabricaGenerica
        Private Shared ObjetoLock As New Object
        Private Shared TipoDeDistribuicao As String

        Private Sub New()
            If DicionarioDeAssemblyTypes Is Nothing Then
                DicionarioDeAssemblyTypes = New Dictionary(Of String, Type())
            End If

            TipoDeDistribuicao = Util.ObtenhaTipoDeDistribuicao
        End Sub

        Public Shared Function GetInstancia() As FabricaGenerica

            If InstanciaSolitaria Is Nothing Then
                SyncLock ObjetoLock
                    If InstanciaSolitaria Is Nothing Then
                        InstanciaSolitaria = New FabricaGenerica
                    End If
                End SyncLock
            End If

            Return InstanciaSolitaria
        End Function

        Private Sub CarregaAssembly(ByVal NomeDoAssembly As String)
            If Not DicionarioDeAssemblyTypes.ContainsKey(NomeDoAssembly) Then
                Dim Asse As Assembly

                Asse = Assembly.LoadWithPartialName(NomeDoAssembly)

                If Asse Is Nothing Then Throw New DLLNaoEncontradaException("A DLL " & NomeDoAssembly & " não instada")
                DicionarioDeAssemblyTypes.Add(NomeDoAssembly, Asse.GetTypes)
            End If
        End Sub

        Public Function CrieObjeto(ByVal FullName As String, _
                                   ByVal NomeDoTipo As String) As Object
            Dim Instancia As Object
            Dim NomeDoAssembly As String
            Dim NomeTipoConcreto As String

            NomeDoAssembly = ObtenhaNomeDoAssembly(FullName)
            NomeTipoConcreto = ObtenhaNomeTipoConcreto(NomeDoTipo)

            If NomeDoAssembly.Contains("Servico") Then
                Instancia = CriaInstanciaDeServico(NomeDoAssembly, NomeTipoConcreto)
            Else
                CarregaAssembly(NomeDoAssembly)
                Instancia = CriaInstancia(NomeDoAssembly, NomeTipoConcreto)
            End If

            Return Instancia
        End Function

        Public Function CrieObjeto(Of T)() As T
            Dim Instancia As Object = Nothing
            Dim NomeDoAssembly As String
            Dim NomeTipoConcreto As String

            If GetType(T).FullName.Contains("Servico") Then
                Instancia = CriaInstanciaDeServico(GetType(T).FullName, GetType(T).Name)
            Else
                NomeDoAssembly = ObtenhaNomeDoAssembly(GetType(T).FullName)
                NomeTipoConcreto = ObtenhaNomeTipoConcreto(GetType(T).Name)

                CarregaAssembly(NomeDoAssembly)
                Instancia = CriaInstancia(NomeDoAssembly, NomeTipoConcreto)
            End If

            Return CType(Instancia, T)
        End Function

        Public Function CrieObjeto(Of T)(ByVal Parametros() As Object) As T
            Dim Instancia As Object
            Dim NomeDoAssembly As String
            Dim NomeTipoConcreto As String

            NomeDoAssembly = ObtenhaNomeDoAssembly(GetType(T).FullName)
            NomeTipoConcreto = ObtenhaNomeTipoConcreto(GetType(T).Name)

            If NomeDoAssembly.Contains("Servico") Then
                Instancia = CriaInstanciaDeServico(NomeDoAssembly, NomeTipoConcreto)
            Else
                CarregaAssembly(NomeDoAssembly)
                Instancia = CriaInstancia(NomeDoAssembly, NomeTipoConcreto, Parametros)
            End If

            Return CType(Instancia, T)
        End Function

        Private Function CriaInstanciaDeServico(ByVal NomeDoAssembly As String, ByVal NomeDoTipoConcreto As String) As Object
            Dim Credencial As ICredencial
            Dim Instancia As Object = Nothing

            Credencial = Util.ConstruaCredencial

            If TipoDeDistribuicao.Equals("Remoting") Then
                Instancia = Activator.GetObject(ObtenhaTipoParaInstanciacao(NomeDoAssembly, NomeDoTipoConcreto), "tcp://localhost:1235/" & NomeDoTipoConcreto)
                CType(Instancia, IServicoRemoto).SetaCredencial(Credencial)
            ElseIf TipoDeDistribuicao.Equals("Local") Then
                Dim Parametro As Object() = New Object() {Credencial}

                Instancia = Activator.CreateInstance(ObtenhaTipoParaInstanciacao(NomeDoAssembly, NomeDoTipoConcreto), Parametro)
            Else
                Throw New ApplicationException("O Tipo de Distribuição configurado é desconhecido.")
            End If

            Return Instancia
        End Function

        Private Function CriaInstancia(ByVal NomeDoAssembly As String, _
                                       ByVal NomeTipoConcreto As String, _
                                       ByVal Parametros() As Object) As Object
            Return Activator.CreateInstance(ObtenhaTipoParaInstanciacao(NomeDoAssembly, NomeTipoConcreto), Parametros)
        End Function

        Private Function CriaInstancia(ByVal NomeDoAssembly As String, _
                                       ByVal NomeTipoConcreto As String) As Object
            Return Activator.CreateInstance(ObtenhaTipoParaInstanciacao(NomeDoAssembly, NomeTipoConcreto))
        End Function

        Private Function ObtenhaTipoParaInstanciacao(ByVal NomeDoAssembly As String, _
                                                     ByVal NomeDoTipoConcreto As String) As Type
            Dim TipoDaInstanciaFutura As Type = Nothing
            Dim TiposDoAssembly As IList(Of Type)

            TiposDoAssembly = DicionarioDeAssemblyTypes.Item(NomeDoAssembly)

            For Each TipoDoAssembly As Type In TiposDoAssembly
                If TipoDoAssembly.Name.Equals(NomeDoTipoConcreto, StringComparison.InvariantCultureIgnoreCase) Then
                    TipoDaInstanciaFutura = TipoDoAssembly
                    Exit For
                End If
            Next

            Return TipoDaInstanciaFutura
        End Function

        Private Function ObtenhaNomeDoAssembly(ByVal TipoDaInterface As String) As String
            Dim NomeDoAssemblyEmPartes() As String
            Dim NomeDoAssembly As String

            NomeDoAssemblyEmPartes = TipoDaInterface.Split(New Char() {"."c})

            If TipoDaInterface.Contains("Servico") Then

            End If

            If TipoDaInterface.StartsWith("Compartilhados.Interfaces") Then
                NomeDoAssembly = String.Concat(NomeDoAssemblyEmPartes(2), ".", NomeDoAssemblyEmPartes(3))
            Else
                NomeDoAssembly = String.Concat(NomeDoAssemblyEmPartes(0), ".", NomeDoAssemblyEmPartes(2))
            End If

            'If NomeDoAssembly.Contains("Servico") Then
            '    NomeDoAssembly &= String.Concat(".", TipoDeDistribuicao)
            'End If

            Return NomeDoAssembly
        End Function

        Private Function ObtenhaNomeTipoConcreto(ByVal TipoDaInterface As String) As String
            Dim NomeDoTipoConcreto As String

            NomeDoTipoConcreto = TipoDaInterface.Substring(1)

            'If NomeDoTipoConcreto.StartsWith("Servico") Then
            '    NomeDoTipoConcreto &= TipoDeDistribuicao
            'End If

            Return NomeDoTipoConcreto
        End Function

    End Class

End Namespace