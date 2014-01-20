Imports PetSys.Interfaces.Negocio
Imports Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Veterinario
    Inherits PapelPessoa
    Implements IVeterinario

    Private _CRMV As String
    Private _UF As UF

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

    Public Property CRMV() As String Implements IVeterinario.CRMV
        Get
            Return _CRMV
        End Get
        Set(ByVal value As String)
            _CRMV = value
        End Set
    End Property

    Public Property UF() As UF Implements IVeterinario.UF
        Get
            Return _UF
        End Get
        Set(ByVal value As UF)
            _UF = value
        End Set
    End Property

End Class