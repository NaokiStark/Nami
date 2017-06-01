using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nami.Automatization
{
    public class MusicTrackListWrapper
    {
        List<MusicScheduleTrackList> TrackLists;

        public MusicTrackListWrapper()
        {
            TrackLists = new List<MusicScheduleTrackList>();
            
            //PoC
            MusicScheduleTrackList countryTrackList = new MusicScheduleTrackList {
                PlayTime = DateTime.Now.AddDays(3) 
            };

            countryTrackList.AddTrack(
                Track.BuildNew("C:\\CountryTheme.mp3", DateTime.Now)
                );
            countryTrackList.AddTrack(
                Track.BuildNew("C:\\AnotherCountryTheme.mp3", DateTime.Now)
                );

            TrackLists.Add(countryTrackList);
            //EOF PoC
        }
    }
}
