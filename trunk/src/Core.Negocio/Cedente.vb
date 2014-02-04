Imports Compartilhados.Interfaces.Core.Negocio

<Serializable()> _
Public Class Cedente
    Inherits PapelPessoa
    Implements ICedente

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
    End Sub

End Class
