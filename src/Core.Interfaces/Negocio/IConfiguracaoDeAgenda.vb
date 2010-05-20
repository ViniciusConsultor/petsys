Imports Compartilhados.Interfaces.Core.Negocio

Namespace Negocio

    Public Interface IConfiguracaoDeAgenda

        Property ID() As Nullable(Of Long)
        Property Nome() As String
        Property HoraDeInicio() As DateTime
        Property HoraDeTermino() As DateTime
        Property IntervaloEntreHorarios() As DateTime
        Property PrimeiroDiaDaSemana() As DiaDaSemana
        Function ObtenhaHorariosDaAgenda() As IList(Of Date)

    End Interface

End Namespace