using Nami.Audio;
using Nami.Audio.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nami
{
    public partial class Form1 : Form
    {
        NAudioPlayer player;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            player = new NAudioPlayer();
            player.OnPeak += Player_OnPeak;
            FormClosing += (obj, args)=>{
                player?.Dispose();
            };
        }

        private void Player_OnPeak(IPlayerEventArgs args)
        {
            PeakEventArgs pArgs = (PeakEventArgs)args;

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
