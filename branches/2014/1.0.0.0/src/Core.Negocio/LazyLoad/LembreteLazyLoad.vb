Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Namespace LazyLoad
    <Serializable()> _
    Public Class LembreteLazyLoad
        Implements ILembreteLazyLoad

        Private _LembreteReal As ILembrete

        Public Sub New(ByVal ID As Long)
            Me.ID = ID
        End Sub

        Public Property Assunto() As String Implements ICompromisso.Assunto
            Get
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                Return _LembreteReal.Assunto
            End Get
            Set(ByVal value As String)
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                _LembreteReal.Assunto = value
            End Set
        End Property

        Public Property Descricao() As String Implements ICompromisso.Descricao
            Get
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                Return _LembreteReal.Descricao
            End Get
            Set(ByVal value As String)
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                _LembreteReal.Descricao = value
            End Set
        End Property

        Public Property Fim() As Date Implements ICompromisso.Fim
            Get
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                Return _LembreteReal.Fim
            End Get
            Set(ByVal value As Date)
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                _LembreteReal.Fim = value
            End Set
        End Property

        Private _ID As Nullable(Of Long)
        Public Property ID() As Long? Implements ICompromisso.ID
            Get
                Return _ID
            End Get
            Set(ByVal value As Long?)
                _ID = value
            End Set
        End Property

        Public Property Inicio() As Date Implements ICompromisso.Inicio
            Get
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                Return _LembreteReal.Inicio
            End Get
            Set(ByVal value As Date)
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                _LembreteReal.Inicio = value
            End Set
        End Property

        Public Property Local() As String Implements ICompromisso.Local
            Get
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                Return _LembreteReal.Local
            End Get
            Set(ByVal value As String)
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                _LembreteReal.Local = value
            End Set
        End Property

        Public Property Proprietario() As IPessoaFisica Implements ICompromisso.Proprietario
            Get
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                Return _LembreteReal.Proprietario
            End Get
            Set(ByVal value As IPessoaFisica)
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                _LembreteReal.Proprietario = value
            End Set
        End Property

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                _LembreteReal = Servico.ObtenhaLembrete(Me.ID.Value)
            End Using
        End Sub

        Public Sub EstaConsistente() Implements ICompromisso.EstaConsistente
            If _LembreteReal Is Nothing Then CarregueObjetoReal()
            _LembreteReal.EstaConsistente()
        End Sub

        Public Property Status() As StatusDoCompromisso Implements ICompromisso.Status
            Get
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                Return _LembreteReal.Status
            End Get
            Set(ByVal value As StatusDoCompromisso)
                If _LembreteReal Is Nothing Then CarregueObjetoReal()
                _LembreteReal.Status = value
            End Set
        End Property

    End Class
End Namespace