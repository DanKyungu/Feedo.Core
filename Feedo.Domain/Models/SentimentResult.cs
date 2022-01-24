using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedo.Domain.Models
{
    public class SentimentResult
    {
        public string CommentId { get; set; }
        public double Score { get; set; }
        public string Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NegativeScore { get; set; }
        public double NeutralScore { get; set; }
    }
}
