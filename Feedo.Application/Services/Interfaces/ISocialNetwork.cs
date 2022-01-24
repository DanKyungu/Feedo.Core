using Feedo.Domain.Models;
using LinqToTwitter.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedo.Application.Services.Interfaces
{
    public interface ISocialNetwork
    {
        Task<IEnumerable<SocialComment>> GetSocialCommentByKeywork(string keyword);
        Task<ApplicationOnlyAuthorizer> GetAuthorization();
    }
}
