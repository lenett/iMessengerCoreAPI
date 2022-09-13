using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iMessengerCoreAPI.Models;


namespace iMessengerCoreAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DialogsController : ControllerBase
    {
        /// <summary>
        /// Возвращает идентификатор диалога, в котором есть переданные клиенты.
        /// </summary>
       
        [HttpPost]
        public async Task<ActionResult<DialogsModel>> Post(ClientsModel clientIds)
        {
            if (clientIds == null)
            {
                return BadRequest();
            }

            DialogsModel returnableData = LookForDialog(clientIds);
            return Ok(returnableData); 
        }

        private DialogsModel LookForDialog(ClientsModel clientIds)
        {
            List<RGDialogsClients> dialogsClients = RGDialogsClients.Init();
            Dictionary<Guid, List<Guid>> sortedDialogs = SortDialogs(dialogsClients);
            bool founded = false;
            Guid returnableGuid = Guid.Empty;

            foreach (var dialog in sortedDialogs)
            {
                foreach (var client in clientIds.ClientGuids)
                {
                    if (dialog.Value.Contains(client))
                    {
                        founded = true;
                    }
                    else
                    {
                        founded = false;
                        break;
                    }
                }

                if (founded && dialog.Value.Count == clientIds.ClientGuids.Count)
                {
                    returnableGuid = dialog.Key;
                }
            }

            DialogsModel dialogModel = new DialogsModel(returnableGuid);
            return dialogModel;
        }

        private Dictionary<Guid, List<Guid>> SortDialogs(List<RGDialogsClients> dialogsClients)
        {
            Dictionary<Guid, List<Guid>> sortedDialogs = new Dictionary<Guid, List<Guid>>();

            foreach (RGDialogsClients rGDialogsClients in dialogsClients)
            {
                if (!sortedDialogs.ContainsKey(rGDialogsClients.IDRGDialog))
                {
                    sortedDialogs.Add(rGDialogsClients.IDRGDialog, new List<Guid>());
                }
                sortedDialogs[rGDialogsClients.IDRGDialog].Add(rGDialogsClients.IDClient);
            }

            return sortedDialogs;
        }
    }

}
