using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using zaloclone_test.Helper;
using zaloclone_test.Models;
using zaloclone_test.Utilities;
using zaloclone_test.ViewModels;
using zaloclone_test.ViewModels.Token;

namespace zaloclone_test.Services
{
    public interface IPostService
    {
        public Task<string> InsertUpdatePost(InsertUpdatePostVM input, string userId);
        public Task<(string msg, List<PostVM>? result)> GetListPosts(string? userId = null);
        //public Task<(string msg, List<PostVM>? result)> GetPostsProfile(string? userId = null);
        public Task<(string msg, List<PostVM>? result)> GetPostsProfile(string? userid = null, string? otherUserId = null);

        public string GetDetailPost(string postId, out dynamic result);
        public Task<string> DoDeletePost(string postId);
        public Task<string> DoAllowComment(string postId);
        public Task<string> DoPinTopPost(string postId, string userid);
    }

    public class PostService : IPostService
    {
        private readonly Zalo_CloneContext _context;
        private readonly JwtAuthentication _jwtAuthen;
        public PostService(Zalo_CloneContext context, JwtAuthentication jwtAuthen)
        {
            _context = context;
            _jwtAuthen = jwtAuthen;
        }

        public async Task<string> DoAllowComment(string postId)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postId);
            if (post == null) return "Không tìm thấy bài viết";

            post.IsComment = !post.IsComment;
            post.UpdatedAt = DateTime.Now;
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
            return "";
        }

        public async Task<string> DoDeletePost(string postId)
        {
            var post = await _context.Posts.Include(p => p.PostImages).FirstOrDefaultAsync(x => x.PostId == postId);
            if (post == null) return "Không tìm thấy bài viết";

            var imagePaths = post.PostImages.Select(img => Path.Combine(Directory.GetCurrentDirectory(), Constant.UrlImagePath, img.ImageUrl)).ToList();

            _context.Posts.Remove(post);
            if (imagePaths.Any())
            {
                foreach (var imagePath in imagePaths)
                {
                    if (File.Exists(imagePath))
                    {
                        File.Delete(imagePath);
                    }
                }
            }
            await _context.SaveChangesAsync();
            return "";
        }

        public async Task<string> DoPinTopPost(string postId, string userId)
        {
            var postToPin = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postId);
            if (postToPin == null) return "Không tìm thấy bài viết";

            var oldPostPin = await _context.Posts.FirstOrDefaultAsync(x => x.PinTop == true && x.UserId == userId);
            if (oldPostPin != null && oldPostPin.PostId != postId)
            {
                oldPostPin.PinTop = false;
                _context.Posts.Update(oldPostPin);
            }

            postToPin.PinTop = !postToPin.PinTop;
            _context.Posts.Update(postToPin);
            await _context.SaveChangesAsync();
            return "";
        }

        public string GetDetailPost(string postId, out dynamic result)
        {
            throw new NotImplementedException();
        }

        public async Task<(string msg, List<PostVM>? result)> GetListPosts(string? userId = null)
        {
            var listPost = await _context.Posts.Where(x => (string.IsNullOrEmpty(userId) && x.Privacy != (int)PostPrivacy.Private) || x.UserId == userId)
                .Include(x => x.PostImages).Include(x => x.User)
                 .OrderByDescending(x => x.PinTop).ThenByDescending(x => x.CreatedAt)
                    .ToListAsync();
            if (listPost == null || !listPost.Any()) return ("Chưa có bài viết.", null);

            var result = listPost.Select(x => new PostVM
            {
                PostID = x.PostId,
                UserID = x.UserId,
                Username = x.User.Username,
                AvatarUrl = x.User.Avatar,
                Content = x.Content,
                Privacy = x.Privacy,
                Likes = x.Likes,
                Comments = x.Comments,
                IsComment = x.IsComment,
                Shares = x.Shares,
                CreateAt = x.CreatedAt,
                PinToTop = x.PinTop,
                Images = string.Join(";", x.PostImages.Select(img => img.ImageUrl))
            }).ToList();
            return (string.Empty, result);
        }

        public async Task<(string msg, List<PostVM>? result)> GetPostsProfile(string? userid = null, string? otherUserId = null)
        {
            var listPost = await _context.Posts.Where(x => (otherUserId != null && x.UserId == otherUserId
                    && x.Privacy != (int)PostPrivacy.Private) || (userid != null && x.UserId == userid))
                    .Include(x => x.PostImages).Include(x => x.User)
                    .OrderByDescending(x => x.PinTop).ThenByDescending(x => x.CreatedAt)
                    .ToListAsync();

            if (listPost == null || !listPost.Any()) return ("Chưa có bài viết.", null);

            var result = listPost.Select(x => new PostVM
            {
                PostID = x.PostId,
                UserID = x.UserId,
                Username = x.User.Username,
                AvatarUrl = x.User.Avatar,
                Content = x.Content,
                Privacy = x.Privacy,
                Likes = x.Likes,
                Comments = x.Comments,
                IsComment = x.IsComment,
                Shares = x.Shares,
                CreateAt = x.CreatedAt,
                PinToTop = x.PinTop,
                Images = string.Join(";", x.PostImages.Select(img => img.ImageUrl))
            }).ToList();
            return (string.Empty, result);
        }

        public async Task<string> InsertUpdatePost(InsertUpdatePostVM input, string userId)
        {
            string msg = "";
            var files = input.Images;
            List<string>? fileNames = new();
            if (files != null)
            {
                (msg, fileNames) = await Common.GetUrlImages(files);
                if (msg.Length > 0) return msg;
            }
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    if (!string.IsNullOrEmpty(input.PostID))
                    {
                        var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == input.PostID);
                        if (post == null)
                            return "Không tìm thấy bài viết";

                        post.Content = input.Content;
                        post.Privacy = input.Privacy;
                        post.UpdatedAt = DateTime.Now;

                        _context.Posts.Update(post);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        Guid postID = Guid.NewGuid();
                        var newPost = new Post
                        {
                            PostId = postID.ToString(),
                            UserId = userId,
                            Content = input.Content,
                            Privacy = input.Privacy,
                            IsComment = true,
                            CreatedAt = DateTime.Now,
                            PinTop = false,
                        };
                        Guid imagesId = Guid.NewGuid();
                        var newImages = new PostImage
                        {
                            ImageId = imagesId.ToString(),
                            PostId = postID.ToString(),
                            ImageUrl = string.Join(";", fileNames),
                            CreatedAt = DateTime.Now,
                        };

                        await _context.Posts.AddAsync(newPost);
                        await _context.PostImages.AddAsync(newImages);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                }
                catch
                {
                    await transaction.RollbackAsync();
                    return "An error occurred while creating the post.";
                }
            }
            return "";
        }


        private async Task<(string, Post?)> GetPostByID(string postID)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postID);
            if (post == null) return ("Không tìm thấy bài viết", null);

            return ("", post);
        }
    }
}
