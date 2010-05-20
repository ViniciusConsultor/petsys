Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Cliente
    Inherits PapelPessoa
    Implements ICliente

    Private _DataDoCadastro As Nullable(Of Date)

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

    Public Property DataDoCadastro() As Date? Implements ICliente.DataDoCadastro
        Get
            Return _DataDoCadastro
        End Get
        Set(ByVal value As Date?)
            _DataDoCadastro = value
        End Set
    End Property

End Class