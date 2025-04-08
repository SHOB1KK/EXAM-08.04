using Domain.Entities;
using Domain.Entities.DTOs;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IUserService
{
    Task<Response<List<User>>> GetAllUsersAsync();
    Task<Response<User>> GetUserByIdAsync(int id);
    Task<Response<User>> CreateUserAsync(CreateUserDto createUserDto);
    Task<Response<User>> UpdateUserAsync(int id, UpdateUserDto updateUserDto);
    Task<Response<string>> DeleteUserAsync(int id);
}