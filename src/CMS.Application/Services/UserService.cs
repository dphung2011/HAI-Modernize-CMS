using AutoMapper;
using CMS.Application.DTOs;
using CMS.Application.Interfaces;
using CMS.Domain.Entities;
using CMS.Domain.Interfaces;

namespace CMS.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user != null ? _mapper.Map<UserDto>(user) : null;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
    {
        // Check if username is unique
        var isUnique = await _userRepository.IsUsernameUniqueAsync(createUserDto.Username);
        if (!isUnique)
        {
            throw new InvalidOperationException($"Username '{createUserDto.Username}' is already taken.");
        }

        // Map DTO to entity
        var user = _mapper.Map<User>(createUserDto);
        
        // In a real application, you would hash the password here
        // user.Password = _passwordHasher.HashPassword(createUserDto.Password);

        // Save to database
        var createdUser = await _userRepository.AddAsync(user);
        
        // Return the created user as DTO
        return _mapper.Map<UserDto>(createdUser);
    }

    public async Task UpdateUserAsync(int id, UpdateUserDto updateUserDto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        // Update properties
        _mapper.Map(updateUserDto, user);
        
        // If password is provided, hash it
        if (!string.IsNullOrEmpty(updateUserDto.Password))
        {
            // In a real application, you would hash the password here
            // user.Password = _passwordHasher.HashPassword(updateUserDto.Password);
            user.Password = updateUserDto.Password;
        }

        // Save changes
        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {id} not found.");
        }

        await _userRepository.DeleteAsync(user);
    }
}