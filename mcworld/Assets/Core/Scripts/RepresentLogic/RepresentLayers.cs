using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.RepresentLogic
{
    public enum RepresentLayers
    {
        Default = 1 << 0,
        TransparentFX = 1 << 1,
        IgnoreRaycast = 1 << 2,
        Water = 1 << 4,
        UI = 1 << 5,
    }
}
