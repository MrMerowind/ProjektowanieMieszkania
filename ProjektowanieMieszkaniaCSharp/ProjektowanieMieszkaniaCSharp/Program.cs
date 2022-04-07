using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ProjektowanieMieszkaniaCSharp
{
    
    static class Program
    {
        
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Prekonfiguracja());
            if (!Prekonfiguracja.isAppClosingInitialized)
            {
                Application.Run(new Form1());
            }
        }
    }
}
