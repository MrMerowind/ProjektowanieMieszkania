using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace ProjektowanieMieszkaniaCSharp.Klasy
{
    class Room
    {
        public List<PointF> corners;
        public string roomName = "POKOJ";
        private Color roomColor; // Moze do kolorowania pokojow na rozne kolory

        public Room(){}

        public Room(string name)
        {
            corners = new List<PointF>();
            roomName = name;
        }

        public Room(string name, Color c)
        {
            corners = new List<PointF>();
            roomName = name;
            roomColor = c;
        }

        public Room(List<PointF> allCorners)
        {
            corners = allCorners;
        }

        public Room(List<PointF> allCorners, string name)
        {
            corners = allCorners;
            roomName = name;
        }


        // GETy

        public string getRoomName()
        {
            return roomName;
        }

        public Color getRoomColor()
        {
            return roomColor;
        }

        public override string ToString()
        {
            return roomName;
        }

        public List<PointF> getCorners() => corners;

        public void printCorners()
        {
            Console.WriteLine("New room corners:");
            for (int i = 0; i < corners.Count(); i++)
            {
                Console.Write("{" + corners[i].X + ", " + corners[i].Y + "}, ");
            }
            Console.WriteLine("\n-----------------");
        }

        public void setRoomName(string newName)
        {
            roomName = newName;
        }

        public void setRoomColor(Color newColor)
        {
            roomColor = newColor;
        }
    }
}
