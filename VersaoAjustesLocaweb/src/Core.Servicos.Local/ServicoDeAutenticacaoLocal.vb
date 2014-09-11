Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados.Fabricas

Public Class ServicoDeAutenticacaoLocal
    Inherits Servico
    Implements IServicoDeAutenticacao

    Public Sub New(ByVal Credencial As ICredencial)
        MyBase.New(Credencial)
    End Sub

    Public Function FacaLogon(ByVal LoginInformado As String, _
                              ByVal SenhaInformada As String) As IOperador Implements IServicoDeAutenticacao.FacaLogon
        Dim Operador As IOperador = Nothing

        ServerUtils.setCredencial(MyBase._Credencial)
        Using ServicoDeOperador As IServicoDeOperador = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeOperador)()
            Operador = ServicoDeOperador.ObtenhaOperadorPorLogin(LoginInformado)
        End Using

        If Operador Is Nothing Then
            Throw New BussinesException("Login inválido.")
        End If

        Operador.OperadorPodeEfetuarLogon()

        Dim Senha As ISenha

        Using ServicoDeSenha As IServicoDeSenha = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeSenha)()
            Senha = ServicoDeSenha.ObtenhaSenhaDoOperador(Operador)
        End Using

        Senha.SenhaEhValida(SenhaInformada)

        Return Operador
    End Function

End Class