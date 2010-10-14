Imports PetSys.Interfaces.Negocio.LazyLoad
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio.LazyLoad
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas

Namespace LazyLoad

    <Serializable()> _
    Public Class VeterinarioLazyLoad
        Implements IVeterinarioLazyLoad

        Private _VeterinarioReal As IVeterinario

        Public Sub New(ByVal Pessoa As IPessoa)
            _Pessoa = Pessoa
        End Sub

        Private _Pessoa As IPessoa
        Public ReadOnly Property Pessoa() As IPessoa Implements IPapelPessoa.Pessoa
            Get
                Return _Pessoa
            End Get
        End Property

        Public Sub CarregueObjetoReal() Implements IObjetoLazyLoad.CarregueObjetoReal
            Using Servico As IServicoDeVeterinario = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeVeterinario)()
                _VeterinarioReal = Servico.Obtenha(Pessoa)
            End Using
        End Sub

        Public Property CRMV() As String Implements IVeterinario.CRMV
            Get
                If _VeterinarioReal Is Nothing Then CarregueObjetoReal()
                Return _VeterinarioReal.CRMV
            End Get
            Set(ByVal value As String)
                If _VeterinarioReal Is Nothing Then CarregueObjetoReal()
                _VeterinarioReal.CRMV = value
            End Set
        End Property

        Public Property UF() As UF Implements IVeterinario.UF
            Get
                If _VeterinarioReal Is Nothing Then CarregueObjetoReal()
                Return _VeterinarioReal.UF
            End Get
            Set(ByVal value As UF)
                If _VeterinarioReal Is Nothing Then CarregueObjetoReal()
                _VeterinarioReal.UF = value
            End Set
        End Property

    End Class

End Namespace