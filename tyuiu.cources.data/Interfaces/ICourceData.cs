using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tyuiu.cources.data.Models;

namespace tyuiu.cources.data.Interfaces
{
    public interface ICourceData
    {
        CourceTask GetCourceTask(int sprintId, int taskId);
    }
}
