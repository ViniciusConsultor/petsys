Imports T13.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class LacamentoDeServicosPrestados
    Implements ILacamentoDeServicosPrestados

    Private _ItensDeLancamento As IList(Of IItemDeLancamento)
    Private _Aliquota As String
    Private _DataDeLancamento As Date
    Private _NaturezaDaOperacao As String
    Private _Observacoes As String
    Private _Cliente As ICliente
    Private _ID As Nullable(Of Long)
    Private _Numero As Nullable(Of Long)

    Public Sub New()
        _ItensDeLancamento = New List(Of IItemDeLancamento)
        _DataDeLancamento = Now
    End Sub

    Public Sub AdicionaItemDeLancamento(ByVal Item As IItemDeLancamento) Implements ILacamentoDeServicosPrestados.AdicionaItemDeLancamento
        _ItensDeLancamento.Add(Item)
    End Sub

    Public Property Aliquota() As String Implements ILacamentoDeServicosPrestados.Aliquota
        Get
            If _Aliquota.Contains("%") Then
                Return _Aliquota.Remove(_Aliquota.IndexOf("%"), 1)
            End If
            Return _Aliquota
        End Get
        Set(ByVal value As String)
            _Aliquota = value
        End Set
    End Property

    Public Property DataDeLancamento() As Date Implements ILacamentoDeServicosPrestados.DataDeLancamento
        Get
            Return _DataDeLancamento
        End Get
        Set(ByVal value As Date)
            _DataDeLancamento = value
        End Set
    End Property

    Public Property NaturezaDaOperacao() As String Implements ILacamentoDeServicosPrestados.NaturezaDaOperacao
        Get
            Return _NaturezaDaOperacao
        End Get
        Set(ByVal value As String)
            _NaturezaDaOperacao = value
        End Set
    End Property

    Public Property Observacoes() As String Implements ILacamentoDeServicosPrestados.Observacoes
        Get
            Return _Observacoes
        End Get
        Set(ByVal value As String)
            _Observacoes = value
        End Set
    End Property

    Public Function ObtenhaItensDeLancamento() As IList(Of IItemDeLancamento) Implements ILacamentoDeServicosPrestados.ObtenhaItensDeLancamento
        Return _ItensDeLancamento
    End Function

    Public Property Cliente() As ICliente Implements ILacamentoDeServicosPrestados.Cliente
        Get
            Return _Cliente
        End Get
        Set(ByVal value As ICliente)
            _Cliente = value
        End Set
    End Property

    Public Property ID() As Long? Implements ILacamentoDeServicosPrestados.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Public Property Numero() As Long? Implements ILacamentoDeServicosPrestados.Numero
        Get
            Return _Numero
        End Get
        Set(ByVal value As Long?)
            _Numero = value
        End Set
    End Property

    Public Function ObtenhaTotalDosItensLancados() As Double Implements ILacamentoDeServicosPrestados.ObtenhaTotalDosItensLancados
        Dim Total As Double = 0

        For Each Item As IItemDeLancamento In ObtenhaItensDeLancamento()

            If Item.Servico.CaracterizaDesconto Then
                Total -= Item.Total
            Else
                Total += Item.Total
            End If
        Next

        Return Total
    End Function

    Public Function ObtenhaValorDoISSQN() As Double Implements ILacamentoDeServicosPrestados.ObtenhaValorDoISSQN
        If Not String.IsNullOrEmpty(Aliquota) AndAlso Not ObtenhaTotalDosItensLancados() = 0 Then
            Return (CDbl(Aliquota) / 100) * ObtenhaTotalDosItensLancados()
        End If

        Return 0
    End Function

    Public Function Clone() As Object Implements System.ICloneable.Clone
        Dim InstanciaClone As ILacamentoDeServicosPrestados

        InstanciaClone = New LacamentoDeServicosPrestados()
        InstanciaClone.Aliquota = Me._Aliquota
        InstanciaClone.Cliente = Me.Cliente
        InstanciaClone.NaturezaDaOperacao = Me.NaturezaDaOperacao
        InstanciaClone.Observacoes = Me.Observacoes

        For Each Item As IItemDeLancamento In Me.ObtenhaItensDeLancamento
            InstanciaClone.AdicionaItemDeLancamento(Item)
        Next

        Return InstanciaClone
    End Function
End Class