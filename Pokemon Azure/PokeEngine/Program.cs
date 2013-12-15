using System;
using System.IO;
using System.Reflection;


///     >2012
///     >not blatantly copying copde from 
///     http://social.msdn.microsoft.com/Forums/en/netfxbcl/thread/30fb8225-c558-4d8d-b54a-a4a42b5d602d
///     to save time
///     I SERIOUSLY HOPE YOU GUYS DON'T DO THIS

namespace PokeEngine
{
#if WINDOWS || XBOX
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {

            using (Game1 game = new Game1())
            {
#if DEBUG
                string buildPath = Assembly.GetExecutingAssembly().Location.Replace("\\PokeEngine.exe", "");
                Console.WriteLine(buildPath);
                string contentPath = Directory.GetParent(buildPath) + "\\Content";
                Console.WriteLine(contentPath);
                CopyFolder(contentPath, buildPath, true);
#endif

                game.Run();
            }
        }

        private static void CopyFolder(string source, string destination, bool overwrite)
        {

            try
            {

                if (System.IO.Directory.Exists(source))
                {

                    if (System.IO.Directory.Exists(destination + "\\" + (new System.IO.DirectoryInfo(source).Name)) && (!overwrite))
                        throw new System.Exception("Sorry, but the folder " + destination + " already exists.");
                    else if (!System.IO.Directory.Exists(destination + "\\" + (new System.IO.DirectoryInfo(source).Name)))
                        System.IO.Directory.CreateDirectory(destination + "\\" + (new System.IO.DirectoryInfo(source).Name));

                    //Copy all the files
                    System.IO.FileInfo[] fiA = (new System.IO.DirectoryInfo(source).GetFiles());

                    foreach (System.IO.FileInfo fi in fiA)
                    {

                        Console.WriteLine("Copying " + fi.FullName + " to " + destination + "\\" + (new System.IO.DirectoryInfo(source).Name) + "\\");
                        Console.WriteLine(destination + "\\" + (new System.IO.DirectoryInfo(source).Name) + "\\" + fi.Name);
                        fi.CopyTo(destination + "\\" + (new System.IO.DirectoryInfo(source).Name) + "\\" + fi.Name, overwrite);

                    }

                    //Recursively fill the child directories
                    System.IO.DirectoryInfo[] diA = (new System.IO.DirectoryInfo(source).GetDirectories());

                    foreach (System.IO.DirectoryInfo di in diA)
                    {

                        Console.WriteLine("Copying " + di.FullName + " to " + destination + "\\" + (new System.IO.DirectoryInfo(source).Name) + "\\");
                        CopyFolder(di.FullName, destination + "\\" + (new System.IO.DirectoryInfo(source).Name), overwrite );

                    }

                }
                else
                    Console.WriteLine("Sorry, source folder " + source + " doesn't exist.");

            }
            catch(System.Exception ex)
            {
                Console.WriteLine("Sorry an error occurred while copying " + source + ": " + ex.Message);
            }
        }
    }
#endif
}

