using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeastBattle.Shared.Entities
{
    public class Beast
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAttack { get; set; }
        public int SpDefense { get; set; }
        public int Speed { get; set; }
        public string Species { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Evolution { get; set; }
        public string Height { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public string ImageSprite { get; set; } = string.Empty;
        public string ImageThumbnail { get; set; } = string.Empty;
        public string ImageHires { get; set; } = string.Empty;
    }
}