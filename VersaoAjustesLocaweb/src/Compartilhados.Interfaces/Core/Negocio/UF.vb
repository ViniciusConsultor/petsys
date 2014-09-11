Namespace Core.Negocio

    <Serializable()> _
    Public Class UF

        Private _ID As Short
        Private _Nome As String
        Private _Sigla As String

        Public Shared AC As UF = New UF(1S, "Acre", "AC")
        Public Shared AM As UF = New UF(2S, "Amazonas", "AM")
        Public Shared AP As UF = New UF(3S, "Amapá", "AP")
        Public Shared BA As UF = New UF(4S, "Bahia", "BA")
        Public Shared CE As UF = New UF(5S, "Ceará", "CE")
        Public Shared DF As UF = New UF(6S, "Distrito Federal", "DF")
        Public Shared ES As UF = New UF(7S, "Espítiro Santo", "ES")
        Public Shared GO As UF = New UF(8S, "Goiás", "GO")
        Public Shared MA As UF = New UF(9S, "Maranhão", "MA")
        Public Shared MG As UF = New UF(10S, "Minas Gerais", "MG")
        Public Shared MS As UF = New UF(11S, "Mato Grosso Do Sul", "MS")
        Public Shared MT As UF = New UF(12S, "Mato Grosso", "MT")
        Public Shared PA As UF = New UF(13S, "Pará", "PA")
        Public Shared PB As UF = New UF(14S, "Paraíba", "PB")
        Public Shared PE As UF = New UF(15S, "Pernambuco", "PE")
        Public Shared PI As UF = New UF(16S, "Piauí", "PI")
        Public Shared PR As UF = New UF(17S, "Paraná", "PR")
        Public Shared RJ As UF = New UF(18S, "Rio De Janeiro", "RJ")
        Public Shared RN As UF = New UF(19S, "Rio Grande Do Norte", "RN")
        Public Shared RO As UF = New UF(20S, "Rondônia", "RO")
        Public Shared RR As UF = New UF(21S, "Rorâima", "RR")
        Public Shared RS As UF = New UF(22S, "Rio Grande Do Sul", "RS")
        Public Shared SC As UF = New UF(23S, "Santa Catarina", "SC")
        Public Shared SE As UF = New UF(24S, "Sergipe", "SE")
        Public Shared SP As UF = New UF(25S, "São Paulo", "SP")
        Public Shared TOC As UF = New UF(26S, "Tocantins", "TO")
        Public Shared AL As UF = New UF(27S, "Alagoas", "AL")

        Private Shared Lista As UF() = {AC, AM, AP, BA, CE, DF, ES, GO, MA, MG, MS, MT, PA, _
                                        PB, PE, PI, PR, RJ, RN, RO, RR, RS, SC, SE, SP, TOC, AL}

        Private Sub New(ByVal ID As Short, _
                        ByVal Nome As String, _
                        ByVal Sigla As String)
            _ID = ID
            _Nome = Nome
            _Sigla = Sigla
        End Sub

        Public ReadOnly Property ID() As Short
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Nome() As String
            Get
                Return _Nome
            End Get
        End Property

        Public ReadOnly Property Sigla() As String
            Get
                Return _Sigla
            End Get
        End Property

        Public Shared Function Obtenha(ByVal ID As Short) As UF
            For Each Item As UF In Lista
                If Item.ID = ID Then
                    Return Item
                End If
            Next

            Return Nothing
        End Function

        Public Shared Function ObtenhaTodos() As IList(Of UF)
            Return New List(Of UF)(Lista)
        End Function

        Public Overrides Function Equals(ByVal obj As Object) As Boolean
            Return CType(obj, UF).ID = Me.ID
        End Function

    End Class

End Namespace