using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Nami.Audio.Events;
using System.Runtime.InteropServices;

namespace Nami.Audio
{
    public class NAudioPlayer : IDisposable
    {

        IWavePlayer waveOut;
        AudioFileReader audioFile;
        MeteringSampleProvider meterSampleProvider;

        public delegate void AudioEventHandler(IPlayerEventArgs args);
        public event AudioEventHandler OnPlay;
        public event AudioEventHandler OnStop;
        public event AudioEventHandler OnPause;
        public event AudioEventHandler OnPeak;

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
                    _volume = value;
                }

                _volume = value;
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
            OnStop?.Invoke(new GenericEventArgs());
        }

        public void Play(string File)
        {
            try
            {
                if(waveOut != null)
                {
                    if(waveOut.PlaybackState != PlaybackState.Stopped)
                    {
                        waveOut.Stop();
                        audioFile.Dispose();
                        waveOut.Dispose();
                    }
                }

                waveOut = new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, 100);
                audioFile = new AudioFileReader(File);
                meterSampleProvider = new MeteringSampleProvider(audioFile);
                meterSampleProvider.SamplesPerNotification = 100;
                meterSampleProvider.StreamVolume += MeterSampleProvider_StreamVolume;

                audioFile.Volume = _volume;
                waveOut.Init(meterSampleProvider);
                

                waveOut.Play();
                OnPlay?.Invoke(new PlayEventArgs { File = File });
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
           
        }

        private void MeterSampleProvider_StreamVolume(object sender, StreamVolumeEventArgs e)
        {
            PeakValueLeft = Normalize(e.MaxSampleValues[0]);

            if(e.MaxSampleValues.Length > 1)
            {
                PeakValueRight = Normalize(e.MaxSampleValues[1]);
            }
            else
            {
                PeakValueRight = PeakValueLeft;
            }

            OnPeak?.Invoke(new PeakEventArgs {
                Left = PeakValueLeft,
                Right = PeakValueRight
            });
          
        }       

        public void Play(FileInfo File)
        {
            Play(File.FullName);
        }

        public void Stop()
        {
            waveOut?.Stop();
            
        }

        public void Pause()
        {
            if (waveOut.PlaybackState != PlaybackState.Stopped)
            {
                if(waveOut.PlaybackState == PlaybackState.Paused)
                {
                    waveOut?.Play();
                }
                else
                {
                    waveOut?.Pause();
                    OnPause?.Invoke(new GenericEventArgs());
                }                
                
            }
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

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {

            waveOut?.Stop();
            waveOut?.Dispose();
        }
    }
}
