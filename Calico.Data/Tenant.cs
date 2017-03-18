namespace Calico.Data
{
    using System.ComponentModel.DataAnnotations;

    public class Tenant
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}