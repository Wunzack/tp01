using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ecsfc
{
    public class fphub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public void addGroup(string GroupID)
        {
            Groups.Add(Context.ConnectionId, GroupID);
        }
    }
}