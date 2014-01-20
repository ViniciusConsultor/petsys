Namespace Core.Negocio

    Public Interface IEndereco

        Property Logradouro() As String
        Property Complemento() As String
        Property Municipio() As IMunicipio
        Property CEP() As CEP
        Property Bairro() As String
        Property TipoDeEndereco As ITipoDeEndereco

    End Interface

End Namespace