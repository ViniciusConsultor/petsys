using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MP.Interfaces.Utilidades
{
    public class UtilidadesDeStream
    {
        public static StreamReader ConvertaArquivoAnsiParaUtf8(Stream arquivoTxtAnsi)
        {
            var bytes = default(byte[]);

            using (var memstream = new MemoryStream())
            {
                arquivoTxtAnsi.CopyTo(memstream);
                bytes = memstream.ToArray();
            }

            var arquivoConvertido = new MemoryStream(Encoding.Convert(Encoding.Default, Encoding.UTF8, bytes));
            return new StreamReader(arquivoConvertido, Encoding.UTF8, true);
        }
    }
}
