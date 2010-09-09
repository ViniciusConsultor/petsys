Imports Diary.Interfaces.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Diary.Interfaces.Negocio
Imports Diary.Interfaces.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad

    <Serializable()> _
    Public Class SolicitacaoDeConviteLazyLoad
        Implements ISolicitacaoDeConviteLazyLoad

        Private _SolicitacaoDeConviteReal As ISolicitacaoDeConvite

        Public Sub New(ByVal ID As Long)
            Me.ID = ID
        End Sub

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeSolicitacaoDeConvite = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSolicitacaoDeConvite)()
                _SolicitacaoDeConviteReal = Servico.ObtenhaSolicitacaoDeConvite(Me.ID.Value)
            End Using
        End Sub

        Public Property Ativa() As Boolean Implements ISolicitacao.Ativa
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.Ativa
            End Get
            Set(ByVal value As Boolean)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.Ativa = value
            End Set
        End Property

        Public Property Codigo() As Long Implements ISolicitacao.Codigo
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.Codigo
            End Get
            Set(ByVal value As Long)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.Codigo = value
            End Set
        End Property

        Public Property Contato() As IContato Implements ISolicitacao.Contato
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.Contato
            End Get
            Set(ByVal value As IContato)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.Contato = value
            End Set
        End Property

        Public Property DataDaSolicitacao() As Date Implements ISolicitacao.DataDaSolicitacao
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.DataDaSolicitacao
            End Get
            Set(ByVal value As Date)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.DataDaSolicitacao = value
            End Set
        End Property

        Public Property Descricao() As String Implements ISolicitacao.Descricao
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.Descricao
            End Get
            Set(ByVal value As String)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.Descricao = value
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
                Return TipoDeSolicitacao.Convite
            End Get
        End Property

        Public Property UsuarioQueCadastrou() As Compartilhados.Usuario Implements ISolicitacao.UsuarioQueCadastrou
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.UsuarioQueCadastrou
            End Get
            Set(ByVal value As Compartilhados.Usuario)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.UsuarioQueCadastrou = value
            End Set
        End Property

        Public Property DataEHorario() As Date Implements ISolicitacaoDeConvite.DataEHorario
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.DataEHorario
            End Get
            Set(ByVal value As Date)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.DataEHorario = value
            End Set
        End Property

        Public Property Observacao() As String Implements ISolicitacaoDeConvite.Observacao
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.Observacao
            End Get
            Set(ByVal value As String)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.Local = value
            End Set
        End Property

        Public Sub EstaConsistente() Implements Interfaces.Negocio.ISolicitacaoDeConvite.EstaConsistente
            If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
            _SolicitacaoDeConviteReal.EstaConsistente()
        End Sub

        Public Property Local() As String Implements Interfaces.Negocio.ISolicitacao.Local
            Get
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                Return _SolicitacaoDeConviteReal.Local
            End Get
            Set(ByVal value As String)
                If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
                _SolicitacaoDeConviteReal.Local = value
            End Set
        End Property

        Public Function TemDespacho() As Boolean Implements ISolicitacao.TemDespacho
            If _SolicitacaoDeConviteReal Is Nothing Then CarregueObjetoReal()
            Return _SolicitacaoDeConviteReal.TemDespacho
        End Function

    End Class

End Namespace