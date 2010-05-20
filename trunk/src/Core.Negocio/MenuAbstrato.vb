Imports Core.Interfaces.Negocio

Public Class MenuAbstrato
    Implements IMenuAbstrato

    Private _ID As String
    Private _Imagem As String
    Private _Nome As String

    Public Property ID() As String Implements IMenuAbstrato.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As String)
            _ID = value
        End Set
    End Property

    Public Property Imagem() As String Implements IMenuAbstrato.Imagem
        Get
            Return _Imagem
        End Get
        Set(ByVal value As String)
            _Imagem = value
        End Set
    End Property

    Public Property Nome() As String Implements IMenuAbstrato.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Function IDSemFormatacao() As String Implements IMenuAbstrato.IDSemFormatacao
        Return Me._ID.Replace("."c, "_"c)
    End Function

End Class
