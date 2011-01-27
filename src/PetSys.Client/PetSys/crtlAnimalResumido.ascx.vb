Imports PetSys.Interfaces.Negocio

Partial Public Class crtlAnimalResumido
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub ApresentaDadosResumidosDoAnimal(ByVal Animal As IAnimal)
        lblAnimal.Text = Animal.Nome
        If Animal.DataDeNascimento.HasValue Then lblDataDeNascimento.Text = Animal.DataDeNascimento.Value.ToString("dd/MM/yyyy")
        If Not Animal.Especie Is Nothing Then lblEspecie.Text = Animal.Especie.Descricao
        lblIdade.Text = Animal.Idade
        lblRaca.Text = Animal.Raca
        lblSexo.Text = Animal.Sexo.Descricao
    End Sub

End Class