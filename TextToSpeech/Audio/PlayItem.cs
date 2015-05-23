using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace JocysCom.TextToSpeech.Monitor.Audio
{
    /// <remarks>
    /// Test Text:
    /// This is my fault. I. I ignored his warnings, his complaints, everything. I couldn't help myself - I saw those Scourge just beyond the tower, and 
    /// I. I HAD to go kill them. Argus says that Gidwin was taken northwest, and that's our only lead, so that's where we'll head. Northdale and the 
    /// Argent Crusade be damned! We're getting Gidwin back from those Scourge trash. Your objective is to Hop in Fiona's Caravan and ride to Northpass 
    /// Tower. You will be rewarded!
    /// </remarks>
    public class PlayItem : INotifyPropertyChanged, IDisposable
    {

        System.Timers.Timer playTimer;

        ISynchronizeInvoke _parent;

        public PlayItem(ISynchronizeInvoke parent)
        {
            _parent = parent;
        }

        public void StartPlayTimer()
        {
            if (IsDisposing) return;
            // Set up timer.
            _Status = JobStatusType.Playing;
            NotifyPropertyChanged("Status");
            playTimer = new System.Timers.Timer();
            playTimer.AutoReset = false;
            playTimer.Elapsed += playTimer_Elapsed;
            playTimer.Interval = Duration;
            playTimer.Start();
        }

        void playTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _Status = JobStatusType.Played;
            NotifyPropertyChanged("Status");
        }

        bool _IsComment;
        public bool IsComment
        {
            get { return _IsComment; }
            set { _IsComment = value; NotifyPropertyChanged("IsComment"); }
        }

        string _Text;
        public string Text
        {
            get { return _Text; }
            set { _Text = value; NotifyPropertyChanged("Text"); }
        }

        string _Group;
        public string Group
        {
            get { return _Group; }
            set { _Group = value; NotifyPropertyChanged("Group"); }
        }

        string _Xml;
        public string Xml
        {
            get { return _Xml; }
            set { _Xml = value; NotifyPropertyChanged("Xml"); }
        }

        byte[] _Data;
        public byte[] WavData
        {
            get { return _Data; }
            set { _Data = value; NotifyPropertyChanged("Data"); }
        }

        Stream _StreamData;
        public Stream StreamData
        {
            get { return _StreamData; }
            set { _StreamData = value; NotifyPropertyChanged("StreamData"); }
        }


        int _Duration;
        public int Duration
        {
            get { return _Duration; }
            set { _Duration = value; NotifyPropertyChanged("Duration"); }
        }

        JobStatusType _Status;
        public JobStatusType Status
        {
            get { return _Status; }
            set { _Status = value; NotifyPropertyChanged("Status"); }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (IsDisposing) return;
            var ev = PropertyChanged;
            if (ev == null) return;
            var p = _parent;
            if (p == null)
            {
                ev(this, new PropertyChangedEventArgs(propertyName));
            }
            else
            {
                p.BeginInvoke((Action)(() =>
                {
                    var args = new PropertyChangedEventArgs(propertyName);
                    ev(this, args);
                }), new object[0]);
            }
        }

        #endregion

        #region IDisposable

        bool IsDisposing;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            IsDisposing = true;
            if (disposing)
            {
                if (playTimer != null)
                {
                    playTimer.Dispose();
                    playTimer = null;
                }
                if (StreamData != null)
                {
                    StreamData.Close();
                    StreamData = null;
                }
            }
        }

        #endregion

    }
}
