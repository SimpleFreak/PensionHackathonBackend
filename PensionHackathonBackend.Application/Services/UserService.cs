using PensionHackathonBackend.Application.Interfaces;
using PensionHackathonBackend.Core.Abstractions;
using PensionHackathonBackend.Core.Models;
using PensionHackathonBackend.Infrastructure.Abstraction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PensionHackathonBackend.Application.Services
{
    /* Класс сервиса пользователя по реализации репозитория пользователя */
    public class UserService(IPasswordHasher passwordHasher,
        IUserRepository userRepository, IJwtProvider jwtProvider) : IUserService
    {
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        /* Метод по обеспечению регистрации нового пользователя */
        public async Task RegistrationUser(string login, string password, string role)
        {
            var hashedPassword = _passwordHasher.Generate(password);

            var (user, error) = User.Create(Guid.NewGuid(),
                login, hashedPassword, role);

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception(error);
            }

            await _userRepository.Create(user);
        }

        /* Проверка логина и пароля */
        public async Task<string> AuthorizationUser(string login, string password)
        {
            var user = await _userRepository.GetByLogin(login);

            var result = _passwordHasher.Verify(password, user.Password);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }

        /* Получение всех пользователей */
        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.Get();
        }

        /* Создание нового пользователя */
        public async Task<Guid> CreateUser(User user)
        {
            return await _userRepository.Create(user);
        }

        /* Обновление данных пользователя */
        public async Task<Guid> UpdateUser(Guid id,
            string login, string password, string role)
        {
            return await _userRepository.Update(id, login, password, role);
        }

        /* Удаление пользователя */
        public async Task<Guid> DeleteUser(Guid id)
        {
            return await _userRepository.Delete(id);
        }
    }
}
