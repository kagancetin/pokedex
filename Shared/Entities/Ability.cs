using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeastBattle.Shared.Entities
{
    public class Ability
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public int Accuracy { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Power { get; set; }
        public int PP { get; set; }
        public string TypeName { get; set; } = string.Empty;
    }
}