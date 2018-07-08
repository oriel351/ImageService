using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SharedLib;
using GUI.ViewModel;

namespace GUI.Model
{
    class LogsVM : ILogsModel
    {
        //implementing the interface
        public event PropertyChangedEventHandler PropertyChanged;
        private ILogsModel logsModel;

        public LogsVM()
        {
            //Creates a new logs model
            this.logsModel = new LogsModel();
            //delegates
            this.logsModel.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                //notify from th logvm that the property changed
                this.NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        private void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<MessageReceivedEventArgs> VM_LogEntries
        {
            get
            {
                return this.logsModel.LogEntries;
            }
        }
        /*public ObservableCollection<LogObject> Logs
        {
            get { return this.logsModel.Logs; }
            set { throw new NotImplementedException(); }
        }
        */
        ObservableCollection<MessageRecievedEventArgs> ILogsModel.Logs { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
