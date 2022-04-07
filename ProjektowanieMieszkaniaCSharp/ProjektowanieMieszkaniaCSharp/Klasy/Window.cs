using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ProjektowanieMieszkaniaCSharp.Klasy;

namespace ProjektowanieMieszkaniaCSharp
{
    class Window
    {
        private PointF start;
        private PointF end;
        private float thickness;
        private Color color = Color.White;

        public Window()
        {
            start = new PointF();
            end = new PointF();
            thickness = new float();
        }

        public Window(Window e)
        {
            start = e.start;
            end = e.end;
            thickness = e.thickness;
        }

        public Window(PointF _start, PointF _end, float _thickness)
        {
            start = _start;
            end = _end;
            thickness = _thickness;
        }

        public Window(float _startX, float _startY, float _endX, float _endY, float _thickness)
        {
            start.X = _startX;
            start.Y = _startY;

            end.X = _endX;
            end.Y = _endY;

            thickness = _thickness;
        }

        public void setStart(PointF position)
        {
            start = position;
        }

        public void setEnd(PointF position)
        {
            end = position;
        }

        public Color getColor()
        {
            return color;
        }

        public void repairPoints()
        {
            // Zamienia punkty ze soba tak ze startowy
            // jest na lewo i w gore od koncowego
            if (isHorizontal())
            {
                if (start.X > end.X)
                {
                    PointF tmpPointF = start;
                    start = end;
                    end = tmpPointF;
                }
            }
            else if (isVertical())
            {
                if (start.Y > end.Y)
                {
                    PointF tmpPointF = start;
                    start = end;
                    end = tmpPointF;
                }
            }
        }

        public void setStartSnapToGrid(PointF position)
        {
            if (position.X % Rysunek.getGridSize() < Rysunek.getGridSize() / 2.0f)
                start.X = position.X - position.X % Rysunek.getGridSize();
            else
                start.X = position.X - position.X % Rysunek.getGridSize() + Rysunek.getGridSize();

            if (position.Y % Rysunek.getGridSize() < Rysunek.getGridSize() / 2.0f)
                start.Y = position.Y - position.Y % Rysunek.getGridSize();
            else
                start.Y = position.Y - position.Y % Rysunek.getGridSize() + Rysunek.getGridSize();
        }

        public void setEndSnapToGrid(PointF position)
        {
            if (position.X % Rysunek.getGridSize() < Rysunek.getGridSize() / 2.0f)
                end.X = position.X - position.X % Rysunek.getGridSize();
            else
                end.X = position.X - position.X % Rysunek.getGridSize() + Rysunek.getGridSize();

            if (position.Y % Rysunek.getGridSize() < Rysunek.getGridSize() / 2.0f)
                end.Y = position.Y - position.Y % Rysunek.getGridSize();
            else
                end.Y = position.Y - position.Y % Rysunek.getGridSize() + Rysunek.getGridSize();
        }

        public void snapToGrid()
        {
            setStartSnapToGrid(start);
            setEndSnapToGrid(end);
        }
        public void setThickness(float value)
        {
            if (value < 10.0f) value = 10.0f;
            else if (value > 30.0f) value = 30.0f;
            thickness = value;
        }

        public PointF getStart()
        {
            return start;
        }
        public PointF getEnd()
        {
            return end;
        }
        public bool isVertical()
        {
            if (start.X == end.X) return true;
            else return false;
        }
        public bool isHorizontal()
        {
            if (start.Y == end.Y) return true;
            else return false;
        }
        public float getThickness()
        {
            return thickness;
        }

        public PointF getPositionWithThickness()
        {
            if (isHorizontal())
            {
                PointF point = new PointF();
                point.X = start.X;
                point.Y = start.Y - getThickness() / 2.0f;
                return point;
            }
            else if (isVertical())
            {
                PointF point = new PointF();
                point.X = start.X - getThickness() / 2.0f;
                point.Y = start.Y;
                return point;
            }
            else
            {
                throw new System.InvalidOperationException("Sciana nie jest ani pozioma ani pionowa getPositionWithThickness()");
            }
        }
        public PointF getSizeWithThickness()
        {
            if (isHorizontal())
            {
                PointF point = new PointF();
                point.X = end.X - start.X;
                point.Y = end.Y - start.Y + getThickness();
                return point;
            }
            else if (isVertical())
            {
                PointF point = new PointF();
                point.X = end.X - start.X + getThickness();
                point.Y = end.Y - start.Y;
                return point;
            }
            else
            {
                throw new System.InvalidOperationException("Sciana nie jest ani pozioma ani pionowa getSizeWithThickness()");
            }
        }
        public void move(PointF distance)
        {
            start.X += distance.X;
            start.Y += distance.Y;
            end.X += distance.X;
            end.Y += distance.Y;
        }
        public float getWidth()
        {
            if (isHorizontal())
            {
                return end.X - start.X;
            }
            else if (isVertical())
            {
                return end.Y - start.Y;
            }
            else
            {
                throw new System.InvalidOperationException("Sciana nie jest ani pozioma ani pionowa getWidth()");
            }
        }
        void setWidth(float width)
        {
            if (width == 0.0f) return;
            if (isHorizontal())
            {
                end.X = start.X + width;
            }
            else if (isVertical())
            {
                end.Y = start.Y + width;
            }
        }
        Color GetColor()
        {
            return color;
        }
        void setColor(Color col)
        {
            color = Color.Aqua;
        }

        public void draw(Graphics g)
        {
            g.DrawLine(new Pen(GetColor(), thickness), start, end);
            g.DrawLine(new Pen(Color.Black, 1.0f), start, end);
        }
        public void straighten()
        {
            float width = Math.Abs(start.X - end.X);
            float height = Math.Abs(start.Y - end.Y);
            if (width > height)
            {
                end.Y = start.Y;
            }
            else
            {
                end.X = start.X;
            }

        }


    }
}
