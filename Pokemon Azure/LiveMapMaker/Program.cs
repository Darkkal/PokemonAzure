using System;

namespace LiveMapMaker
{
#if WINDOWS || XBOX
    static class Program
    {
        static Game1 game = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Editor form = new Editor();
            form.Disposed += new EventHandler(form_Disposed);

            using (game = new Game1(form))
            {
                game.Run();
            }
        }

        static void form_Disposed(object sender, EventArgs e)
        {
            game.Exit();
        }
    }
#endif
}

