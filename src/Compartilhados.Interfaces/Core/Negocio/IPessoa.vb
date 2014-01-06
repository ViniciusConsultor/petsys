Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio.Documento

Namespace Core.Negocio

    Public Interface IPessoa

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        ReadOnly Property Tipo() As TipoDePessoa
        Property EnderecoDeEmail() As EnderecoDeEmail
        ReadOnly Property Telefones() As IList(Of ITelefone)
        ReadOnly Property Enderecos As IList(Of IEndereco)
        Function ObtenhaDocumento(ByVal TipoDocumento As TipoDeDocumento) As IDocumento
        Function ObtenhaTelefones(ByVal TipoTelefone As TipoDeTelefone) As IList(Of ITelefone)
        Function ObtenhaEnderecos(ByVal TipoDeEndereco As ITipoDeEndereco) As IList(Of IEndereco)
        Sub AdicioneDocumento(ByVal Documento As IDocumento)
        Sub AdicioneTelefone(ByVal Telefone As ITelefone)
        Sub AdicioneTelefones(ByVal Telefones As IList(Of ITelefone))
        Sub AdicioneEndereco(ByVal Endereco As IEndereco)
        Sub AdicioneEnderecos(ByVal Enderecos As IList(Of IEndereco))
        Property DadoBancario() As IDadoBancario
        Property Site() As String
        Function ObtenhaDocumentos() As IList(Of IDocumento)
        Sub AdicioneContato(Contato As String)
        Function Contatos() As IList(Of String)
        Sub AdicioneContatos(Contatos As IList(Of String))

    End Interface

End Namespace