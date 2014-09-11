Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Core.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeOperador
        Inherits IServico

        Function ObtenhaOperadorPorLogin(ByVal Login As String) As IOperador
        Function ObtenhaOperador(ByVal Pessoa As IPessoa) As IOperador
        Sub Inserir(ByVal Operador As IOperador, ByVal Senha As ISenha)
        Sub Modificar(ByVal Operador As IOperador)
        Sub Remover(ByVal ID As Long)
        Function ObtenhaOperadores(ByVal Nome As String, ByVal Quantidade As Integer) As IList(Of IOperador)
        
    End Interface

End Namespace