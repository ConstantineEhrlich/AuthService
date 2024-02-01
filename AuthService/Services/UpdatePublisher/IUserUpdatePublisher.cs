using AuthService.Services.DTO;

namespace AuthService.Services.UpdatePublisher;

public interface IUserUpdatePublisher
{
    void NewUserCreated(SignUp signUpData);
}