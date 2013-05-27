
namespace BB.SmsQuiz.Infrastructure.Authentication
{
    public interface IFormsAuthentication
    { 
        void SetAuthenticationToken(string token);
        string AuthenticationToken { get; }
        void SignOut();
    }
}