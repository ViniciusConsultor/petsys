Imports Diary.Interfaces.Negocio
Imports Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Contato
    Inherits PapelPessoa
    Implements IContato

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

    Private _Cargo As String
    Public Property Cargo() As String Implements IContato.Cargo
        Get
            Return _Cargo
        End Get
        Set(ByVal value As String)
            _Cargo = value
        End Set
    End Property

    Private _Observacoes As String
    Public Property Observacoes() As String Implements IContato.Observacoes
        Get
            Return _Observacoes
        End Get
        Set(ByVal value As String)
            _Observacoes = value
        End Set
    End Property

End Class