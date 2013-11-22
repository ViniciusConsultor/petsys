﻿Imports System.Text
Imports Core.Interfaces.Negocio
Imports Core.Interfaces.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio
Imports Compartilhados
Imports Compartilhados.Componentes.Web

Public Class FabricaDeMenu

    Private _JsMenu As StringBuilder
    Private _Menu As IMenuComposto
    Private _Principal As Principal

    Public Sub New()
        _JsMenu = New StringBuilder
    End Sub

    Public Function ObtenhaMenu() As String
        _Principal = FabricaDeContexto.GetInstancia.GetContextoAtual

        Using Servico As IServicoDeMenu = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDeMenu)()
            _Menu = Servico.ObtenhaMenu
        End Using

        _JsMenu.AppendLine("<script language='javascript' type='text/javascript'>")
        _JsMenu.AppendLine(ObtenhaMenuIniciar())

        For Each MenuModulo As IMenuComposto In From MenuModulo1 In _Menu.ObtenhaItens Where _Principal.EstaAutorizado(MenuModulo1.ID)
            ObtenhaMenuModulo(MenuModulo)
        Next

        _JsMenu.AppendLine("</script>")
        Return _JsMenu.ToString
    End Function

    Private Function ObtenhaMenuIniciar() As String
        Dim Mi As New StringBuilder
        Dim NomeDosModulos As New StringBuilder
        Dim UsuarioLogado As Usuario

        UsuarioLogado = _Principal.Usuario

        Mi.AppendLine("MyDesktop = new Ext.app.App({")
        Mi.AppendLine("init :function(){")
        Mi.AppendLine("Ext.QuickTips.init();},")

        If _Menu.ObtenhaItens.Count <> 0 Then
            'Obtem os IDs dos modulos para criar as funções JS dinamicamente
            For Each MenuComposto As IMenuAbstrato In From MenuComposto1 In _Menu.ObtenhaItens Where _Principal.EstaAutorizado(MenuComposto1.ID)
                NomeDosModulos.Append(String.Concat("new MyDesktop.", MenuComposto.IDSemFormatacao, "(),"))
            Next

            If NomeDosModulos.Length > 0 Then
                'Remove a virgula do ultimo modulo
                NomeDosModulos = NomeDosModulos.Remove(NomeDosModulos.Length - 1, 1)
            End If
            
            'Modulos
            Mi.AppendLine("getModules : function(){")
            Mi.AppendLine("return [")
            Mi.AppendLine(NomeDosModulos.ToString)
            Mi.AppendLine("];},")
        End If

        'Configurações do menu iniciar
        Mi.AppendLine("getStartConfig : function(){")
        Mi.AppendLine("return {")
        Mi.AppendLine(String.Concat("title:'", UsuarioLogado.Nome, "',"))

        If UsuarioLogado.Sexo = "M" Then
            Mi.AppendLine("iconCls:'user',")
        Else
            Mi.AppendLine("iconCls:'user-girl',")
        End If

        Mi.AppendLine("toolItems: [{")
        If FabricaDeContexto.GetInstancia().GetContextoAtual().Usuario.EmpresasVisiveis().Count > 1 Then
            Mi.AppendLine("text: 'Empresas',")
            Mi.AppendLine("iconCls:'settings',")
            Mi.AppendLine("scope: this,")
            Mi.AppendLine("href:'frmEscolhaDaEmpresa.aspx'")
            Mi.AppendLine("},'-',{")
        End If
        
        Mi.AppendLine("text:'Sair',")
        Mi.AppendLine("iconCls:'logout',")
        Mi.AppendLine("scope:this,")
        Mi.AppendLine("href:'logout.aspx'")
        Mi.AppendLine("}]")
        Mi.AppendLine("};")
        Mi.AppendLine("}")
        Mi.AppendLine("});")

        Return Mi.ToString
    End Function

    Private Sub MontaMenus(Menus As IMenuComposto)
        For Each Funcao As IMenuFolha In From Funcao1 In Menus.ObtenhaItens Where _Principal.EstaAutorizado(Funcao1.ID)
            _JsMenu.AppendLine(String.Concat("{text:'", Funcao.Nome, "',"))
            _JsMenu.AppendLine(String.Concat("iconCls:'", Funcao.Imagem, "',"))

            _JsMenu.AppendLine("handler:  function() {")
            _JsMenu.AppendLine("var desktop = this.app.getDesktop();")
            _JsMenu.AppendLine(String.Concat("var win = desktop.getWindow('win", Funcao.IDSemFormatacao, "');"))
            _JsMenu.AppendLine("if(!win){")
            _JsMenu.AppendLine("win = desktop.createWindow({")
            _JsMenu.AppendLine(String.Concat("id:'win", Funcao.IDSemFormatacao, "',"))
            _JsMenu.AppendLine(String.Concat("title:'", Funcao.Nome, "',"))
            _JsMenu.AppendLine("width:800,")
            _JsMenu.AppendLine("height:550,")
            _JsMenu.AppendLine(String.Concat("html:'" & CrieHTML(UtilidadesWeb.ObtenhaURLHostDiretorioVirtual & Funcao.URL) & "',"))
            _JsMenu.AppendLine(String.Concat("iconCls:'", Funcao.Imagem, "',"))
            _JsMenu.AppendLine("shim:false,")
            _JsMenu.AppendLine("animCollapse:true,")
            _JsMenu.AppendLine("constrainHeader:true")
            _JsMenu.AppendLine("});")
            _JsMenu.AppendLine("}")
            _JsMenu.AppendLine("win.show();")
            _JsMenu.AppendLine("},")
            _JsMenu.AppendLine("scope: this")
            _JsMenu.Append("},")
        Next

        'retira a virgula
        _JsMenu.Remove(_JsMenu.Length - 1, 1)
    End Sub

    Private Sub ObtenhaMenuModulo(ByVal MenuItemModulo As IMenuComposto)
        _JsMenu.AppendLine(String.Concat("MyDesktop.", MenuItemModulo.IDSemFormatacao, " = Ext.extend(Ext.app.Module, {"))
        _JsMenu.AppendLine(String.Concat("id:'", MenuItemModulo.IDSemFormatacao, "',"))
        _JsMenu.AppendLine("init : function(){")
        _JsMenu.AppendLine("this.launcher = {")
        _JsMenu.AppendLine(String.Concat("text:'", MenuItemModulo.Nome, "',"))
        _JsMenu.AppendLine(String.Concat("iconCls: '", MenuItemModulo.Imagem, "',"))
        _JsMenu.AppendLine("handler: function() {")
        _JsMenu.AppendLine("return false;")
        _JsMenu.AppendLine("},")
        _JsMenu.AppendLine("menu: {")
        _JsMenu.AppendLine("items:[")

        If MenuItemModulo.Agrupador.Count() > 0 Then
            For Each Agrupador As KeyValuePair(Of String, IMenuComposto) In MenuItemModulo.Agrupador
                _JsMenu.Append("{")
                _JsMenu.AppendLine(String.Concat("text:'", Agrupador.Key, "',"))
                _JsMenu.AppendLine(String.Concat("iconCls: '", MenuItemModulo.Imagem, "',"))
                _JsMenu.AppendLine("menu: {")
                
                _JsMenu.AppendLine("items:[")

                MontaMenus(Agrupador.Value)

                _JsMenu.AppendLine("]")
                _JsMenu.AppendLine("}")
                _JsMenu.Append("},")
            Next

        End If
        
        MontaMenus(MenuItemModulo)
        
        _JsMenu.AppendLine("]")
        _JsMenu.AppendLine("}")
        _JsMenu.AppendLine("}")
        _JsMenu.AppendLine("}")
        _JsMenu.AppendLine("});")
    End Sub

    Private Function CrieHTML(ByVal URL As String) As String
        Dim HTML As New StringBuilder
        Dim IDDiv As String = Guid.NewGuid.ToString

        IDDiv = IDDiv.Replace("-", "_")
        IDDiv = String.Concat("div_", IDDiv)

        HTML.Append("<div id=""" & IDDiv & """ style=""position: absolute; width: 100%; height: 100%; background-color: white; padding: 0pt;"">")
        HTML.Append("<div style=""position: absolute; text-align: center; width: 100%; height: 70px; top: 40%;"">")
        HTML.Append("<img src=""imagens/carregandopagina.gif"" alt="""">")
        HTML.Append("</div>")
        HTML.Append("</div>")
        HTML.Append("<iframe src=""" & URL & """ & frameborder=""0""  onload=""hideLoading(" & IDDiv & ")""  style=""width: 100%; height: 100%; background-color: white;""/>")

        Return HTML.ToString
    End Function

End Class