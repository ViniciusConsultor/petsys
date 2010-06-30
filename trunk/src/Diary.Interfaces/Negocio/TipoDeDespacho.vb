Namespace Negocio

    <Serializable()> _
    Public Class TipoDeDespacho

        Private _ID As Byte
        Private _Descricao As String

        Public Shared Telegrama As TipoDeDespacho = New TipoDeDespacho(1, "TELEGRAMA")
        Public Shared Agendar As TipoDeDespacho = New TipoDeDespacho(2, "AGENDAR")
        Public Shared Mensagem As TipoDeDespacho = New TipoDeDespacho(3, "MENSAGEM")
        Public Shared Representante As TipoDeDespacho = New TipoDeDespacho(4, "REPRESENTANTE")
        Public Shared Lembrente As TipoDeDespacho = New TipoDeDespacho(5, "LEMBRETE")
        Public Shared Presente As TipoDeDespacho = New TipoDeDespacho(6, "PRESENTE")
        Public Shared Remarcar As TipoDeDespacho = New TipoDeDespacho(7, "REMARCAR")
        Public Shared Outros As TipoDeDespacho = New TipoDeDespacho(8, "OUTROS")

        Private Sub New(ByVal ID As Byte, ByVal Descricao As String)

        End Sub

    End Class

End Namespace