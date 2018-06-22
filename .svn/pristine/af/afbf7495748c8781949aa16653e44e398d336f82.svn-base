using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3DPrinter.utils
{
    public class EnglishStreamWriter : StreamWriter
    {
        public EnglishStreamWriter(Stream path)
            : base(path, Encoding.ASCII)
        {
            Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        }
        public override IFormatProvider FormatProvider
        {
            get
            {
                return System.Globalization.CultureInfo.InvariantCulture;
            }
        }
    }
}
