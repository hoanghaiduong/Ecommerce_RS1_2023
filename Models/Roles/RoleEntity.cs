using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce_2023.Models.Role
{
    [Table("Roles")]
    public class RoleEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        public string Name { get; set; }

        [Column("isActive")]
        [DefaultValue(true)] // Đặt giá trị mặc định là true
        public bool? IsActive { get; set; }

    }
}
