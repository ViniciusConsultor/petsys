using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class SituacaoDoProcesso
    {
        private int _codigoSituacaoProcesso;
        private string _descricaoSituacao;

        // Classificação da situação do processo

        public static SituacaoDoProcesso Situacao1 = new SituacaoDoProcesso(1, "Protocolada");
        public static SituacaoDoProcesso Situacao2 = new SituacaoDoProcesso(2, "Pendente");
        public static SituacaoDoProcesso Situacao3 = new SituacaoDoProcesso(3, "Depositada");
        public static SituacaoDoProcesso Situacao4 = new SituacaoDoProcesso(4, "Indeferida");
        public static SituacaoDoProcesso Situacao5 = new SituacaoDoProcesso(5, "Deferida");
        public static SituacaoDoProcesso Situacao6 = new SituacaoDoProcesso(6, "Registrada");
        public static SituacaoDoProcesso Situacao7 = new SituacaoDoProcesso(7, "Caducidade");
        public static SituacaoDoProcesso Situacao8 = new SituacaoDoProcesso(8, "Abandonada");
        public static SituacaoDoProcesso Situacao9 = new SituacaoDoProcesso(9, "Extinta");
        public static SituacaoDoProcesso Situacao10 = new SituacaoDoProcesso(10, "Arquivada");
        public static SituacaoDoProcesso Situacao11 = new SituacaoDoProcesso(11, "Renov. Pend.");

        private static IList<SituacaoDoProcesso> SituacoesDoProcesso = new List<SituacaoDoProcesso>()
                                                                           {
                                                                               Situacao1,
                                                                               Situacao2,
                                                                               Situacao3,
                                                                               Situacao4,
                                                                               Situacao5,
                                                                               Situacao6,
                                                                               Situacao7,
                                                                               Situacao8,
                                                                               Situacao9,
                                                                               Situacao10,
                                                                               Situacao11
                                                                           };

        private SituacaoDoProcesso(int codigoSituacaoProcesso, string descricao)
        {
            CodigoSituacaoProcesso = codigoSituacaoProcesso;
            DescricaoSituacao = descricao;
        }

        public int CodigoSituacaoProcesso
        {
            get { return _codigoSituacaoProcesso; }
            private set { _codigoSituacaoProcesso = value; }
        }

        public string DescricaoSituacao
        {
            get { return _descricaoSituacao; }
            private set { _descricaoSituacao = value; }
        }

        public static IList<SituacaoDoProcesso> ObtenhaSituacoesDoProcesso()
        {
            return SituacoesDoProcesso;
        }

        //public static SituacaoDoProcesso ObtenhaPorCodigo(int codigo)
        //{
        //    return ObtenhaSituacoesDoProcesso().FirstOrDefault(situacao => situacao.CodigoSituacaoProcesso.Equals(codigo));
        //}

        public static string RetornaDescricaoPorCodigo(int codigo)
        {
            var situacaoDoProcesso = ObtenhaSituacoesDoProcesso().FirstOrDefault(situacao => situacao.CodigoSituacaoProcesso.Equals(codigo));
            
            return situacaoDoProcesso != null ? situacaoDoProcesso.DescricaoSituacao : string.Empty;
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as SituacaoDoProcesso;

            return objeto != null && objeto.CodigoSituacaoProcesso == CodigoSituacaoProcesso;
        }

        public override int GetHashCode()
        {
            return _codigoSituacaoProcesso.GetHashCode();
        }
    }
}
