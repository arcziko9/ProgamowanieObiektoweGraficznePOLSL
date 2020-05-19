using MiniTotalCommander.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTotalCommander.ViewModel.FileTreeCommands
{
    internal static class RunFile
    {
        private static void run(object param)
        {
            TCPanelViewModel vm = param as TCPanelViewModel;
            Process.Start(vm.SelectedItem.Path);
        }

        public static RelayCommand Run
        {
            get
            {
                return new RelayCommand(argument => { run(argument); }, argument => true);
            }
        }
    }
}
