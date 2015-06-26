﻿using Dev2.Common.Interfaces.Explorer;
using Dev2.Common.Interfaces.ServerDialogue;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Warewolf.Studio.ViewModels
{
    class ServerAddedEvent:PubSubEvent<IServerSource>
    {
    }
    class ItemAddedEvent : PubSubEvent<IExplorerItem>
    {
    }
}
