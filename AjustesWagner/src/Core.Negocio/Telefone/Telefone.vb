Imports Compartilhados.Interfaces.Core.Negocio.Telefone
Imports Compartilhados.Interfaces.Core.Negocio

Namespace Telefone

    <Serializable()> _
    Public Class Telefone
        Implements ITelefone

        Private _DDD As Short
        Private _Numero As Long
        Private _Contato As String

        Public Property DDD() As Short Implements ITelefone.DDD
            Get
                Return _DDD
            End Get
            Set(ByVal value As Short)
                _DDD = value
            End Set
        End Property

        Public Property Numero() As Long Implements ITelefone.Numero
            Get
                Return _Numero
            End Get
            Set(ByVal value As Long)
                _Numero = value
            End Set
        End Property

        Private _Tipo As TipoDeTelefone
        Public Property Tipo() As TipoDeTelefone Implements ITelefone.Tipo
            Get
                Return _Tipo
            End Get
            Set(ByVal value As TipoDeTelefone)
                _Tipo = value
            End Set
        End Property

        Public Property Contato() As String Implements ITelefone.Contato
            Get
                Return _Contato
            End Get
            Set(ByVal value As String)
                _Contato = value
            End Set
        End Property

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Dim TelefoneAComparar As ITelefone = CType(obj, ITelefone)
            Return ((Me.DDD = TelefoneAComparar.DDD) And (Me.Numero = TelefoneAComparar.Numero) And (Me.Tipo.Equals(TelefoneAComparar.Tipo)))
        End Function

        Public Overrides Function GetHashCode() As Integer
            Dim StringChave As String

            StringChave = String.Concat(Me.DDD, Me.Numero, Me.Tipo.ID)
            Return StringChave.GetHashCode
        End Function

        Public Overrides Function ToString() As String
            Return "(" & DDD.ToString & ") - " & Numero.ToString
        End Function

    End Class

End Namespace