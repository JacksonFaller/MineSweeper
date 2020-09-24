using System.ComponentModel.DataAnnotations;

namespace MineSweeper.Web.API.DTO
{
    public class StartGameModel
    {
        [Required]
        [Range(5, 100)]
        public int Width { get; set; }

        [Required]
        [Range(5, 100)]
        public int Height { get; set; }

        [Required]
        public int MinesCount { get; set; }
    }
}
