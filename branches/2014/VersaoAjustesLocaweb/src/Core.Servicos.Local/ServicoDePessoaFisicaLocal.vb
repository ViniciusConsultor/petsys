Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.Documento

Public Class ServicoDePessoaFisicaLocal
    Inherits Servico
    Implements IServicoDePessoaFisica

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Sub Inserir(ByVal Pessoa As IPessoaFisica) Implements IServicoDePessoaFisica.Inserir
        Dim Mapeador As IMapeadorDePessoaFisica

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePessoaFisica)()

        ServerUtils.BeginTransaction()

        Try
            ExecuteRegrasDeNegocio(Pessoa)
            Mapeador.Inserir(Pessoa)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Public Sub Modificar(ByVal Pessoa As IPessoaFisica) Implements IServicoDePessoaFisica.Modificar
        Dim Mapeador As IMapeadorDePessoaFisica

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePessoaFisica)()

        ServerUtils.BeginTransaction()

        Try
            ExecuteRegrasDeNegocio(Pessoa)
            Mapeador.Atualizar(Pessoa)
            ServerUtils.CommitTransaction()
        Catch
            ServerUtils.RollbackTransaction()
            Throw
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Private Sub ExecuteRegrasDeNegocio(pessoa As IPessoaFisica)

        For Each Documento As IDocumento In pessoa.ObtenhaDocumentos()
            If Not Documento.EhValido() Then
                Throw New BussinesException("O número do documento " + Documento.Tipo.Descricao + " é inválido.")
            End If
        Next

    End Sub

    Public Function ObtenhaPessoa(ByVal ID As Long) As IPessoaFisica Implements IServicoDePessoaFisica.ObtenhaPessoa
        Dim Mapeador As IMapeadorDePessoaFisica

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePessoaFisica)()

        Try
            Return Mapeador.Obtenha(ID)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Function ObtenhaPessoasPorNomeComoFiltro(ByVal Nome As String, _
                                                    ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IPessoaFisica) Implements IServicoDePessoaFisica.ObtenhaPessoasPorNomeComoFiltro
        Dim Mapeador As IMapeadorDePessoaFisica

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePessoaFisica)()

        Try
            Return Mapeador.ObtenhaPessoasPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros, 0)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IServicoDePessoaFisica.Remover
        Dim Mapeador As IMapeadorDePessoaFisica

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDePessoaFisica)()

        ServerUtils.BeginTransaction()

        Try
            Mapeador.Remover(ID)
            ServerUtils.CommitTransaction()
        Catch ex As Exception
            ServerUtils.RollbackTransaction()
            Throw New BussinesException("A pessoa não pode ser removida pois a mesma está sendo utilizada em outras funcionalidades do sistema.")
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

End Class