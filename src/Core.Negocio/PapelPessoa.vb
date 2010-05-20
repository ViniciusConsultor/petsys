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

End Class