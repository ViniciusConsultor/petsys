Imports Diary.Interfaces.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad

    <Serializable()> _
    Public Class SolicitacaoDeAudienciaLazyLoad
        Implements ISolicitacaoDeAudienciaLazyLoad

        Private _SolicitacaoDeAudienciaReal As ISolicitacaoDeAudiencia

        Public Sub New(ByVal ID As Long)
            Me.ID = ID
        End Sub

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeSolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeAudiencia)()
                _SolicitacaoDeAudienciaReal = Servico.ObtenhaSolicitacaoDeAudiencia(Me.ID.Value)
            End Using
        End Sub

        Public Property Ativa() As Boolean Implements ISolicitacao.Ativa
            Get
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeAudienciaReal.Ativa
            End Get
            Set(ByVal value As Boolean)
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeAudienciaReal.Ativa = value
            End Set
        End Property

        Public Property Codigo() As Long Implements ISolicitacao.Codigo
            Get
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeAudienciaReal.Codigo
            End Get
            Set(ByVal value As Long)
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeAudienciaReal.Codigo = value
            End Set
        End Property

        Public Property Contato() As IContato Implements ISolicitacao.Contato
            Get
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeAudienciaReal.Contato
            End Get
            Set(ByVal value As IContato)
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeAudienciaReal.Contato = value
            End Set
        End Property

        Public Property DataDaSolicitacao() As Date Implements ISolicitacao.DataDaSolicitacao
            Get
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeAudienciaReal.DataDaSolicitacao
            End Get
            Set(ByVal value As Date)
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeAudienciaReal.DataDaSolicitacao = value
            End Set
        End Property

        Public Property Descricao() As String Implements ISolicitacao.Descricao
            Get
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeAudienciaReal.Descricao
            End Get
            Set(ByVal value As String)
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeAudienciaReal.Descricao = value
            End Set
        End Property

        Private _ID As Nullable(Of Long)
        Public Property ID() As Long? Implements ISolicitacao.ID
            Get
                Return _ID
            End Get
            Set(ByVal value As Long?)
                _ID = value
            End Set
        End Property

        Public ReadOnly Property Tipo() As TipoDeSolicitacao Implements ISolicitacao.Tipo
            Get
                Return TipoDeSolicitacao.Audiencia
            End Get
        End Property

        Public Property UsuarioQueCadastrou() As Compartilhados.Usuario Implements ISolicitacao.UsuarioQueCadastrou
            Get
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeAudienciaReal.UsuarioQueCadastrou
            End Get
            Set(ByVal value As Compartilhados.Usuario)
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeAudienciaReal.UsuarioQueCadastrou = value
            End Set
        End Property

        Public Property Assunto() As String Implements ISolicitacaoDeAudiencia.Assunto
            Get
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeAudienciaReal.Assunto
            End Get
            Set(ByVal value As String)
                If _SolicitacaoDeAudienciaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeAudienciaReal.Assunto = value
            End Set
        End Property

    End Class

End Namespace