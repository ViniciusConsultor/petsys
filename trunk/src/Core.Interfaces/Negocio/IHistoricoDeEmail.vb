Namespace Negocio

    Public Interface IHistoricoDeEmail

        Property ID As Nullable(Of Long)
        Property Assunto As String
        Property Remetente As String
        Property Mensagem As String
        Property DestinatariosEmCopia As IList(Of String)
        Property DestinatariosEmCopiaOculta As IList(Of String)
        Property Anexos() As IList(Of String)
        Property Contexto As String
        
    End Interface

End Namespace
