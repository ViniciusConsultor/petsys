Namespace Core.Negocio
    Public Interface IHistoricoDeEmail

        Property ID As Nullable(Of Long)
        Property Data As Date
        Property Assunto As String
        Property Remetente As String
        Property Mensagem As String
        Property Destinatarios As IList(Of String)
        Property DestinatariosEmCopia As IList(Of String)
        Property DestinatariosEmCopiaOculta As IList(Of String)
        Property Contexto As String
        Property PossuiAnexo As Boolean

    End Interface
End Namespace