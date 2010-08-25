Imports System.Xml
Imports System.Xml.XPath
Imports Compartilhados.Fabricas
Imports System.Reflection

Public Class GerenciadorDeGatilhos

    Private _Gatilhos As XmlDocument
    Private Shared InstanciaSolitaria As GerenciadorDeGatilhos
    Private Shared ObjetoLock As New Object

    Public Sub DispareGatilhoAntes(ByVal TipoDaClasse As String, _
                                   ByVal Metodo As String, _
                                   ByVal Parametros() As Object)
        Dim NoGatilhoAntes As XmlNode = ObtenhaNoCorrepondente(TipoDaClasse, Metodo, True)

        If Not NoGatilhoAntes Is Nothing Then

            For Each NoInteressados As XmlNode In NoGatilhoAntes.ChildNodes
                For Each NoInteressado As XmlNode In NoInteressados
                    Dim Instancia As Object = Nothing

                    Try
                        Instancia = FabricaGenerica.GetInstancia.CrieObjeto(NoInteressado.Attributes("fullname").Value, NoInteressado.Attributes("type").Value)

                        Dim mi As MethodInfo = Instancia.GetType().GetMethod(NoInteressado.Attributes("metodo").Value)

                        mi.Invoke(Instancia, BindingFlags.InvokeMethod, Nothing, Parametros, Nothing)
                    Catch ex As DLLNaoEncontradaException
                        'Não faz nada
                    End Try
                Next
            Next
        End If
    End Sub

    Public Sub DispareGatilhoDepois(ByVal TipoDaClasse As String, _
                                    ByVal Metodo As String, _
                                    ByVal Parametros() As Object)
        Dim NoGatilhoDepois As XmlNode = ObtenhaNoCorrepondente(TipoDaClasse, Metodo, False)

        If Not NoGatilhoDepois Is Nothing Then

            For Each NoInteressados As XmlNode In NoGatilhoDepois.ChildNodes
                For Each NoInteressado As XmlNode In NoInteressados
                    Dim Instancia As Object

                    Try
                        Instancia = FabricaGenerica.GetInstancia.CrieObjeto(NoInteressado.Attributes("fullname").Value, NoInteressado.Attributes("type").Value)

                        Dim mi As MethodInfo = Instancia.GetType().GetMethod(NoInteressado.Attributes("metodo").Value)

                        mi.Invoke(Instancia, BindingFlags.InvokeMethod, Nothing, Parametros, Nothing)
                    Catch ex As DLLNaoEncontradaException
                        'Não faz nada
                    End Try
                Next
            Next
        End If
    End Sub

    Private Function ObtenhaNoCorrepondente(ByVal TipoDaClasse As String, ByVal Metodo As String, ByVal GatilhoAntes As Boolean) As XmlNode
        Dim NoCorrespondente As XmlNode = Nothing

        For Each Gatilhos As XmlNode In _Gatilhos.ChildNodes
            For Each Gatilho As XmlNode In Gatilhos.ChildNodes
                If Gatilho.Attributes("type").Value.Equals(TipoDaClasse, StringComparison.InvariantCultureIgnoreCase) Then
                    For Each MetodoDoTipo As XmlNode In Gatilho.ChildNodes
                        If MetodoDoTipo.Attributes("nome").Value.Equals(Metodo) Then
                            For Each NoTipoGatilho As XmlNode In MetodoDoTipo.ChildNodes
                                If GatilhoAntes AndAlso NoTipoGatilho.Name.ToString.Equals("Antes") Then
                                    NoCorrespondente = NoTipoGatilho
                                    Exit For
                                Else
                                    NoCorrespondente = NoTipoGatilho
                                End If
                            Next
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next
        Next

        Return NoCorrespondente
    End Function

    Private Sub New()
        CarregueGatilhos()
    End Sub

    Public Shared Function GetInstancia() As GerenciadorDeGatilhos

        If InstanciaSolitaria Is Nothing Then
            SyncLock ObjetoLock
                If InstanciaSolitaria Is Nothing Then
                    InstanciaSolitaria = New GerenciadorDeGatilhos
                End If
            End SyncLock
        End If

        Return InstanciaSolitaria
    End Function

    Private Sub CarregueGatilhos()
        _Gatilhos = New XmlDocument
        _Gatilhos.Load(Util.ObtenhaCaminhoArquivoXMLDeGatilho)
    End Sub

End Class
