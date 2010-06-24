Imports T13.Interfaces.Negocio
Imports iTextSharp.text.pdf
Imports iTextSharp.text
Imports System.IO
Imports Compartilhados.Componentes.Web
Imports Compartilhados.Interfaces.Core.Negocio
Imports Core.Interfaces.Servicos
Imports Compartilhados.Interfaces.Core.Servicos
Imports Compartilhados.Fabricas
Imports Compartilhados.Interfaces.Core.Negocio.Documento

Public Class CriadorDeRelatorio

    Private _documento As Document
    Private _lancamentos As IList(Of ILacamentoDeServicosPrestados)
    Private _Fonte1 As Font
    Private _Fonte2 As Font
    Private _Fonte3 As Font
    Private _lancamentoCorrente As ILacamentoDeServicosPrestados
    Private _PaginaAtual As Integer = 0

    Public Sub New(ByVal Lancamentos As IList(Of ILacamentoDeServicosPrestados))
        _lancamentos = Lancamentos
        _Fonte1 = New Font(Font.TIMES_ROMAN, 10)
        _Fonte2 = New Font(Font.TIMES_ROMAN, 10, Font.BOLD)
        _Fonte3 = New Font(Font.TIMES_ROMAN, 10)
    End Sub

    Public Function GereNotaFiscal() As String
        Dim NomeDoPDF As String
        Dim CaminhoDoPDF As String
        Dim Escritor As PdfWriter

        NomeDoPDF = String.Concat(Now.ToString("yyyyMMddhhmmss"), ".pdf")
        CaminhoDoPDF = String.Concat(HttpContext.Current.Request.PhysicalApplicationPath, UtilidadesWeb.PASTA_LOADS)

        _documento = New Document(New Rectangle(603.779527559055, 670))
        _documento.SetMargins(5.6692913385826769, 2.8346456692913389, 0, 0)
        Escritor = PdfWriter.GetInstance(_documento, New FileStream(Path.Combine(CaminhoDoPDF, NomeDoPDF), FileMode.Create))
        Escritor.PageEvent = New Ouvinte()
        Escritor.AddViewerPreference(PdfName.PRINTSCALING, PdfName.NONE)
        Escritor.AddViewerPreference(PdfName.PICKTRAYBYPDFSIZE, PdfName.NONE)

        _documento.Open()

        For Each Lancamento As ILacamentoDeServicosPrestados In _lancamentos
            _lancamentoCorrente = Lancamento
            GeraCabecalho()
            GeraDadosDoUsuario()
            GeraDiscriminacaoDosServicos()
        Next
        
        _documento.Close()

        Return NomeDoPDF
    End Function

    Private Sub GeraCabecalho()
        Dim NumeroDaNota As Paragraph
        Dim DataDaEmissao As Paragraph
        Dim NaturezaDaOperacao As Paragraph

        NumeroDaNota = New Paragraph(14.173228346456691, _lancamentoCorrente.Numero.Value.ToString("0000"), _Fonte1)
        NumeroDaNota.IndentationLeft = 532.86
        _documento.Add(NumeroDaNota)

        DataDaEmissao = New Paragraph(62.64, _lancamentoCorrente.DataDeLancamento.ToString("dd/MM/yyyy"), _Fonte1)
        DataDaEmissao.IndentationLeft = 339.71
        _documento.Add(DataDaEmissao)

        Dim TextoNaturezaDaOperacao As String = String.Empty

        If Not String.IsNullOrEmpty(_lancamentoCorrente.NaturezaDaOperacao) Then
            TextoNaturezaDaOperacao = _lancamentoCorrente.NaturezaDaOperacao
        End If

        NaturezaDaOperacao = New Paragraph(14.4, TextoNaturezaDaOperacao, _Fonte1)
        NaturezaDaOperacao.IndentationLeft = 339.71
        _documento.Add(NaturezaDaOperacao)
    End Sub

    Private Sub GeraDadosDoUsuario()
        Dim NomeDoCliente As Paragraph
        Dim Endereco As Paragraph
        Dim Cidade As Paragraph
        Dim Estado As Paragraph
        Dim CEP As Paragraph
        Dim CPFCNPJ As Paragraph
        Dim InscMunicipal As Paragraph
        Dim InscEstadual As Paragraph
        Dim Pessoa As IPessoa

        If _lancamentoCorrente.Cliente.Pessoa.Tipo.Equals(TipoDePessoa.Fisica) Then
            Using ServicoDePessoaFisica As IServicoDePessoaFisica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaFisica)()
                Pessoa = ServicoDePessoaFisica.ObtenhaPessoa(_lancamentoCorrente.Cliente.Pessoa.ID.Value)
            End Using
        Else
            Using ServicoDePessoaJuridica As IServicoDePessoaJuridica = FabricaGenerica.GetInstancia.CrieObjeto(Of IServicoDePessoaJuridica)()
                Pessoa = ServicoDePessoaJuridica.ObtenhaPessoa(_lancamentoCorrente.Cliente.Pessoa.ID.Value)
            End Using

        End If

        NomeDoCliente = New Paragraph(31.1811, Pessoa.Nome, _Fonte1)
        NomeDoCliente.IndentationLeft = 16.78
        _documento.Add(NomeDoCliente)

        Dim TextoEndereco As String = " "
        Dim TextoCidade As String = " "
        Dim TextoEstado As String = " "
        Dim TextoCEP As String = " "

        If Not Pessoa.Endereco Is Nothing Then

            If Not String.IsNullOrEmpty(Pessoa.Endereco.Complemento) Then
                TextoEndereco = String.Concat(Pessoa.Endereco.Logradouro, " ", Pessoa.Endereco.Complemento)
            Else
                TextoEndereco = Pessoa.Endereco.Logradouro
            End If

            TextoCidade = Pessoa.Endereco.Municipio.Nome
            TextoEstado = Pessoa.Endereco.Municipio.UF.Sigla
            TextoCEP = Pessoa.Endereco.CEP.ToString
        End If

        Endereco = New Paragraph(14.4, TextoEndereco, _Fonte1)
        Endereco.IndentationLeft = 36.62
        _documento.Add(Endereco)

        Cidade = New Paragraph(13, TextoCidade, _Fonte1)
        Cidade.IndentationLeft = 19
        _documento.Add(Cidade)

        Estado = New Paragraph(0, TextoEstado, _Fonte1)
        Estado.IndentationLeft = 267.9527
        _documento.Add(Estado)

        CEP = New Paragraph(0, TextoCEP, _Fonte1)
        CEP.IndentationLeft = 315.9842
        _documento.Add(CEP)

        Dim TextoCPFCNPJ As String = " "
        Dim TextoInscricaoMunicipal As String = " "
        Dim TextoInscricaoEstadual As String = " "

        If Pessoa.Tipo.Equals(TipoDePessoa.Fisica) Then
            Dim CPF As ICPF

            CPF = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.CPF), ICPF)

            If Not CPF Is Nothing Then
                TextoCPFCNPJ = CPF.ToString
            End If
        Else
            Dim CNPJ As ICNPJ
            Dim InscricaoMunicipal As IInscricaoMunicipal
            Dim InscricaoEstadual As IInscricaoEstadual

            CNPJ = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.CNPJ), ICNPJ)

            If Not CNPJ Is Nothing Then
                TextoCPFCNPJ = CNPJ.ToString
            End If

            InscricaoEstadual = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.IE), IInscricaoEstadual)
            InscricaoMunicipal = CType(Pessoa.ObtenhaDocumento(TipoDeDocumento.IM), IInscricaoMunicipal)

            If Not InscricaoEstadual Is Nothing Then
                TextoInscricaoEstadual = InscricaoEstadual.ToString
            End If

            If Not InscricaoMunicipal Is Nothing Then
                TextoInscricaoMunicipal = InscricaoMunicipal.ToString
            End If
        End If

        CPFCNPJ = New Paragraph(13, TextoCPFCNPJ, _Fonte1)
        CPFCNPJ.IndentationLeft = 62
        _documento.Add(CPFCNPJ)

        InscMunicipal = New Paragraph(0, TextoInscricaoMunicipal, _Fonte1)
        InscMunicipal.IndentationLeft = 300.1417
        _documento.Add(InscMunicipal)

        InscEstadual = New Paragraph(0, TextoInscricaoEstadual, _Fonte1)
        InscEstadual.IndentationLeft = 453.5433
        _documento.Add(InscEstadual)

    End Sub

    Private Sub GeraDiscriminacaoDosServicos()
        Dim TamanhoTotal As Single = 0

        _documento.Add(New Paragraph(84.96, " ", _Fonte1))

        For Each Item As IItemDeLancamento In _lancamentoCorrente.ObtenhaItensDeLancamento
            Dim Quantidade As Paragraph
            Dim TextoQuantidade As String = " "

            If Not Item.Quantidade Is Nothing Then
                TextoQuantidade = Item.Quantidade.Value.ToString
            End If

            Quantidade = New Paragraph(11.3385, TextoQuantidade, _Fonte1)
            Quantidade.IndentationLeft = 13.95
            _documento.Add(Quantidade)

            Dim Unidade As Paragraph
            Dim TextoUnidade As String = " "

            If Not String.IsNullOrEmpty(Item.Unidade) Then
                TextoUnidade = Item.Unidade
            End If

            Unidade = New Paragraph(0, TextoUnidade, _Fonte1)
            Unidade.IndentationLeft = 59
            _documento.Add(Unidade)

            Dim TextoDiscriminacao As String
            Dim Discriminacao As Paragraph

            If Not String.IsNullOrEmpty(Item.Observacao) Then
                TextoDiscriminacao = String.Concat(Item.Servico.Nome, " - ", Item.Observacao)
            Else
                TextoDiscriminacao = Item.Servico.Nome
            End If

            Discriminacao = New Paragraph(0, TextoDiscriminacao, _Fonte1)
            Discriminacao.IndentationLeft = 98.99
            _documento.Add(Discriminacao)

            Dim Valor As Paragraph

            Valor = New Paragraph(0, Format(Item.Valor, "###,###,##0.00"), _Fonte1)
            Valor.IndentationLeft = 439.37
            _documento.Add(Valor)

            Dim Total As Paragraph

            Total = New Paragraph(0, Format(Item.Total, "###,###,##0.00"), _Fonte1)
            Total.IndentationLeft = 518.7401
            _documento.Add(Total)

            TamanhoTotal = CSng(TamanhoTotal + 11.3385)
        Next

        If TamanhoTotal < 175.748 Then
            While TamanhoTotal < 175.478
                _documento.Add(New Paragraph(11.3385, " ", _Fonte1))
                TamanhoTotal = CSng(TamanhoTotal + 11.3385)
            End While
        End If

        Dim Observacoes As Paragraph
        Dim TextoObservacoes As String = " "

        If Not String.IsNullOrEmpty(_lancamentoCorrente.Observacoes) Then
            TextoObservacoes = _lancamentoCorrente.Observacoes
        End If

        Observacoes = New Paragraph(28.8, TextoObservacoes, _Fonte1)
        Observacoes.IndentationLeft = 36.8503
        _documento.Add(Observacoes)

        Dim ISSQN As Paragraph
        Dim TextoISSQN As String = " "

        TextoISSQN = Format(_lancamentoCorrente.ObtenhaValorDoISSQN, "###,###,##0.00")

        ISSQN = New Paragraph(49.5, TextoISSQN, _Fonte1)
        ISSQN.IndentationLeft = 169.92
        _documento.Add(ISSQN)

        Dim Aliquota As Paragraph
        Dim TextoAliquota As String = " "

        If Not String.IsNullOrEmpty(_lancamentoCorrente.Aliquota) Then
            TextoAliquota = _lancamentoCorrente.Aliquota & "%"
        End If

        Aliquota = New Paragraph(0, TextoAliquota, _Fonte1)
        Aliquota.IndentationLeft = 339.84
        _documento.Add(Aliquota)

        Dim ValorTotal As Paragraph

        ValorTotal = New Paragraph(0, Format(_lancamentoCorrente.ObtenhaTotalDosItensLancados, "###,###,##0.00"), _Fonte2)
        ValorTotal.IndentationLeft = 481.8897
        _documento.Add(ValorTotal)

        Dim NumeroDoLancamento As Paragraph

        NumeroDoLancamento = New Paragraph(76.9, _lancamentoCorrente.Numero.Value.ToString("0000"), _Fonte3)
        NumeroDoLancamento.IndentationLeft = 481.8897
        _documento.Add(NumeroDoLancamento)
    End Sub

    Private Class Ouvinte
        Implements IPdfPageEvent

        Public Sub OnChapter(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single, ByVal title As iTextSharp.text.Paragraph) Implements iTextSharp.text.pdf.IPdfPageEvent.OnChapter

        End Sub

        Public Sub OnChapterEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnChapterEnd

        End Sub

        Public Sub OnCloseDocument(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnCloseDocument

        End Sub

        Public Sub OnEndPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnEndPage

        End Sub

        Public Sub OnGenericTag(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal rect As iTextSharp.text.Rectangle, ByVal text As String) Implements iTextSharp.text.pdf.IPdfPageEvent.OnGenericTag

        End Sub

        Public Sub OnOpenDocument(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnOpenDocument

        End Sub

        Public Sub OnParagraph(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnParagraph

        End Sub

        Public Sub OnParagraphEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnParagraphEnd

        End Sub

        Public Sub OnSection(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single, ByVal depth As Integer, ByVal title As iTextSharp.text.Paragraph) Implements iTextSharp.text.pdf.IPdfPageEvent.OnSection

        End Sub

        Public Sub OnSectionEnd(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document, ByVal paragraphPosition As Single) Implements iTextSharp.text.pdf.IPdfPageEvent.OnSectionEnd

        End Sub

        Public Sub OnStartPage(ByVal writer As iTextSharp.text.pdf.PdfWriter, ByVal document As iTextSharp.text.Document) Implements iTextSharp.text.pdf.IPdfPageEvent.OnStartPage

        End Sub

    End Class

End Class