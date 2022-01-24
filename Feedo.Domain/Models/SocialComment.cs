using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedo.Domain.Models
{
    public class SocialComment
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? FullUsername { get; set; }
        public string? Comment { get; set; }
    }
}
