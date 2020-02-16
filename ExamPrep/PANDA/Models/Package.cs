namespace PANDA.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        public decimal Weight { get; set; }

        [Required]
        [MaxLength(1000)]
        public string ShippingAddress { get; set; }

        public PackageStatus Status { get; set; }

        public DateTime EstimatedDeliveryDate { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public virtual User Recipient { get; set; }

        [Required]
        public virtual Receipt Receipt { get; set; }
    }
}
