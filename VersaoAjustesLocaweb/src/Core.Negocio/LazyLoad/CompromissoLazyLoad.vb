Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.Documento
Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad

Namespace LazyLoad
    <Serializable()> _
    Public Class CompromissoLazyLoad
        Implements ICompromissoLazyLoad

        Private _CompromissoReal As ICompromisso

        Public Sub New(ByVal ID As Long)
            Me.ID = ID
        End Sub

        Public Property Assunto() As String Implements ICompromisso.Assunto
            Get
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                Return _CompromissoReal.Assunto
            End Get
            Set(ByVal value As String)
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                _CompromissoReal.Assunto = value
            End Set
        End Property

        Public Property Descricao() As String Implements ICompromisso.Descricao
            Get
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                Return _CompromissoReal.Descricao
            End Get
            Set(ByVal value As String)
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                _CompromissoReal.Descricao = value
            End Set
        End Property

        Public Property Fim() As Date Implements ICompromisso.Fim
            Get
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                Return _CompromissoReal.Fim
            End Get
            Set(ByVal value As Date)
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                _CompromissoReal.Fim = value
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
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                Return _CompromissoReal.Inicio
            End Get
            Set(ByVal value As Date)
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                _CompromissoReal.Inicio = value
            End Set
        End Property

        Public Property Local() As String Implements ICompromisso.Local
            Get
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                Return _CompromissoReal.Local
            End Get
            Set(ByVal value As String)
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                _CompromissoReal.Local = value
            End Set
        End Property

        Public Property Proprietario() As IPessoaFisica Implements ICompromisso.Proprietario
            Get
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                Return _CompromissoReal.Proprietario
            End Get
            Set(ByVal value As IPessoaFisica)
                If _CompromissoReal Is Nothing Then CarregueObjetoReal()
                _CompromissoReal.Proprietario = value
            End Set
        End Property

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeAgenda = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAgenda)()
                _CompromissoReal = Servico.ObtenhaCompromisso(Me.ID.Value)
            End Using
        End Sub

        Public Sub EstaConsistente() Implements ICompromisso.EstaConsistente
            If _CompromissoReal Is Nothing Then CarregueObjetoReal()
            _CompromissoReal.EstaConsistente()
        End Sub

        Private _Status As StatusDoCompromisso
        Public Property Status() As StatusDoCompromisso Implements ICompromisso.Status
            Get
                Return _Status
            End Get
            Set(ByVal value As StatusDoCompromisso)
                _Status = value
            End Set
        End Property

    End Class
End Namespace