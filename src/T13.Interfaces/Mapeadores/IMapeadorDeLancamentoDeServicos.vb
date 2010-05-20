Imports T13.Interfaces.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeLancamentoDeServicos

        Function ObtenhaLancamentosTardio(ByVal IDCliente As Long) As IList(Of ILacamentoDeServicosPrestados)
        Sub Inserir(ByVal Lancamento As ILacamentoDeServicosPrestados)
        Sub Modificar(ByVal Lancamento As ILacamentoDeServicosPrestados)
        Sub Excluir(ByVal ID As Long)
        Function ObtenhaLancamento(ByVal ID As Long) As ILacamentoDeServicosPrestados
        Function ObtenhaProximoNumeroDisponivel() As Long
        Function NumeroEstaSendoUtilizando(ByVal Numero As Long) As Boolean
        Function ObtenhaLancamentosTardio(ByVal DataInicio As Date, ByVal DataFim As Date) As IList(Of ILacamentoDeServicosPrestados)
    End Interface

End Namespace