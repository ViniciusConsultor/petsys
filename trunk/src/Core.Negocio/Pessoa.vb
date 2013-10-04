Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados

<Serializable()> _
Public MustInherit Class Pessoa
    Implements IPessoa

    Private _ID As Nullable(Of Long)
    Private _Nome As String
    Private _EnderecoDeEmail As EnderecoDeEmail
    Private _Telefones As IList(Of ITelefone)
    Private _Documentos As IDictionary(Of TipoDeDocumento, IDocumento)
    Private _Enderecos As IList(Of IEndereco)
    Private _Site As String
    Private _DadoBancario As IDadoBancario

    Protected Sub New()
        _Documentos = New Dictionary(Of TipoDeDocumento, IDocumento)
        _Telefones = New List(Of ITelefone)
        _Enderecos = New List(Of IEndereco)
    End Sub

    Public Property Nome() As String Implements IPessoa.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property ID() As Long? Implements IPessoa.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public MustOverride ReadOnly Property Tipo() As TipoDePessoa Implements IPessoa.Tipo

    Public Sub AdicioneDocumento(ByVal Documento As IDocumento) Implements IPessoa.AdicioneDocumento
        If _Documentos.ContainsKey(Documento.Tipo) Then
            _Documentos(Documento.Tipo) = Documento
            Exit Sub
        End If

        _Documentos.Add(Documento.Tipo, Documento)
    End Sub

    Public Function ObtenhaDocumento(ByVal TipoDocumento As TipoDeDocumento) As IDocumento Implements IPessoa.ObtenhaDocumento
        Dim Documento As IDocumento = Nothing

        _Documentos.TryGetValue(TipoDocumento, Documento)

        Return Documento
    End Function

    Public Sub AdicioneTelefone(ByVal Telefone As ITelefone) Implements IPessoa.AdicioneTelefone
        If _Telefones.Contains(Telefone) Then
            _Telefones(_Telefones.IndexOf(Telefone)) = Telefone
            Exit Sub
        End If

        _Telefones.Add(Telefone)
    End Sub

    Public ReadOnly Property Telefones() As IList(Of ITelefone) Implements IPessoa.Telefones
        Get
            Return _Telefones
        End Get
    End Property

    Public Property EnderecoDeEmail() As EnderecoDeEmail Implements IPessoa.EnderecoDeEmail
        Get
            Return _EnderecoDeEmail
        End Get
        Set(ByVal value As EnderecoDeEmail)
            _EnderecoDeEmail = value
        End Set
    End Property

    Public Property Site() As String Implements IPessoa.Site
        Get
            Return _Site
        End Get
        Set(ByVal value As String)
            _Site = value
        End Set
    End Property

    Public Sub AdicioneTelefones(ByVal Telefones As IList(Of ITelefone)) Implements IPessoa.AdicioneTelefones
        If Not Telefones Is Nothing Then
            CType(_Telefones, List(Of ITelefone)).AddRange(Telefones)
        End If
    End Sub

    Public Function ObtenhaTelelefones(ByVal TipoTelefone As TipoDeTelefone) As IList(Of ITelefone) Implements IPessoa.ObtenhaTelefones
        If Me._Telefones Is Nothing Then Return Nothing

        Dim TelefonesDoTipo As IList(Of ITelefone) = New List(Of ITelefone)

        For Each Item As ITelefone In Me._Telefones
            If Item.Tipo.Equals(TipoTelefone) Then
                TelefonesDoTipo.Add(Item)
            End If
        Next

        Return TelefonesDoTipo
    End Function

    Public Property DadoBancario() As IDadoBancario Implements IPessoa.DadoBancario
        Get
            Return _DadoBancario
        End Get
        Set(ByVal value As IDadoBancario)
            _DadoBancario = value
        End Set
    End Property

    Public Sub AdicioneEndereco(ByVal Endereco As IEndereco) Implements IPessoa.AdicioneEndereco
        If _Enderecos.Contains(Endereco) Then
            _Enderecos(_Enderecos.IndexOf(Endereco)) = Endereco
            Exit Sub
        End If

        _Enderecos.Add(Endereco)
    End Sub

    Public Sub AdicioneEnderecos(ByVal Enderecos As IList(Of IEndereco)) Implements IPessoa.AdicioneEnderecos
        If Not Enderecos Is Nothing Then
            CType(_Enderecos, List(Of IEndereco)).AddRange(Enderecos)
        End If
    End Sub

    Public ReadOnly Property Enderecos As IList(Of IEndereco) Implements IPessoa.Enderecos
        Get
            Return _Enderecos
        End Get
    End Property

    Public Function ObtenhaEnderecos(ByVal TipoDeEndereco As ITipoDeEndereco) As IList(Of IEndereco) Implements IPessoa.ObtenhaEnderecos
        If Me._Enderecos Is Nothing Then Return Nothing

        Dim EnderecosDoTipo As IList(Of IEndereco) = New List(Of IEndereco)

        For Each Item As IEndereco In Me._Enderecos
            If Item.TipoDeEndereco.Equals(TipoDeEndereco) Then
                EnderecosDoTipo.Add(Item)
            End If
        Next

        Return EnderecosDoTipo
    End Function

End Class