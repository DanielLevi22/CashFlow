using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Criptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExecptionsBase;

namespace CashFlow.Application.UseCase.Login
{
    public class DoLoginUseCase : IDoLoginUseCase
    {
        private readonly IUserReadOnlyRepository _userRepository;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IAccessTokenGenerator _accessToken;


        public DoLoginUseCase(IAccessTokenGenerator acessToken,
            IPasswordEncripter passwordEncripter,
            IUserReadOnlyRepository userRepostory
           )
        {
            _userRepository = userRepostory;
            _accessToken = acessToken;
            _passwordEncripter = passwordEncripter;
        }



        public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null) { 
                throw new InvalidLoginException();
            }


            var passwordMatch = _passwordEncripter.Verify(request.Password, user.Password);

            if (passwordMatch == false)
            {
                throw new InvalidLoginException();
            }


            return new ResponseRegisteredUserJson
            {
                Name = user.Name,
                Token = _accessToken.Generate(user)
            };
        }
    }
}
