using DSC.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace DSC.Core.DomainObjects
{
    public abstract class BaseIdentityEntity : IdentityUser
    {
        public Guid Id { get; set; }
        public DateTime DateCreateAt { get; private set; }
        public EntityStatusEnum Status { get; set; }

        protected BaseIdentityEntity()
        {
            DateCreateAt = DateTime.Now;
            Status = EntityStatusEnum.Ativa;
        }

        public void Excluir()
        {
            if (Status == EntityStatusEnum.Ativa)
            {
                Status = EntityStatusEnum.Inativa;
            }
        }
    }
}