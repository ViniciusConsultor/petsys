Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Negocio
Imports Compartilhados
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas

<Serializable()> _
Public Class Operador
    Inherits PapelPessoa
    Implements IOperador
    
    Private _Status As StatusDoOperador
    Private _Login As String
    Private _Grupos As IList(Of IGrupo)

    Public Sub New(ByVal Pessoa As IPessoa)
        MyBase.New(Pessoa)
        _Grupos = New List(Of IGrupo)
    End Sub

    Public Property Status() As StatusDoOperador Implements IOperador.Status
        Get
            Return _Status
        End Get
        Set(ByVal value As StatusDoOperador)
            _Status = value
        End Set
    End Property

    Public Sub AdicioneGrupo(ByVal Grupo As IGrupo) Implements IOperador.AdicioneGrupo
        _Grupos.Add(Grupo)
    End Sub

    Public Sub AdicioneGrupos(ByVal Grupos As IList(Of IGrupo)) Implements IOperador.AdicioneGrupos
        CType(_Grupos, List(Of IGrupo)).AddRange(Grupos)
    End Sub

    Public Property Login() As String Implements IOperador.Login
        Get
            Return _Login
        End Get
        Set(ByVal value As String)
            _Login = value
        End Set
    End Property

    Public Function ObtenhaGrupos() As IList(Of IGrupo) Implements IOperador.ObtenhaGrupos
        Return _Grupos
    End Function

    Private Sub EstaBloqueado()
        If Status.Equals(StatusDoOperador.Bloqueado) Then
            Throw New BussinesException("O operador encontra-se bloqueado e não poderá acessar o sistema.")
        End If
    End Sub

    Private Sub EstaInativo()
        If Status.Equals(StatusDoOperador.Inativo) Then
            Throw New BussinesException("O operador encontra-se inativo e não poderá acessar o sistema.")
        End If
    End Sub

    Public Sub OperadorPodeEfetuarLogon() Implements IOperador.OperadorPodeEfetuarLogon
        EstaInativo()
        EstaBloqueado()
    End Sub

    Public Function ObtenhaEmpresasVisiveis() As IList(Of IEmpresa) Implements IOperador.ObtenhaEmpresasVisiveis
        Using Servico As IServicoDeVisibilidadePorEmpresa = FabricaGenerica.GetInstancia().CrieObjeto(Of IServicoDeVisibilidadePorEmpresa)()
            Return Servico.Obtenha(Me.Pessoa.ID.Value)
        End Using
    End Function

End Class