using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using DiscordRPC;
using DiscordRPC.Logging;
using System.Globalization;
using System.Windows.Data;

namespace RuGJlauncher
{
    public partial class MainWindow : Window
    {
        private bool isDownloading = false;
        private bool isFileDownloaded = false;
        private bool isFileDownloaded_2 = false;
        private bool isFileDownloaded_3 = false;
        private bool isFileDownloaded_4 = false;
        private DiscordRpcClient client;

        private long totalBytes;
        private long downloadedBytes;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Normal;
            StatusTextBlock.Text = "";

            // Инициализация Discord RPC
            client = new DiscordRpcClient("1317174842273038418");
            client.Initialize();

            client.SetPresence(new RichPresence()
            {
                Details = "Просматривает игры",
                State = "В главном меню",
                Assets = new Assets()
                {
                    LargeImageKey = "logo",
                    LargeImageText = "RuGJlauncher"
                }
            });
        }
        
        private void GamesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesList.SelectedItem == null)
            {
                // Если ничего не выбрано, скрываем все превью
                HoverImage.Visibility = Visibility.Collapsed;
                HoverImage_2.Visibility = Visibility.Collapsed;
                gifl.Visibility = Visibility.Collapsed;
                return;
            }

            ListBoxItem selectedItem = (ListBoxItem)GamesList.SelectedItem;
            string tag = selectedItem.Tag.ToString();

            // Показываем превью в зависимости от выбранной игры
            switch (tag)
            {
                case "1":
                    HoverImage.Visibility = Visibility.Visible;
                    HoverImage_2.Visibility = Visibility.Collapsed;
                    gifl.Visibility = Visibility.Collapsed;
                    HoverImage_4.Visibility = Visibility.Collapsed;
                    break;
                case "2":
                    HoverImage.Visibility = Visibility.Collapsed;
                    HoverImage_2.Visibility = Visibility.Visible;
                    gifl.Visibility = Visibility.Collapsed;
                    HoverImage_4.Visibility = Visibility.Collapsed;
                    break;
                case "3":
                    HoverImage.Visibility = Visibility.Collapsed;
                    HoverImage_2.Visibility = Visibility.Collapsed;
                    gifl.Visibility = Visibility.Visible;
                    HoverImage_4.Visibility = Visibility.Collapsed;
                    break;
                case "4":
                    HoverImage.Visibility = Visibility.Collapsed;
                    HoverImage_2.Visibility = Visibility.Collapsed;
                    gifl.Visibility = Visibility.Collapsed;
                    HoverImage_4.Visibility = Visibility.Visible;
                    break;
            }
        }
        private void LaunchSelectedGame(object sender, RoutedEventArgs e)
        {
            if (GamesList.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите игру из списка", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ListBoxItem selectedItem = (ListBoxItem)GamesList.SelectedItem;
            string tag = selectedItem.Tag.ToString();

            switch (tag)
            {
                case "1": // МСИБ
                    if (File.Exists(@"Resources/BackRooms.exe"))
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Играет в Лунтик в Бекрумс",
                            State = "В игре", 
                            Assets = new Assets()
                            {
                                LargeImageKey = "backrooms",
                                LargeImageText = "Лунтик в Бекрумс"
                            }
                        });
                        LaunchGame_Click(this, null);
                    }
                    else
                    {
                        DownloadButton_Click(this, null);
                    }
                    break;

                case "2": // Moonzy X Fanmade
                    if (File.Exists(@"Resources/luntikxgame.exe"))
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Играет в Лунтик X",
                            State = "В игре",
                            Assets = new Assets()
                            {
                                LargeImageKey = "luntikx",
                                LargeImageText = "Лунтик X"
                            }
                        });
                        LaunchGame_Click_2(this, null);
                    }
                    else
                    {
                        DownloadButton_Click_2(this, null);
                    }
                    break;

                case "3": // Moonzy X Remake
                    if (File.Exists(@"Resources/luntikxremake.exe"))
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Играет в Лунтик X Remake",
                            State = "В игре",
                            Assets = new Assets()
                            {
                                LargeImageKey = "luntixremake",
                                LargeImageText = "Лунтик X Remake"
                            }
                        });
                        LaunchGame_Click_3(this, null);
                    }
                    else
                    {
                        DownloadButton_Click_3(this, null);
                    }
                    break;

                case "4": // Lapik The Game
                    if (File.Exists(@"Resources/lapik.exe"))
                    {
                        client.SetPresence(new RichPresence()
                        {
                            Details = "Играет в Lapik The Game",
                            State = "В игре",
                            Assets = new Assets()
                            {
                                LargeImageKey = "lapik",
                                LargeImageText = "Lapik The Game"
                            }
                        });
                        LaunchGame_Click_4(this, null);
                    }
                    else
                    {
                        DownloadButton_Click_4(this, null);
                    }
                    break;

                default:
                    MessageBox.Show("Неизвестная игра", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void DeleteFile(object sender, RoutedEventArgs e)
        {
            if (GamesList.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите игру для удаления", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedItem = (ListBoxItem)GamesList.SelectedItem;
            string filePath = "";

            switch (selectedItem.Tag.ToString())
            {
                case "1":
                    filePath = @"Resources/BackRooms.exe";
                    break;
                case "2": 
                    filePath = @"Resources/luntikxgame.exe";
                    break;
                case "3":
                    filePath = @"Resources/luntikxremake.exe";
                    break;
                case "4":
                    filePath = @"Resources/lapik.exe";
                    break;
            }

            if (File.Exists(filePath))
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить эту игру?", "Подтверждение", 
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        File.Delete(filePath);
                        MessageBox.Show("Файл успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении файла: {ex.Message}", "Ошибка", 
                            MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        


        //открытие игр
        private void LaunchGame_Click(object sender, RoutedEventArgs e)
        {
            string gamePath = @"Resources/BackRooms.exe";

            try
            {
                if(!File.Exists(@"Resources/BackRooms.exe"))
                {
                    DownloadButton_Click(this, null);
                    return;
                }
                if(File.Exists(@"Resources/BackRooms.exe")) 
                {
                    var process = Process.Start(gamePath);
                    process.EnableRaisingEvents = true;
                    process.Exited += (s, args) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            client.SetPresence(new RichPresence()
                            {
                                Details = "Просматривает игры",
                                State = "В главном меню",
                                Assets = new Assets()
                                {
                                    LargeImageKey = "logo",
                                    LargeImageText = "RuGJlauncher"
                                }
                            });
                        });
                    };
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Не удалось запустить игру: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LaunchGame_Click_2(object sender, RoutedEventArgs e)
        {
            string gamePath = @"Resources/luntikxgame.exe";

            try
            {
                if(!File.Exists(@"Resources/luntikxgame.exe"))
                {
                    DownloadButton_Click_2(this, null);
                    return;
                }
                if(File.Exists(@"Resources/luntikxgame.exe")) 
                {
                    var process = Process.Start(gamePath);
                    process.EnableRaisingEvents = true;
                    process.Exited += (s, args) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            client.SetPresence(new RichPresence()
                            {
                                Details = "Просматривает игры",
                                State = "В главном меню",
                                Assets = new Assets()
                                {
                                    LargeImageKey = "logo",
                                    LargeImageText = "RuGJlauncher"
                                }
                            });
                        });
                    };
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Не удалось запустить игру: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LaunchGame_Click_3(object sender, RoutedEventArgs e)
        {
            string gamePath = @"Resources/luntikxRemake.exe";

            try
            {
                if(!File.Exists(@"Resources/luntikxRemake.exe"))
                {
                    DownloadButton_Click_3(this, null);
                    return;
                }
                if(File.Exists(@"Resources/luntikxRemake.exe")) 
                {
                    var process = Process.Start(gamePath);
                    process.EnableRaisingEvents = true;
                    process.Exited += (s, args) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            client.SetPresence(new RichPresence()
                            {
                                Details = "Просматривает игры",
                                State = "В главном меню",
                                Assets = new Assets()
                                {
                                    LargeImageKey = "logo",
                                    LargeImageText = "RuGJlauncher"
                                }
                            });
                        });
                    };
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Не удалось запустить игру: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LaunchGame_Click_4(object sender, RoutedEventArgs e)
        {
            string gamePath = @"Resources/lapik.exe";

            try
            {
                if(!File.Exists(@"Resources/lapik.exe"))
                {
                    DownloadButton_Click_4(this, null);
                    return;
                }
                if(File.Exists(@"Resources/lapik.exe")) 
                {
                    var process = Process.Start(gamePath);
                    process.EnableRaisingEvents = true;
                    process.Exited += (s, args) =>
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            client.SetPresence(new RichPresence()
                            {
                                Details = "Просматривает игры",
                                State = "В главном меню",
                                Assets = new Assets()
                                {
                                    LargeImageKey = "logo",
                                    LargeImageText = "RuGJlauncher"
                                }
                            });
                        });
                    };
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Не удалось запустить игру: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Credits(object sender, RoutedEventArgs e)
        {
            Window creditsWindow = new Window
            {
                Title = "Благодарности",
                Width = 400,
                Height = 300,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ResizeMode = ResizeMode.NoResize,
                Background = Brushes.Black
            };

            Grid grid = new Grid();
            
            Image image = new Image
            {
                Source = new BitmapImage(new Uri("Resources/ICS.png", UriKind.Relative)),
                Stretch = Stretch.Uniform
            };

            TextBlock creditsText = new TextBlock
            {
                Text = "Фон и иконка - @qoffic\n@TheCreoKaiser - логотип",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom,
                TextAlignment = TextAlignment.Center,
                FontSize = 16,
                Foreground = Brushes.White,
                Margin = new Thickness(0, 0, 0, 10)
            };

            grid.Children.Add(image);
            grid.Children.Add(creditsText);
            
            creditsWindow.Content = grid;
            creditsWindow.ShowDialog();
        }


        //наведение мыши
        private void HoverButton_MouseEnter(object sender, MouseEventArgs e)
        {
            HoverImage.Visibility = Visibility.Visible;
        }

        private void HoverButton_MouseLeave(object sender, MouseEventArgs e)
        {
            HoverImage.Visibility = Visibility.Collapsed;
        }  

//игра про лунтик икс
        private void HoverButton_MouseEnter_2(object sender, MouseEventArgs e)
        {
            HoverImage_2.Visibility = Visibility.Visible;
        }

        private void HoverButton_MouseLeave_2(object sender, MouseEventArgs e)
        {
            HoverImage_2.Visibility = Visibility.Collapsed;
        }

        private void HoverButton_MouseEnter_3(object sender, MouseEventArgs e)
        {
            gifl.Visibility = Visibility.Visible;
        }

        private void HoverButton_MouseLeave_3(object sender, MouseEventArgs e)
        {
            gifl.Visibility = Visibility.Collapsed;
        }

//скачивание игр из url



        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            if(!isDownloading && !File.Exists(@"Resources/BackRooms.exe"))
            {
                string url = "https://drive.usercontent.google.com/download?id=1UmGRYHk7iVlsE262cWy_GARSGtwQUWrF&export=download&authuser=2&confirm=t&uuid=9a0a4344-f9d3-4dc8-bb22-9c16d83d31d3&at=APvzH3qgOhjK5OrwLwWKrty5DRdl:1734082394877"; // Укажите свой URL
                string targetFileName = "BackRooms.exe";
                string targetPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", targetFileName);
    
                DownloadProgress.Visibility = Visibility.Visible;
                StatusTextBlock.Visibility = Visibility.Visible;

                isDownloading = true;

                try
                {
                    await DownloadFileAsync(url, targetPath);
                    isFileDownloaded = true;
                    MessageBox.Show($"Файл успешно загружен!\nПуть: {targetPath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    StatusTextBlock.Visibility = Visibility.Collapsed;
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    DownloadProgress.Visibility = Visibility.Collapsed;
                    isDownloading = false;
                    DownloadProgress.Value = 0;
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Возможно файл уже скачан или другой файл скачивается в данный момент.",
                            " ",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            }
        }

        private async Task DownloadFileAsync(string url, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    totalBytes = response.Content.Headers.ContentLength ?? -1; // Общее количество байтов
                    downloadedBytes = 0;

                    string directoryPath = System.IO.Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var inputStream = await response.Content.ReadAsStreamAsync())
                    using (var outputStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        long totalBytes = response.Content.Headers.ContentLength ?? -1;
                        byte[] buffer = new byte[8192];
                        int bytesRead;
                        long downloadedBytes = 0;

                        while ((bytesRead = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await outputStream.WriteAsync(buffer, 0, bytesRead);
                            downloadedBytes += bytesRead;

                            
                            if (totalBytes > 0)
                            {
                                DownloadProgress.Value = (downloadedBytes * 100) / totalBytes;
                                double totalMB = totalBytes / 1048576.0;
                                double downloadedMB = downloadedBytes / 1048576.0;
                                StatusTextBlock.Text = $"Загружено: {downloadedMB:F2} MB из {totalMB:F2} MB.";
                            }
                        }
                        StatusTextBlock.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }






        private async void DownloadButton_Click_2(object sender, RoutedEventArgs e)
        {
            if(!isDownloading && !File.Exists(@"Resources/luntikxgame.exe"))
            {
                string url = "https://drive.usercontent.google.com/download?id=15y61psDrx-K57e3_ViaA3Xz53Q7Yr97W&export=download&authuser=0&confirm=t&uuid=3465c9a4-e119-467f-8ed9-c54579995c5b&at=APvzH3rUeI2v6LacLbvwgFQhRm2X%3A1734191575201";
                string targetFileName = "luntikxgame.exe";
                string targetPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", targetFileName);

                DownloadProgress.Visibility = Visibility.Visible;
                StatusTextBlock.Visibility = Visibility.Visible;
                isDownloading = true;

                try
                {
                    await DownloadFileAsync_2(url, targetPath);
                    isFileDownloaded_2 = true;
                    MessageBox.Show($"Файл успешно загружен!\nПуть: {targetPath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    StatusTextBlock.Visibility = Visibility.Collapsed;
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    DownloadProgress.Visibility = Visibility.Collapsed;
                    isDownloading = false;
                    DownloadProgress.Value = 0;
                }
            }
            else if(File.Exists(@"Resources/luntikxgame.exe"))
            {
                MessageBox.Show("Ошибка! Файл уже скачан.",
                            " ",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            }
        }
        private async Task DownloadFileAsync_2(string url, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    totalBytes = response.Content.Headers.ContentLength ?? -1; // Общее количество байтов
                    downloadedBytes = 0;

                    string directoryPath = System.IO.Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var inputStream = await response.Content.ReadAsStreamAsync())
                    using (var outputStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        long totalBytes = response.Content.Headers.ContentLength ?? -1;
                        byte[] buffer = new byte[8192];
                        int bytesRead;
                        long downloadedBytes = 0;

                        while ((bytesRead = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await outputStream.WriteAsync(buffer, 0, bytesRead);
                            downloadedBytes += bytesRead;

                            
                            if (totalBytes > 0)
                            {
                                DownloadProgress.Value = (downloadedBytes * 100) / totalBytes;
                                double totalMB = totalBytes / 1048576.0;
                                double downloadedMB = downloadedBytes / 1048576.0;
                                StatusTextBlock.Text = $"Загружено: {downloadedMB:F2} MB из {totalMB:F2} MB.";
                            }
                        }
                        StatusTextBlock.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

    private async void DownloadButton_Click_3(object sender, RoutedEventArgs e)
        {
            if(!isDownloading && !File.Exists(@"Resources/luntikxRemake.exe"))
            {
                string url = "https://drive.usercontent.google.com/download?id=1bBoCo2WJwsQXxkdFe_iaGHPGPNi2Rx02&export=download&authuser=0&confirm=t&uuid=fa16a045-0fad-4abd-a298-1f0d7c4305ea&at=APvzH3qwAR397RfmNe5ZffV06a3B%3A1734173030247";
                string targetFileName = "luntikxRemake.exe";
                string targetPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", targetFileName);

                DownloadProgress.Visibility = Visibility.Visible;
                isFileDownloaded_3 = true;
                StatusTextBlock.Visibility = Visibility.Visible;
                isDownloading = true;

                try
                {
                    await DownloadFileAsync_3(url, targetPath);
                    MessageBox.Show($"Файл успешно загружен!\nПуть: {targetPath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    StatusTextBlock.Visibility = Visibility.Collapsed;
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    DownloadProgress.Visibility = Visibility.Collapsed;
                    isDownloading = false;
                    DownloadProgress.Value = 0;
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Возможно файл уже скачан или другой файл скачивается в данный момент.",
                            " ",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
            }
        }

        private async Task DownloadFileAsync_3(string url, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    totalBytes = response.Content.Headers.ContentLength ?? -1; // Общее количество байтов
                    downloadedBytes = 0;

                    string directoryPath = System.IO.Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var inputStream = await response.Content.ReadAsStreamAsync())
                    using (var outputStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        long totalBytes = response.Content.Headers.ContentLength ?? -1;
                        byte[] buffer = new byte[8192];
                        int bytesRead;
                        long downloadedBytes = 0;

                        while ((bytesRead = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await outputStream.WriteAsync(buffer, 0, bytesRead);
                            downloadedBytes += bytesRead;

                            
                            if (totalBytes > 0)
                            {
                                DownloadProgress.Value = (downloadedBytes * 100) / totalBytes;
                                double totalMB = totalBytes / 1048576.0;
                                double downloadedMB = downloadedBytes / 1048576.0;
                                StatusTextBlock.Text = $"Загружено: {downloadedMB:F2} MB из {totalMB:F2} MB.";
                            }
                        }
                        StatusTextBlock.Visibility = Visibility.Collapsed;
                    }
                }
            }
        } 

        private async void DownloadButton_Click_4(object sender, RoutedEventArgs e)
        {
            if(!isDownloading && !File.Exists(@"Resources/lapik.exe"))
            {
                string url = "https://drive.usercontent.google.com/download?id=1Cd3BKxdukJSqjicHhLrcQk5dDwyBecBq&export=download&authuser=0&confirm=t&uuid=9472385b-334a-4de9-b91b-5d190f8ebd48&at=APvzH3o2l_HF5xiE9804JutZHWuL%3A1734196201401";
                string targetFileName = "lapik.exe";
                string targetPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", targetFileName);

                DownloadProgress.Visibility = Visibility.Visible;
                isFileDownloaded_4 = true;
                StatusTextBlock.Visibility = Visibility.Visible;
                isDownloading = true;

                try
                {
                    await DownloadFileAsync_4(url, targetPath);
                    MessageBox.Show($"Файл успешно загружен!\nПуть: {targetPath}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    StatusTextBlock.Visibility = Visibility.Collapsed;
                    MessageBox.Show($"Ошибка при загрузке файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    StatusTextBlock.Visibility = Visibility.Collapsed;
                    DownloadProgress.Visibility = Visibility.Collapsed;
                    isDownloading = false;
                    DownloadProgress.Value = 0;
                }
            }
            else
            {
                MessageBox.Show("Ошибка! Возможно файл уже скачан или другой файл скачивается в данный момент.",
                            " ",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);
                            StatusTextBlock.Visibility = Visibility.Collapsed;
            }
        }

        private async Task DownloadFileAsync_4(string url, string destinationPath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();

                    totalBytes = response.Content.Headers.ContentLength ?? -1; // Общее количество байтов
                    downloadedBytes = 0;

                    string directoryPath = System.IO.Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    using (var inputStream = await response.Content.ReadAsStreamAsync())
                    using (var outputStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        byte[] buffer = new byte[8192];
                        int bytesRead;

                        while ((bytesRead = await inputStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await outputStream.WriteAsync(buffer, 0, bytesRead);
                            downloadedBytes += bytesRead;

                            if (totalBytes > 0)
                            {
                                double progressPercentage = (double)downloadedBytes / totalBytes * 100;
                                DownloadProgress.Value = progressPercentage;

                                double totalMB = totalBytes / 1048576.0;
                                double downloadedMB = downloadedBytes / 1048576.0;
                                StatusTextBlock.Text = $"Загружено: {downloadedMB:F2} MB из {totalMB:F2} MB";
                            }
                        }
                    }
                }
            }
        }
    }

    public class ProgressToWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 2 || values[0] == DependencyProperty.UnsetValue || values[1] == DependencyProperty.UnsetValue)
                return 0.0;

            double value = (double)values[0];
            double width = (double)values[1];
            
            return (value / 100.0) * width;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
