using System;
using System.Collections.Generic;
using System.Linq;
using Curio.Domain.Entities;
using Curio.Domain.Enums;

namespace Curio.Web.ViewModels
{
    public class UserPostViewModel
    {
        /// <summary>
        /// Use <see cref="FromUserPost(UserPost)"/>
        /// </summary>
        private UserPostViewModel()
        {

        }

        public string Contents { get; set; }
        public byte[] AuthorAvatar { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsAuthorVerified { get; set; }
        public int NumberOfReplies { get; set; }
        public int NumberOfReposts { get; set; }
        public int NumberOfLikes { get; set; }
        public UserPostSourceType UserPostSource { get; set; }
        public IEnumerable<UserPostReplyViewModel> Replies { get; set; } = Enumerable.Empty<UserPostReplyViewModel>();

        public static UserPostViewModel FromUserPost(UserPost userPost)
        {
            return new UserPostViewModel()
            {
                Contents = userPost.Contents
            };
        }
    }

    public class UserPostReplyViewModel
    {

    }
}
