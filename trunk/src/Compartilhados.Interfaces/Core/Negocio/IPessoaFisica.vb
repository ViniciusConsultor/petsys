Namespace Core.Negocio

    Public Interface IPessoaFisica
        Inherits IPessoa

        Property Foto() As String
        Property Sexo() As Sexo
        Property DataDeNascimento() As Nullable(Of Date)
        Property Nacionalidade() As Nacionalidade
        Property EstadoCivil() As EstadoCivil
        Property Raca() As Raca
        Property GrauDeInstrucao() As GrauDeInstrucao
        Property NomeDaMae() As String
        Property NomeDoPai() As String
        Property Naturalidade() As IMunicipio

    End Interface

End Namespace