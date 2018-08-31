using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Controls;
using System.Windows;

namespace QQVideo.Utils
{
    public class VideoUtil
    {
        private delegate void delegateValue();
        public MediaElement player;
        public TimeSpan duration { get; set; }
        public TimeSpan current{ get; set; }
        public VideoUtil(MediaElement player)
        {
            this.player = player;
            player.LoadedBehavior = MediaState.Manual;
        }
        public void PlayVideo(string file)//播放视频，返回总长度
        {
            player.Source = new Uri(file);
            player.MediaOpened += getDuration;
            player.Play();

        }
        private void getDuration(object sender, RoutedEventArgs e)
        {
            duration = player.NaturalDuration.TimeSpan;
        }
        public void PauseVideo()//暂停视频
        {
            player.Pause();
        }
        public void StopVideo()//停止视频
        {
            player.Stop();
        }
        public void SetVideo(double rate)//跳播视频
        {
            player.Position = TimeSpan.FromSeconds(duration.TotalSeconds * rate);
            //MessageBox.Show(duration.ToString());
            player.Play();
        }

        public double ReturnValue()
        {
            player.Dispatcher.Invoke(new delegateValue(updateSlider));
            return current.TotalSeconds/duration.TotalSeconds*100;
        }
        public void updateSlider()
        {
            current = player.Position ;
        }
        public void ResumeVideo()
        {
            player.Play();
        }


    }
}
