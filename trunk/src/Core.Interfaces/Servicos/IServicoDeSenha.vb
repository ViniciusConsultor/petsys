﻿Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Core.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeSenha
        Inherits IServico

        Function ObtenhaSenhaDoOperador(ByVal Operador As IOperador) As ISenha
        Sub Altere(ByVal IDOperador As Long, _
                   ByVal SenhaAntigaInformada As ISenha, _
                   ByVal NovaSenha As ISenha, _
                   ByVal ConfirmacaoNovaSenha As ISenha)
        Sub Altere(ByVal IDOperador As Long, _
                   ByVal NovaSenha As ISenha, _
                   ByVal ConfirmacaoNovaSenha As ISenha)

        Sub RegistreDefinicaoDeNovaSenha(ByVal Operador As IOperador, ByVal Link As String)

    End Interface

End Namespace