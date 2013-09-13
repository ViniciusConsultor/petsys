Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public MustInherit Class PapelPessoa
    Implements IPapelPessoa

    Private _Pessoa As IPessoa

    Protected Sub New(ByVal Pessoa As IPessoa)
        _Pessoa = Pessoa
    End Sub

    Public ReadOnly Property Pessoa() As IPessoa Implements IPapelPessoa.Pessoa
        Get
            Return _Pessoa
        End Get
    End Property

    Public Overrides Function Equals(ByVal obj As Object) As Boolean

        If _Pessoa Is Nothing Then
            Return MyBase.Equals(obj)
        End If

        Return _Pessoa.ID.Equals(DirectCast(obj, IPapelPessoa).Pessoa.ID)

    End Function

    Public Overrides Function GetHashCode() As Integer
        If _Pessoa Is Nothing Then
            Return MyBase.GetHashCode
        End If

        Return _Pessoa.ID.GetHashCode
    End Function



End Class