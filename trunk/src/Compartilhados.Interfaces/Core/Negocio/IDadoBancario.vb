Namespace Core.Negocio

    Public Interface IDadoBancario

        Property Banco() As IBanco
        Property Agencia() As IAgencia
        Property Conta() As IContaBancaria

    End Interface

End Namespace