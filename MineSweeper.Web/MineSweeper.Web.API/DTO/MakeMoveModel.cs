using MineSweeper.Data.Models;

namespace MineSweeper.Web.API.DTO
{
    public class MakeMoveModel : GameModelBase
    {
        public Move PlayerMove { get; set; }
    }
}
