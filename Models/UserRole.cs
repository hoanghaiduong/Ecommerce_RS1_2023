﻿using System;
using System.Collections.Generic;

namespace Ecommerce_2023.Models;

public partial class UserRole
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
