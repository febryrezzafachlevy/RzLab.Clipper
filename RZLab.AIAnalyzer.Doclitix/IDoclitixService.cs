using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZLab.AIAnalyzer.Doclitix
{
    public interface IDoclitixService
    {
        void Process(string documentName, string documentType);
    }
}
