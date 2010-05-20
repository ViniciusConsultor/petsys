﻿Imports Estoque.Interfaces.Negocio

<Serializable()> _
Public Class Produto
    Implements IProduto

    Private _CodigoDeBarras As String
    Public Property CodigoDeBarras() As String Implements IProduto.CodigoDeBarras
        Get
            Return _CodigoDeBarras
        End Get
        Set(ByVal value As String)
            _CodigoDeBarras = value
        End Set
    End Property

    Private _GrupoDeProduto As IGrupoDeProduto
    Public Property GrupoDeProduto() As IGrupoDeProduto Implements IProduto.GrupoDeProduto
        Get
            Return _GrupoDeProduto
        End Get
        Set(ByVal value As IGrupoDeProduto)
            _GrupoDeProduto = value
        End Set
    End Property

    Private _ID As Nullable(Of Long)
    Public Property ID() As Long? Implements IProduto.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

    Private _Marca As IMarcaDeProduto
    Public Property Marca() As IMarcaDeProduto Implements IProduto.Marca
        Get
            Return _Marca
        End Get
        Set(ByVal value As IMarcaDeProduto)
            _Marca = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome() As String Implements IProduto.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Private _Observacoes As String
    Public Property Observacoes() As String Implements IProduto.Observacoes
        Get
            Return _Observacoes
        End Get
        Set(ByVal value As String)
            _Observacoes = value
        End Set
    End Property

    Private _PorcentagemDeLucro As Double
    Public Property PorcentagemDeLucro() As Double Implements IProduto.PorcentagemDeLucro
        Get
            Return _PorcentagemDeLucro
        End Get
        Set(ByVal value As Double)
            _PorcentagemDeLucro = value
        End Set
    End Property

    Public ReadOnly Property QuantidadeEmEstoque() As Integer Implements IProduto.QuantidadeEmEstoque
        Get

        End Get
    End Property

    Private _QuantidadeMinimaEmEstoque As Integer
    Public Property QuantidadeMinimaEmEstoque() As Integer Implements IProduto.QuantidadeMinimaEmEstoque
        Get
            Return _QuantidadeMinimaEmEstoque
        End Get
        Set(ByVal value As Integer)
            _QuantidadeMinimaEmEstoque = value
        End Set
    End Property

    Private _Unidade As String
    Public Property Unidade() As String Implements IProduto.Unidade
        Get
            Return _Unidade
        End Get
        Set(ByVal value As String)
            _Unidade = value
        End Set
    End Property

    Private _ValorDeCusto As Double
    Public Property ValorDeCusto() As Double Implements IProduto.ValorDeCusto
        Get
            Return _ValorDeCusto
        End Get
        Set(ByVal value As Double)
            _ValorDeCusto = value
        End Set
    End Property

    Private _ValorMinimo As Double
    Public Property ValorMinimo() As Double Implements IProduto.ValorMinimo
        Get
            Return _ValorMinimo
        End Get
        Set(ByVal value As Double)
            _ValorMinimo = value
        End Set
    End Property

    Private _ValorDeVenda As Double
    Public Property ValorDeVenda() As Double Implements IProduto.ValorDeVenda
        Get
            Return _ValorDeVenda
        End Get
        Set(ByVal value As Double)
            _ValorDeVenda = value
        End Set
    End Property

End Class