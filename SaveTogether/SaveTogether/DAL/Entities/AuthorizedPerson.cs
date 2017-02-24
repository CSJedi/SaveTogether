using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SaveTogether.DAL.Entities
{
    public abstract class AuthorizedPerson: IdentityUser
    {
        public string SecondName { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}