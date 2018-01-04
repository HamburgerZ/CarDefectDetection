using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThridLibray;

using Emgu.CV;
using Emgu.Util;
using Emgu.CV.Structure;

namespace SoftwareTrigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            btnClose.Enabled = false;
            btnSoftwareTrigger.Enabled = false;
        }

        // 设备对象
        private ThridLibray.IDevice m_dev;

        // 相机打开回调
        private void OnCameraOpen(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                btnOpen.Enabled = false;
                btnClose.Enabled = true;
                btnSoftwareTrigger.Enabled = true;
            }));
        }

        // 相机关闭回调
        private void OnCameraClose(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                btnSoftwareTrigger.Enabled = false;
            }));
        }

        // 相机丢失回调
        private void OnConnectLoss(object sender, EventArgs e)
        {
            m_dev.ShutdownGrab();
            m_dev.Dispose();
            m_dev = null;

            this.Invoke(new Action(() =>
            {
                btnOpen.Enabled = true;
                btnClose.Enabled = false;
                btnSoftwareTrigger.Enabled = false;
            }));
        }

        /// <summary>
        /// 图像显示
        /// </summary>
        private Graphics _g = null;

        // 码流数据回调
        private void OnImageGrabbed(Object sender, GrabbedEventArgs e)
        {
            
            // 转换帧数据为Bitmap
            var bitmap = e.GrabResult.ToBitmap(false);

            /*
            //转Bitmap图像为Emgucv图像，并进行二值化
            Image<Rgb, byte> original_img = new Image<Rgb, byte>( bitmap );
            var gray_img = original_img.Convert<Gray, Byte>();
            var threshold_img = original_img.Convert<Gray, Byte>();

            CvInvoke.cvThreshold( gray_img, threshold_img, 20, 255, Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY);

            //转Emgucv图像为Bitmap图像
            var show_img = threshold_img.ToBitmap();
            */
             
            
    
            // 显示图片数据
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(() =>
                {
                    try
                    {
                       
                        if (_g == null)
                        {
                            _g = pbImage.CreateGraphics();
                        }
                       
                        /*
                        _g.DrawImage( show_img, new Rectangle(0, 0, pbImage.Width, pbImage.Height),
                            new Rectangle(0, 0, show_img.Width, show_img.Height), GraphicsUnit.Pixel);
                        show_img.Dispose();
                         */
                        
                        _g.DrawImage( bitmap, new Rectangle(0, 0, pbImage.Width, pbImage.Height),
                            new Rectangle(0, 0, bitmap.Width, bitmap.Height), GraphicsUnit.Pixel);
                        bitmap.Dispose();
                        
                        //pbImage.Image = bitmap;                  
                    }
                    catch (Exception exception)
                    {
                        Catcher.Show(exception);
                    }
                }));
            }
        }

        // 打开相机
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                // 设备搜索
                List<IDeviceInfo> li = Enumerator.EnumerateDevices();
                if (li.Count > 0)
                {
                    // 获取搜索到的第一个设备
                    m_dev = Enumerator.GetDeviceByIndex(0);

                    // 注册链接时间
                    m_dev.CameraOpened += OnCameraOpen;
                    m_dev.ConnectionLost += OnConnectLoss;
                    m_dev.CameraClosed += OnCameraClose;

                    // 打开设备
                    if (!m_dev.Open())
                    {
                        MessageBox.Show(@"连接相机失败");
                        return;
                    }

                    // 打开Software Trigger
                    m_dev.TriggerSet.Open(TriggerSourceEnum.Software);

                    // 设置图像格式
                    using (IEnumParameter p = m_dev.ParameterCollection[ParametrizeNameSet.ImagePixelFormat])
                    {
                        p.SetValue("BayerRG12Packed");
                    }
                    // 设置图片亮度
                    using (IIntegraParameter p = m_dev.ParameterCollection[ParametrizeNameSet.Brightness] )
                    {
                        p.SetValue(100);
                    }
                    //设置曝光时间
                    using ( IFloatParameter p = m_dev.ParameterCollection[ParametrizeNameSet.ExposureTime])
                    {
                        p.SetValue(50000);
                    }
                    /*
                    // 设置图片高度
                    using (IIntegraParameter p = m_dev.ParameterCollection[ParametrizeNameSet.ImageHeight])
                    {
                        p.SetValue(600);
                    }
                    // 设置图片宽度
                    using (IIntegraParameter p = m_dev.ParameterCollection[ParametrizeNameSet.ImageWidth])
                    {
                        p.SetValue(600);
                    }
                    */

                    // 注册码流回调事件
                    m_dev.StreamGrabber.ImageGrabbed += OnImageGrabbed;

                    // 开启码流
                    if (!m_dev.GrabUsingGrabLoopThread())
                    {
                        MessageBox.Show(@"开启码流失败");
                        return;
                    }

                    m_dev.ExecuteSoftwareTrigger();
                }
            }
            catch (Exception exception)
            {
                Catcher.Show(exception);
            }
        }

        // 关闭相机
        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dev == null)
                {
                    throw new InvalidOperationException("Device is invalid");
                }

                m_dev.ShutdownGrab();
                m_dev.Close();
            }
            catch (Exception exception)
            {
                Catcher.Show(exception);
            }
        }

        // 窗口关闭
        protected override void OnClosed(EventArgs e)
        {
            if (m_dev != null)
            {
                m_dev.Dispose();
                m_dev = null;
            }
           
            if (_g != null)
            {
                _g.Dispose();
                _g = null;
            }
          
            base.OnClosed(e);
        }

        // 执行软触发
        private void btnSoftwareTrigger_Click(object sender, EventArgs e)
        {
            if (m_dev == null)
            {
                throw new InvalidOperationException("Device is invalid");
            }

            try
            {
                m_dev.ExecuteSoftwareTrigger();
            }
            catch (Exception exception)
            {
                Catcher.Show(exception);
            }
        }

    }
}
