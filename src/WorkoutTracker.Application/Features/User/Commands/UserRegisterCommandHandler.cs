using MediatR;
using WorkoutTracker.Application.Common.DTOs;
using WorkoutTracker.Application.Interfaces;
using UserEntity = WorkoutTracker.Domain.Entities.User;

namespace WorkoutTracker.Application.Features.User.Commands;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserRegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var emailTaken = await _userRepository.ExistsByEmailAsync(request.Email);
        if (emailTaken)
            throw new InvalidOperationException("Email is already in use.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = UserEntity.Create(request.Email, passwordHash, request.FirstName, request.LastName, request.Gender);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user.Email, user.FirstName);

        return new AuthResponseDto(token, user.Email, user.FirstName);
    }
}
