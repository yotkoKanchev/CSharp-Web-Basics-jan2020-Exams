namespace PANDA.Models
{
    using SIS.MvcFramework;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User : IdentityUser<string>
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Packages = new HashSet<Package>();
            this.Receipts = new HashSet<Receipt>();
        }

        [ForeignKey("RecipientId")]
        public virtual ICollection<Package> Packages { get; set; }

        [ForeignKey("RecipientId")]
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
