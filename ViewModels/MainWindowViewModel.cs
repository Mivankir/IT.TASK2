using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GeometricShapesApp.Models;
using GeometricShapesApp.Views;

namespace GeometricShapesApp.ViewModels
{
    // Основная ViewModel для главного окна приложения
    public partial class MainWindowViewModel : ObservableObject
    {
        // Автоматически генерируемое ObservableProperty для коллекции фигур
        // При изменении коллекции будет уведомлять UI
        [ObservableProperty]
        private ObservableCollection<Shape> _shapes;

        // Конструктор ViewModel
        public MainWindowViewModel()
        {
            // Инициализация коллекции фигур с некоторыми начальными значениями
            Shapes = new ObservableCollection<Shape>
            {
                new Point(100, 100) { Name = "Точка" },
                new Line(150, 150, 300, 300) { Name = "Линия" },
                new Ellipse(400, 200, 50, 50) { Name = "Эллипс" },
                new Polygon(0, 0, new List<(double X, double Y)>
                {
                    (200, 200),
                    (250, 250),
                    (300, 200),
                    (250, 150)
                }) { Name = "Многоугольник" }
            };

            // Подписка на событие изменения коллекции
            Shapes.CollectionChanged += Shapes_CollectionChanged;
        }

        // Обработчик события изменения коллекции фигур
        private void Shapes_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Уведомление UI об изменении коллекции
            OnPropertyChanged(nameof(Shapes));
        }

        // Команда для добавления точки (генерируется RelayCommand)
        [RelayCommand]
        private void AddPoint()
        {
            var window = new AddPointWindow();
            window.DataContext = new AddPointViewModel(this, window);
            window.Show();
        }

        // Команда для добавления линии
        [RelayCommand]
        private void AddLine()
        {
            var window = new AddLineWindow();
            window.DataContext = new AddLineViewModel(this, window);
            window.Show();
        }

        // Команда для добавления эллипса
        [RelayCommand]
        private void AddEllipse()
        {
            var window = new AddEllipseWindow();
            window.DataContext = new AddEllipseViewModel(this, window);
            window.Show();
        }

        // Команда для добавления многоугольника
        [RelayCommand]
        private void AddPolygon()
        {
            var window = new AddPolygonWindow();
            window.DataContext = new AddPolygonViewModel(this, window);
            window.Show();
        }

        // Метод для добавления новой фигуры в коллекцию
        public void AddShape(Shape shape)
        {
            Shapes.Add(shape);
        }
    }
}