using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;

using Newtonsoft.Json.Linq;

using practice.Database;
using practice.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Wpf.Ui;
using Wpf.Ui.Controls;

namespace practice.Forms
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : FluentWindow
    {
        public string FilePath { get; set; }
        public bool IsPhotoChanged { get; set; } = false;
        public Profile(User user)
        {
            User = user;
            this.DataContext = this;
            InitializeComponent();
            PhoneTxt.Text = "";
            PhoneTxt.Text = User.Phone;
            if (user.Gender.Equals("женский"))
            {
                GenderCB.SelectedIndex = 1;
            }
            else
            {
                GenderCB.SelectedIndex = 0;
            }

        }
        public User User { get; set; }


        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0))
            { e.Handled = true; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            User.Surname = SurnameTxt.Text;
            User.Name = NameTxt.Text;
            User.Patronomic = PatronomicTxt.Text;
            User.Phone = new string(PhoneTxt.Text.Where(t => Char.IsDigit(t)).ToArray());
            User.Email = EmailTxt.Text;

            if (Check() && CheckEmail() && CheckPhone())
            {
                if (IsPhotoChanged) 
                {
                    Task<string> task = UploadImage(FilePath);
                    task.ContinueWith(t =>
                    {
                        if (!task.IsFaulted)
                        {
                            User.Photo = task.Result;
                        }
                        PracticeContext.Instance.Users.Update(User);

                        PracticeContext.Instance.SaveChanges();

                    });
                }

                try
                {
                    PracticeContext.Instance.Users.Update(User);

                    PracticeContext.Instance.SaveChanges();
                    SnackbarService snackbarService = new SnackbarService();
                    snackbarService.SetSnackbarPresenter(snack);
                    snackbarService.Show(
                        "Готов",
                        "Данные профиля обновлены",
                        ControlAppearance.Success,
                        new SymbolIcon(SymbolRegular.Checkmark12),
                        TimeSpan.FromSeconds(3)
                        );
                }
                catch (Exception ex)
                {
                    SnackbarService snackbarService = new SnackbarService();
                    snackbarService.SetSnackbarPresenter(snack);
                    snackbarService.Show(
                        "Ошибка",
                        "Не удалось обновить данные профиля",
                        ControlAppearance.Danger,
                        new SymbolIcon(SymbolRegular.Checkmark12),
                        TimeSpan.FromSeconds(3)
                        );
                }
               
            }
            else
            {
                SnackbarService snackbarService = new SnackbarService();
                snackbarService.SetSnackbarPresenter(snack);
                snackbarService.Show(
                    "Ошибка",
                    "Правильно заполните все поля",
                    ControlAppearance.Danger,
                    new SymbolIcon(SymbolRegular.SlideText16),
                    TimeSpan.FromSeconds(3)
                    );
            }


            //switch (user.RoleId)
            //{
            //    case 3:
            //        {
            //            ActivityJury activityJury = new ActivityJury();
            //            activityJury.Id = db.ActivityJures.Max(x => x.Id)+1;
            //            activityJury.JuryID = user.Id;
            //            activityJury.ActivityId = db.Activites.Where(x => x.Name == ActivityCB.SelectedItem.ToString()).FirstOrDefault().ID;
            //            db.ActivityJures.Add(activityJury);
            //            break;
            //        }
            //}
            //db.SaveChanges();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private bool Check()
        {
            if (SurnameTxt.Text.IsNullOrEmpty() ||
                NameTxt.Text.IsNullOrEmpty() ||
                PatronomicTxt.Text.IsNullOrEmpty() ||
                PhoneTxt.Text.IsNullOrEmpty() ||
                string.IsNullOrWhiteSpace(SurnameTxt.Text) ||
                string.IsNullOrWhiteSpace(NameTxt.Text) ||
                string.IsNullOrWhiteSpace(PatronomicTxt.Text) ||
                string.IsNullOrWhiteSpace(PhoneTxt.Text) ||
                EmailTxt.Text.IsNullOrEmpty())
            {
                return false;
            }
            return true;
        }
        private bool CheckEmail()
        {
            Regex regex = new Regex(@"([a-zA-Z0-9.-]+@[a-zA-Z0-9.-]+.[a-zA-Z0-9_-]+)");
            MatchCollection matchCollection = regex.Matches(EmailTxt.Text);
            if (matchCollection.Count < 1)
            {
                return false;
            }
            return true;
        }
        private bool CheckPhone()
        {
            if (User.Phone.Length < 11)
            {
                return false;
            }
            return true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (GenderCB.SelectedIndex)
            {
                case 0:
                    {
                        User.Gender = "мужской";
                        break;
                    }
                case 1:
                    {
                        User.Gender = "женский";
                        break;
                    }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                IsPhotoChanged = true;
                FilePath = openFile.FileName;
                ImagePhoto.Source = new BitmapImage(new Uri(FilePath, UriKind.Absolute));
            }
        }
        async public static Task<string> UploadImage(string imagePath)
        {
            string apiUrl = "http://hnt8.ru:7563/upload"; // URL вашего сервера для загрузки изображения

            using (var httpClient = new HttpClient())
            {
                using (var form = new MultipartFormDataContent())
                {
                    // Читаем изображение из файла
                    using (var imageStream = File.OpenRead(imagePath))
                    {
                        // Создаем содержимое для загрузки
                        var imageContent = new StreamContent(imageStream);
                        form.Add(imageContent, "image", System.IO.Path.GetFileName(imagePath)); // Параметр "image" должен соответствовать имени поля в вашем запросе

                        // Отправляем POST запрос на сервер
                        var response = await httpClient.PostAsync(apiUrl, form);

                        // Проверяем ответ
                        if (response.IsSuccessStatusCode)
                        {
                            string responseContent = await response.Content.ReadAsStringAsync();
                            JObject obj = JObject.Parse(responseContent);
                            return obj["imageUrl"].ToString();
                            
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
    }
}
