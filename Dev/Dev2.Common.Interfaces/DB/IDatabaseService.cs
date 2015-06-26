﻿using System;
using System.Collections.Generic;
using Dev2.Common.Interfaces.ServerProxyLayer;

namespace Dev2.Common.Interfaces.DB
{
    public interface IDatabaseService
    {
        string Name { get; set; }
        string Path { get; set; }
        IDbSource Source { get; set; }
        IDbAction Action { get; set; }
        IList<IServiceInput> Inputs { get; set; }
        IList<IServiceOutputMapping> OutputMappings { get; set; }
        Guid Id { get; set; }
    }
}