using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace iMessengerCoreAPI.Models
{
    public class ClientsModel
    {
        /// <summary>Идентификаторы клиентов</summary>
        [Required]
        public List<Guid> ClientGuids { get; set; }
    }
}
