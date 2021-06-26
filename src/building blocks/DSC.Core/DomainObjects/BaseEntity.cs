using DSC.Core.Enums;
using System;

namespace DSC.Core.DomainObjects
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreateAt { get; private set; }
        public EntityStatusEnum Status { get; set; }

        protected BaseEntity()
        {

            DateCreateAt = DateTime.Now;
            Status = EntityStatusEnum.Ativa;
        }

        public void Delete()
        {
            if (Status == EntityStatusEnum.Ativa)
            {
                Status = EntityStatusEnum.Inativa;
            }
        }
    }
}