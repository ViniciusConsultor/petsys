
Namespace Negocio

    <Serializable()> _
    Public Class DTOAjudanteDePesquisaDeMenu

        Public Sub New(ByVal IDDaFuncao As String, ByVal NomeDaFuncao As String, ByVal CaminhoMenu As String)
            _IDDaFuncao = IDDaFuncao
            _NomeDaFuncao = NomeDaFuncao
            _CaminhoMenu = CaminhoMenu
        End Sub

        Private _IDDaFuncao As String
        Public ReadOnly Property IDDaFuncao As String
            Get
                Return _IDDaFuncao
            End Get
        End Property

        Private _NomeDaFuncao As String
        Public ReadOnly Property NomeDaFuncao As String
            Get
                Return _NomeDaFuncao
            End Get
        End Property

        Private _CaminhoMenu As String
        Public ReadOnly Property CaminhoMenu As String
            Get
                Return _CaminhoMenu
            End Get
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, DTOAjudanteDePesquisaDeMenu).IDDaFuncao = Me.IDDaFuncao
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return IDDaFuncao.GetHashCode
        End Function


    End Class

End Namespace
