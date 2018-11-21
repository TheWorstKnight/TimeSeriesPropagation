using System.Windows.Forms;
using Time_Series_Propagation.Utils.Abstract;

namespace Time_Series_Propagation.Utils
{
    public class MyApplication :IApplication
    {
        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
