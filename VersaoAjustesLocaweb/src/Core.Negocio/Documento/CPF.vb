Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class CPF
    Inherits Documento
    Implements ICPF

    Public Sub New(ByVal Numero As String)
        MyBase.New(Numero)
    End Sub

    Public Overrides ReadOnly Property Tipo() As TipoDeDocumento
        Get
            Return TipoDeDocumento.CPF
        End Get
    End Property

    Public Overrides Function EhValido() As Boolean
        Dim Soma As Integer
        Dim Resto As Integer
        Dim I As Integer

        Me.Numero = Format(CLng(Me.Numero), "00000000000")

        Soma = 0

        For I = 1 To 9
            Soma = CInt(Soma + CInt(Mid$(Me.Numero, I, 1)) * (11 - I))
        Next I

        Resto = CInt(11 - (Soma - (Int(Soma / 11) * 11)))

        If Resto = 10 Or Resto = 11 Then Resto = 0

        If Resto <> CInt(Mid$(Me.Numero, 10, 1)) Then
            Return False
        End If

        Soma = 0

        For I = 1 To 10
            Soma = CInt(Soma + CInt(Mid$(Me.Numero, I, 1)) * (12 - I))
        Next I

        Resto = CInt(11 - (Soma - (Int(Soma / 11) * 11)))

        If Resto = 10 Or Resto = 11 Then Resto = 0

        If Resto <> CInt(Mid$(Me.Numero, 11, 1)) Then
            Return False
        End If

        Return True
    End Function

    Public Overrides Function ToString() As String
        Return Format(CLng(Me.Numero), "000\.###\.###-##")
    End Function

End Class
