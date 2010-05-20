Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeMenu
        Inherits IServico

        Function ObtenhaMenu() As IMenuComposto

    End Interface

End Namespace