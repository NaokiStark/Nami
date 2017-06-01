using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nami.Automatization
{
    public class MusicScheduleTrackList : TrackListBase
    {
        public DateTime PlayTime { get; set; }
        public MusicScheduleTrackList()
            : base("MusicScheduleTrackList")
        {

        }
    }
}
