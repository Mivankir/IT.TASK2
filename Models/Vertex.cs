namespace GeometricShapesApp.Models
{
    // Неизменяемая точка с координатами X/Y для геометрических фигур
    public class Vertex
    {
        // Координаты только для чтения - гарантия неизменяемости
        public double X { get; }
        public double Y { get; }

        // Создание вершины с заданными координатами
        public Vertex(double x, double y)
        {
            X = x;
            Y = y;
        }

        // Строковое представление в формате "(X, Y)" 
        public override string ToString() => $"({X}, {Y})";
    }
}