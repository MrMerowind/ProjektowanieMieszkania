using System;
using System.Drawing;

namespace ProjektowanieMieszkaniaCSharp.Klasy
{
    class Furniture
    {
        private string name;
        private string imgSource;

        private int rotationAngle;

        private bool mirrorTransformation = false;

        private bool wrongPosition = false;

        private float width;
        private float height;
        private float imageScaleWidth = 1.0f;
        private float imageScaleHeight = 1.0f;

        private PointF location;


        public Furniture()
        {
            name = "Blank";
            imgSource = "Blank";

            width = height = 0.2f;

            rotationAngle = 0;

            location.X = location.Y = 0.0f;
        }

        public Furniture(Furniture f)
        {
            name = f.name;
            imgSource = f.imgSource;

            width = f.width;
            height = f.height;
            imageScaleWidth = f.imageScaleWidth;
            imageScaleHeight = f.imageScaleHeight;

            rotationAngle = f.rotationAngle;

            wrongPosition = f.wrongPosition;

            location = f.location;
            mirrorTransformation = f.mirrorTransformation;
        }

        public Furniture(string nm, string imgSrc)
        {
            // DODAC SPRAWDZANIE POPRAWNOSCI

            name = nm;
            imgSource = imgSrc;

            rotationAngle = 0;

            loadSize();
        }

        public Furniture(string nm, string imgSrc, PointF loc)
        {
            // DODAC SPRAWDZANIE POPRAWNOSCI

            name = nm;
            imgSource = imgSrc;

            rotationAngle = 0;

            loadSize();

            location = loc;
        }

        public Furniture(string nm, string imgSrc, float locX, float locY)
        {
            // DODAC SPRAWDZANIE POPRAWNOSCI

            name = nm;
            imgSource = imgSrc;

            rotationAngle = 0;

            loadSize();

            location.X = locX;
            location.Y = locY;
        }

        public Furniture(string nm, string imgSrc, float locX, float locY, int rotation)
        {
            // DODAC SPRAWDZANIE POPRAWNOSCI

            name = nm;
            imgSource = imgSrc;

            rotationAngle = 0;

            loadSize();

            location.X = locX;
            location.Y = locY;

            rotationAngle = rotation;
        }

        public Furniture(string nm, string imgSrc, float locX, float locY, int rotation, bool mirrorState)
        {
            // DODAC SPRAWDZANIE POPRAWNOSCI

            name = nm;
            imgSource = imgSrc;

            rotationAngle = 0;

            loadSize();

            location.X = locX;
            location.Y = locY;

            rotationAngle = rotation;

            mirrorTransformation = mirrorState;
        }

        public Furniture(string nm, string imgSrc, int locX, int locY, float scaleW, float scaleH, int rotation, bool mirrorState)
        {
            // DODAC SPRAWDZANIE POPRAWNOSCI

            name = nm;
            imgSource = imgSrc;

            rotationAngle = 0;

            loadSize();

            imageScaleWidth = scaleW / 100;
            imageScaleHeight = scaleH / 100;

            location.X = locX;
            location.Y = locY;

            rotationAngle = rotation;

            mirrorTransformation = mirrorState;
        }



        // GETy START

        public string getName()
        {
            return name;
        }

        public string getImgSource()
        {
            return imgSource;
        }

        public float getWidth()
        {
            if (rotationAngle % 180 == 0) return width * imageScaleWidth;
            else return height * imageScaleHeight;
        }

        public float getHeight()
        {
            if (rotationAngle % 180 == 0) return height * imageScaleHeight;
            else return width * imageScaleWidth;
        }

        public PointF getLocation()
        {
            return location;
        }

        public bool isWrongPosition()
        {
            return wrongPosition;
        }

        public void setWrongPosition(bool state)
        {
            wrongPosition = state;
        }

        public int getHowManyRotations()
        {
            return (int)rotationAngle / 90;
        }

        public int getRotationAngle()
        {
            return rotationAngle;
        }

        public bool isMirrorTransformated()
        {
            if (mirrorTransformation)
            {
                return true;
            }
            else return false;
        }

        public float getImageScaleWidth()
        {
            return imageScaleWidth;
        }

        public float getImageScaleHeight()
        {
            return imageScaleHeight;
        }

        public int getImageScaleWidthForProjectXML()
        {
            return (int)(imageScaleWidth*100);
        }

        public int getImageScaleHeightForProjectXML()
        {
            return (int)(imageScaleHeight*100);
        }

        // GETy END

        // SETy Switche START

        public void setLocation(PointF x)
        {
            location = x;
        }

        public void setName(string newName)
        {
            name = newName;
        }

        public void switchMirrorTransformation()
        {
            if (mirrorTransformation) mirrorTransformation = false;
            else mirrorTransformation = true;
        }

        // SETy END

        // Reszta

        public void draw(Graphics g, Image i, PointF start)
        {
            
        }
        public void loadSize()
        {
            Image img = Bitmap.FromFile(imgSource);
            width = img.Size.Width;
            height = img.Size.Height;
            Console.WriteLine("name: {0} wh: {1} ht: {2}", imgSource, width, height);
        }

        public void flipSizes()
        {
            float tmp = width;
            width = height;
            height = tmp;
        }

        public void rotate(int angle)
        {
            rotationAngle += angle;
            rotationAngle %= 360;
        }

        public PointF getSize()
        {
            PointF size = new PointF();

            if (rotationAngle % 180 == 0)
            {
                size.X = width * imageScaleWidth;
                size.Y = height * imageScaleHeight;
            }
            else
            {
                size.X = height * imageScaleHeight;
                size.Y = width * imageScaleWidth;
            }

            
            return size;
        }

        public void changeImageScaleWidthBy(float difference)
        {
            if (rotationAngle % 180 == 0)
            {
                imageScaleWidth += difference;
                if (imageScaleWidth > 1.5f) imageScaleWidth = 1.5f;
                if (imageScaleWidth < 0.5f) imageScaleWidth = 0.5f;
            }
            else
            {
                imageScaleHeight += difference;
                if (imageScaleHeight > 1.5f) imageScaleHeight = 1.5f;
                if (imageScaleHeight < 0.5f) imageScaleHeight = 0.5f;
            }


            
        }

        public void changeImageScaleHeightBy(float difference)
        {
            if (rotationAngle % 180 == 0)
            {
                imageScaleHeight += difference;
                if (imageScaleHeight > 1.5f) imageScaleHeight = 1.5f;
                if (imageScaleHeight < 0.5f) imageScaleHeight = 0.5f;
            }
            else
            {
                imageScaleWidth += difference;
                if (imageScaleWidth > 1.5f) imageScaleWidth = 1.5f;
                if (imageScaleWidth < 0.5f) imageScaleWidth = 0.5f;
            }
        }

    }
}
