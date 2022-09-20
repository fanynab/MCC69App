using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_App.ViewModel
{
    public class Results<T>
    {
        public int result { get; set; }
        public T data { get; set; }
    }
}
