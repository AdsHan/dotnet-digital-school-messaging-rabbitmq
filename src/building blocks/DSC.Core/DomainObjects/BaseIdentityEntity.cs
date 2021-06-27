using DSC.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace DSC.Core.DomainObjects
{
    public abstract class BaseIdentityEntity : IdentityUser
    {
        public Guid Id { get; set; }
        public EntityStatusEnum Status { get; set; }
        public DateTime DateCreateAt { get; private set; }
        public DateTime? DateDeleteAt { get; private set; }

        protected BaseIdentityEntity()
        {
            DateCreateAt = DateTime.Now;
            Status = EntityStatusEnum.Active;
        }

        public void Delete()
        {
            if (Status == EntityStatusEnum.Active)
            {
                Status = EntityStatusEnum.Inactive;
                DateDeleteAt = DateTime.Now;
            }
        }
    }
}