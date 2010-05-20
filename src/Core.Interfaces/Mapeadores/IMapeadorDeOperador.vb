Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeOperador

        Function ObtenhaOperadorPorLogin(ByVal Login As String) As IOperador
        Function ObtenhaOperador(ByVal Pessoa As IPessoa) As IOperador
        Sub Inserir(ByRef Operador As IOperador)
        Sub Modificar(ByVal Operador As IOperador)
        Sub Remover(ByVal ID As Long)

    End Interface

End Namespace