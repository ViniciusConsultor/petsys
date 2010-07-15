Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio.Documento

Namespace Core.Negocio

    Public Interface IPessoa

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        ReadOnly Property Tipo() As TipoDePessoa
        Property EnderecoDeEmail() As EnderecoDeEmail
        ReadOnly Property Telefones() As IList(Of ITelefone)
        ReadOnly Property Documentos() As IList(Of IDocumento)
        Function ObtenhaDocumento(ByVal TipoDocumento As TipoDeDocumento) As IDocumento
        Function ObtenhaTelelefone(ByVal TipoTelefone As TipoDeTelefone) As ITelefone
        Sub AdicioneDocumento(ByVal Documento As IDocumento)
        Property Endereco() As IEndereco
        Sub AdicioneTelefone(ByVal Telefone As ITelefone)
        Sub AdicioneTelefones(ByVal Telefones As IList(Of ITelefone))
        Sub AdicioneDadoBancario(ByVal DadoBancario As IDadoBancario)
        Function ObtenhaDadosBancarios() As IList(Of IDadoBancario)
        Property Site() As String

    End Interface

End Namespace