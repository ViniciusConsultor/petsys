Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Tarefa
    Implements ITarefa

    Private _Assunto As String
    Public Property Assunto() As String Implements ITarefa.Assunto
        Get
            Return _Assunto
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private _DataDeConclusao As Date
    Public Property DataDeConclusao() As Date Implements ITarefa.DataDeConclusao
        Get
            Return _DataDeConclusao
        End Get
        Set(ByVal value As Date)
            _DataDeConclusao = value
        End Set
    End Property

    Private _DataDeInicio As Date
    Public Property DataDeInicio() As Date Implements ITarefa.DataDeInicio
        Get
            Return _DataDeInicio
        End Get
        Set(ByVal value As Date)
            _DataDeInicio = value
        End Set
    End Property

    Private _Descricao As String
    Public Property Descricao() As String Implements ITarefa.Descricao
        Get
            Return _Descricao
        End Get
        Set(ByVal value As String)
            _Descricao = value
        End Set
    End Property

    Private _Prioridade As PrioridadeDaTarefa
    Public Property Prioridade() As PrioridadeDaTarefa Implements ITarefa.Prioridade
        Get
            Return _Prioridade
        End Get
        Set(ByVal value As PrioridadeDaTarefa)
            _Prioridade = value
        End Set
    End Property

    Private _Proprietario As IPessoaFisica
    Public Property Proprietario() As IPessoaFisica Implements ITarefa.Proprietario
        Get
            Return _Proprietario
        End Get
        Set(ByVal value As IPessoaFisica)
            _Proprietario = value
        End Set
    End Property

End Class