using System.Runtime.Serialization;

namespace Service.Contracts
{
    [DataContract]
    public class Color
    {
        [DataMember]
        public byte A { get; set; }
        
        [DataMember]
        public byte R { get; set; }

        [DataMember]
        public byte G { get; set; }

        [DataMember]
        public byte B { get; set; }
    }
}