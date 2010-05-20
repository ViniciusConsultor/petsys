Imports PetSys.Interfaces.Negocio
Imports Core.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class ProprietarioDeAnimal
    Inherits PapelPessoa
    Implements IProprietarioDeAnimal

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

End Class