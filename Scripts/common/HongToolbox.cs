using UnityEngine; 
using QFramework; 
using System; 
using System.Collections; 

namespace Hong { 
    public class Toolbox : Singleton<ToolBox> {
        public TimeItem setInterval(Action<int> cb, float step) {
            return QFramework.Timer.Instance.Post2Really(cb, step);
        }
    }
}