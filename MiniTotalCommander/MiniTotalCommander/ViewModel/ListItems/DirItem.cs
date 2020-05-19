﻿using MiniTotalCommander.ViewModel.FileInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTotalCommander.ViewModel.ListItems
{
    internal class DirItem : ListItemBase
    {
        public override string ToString()
        {
            return MiniTotalCommander.Properties.Resources.DirPrefix + $" {Name}";
        }
    }
}
