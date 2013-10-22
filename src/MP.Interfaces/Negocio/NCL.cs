using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Negocio
{
    [Serializable]
    public class NCL
    {
        private int _codigo;
        private string _descricao;
        private NaturezaDeMarca _natureza;

        //Classificações de produto
        public static NCL NCL1 = new NCL(1, "Substâncias químicas destinadas à indústria, às ciências, à fotografia, assim como à agricultura, à horticultura e à silvicultura; resinas artificiais não-processadas, matérias plásticas não processadas; adubo; composições extintoras de fogo; preparações para temperar e soldar; substâncias químicas destinadas a conservar alimentos; substâncias tanantes; substâncias adesivas destinados à indústria.", NaturezaDeMarca.DeProduto);
        public static NCL NCL2 = new NCL(2, "Tintas, vernizes, lacas; preservativos contra oxidação e contra deterioração da madeira; matérias tintoriais; mordentes; resinas naturais em estado bruto; metais em folhas e em pó para pintores, decoradores, impressores e artistas.", NaturezaDeMarca.DeProduto);
        public static NCL NCL3 = new NCL(3, "Preparações para branquear e outras substâncias para uso em lavanderia; produtos para limpar, polir e decapar; produtos abrasivos; sabões; perfumaria, óleos essenciais, cosméticos, loções para os cabelos; dentifrícios.", NaturezaDeMarca.DeProduto);
        public static NCL NCL4 = new NCL(4, "Graxas e óleos industriais; lubrificantes; produtos para absorver, molhar e ligar pó; combustíveis (incluindo gasolina para motores) e materiais para iluminação; velas e pavios para iluminação.", NaturezaDeMarca.DeProduto);
        public static NCL NCL5 = new NCL(5, "Preparações farmacêuticas e veterinárias; preparações higiênicas para uso medicinal; substâncias dietéticas adaptadas para uso medicinal, alimentos para bebês; emplastros, materiais para curativos; material para obturações dentárias, cera dentária; desinfetantes; preparações para destruição de vermes; fungicidas, herbicidas.", NaturezaDeMarca.DeProduto);
        public static NCL NCL6 = new NCL(6, "Metais comuns e suas ligas; materiais de metal para construção; construções transportáveis de metal; materiais de metal para vias férreas; cabos e fios de metal comum não elétricos; serralharia, pequenos artigos de ferragem; canos e tubos de metal; cofres; produtos de metal comum não incluídos em outras classes; minérios.", NaturezaDeMarca.DeProduto);
        public static NCL NCL7 = new NCL(7, "Máquinas e ferramentas mecânicas; motores (exceto para veículos terrestres); e engates de máquinas e componentes de transmissão (exceto para veículos terrestres); instrumentos agrícolas não manuais; chocadeiras.", NaturezaDeMarca.DeProduto);
        public static NCL NCL8 = new NCL(8, "Ferramentas e instrumentos manuais (propulsão muscular); cutelaria; armas brancas; aparelhos de barbear.", NaturezaDeMarca.DeProduto);
        public static NCL NCL9 = new NCL(9, "Aparelhos e instrumentos científicos, náuticos, geodésicos, fotográficos, cinematográficos, ópticos, de pesagem, de medição, de sinalização, de controle (inspeção), de salvamento e de ensino; aparelhos e instrumentos para conduzir, interromper, transformar, acumular, regular ou controlar eletricidade; aparelhos para registrar, transmitir ou reproduzir som ou imagens; suporte de registro magnético, discos acústicos; máquinas distribuidoras automáticas e mecanismos para aparelhos operados com moedas; caixas registradoras, máquinas de calcular, equipamento de processamento de dados e computadores; aparelhos extintores de incêndio.", NaturezaDeMarca.DeProduto);
        public static NCL NCL10 = new NCL(10, "Aparelhos e instrumentos cirúrgicos, médicos, odontológicos e veterinários, membros, olhos e dentes artificiais; artigos ortopédicos; material de sutura.", NaturezaDeMarca.DeProduto);
        public static NCL NCL11 = new NCL(11, "Aparelhos para iluminação, aquecimento, produção de vapor, cozinhar, refrigeração, secagem, ventilação, fornecimento de água e para fins sanitários.", NaturezaDeMarca.DeProduto);
        public static NCL NCL12 = new NCL(12, "Veículos; aparelhos para locomoção por terra, ar ou água.", NaturezaDeMarca.DeProduto);
        public static NCL NCL13 = new NCL(13, "Armas de fogo; munições e projéteis; explosivos; fogos de artifício.", NaturezaDeMarca.DeProduto);
        public static NCL NCL14 = new NCL(14, "Metais preciosos e suas ligas e produtos nessas matérias ou folheados, não incluídos em outras classes; jóias, bijuteria, pedras preciosas; relojoaria e instrumentos cronométricos.", NaturezaDeMarca.DeProduto);
        public static NCL NCL15 = new NCL(15, "Instrumentos musicais.", NaturezaDeMarca.DeProduto);
        public static NCL NCL16 = new NCL(16, "Papel, papelão e produtos feitos desses materiais e não incluídos em outras classes; material impresso; artigos para encadernação; fotografias; papelaria; adesivos para papelaria ou uso doméstico; materiais para artistas; pincéis; máquinas de escrever e material de escritório (exceto móveis); material de instrução e didático (exceto aparelhos); matérias plásticas para embalagem (não incluídas em outras classes); caracteres de imprensa; clichês.", NaturezaDeMarca.DeProduto);
        public static NCL NCL17 = new NCL(17, "Borracha, guta-percha, goma, amianto, mica e produtos feitos com estes materiais e não incluídos em outras classes; produtos em matérias plásticas semiprocessadas; materiais para calafetar, vedar e isolar; canos flexíveis, não metálicos.", NaturezaDeMarca.DeProduto);
        public static NCL NCL18 = new NCL(18, "Couro e imitações de couros, produtos nessas matérias não incluídos em outras classes; peles de animais; malas e bolsas de viagem; guarda-chuvas, guarda-sóis e bengalas; chicotes, arreios e selaria.", NaturezaDeMarca.DeProduto);
        public static NCL NCL19 = new NCL(19, "Materiais de construção (não metálicos); canos rígidos não metálicos para construção; asfalto, piche e betume; construções transportáveis não metálicas; monumentos não metálicos.", NaturezaDeMarca.DeProduto);
        public static NCL NCL20 = new NCL(20, "Móveis, espelhos, molduras; produtos (não incluídos em outras classes), de madeira, cortiça, junco, cana, vime, chifre, marfim, osso, barbatana de baleia, concha, tartaruga, âmbar, madrepérola, espuma-do-mar e sucedâneos de todas estas matérias ou de matérias plásticas.", NaturezaDeMarca.DeProduto);
        public static NCL NCL21 = new NCL(21, "Utensílios e recipientes para a casa ou cozinha (não de metal precioso ou folheado); pentes e esponjas; escovas (exceto para pintura); materiais para fabricação de escovas; materiais de limpeza; palha de aço; vidro não trabalhado ou semitrabalhado (exceto para construção); artigos de vidro, porcelana e louça de faiança não incluídos em outras classes.", NaturezaDeMarca.DeProduto);
        public static NCL NCL22 = new NCL(22, "Cordas, fios, redes, tendas, toldos, oleados, velas, sacos, sacolas (não incluídos em outras classes); matérias de enchimento (exceto borrachas e plásticos); matérias têxteis fibrosas em bruto.", NaturezaDeMarca.DeProduto);
        public static NCL NCL23 = new NCL(23, "Fios para uso têxtil.", NaturezaDeMarca.DeProduto);
        public static NCL NCL24 = new NCL(24, "Tecidos e produtos têxteis, não incluídos em outras classes; coberturas de cama e mesa.", NaturezaDeMarca.DeProduto);
        public static NCL NCL25 = new NCL(25, "Vestuário, calçados e chapelaria.", NaturezaDeMarca.DeProduto);
        public static NCL NCL26 = new NCL(26, "Rendas e bordados, fitas e laços; botões, colchetes e ilhós, alfinetes e agulhas; flores artificiais.", NaturezaDeMarca.DeProduto);
        public static NCL NCL27 = new NCL(27, "Carpetes, tapetes, capachos e esteiras, linóleo e outros revestimentos de assoalhos; colgaduras que não sejam em matérias têxteis.", NaturezaDeMarca.DeProduto);
        public static NCL NCL28 = new NCL(28, "Jogos e brinquedos; artigos para ginástica e esporte não incluídos em outras classes; decorações para árvores de Natal.", NaturezaDeMarca.DeProduto);
        public static NCL NCL29 = new NCL(29, "Carne, peixe, aves e caça; extratos de carne; frutas, legumes e verduras em conserva, secos e cozidos; geléias, doces e compotas; ovos, leite e laticínio; óleos e gorduras comestíveis.", NaturezaDeMarca.DeProduto);
        public static NCL NCL30 = new NCL(30, "Café, chá, cacau, açúcar, arroz, tapioca, sagu, sucedâneos de café; farinhas e preparações feitas de cereais, pão, massas e confeitos, sorvetes; mel, xarope de melaço; lêvedo, fermento em pó; sal, mostarda; vinagre, molhos (condimentos); especiarias; gelo.", NaturezaDeMarca.DeProduto);
        public static NCL NCL31 = new NCL(31, "Produtos agrícolas, hortícolas, florestais e grãos não incluídos em outras classes; animais vivos; frutas, legumes e verduras frescos; sementes, plantas e flores naturais; alimentos para animais, malte.", NaturezaDeMarca.DeProduto);
        public static NCL NCL32 = new NCL(32, "Cervejas; águas minerais e gasosas e outras bebidas não alcoólicas; bebidas de frutas e sucos de fruta; xaropes e outras preparações para fabricar bebidas.", NaturezaDeMarca.DeProduto);
        public static NCL NCL33 = new NCL(33, "Bebidas alcoólicas (exceto cervejas).", NaturezaDeMarca.DeProduto);
        public static NCL NCL34 = new NCL(34, "Tabaco; artigos para fumantes; fósforos.", NaturezaDeMarca.DeProduto);

        //Classificações de serviços
        public static NCL NCL35 = new NCL(35, "Propaganda; gestão de negócios; administração de negócios; funções de escritório.", NaturezaDeMarca.DeServico);
        public static NCL NCL36 = new NCL(36, "Seguros; negócios financeiros; negócios monetários; negócios imobiliários.", NaturezaDeMarca.DeServico);
        public static NCL NCL37 = new NCL(37, "Construção civil; reparos; serviços de instalação.", NaturezaDeMarca.DeServico);
        public static NCL NCL38 = new NCL(38, "Telecomunicações.", NaturezaDeMarca.DeServico);
        public static NCL NCL39 = new NCL(39, "Transporte; embalagem e armazenagem de produtos; organização de viagens.", NaturezaDeMarca.DeServico);
        public static NCL NCL40 = new NCL(40, "Tratamento de materiais.", NaturezaDeMarca.DeServico);
        public static NCL NCL41 = new NCL(41, "Educação, provimento de treinamento; entretenimento; atividades desportivas e culturais.", NaturezaDeMarca.DeServico);
        public static NCL NCL42 = new NCL(42, "Serviços científicos e tecnológicos, pesquisa e desenho relacionados a estes; serviços de análise industrial e pesquisa; concepção, projeto e desenvolvimento de hardware e software de computador; serviços jurídicos.", NaturezaDeMarca.DeServico);
        public static NCL NCL43 = new NCL(43, "Serviços de fornecimento de comida e bebida; acomodações temporárias.", NaturezaDeMarca.DeServico);
        public static NCL NCL44 = new NCL(44, "Serviços médicos; serviços veterinários; serviços de higiene e beleza para seres humanos ou animais; serviços de agricultura, de horticultura e de silvicultura.", NaturezaDeMarca.DeServico);
        public static NCL NCL45 = new NCL(45, "Serviços pessoais e sociais prestados por terceiros, para satisfazer necessidades de indivíduos; serviços de segurança para proteção de bens e pessoas.", NaturezaDeMarca.DeServico);


        private static IList<NCL> NCLsDeProduto = new List<NCL>()
                                                      {
                                                          NCL1,
                                                          NCL2,
                                                          NCL3,
                                                          NCL4,
                                                          NCL5,
                                                          NCL6,
                                                          NCL7,
                                                          NCL8,
                                                          NCL9,
                                                          NCL10,
                                                          NCL11,
                                                          NCL12,
                                                          NCL13,
                                                          NCL14,
                                                          NCL15,
                                                          NCL16,
                                                          NCL17,
                                                          NCL18,
                                                          NCL19,
                                                          NCL20,
                                                          NCL21,
                                                          NCL22,
                                                          NCL23,
                                                          NCL24,
                                                          NCL25,
                                                          NCL26,
                                                          NCL27,
                                                          NCL28,
                                                          NCL29,
                                                          NCL30,
                                                          NCL31,
                                                          NCL32,
                                                          NCL33,
                                                          NCL34,
                                                          NCL34
                                                      };

        private static IList<NCL> NCLsDeServico = new List<NCL>()
                                                      {
                                                          NCL35,
                                                          NCL36,
                                                          NCL37,
                                                          NCL38,
                                                          NCL39,
                                                          NCL40,
                                                          NCL41,
                                                          NCL42,
                                                          NCL43,
                                                          NCL44,
                                                          NCL45
                                                      };
        
        private NCL(int codigo, string descricao, NaturezaDeMarca natureza)
        {
            Codigo = codigo;
            Descricao = descricao;
            NaturezaDeMarca = natureza;
        }

        public int Codigo
        {
            get { return _codigo; }
            private set { _codigo = value; }
        }

        public string Descricao
        {
            get { return _descricao; }
            private set { _descricao = value; }
        }

        public NaturezaDeMarca NaturezaDeMarca
        {
            get { return _natureza; }
            private set { _natureza = value; }
        }

        public static IList<NCL> ObtenhaTodas()
        {
            var todas = new List<NCL>();

            todas.AddRange(NCLsDeProduto);
            todas.AddRange(NCLsDeServico);
            
            return todas;

        }

        public static IList<NCL> ObtenhaNCLsDeServico()
        {
            return NCLsDeServico;
        }

        public static IList<NCL> ObtenhaNCLsDeProduto()
        {
            return NCLsDeProduto;
        } 

        public static NCL ObtenhaPorCodigo(int codigo)
        {
            return ObtenhaTodas().FirstOrDefault(ncl => ncl.Codigo.Equals(codigo));
        }

        public override bool Equals(object obj)
        {
            var objeto = obj as NCL;

            return objeto != null && objeto.Codigo == Codigo;
        }

        public override int GetHashCode()
        {
            return _codigo.GetHashCode();
        }
    }
}
