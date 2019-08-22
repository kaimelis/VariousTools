using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Custom.Tool
{
    public static class GitHande
    {
        public static void RunGitCommand(string command)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"C:/Program Files/Git/git-bash.exe",
                        Arguments = command,
                        UseShellExecute = false,
                        CreateNoWindow = true

                    }
                };
                process.Start();
                process.WaitForExit();

            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public static string GetGitOutput(string command)
        {
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = @"C:/Program Files/Git/git-bash.exe",
                        Arguments = command,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true

                    }
                };
                process.Start();

                while (!process.StandardOutput.EndOfStream)
                {
                    var line = process.StandardOutput.ReadLine();
                    return line;
                }
                process.WaitForExit();
            }
            catch (System.Exception)
            {

                throw;
            }
            return "";
        }
    }
}
