using MediatR;
using WorkoutTracker.Application.Common.DTOs;
using WorkoutTracker.Application.Interfaces;
using UserEntity = WorkoutTracker.Domain.Entities.User;

namespace WorkoutTracker.Application.Features.User.Commands;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, AuthResponseDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserRegisterCommandHandler(IUnitOfWork unitOfWork, IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<AuthResponseDto> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        var emailTaken = await _unitOfWork.UserRepository.ExistsByEmailAsync(request.Email);
        if (emailTaken)
            throw new InvalidOperationException("Email is already in use.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = UserEntity.Create(request.Email, passwordHash, request.FirstName, request.LastName, request.Gender);

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtTokenGenerator.GenerateToken(user.Email, user.FirstName);

        return new AuthResponseDto(token, user.Email, user.FirstName);
    }
}
