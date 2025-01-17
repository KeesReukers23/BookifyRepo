﻿using System.ComponentModel.DataAnnotations;

namespace Interfaces
{
    public class LikeDto
    {
        [Key]
        public Guid LikeId { get; private set; }

        //FK to User
        public Guid UserId { get; set; }
        public UserDto User { get; set; } = null!;

        //FK to Post
        public Guid PostId { get; set; }
        public PostDto Post { get; private set; } = null!;

    }
}
