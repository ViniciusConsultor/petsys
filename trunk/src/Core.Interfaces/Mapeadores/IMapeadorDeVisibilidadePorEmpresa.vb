﻿Imports Compartilhados.Interfaces.Core.Negocio

Namespace Mapeadores

    Public Interface IMapeadorDeVisibilidadePorEmpresa

        Function Obtenha(IDOperador As Long) As IList(Of IEmpresa)
        Sub Inserir(IDOperador As Long, EmpresasVisiveis As IList(Of IEmpresa))
        Sub Modifique(IDOperador As Long, EmpresasVisiveis As IList(Of IEmpresa))
        Sub Remova(IDOperador As Long)

    End Interface

End Namespace