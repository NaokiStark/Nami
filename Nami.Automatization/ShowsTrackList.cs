using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nami.Automatization
{
    public class ShowsTrackList : TrackListBase
    {
        public ShowsTrackList()
            : base("ShowsTrackList")
        {
            ListPriority = TrackListPriority.Highest;
        }
    }
}
