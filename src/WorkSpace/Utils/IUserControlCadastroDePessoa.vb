Imports Compartilhados.Interfaces.Core.Negocio

Namespace Utils

    Public Interface IUserControlCadastroDePessoa

        Sub ExibaTelaInicial()
        Sub ExibaTelaNovo()
        Sub ExibaTelaConsultar(pessoa As IPessoa)
        Sub ExibaTelaModificar()
        Sub ExibaTelaExcluir()
        Function Salve(nome As String) As Boolean
        Sub Exclua()

    End Interface

End Namespace