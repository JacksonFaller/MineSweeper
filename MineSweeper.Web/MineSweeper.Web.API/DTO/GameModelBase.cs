using System;
using System.ComponentModel.DataAnnotations;

namespace MineSweeper.Web.API.DTO
{
    public class GameModelBase
    {
        [Required]
        public Guid GameKey { get; set; }
    }
}
