

using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Response;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Criptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception;
using CashFlow.Exception.ExecptionsBase;

namespace CashFlow.Application.UseCase.Users.Register
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IMapper _iMapper;
        private readonly IPasswordEncripter _passwordEncripter;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccessTokenGenerator _accessToken;
        public RegisterUserUseCase(
            IMapper iMapper,
            IPasswordEncripter passwordEncripter,
            IUserReadOnlyRepository userReadOnlyRepository,
            IUserWriteOnlyRepository userWriteOnlyRepository,
            IUnitOfWork unitOfWork,
            IAccessTokenGenerator accessToken
           )
        {

            _iMapper = iMapper;
            _passwordEncripter = passwordEncripter;
           _userReadOnlyRepository = userReadOnlyRepository;
            _userWriteOnlyRepository = userWriteOnlyRepository;
            _unitOfWork = unitOfWork;
            _accessToken = accessToken;
        }

        public async Task<ResponseRegisteredUserJson> Execute(RequestUserJson request)
        {
            await Validated(request);
            var user = _iMapper.Map<Domain.Entities.User>(request);
            user.Password = _passwordEncripter.Encript(request.Password);
            user.UserIndetifier = Guid.NewGuid();

            await _userWriteOnlyRepository.Add(user);

            await _unitOfWork.Commit();

           return new ResponseRegisteredUserJson { 
            Name = user.Name,
            Token = _accessToken.Generate(user)
           };

        }


        private async Task Validated(RequestUserJson request)
        {
            var validator = new RegisterUserValidator();
            var validationResult = validator.Validate(request);

            var emailExists = await _userReadOnlyRepository.ExistsActiveUserWithEmail(request.Email);


            if (emailExists)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
            }


            if (!validationResult.IsValid)
            {

                var errosMessages = validationResult.Errors.Select(err => err.ErrorMessage).ToList();
                throw new ErrorOnValidationExcption(errosMessages);
            }

        }

    }
}
