using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Random_String_Generator.Models
{
    public class GeneratedData 
    {
        public int ID { get; set; }
        public int ThreadID { get; set; }
        public DateTime Time { get; set; }
        public string Data { get; set; }
       
    }

}
