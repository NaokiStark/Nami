using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nami.Automatization
{
    public class TrackListBase : ITrackList
    {
        /// <summary>
        /// Name of track list
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of Tracks
        /// </summary>
        public List<Track> Tracks { get; set; }

        /// <summary>
        /// List Priority, TrackListPriority.Normal by default
        /// </summary>
        public TrackListPriority ListPriority { get; set; }

        /// <summary>
        /// New Instance of TrackListBase
        /// </summary>
        /// <param name="TrackListName">TrackList Name</param>
        public TrackListBase(string TrackListName)
        {
            ListPriority = TrackListPriority.Normal;
            Tracks = new List<Track>();
            Name = TrackListName;
        }

        /// <summary>
        /// Adds a new track
        /// </summary>
        /// <param name="track">Track object</param>
        public void AddTrack(Track track)
        {
            Tracks.Add(track);
        }
    }
}
