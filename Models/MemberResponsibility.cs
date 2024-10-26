using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PartyApi.Models;

public partial class MemberResponsibility
{
    public int MemberResponsibilityId { get; set; }

    public int PartyId { get; set; }

    public int PartyMemberId { get; set; }

    public int ResponsibilityId { get; set; }

    [JsonIgnore]
    public virtual Party Party { get; set; } = null!;

    [JsonIgnore]
    public virtual PartyMember PartyMember { get; set; } = null!;

    [JsonIgnore]
    public virtual Responsibility Responsibility { get; set; } = null!;
}
