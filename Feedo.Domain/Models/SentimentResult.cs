using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedo.Domain.Models
{
    public class SentimentResult
    {
        public double Score { get; set; }
        public int Sentiment { get; set; }
    }
}
