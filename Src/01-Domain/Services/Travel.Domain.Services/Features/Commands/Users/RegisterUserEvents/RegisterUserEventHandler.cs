using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Events;

namespace Travel.Domain.Service.Features.Commands.Users.RegisterUserEvents;

public class RegisterUserEventHandler : INotificationHandler<RegisterUserEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IProfileRepository _profileRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserEventHandler(IProfileRepository profileRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _profileRepository = profileRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task Handle(RegisterUserEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithProfileById(notification.UserId, cancellationToken);
        if (user == null)
            return;

        user.CreateProfile();

        await _userRepository.UpdateUserProfile(user, cancellationToken);
    }
}
