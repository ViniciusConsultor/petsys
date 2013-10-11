Imports Core.Interfaces.Mapeadores
Imports Compartilhados.Interfaces.Core.Negocio
Imports System.Text
Imports Compartilhados.DBHelper
Imports Compartilhados
Imports Compartilhados.Fabricas

Public Class MapeadorDeCliente
    Implements IMapeadorDeCliente

    Public Sub Inserir(ByVal Cliente As ICliente) Implements IMapeadorDeCliente.Inserir
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("INSERT INTO NCL_CLIENTE (")
        Sql.Append("IDPESSOA, TIPOPESSOA, DTCADASTRO, INFOADICIONAL, FAIXASALARIAL, DESCONTOAUTOMATICO, VLRMAXCOMPRAS, SALDOCOMPRAS, NUMREGISTRO, DTREGISTRO, IDGRPATIVIDADE, IDEMPRESA)")
        Sql.Append(" VALUES (")
        Sql.Append(String.Concat(Cliente.Pessoa.ID.ToString, ", "))
        Sql.Append(String.Concat(Cliente.Pessoa.Tipo.ID, ", "))
        Sql.Append(String.Concat(Cliente.DataDoCadastro.Value.ToString("yyyyMMdd"), ", "))

        If String.IsNullOrEmpty(Cliente.InformacoesAdicionais) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Cliente.InformacoesAdicionais), "', "))
        End If

        If Not Cliente.FaixaSalarial.HasValue Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Cliente.FaixaSalarial.Value), ", "))
        End If

        If Not Cliente.PorcentagemDeDescontoAutomatico.HasValue Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Cliente.PorcentagemDeDescontoAutomatico.Value), ", "))
        End If

        If Not Cliente.ValorMaximoParaCompras.HasValue Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Cliente.ValorMaximoParaCompras.Value), ", "))
        End If

        If Not Cliente.SaldoParaCompras.HasValue Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(UtilidadesDePersistencia.TPVd(Cliente.SaldoParaCompras.Value), ", "))
        End If

        If String.IsNullOrEmpty(Cliente.NumeroDoRegistro) Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat("'", UtilidadesDePersistencia.FiltraApostrofe(Cliente.NumeroDoRegistro), "', "))
        End If

        If Not Cliente.DataDoRegistro.HasValue Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(Cliente.DataDoRegistro.Value.ToString("yyyyMMdd"), ", "))
        End If

        If Cliente.GrupoDeAtividade Is Nothing Then
            Sql.Append("NULL, ")
        Else
            Sql.Append(String.Concat(Cliente.GrupoDeAtividade.ID, ", "))
        End If


        Sql.Append(ServerUtils.getCredencial().EmpresaLogada.ID & ")")


        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Overloads Function Obtenha(ByVal Pessoa As IPessoa) As ICliente Implements IMapeadorDeCliente.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cliente As ICliente = Nothing

        Sql.Append("SELECT IDPESSOA, TIPOPESSOA, DTCADASTRO, INFOADICIONAL, FAIXASALARIAL, DESCONTOAUTOMATICO, VLRMAXCOMPRAS, SALDOCOMPRAS, NUMREGISTRO, DTREGISTRO, IDGRPATIVIDADE, NOME FROM NCL_CLIENTE ")
        Sql.Append("LEFT JOIN NCL_GRUPO_DE_ATIVIDADE ON IDGRPATIVIDADE = ID ")
        Sql.Append("WHERE ")
        Sql.Append(String.Concat("IDPESSOA = ", Pessoa.ID.Value.ToString, " AND "))
        Sql.Append(String.Concat("TIPOPESSOA = ", Pessoa.Tipo.ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    Cliente = MontaObjetoCliente(Leitor, Pessoa)
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Cliente
    End Function

    Private Function MontaObjetoCliente(ByVal Leitor As IDataReader, ByVal Pessoa As IPessoa) As ICliente
        Dim Cliente As ICliente = Nothing

        Cliente = FabricaGenerica.GetInstancia.CrieObjeto(Of ICliente)(New Object() {Pessoa})

        Cliente.DataDoCadastro = UtilidadesDePersistencia.getValorDate(Leitor, "DTCADASTRO")

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "INFOADICIONAL") Then
            Cliente.InformacoesAdicionais = UtilidadesDePersistencia.GetValorString(Leitor, "INFOADICIONAL")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "FAIXASALARIAL") Then
            Cliente.FaixaSalarial = UtilidadesDePersistencia.getValorDouble(Leitor, "FAIXASALARIAL")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "DESCONTOAUTOMATICO") Then
            Cliente.PorcentagemDeDescontoAutomatico = UtilidadesDePersistencia.getValorDouble(Leitor, "DESCONTOAUTOMATICO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "VLRMAXCOMPRAS") Then
            Cliente.ValorMaximoParaCompras = UtilidadesDePersistencia.getValorDouble(Leitor, "VLRMAXCOMPRAS")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "SALDOCOMPRAS") Then
            Cliente.SaldoParaCompras = UtilidadesDePersistencia.getValorDouble(Leitor, "SALDOCOMPRAS")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "NUMREGISTRO") Then
            Cliente.NumeroDoRegistro = UtilidadesDePersistencia.GetValorString(Leitor, "NUMREGISTRO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "DTREGISTRO") Then
            Cliente.DataDoRegistro = UtilidadesDePersistencia.getValorDate(Leitor, "DTREGISTRO")
        End If

        If Not UtilidadesDePersistencia.EhNulo(Leitor, "IDGRPATIVIDADE") Then
            Dim GrupoDeAtividade As IGrupoDeAtividade = FabricaGenerica.GetInstancia.CrieObjeto(Of IGrupoDeAtividade)()
            GrupoDeAtividade.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "IDGRPATIVIDADE")
            GrupoDeAtividade.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOME")

            Cliente.GrupoDeAtividade = GrupoDeAtividade
        End If

        Return Cliente
    End Function

    Public Sub Remover(ByVal ID As Long) Implements IMapeadorDeCliente.Remover
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("DELETE FROM NCL_CLIENTE")
        Sql.Append(String.Concat(" WHERE IDPESSOA = ", ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)
    End Sub

    Public Function ObtenhaPorNomeComoFiltro(ByVal Nome As String, _
                                             ByVal QuantidadeMaximaDeRegistros As Integer) As IList(Of ICliente) Implements IMapeadorDeCliente.ObtenhaPorNomeComoFiltro
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cliente As ICliente = Nothing
        Dim Pessoa As IPessoa = Nothing
        Dim Tipo As TipoDePessoa
        Dim Clientes As IList(Of ICliente) = New List(Of ICliente)

        Sql.Append("SELECT NCL_PESSOA.ID IDDAPESSOA, NCL_PESSOA.NOME NOMEPESSOA, NCL_PESSOA.TIPO,")
        Sql.Append("NCL_CLIENTE.IDPESSOA, NCL_CLIENTE.TIPOPESSOA, NCL_CLIENTE.DTCADASTRO, NCL_CLIENTE.INFOADICIONAL, NCL_CLIENTE.FAIXASALARIAL, NCL_CLIENTE.DESCONTOAUTOMATICO, NCL_CLIENTE.VLRMAXCOMPRAS, NCL_CLIENTE.SALDOCOMPRAS, NCL_CLIENTE.NUMREGISTRO, NCL_CLIENTE.DTREGISTRO, NCL_CLIENTE.IDGRPATIVIDADE, NCL_GRUPO_DE_ATIVIDADE.NOME  ")
        Sql.Append("FROM NCL_PESSOA, NCL_CLIENTE ")
        Sql.Append("LEFT JOIN NCL_GRUPO_DE_ATIVIDADE ON IDGRPATIVIDADE = NCL_GRUPO_DE_ATIVIDADE.ID ")
        Sql.Append("WHERE NCL_CLIENTE.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND NCL_CLIENTE.TIPOPESSOA = NCL_PESSOA.TIPO ")
        Sql.Append("AND IDEMPRESA = " & ServerUtils.getCredencial().EmpresaLogada.ID)

        If Not String.IsNullOrEmpty(Nome) Then
            Sql.Append(String.Concat(" AND NOME LIKE '%", UtilidadesDePersistencia.FiltraApostrofe(Nome), "%'"))
        End If

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString, QuantidadeMaximaDeRegistros)
            Try
                While Leitor.Read
                    Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))

                    If Tipo.Equals(TipoDePessoa.Fisica) Then
                        Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                    Else
                        Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                    End If

                    Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "IDDAPESSOA")
                    Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEPESSOA")

                    Cliente = MontaObjetoCliente(Leitor, Pessoa)

                    Clientes.Add(Cliente)
                End While
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Clientes
    End Function

    Public Overloads Function Obtenha(ByVal ID As Long) As ICliente Implements IMapeadorDeCliente.Obtenha
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper
        Dim Cliente As ICliente = Nothing
        Dim Tipo As TipoDePessoa
        Dim Pessoa As IPessoa = Nothing

        Sql.Append("SELECT NCL_PESSOA.ID IDDAPESSOA, NCL_PESSOA.NOME NOMEPESSOA, NCL_PESSOA.TIPO, ")
        Sql.Append("NCL_CLIENTE.IDPESSOA, NCL_CLIENTE.TIPOPESSOA, NCL_CLIENTE.DTCADASTRO, NCL_CLIENTE.INFOADICIONAL, NCL_CLIENTE.FAIXASALARIAL, NCL_CLIENTE.DESCONTOAUTOMATICO, NCL_CLIENTE.VLRMAXCOMPRAS, NCL_CLIENTE.SALDOCOMPRAS, NCL_CLIENTE.NUMREGISTRO, NCL_CLIENTE.DTREGISTRO, NCL_CLIENTE.IDGRPATIVIDADE, NCL_GRUPO_DE_ATIVIDADE.NOME  ")
        Sql.Append("FROM NCL_PESSOA, NCL_CLIENTE ")
        Sql.Append("LEFT JOIN NCL_GRUPO_DE_ATIVIDADE ON IDGRPATIVIDADE = NCL_GRUPO_DE_ATIVIDADE.ID ")
        Sql.Append("WHERE NCL_CLIENTE.IDPESSOA = NCL_PESSOA.ID ")
        Sql.Append("AND NCL_CLIENTE.TIPOPESSOA = NCL_PESSOA.TIPO ")
        Sql.Append(String.Concat("AND IDPESSOA = ", ID.ToString))

        DBHelper = ServerUtils.criarNovoDbHelper

        Using Leitor As IDataReader = DBHelper.obtenhaReader(Sql.ToString)
            Try
                If Leitor.Read Then
                    Tipo = TipoDePessoa.Obtenha(UtilidadesDePersistencia.getValorShort(Leitor, "TIPO"))

                    If Tipo.Equals(TipoDePessoa.Fisica) Then
                        Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaFisica)()
                    Else
                        Pessoa = FabricaGenerica.GetInstancia.CrieObjeto(Of IPessoaJuridica)()
                    End If

                    Pessoa.ID = UtilidadesDePersistencia.GetValorLong(Leitor, "IDDAPESSOA")
                    Pessoa.Nome = UtilidadesDePersistencia.GetValorString(Leitor, "NOMEPESSOA")

                    Cliente = MontaObjetoCliente(Leitor, Pessoa)
                End If
            Finally
                Leitor.Close()
            End Try
        End Using

        Return Cliente
    End Function

    Public Sub Modificar(ByVal Cliente As ICliente) Implements IMapeadorDeCliente.Modificar
        Dim Sql As New StringBuilder
        Dim DBHelper As IDBHelper

        DBHelper = ServerUtils.getDBHelper

        Sql.Append("UPDATE NCL_CLIENTE SET ")

        If String.IsNullOrEmpty(Cliente.InformacoesAdicionais) Then
            Sql.Append("INFOADICIONAL = NULL, ")
        Else
            Sql.Append(String.Concat("INFOADICIONAL = '", UtilidadesDePersistencia.FiltraApostrofe(Cliente.InformacoesAdicionais), "', "))
        End If

        If Not Cliente.FaixaSalarial.HasValue Then
            Sql.Append("FAIXASALARIAL = NULL, ")
        Else
            Sql.Append(String.Concat("FAIXASALARIAL = ", UtilidadesDePersistencia.TPVd(Cliente.FaixaSalarial.Value), ", "))
        End If

        If Not Cliente.PorcentagemDeDescontoAutomatico.HasValue Then
            Sql.Append("DESCONTOAUTOMATICO = NULL, ")
        Else
            Sql.Append(String.Concat("DESCONTOAUTOMATICO = ", UtilidadesDePersistencia.TPVd(Cliente.PorcentagemDeDescontoAutomatico.Value), ", "))
        End If

        If Not Cliente.ValorMaximoParaCompras.HasValue Then
            Sql.Append("VLRMAXCOMPRAS = NULL, ")
        Else
            Sql.Append(String.Concat("VLRMAXCOMPRAS = ", UtilidadesDePersistencia.TPVd(Cliente.ValorMaximoParaCompras.Value), ", "))
        End If

        If Not Cliente.SaldoParaCompras.HasValue Then
            Sql.Append("SALDOCOMPRAS = NULL, ")
        Else
            Sql.Append(String.Concat("SALDOCOMPRAS = ", UtilidadesDePersistencia.TPVd(Cliente.SaldoParaCompras.Value), ", "))
        End If

        If String.IsNullOrEmpty(Cliente.NumeroDoRegistro) Then
            Sql.Append("NUMREGISTRO = NULL, ")
        Else
            Sql.Append(String.Concat("NUMREGISTRO = '", UtilidadesDePersistencia.FiltraApostrofe(Cliente.NumeroDoRegistro), "', "))
        End If

        If Not Cliente.DataDoRegistro.HasValue Then
            Sql.Append("DTREGISTRO = NULL, ")
        Else
            Sql.Append(String.Concat("DTREGISTRO = ", Cliente.DataDoRegistro.Value.ToString("yyyyMMdd"), ", "))
        End If

        If Cliente.GrupoDeAtividade Is Nothing Then
            Sql.Append("IDGRPATIVIDADE = NULL")
        Else
            Sql.Append(String.Concat("IDGRPATIVIDADE = ", Cliente.GrupoDeAtividade.ID))
        End If

        Sql.Append(String.Concat(" WHERE IDPESSOA = ", Cliente.Pessoa.ID.ToString))
        DBHelper.ExecuteNonQuery(Sql.ToString)

    End Sub

End Class