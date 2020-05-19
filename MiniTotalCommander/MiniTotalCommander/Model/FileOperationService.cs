using MiniTotalCommander.Model.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTotalCommander.Model
{
    internal class FileOperationService
    {
        public List<IOperation> Commands = new List<IOperation>();

        public void AddCommand(IOperation command)
        {
            if (!Commands.Contains(command))
            {
                Commands.Add(command);
            }
        }

        public bool canExecuteOperationByName(string name, string sourcePath, string targetPath)
        {
            foreach (var cmd in Commands)
            {
                if (cmd.Name == name) { return canExecute(cmd, sourcePath, targetPath); }
            }
            throw new Exception("There is no command with this name.");
        }

        public void ExecuteOperationByName(string name, string sourcePath, string targetPath)
        {
            foreach (var cmd in Commands)
            {
                if (cmd.Name == name) { Execute(cmd, sourcePath, targetPath); return; }
            }
            throw new Exception("There is no command with this name.");
        }

        public void Execute(IOperation command, string sourcePath, string targetPath)
        {
            try
            {
                command.Execute(sourcePath, targetPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool canExecute(IOperation command, string sourcePath, string targetPath)
        {
            return command.canExecute(sourcePath, targetPath);
        }
    }
}
