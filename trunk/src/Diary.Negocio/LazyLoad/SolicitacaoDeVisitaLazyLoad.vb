Imports Diary.Interfaces.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad

    <Serializable()> _
    Public Class SolicitacaoDeVisitaLazyLoad
        Implements ISolicitacaoDeVisitaLazyLoad

        Private _SolicitacaoDeVisitaReal As ISolicitacaoDeVisita

        Public Sub New(ByVal ID As Long)
            Me.ID = ID
        End Sub

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeSolicitacaoDeVisita = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeVisita)()
                _SolicitacaoDeVisitaReal = Servico.ObtenhaSolicitacaoDeVisita(Me.ID.Value)
            End Using
        End Sub

        Public Property Ativa() As Boolean Implements ISolicitacao.Ativa
            Get
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeVisitaReal.Ativa
            End Get
            Set(ByVal value As Boolean)
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeVisitaReal.Ativa = value
            End Set
        End Property

        Public Property Codigo() As Long Implements ISolicitacao.Codigo
            Get
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeVisitaReal.Codigo
            End Get
            Set(ByVal value As Long)
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeVisitaReal.Codigo = value
            End Set
        End Property

        Public Property Contato() As IContato Implements ISolicitacao.Contato
            Get
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeVisitaReal.Contato
            End Get
            Set(ByVal value As IContato)
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeVisitaReal.Contato = value
            End Set
        End Property

        Public Property DataDaSolicitacao() As Date Implements ISolicitacao.DataDaSolicitacao
            Get
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeVisitaReal.DataDaSolicitacao
            End Get
            Set(ByVal value As Date)
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeVisitaReal.DataDaSolicitacao = value
            End Set
        End Property

        Public Property Descricao() As String Implements ISolicitacao.Descricao
            Get
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeVisitaReal.Descricao
            End Get
            Set(ByVal value As String)
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeVisitaReal.Descricao = value
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
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeVisitaReal.UsuarioQueCadastrou
            End Get
            Set(ByVal value As Compartilhados.Usuario)
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeVisitaReal.UsuarioQueCadastrou = value
            End Set
        End Property

        Public Property Assunto() As String Implements ISolicitacaoDeVisitaLazyLoad.Assunto
            Get
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeVisitaReal.Assunto
            End Get
            Set(ByVal value As String)
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeVisitaReal.Assunto = value
            End Set
        End Property

        Public Property Local() As String Implements ISolicitacao.Local
            Get
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeVisitaReal.Local
            End Get
            Set(ByVal value As String)
                If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeVisitaReal.Local = value
            End Set
        End Property

        Public Function TemDespacho() As Boolean Implements ISolicitacao.TemDespacho
            If _SolicitacaoDeVisitaReal Is Nothing Then CarregueObjetoReal()
            Return _SolicitacaoDeVisitaReal.TemDespacho
        End Function
    End Class

End Namespace