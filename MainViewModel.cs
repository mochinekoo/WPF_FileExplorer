using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace WPF_FileExplorer {
    class MainViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainViewModel() {

        }

        private string filePath_ = "";
        public string FilePath {
            get { return filePath_; }
            set {
                filePath_ = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilePath)));

            }
        }

    }
}
