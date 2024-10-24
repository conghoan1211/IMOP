using Microsoft.EntityFrameworkCore;
using zaloclone_test.Models;
using zaloclone_test.ViewModels;

namespace zaloclone_test.Services
{
    public interface IAsideContactService
    {
        Task<List<AsideContactVM>> GetFriendsList(string userId);
        Task<List<AsideContactVM>> FilterFriends(string userId, FriendFilterModel filter);
        Task<FriendOperationResult> DeleteFriend(string userId, string friendId);
        Task<FriendOperationResult> BlockFriend(string userId, BlockFriendModel model);
        Task<FriendOperationResult> UnblockFriend(string userId, string friendId);
    }

    public class AsideContactService : IAsideContactService
    {
        private readonly Zalo_CloneContext _context;

        public AsideContactService(Zalo_CloneContext context)
        {
            _context = context;
        }

        public async Task<List<AsideContactVM>> GetFriendsList(string userId)
        {
            var friends = await _context.Friends
                .Where(f => (f.UserId1 == userId || f.UserId2 == userId))
                .Include(f => f.UserId1Navigation)
                .Include(f => f.UserId2Navigation)
                .Select(f => new AsideContactVM
                {
                    UserId = f.UserId1 == userId ? f.UserId2 : f.UserId1,
                    Username = f.UserId1 == userId ?
                        f.UserId2Navigation.Username :
                        f.UserId1Navigation.Username,
                    Avatar = f.UserId1 == userId ?
                        f.UserId2Navigation.Avatar :
                        f.UserId1Navigation.Avatar,
                    IsBlocked = f.Status == 2,
                    LastInteraction = f.UpdateAt,
                    IsOnline = f.UserId1 == userId ?
                        f.UserId2Navigation.Status == 1 :
                        f.UserId1Navigation.Status == 1
                })
                .ToListAsync();

            return friends;
        }

        public async Task<List<AsideContactVM>> FilterFriends(string userId, FriendFilterModel filter)
        {
            var query = _context.Friends
                .Where(f => (f.UserId1 == userId || f.UserId2 == userId))
                .Include(f => f.UserId1Navigation)
                .Include(f => f.UserId2Navigation)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(f =>
                    (f.UserId1Navigation.Username.Contains(filter.SearchTerm) ||
                     f.UserId2Navigation.Username.Contains(filter.SearchTerm)));
            }

            // Apply type filter
            if (filter.FilterType == "online")
            {
                query = query.Where(f =>
                    ((f.UserId1Navigation.Status == 1 || f.UserId2Navigation.Status == 1) && f.Status == 1));
            }
            else if (filter.FilterType == "blocked")
            {
                query = query.Where(f => f.Status == 2);
            }

            // Apply sorting
            switch (filter.SortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(f => f.UserId1Navigation.Username);
                    break;
                case "date_asc":
                    query = query.OrderBy(f => f.CreateAt);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(f => f.CreateAt);
                    break;
                default: // name_asc
                    query = query.OrderBy(f => f.UserId1Navigation.Username);
                    break;
            }

            var friends = await query
                .Select(f => new AsideContactVM
                {
                    UserId = f.UserId1 == userId ? f.UserId2 : f.UserId1,
                    Username = f.UserId1 == userId ?
                        f.UserId2Navigation.Username :
                        f.UserId1Navigation.Username,
                    Avatar = f.UserId1 == userId ?
                        f.UserId2Navigation.Avatar :
                        f.UserId1Navigation.Avatar,
                    IsBlocked = f.Status == 2,
                    LastInteraction = f.UpdateAt,
                    IsOnline = f.UserId1 == userId ?
                        f.UserId2Navigation.Status == 1 :
                        f.UserId1Navigation.Status == 1
                })
                .ToListAsync();

            return friends;
        }

        public async Task<FriendOperationResult> DeleteFriend(string userId, string friendId)
        {
            var friendship = await _context.Friends
                .FirstOrDefaultAsync(f =>
                    (f.UserId1 == userId && f.UserId2 == friendId) ||
                    (f.UserId1 == friendId && f.UserId2 == userId));

            if (friendship == null)
            {
                return new FriendOperationResult
                {
                    Success = false,
                    Message = "Không tìm thấy mối quan hệ bạn bè"
                };
            }

            _context.Friends.Remove(friendship);
            await _context.SaveChangesAsync();

            return new FriendOperationResult
            {
                Success = true,
                Message = "Đã xóa bạn bè thành công"
            };
        }

        public async Task<FriendOperationResult> BlockFriend(string userId, BlockFriendModel model)
        {
            var friendship = await _context.Friends
                .FirstOrDefaultAsync(f =>
                    (f.UserId1 == userId && f.UserId2 == model.UserId) ||
                    (f.UserId1 == model.UserId && f.UserId2 == userId));

            if (friendship == null)
            {
                return new FriendOperationResult
                {
                    Success = false,
                    Message = "Không tìm thấy mối quan hệ bạn bè"
                };
            }

            friendship.Status = 2; // Blocked status
            friendship.UpdateAt = DateTime.Now;
            friendship.UpdateUser = userId;

            await _context.SaveChangesAsync();

            return new FriendOperationResult
            {
                Success = true,
                Message = "Đã chặn người dùng thành công"
            };
        }

        public async Task<FriendOperationResult> UnblockFriend(string userId, string friendId)
        {
            var friendship = await _context.Friends
                .FirstOrDefaultAsync(f =>
                    (f.UserId1 == userId && f.UserId2 == friendId) ||
                    (f.UserId1 == friendId && f.UserId2 == userId));

            if (friendship == null)
            {
                return new FriendOperationResult
                {
                    Success = false,
                    Message = "Không tìm thấy mối quan hệ bạn bè"
                };
            }

            if (friendship.Status != 2)
            {
                return new FriendOperationResult
                {
                    Success = false,
                    Message = "Người dùng này không bị chặn"
                };
            }

            friendship.Status = 1; // Unblocked/Friend status
            friendship.UpdateAt = DateTime.Now;
            friendship.UpdateUser = userId;

            await _context.SaveChangesAsync();

            return new FriendOperationResult
            {
                Success = true,
                Message = "Đã bỏ chặn người dùng thành công"
            };
        }
    }
}