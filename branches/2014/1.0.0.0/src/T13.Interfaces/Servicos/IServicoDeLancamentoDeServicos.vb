Imports T13.Interfaces.Negocio

Namespace Servicos

    Public Interface IServicoDeLancamentoDeServicos
        Inherits Compartilhados.IServico

        Sub Inserir(ByVal Lancamento As ILacamentoDeServicosPrestados)
        Sub Modificar(ByVal Lancamento As ILacamentoDeServicosPrestados)
        Sub Excluir(ByVal ID As Long)
        Function ObtenhaLancamentosTardio(ByVal IDCliente As Long) As IList(Of ILacamentoDeServicosPrestados)
        Function ObtenhaLancamento(ByVal ID As Long) As ILacamentoDeServicosPrestados
        Function ObtenhaProximoNumeroDisponivel() As Long
        Function ObtenhaLancamentosTardio(ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ILacamentoDeServicosPrestados)

    End Interface

End Namespace