Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

<Serializable()> _
Public Class TarefaLazyLoad
    Implements ITarefaLazyLoad

    Private _TarefaReal As ITarefa

    Public Sub New(ByVal ID As Long)
        Me.ID = ID
    End Sub

    Public Property Assunto() As String Implements ITarefa.Assunto
        Get
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            Return _TarefaReal.Assunto
        End Get
        Set(ByVal value As String)
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            _TarefaReal.Assunto = value
        End Set
    End Property

    Public Property DataDeConclusao() As Date Implements ITarefa.DataDeConclusao
        Get
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            Return _TarefaReal.DataDeConclusao
        End Get
        Set(ByVal value As Date)
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            _TarefaReal.DataDeConclusao = value
        End Set
    End Property

    Public Property DataDeInicio() As Date Implements ITarefa.DataDeInicio
        Get
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            Return _TarefaReal.DataDeInicio
        End Get
        Set(ByVal value As Date)
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            _TarefaReal.DataDeInicio = value
        End Set
    End Property

    Public Property Descricao() As String Implements ITarefa.Descricao
        Get
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            Return _TarefaReal.Descricao
        End Get
        Set(ByVal value As String)
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            _TarefaReal.Descricao = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements ITarefa.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Property Prioridade() As PrioridadeDaTarefa Implements ITarefa.Prioridade
        Get
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            Return _TarefaReal.Prioridade
        End Get
        Set(ByVal value As PrioridadeDaTarefa)
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            _TarefaReal.Prioridade = value
        End Set
    End Property

    Public Property Proprietario() As IPessoaFisica Implements ITarefa.Proprietario
        Get
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            Return _TarefaReal.Proprietario
        End Get
        Set(ByVal value As IPessoaFisica)
            If _TarefaReal Is Nothing Then CarregueObjetoReal()
            _TarefaReal.Proprietario = value
        End Set
    End Property

    Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
        Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
            _TarefaReal = Servico.ObtenhaTarefa(Me.ID.Value)
        End Using
    End Sub

End Class
