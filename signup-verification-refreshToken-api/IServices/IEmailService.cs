namespace signup_verification_refreshToken_api.IServices
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
