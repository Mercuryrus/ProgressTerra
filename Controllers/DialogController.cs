using Microsoft.AspNetCore.Mvc;
using ProgressTerra.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProgressTerra.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FindIDRGDialogController : ControllerBase
    {
        [HttpPost]
        public List<Guid> Post(IList<Guid> guids)
        {
            var dialogs = new RGDialogsClients().Init();

            Dictionary<Guid, List<Guid>> chats = new();
            foreach (RGDialogsClients dialog in dialogs)
            {
                if (!chats.ContainsKey(dialog.IDRGDialog))
                {
                    chats[dialog.IDRGDialog] = new();
                }
                chats[dialog.IDRGDialog].Add(dialog.IDClient);
            }

            List<Guid> result = new();

            foreach (KeyValuePair<Guid, List<Guid>> item in chats)
            {
                bool isContain = true;
                foreach (Guid guid in guids)
                {
                    if (!item.Value.Contains(guid))
                    {
                        isContain = false;
                        break;
                    }
                }
                if (isContain)
                {
                    result.Add(item.Key);
                }
            }


            if (result.Count>0)
            return result;
            else
            return new() { new Guid() };
        }
    }
}
