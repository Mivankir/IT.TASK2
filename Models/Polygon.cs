using System;
using System.Collections.Generic;
using System.Linq;

namespace GeometricShapesApp.Models
{
    public class Polygon : Shape
    {
        // Вершины хранятся в порядке обхода (по или против часовой стрелки)
        private readonly List<Vertex> _vertices = new List<Vertex>();

        // Наружу выдаем только read-only интерфейс для безопасности
        public IReadOnlyList<Vertex> Vertices => _vertices.AsReadOnly();
        public int VertexCount => _vertices.Count;

        // Базовый конструктор принимает опорную точку (x,y) и список вершин относительно этой точки
        public Polygon(double x, double y, List<(double X, double Y)> vertices) : base(x, y)
        {
            _vertices = vertices?.Select(v => new Vertex(v.X, v.Y)).ToList()
                       ?? new List<Vertex>();
            Name = "Многоугольник";
        }

        #region Vertex mutation API

        // Все методы возвращают bool для явного контроля успеха операции
        public bool AddVertex(Vertex vertex)
        {
            if (vertex == null) return false;
            _vertices.Add(vertex);
            return true;
        }

        // Защита от IndexOutOfRange без выбрасывания исключения
        public bool RemoveVertexAt(int index)
        {
            if (index < 0 || index >= _vertices.Count) return false;
            _vertices.RemoveAt(index);
            return true;
        }

        // Комбинированная проверка индекса и валидности вершины
        public bool UpdateVertex(int index, Vertex vertex)
        {
            if (index < 0 || index >= _vertices.Count || vertex == null)
                return false;
            _vertices[index] = vertex;
            return true;
        }

        // Полная замена вершин с null-check
        public void ReplaceAllVertices(IEnumerable<Vertex> newVertices)
        {
            _vertices.Clear();
            _vertices.AddRange(newVertices ?? throw new ArgumentNullException(nameof(newVertices)));
        }

        #endregion

        // Вычисляет AABB для отрисовки и коллизий
        public override (double x1, double y1, double x2, double y2) GetBoundingBox()
        {
            if (!_vertices.Any()) return (X, Y, X, Y);

            double minX = _vertices.Min(v => v.X);
            double minY = _vertices.Min(v => v.Y);
            double maxX = _vertices.Max(v => v.X);
            double maxY = _vertices.Max(v => v.Y);

            return (minX, minY, maxX, maxY);
        }

        // Формула шнурования для площади произвольного N-угольника
        public override double GetArea()
        {
            if (_vertices.Count < 3) return 0;

            double sum = 0;
            for (int i = 0; i < _vertices.Count; i++)
            {
                var next = (i + 1) % _vertices.Count;
                sum += _vertices[i].X * _vertices[next].Y
                     - _vertices[next].X * _vertices[i].Y;
            }

            return Math.Abs(sum) * 0.5;
        }
    }
}