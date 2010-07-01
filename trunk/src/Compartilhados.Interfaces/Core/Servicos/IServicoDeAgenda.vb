Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeAgenda
        Inherits IServico

        Sub Agende(ByVal Pessoa As IPessoa, _
                   ByVal Assunto As String, _
                   ByVal Descricao As String, _
                   ByVal Local As String, _
                   ByVal DataEHoraDeInicio As Date, _
                   ByVal DataEHoraDeTermino As Date)

    End Interface

End Namespace