Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados
Imports Compartilhados.Fabricas
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces

Public Class ServicoDeGeracaoDeIDLocal
    Inherits Servico
    Implements IServicoDeGeracaoDeID

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub ArmazeneNumeroHigh(ByVal NumeroHigh As Integer) Implements IServicoDeGeracaoDeID.ArmazeneNumeroHigh
        Dim Mapeador As IMapeadorDeGeracaoDeID

        ServerUtils.setCredencial(MyBase._Credencial)

        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGeracaoDeID)()
        Mapeador.ArmazeneNumeroHigh(NumeroHigh)
    End Sub

    Public Function ObtenhaNumeroHigh() As Integer Implements IServicoDeGeracaoDeID.ObtenhaNumeroHigh
        Dim Mapeador As IMapeadorDeGeracaoDeID

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeGeracaoDeID)()
        Return Mapeador.ObtenhaNumeroHigh()
    End Function

End Class