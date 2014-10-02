Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class HistoricoDeEmail
    Implements IHistoricoDeEmail
    
    Private _Data As Date
    Public Property Data() As Date Implements IHistoricoDeEmail.Data
        Get
            Return _Data
        End Get
        Set(ByVal value As Date)
            _Data = value
        End Set
    End Property

    Private _Assunto As String
    Public Property Assunto As String Implements IHistoricoDeEmail.Assunto
        Get
            Return _Assunto
        End Get
        Set(value As String)
            _Assunto = value
        End Set
    End Property

    Private _Contexto As String
    Public Property Contexto As String Implements IHistoricoDeEmail.Contexto
        Get
            Return _Contexto
        End Get
        Set(value As String)
            _Contexto = value
        End Set
    End Property

    Private _PossuiAnexo As Boolean
    Public Property PossuiAnexo() As Boolean Implements IHistoricoDeEmail.PossuiAnexo
        Get
            Return _PossuiAnexo
        End Get
        Set(ByVal value As Boolean)
            _PossuiAnexo = value
        End Set
    End Property

    Private _DestinatariosEmCopia As IList(Of String)
    Public Property DestinatariosEmCopia As IList(Of String) Implements IHistoricoDeEmail.DestinatariosEmCopia
        Get
            Return _DestinatariosEmCopia
        End Get
        Set(value As IList(Of String))
            _DestinatariosEmCopia = value
        End Set
    End Property

    Private _DestinatariosEmCopiaOculta As IList(Of String)
    Public Property DestinatariosEmCopiaOculta As IList(Of String) Implements IHistoricoDeEmail.DestinatariosEmCopiaOculta
        Get
            Return _DestinatariosEmCopiaOculta
        End Get
        Set(value As IList(Of String))
            _DestinatariosEmCopiaOculta = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID As Long? Implements IHistoricoDeEmail.ID
        Get
            Return _ID
        End Get
        Set(value As Long?)
            _ID = value
        End Set
    End Property

    Private _Mensagem As String
    Public Property Mensagem As String Implements IHistoricoDeEmail.Mensagem
        Get
            Return _Mensagem
        End Get
        Set(value As String)
            _Mensagem = value
        End Set
    End Property

    Private _Remetente As String
    Public Property Remetente As String Implements IHistoricoDeEmail.Remetente
        Get
            Return _Remetente
        End Get
        Set(value As String)
            _Remetente = value
        End Set
    End Property

    Private _Destinatarios As IList(Of String)
    Public Property Destinatarios As IList(Of String) Implements IHistoricoDeEmail.Destinatarios
        Get
            Return _Destinatarios
        End Get
        Set(value As IList(Of String))
            _Destinatarios = value
        End Set
    End Property

End Class