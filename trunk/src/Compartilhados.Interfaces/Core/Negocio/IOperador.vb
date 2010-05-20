Namespace Core.Negocio

    Public Interface IOperador
        Inherits IPapelPessoa

        Property Status() As StatusDoOperador
        Property Login() As String
        Function ObtenhaGrupos() As IList(Of IGrupo)
        Sub AdicioneGrupo(ByVal Grupo As IGrupo)
        Sub AdicioneGrupos(ByVal Grupos As IList(Of IGrupo))
        Sub OperadorPodeEfetuarLogon()

    End Interface

End Namespace