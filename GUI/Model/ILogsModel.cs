//using GlobalClasses.Modal;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace GUI.Model
{
    interface ILogsModel : INotifyPropertyChanged
    {
        ObservableCollection<MessageRecievedEventArgs> LogEntries { get; set; }
    }
}
