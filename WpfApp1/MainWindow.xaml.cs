using System;
using System.Reactive.Linq;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Random _random = new Random();
        private double _threshold = 5; // Пороговая температура для уведомления

        public MainWindow()
        {
            InitializeComponent();

            // Генерация рандомных температур каждые 5 секунд
            var temperatureObservable = Observable.Interval(TimeSpan.FromSeconds(5))
                .Select(_ => _random.Next(-10, 30));

            // Подписка на изменение температуры
            temperatureObservable.Subscribe(temperature =>
            {
                // Обновление отображаемой температуры
                Dispatcher.Invoke(() => TemperatureLabel.Content = $"{temperature}°C");

                // Проверка на приближение к критической температуре и отправка уведомления
                if (temperature <= _threshold)
                {
                    Dispatcher.Invoke(() => ShowCriticalTemperatureNotification());
                }
            });
        }

        // Метод для отображения уведомления о критической температуре
        private void ShowCriticalTemperatureNotification()
        {
            MessageBox.Show("Температура приближается к критической!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
