using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Construction;
using Microsoft.Build.Execution;

namespace LiveMapMaker
{
    /// <summary>
    /// This class is used to compile .x and .fbx files into .xnb files, which are loadable by XNA games
    /// </summary>
    static class ModelBuilder
    {
        //temp directory is used to compile the MSBuild project
        static String tempDirectory = Path.GetTempPath() + "PokeMapMaker\\";
        static String outputDirectory = tempDirectory + "bin\\";
        static String projectDirectory = tempDirectory + "project\\";


        static Project buildProject;
        static ProjectRootElement projectRootElement;
        static BuildParameters buildParameters;
        static List<ProjectItem> projectItems = new List<ProjectItem>();

        static public void addModel(String filename)
        {
            //first delete everything in the Temp directory
            //String[] files = Directory.GetFiles(tempDirectory);
            try
            {
                Directory.Delete(tempDirectory, true);
            }
            catch(DirectoryNotFoundException)
            {}
            Directory.CreateDirectory(tempDirectory);

            //Then we create an MSBuild project
            makeBuildProject();

            //Then add the model to the project
            addToProject(filename);
            //build the project which will compile the xnb
            build();

            //then copy the xnb to this project's content folder
            copyToContentFolder(filename);

            //delete temporary directory recursively
            Directory.Delete(tempDirectory, true);
            
        }

        private static void copyToContentFolder(String filename)
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\Content\\Models\\"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\Content\\Models\\");
            }

            copyRecursive(outputDirectory, Directory.GetCurrentDirectory() + "\\Content\\Models\\");

            //copy the created xnb to the folder, it will overwrite whatever was there previously
            //String outLoc = Directory.GetCurrentDirectory() + "\\Content\\Models\\" + Path.GetFileNameWithoutExtension(filename) + ".xnb";
            //File.Copy(outputDirectory + Path.GetFileNameWithoutExtension(filename) + ".xnb", outLoc, true);
        }

        private static void copyRecursive(String folder, String destination)
        {
            String[] files = Directory.GetFiles(folder);
            String[] folders = Directory.GetDirectories(folder);

            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            foreach(String s in files)
            {
                File.Copy(s, destination + Path.GetFileName(s), true);
            }
            foreach (String s in folders)
            {
                copyRecursive(s, destination + getFolderName(s) + "\\");
            }
        }

        private static String getFolderName(String location)
        {
            String[] words = location.Split('\\');
            return words[(words.Length - 1)];
        }

        private static void build()
        {
            buildParameters = new BuildParameters();
            BuildManager.DefaultBuildManager.BeginBuild(buildParameters);

            BuildRequestData request = new BuildRequestData(buildProject.CreateProjectInstance(), new string[0]);
            BuildSubmission submission = BuildManager.DefaultBuildManager.PendBuildRequest(request);

            submission.ExecuteAsync(null, null);

            submission.WaitHandle.WaitOne();
            BuildManager.DefaultBuildManager.EndBuild();
        }

        private static void addToProject(string filename)
        {
            ProjectItem item = buildProject.AddItem("Compile", filename)[0];

            item.SetMetadataValue("Link", Path.GetFileName(filename));
            item.SetMetadataValue("Name", Path.GetFileNameWithoutExtension(filename));

            //using model processor obviously
            item.SetMetadataValue("Processor", "ModelProcessor");

            //finally add the item
            projectItems.Add(item);
        }

        private static void makeBuildProject()
        {
            projectRootElement = ProjectRootElement.Create(projectDirectory);

            //stuff for importing XNA Framework 
            projectRootElement.AddImport("$(MSBuildExtensionsPath)\\Microsoft\\XNA Game Studio\\" +
                                         "v4.0\\Microsoft.Xna.GameStudio.ContentPipeline.targets");

            if(buildProject == null)
                buildProject = new Project(projectRootElement);

            buildProject.SetProperty("XnaPlatform", "Windows");
            buildProject.SetProperty("XnaProfile", "Reach");
            buildProject.SetProperty("XnaFrameworkVersion", "v4.0");
            buildProject.SetProperty("Configuration", "Release");
            buildProject.SetProperty("OutputPath", outputDirectory);

            //add references to required content importers
            buildProject.AddItem("Reference", "Microsoft.Xna.Framework.Content.Pipeline.FBXImporter, Version=4.0.0.0, PublicKeyToken=842cf8be1de50553");
            buildProject.AddItem("Reference", "Microsoft.Xna.Framework.Content.Pipeline.XImporter, Version=4.0.0.0, PublicKeyToken=842cf8be1de50553");
            buildProject.AddItem("Reference", "Microsoft.Xna.Framework.Content.Pipeline.TextureImporter, Version=4.0.0.0, PublicKeyToken=842cf8be1de50553");
            buildProject.AddItem("Reference", "Microsoft.Xna.Framework.Content.Pipeline.EffectImporter, Version=4.0.0.0, PublicKeyToken=842cf8be1de50553");

        }
    }
}
