using System;

namespace DomainLibrary.Shared
{
    public class TransLog
    {

        public int AddedById { get; set; }
        public DateTime AddedOn { get; set; }

        public int? LastUpdatedById { get; set; }
        public DateTime? LastUpdatedByOn { get; set; }
    }
}
