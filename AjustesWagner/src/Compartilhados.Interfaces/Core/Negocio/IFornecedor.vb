Namespace Core.Negocio

    Public Interface IFornecedor
        Inherits IPapelPessoa

        ReadOnly Property Contatos() As IList(Of IPessoaFisica)
        Sub AdicionaContato(ByVal Contato As IPessoaFisica)
        Property InformacoesAdicionais() As String
        Property DataDoCadastro() As Date

    End Interface

End Namespace