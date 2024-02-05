﻿using System;
using EasyCaptcha.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using ReCaptcha.Desktop.WPF.Client;
using ReCaptcha.Desktop.WPF.Client.Interfaces;
using ReCaptcha.Desktop.WPF.Configuration;
using ReCaptcha.Desktop.WPF.UI;
using System.Threading;
using Wpf.Ui.Controls;

namespace practice.Forms
{
    /// <summary>
    /// Логика взаимодействия для Captcha.xaml
    /// </summary>
    public partial class Captcha : UiWindow
    {
        public Captcha()
        {
            InitializeComponent();
            MyCaptcha.CreateCaptcha(EasyCaptcha.Wpf.Captcha.LetterOption.Alphanumeric, 4);         
        }
        private async Task<string> InitRecaptcha()
        {
            ReCaptchaConfig config = "6LeQelkpAAAAAJXVyaw3nBW7YIT61LGFPqU_wqlw".AsReCaptchaConfig("HOST_NAME", "en", "Token recieved: %token%", "Token recieved and sent to application.");


            WindowConfig uiConfig = new("WINDOW_TITLE"); // WPF
            IReCaptchaClient reCaptcha = new ReCaptchaClient(config, uiConfig);
            CancellationTokenSource cts = new(TimeSpan.FromMinutes(1));
            return await reCaptcha.VerifyAsync(cts.Token);
        }

      

        private void btnCapt_Click(object sender, RoutedEventArgs e)
        {
            var answer = MyCaptcha.CaptchaText;
            if (/*txtCapt.Text == answer*/ true)
            {
                this.Close();
            }
            
        }

    }
}
