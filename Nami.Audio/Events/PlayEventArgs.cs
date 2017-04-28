using System;

namespace Nami.Audio.Events
{
    public class PlayEventArgs : EventArgs, IPlayerEventArgs
    {
        public string File { get; set; }
    }
}