using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTotalCommander.Model.Commands
{
    public interface IOperation
    {
        string Name { get; }

        void Execute(string sourcePath, string targetPath);

        bool canExecute(string sourcePath, string targetPath);
    }
}
