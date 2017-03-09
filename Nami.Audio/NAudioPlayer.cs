using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nami.Audio
{
    public class NAudioPlayer
    {

        IWavePlayer waveOut;
        AudioFileReader audioFile;
        MeteringSampleProvider meterSampleProvider;

        /// <summary>
        /// Position in milliseconds
        /// </summary>
        public long RawPosition
        {
            get;
            set;
        }

        /// <summary>
        /// Volume of player
        /// </summary>
        public float Volume
        {
            get
            {
                if(audioFile != null)
                {
                    return audioFile.Volume;
                }

                return _volume;
            }

            set
            {
                if(audioFile != null)
                {
                    audioFile.Volume = value;
                }

                _volume = Volume;
            }
        }

        /// <summary>
        /// Returns a peak value on the moment when called on channel left
        /// </summary>
        public float PeakValueLeft
        {
            get;
            set;
        }

        /// <summary>
        /// Returns a peak value on the moment when called on channel Right
        /// </summary>
        public float PeakValueRight
        {
            get;
            set;
        }

        private float _volume
        {
            get;
            set;
        }



        /// <summary>
        /// New Instance of NAudioPlayer
        /// </summary>
        public NAudioPlayer()
        {
            waveOut = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 100);
            waveOut.PlaybackStopped += waveOut_PlaybackStopped;
        }

        private void waveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            
        }

        public void Play(string File)
        {
            audioFile = new AudioFileReader(File);
            meterSampleProvider = new MeteringSampleProvider(audioFile);
            meterSampleProvider.SamplesPerNotification = 100;
            meterSampleProvider.StreamVolume += MeterSampleProvider_StreamVolume;

            audioFile.Volume = _volume;
            waveOut.Init(meterSampleProvider);
            
            waveOut.Play();
        }

        private void MeterSampleProvider_StreamVolume(object sender, StreamVolumeEventArgs e)
        {
            PeakValueLeft = Normalize(e.MaxSampleValues[0]);
            PeakValueRight = Normalize(e.MaxSampleValues[1]);
        }

        public void Play(FileInfo File)
        {
            Play(File.FullName);
        }

        public void Stop()
        {

        }

        public void Pause()
        {

        }

        /// <summary>
        /// Normalizes audio 
        /// </summary>
        /// <param name="value">Raw value</param>
        /// <returns></returns>
        private float Normalize(float value)
        {
            if (Volume == 0) return 0;
            return ((value) * (1 / Volume));
        }
    }
}
