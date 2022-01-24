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
        Task<SentimentResult> GetSentimentScore(string content);
    }
}
