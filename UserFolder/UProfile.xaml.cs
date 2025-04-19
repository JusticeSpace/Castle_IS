using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;

namespace Castle.UserFolder
{
    public partial class UProfile : Page
    {
        private readonly Amo_CastleEntities1 _context;
        private User _currentUser;
        private byte[] _photoData;

        public UProfile()
        {
            InitializeComponent();
            _context = new Amo_CastleEntities1();
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                _currentUser = _context.User
                    .Include(u => u.Roles)
                    .FirstOrDefault(u => u.IdUser == App.CurrentUserId);

                if (_currentUser != null)
                {
                    DataContext = _currentUser;

                    // Проверяем, есть ли фото (PhotoID не null)
                    if (_currentUser.PhotoID.HasValue)
                    {
                        var photo = _context.Photos
                            .FirstOrDefault(p => p.PhotoID == _currentUser.PhotoID);
                        if (photo != null && photo.Photo != null && photo.Photo.Length > 0)
                        {
                            try
                            {
                                _photoData = photo.Photo;
                                using (var stream = new MemoryStream(_photoData))
                                {
                                    var bitmap = new BitmapImage();
                                    bitmap.BeginInit();
                                    bitmap.StreamSource = stream;
                                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                                    bitmap.EndInit();
                                    UserImage.Source = bitmap;
                                    UserImage.Visibility = Visibility.Visible;
                                    ImageStatusText.Visibility = Visibility.Collapsed;
                                }
                            }
                            catch (Exception ex)
                            {
                                // Если не удалось обработать изображение, показываем статус "Нет фото"
                                UserImage.Source = null;
                                UserImage.Visibility = Visibility.Collapsed;
                                ImageStatusText.Visibility = Visibility.Visible;
                                MessageBox.Show($"Не удалось загрузить фото: {ex.Message}", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        else
                        {
                            // Если фото отсутствует или повреждено, показываем статус
                            UserImage.Source = null;
                            UserImage.Visibility = Visibility.Collapsed;
                            ImageStatusText.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        // Если PhotoID отсутствует, просто показываем статус
                        UserImage.Source = null;
                        UserImage.Visibility = Visibility.Collapsed;
                        ImageStatusText.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь не найден!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UploadPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _photoData = File.ReadAllBytes(openFileDialog.FileName);
                    UserImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                    UserImage.Visibility = Visibility.Visible;
                    ImageStatusText.Visibility = Visibility.Collapsed;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось загрузить фото: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_photoData != null)
                {
                    if (_currentUser.PhotoID.HasValue)
                    {
                        var photo = _context.Photos
                            .FirstOrDefault(p => p.PhotoID == _currentUser.PhotoID);
                        if (photo != null)
                        {
                            photo.Photo = _photoData;
                        }
                    }
                    else
                    {
                        var newPhoto = new Photos
                        {
                            Photo = _photoData
                        };
                        _context.Photos.Add(newPhoto);
                        _context.SaveChanges();
                        _currentUser.PhotoID = newPhoto.PhotoID;
                    }
                }

                _context.SaveChanges();
                MessageBox.Show("Изменения сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow(_currentUser.IdUser);
            changePasswordWindow.Owner = Window.GetWindow(this);
            changePasswordWindow.ShowDialog();
        }
    }
}