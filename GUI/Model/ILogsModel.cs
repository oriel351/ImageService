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
        // added branch
        ObservableCollection<MessageRecievedEventArgs> LogEntries { get; set; }
    }
}
