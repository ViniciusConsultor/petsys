Imports PetSys.Interfaces.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad

    <Serializable()> _
    Public Class AnimalLazyLoad
        Implements IAnimalLazyLoad

        Private _AnimalReal As IAnimal

        Public Sub New(ByVal ID As Long)
            Me.ID = ID
        End Sub

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeAnimal = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAnimal)()
                _AnimalReal = Servico.ObtenhaAnimal(Me.ID.Value)
            End Using
        End Sub

        Public Property DataDeNascimento() As Date? Implements IAnimal.DataDeNascimento
            Get
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                Return _AnimalReal.DataDeNascimento
            End Get
            Set(ByVal value As Date?)
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                _AnimalReal.DataDeNascimento = value
            End Set
        End Property

        Public Property Especie() As Especie Implements IAnimal.Especie
            Get
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                Return _AnimalReal.Especie
            End Get
            Set(ByVal value As Especie)
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                _AnimalReal.Especie = value
            End Set
        End Property

        Public Property Foto() As String Implements IAnimal.Foto
            Get
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                Return _AnimalReal.Foto
            End Get
            Set(ByVal value As String)
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                _AnimalReal.Foto = value
            End Set
        End Property

        Private _ID As Nullable(Of Long)
        Public Property ID() As Long? Implements IAnimal.ID
            Get
                Return _ID
            End Get
            Set(ByVal value As Long?)
                _ID = value
            End Set
        End Property

        Public ReadOnly Property Idade() As String Implements IAnimal.Idade
            Get
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                Return _AnimalReal.Idade
            End Get
        End Property

        Public Property Nome() As String Implements IAnimal.Nome
            Get
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                Return _AnimalReal.Nome
            End Get
            Set(ByVal value As String)
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                _AnimalReal.Nome = value
            End Set
        End Property

        Public Property Proprietario() As IProprietarioDeAnimal Implements IAnimal.Proprietario
            Get
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                Return _AnimalReal.Proprietario
            End Get
            Set(ByVal value As IProprietarioDeAnimal)
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                _AnimalReal.Proprietario = value
            End Set
        End Property

        Public Property Raca() As String Implements IAnimal.Raca
            Get
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                Return _AnimalReal.Raca
            End Get
            Set(ByVal value As String)
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                _AnimalReal.Raca = value
            End Set
        End Property

        Public Property Sexo() As SexoDoAnimal Implements IAnimal.Sexo
            Get
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                Return _AnimalReal.Sexo
            End Get
            Set(ByVal value As SexoDoAnimal)
                If _AnimalReal Is Nothing Then CarregueObjetoReal()
                _AnimalReal.Sexo = value
            End Set
        End Property

    End Class

End Namespace