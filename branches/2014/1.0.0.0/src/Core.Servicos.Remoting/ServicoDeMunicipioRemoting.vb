Imports Compartilhados
Imports Compartilhados.Interfaces.Core.Servicos
Imports Core.Servicos.Local
Imports Compartilhados.Interfaces.Core.Negocio

Public Class ServicoDeMunicipioRemoting
    Inherits ServicoRemoto
    Implements IServicoDeMunicipio

    Private _ServicoLocal As ServicoDeMunicipioLocal

    Public Overrides Sub SetaCredencial(ByVal Credencial As ICredencial)
        _ServicoLocal = New ServicoDeMunicipioLocal(Credencial)
    End Sub

    Public Sub Excluir(ByVal Id As Long) Implements IServicoDeMunicipio.Excluir
        _ServicoLocal.Excluir(Id)
    End Sub

    Public Sub Inserir(ByVal Municipio As IMunicipio) Implements IServicoDeMunicipio.Inserir
        _ServicoLocal.Inserir(Municipio)
    End Sub

    Public Sub Modificar(ByVal Municipio As IMunicipio) Implements IServicoDeMunicipio.Modificar
        _ServicoLocal.Modificar(Municipio)
    End Sub

    Public Function ObtenhaMunicipio(ByVal Id As Long) As IMunicipio Implements IServicoDeMunicipio.ObtenhaMunicipio
        Return _ServicoLocal.ObtenhaMunicipio(Id)
    End Function

    Public Function ObtenhaMunicipiosPorNomeComoFiltro(ByVal Nome As String, ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of IMunicipio) Implements IServicoDeMunicipio.ObtenhaMunicipiosPorNomeComoFiltro
        Return _ServicoLocal.ObtenhaMunicipiosPorNomeComoFiltro(Nome, QuantidadeMaximaDeRegistros)
    End Function

End Class