using BB.SmsQuiz.Infrastructure.Authentication;

namespace BB.SmsQuiz.Api.Infrastructure
{
    public class FakeTokenAuthentication : ITokenAuthentication
    {
        public bool IsValid(string token)
        {
            return (token == "teddybear");
        }

        public string Token
        {
            get { return "teddybear"; }
        }
    }
}