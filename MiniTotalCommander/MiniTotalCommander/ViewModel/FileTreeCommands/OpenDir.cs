using MiniTotalCommander.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTotalCommander.ViewModel.FileTreeCommands
{
    internal static class OpenDir
    {
        private static void open(object param)
        {
            TCPanelViewModel vm = param as TCPanelViewModel;
            if (vm.fileTreeService.isAccesible(vm.SelectedItem.Path))
            {
                vm.Path = vm.SelectedItem.Path;
                vm.RefreshPanel();
            }
        }

        public static RelayCommand Open
        {
            get
            {
                return new RelayCommand(argument => { open(argument); }, argument => true);
            }
        }
    }
}
