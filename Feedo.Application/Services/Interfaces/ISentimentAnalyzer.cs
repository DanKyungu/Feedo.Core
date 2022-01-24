using Feedo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedo.Application.Services.Interfaces
{
    public interface ISentimentAnalyzer
    {
        Task<IEnumerable<SentimentResult>> GetSentimentScore(Dictionary<string, string> content);
    }
}
