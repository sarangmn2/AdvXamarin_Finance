using Finance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace Finance.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        public Posts Blog { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public MainVM()
        {
            // call ReadRss method that will be declared and implemented in this class
            ReadRss();

        }

        public void ReadRss()
        {
            // this section of the code will create a new XMLSerializer object (from namespace System.Xml.Serialization;) of Type Posts
            // followed by creating a new WebClient (namespace : System.Net) that will refer to the Rss feed
            // and then createa  new "reader" object that will read the xml string and insert that into Blog
            // 
            XmlSerializer serializer = new XmlSerializer(typeof(Posts));
            using(WebClient client = new WebClient())
            {
                string xml = Encoding.Default.GetString(client.DownloadData("https://www.nasa.gov/rss/dyn/educationnews.rss"));
                // read the xml string by declaring "reader" of type Stream (namespace: System.IO that is a new MemoryStream object
                using (Stream reader = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    Blog = (Posts)serializer.Deserialize(reader);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
