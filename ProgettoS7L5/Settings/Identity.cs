﻿namespace ProgettoS7L5.Settings
{
    public class Identity
    {
            public bool SignInRequireConfirmedAccount { get; set; }
            public int RequiredLength { get; set; }
            public bool RequireDigit { get; set; }
            public bool RequireLowercase { get; set; }
            public bool RequireNonAlphanumeric { get; set; }
            public bool RequireUppercase { get; set; }
        
    }
}
