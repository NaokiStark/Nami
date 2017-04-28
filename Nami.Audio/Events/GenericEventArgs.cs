using System;

namespace Nami.Audio.Events
{
    public class GenericEventArgs : EventArgs, IPlayerEventArgs
    {
        public string File { get; set; }
    }
}