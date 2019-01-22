using System.ComponentModel;
using System.Windows;

namespace _005_Global_Event
{
    public class HostModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region index
        private int _index;
        public int Index
        {
            get { return _index; }
            set
            {
                if (_index != value)
                {
                    _index = value;
                    NotifyPropertyChanged("Index");
                }
            }
        }
        #endregion

        #region enable
        private bool _enable;
        public bool Enable
        {
            get { return _enable; }
            set
            {
                if (_enable != value)
                {
                    _enable = value;
                    NotifyPropertyChanged("CheckShow");
                    NotifyPropertyChanged("CheckHide");
                }
            }
        }
        public Visibility CheckShow
        {
            get
            {
                return _enable ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public Visibility CheckHide
        {
            get
            {
                return _enable ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        #endregion

        #region ip and port
        private string _ip;
        public string IP
        {
            get { return _ip; }
            set
            {
                if (_ip != value)
                {
                    _ip = value;
                    NotifyPropertyChanged("IpAndPort");
                }
            }
        }

        private string _port;
        public string Port
        {
            get { return _port; }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    NotifyPropertyChanged("IpAndPort");
                }
            }
        }

        public string IpAndPort
        {
            get
            {
                if (_port.Length <= 0)
                {
                    return "IP / " + _ip;
                }
                else
                {
                    return "IP / " + _ip + ":" + _port;
                }
            }
        }
        #endregion

        #region url
        private string _url;
        public string Url
        {
            get { return _url; }
            set
            {
                if (_url != value)
                {
                    _url = value;
                    NotifyPropertyChanged("TipsAndUrl");
                }
            }
        }
        public string TipsAndUrl
        {
            get { return "URL / " + _url; }
        }
        #endregion

        public HostModel(int index, bool enable, string ip, string port, string url)
        {
            _index = index;
            _enable = enable;
            _ip = ip;
            _port = port;
            _url = url;
        }

        #region 通知UI更新数据
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}

