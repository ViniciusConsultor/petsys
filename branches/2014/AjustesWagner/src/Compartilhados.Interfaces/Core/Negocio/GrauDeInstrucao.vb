Namespace Core.Negocio

    <Serializable()> _
    Public Class GrauDeInstrucao

        Private _ID As Short
        Private _Descricao As String

        Private Sub New(ByVal ID As Short, _
                        ByVal Nome As String)
            _ID = ID
            _Descricao = Nome
        End Sub

        Public ReadOnly Property ID() As Short
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Descricao() As String
            Get
                Return _Descricao
            End Get
        End Property

        Public Shared Analfabeto As GrauDeInstrucao = New GrauDeInstrucao(1, "Analfabeto")
        Public Shared QuartaSerieIncompleta As GrauDeInstrucao = New GrauDeInstrucao(2, "4ª série incompleta do ensino fundamental")
        Public Shared QuartaSerieCompleta As GrauDeInstrucao = New GrauDeInstrucao(3, "4ª série completa do ensino fundamental")
        Public Shared QuintaAOitavaEnsinoFundamental As GrauDeInstrucao = New GrauDeInstrucao(4, "Da 5ª à 8ª série do ensino fundamental")
        Public Shared EnsinoFundamentalCompleto As GrauDeInstrucao = New GrauDeInstrucao(5, "Ensino Fundamental Completo")
        Public Shared EnsinoMedioIncompleto As GrauDeInstrucao = New GrauDeInstrucao(6, "Ensino Médio Incompleto")
        Public Shared EnsinoMedioCompleto As GrauDeInstrucao = New GrauDeInstrucao(7, "Ensino Médio Completo")
        Public Shared EducacaoSuperiorIncompleta As GrauDeInstrucao = New GrauDeInstrucao(8, "Educação Superior Incompleta")
        Public Shared EducacaoSuperiorCompleta As GrauDeInstrucao = New GrauDeInstrucao(9, "Educação Superior Completa")
        Public Shared PosGraduacao As GrauDeInstrucao = New GrauDeInstrucao(10, "Pós-Graduação")
        Public Shared Doutorado As GrauDeInstrucao = New GrauDeInstrucao(11, "Doutorado")
        Public Shared SegundoGrauTecnicoIncompleto As GrauDeInstrucao = New GrauDeInstrucao(12, "Segundo Grau Técnico Incompleto")
        Public Shared SegundoGrauTecnicoCompleto As GrauDeInstrucao = New GrauDeInstrucao(13, "Segundo Grau Técnico Completo")
        Public Shared Mestrado As GrauDeInstrucao = New GrauDeInstrucao(14, "Mestrado")

        Private Shared Lista() As GrauDeInstrucao = {Analfabeto, QuartaSerieIncompleta, QuartaSerieCompleta, QuintaAOitavaEnsinoFundamental, _
                                                     EnsinoFundamentalCompleto, EnsinoMedioIncompleto, EnsinoMedioCompleto, EducacaoSuperiorIncompleta, _
                                                     EducacaoSuperiorCompleta, PosGraduacao, Doutorado, SegundoGrauTecnicoIncompleto, SegundoGrauTecnicoCompleto, Mestrado}

        Public Shared Function Obtenha(ByVal ID As Short) As GrauDeInstrucao
            For Each Item As GrauDeInstrucao In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of GrauDeInstrucao)
            Return New List(Of GrauDeInstrucao)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, GrauDeInstrucao).ID = Me.ID
        End Function

    End Class

End Namespace