Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados

Namespace Servicos

    Public Interface IServicoDeAutenticacao
        Inherits IServico

        Function FacaLogon(ByVal LoginInformado As String, ByVal SenhaInformada As String) As IOperador

    End Interface

End Namespace