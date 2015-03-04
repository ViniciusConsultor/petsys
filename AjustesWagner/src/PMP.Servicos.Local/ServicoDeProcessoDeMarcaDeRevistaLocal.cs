using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Compartilhados;
using Compartilhados.Fabricas;
using PMP.Interfaces.Servicos;
using System.IO;
using PMP.Interfaces.Utilidades;

namespace PMP.Servicos.Local
{
    public class ServicoDeProcessoDeMarcaDeRevistaLocal : Servico, IServicoDeProcessoDeMarcaDeRevista
    {
        public ServicoDeProcessoDeMarcaDeRevistaLocal(ICredencial Credencial)
            : base(Credencial)
        {
        }

        public void ProcesseEmLote(string pastaDeArmazenamentoDasRevistas)
        {
            if (Directory.Exists(pastaDeArmazenamentoDasRevistas))
                throw new ApplicationException("Pasta informada não existe!");


            var informacoesDoDiretorio = new DirectoryInfo(pastaDeArmazenamentoDasRevistas);
            var arquivosAProcessar = informacoesDoDiretorio.GetFiles();
            var listaDeProcessosDeMarcasDeRevista = new List<DTOProcessoMarcaRevista>();

            foreach (var arquivo in arquivosAProcessar)
                listaDeProcessosDeMarcasDeRevista.AddRange(ExtraiaProcessoDeMarcasDeRevistaDoArquivo(arquivo.FullName));
        }

        private IList<DTOProcessoMarcaRevista> ExtraiaProcessoDeMarcasDeRevistaDoArquivo(string caminhoCompletoDoArquivo)
        {
            //Verificar se arquivo é um txt, se for converter primeiro

            var arquivoDaRevista = XDocument.Load(caminhoCompletoDoArquivo);
            var conteudoDaRevista = arquivoDaRevista.Element("revista");
            var numeroDaRevista = (int) conteudoDaRevista.Attribute("numero");
            var dataDaPublicacaoDaRevista = (DateTime) conteudoDaRevista.Attribute("data");

            var processosNaRevista = (from conteudoProcesso in conteudoDaRevista.Elements("processo")
                select new DTOProcessoMarcaRevista
                {
                    NumeroDaRevista = numeroDaRevista,
                    DataDePublicacaoDaRevista = dataDaPublicacaoDaRevista,
                    NumeroProcessoDeMarca = (long) conteudoProcesso.Attribute("numero"),
                    DataDaConcessao = conteudoProcesso.Attribute("data-concessao") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-concessao").Value),
                    DataDaVigencia = conteudoProcesso.Attribute("data-vigencia") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-vigencia").Value),
                    DataDoDeposito = conteudoProcesso.Attribute("data-deposito") == null ? (DateTime?) null : DateTime.Parse(conteudoProcesso.Attribute("data-deposito").Value),
                    CodigoDoDespacho = conteudoProcesso.Element("despachos").Element("despacho").Attribute("codigo").Value,
                    NomeDoDespacho =  conteudoProcesso.Element("despachos").Element("despacho").Attribute("nome").Value,
                    TextoComplementarDoDespacho =  conteudoProcesso.Element("despachos").Element("despacho").Element("texto-complementar") == null ? null :  conteudoProcesso.Element("despachos").Element("despacho").Element("texto-complementar").Value,

                    



                //StudentName = _student.Element("name").Value,
                //Batch = _student.Element("batch").Value,
                //School = _student.Element("school").Value,
                //objMarkList = (from _marks in _student.Element("marks").Elements("mark")
                //               select new Mark
                //               {
                //                   Term = _marks.Element("term").Value,
                //                   Science = _marks.Element("science").Value,
                //                   Mathematics = _marks.Element("mathematics").Value,
                //                   Language = _marks.Element("language").Value,
                //                   Result = _marks.Element("result").Value,
                //                   objComment = (from _cmt in _marks.Elements("comments")
                //                                 select new Comment
                //                                 {
                //                                     TeacherComment = _cmt.Element("teacher").Value,
                //                                     ParentComment = _cmt.Element("parent").Value
                //                                 }).FirstOrDefault(),
                //               }).ToList()
                }).ToList();
                

            return processosNaRevista;

        }



    }
}
