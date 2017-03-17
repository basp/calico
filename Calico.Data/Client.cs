namespace Calico.Data
{
    using System.Collections.Generic;

    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Plot> Plots { get; set; }
    }
}