using System.ComponentModel.DataAnnotations;

namespace Kasp.Identity.Entities.UserEntities.XEntities {
	public class UserEditModelBase {
		[MaxLength(100)]
		public string Name { get; set; }
	}
}