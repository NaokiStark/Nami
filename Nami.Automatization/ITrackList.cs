using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nami.Automatization
{
    public interface ITrackList
    {
        string Name { get; set; }
        List<Track> Tracks { get; set; }
        TrackListPriority ListPriority { get; set; }
    }
}
