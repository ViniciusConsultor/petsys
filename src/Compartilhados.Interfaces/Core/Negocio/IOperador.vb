Namespace Core.Negocio

    Public Interface IOperador
        Inherits IPapelPessoa

        Property Status() As StatusDoOperador
        Property Login() As String
        Function ObtenhaGrupos() As IList(Of IGrupo)
        Sub AdicioneGrupo(ByVal Grupo As IGrupo)
        Sub AdicioneGrupos(ByVal Grupos As IList(Of IGrupo))
        Sub OperadorPodeEfetuarLogon()
        Function ObtenhaEmpresasVisiveis() As IList(Of IEmpresa)
    End Interface

End Namespace