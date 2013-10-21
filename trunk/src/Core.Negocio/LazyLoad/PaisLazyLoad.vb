Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad
    <Serializable()> _
    Public Class PaisLazyLoad
        Implements IPaisLazyLoad

        Private _ID as Long?
        Private _PaisReal As IPais

        Public Sub New(ByVal ID As Long)
            _ID = ID
        End Sub

        Public Property ID() As Long? Implements IPais.ID
            Get
                Return _ID
            End Get
            Set (ByVal value As Long?)
                _ID = value
            End Set
        End Property

        Public Property Nome() As String Implements IPais.Nome
            Get
                If _PaisReal Is Nothing Then CarregueObjetoReal()
                Return _PaisReal.Nome
            End Get
            Set (ByVal value As String)
                If _PaisReal Is Nothing Then CarregueObjetoReal()
                _PaisReal.Nome = value
            End Set
        End Property

        Public Property Sigla() As String Implements IPais.Sigla
            Get
                If _PaisReal Is Nothing Then CarregueObjetoReal()
                Return _PaisReal.Sigla
            End Get
            Set (ByVal value As String)
                If _PaisReal Is Nothing Then CarregueObjetoReal()
                _PaisReal.Sigla = value
            End Set
        End Property

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDePais = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePais)()
                _PaisReal = Servico.ObtenhaPais(ID.Value)
            End Using
        End Sub
    End Class
End Namespace

