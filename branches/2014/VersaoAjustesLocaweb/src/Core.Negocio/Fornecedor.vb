Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Fornecedor
    Inherits PapelPessoa
    Implements IFornecedor

    Private _Contatos As IList(Of IPessoaFisica)

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
        _Contatos = New List(Of IPessoaFisica)
    End Sub

    Public ReadOnly Property Contatos() As IList(Of IPessoaFisica) Implements IFornecedor.Contatos
        Get
            Return _Contatos
        End Get
    End Property

    Public Sub AdicionaContato(ByVal Contato As IPessoaFisica) Implements IFornecedor.AdicionaContato
        If Not _Contatos.Contains(Contato) Then _Contatos.Add(Contato)
    End Sub

    Private _InformacoesAdicionais As String
    Public Property InformacoesAdicionais() As String Implements IFornecedor.InformacoesAdicionais
        Get
            Return _InformacoesAdicionais
        End Get
        Set(ByVal value As String)
            _InformacoesAdicionais = value
        End Set
    End Property

    Private _DataDoCadastro As Date
    Public Property DataDoCadastro() As Date Implements IFornecedor.DataDoCadastro
        Get
            Return _DataDoCadastro
        End Get
        Set(ByVal value As Date)
            _DataDoCadastro = value
        End Set
    End Property

End Class