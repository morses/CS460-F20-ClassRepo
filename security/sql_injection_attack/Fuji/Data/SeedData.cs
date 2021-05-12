using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fuji.Data
{
    /// <summary>
    /// Helper class to hold all the information we need to construct the users for this app.  Basically
    /// a union of FujiUser and IdentityUser.  Not great to have to duplicate this data but it is only for one-time seeding
    /// of the databases.  Does NOT hold passwords since we need to store those in a secret location.
    /// </summary>
    public class UserInfoData
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool EmailConfirmed { get; set; } = true;
    }

    public class SeedData
    { 
        /// <summary>
        /// Data to be used to seed the FujiUsers and ASPNetUsers tables
        /// </summary>
        public static readonly UserInfoData[] UserSeedData = new UserInfoData[]
        {
            new UserInfoData { UserName = "TaliaK", Email = "knott@example.com", FirstName = "Talia", LastName = "Knott" },
            new UserInfoData { UserName = "ZaydenC", Email = "clark@example.com", FirstName = "Zayden", LastName = "Clark" },
            new UserInfoData { UserName = "DavilaH", Email = "hareem@example.com", FirstName = "Hareem", LastName = "Davila" },
            new UserInfoData { UserName = "KrzysztofP", Email = "krzysztof@example.com", FirstName = "Krzysztof", LastName = "Ponce" }
        };
    }
}
