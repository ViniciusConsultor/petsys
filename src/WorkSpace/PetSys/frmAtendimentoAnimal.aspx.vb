Imports Compartilhados.Componentes.Web
Imports PetSys.Interfaces.Negocio
Imports PetSys.Interfaces.Servicos
Imports Compartilhados.Fabricas

Partial Public Class frmAtendimentoAnimal
    Inherits SuperPagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddHandler crtlAnimal1.AnimalFoiSelecionado, AddressOf AnimalFoiSelecionado

        Dim lista As New List(Of String)

        lista.Add("tESTE")

        grdAtendimentos.DataSource = lista
        grdAtendimentos.DataBind()
    End Sub

    Protected Overrides Function ObtenhaBarraDeFerramentas() As Telerik.Web.UI.RadToolBar
        Return rtbToolBar
    End Function

    Protected Overrides Function ObtenhaIdFuncao() As String
        Return "FUN.PET.003"
    End Function

    Private Sub AnimalFoiSelecionado(ByVal Animal As IAnimal)
        Dim Atendimentos As IList(Of IAtendimentoDoAnimal)

        Using Servico As IServicoDeAtendimentoDoAnimal = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeAtendimentoDoAnimal)()
            Atendimentos = Servico.ObtenhaAtendimentos(Animal)
        End Using

        ExibaAtendimentosHistoricos(Atendimentos)
    End Sub

    Private Sub ExibaAtendimentosHistoricos(ByVal Atendimentos As IList(Of IAtendimentoDoAnimal))

    End Sub

End Class