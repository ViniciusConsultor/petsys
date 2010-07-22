Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeSenha

        Function ObtenhaSenhaDoOperador(ByVal IDOperador As Long) As ISenha
        Sub Altere(ByVal IDOperador As Long, ByVal Senha As ISenha)
        Sub Insira(ByVal IDOperador As Long, ByVal Senha As ISenha)
        Sub Remova(ByVal IDOperador As Long)

    End Interface

End Namespace