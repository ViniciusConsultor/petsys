Imports Compartilhados.Interfaces.Core.Negocio

Namespace Core.Servicos

    Public Interface IServicoDeMunicipio
        Inherits IServico

        Function ObtenhaMunicipio(ByVal Id As Long) As IMunicipio
        Function ObtenhaMunicipiosPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IMunicipio)
        Sub Inserir(ByVal Municipio As IMunicipio)
        Sub Excluir(ByVal Id As Long)
        Sub Modificar(ByVal Municipio As IMunicipio)

    End Interface

End Namespace