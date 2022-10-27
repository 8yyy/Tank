using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyForm
{
    class Program
    {
        static void Main(string[] args)
        {
            MyForm myForm = new MyForm();
            
            Application.Run(myForm);
            myForm.StartPosition = FormStartPosition.CenterParent;
        }
    }
}
