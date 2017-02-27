namespace Calico
{
    using System.Collections.Generic;

    public class GetClientsResponse
    {
        public IEnumerable<ClientRecord> Clients { get; set; }
    }
}
