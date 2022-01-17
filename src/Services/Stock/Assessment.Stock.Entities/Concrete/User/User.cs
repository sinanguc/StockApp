using System;

namespace Assessment.Stock.Entities.Concrete.Users
{
    public class User : BaseEntity, IEntity, ISoftDeleteEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public bool Deleted { get; set; }
        public DateTime? PackageEndDate { get; set; }
        public string ApiCode { get; set; }
    }
}
