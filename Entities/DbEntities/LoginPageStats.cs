using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{

    public  class LoginPageStats: IGenericEntity
    {
        public enum Buttons { Login, Logout, DisplayStats }

        public int Id { get; set; }
        public int LoginUserStatsId { get; set; }
        public string ButtonType { get; set; }
        public int ClickedAfterInSeconds { get; set; }

      //  public virtual LoginUserStats LoginUserStats { get; set; }

    }
}
