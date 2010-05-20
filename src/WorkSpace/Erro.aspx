<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Erro.aspx.vb" Inherits="WorkSpace.Erro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Erro no sistema!</title>
    <style type="text/css">
        body
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            background-color: #FFFFFF;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            margin-left: 0px;
        }
        #centro
        {
            width: 450px;
            height: 800px;
            position: absolute;
            margin-left: -225px;
            left: 50%;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: none;
            border-left-style: none;
            background: transparent url(imagens/erro_fundo2.jpg) repeat-x scroll center center;
        }
        #container
        {
            width: 500px;
            height: 800px;
            position: absolute;
            margin-left: -250px;
            left: 50%;
            border-top-style: none;
            border-right-style: none;
            border-bottom-style: none;
            border-left-style: none;
            background: #FFFFFF url(imagens/fundo.jpg) repeat-y scroll center center;
        }
        #titulo
        {
            font-size: 10px;
            font-weight: bold;
            color: #FFFFFF;
            text-align: center;
            text-transform: uppercase;
            width: 100%;
            height: 15px;
            background: #990000 url(imagens/bg_titulo_erro.jpg) repeat-x;
        }
        #imagem
        {
            width: 450px;
            height: 70px;
            background: #990000 url(imagens/error.png) no-repeat center center;
        }
        #texto
        {
            font-size: 12px;
            font-weight: bold;
            text-align: center;
            color: #FFFFFF;
            height: 30px;
            line-height: 30px;
            vertical-align: 30px;
            background-color: #990000;
        }
        #botaoarea
        {
            font-size: 10px;
            text-align: center;
            color: #FFFFFF;
            height: 30px;
            line-height: 30px;
            vertical-align: middle;
            padding: 2px;
            background-color: #990000;
        }
        #botao
        {
            width: 80px;
            height: 20px;
            font-size: 10px;
            text-align: center;
            color: #FFFFFF;
            font-weight: bold;
            text-align: center;
            border: 1px solid #FFFFFF;
            margin-top: 20px;
            background: transparent url(imagens/bg_titulo_erro.jpg) repeat-x left bottom;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="container">
        <div id="centro">
            <div id="titulo">
                Aten&ccedil;&atilde;o!</div>
            <div id="imagem">
            </div>
            <div id="texto">
                Ocorreu um erro no sistema.</div>
            <div id="botaoarea">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
