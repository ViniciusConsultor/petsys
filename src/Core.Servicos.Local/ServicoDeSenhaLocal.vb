Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Fabricas

Public Class ServicoDeSenhaLocal
    Inherits Servico
    Implements IServicoDeSenha

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function ObtenhaSenhaDoOperador(ByVal Operador As IOperador) As ISenha Implements IServicoDeSenha.ObtenhaSenhaDoOperador
        Dim Senha As ISenha = Nothing
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)

        Try
            Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()
            Senha = Mapeador.ObtenhaSenhaDoOperador(Operador.Pessoa.ID.Value)

            If Senha Is Nothing Then
                Throw New BussinesException("Não existe senha cadastrada para este operador.")
            End If

            Return Senha
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Function

    Public Sub Altere(ByVal IDOperador As Long, _
                      ByVal SenhaAntigaInformada As ISenha, _
                      ByVal NovaSenha As ISenha, _
                      ByVal ConfirmacaoNovaSenha As ISenha) Implements IServicoDeSenha.Altere
        Dim SenhaAntigaGravada As ISenha = Nothing
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        Try
            SenhaAntigaGravada = Mapeador.ObtenhaSenhaDoOperador(IDOperador)
            ValidaRegras(SenhaAntigaGravada, SenhaAntigaInformada, NovaSenha, ConfirmacaoNovaSenha)
            Mapeador.Altere(IDOperador, NovaSenha)
        Finally
            ServerUtils.libereRecursos()
        End Try

    End Sub

    Public Sub Altere(ByVal IDOperador As Long, _
                      ByVal NovaSenha As ISenha, _
                      ByVal ConfirmacaoNovaSenha As ISenha) Implements IServicoDeSenha.Altere
        Dim SenhaAntigaGravada As ISenha = Nothing
        Dim Mapeador As IMapeadorDeSenha

        ServerUtils.setCredencial(MyBase._Credencial)
        Mapeador = FabricaGenerica.GetInstancia.CrieObjeto(Of IMapeadorDeSenha)()

        Try
            SenhaAntigaGravada = Mapeador.ObtenhaSenhaDoOperador(IDOperador)
            ValidaRegras(SenhaAntigaGravada, SenhaAntigaGravada, NovaSenha, ConfirmacaoNovaSenha)
            Mapeador.Altere(IDOperador, NovaSenha)
        Finally
            ServerUtils.libereRecursos()
        End Try
    End Sub

    Private Sub ValidaRegras(ByVal SenhaAntigaGravada As ISenha, _
                             ByVal SenhaAntigaInformada As ISenha, _
                             ByVal NovaSenha As ISenha, _
                             ByVal ConfirmacaoNovaSenha As ISenha)
        If Not SenhaAntigaGravada.ToString.Equals(SenhaAntigaInformada.ToString) Then
            Throw New BussinesException("A senha atual informada não está correta.")
        End If

        If Not NovaSenha.ToString.Equals(ConfirmacaoNovaSenha.ToString) Then
            Throw New BussinesException("A confirmação da nova senha não confere com a nova senha.")
        End If

        If SenhaAntigaGravada.ToString.Equals(NovaSenha.ToString) Then
            Throw New BussinesException("A nova senha não deve ser igual a senha antiga.")
        End If
    End Sub

End Class