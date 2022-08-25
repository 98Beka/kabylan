using AutoMapper;
using Kabylan.BLL.DataTransferObjects;
using Kabylan.BLL.Infrastructure;
using Kabylan.BLL.Interfaces;
using Kabylan.DAL.Interfaces;
using Kabylan.DAL.Repository;
using Kabylan.DAL.Models;
using Kabylan.BLL.Profiles;

namespace Kabylan.BLL.Services {
    public class UserService : IService<UserDTO> {
        private readonly IUnitOfWork _database;
        private readonly IMapper _mapper;
        public UserService(string connectionString) {
            _database = new EFUnitOfWork(connectionString);
            _mapper = new MapperConfiguration(c => {
                c.AddProfile<MapperConfig>();
            }).CreateMapper();
        }

        public async Task<UserDTO> GetAsync(int id) {
            var User = await _database.Users.Get(id);
            if (User == null)
                throw new ValidationException("User not found", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<UserDTO>(User);
        }

        public IEnumerable<UserDTO> GetAll() {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(_database.Users.GetAll());
        }

        public UserDTO Create() {
            var user = new User();
            _database.Users.Create(user);
            _database.Save();
            return _mapper.Map<UserDTO>(user);
        }


        public async Task EditAsync(UserDTO User) {
            if (User == null)
                throw new ValidationException("User = null", "");
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()).CreateMapper();
            var oldUser = await _database.Users.Get(User.Id);
            mapper.Map(User, oldUser);
            _database.Save();
        }
        public async Task DeleteAsync(int id) {
            var User = await _database.Users.Get(id);
            if (User == null)
                throw new ValidationException("User not found", "");
            _database.Users.Delete(id);
            _database.Save();
        }

    }
}
