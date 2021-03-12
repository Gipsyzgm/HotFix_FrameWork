using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class CTaskExtension
{
    public static CTaskHandle Run(this CTask task)
    {
        return MainMgr.Task.Manager.Run(task);
    }
}
