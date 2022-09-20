using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_App.ViewModel
{
    public class Json<T>
    {
        public int result { get; set; }
        public List<T> data { get; set; }
    }
}
