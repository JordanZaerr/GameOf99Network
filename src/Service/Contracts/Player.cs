using System.Runtime.Serialization;

namespace Service.Contracts
{
    [DataContract]
    public class Player
    {
        [DataMember]
        public int Id { get; internal set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Color Color { get; set; }

        internal IGameServiceEvents EventsHandler { get; set; }
    }
}