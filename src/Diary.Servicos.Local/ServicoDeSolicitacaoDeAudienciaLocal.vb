Imports Compartilhados
Imports Diary.Interfaces.Servicos
Imports Diary.Interfaces.Negocio
Imports Compartilhados.Fabricas

Public Class ServicoDeSolicitacaoDeAudienciaLocal
    Inherits Servico
    Implements IServicoDeSolicitacaoDeAudiencia

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia) Implements IServicoDeSolicitacaoDeAudiencia.Inserir

    End Sub

    Public Sub Modificar(ByVal SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia) Implements IServicoDeSolicitacaoDeAudiencia.Modificar

    End Sub

    Public Function ObtenhaSolicitacaoDeAudiencia(ByVal ID As Long) As ISolicitacaoDeAudiencia Implements IServicoDeSolicitacaoDeAudiencia.ObtenhaSolicitacaoDeAudiencia
        Return Nothing
    End Function

    Public Function ObtenhaSolicitacoesDeAudiencia(ByVal TrazApenasAtivas As Boolean) As IList(Of ISolicitacaoDeAudiencia) Implements IServicoDeSolicitacaoDeAudiencia.ObtenhaSolicitacoesDeAudiencia
        Dim Contato As IContato
        Dim SolicitacaoDeAudiencia As ISolicitacaoDeAudiencia
        Dim Solicitacoes As IList(Of ISolicitacaoDeAudiencia) = New List(Of ISolicitacaoDeAudiencia)

        Using Servico As IServicoDeContato = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeContato)()
            Contato = Servico.Obtenha(30001)
        End Using

        SolicitacaoDeAudiencia = FabricaGenerica.GetInstancia.CrieObjeto(Of ISolicitacaoDeAudiencia)()
        SolicitacaoDeAudiencia.Assunto = "ASSUNTO"
        SolicitacaoDeAudiencia.Ativa = True
        SolicitacaoDeAudiencia.Contato = Contato
        SolicitacaoDeAudiencia.DataDaSolicitacao = Now
        SolicitacaoDeAudiencia.Descricao = "DESCRICAO"
        SolicitacaoDeAudiencia.ID = 1234
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)

        Solicitacoes.Add(SolicitacaoDeAudiencia)
        Solicitacoes.Add(SolicitacaoDeAudiencia)

        Return Solicitacoes
    End Function

    Public Function ObtenhaSolicitacoesDeAudiencia(ByVal TrazApenasAtivas As Boolean, ByVal DataInicio As Date, ByVal DataFim As Date) As System.Collections.Generic.IList(Of Interfaces.Negocio.ISolicitacaoDeAudiencia) Implements Interfaces.Servicos.IServicoDeSolicitacaoDeAudiencia.ObtenhaSolicitacoesDeAudiencia
        Return Nothing
    End Function

    Public Sub Remover(ByVal ID As Long) Implements Interfaces.Servicos.IServicoDeSolicitacaoDeAudiencia.Remover

    End Sub
End Class
