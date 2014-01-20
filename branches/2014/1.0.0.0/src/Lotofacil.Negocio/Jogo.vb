Imports Lotofacil.Interfaces.Negocio
Imports System.Text

<Serializable()> _
Public Class Jogo
    Implements IJogo

    Private _Dezenas As IList(Of IDezena)
    Private _ID As Nullable(Of Long)

    Public Sub New()
        _Dezenas = New List(Of IDezena)
    End Sub

    Public Sub New(ByVal Dezenas As IList(Of IDezena))
        _Dezenas = Dezenas
    End Sub

    Public Sub AdicionaDezena(ByVal Dezena As IDezena) Implements IJogo.AdicionaDezena
        If _Dezenas.Contains(Dezena) Then
            Throw New Exception("Dezena já adicionada no jogo")
        End If
        _Dezenas.Add(Dezena)
    End Sub

    Public Function ObtenhaDezenas() As IList(Of IDezena) Implements IJogo.ObtenhaDezenas
        Return _Dezenas
    End Function

    Public Function ObtenhaQuantidadeDeDezenasAcertadas(ByVal DezenasSorteadas As IList(Of IDezena)) As Short Implements IJogo.ObtenhaQuantidadeDeDezenasAcertadas
        Dim QuantidadeDeDezenasAcertadas As Short = 0
        Dim DezenasJogadas As IList(Of IDezena)

        DezenasJogadas = Me.ObtenhaDezenas

        For Each DezenaSorteada As Dezena In DezenasSorteadas
            If DezenasJogadas.Contains(DezenaSorteada) Then
                QuantidadeDeDezenasAcertadas = QuantidadeDeDezenasAcertadas + 1S
            End If
        Next

        Return QuantidadeDeDezenasAcertadas
    End Function

    Public Function Clone() As Object Implements ICloneable.Clone
        Return New Jogo()
    End Function

    Public Sub AdicionaDezenas(ByVal Dezenas As IList(Of IDezena)) Implements IJogo.AdicionaDezenas
        _Dezenas = Dezenas
    End Sub

    Public ReadOnly Property DezenasToString() As String Implements IJogo.DezenasToString
        Get
            Dim DezenasEmString As New StringBuilder

            For Each Dezena As IDezena In Me.ObtenhaDezenas
                DezenasEmString.Append(String.Concat(Dezena.Numero.ToString, " - "))
            Next

            If Not DezenasEmString.Length = 0 Then
                DezenasEmString.Remove(DezenasEmString.Length - 3, 3)
            End If

            Return DezenasEmString.ToString

        End Get
    End Property

    Public Property ID() As Long? Implements IJogo.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Sub AdicionaDezenas(ByVal Dezenas As String) Implements Interfaces.Negocio.IJogo.AdicionaDezenas
        Dim ArrayDeDezenas() As String

        ArrayDeDezenas = Dezenas.Split("-"c)

        For Each Dezena As String In ArrayDeDezenas
            _Dezenas.Add(FabricaDeDezena.CrieObjeto(CByte(Dezena.Trim)))
        Next

    End Sub

End Class