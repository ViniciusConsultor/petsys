Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeAgenda
        Inherits IServico

        Sub Agende(ByVal Proprietario As IProprietarioDeAgenda, _
                   ByVal Assunto As String, _
                   ByVal Descricao As String, _
                   ByVal Local As String, _
                   ByVal DataEHoraDeInicio As Date, _
                   ByVal DataEHoraDeTermino As Date)

        Sub RemovaCompromisso(ByVal ID As Long)

        Sub InsiraProprietarioDeAgenda(ByVal Proprietario As IProprietarioDeAgenda)
        Sub RemovaProprietarioDeAgenda(ByVal IDProprietario As IProprietarioDeAgenda)

    End Interface

End Namespace