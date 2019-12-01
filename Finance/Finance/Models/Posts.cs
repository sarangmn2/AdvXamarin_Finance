using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace Finance.Models
{
    // Rss feed link: https://www.nasa.gov/rss/dyn/educationnews.rss
    // contains rss > channel > item > (a) title (b) link (c) description (d) enclosure: url, length, type attribute 
    //...(e) guid : isPermaLink attribute and a link to image (f) pubdate (g) source: url attribute
    // let's create an item class since it's the main child structure in the rss feed
        // start with Item class and then you will see why enclosure, GUid, and source class is created here first
    public class Enclosure
    {
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }
        [XmlAttribute(AttributeName ="length")]
        public uint Length { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
    }
    public class GUID
    {
        [XmlAttribute(AttributeName ="isPermaLink")]
        public Boolean IsPermaLink { get; set; }
        public string ImageLink { get; set; }
    }
    public class Source
    {
        [XmlAttribute(AttributeName ="url")]
        public string Url { get; set; }
    }
    [XmlRoot(ElementName ="item")]
    public class Item
    {
        // within item we have several properties and each with an elementName
        [XmlElement(ElementName ="title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "link")]
        public string ItemLink { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        // next is enclosure. Enclosure itself has a URL attribute, so let's create a separate Enclosure class with url property first and then create a property here of type Enclosure
        [XmlElement(ElementName = "enclosure")]
        public Enclosure Enclosure { get; set; }
        // next is Guid that has isPermalink boolean attribute and then a link to the image. So let's create a class for Guid first
        [XmlElement(ElementName = "guid")]
        public GUID Guid { get; set; }
        // next is pubDate. if you see its value it's as a string, that w will need to convert. So let's create a full property that takes in a value and then assigns parsed value to another property
        [XmlElement(ElementName = "pubDate")]
        private string pubDate;
        public DateTime PublishedDate { get; private set; } // this was generated after we started  on entering full property for pubDate and assigning the parsedDate to a new property variable PublishedDate. 
        // VS intellisense showed that I need to generate a property and did it when I prssed alt enter
        public string PubDate
        {
            get { return pubDate; }
            set { 
                pubDate = value;
                // let's parse pubDate and store into another property value. CultureInfo is in the System.Globalization namespace
                PublishedDate = DateTime.ParseExact(pubDate, "ddd, dd MMM yyyy HH:mm EDT", CultureInfo.InvariantCulture);
            }
        }
        [XmlElement(ElementName = "source")]
        public Source Source { get; set; }
        [XmlElement(ElementName ="dc", Namespace = "http://purl.org/dc/elements/1.1/")]
        public string Dc { get; set; }

    }

    //now item is under channel root, so let's create channel Class
   [XmlRoot(ElementName = "channel")]
   public class Channel
    {
        //channel has multple childs (a) Title (b) Description (c) Link (d) ...Item observable cllection class
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "link")]
        public string Link { get; set; }
        [XmlElement(ElementName = "item")]
        public ObservableCollection<Item> Items { get; set; }
    }
   [XmlRoot(ElementName ="rss")]
   public class Posts
    {
        [XmlElement(ElementName ="channel")]
        public Channel Channel { get; set; }

    }
}
