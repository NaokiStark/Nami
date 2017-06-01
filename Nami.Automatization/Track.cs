using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nami.Automatization
{
    public class Track
    {
        public string FileName { get; set; }
        public string Name {get;set;}
        public DateTime PlayTime { get; set; }

        /// <summary>
        /// Builds a new track
        /// </summary>
        /// <param name="fileName">File path</param>
        /// <param name="playTime">Date to start playing this track</param>
        /// <returns></returns>
        public static Track BuildNew(string fileName, DateTime playTime)
        {
            return new Track {
                FileName = fileName,
                Name = new FileInfo(fileName).Name,
                PlayTime = playTime
            };
        }
    }
}
