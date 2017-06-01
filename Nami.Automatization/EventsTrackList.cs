using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nami.Automatization 
{
    public class EventsTrackList : TrackListBase
    {
        public EventsTrackList()
            : base("EventsTrackList")
        {
            ListPriority = TrackListPriority.Highest;

            //PoC

            //Add new track to tomorrow
            AddTrack(Track.BuildNew("C:\\Test.mp3", DateTime.Now.AddDays(1)));

            //EOF PoC
        }

    }
}
