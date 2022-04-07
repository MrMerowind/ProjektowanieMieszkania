using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;

namespace ProjektowanieMieszkaniaCSharp.Klasy
{
    class Rysunek
    {
        private int height;
        private int width;

        public bool isFurniturePlaced = true;
        public bool furnitureSizeTextTopLeft = true;

        public List<Wall> wallList;             // To by sie chyba przydalo na prywatki zmienic co?
        public List<Window> windowList;         // nie umiem
        public List<Door> doorList;
        public List<Room> roomList;
        public List<Furniture> availableFurnitureList;
        public List<Furniture> furnitureListToDraw;
        public List<Image> wrongPossitionFurnitureImages;

        private Color backgroundColor;
        private static int gridSize;
        public Rysunek(int szerokosc, int wysokosc, Color kolorTla)
        {
            this.width = szerokosc;
            this.height = wysokosc;
            this.backgroundColor = kolorTla;
            this.wallList = new List<Wall>();
            this.windowList = new List<Window>();
            this.doorList = new List<Door>();
            this.roomList = new List<Room>();
            this.availableFurnitureList = new List<Furniture>();
            this.furnitureListToDraw = new List<Furniture>();
            this.wrongPossitionFurnitureImages = new List<Image>();
            gridSize = 10;
        }

        // FURNITURE START

        public void furnitureListPush(Furniture newItem)
        {
            availableFurnitureList.Add(newItem);
        }

        public void availableFurnitureListClear()
        {
            availableFurnitureList.Clear();
        }

        public void furnitureListToDrawClear()
        {
            furnitureListToDraw.Clear();
        }

        public void lastFurnitureRotate(int angle)
        {

        }

        // FURNITURE END

        // SETy
        public void setWidth(int value)
        {
            width = value;
        }

        public void setHeight(int value)
        {
            height = value;
        }

        // GETy

        public List<Wall> getWallList() => wallList;
        public List<Room> getRoomList() => roomList;
        public List<Window> getWindowList() => windowList;
        public List<Door> getDoorList() => doorList;
        public List<Furniture> getFurnitureList() => availableFurnitureList;
        public List<Furniture> getFurnitureListToDraw() => furnitureListToDraw;
        public List<Image> getWrongPossitionFurnitureImages() => wrongPossitionFurnitureImages;

        public static int getGridSize()
        {
            return gridSize;
        }

        public int getWidth()
        { return width; }
        public int getHeight()
        { return height; }

        // RESZTA
        public void flushWallList()
        {
            wallList.Clear();
        }

        public void Rysuj(Graphics g)
        {
            g.Clear(backgroundColor);

            PointF lineStart = new PointF();
            PointF lineEnd = new PointF();
            Color lineColor = new Color();

            // pionowe linie siatki
            lineStart.Y = 0;
            lineEnd.Y = height;
            lineColor = Color.FromArgb(41, 104, 96);

            for (int i = 0; i < width; i += gridSize)
            {
                lineStart.X = i;
                lineEnd.X = i;
                g.DrawLine(new Pen(lineColor, 1.0f), lineStart, lineEnd);
            }

            // poziome linie siatki
            lineStart.X = 0;
            lineEnd.X = width;
            lineColor = Color.FromArgb(41, 104, 96);

            for (int i = 0; i < height; i += gridSize)
            {
                lineStart.Y = i;
                lineEnd.Y = i;
                g.DrawLine(new Pen(lineColor, 1.0f), lineStart, lineEnd);
            }
            foreach (Wall f in wallList)
            {
                f.draw(g);
            }

            foreach (Window f in windowList)
            {
                f.draw(g);
            }

            foreach (Door f in doorList)
            {
                f.draw(g);
            }

            int licznikForFurniture = 0;
            foreach (Furniture f in furnitureListToDraw)
            {
                
                if(f.isWrongPosition())
                {
                    Bitmap tmpImg = (Bitmap)wrongPossitionFurnitureImages[licznikForFurniture];

                    Image img = (Image)tmpImg;

                    f.loadSize();
                    g.DrawImage(img, f.getLocation().X, f.getLocation().Y, f.getWidth(), f.getHeight());
                }
                else
                {
                    Bitmap tmpImg = (Bitmap)Bitmap.FromFile(f.getImgSource());

                    // Na samym dole jest resize
                    // tmpImg.SetResolution(g.DpiX + f.getImageScaleWidth(), g.DpiY + f.getImageScaleHeight());
                    tmpImg.SetResolution(g.DpiX, g.DpiY);
                    Image img = (Image)tmpImg;
                    
                        switch (f.getHowManyRotations())
                        {
                            case 0:
                                break;
                            case 1:
                                img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case 2:
                                img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case 3:
                                img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }

                        if (f.isMirrorTransformated())
                        {
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }

                    f.loadSize();
                    g.DrawImage(img, f.getLocation().X, f.getLocation().Y, f.getWidth(), f.getHeight());
                }
                
                
                licznikForFurniture++;
            }

            if (!isFurniturePlaced)
            {
                // Create string to draw.
                String drawString = "w: " + furnitureListToDraw.Last().getWidth().ToString() + " h: " + furnitureListToDraw.Last().getHeight().ToString();

                // Create font and brush.
                Font drawFont = new Font("Arial", 16);
                SolidBrush drawBrush = new SolidBrush(Color.White);

                if (true)
                {
                    // Create point for upper-left corner of drawing.
                    float x = 0.0F;
                    float y = 0.0F;

                    // Set format of string.
                    StringFormat drawFormat = new StringFormat();

                    // Draw string to screen.
                    g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                }
                else
                {
                    /*
                    // Create point for upper-left corner of drawing.
                    float x = width;
                    float y = height-20;

                    // Set format of string.
                    StringFormat drawFormat = new StringFormat();
                    drawFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;

                    // Draw string to screen.
                    g.DrawString(drawString, drawFont, drawBrush, x, y, drawFormat);
                    */
                }
            }
        }

        public void DrawWithoutBackground(Graphics g)
        {
            g.Clear(Color.FromArgb(255, 0,0,0));
            

            PointF lineStart = new PointF();
            PointF lineEnd = new PointF();
            Color lineColor = new Color();

            // pionowe linie siatki
            

            // poziome linie siatki
            
            foreach (Wall f in wallList)
            {
                f.draw(g);
            }

            foreach (Window f in windowList)
            {
                f.draw(g);
            }

            foreach (Door f in doorList)
            {
                f.draw(g);
            }

            int licznikForFurniture = 0;
            foreach (Furniture f in furnitureListToDraw)
            {

                if (f.isWrongPosition())
                {
                    
                    Bitmap tmpImg = (Bitmap)wrongPossitionFurnitureImages[licznikForFurniture];

                    Image img = (Image)tmpImg;

                    f.loadSize();
                    g.DrawImage(img, f.getLocation().X, f.getLocation().Y, f.getWidth(), f.getHeight());
                }
                else
                {
                    Bitmap tmpImg = (Bitmap)Bitmap.FromFile(f.getImgSource());

                    // Na samym dole jest resize
                    // tmpImg.SetResolution(g.DpiX + f.getImageScaleWidth(), g.DpiY + f.getImageScaleHeight());
                    tmpImg.SetResolution(g.DpiX, g.DpiY);
                    Image img = (Image)tmpImg;

                    switch (f.getHowManyRotations())
                    {
                        case 0:
                            break;
                        case 1:
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            break;
                        case 2:
                            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            break;
                        case 3:
                            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            break;
                    }

                    if (f.isMirrorTransformated())
                    {
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }

                    f.loadSize();
                    g.DrawImage(img, f.getLocation().X, f.getLocation().Y, f.getWidth(), f.getHeight());
                }
                
                licznikForFurniture++;
            }
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        internal void Dodaj(Wall f)
        {
            wallList.Add(f);
        }

        internal void Dodaj(Door f)
        {
            doorList.Add(f);
        }

        internal void Dodaj(Window f)
        {
            windowList.Add(f);
        }

        internal void Dodaj(Furniture f)
        {
            furnitureListToDraw.Add(f);

            Bitmap tmpImg = (Bitmap)Bitmap.FromFile(f.getImgSource());

            
                for (int i = 0; i < tmpImg.Width; i++)
                {
                    for (int j = 0; j < tmpImg.Height; j++)
                    {
                        Color tmpColor = tmpImg.GetPixel(i, j);
                        tmpColor = Color.FromArgb(tmpColor.A, 255, 0, 0);
                        tmpImg.SetPixel(i, j, tmpColor);
                    }
                }


            Image img = (Image)tmpImg;

            switch (f.getHowManyRotations())
            {
                case 0:
                    break;
                case 1:
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    break;
                case 2:
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    break;
                case 3:
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    break;
            }

            if (f.isMirrorTransformated())
            {
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }

            wrongPossitionFurnitureImages.Add(img);
        }

        internal void DodajMebelDoBiblioteki(Furniture f)
        {
            availableFurnitureList.Add(f);
        }

        public void colorMapOld(int[,] map, int mapWidth, int mapHeight, int positionX, int positionY, int color)
        {
            /// Sprawdzanie czy wyszlismy poza mape
            if (positionX < 0 || positionX >= mapWidth) return;
            if (positionY < 0 || positionY >= mapHeight) return;

            /// Sprawdzanie czy pokolorowany jest dany punkt
            if(map[positionX, positionY] == -2)
            {
                /// Kolorowanie
                map[positionX, positionY] = color;
                colorMap(map, mapWidth, mapHeight, positionX - 1, positionY, color);
                colorMap(map, mapWidth, mapHeight, positionX + 1, positionY, color);
                colorMap(map, mapWidth, mapHeight, positionX, positionY - 1, color);
                colorMap(map, mapWidth, mapHeight, positionX, positionY + 1, color);
            }
        }

        public void colorMap(int[,] map, int mapWidth, int mapHeight, int positionX, int positionY, int color)
        {

            Queue<Point> pointQueue = new Queue<Point>();
            pointQueue.Enqueue(new Point(positionX, positionY));

            while(pointQueue.Count > 0)
            {
                Point point = pointQueue.Peek();
                pointQueue.Dequeue();

                /// Sprawdzanie czy wyszlismy poza mape
                if (point.X < 0 || point.X >= mapWidth) continue;
                if (point.Y < 0 || point.Y >= mapHeight) continue;

                /// Sprawdzanie czy pokolorowany jest dany punkt
                if (map[point.X, point.Y] == -2)
                {
                    /// Kolorowanie
                    map[point.X, point.Y] = color;
                    pointQueue.Enqueue(new Point(point.X - 1, point.Y));
                    pointQueue.Enqueue(new Point(point.X + 1, point.Y));
                    pointQueue.Enqueue(new Point(point.X, point.Y - 1));
                    pointQueue.Enqueue(new Point(point.X, point.Y + 1));
                }
            }
        }


        public void searchForRooms()
        {
            /// Tworzenie mapy
            int mapWidth = width / gridSize + 2;
            int mapHeight = height / gridSize + 2;
            int[,] map = new int[mapWidth, mapHeight];

            /// Zerowanie tablicy
            for (int i = 0; i < mapWidth; i++)
            {
                for (int j = 0; j < mapHeight; j++)
                {
                    map[i, j] = -2;
                }
            }
        

            /// Wstawianie scian
            for(int i = 0; i < wallList.Count; i++)
            {
                if(wallList[i].isHorizontal())
                {
                    int iterator = (int) (wallList[i].getStart().X / gridSize);
                    int iteratorEnd = (int)(wallList[i].getEnd().X / gridSize);
                    int y = (int)(wallList[i].getStart().Y / gridSize);
                    
                    for (; iterator <= iteratorEnd; iterator++)
                    {
                        if (iterator >= 0 && iterator < mapWidth && y >= 0 && y < mapHeight)
                            map[iterator, y] = -1;
                    }

                }
                else if(wallList[i].isVertical())
                {
                    int iterator = (int)(wallList[i].getStart().Y / gridSize);
                    int iteratorEnd = (int)(wallList[i].getEnd().Y / gridSize);
                    int x = (int)(wallList[i].getStart().X / gridSize);

                    for (; iterator <= iteratorEnd; iterator++)
                    {
                        if (iterator >= 0 && iterator < mapHeight && x >= 0 && x < mapWidth)
                            map[x, iterator] = -1;
                    }
                }
            }

            /// Pierwsze kolorowanie
            colorMap(map, mapWidth, mapHeight, mapWidth - 1, mapHeight - 1, 0);

            /// Kolejne kolorowania
            int colorValue = 1;
            for(int j = 0; j < mapHeight; j++)
            {
                for (int i = 0; i < mapWidth; i++)
                {
                    if (map[i, j] == -2)
                    {
                        colorMap(map, mapWidth, mapHeight, i, j, colorValue);
                        colorValue++;
                    }
                }
            }


            /// Kopia pokoi
            List<Room> roomListCopy = new List<Room>(roomList);

            /// Szukanie pokoi
            roomList.Clear();
            for(int i = 1; i < colorValue; i++)
            {
                List<PointF> corners = new List<PointF>();
                PointF cornerPoint = new PointF();
                corners.Clear();

                /// Szukanie poczatku pokoju
                int roomPositionX = 0;
                int roomPositionY = 0;

                bool positionFound = false;

                for(roomPositionY = 0; roomPositionY < mapHeight; roomPositionY++)
                {
                    for(roomPositionX = 0; roomPositionX < mapWidth; roomPositionX++)
                    {
                        if(map[roomPositionX,roomPositionY] == i)
                        {
                            positionFound = true;
                            break;
                        }
                    }
                    if (positionFound) break;
                }

                /// Dodawanie pierwszego wierzcholka
                int roomPositionStartX = roomPositionX;
                int roomPositionStartY = roomPositionY;
                cornerPoint.X = (roomPositionX - 1) * gridSize;
                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                corners.Add(cornerPoint);

                int direction = 1;
                /// 0 - gora
                /// 1 - prawo
                /// 2 - dol
                /// 3 - lewo

                /// Pokoj 1x1
                bool roomIs1x1 = true;
                if (roomPositionX - 1 >= 0 && roomPositionX - 1 < mapWidth && roomPositionY >= 0 && roomPositionY < mapHeight)
                {
                    if (map[roomPositionX - 1, roomPositionY] == i)
                    {
                        roomIs1x1 = false;
                    }
                }
                if (roomPositionX + 1 >= 0 && roomPositionX + 1 < mapWidth && roomPositionY >= 0 && roomPositionY < mapHeight)
                {
                    if (map[roomPositionX + 1, roomPositionY] == i)
                    {
                        roomIs1x1 = false;
                    }
                }
                if (roomPositionX >= 0 && roomPositionX < mapWidth && roomPositionY - 1 >= 0 && roomPositionY - 1 < mapHeight)
                {
                    if (map[roomPositionX, roomPositionY - 1] == i)
                    {
                        roomIs1x1 = false;
                    }
                }
                if (roomPositionX >= 0 && roomPositionX < mapWidth && roomPositionY + 1 >= 0 && roomPositionY + 1 < mapHeight)
                {
                    if (map[roomPositionX, roomPositionY + 1] == i)
                    {
                        roomIs1x1 = false;
                    }
                }
                if(roomIs1x1)
                {
                    cornerPoint.X = (roomPositionX + 1) * gridSize;
                    cornerPoint.Y = (roomPositionY - 1) * gridSize;
                    corners.Add(cornerPoint);
                    cornerPoint.X = (roomPositionX + 1) * gridSize;
                    cornerPoint.Y = (roomPositionY + 1) * gridSize;
                    corners.Add(cornerPoint);
                    cornerPoint.X = (roomPositionX - 1) * gridSize;
                    cornerPoint.Y = (roomPositionY + 1) * gridSize;
                    corners.Add(cornerPoint);
                }

                while (roomPositionX != roomPositionStartX || roomPositionY != roomPositionStartY || direction != 0)
                {
                    /// Blokada chodzenia w kolko w nieskonczonosc
                    if (corners.Count > 100) break;

                    int newRoomPositionX = new int();
                    int newRoomPositionY = new int();
                    if(direction == 0)
                    {
                        /// W lewo
                        newRoomPositionX = roomPositionX - 1;
                        newRoomPositionY = roomPositionY;
                        /// Sprawdzanie czy wyszlismy za mape
                        if(newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >=0 && newRoomPositionY < mapHeight)
                        {
                            if(map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX - 1) * gridSize;
                                cornerPoint.Y = (roomPositionY + 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 3;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W gore
                        newRoomPositionX = roomPositionX;
                        newRoomPositionY = roomPositionY - 1;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = roomPositionX * gridSize;
                                cornerPoint.Y = roomPositionY * gridSize;
                                // corners.Add(cornerPoint);
                                direction = 0;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W prawo
                        newRoomPositionX = roomPositionX + 1;
                        newRoomPositionY = roomPositionY;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX - 1) * gridSize;
                                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 1;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W dol
                        newRoomPositionX = roomPositionX;
                        newRoomPositionY = roomPositionY + 1;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX - 1) * gridSize;
                                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                                corners.Add(cornerPoint);
                                cornerPoint.X = (roomPositionX + 1) * gridSize;
                                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 2;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }

                        direction = 0;
                        continue;
                    }
                    else if(direction == 1)
                    {
                        /// W gore
                        newRoomPositionX = roomPositionX;
                        newRoomPositionY = roomPositionY - 1;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX - 1) * gridSize;
                                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 0;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W prawo
                        newRoomPositionX = roomPositionX + 1;
                        newRoomPositionY = roomPositionY;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = roomPositionX * gridSize;
                                cornerPoint.Y = roomPositionY * gridSize;
                                // corners.Add(cornerPoint);
                                direction = 1;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W dol
                        newRoomPositionX = roomPositionX;
                        newRoomPositionY = roomPositionY + 1;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX + 1) * gridSize;
                                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 2;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W lewo
                        newRoomPositionX = roomPositionX - 1;
                        newRoomPositionY = roomPositionY;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX + 1) * gridSize;
                                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                                corners.Add(cornerPoint);
                                cornerPoint.X = (roomPositionX + 1) * gridSize;
                                cornerPoint.Y = (roomPositionY + 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 3;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }

                        direction = 0;
                        continue;
                    }
                    else if(direction == 2)
                    {
                        /// W prawo
                        newRoomPositionX = roomPositionX + 1;
                        newRoomPositionY = roomPositionY;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX + 1) * gridSize;
                                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 1;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W dol
                        newRoomPositionX = roomPositionX;
                        newRoomPositionY = roomPositionY + 1;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = roomPositionX * gridSize;
                                cornerPoint.Y = roomPositionY * gridSize;
                                // corners.Add(cornerPoint);
                                direction = 2;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W lewo
                        newRoomPositionX = roomPositionX - 1;
                        newRoomPositionY = roomPositionY;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX + 1) * gridSize;
                                cornerPoint.Y = (roomPositionY + 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 3;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W gore
                        newRoomPositionX = roomPositionX;
                        newRoomPositionY = roomPositionY - 1;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX + 1) * gridSize;
                                cornerPoint.Y = (roomPositionY + 1) * gridSize;
                                corners.Add(cornerPoint);
                                cornerPoint.X = (roomPositionX - 1) * gridSize;
                                cornerPoint.Y = (roomPositionY + 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 0;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }

                        direction = 0;
                        continue;
                    }
                    else if(direction == 3)
                    {
                        /// W dol
                        newRoomPositionX = roomPositionX;
                        newRoomPositionY = roomPositionY + 1;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX + 1) * gridSize;
                                cornerPoint.Y = (roomPositionY + 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 2;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W lewo
                        newRoomPositionX = roomPositionX - 1;
                        newRoomPositionY = roomPositionY;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = roomPositionX * gridSize;
                                cornerPoint.Y = roomPositionY * gridSize;
                                // corners.Add(cornerPoint);
                                direction = 3;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W gore
                        newRoomPositionX = roomPositionX;
                        newRoomPositionY = roomPositionY - 1;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX - 1) * gridSize;
                                cornerPoint.Y = (roomPositionY + 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 0;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }
                        /// W prawo
                        newRoomPositionX = roomPositionX + 1;
                        newRoomPositionY = roomPositionY;
                        /// Sprawdzanie czy wyszlismy za mape
                        if (newRoomPositionX >= 0 && newRoomPositionX < mapWidth && newRoomPositionY >= 0 && newRoomPositionY < mapHeight)
                        {
                            if (map[newRoomPositionX, newRoomPositionY] == i)
                            {
                                cornerPoint.X = (roomPositionX - 1) * gridSize;
                                cornerPoint.Y = (roomPositionY + 1) * gridSize;
                                corners.Add(cornerPoint);
                                cornerPoint.X = (roomPositionX - 1) * gridSize;
                                cornerPoint.Y = (roomPositionY - 1) * gridSize;
                                corners.Add(cornerPoint);
                                direction = 1;
                                roomPositionX = newRoomPositionX;
                                roomPositionY = newRoomPositionY;
                                continue;
                            }
                        }

                        direction = 0;
                        continue;
                    }
                }


                Room finalRoom = new Room(corners);
                Console.Write("Nowy pokoj: ");
                for(int it = 0; it < finalRoom.corners.Count; it++)
                {
                    Console.Write("(");
                    Console.Write(finalRoom.corners[it].X);
                    Console.Write("|");
                    Console.Write(finalRoom.corners[it].Y);
                    Console.Write(")");
                    Console.WriteLine();
                }
                roomList.Add(finalRoom);
            }

            /// Nazywanie pokoi
            for(int i = 0; i < roomList.Count; i++)
            {
                for(int j = 0; j < roomListCopy.Count; j++)
                {
                    /// Porownywanie rogow
                    bool cornersMatching = true;
                    if(roomList[i].corners.Count == roomListCopy[j].corners.Count)
                    {
                        for(int k = 0; k < roomList[i].corners.Count; k++)
                        {
                            if(roomList[i].corners[k] != roomListCopy[j].corners[k])
                            {
                                cornersMatching = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        cornersMatching = false;
                    }

                    if(cornersMatching)
                    {
                        roomList[i].roomName = roomListCopy[j].roomName;
                    }
                }
            }
            


        }

        public void searchRoomsBeta()
        {
            // Nie uzywac (chyba ze dla beki) bo nie dziala
            if (roomList.Count() != 0)
            {
                roomList.Clear();
            }

            List<PointF> potentialRoom = new List<PointF>();
            List<Wall> workingList = new List<Wall>(wallList); // kopiowanie listy, a nie wskaznika

            List<int> forbiddenStartIndexes = new List<int>(); // indexy scian po ktorych nie startujemy juz szukania
            List<int> forbiddenSearchIndexes = new List<int>();// indexy scian po ktorych w ogole juz nie bedziemy szukac

            for (int i = 0; i < workingList.Count(); i++)
            {
                // Sprawdzam czy sciana nie jest zakazana do przeszukiwania
                bool decisionOfStart = true;

                for (int j = 0; j < forbiddenStartIndexes.Count(); j++)
                {
                    if (forbiddenStartIndexes[j] == i)
                    {
                        decisionOfStart = false;
                        break;
                    }
                }

                // Sprawdzam czy trzeba dodac sciane do forbiddenow
                if (decisionOfStart == true)
                {
                    potentialRoom.Clear();
                    potentialRoom.Add(workingList[i].getStart());
                    potentialRoom.Add(workingList[i].getEnd());
                    bool presentOrientationHorizontal = workingList[i].isHorizontal();
                    int operations = 0;

                    /*
                    int neighbourCount = 0;
                    for (int j = 0; j < workingList.Count(); j++)
                    {
                        if (i != j)
                        {
                            if (workingList[i].getStart() == workingList[j].getStart() || workingList[i].getEnd() == workingList[j].getEnd())
                            {
                                neighbourCount++;
                            }
                        }
                    }

                    if (neighbourCount < 2)
                    {
                        forbiddenStartIndexes.Add(i);
                    }
                    */
                    float newWallRating = 1.0f;
                    while (operations <= workingList.Count() && newWallRating != 0.0f)
                    {
                        int heldIndex = -1;
                        bool StartOrEnd = true;
                        for (int j = 0; j < workingList.Count(); j++)
                        {
                            newWallRating = 999999.99f; // lower is better
                            //bool decisionStart = false;

                            if (i != j)
                            {
                                if (presentOrientationHorizontal)
                                {
                                    if (wallList[j].isVertical())
                                    {
                                        if (potentialRoom.Last() == wallList[j].getStart())
                                        {
                                            if (newWallRating > (wallList[j].getEnd().Y - potentialRoom.First().Y))
                                            {
                                                newWallRating = (wallList[j].getEnd().Y - potentialRoom.First().Y);
                                                heldIndex = j;
                                                StartOrEnd = true;
                                            }
                                        }
                                        else if (potentialRoom.Last() == wallList[j].getEnd())
                                        {
                                            if (newWallRating > (potentialRoom.First().Y - wallList[j].getEnd().Y))
                                            {
                                                newWallRating = (potentialRoom.First().Y - wallList[j].getEnd().Y);
                                                heldIndex = j;
                                                StartOrEnd = false;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (wallList[j].isHorizontal())
                                    {
                                        if (potentialRoom.Last() == wallList[j].getStart())
                                        {
                                            if (newWallRating > (wallList[j].getEnd().X - potentialRoom.First().X))
                                            {
                                                newWallRating = (wallList[j].getEnd().X - potentialRoom.First().X);
                                                heldIndex = j;
                                                StartOrEnd = true;
                                            }
                                        }
                                        else if (potentialRoom.Last() == wallList[j].getEnd())
                                        {
                                            if (newWallRating > (potentialRoom.First().X - wallList[j].getEnd().X))
                                            {
                                                newWallRating = (potentialRoom.First().X - wallList[j].getEnd().X);
                                                heldIndex = j;
                                                StartOrEnd = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (heldIndex != -1)
                        {
                            if (StartOrEnd == true)
                            {
                                potentialRoom.Add(wallList[heldIndex].getEnd());
                            }
                            else
                            {
                                potentialRoom.Add(wallList[heldIndex].getStart());
                            }

                            forbiddenStartIndexes.Add(heldIndex);
                        }
                        operations++;
                    }

                    if (newWallRating == 0.0f)
                    {
                        Room finalRoom = new Room(potentialRoom);
                        roomList.Add(finalRoom);

                        Console.WriteLine("Room has been added.");
                        finalRoom.printCorners();
                    }
                }
            }


            /*
             * STARA WERSJA KTORA NIE DZIALA, ALE ZOSTAWIAM DO RESOURCOWANIA
             * 
            if (wallList.Count() < 3)
            {
                return;
            }

            if (roomList.Count() != 0)
            {
                roomList.Clear();
            }

            List<PointF> potentialRoom = new List<PointF>();
            List<Wall> workingList = new List<Wall>(wallList); // kopiowanie listy, a nie wskaznika

            /// TEST
            /// 
            Wall walltmp = new Wall() ;
            walltmp.setStart(new PointF(-10, -10));
            walltmp.setEnd(new PointF(-10, -20));
            workingList.Add(walltmp);

            walltmp.setStart(new PointF(-60, -60));
            walltmp.setEnd(new PointF(-60, -50));
            workingList.Add(walltmp);



            /// TEST END

            PointF startPoint = new PointF();
            PointF walker = new PointF();

            for (int i = 0; i < workingList.Count(); i++)
            {
                potentialRoom.Clear();
                potentialRoom.Add(workingList[i].getStart());

                startPoint = workingList[i].getStart();
                walker = workingList[i].getEnd();

                int loopCounter = 0;
                while ( loopCounter < workingList.Count() )
                {
                    loopCounter = 0;
                    for ( int j = 0; j < workingList.Count(); j++ )
                    {
                        loopCounter++;
                        if ( i != j )
                        {
                            if ( walker == workingList[j].getStart() )
                            {
                                potentialRoom.Add(workingList[j].getStart());

                                walker = workingList[j].getEnd();

                                workingList.Remove(workingList[j--]);

                                break;
                            }
                            else if ( walker == workingList[j].getEnd() )
                            {
                                potentialRoom.Add(workingList[j].getEnd());

                                walker = workingList[j].getStart();

                                workingList.Remove(workingList[j--]);

                                break;
                            }
                        }
                    }
                }

                if ( walker == startPoint )
                {
                    Room finalRoom = new Room(potentialRoom);
                    roomList.Add(finalRoom);

                    
                    
                    Console.WriteLine("Room has been added.");
                }
            }
            /// TEST
            // roomList.RemoveAt(0);
            /// TEST END
            Console.WriteLine("Programe has found {0} room/s", roomList.Count());
            */

        }

        public void divideWalls()
        {
            /// Nie potrzebne juz to bo zawiera sie w drugim algorytmie
            /// Sciana zaczyna sie w innej scianie
            /*
            for (int j = 0; j < wallList.Count; j++)
            {
                PointF dividePoint1 = wallList[j].getStart();
                PointF dividePoint2 = wallList[j].getEnd();

                for (int i = 0; i < wallList.Count; i++)
                {
                    if (wallList[i].isHorizontal())
                    {
                        /// Jesli punkt jest na tej samej prostej
                        if (wallList[i].getStart().Y == dividePoint1.Y)
                        {
                            /// Jesli punkt jest na scianie
                            if (wallList[i].getStart().X < dividePoint1.X && wallList[i].getEnd().X > dividePoint1.X)
                            {
                                /// Tworzymy nowe krotsze sciany
                                Wall shortWall1 = new Wall();
                                Wall shortWall2 = new Wall();

                                shortWall1.setStart(wallList[i].getStart());
                                shortWall1.setEnd(dividePoint1);
                                shortWall1.setThickness(wallList[i].getThickness());
                                shortWall1.repairPoints();

                                shortWall2.setStart(wallList[i].getEnd());
                                shortWall2.setEnd(dividePoint1);
                                shortWall2.setThickness(wallList[i].getThickness());
                                shortWall2.repairPoints();

                                wallList.Add(shortWall1);
                                wallList.Add(shortWall2);
                                /// Usuwamy dluzsza sciane
                                wallList.RemoveAt(i);
                                i--;
                            }
                        }
                        else if (wallList[i].getStart().Y == dividePoint2.Y)
                        {
                            /// Jesli punkt jest na scianie
                            if (wallList[i].getStart().X < dividePoint2.X && wallList[i].getEnd().X > dividePoint2.X)
                            {
                                /// Tworzymy nowe krotsze sciany
                                Wall shortWall1 = new Wall();
                                Wall shortWall2 = new Wall();

                                shortWall1.setStart(wallList[i].getStart());
                                shortWall1.setEnd(dividePoint2);
                                shortWall1.setThickness(wallList[i].getThickness());
                                shortWall1.repairPoints();

                                shortWall2.setStart(wallList[i].getEnd());
                                shortWall2.setEnd(dividePoint2);
                                shortWall2.setThickness(wallList[i].getThickness());
                                shortWall2.repairPoints();

                                wallList.Add(shortWall1);
                                wallList.Add(shortWall2);
                                /// Usuwamy dluzsza sciane
                                wallList.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    else if (wallList[i].isVertical())
                    {
                        /// Jesli punkt jest na tej samej prostej
                        if (wallList[i].getStart().X == dividePoint1.X)
                        {
                            /// Jesli punkt jest na scianie
                            if (wallList[i].getStart().Y < dividePoint1.Y && wallList[i].getEnd().Y > dividePoint1.Y)
                            {
                                /// Tworzymy nowe krotsze sciany
                                Wall shortWall1 = new Wall();
                                Wall shortWall2 = new Wall();

                                shortWall1.setStart(wallList[i].getStart());
                                shortWall1.setEnd(dividePoint1);
                                shortWall1.setThickness(wallList[i].getThickness());
                                shortWall1.repairPoints();

                                shortWall2.setStart(wallList[i].getEnd());
                                shortWall2.setEnd(dividePoint1);
                                shortWall2.setThickness(wallList[i].getThickness());
                                shortWall2.repairPoints();

                                wallList.Add(shortWall1);
                                wallList.Add(shortWall2);
                                /// Usuwamy dluzsza sciane
                                wallList.RemoveAt(i);
                                i--;
                            }
                        }
                        else if (wallList[i].getStart().X == dividePoint2.X)
                        {
                            /// Jesli punkt jest na scianie
                            if (wallList[i].getStart().Y < dividePoint2.Y && wallList[i].getEnd().Y > dividePoint2.Y)
                            {
                                /// Tworzymy nowe krotsze sciany
                                Wall shortWall1 = new Wall();
                                Wall shortWall2 = new Wall();

                                shortWall1.setStart(wallList[i].getStart());
                                shortWall1.setEnd(dividePoint2);
                                shortWall1.setThickness(wallList[i].getThickness());
                                shortWall1.repairPoints();

                                shortWall2.setStart(wallList[i].getEnd());
                                shortWall2.setEnd(dividePoint2);
                                shortWall2.setThickness(wallList[i].getThickness());
                                shortWall2.repairPoints();

                                wallList.Add(shortWall1);
                                wallList.Add(shortWall2);
                                /// Usuwamy dluzsza sciane
                                wallList.RemoveAt(i);
                                i--;
                            }
                        }
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Sciana nie jest ani pozioma ani pionowa divideWalls()");
                    }
                }
            }*/
            /// Sciany sie krzyzuja
            for(int j = 0; j < wallList.Count; j++)
            {
                for(int i = 0; i < wallList.Count; i++)
                {
                    if (wallList[j].isHorizontal() && wallList[i].isVertical())
                    {
                        if(wallList[j].getStart().X < wallList[i].getStart().X && wallList[i].getStart().X < wallList[j].getEnd().X)
                        {
                            if(wallList[i].getStart().Y <= wallList[j].getStart().Y && wallList[j].getStart().Y <= wallList[i].getEnd().Y)
                            {
                                /// Tworzymy nowe krotsze sciany
                                Wall shortWall1 = new Wall();
                                Wall shortWall2 = new Wall();

                                shortWall1.setStart(wallList[j].getStart());
                                shortWall1.setEnd(wallList[i].getStart().X, wallList[j].getStart().Y);
                                shortWall1.setThickness(wallList[j].getThickness());
                                shortWall1.repairPoints();

                                shortWall2.setStart(wallList[j].getEnd());
                                shortWall2.setEnd(wallList[i].getStart().X, wallList[j].getStart().Y);
                                shortWall2.setThickness(wallList[j].getThickness());
                                shortWall2.repairPoints();

                                wallList.Add(shortWall1);
                                wallList.Add(shortWall2);
                                /// Usuwamy dluzsza sciane
                                wallList.RemoveAt(j);
                                i = 0;
                            }
                        }
                    }
                    else if (wallList[j].isVertical() && wallList[i].isHorizontal())
                    {
                        if (wallList[j].getStart().Y < wallList[i].getStart().Y && wallList[i].getStart().Y < wallList[j].getEnd().Y)
                        {
                            if (wallList[i].getStart().X <= wallList[j].getStart().X && wallList[j].getStart().X <= wallList[i].getEnd().X)
                            {
                                /// Tworzymy nowe krotsze sciany
                                Wall shortWall1 = new Wall();
                                Wall shortWall2 = new Wall();

                                shortWall1.setStart(wallList[j].getStart());
                                shortWall1.setEnd(wallList[j].getStart().X, wallList[i].getStart().Y);
                                shortWall1.setThickness(wallList[j].getThickness());
                                shortWall1.repairPoints();

                                shortWall2.setStart(wallList[j].getEnd());
                                shortWall2.setEnd(wallList[j].getStart().X, wallList[i].getStart().Y);
                                shortWall2.setThickness(wallList[j].getThickness());
                                shortWall2.repairPoints();

                                wallList.Add(shortWall1);
                                wallList.Add(shortWall2);
                                /// Usuwamy dluzsza sciane
                                wallList.RemoveAt(j);
                                i = 0;
                            }
                        }
                    }
                }
            }
        }
        
    }
}
