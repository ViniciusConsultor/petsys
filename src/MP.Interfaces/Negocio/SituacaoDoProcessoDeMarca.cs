using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class SituacaoDoProcessoDeMarca
    {
        private int _codigoSituacaoProcesso;
        private string _descricaoSituacao;

        // Classificação da situação do processo

        public static SituacaoDoProcessoDeMarca Protocolada = new SituacaoDoProcessoDeMarca(1, "Protocolada");
        public static SituacaoDoProcessoDeMarca Pendente = new SituacaoDoProcessoDeMarca(2, "Pendente");
        public static SituacaoDoProcessoDeMarca Depositada = new SituacaoDoProcessoDeMarca(3, "Depositada");
        public static SituacaoDoProcessoDeMarca Indeferida = new SituacaoDoProcessoDeMarca(4, "Indeferida");
        public static SituacaoDoProcessoDeMarca Deferida = new SituacaoDoProcessoDeMarca(5, "Deferida");
        public static SituacaoDoProcessoDeMarca Registrada = new SituacaoDoProcessoDeMarca(6, "Registrada");
        public static SituacaoDoProcessoDeMarca Caducidade = new SituacaoDoProcessoDeMarca(7, "Caducidade");
        public static SituacaoDoProcessoDeMarca Abandonada = new SituacaoDoProcessoDeMarca(8, "Abandonada");
        public static SituacaoDoProcessoDeMarca Extinta = new SituacaoDoProcessoDeMarca(9, "Extinta");
        public static SituacaoDoProcessoDeMarca Arquivada = new SituacaoDoProcessoDeMarca(10, "Arquivada");
        public static SituacaoDoProcessoDeMarca RenovPend = new SituacaoDoProcessoDeMarca(11, "Renov. Pend.");

        private static IList<SituacaoDoProcessoDeMarca> SituacoesDoProcesso = new List<SituacaoDoProcessoDeMarca>()
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

        private SituacaoDoProcessoDeMarca(int codigoSituacaoProcesso, string descricao)
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

        public static IList<SituacaoDoProcessoDeMarca> ObtenhaSituacoesDoProcesso()
        {
            return SituacoesDoProcesso;
        }

        public static SituacaoDoProcessoDeMarca ObtenhaPorCodigo(int codigo)
        {
            return ObtenhaSituacoesDoProcesso().FirstOrDefault(situacao => situacao.CodigoSituacaoProcesso.Equals(codigo));
        }
        
        public override bool Equals(object obj)
        {
            var objeto = obj as SituacaoDoProcessoDeMarca;

            return objeto != null && objeto.CodigoSituacaoProcesso == CodigoSituacaoProcesso;
        }

        public override int GetHashCode()
        {
            return _codigoSituacaoProcesso.GetHashCode();
        }
    }
}
