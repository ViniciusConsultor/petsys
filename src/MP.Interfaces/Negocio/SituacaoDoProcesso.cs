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

        public static SituacaoDoProcesso Protocolada = new SituacaoDoProcesso(1, "Protocolada");
        public static SituacaoDoProcesso Pendente = new SituacaoDoProcesso(2, "Pendente");
        public static SituacaoDoProcesso Depositada = new SituacaoDoProcesso(3, "Depositada");
        public static SituacaoDoProcesso Indeferida = new SituacaoDoProcesso(4, "Indeferida");
        public static SituacaoDoProcesso Deferida = new SituacaoDoProcesso(5, "Deferida");
        public static SituacaoDoProcesso Registrada = new SituacaoDoProcesso(6, "Registrada");
        public static SituacaoDoProcesso Caducidade = new SituacaoDoProcesso(7, "Caducidade");
        public static SituacaoDoProcesso Abandonada = new SituacaoDoProcesso(8, "Abandonada");
        public static SituacaoDoProcesso Extinta = new SituacaoDoProcesso(9, "Extinta");
        public static SituacaoDoProcesso Arquivada = new SituacaoDoProcesso(10, "Arquivada");
        public static SituacaoDoProcesso RenovPend = new SituacaoDoProcesso(11, "Renov. Pend.");

        private static IList<SituacaoDoProcesso> SituacoesDoProcesso = new List<SituacaoDoProcesso>()
                                                                           {
                                                                               Protocolada,
                                                                               Pendente,
                                                                               Depositada,
                                                                               Indeferida,
                                                                               Deferida,
                                                                               Registrada,
                                                                               Caducidade,
                                                                               Abandonada,
                                                                               Extinta,
                                                                               Arquivada,
                                                                               RenovPend
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

        public static SituacaoDoProcesso ObtenhaPorCodigo(int codigo)
        {
            return ObtenhaSituacoesDoProcesso().FirstOrDefault(situacao => situacao.CodigoSituacaoProcesso.Equals(codigo));
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
