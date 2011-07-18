Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Empresa
    Inherits PapelPessoa
    Implements IEmpresa

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

    Public Function PodeUtilizarOSistema() As Boolean Implements IEmpresa.PodeUtilizarOSistema
        Return True
    End Function

    Private _DataDaAtivacao As Date
    Public Property DataDaAtivacao() As Date Implements IEmpresa.DataDaAtivacao
        Get
            Return _DataDaAtivacao
        End Get
        Set(ByVal value As Date)
            _DataDaAtivacao = value
        End Set
    End Property

End Class
