using MediatR;
using WorkoutTracker.Application.Common.DTOs;
using WorkoutTracker.Application.Interfaces;

namespace WorkoutTracker.Application.Features.Users.Commands.Login;

public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserLoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(UserLoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.FirstName);

        return new AuthResponseDto(token, user.Email, user.FirstName);
    }
}
