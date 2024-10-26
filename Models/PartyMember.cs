using System;
using System.Collections.Generic;

namespace PartyApi.Models;

public partial class PartyMember
{
    public int PartyMemberId { get; set; }

    public string PartyMemberName { get; set; } = null!;

    public string PartyMemberSurname { get; set; } = null!;

    public virtual ICollection<MemberResponsibility> MemberResponsibilities { get; set; } = new List<MemberResponsibility>();
}
