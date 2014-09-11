Namespace Core.Negocio

    <Serializable()> _
    Public Class Banco

        Private _ID As String
        Private _Nome As String

        Public Shared BancoDoBrasil As Banco = New Banco("001", "Banco do Brasil S.A.")
        Public Shared CEF As Banco = New Banco("104", "Caixa Econômica Federal")
        Public Shared Itau As Banco = New Banco("341", "Itaú Unibanco S.A.")
        Public Shared Bradesco As Banco = New Banco("237", "Banco Bradesco S.A.")
        Public Shared HSBC As Banco = New Banco("399", "HSBC Bank Brasil S.A. - Banco Multiplo")
        Public Shared Santander As Banco = New Banco("033", "Banco Santander (Brasil) S.A.")
        Public Shared Mercantil As Banco = New Banco("389", "Banco Mercantil do Brasil S.A.")

        Private Shared ListaDeBancos As Banco() = {BancoDoBrasil, CEF, Itau, Bradesco, HSBC, Santander}

        Private Sub New(ByVal ID As String, _
                        ByVal Nome As String)
            _ID = ID
            _Nome = Nome
        End Sub

        Public ReadOnly Property ID() As String
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Nome() As String
            Get
                Return _Nome
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As String) As Banco
            For Each Item As Banco In From Item1 In ListaDeBancos Where Item1.ID.Equals(ID)
                Return Item
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of Banco)
            Return New List(Of Banco)(ListaDeBancos)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, Banco).ID = Me.ID
        End Function
    End Class

End Namespace