Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeSenha

        Function ObtenhaSenhaDoOperador(ByVal IDOperador As Long) As ISenha
        Sub Altere(ByVal IDOperador As Long, ByVal Senha As ISenha)
        Sub Insira(ByVal IDOperador As Long, ByVal Senha As ISenha)
        Sub Remova(ByVal IDOperador As Long)
        Function RegistreDefinicaoDeNovaSenha(ByVal Operador As IOperador) As Long
        Function ObtenhaIDOperadorParaRedifinirSenha(IDRedefinicaoDeSenha As Long) As Nullable(Of Long)
        Sub ExcluaRefinicaoDeSenha(IDRedefinicaoDeSenha As Long)

    End Interface

End Namespace