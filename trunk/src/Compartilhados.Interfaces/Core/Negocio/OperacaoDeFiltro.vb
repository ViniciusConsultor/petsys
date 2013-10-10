Namespace Core.Negocio

    <Serializable()> _
    Public Class OperacaoDeFiltro

        Private _ID As Byte
        Private _Descricao As String

        Public Shared ComecaCom As OperacaoDeFiltro = New OperacaoDeFiltro(1, "Começa com")
        Public Shared EmQualquerParte As OperacaoDeFiltro = New OperacaoDeFiltro(2, "Em qualquer parte")
        Public Shared IgualA As OperacaoDeFiltro = New OperacaoDeFiltro(3, "Igual a")
        Public Shared MaiorIgualA As OperacaoDeFiltro = New OperacaoDeFiltro(4, "Maior igual a")
        Public Shared MaiorQue As OperacaoDeFiltro = New OperacaoDeFiltro(5, "Maior que")
        Public Shared MenorIgualA As OperacaoDeFiltro = New OperacaoDeFiltro(6, "Menor igual a")
        Public Shared MenorQue As OperacaoDeFiltro = New OperacaoDeFiltro(7, "Menor que")

        Private Shared Lista As OperacaoDeFiltro() = {ComecaCom, _
                                                      EmQualquerParte, _
                                                      IgualA, _
                                                      MaiorIgualA, _
                                                      MaiorQue, _
                                                      MenorIgualA, _
                                                      MenorQue}
        Private Sub New(ByVal ID As Byte, _
                        ByVal Descricao As String)
            _ID = ID
            _Descricao = Descricao
        End Sub

        Public ReadOnly Property ID() As Byte
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Descricao() As String
            Get
                Return _Descricao
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As Byte) As OperacaoDeFiltro
            For Each Operacao As OperacaoDeFiltro In Lista
                If Operacao.ID = ID Then
                    Return Operacao
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of OperacaoDeFiltro)
            Return New List(Of OperacaoDeFiltro)(Lista)
        End Function

    End Class

End Namespace
