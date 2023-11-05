namespace WhoamI.Business.Contracts.DTO.Client
{
    public interface IClientContext
    {
        string Token { get; set; }
        long UserId { get; }
        string Culture { get; }


        void SetToken(string token);
        void SetUserId(long userId);
        void SetCulture(string culture);

    }

    public class ClientContext : IClientContext
    {
        public string Token { get; set; }
        public long UserId { get; private set; }
        public string Culture { get; private set; }

        public ClientContext()
        {
            Token = default(string);
            UserId = 0;
            Culture = "tr-TR";
        }

        public void SetToken(string token)
        {
            Token = token;
        }

        public void SetUserId(long userId)
        {
            UserId = userId;
        }

        public void SetCulture(string culture)
        {
            Culture = culture ?? "tr-TR";
        }       
    }
}
