using System;
using System.Collections.Generic;

namespace PartyApi.Models;

public partial class Responsibility
{
    public int ResponsibilityId { get; set; }

    public string ResponsibilityName { get; set; } = null!;

    public virtual ICollection<MemberResponsibility> MemberResponsibilities { get; set; } = new List<MemberResponsibility>();
}
