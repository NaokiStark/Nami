using System;

namespace Nami.Audio.Events
{
    public class PeakEventArgs : EventArgs, IPlayerEventArgs
    {
        public float Left { get; set; }
        public float Right { get; set; }
    }
}