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

    Private _Solicitacao As ISolicitacao
    Public Property Solicitacao() As ISolicitacao Implements IDespacho.Solicitacao
        Get
            Return _Solicitacao
        End Get
        Set(ByVal value As ISolicitacao)
            _Solicitacao = value
        End Set
    End Property

    Private _Tipo As TipoDeDespacho
    Public Property Tipo() As TipoDeDespacho Implements IDespacho.Tipo
        Get
            Return _Tipo
        End Get
        Set(ByVal value As TipoDeDespacho)
            _Tipo = value
        End Set
    End Property

    Private _DataDoDespaco As Date
    Public Property DataDoDespacho() As Date Implements IDespacho.DataDoDespacho
        Get
            Return _DataDoDespaco
        End Get
        Set(ByVal value As Date)
            _DataDoDespaco = value
        End Set
    End Property

    Public MustOverride ReadOnly Property TipoDestino() As TipoDestinoDespacho Implements IDespacho.TipoDestino

    Private _Alvo As IPessoaFisica
    Public Property Alvo() As IPessoaFisica Implements IDespacho.Alvo
        Get
            Return _Alvo
        End Get
        Set(ByVal value As IPessoaFisica)
            _Alvo = value
        End Set
    End Property

    Private _Solicitante As IPessoaFisica
    Public Property Solicitante() As IPessoaFisica Implements IDespacho.Solicitante
        Get
            Return _Solicitante
        End Get
        Set(ByVal value As IPessoaFisica)
            _Solicitante = value
        End Set
    End Property
End Class