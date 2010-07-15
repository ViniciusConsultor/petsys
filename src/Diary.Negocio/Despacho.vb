Imports Diary.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public MustInherit Class Despacho
    Implements IDespacho

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IDespacho.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Responsavel As IPessoaFisica
    Public Property Responsavel() As IPessoaFisica Implements IDespacho.Responsavel
        Get
            Return _Responsavel
        End Get
        Set(ByVal value As IPessoaFisica)
            _Responsavel = value
        End Set
    End Property

    Private _Solicitacao As ISolicitacao
    Public Property Solicitacao() As ISolicitacao Implements IDespacho.Solicitacao
        Get
            Return _Solicitacao
        End Get
        Set(ByVal value As ISolicitacao)
            _Solicitacao = value
        End Set
    End Property

    Public MustOverride ReadOnly Property Tipo() As TipoDeDespacho Implements IDespacho.Tipo

End Class
