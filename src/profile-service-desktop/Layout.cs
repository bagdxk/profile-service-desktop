using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bagdxk.Profile.Service.Desktop
{
    [DataContract]
    public class Layout
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string WindowState { get; set; }
        [DataMember]
        public double Width { get; set; }
        [DataMember]
        public double Height { get; set; }
        [DataMember]
        public double Left { get; set; }
        [DataMember]
        public double Top { get; set; }
    }
}
