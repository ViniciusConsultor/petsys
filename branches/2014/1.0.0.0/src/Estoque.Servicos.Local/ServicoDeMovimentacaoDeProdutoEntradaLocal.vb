Imports Compartilhados
Imports Estoque.Interfaces.Servicos
Imports Estoque.Interfaces.Negocio
Imports Estoque.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeMovimentacaoDeProdutoEntradaLocal
    Inherits Servico
    Implements IServicoDeMovimentacaoDeProdutoEntrada
    
    Public Sub New(ByVal Credencial As Credencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub EstornarMovimentacaoDeEntrada(Id As Long) Implements IServicoDeMovimentacaoDeProdutoEntrada.EstornarMovimentacaoDeEntrada
        Dim Mapeador As IMapeadorDeMovimentacaoDeProdutoEntrada

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMovimentacaoDeProdutoEntrada)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Estornar(Id)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub InserirMovimentacaoDeEntrada(ByVal Movimentacao As IMovimentacaoDeProdutoEntrada) Implements IServicoDeMovimentacaoDeProdutoEntrada.InserirMovimentacaoDeEntrada
        Dim Mapeador As IMapeadorDeMovimentacaoDeProdutoEntrada

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMovimentacaoDeProdutoEntrada)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Inserir(Movimentacao)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Function ObtenhaMovimentacoes(ByVal DataInicio As Date, _
                                         ByVal DataFinal As Date) As IList(Of IMovimentacaoDeProdutoEntrada) Implements IServicoDeMovimentacaoDeProdutoEntrada.ObtenhaMovimentacoes
        Dim Mapeador As IMapeadorDeMovimentacaoDeProdutoEntrada

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMovimentacaoDeProdutoEntrada)()

        Try
            Return Mapeador.ObtenhaMovimentacoes(DataInicio, DataFinal)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaMovimentacao(ID As Long) As IMovimentacaoDeProdutoEntrada Implements IServicoDeMovimentacaoDeProdutoEntrada.ObtenhaMovimentacao
        Dim Mapeador As IMapeadorDeMovimentacaoDeProdutoEntrada

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeMovimentacaoDeProdutoEntrada)()

        Try
            Return Mapeador.ObtenhaMovimentacao(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

End Class