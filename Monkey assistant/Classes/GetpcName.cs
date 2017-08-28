using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monkey_assistant.Classes
{
    public class GetpcName
    {
        public string NamePc()
        {
            string name = Environment.MachineName.ToLower();

            string[] index = name.Split('-');
            string dbName = "DB" + index[1];

            return dbName;
        }
    }
}
