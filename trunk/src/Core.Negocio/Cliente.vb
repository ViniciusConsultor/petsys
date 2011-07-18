Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Cliente
    Inherits PapelPessoa
    Implements ICliente

    Private _DataDoCadastro As Nullable(Of Date)

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

    Public Property DataDoCadastro() As Date? Implements ICliente.DataDoCadastro
        Get
            Return _DataDoCadastro
        End Get
        Set(ByVal value As Date?)
            _DataDoCadastro = value
        End Set
    End Property

    Private _FaixaSalarial As Nullable(Of Double)
    Public Property FaixaSalarial() As Double? Implements ICliente.FaixaSalarial
        Get
            Return _FaixaSalarial
        End Get
        Set(ByVal value As Double?)
            _FaixaSalarial = value
        End Set
    End Property

    Private _InformacoesAdicionais As String
    Public Property InformacoesAdicionais() As String Implements ICliente.InformacoesAdicionais
        Get
            Return _InformacoesAdicionais
        End Get
        Set(ByVal value As String)
            _InformacoesAdicionais = value
        End Set
    End Property

    Private _PorcentagemDeDescontoAutomatico As Nullable(Of Double)
    Public Property PorcentagemDeDescontoAutomatico() As Double? Implements ICliente.PorcentagemDeDescontoAutomatico
        Get
            Return _PorcentagemDeDescontoAutomatico
        End Get
        Set(ByVal value As Double?)
            _PorcentagemDeDescontoAutomatico = value
        End Set
    End Property

    Private _SaldoParaCompras As Nullable(Of Double)
    Public Property SaldoParaCompras() As Double? Implements ICliente.SaldoParaCompras
        Get
            Return _SaldoParaCompras
        End Get
        Set(ByVal value As Double?)
            _SaldoParaCompras = value
        End Set
    End Property

    Private _ValorMaximoParaCompras As Nullable(Of Double)
    Public Property ValorMaximoParaCompras() As Double? Implements ICliente.ValorMaximoParaCompras
        Get
            Return _ValorMaximoParaCompras
        End Get
        Set(ByVal value As Double?)
            _ValorMaximoParaCompras = value
        End Set
    End Property

End Class