using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
1 Разработать абстрактный класс ГеометрическаяФигура с полями ПлощадьФигуры и ПериметрФигуры
Разработать классы-наследники: Треугольник, Квадрат, Ромб, Прямоугольник, Параллелограмм, Трапеция,
Круг, Эллипс и реализовать свойства, которые однозначно
определяют объекты данных классов
Реализовать интерфейс ПростойНУгольник, который
имеет свойства: Высота, Основание, УголМеждуОснованиемИСмежнойСтороной, КоличествоСторон, ДлиныСторон, Площадь, Периметр
Реализовать класс СоставнаяФигура который может
состоять из любого количества ПростыхНУгольников
Для данного класса определить метод нахождения площади фигуры
Предусмотреть варианты невозможности задания
фигуры (введены отрицательные длины сторон или при
создании объекта треугольника существует пара сторон,
сумма длин которых меньше длины третьей стороны и т п ) 
*/


namespace GeometricFigure2
{
    // власний Exception
    [Serializable]
    public class NoRightLenghtException : Exception
    {

        public NoRightLenghtException() { }
        public NoRightLenghtException(string message) : base(message) { }
        public NoRightLenghtException(string message, Exception inner) : base(message, inner) { }
        protected NoRightLenghtException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    // интерфейс ПростойМногоугольник
    interface ISimplyN_Angle
    {
        double GetHeight();
        double GetBasis();
        double GetAngle();
        double GetSidesQuantity();
        void SetSidesQuantity(int value);
        double GetSidesLength();
        void SetSidesLength(double value);

        double GetArea();
        double GetPerimeter();
    }

    // интерфейс Составная фигура
    interface ICompoundFiguresNAngle
    {
        double GetArea();
    }
    

    // реализация интерфейса
    class N_Angle : ISimplyN_Angle
    {
        private readonly double height;
        private readonly double basis;
        private readonly double angle;
        private int sidesQuantity;
        private double sidesLength;


        public N_Angle(int quantity, double length)
        {
            sidesQuantity = quantity;
            sidesLength = length;
            basis = length;
            height = Math.Round(length / (2 * Math.Tan(Math.PI / quantity)), 2);
            angle = 180 * (quantity - 2) / quantity;
        }

        public double GetHeight()
        {
            return height;
        }

        public double GetBasis()
        {
            return basis;
        }

        public double GetAngle()
        {
            return angle;
        }


        public double GetSidesQuantity()
        {
            return sidesQuantity;
        }

        public void SetSidesQuantity(int value)
        {
            sidesQuantity = value;
        }

        
        public double GetSidesLength()
        {
            return sidesLength;
        }

        public void SetSidesLength(double value)
        {
            sidesLength = value;
        }

        
        public double GetArea()
        {
            return Math.Round((sidesLength * sidesLength * sidesQuantity) / (4 * Math.Tan(Math.PI / sidesQuantity)),2);
        }

        public double GetPerimeter()
        {
            return sidesQuantity * sidesLength;
        }

        public override string ToString()
        {
            return $"Правильный многоугольник {this.GetType().Name.ToString()}:\n" +
                   $"количество сторон: {sidesQuantity} штук\n" +
                   $"длина стороны: {sidesLength} cм\n" +
                   $"основание: {sidesLength} cм\n" +
                   $"угол между сторонами: {angle} градуса\n" +
                   $"площадь: {GetArea()} cм2\n" +
                   $"периметр: {GetPerimeter()} cм\n";
        }
    }


    // абстактный клас Фигура
    abstract class Figure
    {
        public abstract double Perimetr();
        public abstract double Area();

    }


    class Rectangle : Figure
    {
        private double a, b;

        public Rectangle(double a, double b)
        {
            try
            {
                this.a = a;
                this.b = b;

                if (a < 0 || b < 0) { throw new NoRightLenghtException($"ОШИБКА: Введена отрицательная длина одной из сторон объекта прямоугольника!"); }
                else if (a == 0 || b == 0) { throw new NoRightLenghtException($"ОШИБКА: Введена нулевая длина одной из сторон объекта прямоугольника!"); }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message}");

            }
        }

        public override double Area()
        {
            return a * b;
        }

        public override double Perimetr()
        {
            return 2 * a + 2 * b;
        }

        public override string ToString()
        {
            return $" Прямоугольник: a={a}cm, b={b}cм\n Периметр:{Perimetr()}cm\n Площадь:{Area()}cм2\n";
        }
    }
    class Ellipse : Figure
    {
        private double a, b;

        public Ellipse(double a, double b)
        {
            try
            {
                this.a = a;
                this.b = b;

                if (a < 0 || b < 0) { throw new NoRightLenghtException($"ОШИБКА:  Введена отрицательная длина одной из полуосей объекта эллипса !"); }
                else if (a == 0 || b == 0) { throw new NoRightLenghtException($"ОШИБКА:  Введена нулевая длина одной из полуосей объекта эллипса !"); }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message}");

            }

        }
        public override double Area()
        {
            return Math.Round(Math.PI * a * b, 2);
        }

        public override double Perimetr()
        {
            return Math.Round(2 * Math.PI * Math.Sqrt(0.5 * (a * a + b * b)), 2);
        }

        public override string ToString()
        {
            return $" Елипс: малая полуось эллипса a={a}cм, большая полуось эллипса b={b}cm \n Длина окружности:{Perimetr()}cm\n Площадь:{Area()}cм2\n";
        }
    }
    class Parallelogram : Figure
    {
        private double a, b, h;

        public Parallelogram(double a, double b, double h)
        {

            try
            {
                this.a = a;
                this.b = b;
                this.h = h;
                if (a < 0 || b < 0 ) { throw new NoRightLenghtException($"ОШИБКА: Введена отрицательная длина одной из сторон объекта параллелограма !"); }
                else if (a == 0 || b == 0) { throw new NoRightLenghtException($"ОШИБКА: Введена нулевая длина одной из сторон объекта параллелограма !"); }
                else if (h == 0) { throw new NoRightLenghtException($"ОШИБКА: Введена нулевая длина высоты объекта параллелограма !"); }
                else if (h < 0) { throw new NoRightLenghtException($"ОШИБКА: Введена отрицательная длина высоты объекта параллелограма !"); }
            }

            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message}");

            }
        }

        public override double Area()
        {
            return a * h;
        }

        public override double Perimetr()
        {
            return 2 * a + 2 * b;
        }

        public override string ToString()
        {
            return $" Паралелограм: a={a}cм, b={b}cм, h={h}cм\n Периметр:{Perimetr()}cм\n Площадь:{Area()}cм2\n";
        }
    }

    class Rhomb : Figure
    {
        private double a, h;

        public Rhomb(double a, double h)
        {
            try
            {
                this.a = a;
                this.h = h;

                if (a < 0 || h < 0) { throw new NoRightLenghtException($"ОШИБКА: Введена отрицательная длина стороны или высоты объекта ромба !"); }
                else if (a == 0 || h == 0) { throw new NoRightLenghtException($"ОШИБКА: Введена нулевая длина стороны или высоты объекта ромба  !"); }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message}");
            }
        }

        public override double Area()
        {
            return a * h;
        }

        public override double Perimetr()
        {
            return 4 * a;
        }

        public override string ToString()
        {
            return $" Ромб: a={a}cм, h={h}cм\n Периметр:{Perimetr()}cм\n Площадь:{Area()}cм2\n";
        }
    }

    class Square : Figure
    {
        private double a;

        public Square(double a)
        {
           
            try
            {
                this.a = a;
                if (a < 0 ) { throw new NoRightLenghtException($"ОШИБКА: Введена отрицательная длина стороны объекта квадрата !"); }
                else if (a == 0 ) { throw new NoRightLenghtException($"ОШИБКА: Введена нулевая длина стороны  объекта квадрата !"); }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message}");
            }



        }

        public override double Area()
        {
            return a * a;
        }

        public override double Perimetr()
        {
            return 4 * a;
        }

        public override string ToString()
        {
            return $" Квадарат: a={a}cm\n Периметр:{Perimetr()}cм\n площадь:{Area()}cм2\n";
        }
    }

    class Trapeze : Figure
    {
        private double a, b, h;

        public Trapeze(double a, double b, double h)
        {
            try
            {
                this.a = a;
                this.b = b;
                this.h = h;
                if (a < 0 || b < 0) { throw new NoRightLenghtException($"ОШИБКА: Введена отрицательная длина одной из сторон объекта трапеции !"); }
                else if (a == 0 || b == 0) { throw new NoRightLenghtException($"ОШИБКА: Введена нулевая длина одной из сторон объекта трапеции !"); }
                else if (h == 0) { throw new NoRightLenghtException($"ОШИБКА: Введена нулевая длина высоты объекта трапеции!"); }
                else if (h < 0) { throw new NoRightLenghtException($"ОШИБКА: Введена отрицательная длина высоты объекта трапеции !"); }
            }

            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message}");

            }





        }

        public override double Area()
        {
            return Math.Round(0.5 * (a + b) * h, 2);
        }

        public override double Perimetr()
        {
            return 2 * a + 2 * b;
        }

        public override string ToString()
        {
            return $" Трапеция: a={a}cм, b={b}cм, h={h}cм\n Периметр:{Perimetr()}cм\n Площадь:{Area()}cм2\n";
        }
    }
    class Triangle : Figure
    {
        private double a, b, c;

        public Triangle(double a, double b, double c)
        {
            
            try
            {
                this.a = a;
                this.b = b;
                this.c = c;
                if (a < 0 || b < 0 || c < 0) { throw new NoRightLenghtException($"ОШИБКА: Введена отрицательная длина одной из сторон объекта треугольника!"); }
                else if (a == 0 || b == 0 || c == 0) { throw new NoRightLenghtException($"ОШИБКА: Введена нулевая длина одной из сторон объекта треугольника !"); }
                else if ((a+b) <= c  || (c + b) <= a || (a + c) <= b) { throw new NoRightLenghtException($"ОШИБКА: Сумма двух сторон треугольника меньше третьей!"); }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{ e.Message}");
            }
        }

        public override double Area()
        {
            double p = (a + b + c) / 2;
            return Math.Round(Math.Sqrt(p * (p - a) * (p - b) * (p - c)), 2);
        }
        public override double Perimetr()
        {
            return a + b + c;
        }

        public override string ToString()
        {
            return $" Треугольник: a={a}cм, b={b}cм, c={c}cм\n Периметр:{Perimetr()}cм\n Площадь:{Area()}cм2\n";
        }
    }

    class Сircle : Figure
    {
        private double r;

        public Сircle(double r)
        {
            this.r = r;

        }

        public override double Area()
        {
            return Math.Round(Math.PI * r * r, 2);
        }

        public override double Perimetr()
        {
            return Math.Round(2 * Math.PI * r, 2);
        }

        public override string ToString()
        {
            return $"Круг: радиус r={r}cм\n Длина круга:{Perimetr()}cм\nПлощадь:{Area()}cм2\n";
        }
    }

    class CompoundFigures : List<ISimplyN_Angle>, ICompoundFiguresNAngle
    {

        public CompoundFigures(IEnumerable<ISimplyN_Angle> collection) : base(collection) { }

        public CompoundFigures() { }

        public new void Add(ISimplyN_Angle figure)
        {
            this.Add(figure);
        }

        public double GetArea()
        {
            double Sum = 0;
            foreach (var figure in this)
            {
                Sum += figure.GetArea();
            }
            return Sum;

        }
  
        public override string ToString()
        {
            return $"Количество многоугольников: {this.Count()} штук\n" +
                   $"Площадь всех многоугольников: {GetArea()} cм2 \n";
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            
            List<Figure> Figures = new List<Figure> { new Triangle(10, 10, 20),    //трикутник
                                                      new Square(5),               //квадрат
                                                      new Rectangle(0, 12),        //прямокутник
                                                      new Rhomb(10, 5),            //ромб
                                                      new Parallelogram(10, 5, 4), //паралелограм
                                                      new Trapeze(10, 5, 4),       //трапеція
                                                      new Сircle(17),              //круг
                                                      new Ellipse(10,7)};          //еліпс
            foreach (var figure in Figures)
            {
                Console.WriteLine(figure);
            }
            Console.WriteLine("Нажмите Enter для продолжения!");
            Console.ReadLine();
  
            ICompoundFiguresNAngle CFigures = new CompoundFigures(new ISimplyN_Angle[] {new N_Angle(10, 5),
                                                new N_Angle(25, 10)});
            Console.WriteLine(CFigures);

            foreach (var figure in ((List<ISimplyN_Angle>)CFigures))
            {
                Console.WriteLine(figure);
            }

        }
    }
}
