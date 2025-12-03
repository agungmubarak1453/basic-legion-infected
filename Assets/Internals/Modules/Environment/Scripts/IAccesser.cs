using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicLegionInfected.Environment
{
    public interface IAccesser
    {
        bool IsOpen { get; }

        void Receive(IAccesserUser user);
    }
}
