Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeMunicipio

        Function ObtenhaMunicipio(ByVal Id As Long) As IMunicipio
        Function ObtenhaMunicipiosPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IMunicipio)
        Sub Inserir(ByVal Municipio As IMunicipio)
        Sub Excluir(ByVal Id As Long)
        Sub Modificar(ByVal Municipio As IMunicipio)
        Function Existe(ByVal Municipio As IMunicipio) As Boolean

    End Interface

End Namespace