using BiletPortal.Dto;
using Microsoft.AspNetCore.Identity;

namespace BiletPortal.Models
{
    public class Profile
    {

            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }
            public string Email { get; set; }
            public string TicketType { get; set; }
            public string ActivityName { get; set; }
            public Profile() { }    

            public Profile(AppUser appUser, Products products)
            {

            FirstName = appUser.FirstName;
            LastName = appUser.LastName;
            City = appUser.City;
            Email = appUser.Email;
            TicketType = products.Category.CategoryName;
            ActivityName = products.ProductName;
            }

        }
}
