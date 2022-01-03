namespace Feedo.Domain
{
    public class Comment
    {
        public string Id { get; set; }
        public string? OriginalComment { get; set; }
        public string? Username { get; set; }
        public string? UserFullName { get; set; }
        public int SocialNetworkId { get; set; }
        public SocialNetwork? SocialNetwork { get; set; }
    }
}