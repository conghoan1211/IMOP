using Microsoft.EntityFrameworkCore;
using zaloclone_test.Models;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface ISearchService
    {
        public Task<(string msg, ProfileVM? result)> GetListUsers(string userID);
    }

    
    public class SearchService : ISearchService
    {

        private readonly Zalo_CloneContext _context;
        public SearchService(Zalo_CloneContext context)
        {
            _context = context;
        }

        public Task<(string msg, ProfileVM? result)> GetListUsers(string userID)
        {
            throw new NotImplementedException();
        }

        
    }
}
