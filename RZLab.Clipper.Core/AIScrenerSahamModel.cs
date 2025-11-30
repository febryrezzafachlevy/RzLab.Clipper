using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZLab.Clipper.Core
{
    public class AIScrenerSahamModel
    {
        public List<EmitenScrenerSahamModel> Emitens { get; set; }
    }
    public class EmitenScrenerSahamModel
    {
        public string Name { get; set; } = string.Empty;
        public int CurrentValue { get; set; }
    }
    public class EmitenScrenerSahamResultModel
    {
        public string Emiten { get; set; } = string.Empty;
        public int Entry { get; set; }
        public int TP1 { get; set; }
        public int TP2 { get; set; }
        public int TP3 { get; set; }
        public int SL { get; set; }
        public string Confidence { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;

        // Multibagger extension
        public int MultibaggerScore { get; set; }            // 0 - 100
        public string MoatType { get; set; } = string.Empty; // e.g., "Brand", "Network", "Cost Advantage"
        public string GrowthType { get; set; } = string.Empty; // "Early S-Curve", "Expansion", "Mature"
        public string RiskLevel { get; set; } = string.Empty; // Low / Medium / High

        // RR
        public double RR1 { get; set; }
        public double RR2 { get; set; }
        public double RR3 { get; set; }
    }
}
