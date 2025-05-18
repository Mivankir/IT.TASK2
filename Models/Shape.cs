namespace GeometricShapesApp.Models
{
    // јбстрактный базовый класс дл€ всех геометрических фигур
    public abstract class Shape
    {
        //  оордината X фигуры на плоскости
        public double X { get; set; }

        //  оордината Y фигуры на плоскости
        public double Y { get; set; }

        // Ќазвание фигуры (дл€ отображени€ в интерфейсе)
        public string Name { get; set; }

        // Ѕазовый конструктор дл€ инициализации фигуры
        protected Shape(double x, double y)
        {
            X = x;
            Y = y;
            Name = string.Empty; // »нициализаци€ пустым именем
        }

        // јбстрактный метод дл€ получени€ ограничивающего пр€моугольника фигуры
        public abstract (double x1, double y1, double x2, double y2) GetBoundingBox();

        // јбстрактный метод дл€ вычислени€ площади фигуры
        public abstract double GetArea();
    }
}