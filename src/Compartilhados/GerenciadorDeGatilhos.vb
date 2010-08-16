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
        'Dim no As XmlNode = _Gatilhos.DocumentElement.SelectSingleNode("")
        Dim Instancia As Object

        Parametros = New Object() {1}
        Instancia = FabricaGenerica.GetInstancia.CrieObjeto("Diary.Interfaces.Servicos", "IServicoDeAgenda")

        Dim mi As MethodInfo = Instancia.GetType().GetMethod("RemovaDespachoAssociadoACompromisso")

        mi.Invoke(Instancia, BindingFlags.InvokeMethod, Nothing, Parametros, Nothing)


    End Sub

    Public Sub DispareGatilhoDepois(ByVal TipoDaClasse As String, _
                                    ByVal Metodo As String)
        'Dim no As XmlNode = _Gatilhos.DocumentElement.SelectSingleNode("")
        Dim Instancia As Object

        Instancia = FabricaGenerica.GetInstancia.CrieObjeto("Diary.Interfaces.Servicos", "IServicoDeAgenda")
    End Sub

    Private Sub New()
        'CarregueGatilhos()
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
        _Gatilhos.Load("gatilhos.xml")
    End Sub

End Class
