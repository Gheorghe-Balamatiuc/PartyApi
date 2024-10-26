using System;
using System.Collections.Generic;

namespace PartyApi.Models;

public partial class Party
{
    public int PartyId { get; set; }

    public string PartyName { get; set; } = null!;

    public decimal Budget { get; set; }

    public virtual ICollection<MemberResponsibility> MemberResponsibilities { get; set; } = new List<MemberResponsibility>();
}
