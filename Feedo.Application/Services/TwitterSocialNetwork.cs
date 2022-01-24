using Feedo.Application.Services.Interfaces;
using Feedo.Domain.Models;
using LinqToTwitter;
using LinqToTwitter.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feedo.Application.Services
{
    public class TwitterSocialNetwork : ISocialNetwork
    {
        public async Task<ApplicationOnlyAuthorizer> GetAuthorization()
        {
            var auth = new ApplicationOnlyAuthorizer();
            try
            {
                auth.CredentialStore = new InMemoryCredentialStore()
                {
                    ConsumerKey = "URfypDISLj8XL8R3hd3KwwFm4",
                    ConsumerSecret = "82jU6Ei3qcSOeBfJ6CGzdDjquBzLCLV4MnFbTKS1OiTUb6M2CI"
                };
                await auth.AuthorizeAsync();
            }
            catch (System.Exception)
            {
            }

            return auth;
        }

        public async Task<IEnumerable<SocialComment>> GetSocialCommentByKeywork(string keyword)
        {
            var auth = await GetAuthorization();
            var comments = new List<SocialComment>();

            using (var twitterCtx = new TwitterContext(auth))
            {
                //Log
                twitterCtx.Log = Console.Out;
                var searchResponse =
                await
                (from search in twitterCtx.Search
                 where search.Type == SearchType.Search &&
                       search.Query == "\"bralima\""
                 select search)
                .SingleOrDefaultAsync();
                if (searchResponse != null && searchResponse.Statuses != null)
                    searchResponse.Statuses.ForEach(tweet =>
                    {
                        if (tweet is null) return;

                        comments.Add(new SocialComment()
                        {
                            Id = tweet.StatusID.ToString(),
                            Comment = tweet.Text,
                            Username = tweet.User?.Name,
                            FullUsername = tweet.User?.ScreenNameResponse,
                        });
                    });
            }

            return comments;
        }
    }
}
