Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Core.Servicos.Local
Imports Core.Interfaces.Negocio
Imports Compartilhados.Interfaces.Core.Negocio

Public Class ServicoDeSenhaRemoting
    Inherits ServicoRemoto
    Implements IServicoDeSenha
    
    Private _ServicoLocal As ServicoDeSenhaLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeSenhaLocal(Credencial)
    End Sub

    Public Sub Altere(ByVal IDOperador As Long, ByVal NovaSenha As ISenha, ByVal ConfirmacaoNovaSenha As ISenha) Implements IServicoDeSenha.Altere
        _ServicoLocal.Altere(IDOperador, NovaSenha, ConfirmacaoNovaSenha)
    End Sub

    Public Sub Altere(ByVal IDOperador As Long, ByVal SenhaAntigaInformada As ISenha, ByVal NovaSenha As ISenha, ByVal ConfirmacaoNovaSenha As ISenha) Implements IServicoDeSenha.Altere
        _ServicoLocal.Altere(IDOperador, SenhaAntigaInformada, NovaSenha)
    End Sub

    Public Function ObtenhaSenhaDoOperador(ByVal Operador As IOperador) As ISenha Implements IServicoDeSenha.ObtenhaSenhaDoOperador
        Return _ServicoLocal.ObtenhaSenhaDoOperador(Operador)
    End Function

    Public Sub RegistreDefinicaoDeNovaSenha(Operador As IOperador) Implements IServicoDeSenha.RegistreDefinicaoDeNovaSenha
        _ServicoLocal.RegistreDefinicaoDeNovaSenha(Operador)
    End Sub

End Class