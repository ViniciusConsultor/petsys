Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class DadoBancario
    Implements IDadoBancario

    Private _Agencia As IAgencia
    Public Property Agencia() As IAgencia Implements IDadoBancario.Agencia
        Get
            Return _Agencia
        End Get
        Set(ByVal value As IAgencia)
            _Agencia = value
        End Set
    End Property

    Private _Conta As IContaBancaria
    Public Property Conta() As IContaBancaria Implements IDadoBancario.Conta
        Get
            Return _Conta
        End Get
        Set(ByVal value As IContaBancaria)
            _Conta = value
        End Set
    End Property

End Class
