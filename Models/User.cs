using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_2023.Models;

public partial class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; }

    public string? DisplayName { get; set; }
    public string? Photo { get;set; }
    [MaxLength (100)]
    public string? UserName { get; set; }
    [MaxLength(100)]
    public string Email { get; set; } = null!;
   
    public bool? EmailVerified { get; set; }
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }

    [Range(1, 4, ErrorMessage = "The Status must be between 1 and 4.")]
    public int? Status { get; set; }

    public DateTime CreatedAt { get; set; } =DateTime.Now;

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
