using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epxoxy.Helpers
{
    internal class DebugHelper
    {
        private static Dictionary<WeakReference, int> debugDict = new Dictionary<WeakReference, int>();
        private static Dictionary<string, int> msgDict = new Dictionary<string, int>();
        private static WeakReference prevObject;

        public static void debugWrite(object obj, string value)
        {
            var refObj = debugDict.Keys.FirstOrDefault(o => o.Target == obj);
            if (refObj != null)
            {
                if (!refObj.IsAlive)
                {
                    debugDict.Remove(refObj);
                    refObj = new WeakReference(obj);
                    debugDict.Add(refObj, 0);
                }
                else
                {
                    ++debugDict[refObj];
                }
            }else
            {
                refObj = new WeakReference(obj);
                debugDict.Add(refObj, 0);
            }

            if (msgDict.ContainsKey(value)) ++msgDict[value];
            else msgDict.Add(value, 0);

            if (prevObject != null && prevObject.Target == obj)
            {
                System.Diagnostics.Debug.WriteLine("{0}({1})", value, msgDict[value]);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("({0}){1} : \n{2}({3})", debugDict[refObj], obj.ToString(), value, msgDict[value]);
                prevObject = new WeakReference(obj);
            }
        }
    }
}
