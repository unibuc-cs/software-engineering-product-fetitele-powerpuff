﻿using Microsoft.AspNetCore.Identity;

namespace healthy_lifestyle_web_app.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public Profile? Profile { get; set; }
        public ICollection<Food>? Foods { get; set; }
    }
}
