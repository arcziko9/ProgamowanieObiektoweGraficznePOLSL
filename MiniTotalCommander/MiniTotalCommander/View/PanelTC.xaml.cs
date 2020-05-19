using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MiniTotalCommander.Model;
using MiniTotalCommander.ViewModel;
using MiniTotalCommander.View;
using MiniTotalCommander.ViewModel.Base;

namespace MiniTotalCommander.View
{
    public partial class PanelTC : UserControl
    {
        public PanelTC()
        {
            InitializeComponent();
        }

        #region Events

        #region Path

        public static readonly RoutedEvent PathChangedEvent =
        EventManager.RegisterRoutedEvent("PathChanged",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelTC));

        public event RoutedEventHandler PathChanged
        {
            add { AddHandler(PathChangedEvent, value); }
            remove { RemoveHandler(PathChangedEvent, value); }
        }

        public static readonly RoutedEvent PathEnterEvent =
       EventManager.RegisterRoutedEvent("PathEnter",
                    RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                    typeof(PanelTC));

        public event RoutedEventHandler PathEnter
        {
            add { AddHandler(PathChangedEvent, value); }
            remove { RemoveHandler(PathChangedEvent, value); }
        }

        private void RaisePathChanged()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PathChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(
                "Path",
                typeof(string),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        #endregion Path

        #region Drive

        public static readonly RoutedEvent DriveChangedEvent =
        EventManager.RegisterRoutedEvent("DriveChanged",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelTC));

        public event RoutedEventHandler DriveChanged
        {
            add { AddHandler(DriveChangedEvent, value); }
            remove { RemoveHandler(DriveChangedEvent, value); }
        }

        private void RaiseDriveChanged()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(DriveChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty DriveProperty =
            DependencyProperty.Register(
                "Drives",
                typeof(BindingList<String>),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        public static readonly RoutedEvent SelectedDriveChangedEvent =
        EventManager.RegisterRoutedEvent("SelectedDriveChanged",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelTC));

        public event RoutedEventHandler SelectedDriveChanged
        {
            add { AddHandler(SelectedDriveChangedEvent, value); }
            remove { RemoveHandler(SelectedDriveChangedEvent, value); }
        }

        private void RaiseSelectedDriveChanged()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(SelectedDriveChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty SelectedDriveProperty =
            DependencyProperty.Register(
                "SelectedDrive",
                typeof(string),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        #endregion Drive

        #region FileTree

        #region FileSelection

        public static readonly RoutedEvent FileSelectionChangedEvent =
       EventManager.RegisterRoutedEvent("FileSelectionChanged",
                    RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                    typeof(PanelTC));

        public event RoutedEventHandler FileSelectionChanged
        {
            add { AddHandler(FileSelectionChangedEvent, value); }
            remove { RemoveHandler(FileSelectionChangedEvent, value); }
        }

        private void RaiseFileSelectionChanged()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(FileSelectionChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty FileSelectionProperty =
            DependencyProperty.Register(
                "SelectedFile",
                typeof(ViewModel.FileInfo.ListItemBase),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        public static readonly RoutedEvent FileEnterEvent =
   EventManager.RegisterRoutedEvent("FileEnter",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                typeof(PanelTC));

        public event RoutedEventHandler FileEnter
        {
            add { AddHandler(FileEnterEvent, value); }
            remove { RemoveHandler(FileEnterEvent, value); }
        }

        private void RaiseFileEnter()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(FileEnterEvent);
            RaiseEvent(newEventArgs);
        }

        #endregion FileSelection

        #region Contents

        public static readonly RoutedEvent ContentsChangedEvent =
      EventManager.RegisterRoutedEvent("ContentsChanged",
                   RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                   typeof(PanelTC));

        public event RoutedEventHandler ContentsChanged
        {
            add { AddHandler(ContentsChangedEvent, value); }
            remove { RemoveHandler(ContentsChangedEvent, value); }
        }

        private void RaiseContentsChanged()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(ContentsChangedEvent);
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty ContentsProperty =
            DependencyProperty.Register(
                "Contents",
                typeof(BindingList<ViewModel.FileInfo.ListItemBase>),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null)
            );

        #endregion Contents

        #endregion FileTree

        #region Labels

        protected static readonly DependencyProperty PathLabelProperty =
            DependencyProperty.Register("PathLabel", typeof(string), typeof(PanelTC));

        protected static readonly DependencyProperty DriveLabelProperty =
            DependencyProperty.Register("DriveLabel", typeof(string), typeof(PanelTC));

        #endregion Labels

        #endregion Events

        #region Dependency props

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        public BindingList<String> Drives
        {
            get { return (BindingList<String>)GetValue(DriveProperty); }
            set { SetValue(DriveProperty, value); }
        }

        public string SelectedDrive
        {
            get { return (string)GetValue(SelectedDriveProperty); }
            set { SetValue(SelectedDriveProperty, value); }
        }

        public ViewModel.FileInfo.ListItemBase SelectedFile
        {
            get { return (ViewModel.FileInfo.ListItemBase)GetValue(FileSelectionProperty); }
            set { SetValue(FileSelectionProperty, value); }
        }

        public BindingList<ViewModel.FileInfo.ListItemBase> Contents
        {
            get { return (BindingList<ViewModel.FileInfo.ListItemBase>)GetValue(ContentsProperty); }
            set { SetValue(ContentsProperty, value); }
        }

        public string PathLabel
        {
            get { return (string)GetValue(PathLabelProperty); }
            set { SetValue(PathLabelProperty, value); }
        }

        public string DriveLabel
        {
            get { return (string)GetValue(DriveLabelProperty); }
            set { SetValue(DriveLabelProperty, value); }
        }

        #endregion Dependency props

        #region Internal event handlers

        private void driveBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseSelectedDriveChanged();
        }

        private void pathBox_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                RaisePathChanged();
            }
        }

        private void contentsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RaiseFileEnter();
        }

        private void contentsList_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                RaiseFileEnter();
            }
        }

        #endregion Internal event handlers
    }
}
