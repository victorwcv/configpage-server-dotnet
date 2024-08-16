using backend_dotnet.Models;
using MongoDB.Driver;

namespace backend_dotnet.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IMongoDatabase database)
        {
            _userCollection = database.GetCollection<User>("Users");
        }

        public async Task<User?> GetUserAsync(string id) =>
            await _userCollection.Find(user => user.Id == id).FirstOrDefaultAsync();

        public async Task UpdateUserAsync(string id, User updatedUser)
        {
            var filter = Builders<User>.Filter.Eq(user => user.Id, id);
            await _userCollection.ReplaceOneAsync(filter, updatedUser);
        }
        public async Task CreateUserAsync(User newUser) =>
           await _userCollection.InsertOneAsync(newUser);

    }
}
