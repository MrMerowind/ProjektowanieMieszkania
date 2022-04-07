using ProjektowanieMieszkaniaCSharp.Klasy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace ProjektowanieMieszkaniaCSharp
{
    public partial class Form1 : Form
    {
        int prefferedArtboardWidth;
        int prefferedArtboardHeight;

        string prefferedMeasuringUnit;  // default = metry
        string activeFurnitureImageSource;

        Furniture activeFurniture;

        Graphics g;
        Rysunek r, r2;
        
        Wall wallUnderConstruction = new Wall();
        Window windowUnderConstruction = new Window();
        Door doorUnderConstruction = new Door();

        bool firstPositionSet;
        bool secondPositionSet;
        bool furniturePlaced;

        bool doorMouseDown;

        int movingWallId = -1;
        int movingWindowId = -1;

        PointF movingWallPreviousMousePosition;
        PointF movingWindowPreviousMousePosition;

        public Form1()
        {
            InitializeComponent();
            
            

            prefferedArtboardHeight = Prekonfiguracja.prefferedArtboardHeight;
            prefferedArtboardWidth = Prekonfiguracja.prefferedArtboardWidth;
            prefferedMeasuringUnit = Prekonfiguracja.prefferedMeasuringUnit;

            Console.WriteLine("w: " + prefferedArtboardWidth + " h: " + prefferedArtboardHeight);
            Console.WriteLine("unit: " + prefferedMeasuringUnit);

            //this.Size = new Size(prefferedArtboardWidth, prefferedArtboardHeight); // Powinno zmienic rozmiar okna na podany
            

            textBox1.Visible = false;
            firstPositionSet = false;
            secondPositionSet = false;
            menuStrip1.BackColor = Color.DarkSlateGray;
            menuStrip1.ForeColor = Color.White;

            doorMouseDown = false;
            
            artboard.Image = new Bitmap(prefferedArtboardWidth, prefferedArtboardHeight);
            g = Graphics.FromImage(artboard.Image);
            r = new Rysunek(prefferedArtboardWidth, prefferedArtboardHeight, Color.Teal);
            r2 = new Rysunek(prefferedArtboardWidth, prefferedArtboardHeight, Color.Teal);

            LoadFurniture(); // Nie moze byc wyzej
            r.Rysuj(g);

            artboard.Refresh();

            if (Prekonfiguracja.isSaveFileInitialized)
            {
                loadProject();
            }
        }

        private void artboard_Resize(object sender, System.EventArgs e)
        {
            /* To już nie potrzebne ale zostawiam do kopiowania
            
            artboard.Image = new Bitmap(artboard.Width, artboard.Height);
            g = Graphics.FromImage(artboard.Image);
            r.setWidth(artboard.Width);
            r.setHeight(artboard.Height);*/
        }


        private void artboard_MouseDown(object sender, MouseEventArgs e)
        {
            PointF mouseScaledLocation = e.Location;
            mouseScaledLocation.X /= artboardScale;
            mouseScaledLocation.Y /= artboardScale;

            if (e.Button == MouseButtons.Left)
            {
                if (radioPlaceWall.Checked)
                {
                    if (!firstPositionSet)
                    {
                        wallUnderConstruction.setThickness(10);
                        wallUnderConstruction.setStartSnapToGrid(mouseScaledLocation);
                        wallUnderConstruction.setEndSnapToGrid(mouseScaledLocation);
                        firstPositionSet = true;
                    }
                    else if (!secondPositionSet)
                    {
                        wallUnderConstruction.setEndSnapToGrid(mouseScaledLocation);
                        secondPositionSet = true;
                    }
                }
                else if(radioEdit.Checked)
                {
                    for (int i = 0; i < r.wallList.Count; i++)
                    {
                        /// Jeśli mysz jest w obszarze danej sciany
                        if (mouseScaledLocation.X >= r.wallList[i].getPositionWithThickness().X
                            && mouseScaledLocation.X <= r.wallList[i].getPositionWithThickness().X + r.wallList[i].getSizeWithThickness().X
                            && mouseScaledLocation.Y >= r.wallList[i].getPositionWithThickness().Y
                            && mouseScaledLocation.Y <= r.wallList[i].getPositionWithThickness().Y + r.wallList[i].getSizeWithThickness().Y)
                        {
                            movingWallId = i;
                            movingWallPreviousMousePosition = mouseScaledLocation;
                            break;
                        }
                    }
                }
                else if (radioEditWindow.Checked)
                {
                    for (int i = 0; i < r.windowList.Count; i++)
                    {
                        /// Jeśli mysz jest w obszarze danej sciany
                        if (mouseScaledLocation.X >= r.windowList[i].getPositionWithThickness().X
                            && mouseScaledLocation.X <= r.windowList[i].getPositionWithThickness().X + r.windowList[i].getSizeWithThickness().X
                            && mouseScaledLocation.Y >= r.windowList[i].getPositionWithThickness().Y
                            && mouseScaledLocation.Y <= r.windowList[i].getPositionWithThickness().Y + r.windowList[i].getSizeWithThickness().Y)
                        {
                            movingWindowId = i;
                            movingWindowPreviousMousePosition = mouseScaledLocation;
                            break;
                        }
                    }
                }
                else if (radioPlaceWindow.Checked)
                {
                    if (!firstPositionSet)
                    {
                        windowUnderConstruction.setThickness(10);
                        windowUnderConstruction.setStartSnapToGrid(mouseScaledLocation);
                        windowUnderConstruction.setEndSnapToGrid(mouseScaledLocation);
                        firstPositionSet = true;
                    }
                    else if (!secondPositionSet)
                    {
                        windowUnderConstruction.setEndSnapToGrid(mouseScaledLocation);
                        secondPositionSet = true;
                    }
                }
                else if (radioButtonFurniture.Checked)
                {
                    if (furniturePlaced == false && activeFurnitureImageSource != "" && activeFurniture != null)
                    {
                        r.getFurnitureListToDraw().Last().setLocation(mouseScaledLocation);

                        if(!activeFurniture.isWrongPosition())
                        {
                            furniturePlaced = true;
                            r.isFurniturePlaced = true;
                        }
                            
                    }
                }
                else if(doorButton.Checked)
                {
                    if(doorMouseDown == false)
                    {
                        doorMouseDown = true;
                        doorUnderConstruction.setStart(mouseScaledLocation.X - 20.0f, mouseScaledLocation.Y);
                        doorUnderConstruction.setEnd(mouseScaledLocation.X + 20.0f, mouseScaledLocation.Y);

                        for (int i = 0; i < r.wallList.Count; i++)
                        {
                            /// Jeśli mysz jest w obszarze danej sciany
                            if (mouseScaledLocation.X >= r.wallList[i].getPositionWithThickness().X
                                && mouseScaledLocation.X <= r.wallList[i].getPositionWithThickness().X + r.wallList[i].getSizeWithThickness().X
                                && mouseScaledLocation.Y >= r.wallList[i].getPositionWithThickness().Y
                                && mouseScaledLocation.Y <= r.wallList[i].getPositionWithThickness().Y + r.wallList[i].getSizeWithThickness().Y)
                            {
                                if(r.wallList[i].isHorizontal())
                                {
                                    doorUnderConstruction.setStart(mouseScaledLocation.X - 20.0f, mouseScaledLocation.Y);
                                    doorUnderConstruction.setEnd(mouseScaledLocation.X + 20.0f, mouseScaledLocation.Y);
                                }
                                else if(r.wallList[i].isVertical())
                                {
                                    doorUnderConstruction.setStart(mouseScaledLocation.X, mouseScaledLocation.Y - 20.0f);
                                    doorUnderConstruction.setEnd(mouseScaledLocation.X, mouseScaledLocation.Y + 20.0f);
                                }
                                break;
                            }
                        }



                        doorUnderConstruction.setStartSnapToGrid(doorUnderConstruction.getStart());
                        doorUnderConstruction.setEndSnapToGrid(doorUnderConstruction.getEnd());
                        doorUnderConstruction.setThickness(10.0f);

                    }
                }
                

            }
            else if (e.Button == MouseButtons.Right)
            {
                if (radioPlaceWall.Checked)
                {
                    textBox1.Visible = false;
                    if (secondPositionSet)
                    {
                        secondPositionSet = false;
                    }
                    else if(firstPositionSet)
                    {
                        firstPositionSet = false;
                    }
                    else
                    {
                        for(int i = 0; i < r.wallList.Count; i++)
                        {
                            /// Jeśli mysz jest w obszarze danej sciany
                            if(mouseScaledLocation.X >= r.wallList[i].getPositionWithThickness().X
                                && mouseScaledLocation.X <= r.wallList[i].getPositionWithThickness().X + r.wallList[i].getSizeWithThickness().X
                                && mouseScaledLocation.Y >= r.wallList[i].getPositionWithThickness().Y
                                && mouseScaledLocation.Y <= r.wallList[i].getPositionWithThickness().Y + r.wallList[i].getSizeWithThickness().Y)
                            {
                                r.wallList.RemoveAt(i);
                                r.searchForRooms(); // Sprawdza czy usunelo sie jakies pomieszczenie
                                fillListBoxRooms();
                                break;
                            }
                        }
                    }
                }
                else if (radioPlaceWindow.Checked)
                {
                    textBox1.Visible = false;
                    if (secondPositionSet)
                    {
                        secondPositionSet = false;
                    }
                    else if (firstPositionSet)
                    {
                        firstPositionSet = false;
                    }
                    else
                    {
                        for (int i = 0; i < r.windowList.Count; i++)
                        {
                            /// Jeśli mysz jest w obszarze danej sciany
                            if (mouseScaledLocation.X >= r.windowList[i].getPositionWithThickness().X
                                && mouseScaledLocation.X <= r.windowList[i].getPositionWithThickness().X + r.windowList[i].getSizeWithThickness().X
                                && mouseScaledLocation.Y >= r.windowList[i].getPositionWithThickness().Y
                                && mouseScaledLocation.Y <= r.windowList[i].getPositionWithThickness().Y + r.windowList[i].getSizeWithThickness().Y)
                            {
                                r.windowList.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
                else if (doorButton.Checked)
                {
                    if (doorMouseDown)
                    {
                        doorMouseDown = false;
                    }
                    else
                    {
                        for (int i = 0; i < r.doorList.Count; i++)
                        {
                            /// Jeśli mysz jest w obszarze danej sciany
                            if (mouseScaledLocation.X >= r.doorList[i].getPositionWithThickness().X
                                && mouseScaledLocation.X <= r.doorList[i].getPositionWithThickness().X + r.doorList[i].getSizeWithThickness().X
                                && mouseScaledLocation.Y >= r.doorList[i].getPositionWithThickness().Y
                                && mouseScaledLocation.Y <= r.doorList[i].getPositionWithThickness().Y + r.doorList[i].getSizeWithThickness().Y)
                            {
                                r.doorList.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
                else if (radioButtonFurniture.Checked)
                {
                    if (furniturePlaced == true)
                    {
                        /// Czy mysz jest w danym meblu
                        for(int i = 0; i < r.furnitureListToDraw.Count; i++)
                        {
                            if(mouseScaledLocation.X >= r.furnitureListToDraw[i].getLocation().X &&
                                mouseScaledLocation.X <= r.furnitureListToDraw[i].getLocation().X + r.furnitureListToDraw[i].getWidth() &&
                                mouseScaledLocation.Y >= r.furnitureListToDraw[i].getLocation().Y &&
                                mouseScaledLocation.Y <= r.furnitureListToDraw[i].getLocation().Y + r.furnitureListToDraw[i].getHeight())
                            {
                                activeFurnitureImageSource = r.furnitureListToDraw[i].getImgSource();
                                activeFurniture = r.furnitureListToDraw[i];


                                Image tmp = r.wrongPossitionFurnitureImages[i];
                                r.wrongPossitionFurnitureImages[i] = r.wrongPossitionFurnitureImages.Last();
                                r.wrongPossitionFurnitureImages[r.wrongPossitionFurnitureImages.Count()-1] = tmp;

                                /*
                                r.furnitureListToDraw.Add(activeFurniture);
                                r.furnitureListToDraw.RemoveAt(i);*/

                                r.furnitureListToDraw[i] = r.furnitureListToDraw.Last();
                                r.furnitureListToDraw[r.furnitureListToDraw.Count() - 1] = activeFurniture;


                                furniturePlaced = false;

                                break;
                            }
                        }
                    }
                    else
                    {
                        furniturePlaced = true;
                        activeFurnitureImageSource = "";
                        activeFurniture = null;
                        if (r.furnitureListToDraw.Count() > 0)
                        {
                            r.furnitureListToDraw.RemoveAt(r.furnitureListToDraw.Count - 1);
                            r.wrongPossitionFurnitureImages.RemoveAt(r.wrongPossitionFurnitureImages.Count - 1);
                        }

                    }
                }
            }
        }

        private void artboard_MouseMove(object sender, MouseEventArgs e)
        {
            PointF mouseScaledLocation = e.Location;
            mouseScaledLocation.X /= artboardScale;
            mouseScaledLocation.Y /= artboardScale;
            if (radioPlaceWall.Checked)
            {
                if (firstPositionSet && !secondPositionSet)
                {
                    showWallLengthWhileBuilding();
                    wallUnderConstruction.setEndSnapToGrid(mouseScaledLocation);
                    wallUnderConstruction.straighten();
                }
            }
            else if (radioPlaceWindow.Checked)
            {
                if (firstPositionSet && !secondPositionSet)
                {
                    showWindowLengthWhileBuilding();
                    windowUnderConstruction.setEndSnapToGrid(mouseScaledLocation);
                    windowUnderConstruction.straighten();
                }
            }
            else if (radioButtonFurniture.Checked)
            {
                if (activeFurniture != null && furniturePlaced == false)
                {
                    artboard.Focus();
                    /// Sprawdzanie czy mebel jest w scianie
                    bool furnitureColidesWithWall = false;
                    for(int i = 0; i < r.wallList.Count; i++)
                    {
                        if(doesWallColideWithFurniture(r.wallList[i], activeFurniture.getLocation(), activeFurniture.getSize(), activeFurniture, mouseScaledLocation))
                        {
                            furnitureColidesWithWall = true;
                            break;
                        }
                    }
                    if (furnitureColidesWithWall) activeFurniture.setWrongPosition(true);
                    else activeFurniture.setWrongPosition(false);

                    r.isFurniturePlaced = false;

                    if(mouseScaledLocation.X > 1 && mouseScaledLocation.Y > 1)
                    {
                        r.furnitureSizeTextTopLeft = true;
                    }
                    else
                    {
                        r.furnitureSizeTextTopLeft = false;
                    }

                    r.getFurnitureListToDraw().Last().setLocation(mouseScaledLocation);
                }
            }
            if (e.Button == MouseButtons.Left)
            {
                if(radioEdit.Checked)
                {
                    if(movingWallId != -1)
                    {
                        PointF moveDistance = new PointF();
                        moveDistance.X = mouseScaledLocation.X - movingWallPreviousMousePosition.X;
                        moveDistance.Y = mouseScaledLocation.Y - movingWallPreviousMousePosition.Y;
                        movingWallPreviousMousePosition = mouseScaledLocation;
                        r.wallList[movingWallId].move(moveDistance);
                    }
                }
                else if (radioEditWindow.Checked)
                {
                    if (movingWindowId != -1)
                    {
                        PointF moveDistance = new PointF();
                        moveDistance.X = mouseScaledLocation.X - movingWindowPreviousMousePosition.X;
                        moveDistance.Y = mouseScaledLocation.Y - movingWindowPreviousMousePosition.Y;
                        movingWindowPreviousMousePosition = mouseScaledLocation;
                        r.windowList[movingWindowId].move(moveDistance);
                    }
                }
                else if (doorButton.Checked)
                {
                    if (doorMouseDown == true)
                    {
                        doorUnderConstruction.setStart(mouseScaledLocation.X - 20.0f, mouseScaledLocation.Y);
                        doorUnderConstruction.setEnd(mouseScaledLocation.X + 20.0f, mouseScaledLocation.Y);

                        for (int i = 0; i < r.wallList.Count; i++)
                        {
                            /// Jeśli mysz jest w obszarze danej sciany
                            if (mouseScaledLocation.X >= r.wallList[i].getPositionWithThickness().X
                                && mouseScaledLocation.X <= r.wallList[i].getPositionWithThickness().X + r.wallList[i].getSizeWithThickness().X
                                && mouseScaledLocation.Y >= r.wallList[i].getPositionWithThickness().Y
                                && mouseScaledLocation.Y <= r.wallList[i].getPositionWithThickness().Y + r.wallList[i].getSizeWithThickness().Y)
                            {
                                if (r.wallList[i].isHorizontal())
                                {
                                    doorUnderConstruction.setStart(mouseScaledLocation.X - 20.0f, mouseScaledLocation.Y);
                                    doorUnderConstruction.setEnd(mouseScaledLocation.X + 20.0f, mouseScaledLocation.Y);
                                }
                                else if (r.wallList[i].isVertical())
                                {
                                    doorUnderConstruction.setStart(mouseScaledLocation.X, mouseScaledLocation.Y - 20.0f);
                                    doorUnderConstruction.setEnd(mouseScaledLocation.X, mouseScaledLocation.Y + 20.0f);
                                }
                                break;
                            }
                        }

                        doorUnderConstruction.setStartSnapToGrid(doorUnderConstruction.getStart());
                        doorUnderConstruction.setEndSnapToGrid(doorUnderConstruction.getEnd());

                    }
                }
            }

            r.Rysuj(g);

            if (radioPlaceWall.Checked)
            {
                if (firstPositionSet && !secondPositionSet)
                {
                    showWallLengthWhileBuilding();
                    wallUnderConstruction.drawWithoutAdditionalSomething(g);
                }
            }
            else if (radioPlaceWindow.Checked)
            {
                if (firstPositionSet && !secondPositionSet)
                {
                    showWindowLengthWhileBuilding();
                    windowUnderConstruction.draw(g);
                }
            }
            else if(doorButton.Checked)
            {
                if(doorMouseDown)
                {
                    doorUnderConstruction.draw(g);
                }
            }

            artboard.Refresh();
        }

        private void artboard_MouseHover(object sender, EventArgs e)
        {
            // Empty atm
        }

        private void artboard_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioPlaceWall.Checked)
                {
                    if (firstPositionSet && secondPositionSet)
                    {
                        textBox1.Visible = false;
                        wallUnderConstruction.straighten();
                        wallUnderConstruction.repairPoints();
                        r.Dodaj(new Wall(wallUnderConstruction));
                        firstPositionSet = false;
                        secondPositionSet = false;

                        Console.WriteLine("placed wall x:{0} y:{1}", wallUnderConstruction.getStart(), wallUnderConstruction.getEnd());

                        r.divideWalls(); // Dzieli sciane na dwie czesci

                        r.searchForRooms(); // Sprawdza czy utworzylo sie nowe pomieszczenie
                        fillListBoxRooms();


                    }

                }
                else if(radioEdit.Checked)
                {
                    if(movingWallId < r.wallList.Count && movingWallId >= 0)
                        r.wallList[movingWallId].snapToGrid();
                    movingWallId = -1;

                    r.divideWalls();

                    r.searchForRooms(); // Sprawdza czy nie utworzylo sie nowe pomieszczenie
                    fillListBoxRooms();
                }
                else if (radioEditWindow.Checked)
                {
                    if (movingWindowId < r.windowList.Count && movingWindowId >= 0)
                        r.windowList[movingWindowId].snapToGrid();
                    movingWallId = -1;
                }
                else if (radioPlaceWindow.Checked)
                {
                    if (firstPositionSet && secondPositionSet)
                    {
                        textBox1.Visible = false;
                        windowUnderConstruction.straighten();
                        windowUnderConstruction.repairPoints();
                        r.Dodaj(new Window(windowUnderConstruction));
                        firstPositionSet = false;
                        secondPositionSet = false;

                        Console.WriteLine("placed window x:{0} y:{1}", wallUnderConstruction.getStart(), wallUnderConstruction.getEnd());

                        // To tylko okna nie potrzeba szukac nowych pomieszczen
                        // r.searchForRooms(); // Sprawdza czy utworzylo sie nowe pomieszczenie
                        // fillListBoxRooms();
                    }
                }
                else if(doorButton.Checked)
                {
                    if(doorMouseDown == true)
                    {
                        doorUnderConstruction.straighten();
                        doorUnderConstruction.repairPoints();
                        r.Dodaj(new Door(doorUnderConstruction));
                        doorMouseDown = false;
                    }
                }
            }
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {
            saveProject();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            loadProject();
        }

        public void saveProject()
        {
            string filename = "save";
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.DefaultExt = ".xml";
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.Filter = "Pliki xml(*.xml) | *.xml";
   ;

            // Wybieranie sciezki
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
            }
            else
            {
                return;
            }

            // Czy nazwa pliku konczy sie na .xml
            if (!filename.EndsWith(".xml"))
            {
                filename += ".xml";
            }


            // Zapis do xml

            var xml = new XElement("Everything",

                                               r.getWallList().Select(x =>
                                               new XElement("Wall",
                                                    new XAttribute("WallStartX", x.getStart().X),
                                                    new XAttribute("WallStartY", x.getStart().Y),
                                                    new XAttribute("WallEndX", x.getEnd().X),
                                                    new XAttribute("WallEndY", x.getEnd().Y),
                                                    new XAttribute("WallThickness", x.getThickness()))),

                                               r.getWindowList().Select(x =>
                                               new XElement("Window",
                                                    new XAttribute("WindowStartX", x.getStart().X),
                                                    new XAttribute("WindowStartY", x.getStart().Y),
                                                    new XAttribute("WindowEndX", x.getEnd().X),
                                                    new XAttribute("WindowEndY", x.getEnd().Y),
                                                    new XAttribute("WindowThickness", x.getThickness()))),

                                               r.getDoorList().Select(x =>
                                               new XElement("Door",
                                                    new XAttribute("DoorStartX", x.getStart().X),
                                                    new XAttribute("DoorStartY", x.getStart().Y),
                                                    new XAttribute("DoorEndX", x.getEnd().X),
                                                    new XAttribute("DoorEndY", x.getEnd().Y),
                                                    new XAttribute("DoorThickness", x.getThickness()),
                                                    new XAttribute("DoorColor", x.getColor().ToKnownColor().ToString()))),

                                               r.getRoomList().Select(x =>
                                               new XElement("Room",
                                                    new XAttribute("RoomName", x.getRoomName()),
                                                    new XAttribute("RoomColor", x.getRoomColor().ToKnownColor().ToString()))),

                                               r.getFurnitureListToDraw().Select(x =>
                                               new XElement("Furniture",
                                                    new XAttribute("FurnitureLocationX", (int)x.getLocation().X),       // musi byc castowanie na int bo to pixele i float sie wywala
                                                    new XAttribute("FurnitureLocationY", (int)x.getLocation().Y),
                                                    new XAttribute("FurnitureImageSource", x.getImgSource()),
                                                    new XAttribute("FurnitureName", x.getName()),
                                                    new XAttribute("FurnitureWidth", x.getWidth()),
                                                    new XAttribute("FurnitureHeight", x.getHeight()),
                                                    new XAttribute("FurnitureImageScaleWidth", x.getImageScaleWidthForProjectXML()),
                                                    new XAttribute("FurnitureImageScaleHeight", x.getImageScaleHeightForProjectXML()),
                                                    new XAttribute("FurnitureRotationAngle", x.getRotationAngle()),
                                                    new XAttribute("FurnitureIsMirrorTransformed", x.isMirrorTransformated()))),

                                               r.getFurnitureList().Select(x =>
                                               new XElement("FurnitureAddedByUser",
                                                    new XAttribute("Name", x.getName()),
                                                    new XAttribute("ImageSource", x.getImgSource()))),
                                               
                                               new XElement("Config",
                                                    new XAttribute("ArtboardWidth", prefferedArtboardWidth),
                                                    new XAttribute("ArtboardHeight", prefferedArtboardHeight),
                                                    new XAttribute("MeasuringUnit", prefferedMeasuringUnit),
                                                    new XAttribute("ArtboardScale", artboardScale)));

            xml.Save(filename);

            //if ( DialogResult == DialogResult.OK)
            MessageBox.Show("Zapisano do: " + filename);
        }

        public void loadProject()
        {
            string filename = "save.xml";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Pliki xml(*.xml) | *.xml";

            // Wybieranie sciezki
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
            }
            else
            {
                return;
            }

            r.flushWallList();  // Czyszczenie ścian z artboardu
            r.getFurnitureList().Clear();
            r.getFurnitureListToDraw().Clear();
            r.getWrongPossitionFurnitureImages().Clear();
            r.getDoorList().Clear();
            r.getWindowList().Clear();

            var xml = XDocument.Load(filename);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(filename, settings);
            List<Room> roomNamesToRename = new List<Room>();
            // Wczytywanie ścian do pamieci programu

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Wall"))
                {
                    if (reader.HasAttributes)
                    {
                        r.Dodaj(new Wall(float.Parse(reader.GetAttribute("WallStartX")),
                            float.Parse(reader.GetAttribute("WallStartY")),
                            float.Parse(reader.GetAttribute("WallEndX")),
                            float.Parse(reader.GetAttribute("WallEndY")),
                            float.Parse(reader.GetAttribute("WallThickness"))));
                    }
                }
                else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Window"))
                {
                    if (reader.HasAttributes)
                    {
                        r.Dodaj(new Window(float.Parse(reader.GetAttribute("WindowStartX")),
                            float.Parse(reader.GetAttribute("WindowStartY")),
                            float.Parse(reader.GetAttribute("WindowEndX")),
                            float.Parse(reader.GetAttribute("WindowEndY")),
                            float.Parse(reader.GetAttribute("WindowThickness"))));
                    }
                }
                else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Door"))
                {
                    if (reader.HasAttributes)
                    {
                        r.Dodaj(new Door(float.Parse(reader.GetAttribute("DoorStartX")),
                            float.Parse(reader.GetAttribute("DoorStartY")),
                            float.Parse(reader.GetAttribute("DoorEndX")),
                            float.Parse(reader.GetAttribute("DoorEndY")),
                            float.Parse(reader.GetAttribute("DoorThickness")),
                            Color.FromName(reader.GetAttribute("DoorColor"))));
                    }
                }
                else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Room"))
                {
                    if (reader.HasAttributes)
                    {
                        roomNamesToRename.Add(new Room(reader.GetAttribute("RoomName"),
                        Color.FromName(reader.GetAttribute("RoomColor"))));
                    }
                }
                else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Furniture"))
                {
                    if (reader.HasAttributes)
                    {
                        // Furniture(string nm, string imgSrc, float locX, float locY, float scaleW, float scaleH, int rotation, bool mirrorState) -- uzyty konstruktor
                        
                        r.Dodaj(new Furniture(         reader.GetAttribute("FurnitureName"),
                                                       reader.GetAttribute("FurnitureImageSource"),
                                                       int.Parse(reader.GetAttribute("FurnitureLocationX")),
                                                       int.Parse(reader.GetAttribute("FurnitureLocationY")),
                                                       int.Parse(reader.GetAttribute("FurnitureImageScaleWidth")),
                                                       int.Parse(reader.GetAttribute("FurnitureImageScaleHeight")),
                                                       int.Parse(reader.GetAttribute("FurnitureRotationAngle")),
                                                       bool.Parse(reader.GetAttribute("FurnitureIsMirrorTransformed"))));

                    }
                }
                else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "FurnitureAddedByUser"))
                {
                    if (reader.HasAttributes)
                    {
                        r.DodajMebelDoBiblioteki(new Furniture(reader.GetAttribute("Name"),            // Furniture(string nm, string imgSrc, float w, float h, float locX, float locY)
                                                       reader.GetAttribute("ImageSource")));

                    }
                }
                else if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Config"))
                {
                    if (reader.HasAttributes)
                    {
                        prefferedArtboardWidth = int.Parse(reader.GetAttribute("ArtboardWidth"));
                        prefferedArtboardHeight = int.Parse(reader.GetAttribute("ArtboardHeight"));
                        //artboardScale = float.Parse(reader.GetAttribute("ArtboardScale"));

                        prefferedMeasuringUnit = reader.GetAttribute("MeasuringUnit");

                        artboard.Image = new Bitmap(prefferedArtboardWidth, prefferedArtboardHeight);
                        g = Graphics.FromImage(artboard.Image);
                        r.setWidth(prefferedArtboardWidth);
                        r.setHeight(prefferedArtboardHeight);
                    }
                }
            }

            r.searchForRooms();

            for(int i=0; i<r.getRoomList().Count(); i++)
            {
                r.getRoomList()[i].setRoomName(roomNamesToRename[i].getRoomName());
                r.getRoomList()[i].setRoomColor(roomNamesToRename[i].getRoomColor());
            }
            fillListBoxRooms();
            listBoxRooms.Refresh();

            LoadFurniture();
            resetArtbordScale();
        }

        private void artboard_SizeChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("w: " + artboard.Width + " h: " + artboard.Height);
            /*
            artboard.Image = new Bitmap(artboard.Width, artboard.Height);
            g = Graphics.FromImage(artboard.Image);

            r.setWidth(artboard.Width);
            r.setHeight(artboard.Height);

            r.Rysuj(g);
            artboard.Refresh();*/
        }

        private void showWallLengthWhileBuilding()
        {
            textBox1.Visible = true;
            float wallLength;
            if (wallUnderConstruction.isHorizontal())
            {
                if (wallUnderConstruction.getStart().X > wallUnderConstruction.getEnd().X)                  // |   End        Start    |
                {
                    wallLength = wallUnderConstruction.getStart().X - wallUnderConstruction.getEnd().X;
                }
                else
                {                                                                                           // |   Start        End    |
                    wallLength = wallUnderConstruction.getEnd().X - wallUnderConstruction.getStart().X;
                }
            }
            else
            {
                if (wallUnderConstruction.getStart().Y > wallUnderConstruction.getEnd().Y)
                {
                    wallLength = wallUnderConstruction.getStart().Y - wallUnderConstruction.getEnd().Y;
                }
                else
                {
                    wallLength = wallUnderConstruction.getEnd().Y - wallUnderConstruction.getStart().Y;
                }
            }

            if (prefferedMeasuringUnit == "Metry(m)")
            {
                wallLength = wallLength * 0.01f * 2.0f;
                textBox1.Text = ("Długość ściany: " + wallLength.ToString() + " (m)");
            }
            else
            {
                wallLength = wallLength * 0.0304f * 2.0f;
                textBox1.Text = ("Długość ściany: " + wallLength.ToString() + " (ft)");
            }
        }

        private void showWindowLengthWhileBuilding()
        {
            textBox1.Visible = true;
            float windowLength;
            if (windowUnderConstruction.isHorizontal())
            {
                if (windowUnderConstruction.getStart().X > windowUnderConstruction.getEnd().X)                  // |   End        Start    |
                {
                    windowLength = windowUnderConstruction.getStart().X - windowUnderConstruction.getEnd().X;
                }
                else
                {                                                                                           // |   Start        End    |
                    windowLength = windowUnderConstruction.getEnd().X - windowUnderConstruction.getStart().X;
                }
            }
            else
            {
                if (windowUnderConstruction.getStart().Y > windowUnderConstruction.getEnd().Y)
                {
                    windowLength = windowUnderConstruction.getStart().Y - windowUnderConstruction.getEnd().Y;
                }
                else
                {
                    windowLength = windowUnderConstruction.getEnd().Y - windowUnderConstruction.getStart().Y;
                }
            }

            if (prefferedMeasuringUnit == "Metry(m)")
            {
                windowLength = windowLength * 0.01f * 2.0f;
                textBox1.Text = ("Długość okna: " + windowLength.ToString() + " (m)");
            }
            else
            {
                windowLength = windowLength * 0.0304f * 2.0f;
                textBox1.Text = ("Długość okna: " + windowLength.ToString() + " (ft)");
            }
        }

        private void resetArtbordScale()
        {
            // Nie zmieniamy skali
            if (artboardScale <= 0.2f) artboardScale = 0.2f;
            if (artboardScale >= 5.0f) artboardScale = 5f;

            // Zmieniamy rozmiar artboardu
            int newArtboardWidth = (int)((float)prefferedArtboardWidth * artboardScale);
            int newArtboardHeight = (int)((float)prefferedArtboardHeight * artboardScale);
            artboard.Image = new Bitmap(newArtboardWidth, newArtboardHeight);
            g = Graphics.FromImage(artboard.Image);

            // Skalujemy artboard
            g.ResetTransform();
            g.ScaleTransform(artboardScale, artboardScale);


            r.Rysuj(g);
            artboard.Refresh();
        }

        public float artboardScale = 1.0f;

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {

        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {

        }

        private void artboardZoomOut()
        {
            // Zmniejszamy skale
            artboardScale -= 0.1f;
            if (artboardScale <= 0.2f) artboardScale = 0.2f;

            // Zmieniamy rozmiar artboardu
            int newArtboardWidth = (int)((float)prefferedArtboardWidth * artboardScale);
            int newArtboardHeight = (int)((float)prefferedArtboardHeight * artboardScale);
            artboard.Image = new Bitmap(newArtboardWidth, newArtboardHeight);
            g = Graphics.FromImage(artboard.Image);


            // Skalujemy artboard
            g.ResetTransform();
            g.ScaleTransform(artboardScale, artboardScale);

            r.Rysuj(g);
            artboard.Refresh();
        }

        private void artboardZoomIn()
        {
            // Zwiększamy skale
            artboardScale += 0.1f;
            if (artboardScale >= 5.0f) artboardScale = 5f;

            // Zmieniamy rozmiar artboardu
            int newArtboardWidth = (int)((float)prefferedArtboardWidth * artboardScale);
            int newArtboardHeight = (int)((float)prefferedArtboardHeight * artboardScale);
            artboard.Image = new Bitmap(newArtboardWidth, newArtboardHeight);
            g = Graphics.FromImage(artboard.Image);

            // Skalujemy artboard
            g.ResetTransform();
            g.ScaleTransform(artboardScale, artboardScale);


            r.Rysuj(g);
            artboard.Refresh();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public void fillListBoxRooms()
        {
            listBoxRooms.BeginUpdate();
            listBoxRooms.Items.Clear();

            foreach (Room x in r.getRoomList())
            {
                listBoxRooms.Items.Add(x);
            }
            listBoxRooms.Refresh();
            listBoxRooms.EndUpdate();
        }

        private void listBoxRooms_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void listBoxRooms_KeyUp(object sender, KeyEventArgs e)
        {
            if (r.roomList.Count() == 0) return;
            

            Room selectedRoom = (Room)listBoxRooms.SelectedItem;

            if (selectedRoom == null) return;
            
            if (e.KeyCode == Keys.Back)
            {
                if(selectedRoom.roomName != "")
                    selectedRoom.roomName = selectedRoom.roomName.Substring(0, selectedRoom.roomName.Length - 1); 
            }
            else
            {
                char code = (char)e.KeyCode;
                if((code >= 48 && code <= 57) || (code >= 65 && code <= 90) || (code >= 97 && code <= 122))
                    selectedRoom.roomName += code;
            }
            List<Room> l = new List<Room>();
            foreach(Room rr in listBoxRooms.Items)
            {
                l.Add(rr);
            }
            listBoxRooms.Items.Clear();
            listBoxRooms.Items.AddRange(l.ToArray());
            listBoxRooms.SelectedItem = selectedRoom;
        }

        private void listBoxRooms_Click(object sender, EventArgs e)
        {
            Pen blackPen = new Pen(Color.Black, 3);
            SolidBrush polyBrush = new SolidBrush(Color.FromArgb(128, 0, 51, 153)); // Kolor podswietlenia

            Room selectedRoom = (Room)listBoxRooms.SelectedItem;

            /// Tymczasowa naprawa
            if (selectedRoom == null) return;

            r.Rysuj(g);
            artboard.Refresh();

            if (selectedRoom.getCorners().Count >= 4)
            {
                PointF[] polygonCorners = new PointF[selectedRoom.getCorners().Count()];

                for (int i = 0; i < selectedRoom.getCorners().Count(); i++)
                {
                    polygonCorners[i] = selectedRoom.getCorners()[i];

                }

                g.FillPolygon(polyBrush, polygonCorners);
                artboard.Refresh();
            }

            //r.Rysuj(g);
            artboard.Refresh();
        }

        public void LoadFurniture()
        {
            Console.WriteLine("=================================");
            Console.WriteLine("Starting loading furniture library");

            // Wlasciwości listy mebli
            listViewFurniture.LabelEdit = true;                 // czy user moze edytowac nazwy
            listViewFurniture.GridLines = true;                 // siatka
            listViewFurniture.Sorting = SortOrder.Ascending;    // sortowanie alfabetyczne
            listViewFurniture.AllowColumnReorder = true;        // mozna rearanzowac kolumny
            listViewFurniture.Columns.Add("Biblioteka mebli", -2, HorizontalAlignment.Center);
            listViewFurniture.Clear();
            

            // Live
            string filename = "furnitureData.xml";
            var xml = XDocument.Load(filename);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(filename, settings);


            // Wczytywanie mebli
            

            while (reader.Read())
            {
                if ((reader.NodeType == XmlNodeType.Element) && (reader.Name == "Furniture"))
                {
                    if (reader.HasAttributes)
                    {
                        if (File.Exists(reader.GetAttribute("ImageSource").ToString()))
                        {
                            r.furnitureListPush(new Furniture(reader.GetAttribute("Name").ToString(),
                                                          reader.GetAttribute("ImageSource").ToString(),
                                                          float.Parse(reader.GetAttribute("Width")),
                                                          float.Parse(reader.GetAttribute("Height"))));
                        }
                        else
                        {
                            r.furnitureListPush(new Furniture(reader.GetAttribute("Name").ToString(),
                                                          "testImage.bmp",
                                                          float.Parse(reader.GetAttribute("Width")),
                                                          float.Parse(reader.GetAttribute("Height"))));
                        }
                    }
                }
            }
            
            Console.WriteLine("Found {0} furnitures to load.", r.getFurnitureList().Count());

            ImageList imageListForThumbnails = new ImageList();
            imageListForThumbnails.ImageSize = new Size(64, 64);
            imageListForThumbnails.ColorDepth = ColorDepth.Depth32Bit;
            int licznik = 0;

            foreach(Furniture f in r.getFurnitureList())
            {
                Console.WriteLine("Loading {0} to furniture library with source of: {1}.", f.getName(), f.getImgSource());

                if (File.Exists(f.getImgSource()))
                {
                    imageListForThumbnails.Images.Add(Bitmap.FromFile(f.getImgSource()));
                }
                else
                {
                    imageListForThumbnails.Images.Add(Bitmap.FromFile("testImage.bmp"));
                }
                
                

                listViewFurniture.Items.Add(f.getName(), licznik++);
            }
            listViewFurniture.SmallImageList = imageListForThumbnails;
            listViewFurniture.LargeImageList = imageListForThumbnails;


            Console.WriteLine("Finished loading furniture library");
            Console.WriteLine("=================================");
        }

        private void AddFurnitureByUser()
        {
            string filename = "save.xml";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Pliki graficzne(*.png;*.bmp*.jpg;*.jpeg)|*.png;*.bmp*.jpg;*.jpeg|All files (*.*)|*.*";

            // Wybieranie sciezki
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
            }
            else
            {
                return;
            }

            string nazwaNowegoMebla = "Nadaj wlasna nazwe poprzez podwojne powolne klikniecie";
            int numeratorKopii = 0;
            foreach (Furniture f in r.getFurnitureList())
            {
                if (f.getName().Contains(nazwaNowegoMebla))
                {
                    numeratorKopii++;
                }
            }

            if (numeratorKopii != 0) nazwaNowegoMebla += ("(" + numeratorKopii + ")");


            r.furnitureListPush(new Furniture(nazwaNowegoMebla, filename));
            listViewFurniture.Items.Add(nazwaNowegoMebla, r.getFurnitureList().Count());
            listViewFurniture.LargeImageList.Images.Add(Bitmap.FromFile(r.getFurnitureList().Last().getImgSource()));
        }

        
        private bool isMouseOnArtboard()
        {
            if (MousePosition.X > artboard.Location.X && MousePosition.X < artboard.Location.X + artboard.Width)
            {
                if (MousePosition.Y > artboard.Location.Y && MousePosition.Y < artboard.Location.Y + artboard.Height)
                {
                    Console.WriteLine("Mouse over artboard");
                    return true;
                }
                
            }
            Console.WriteLine("x x x Mouse NOT on artboard x x x");
            return false;
        }

        private void listViewFurniture_DoubleClick(object sender, EventArgs e)
        {
            FurnitureListViewPlace();
        }
        private void listViewFurniture_ItemDrag(object sender, ItemDragEventArgs e)
        {
            FurnitureListViewPlace();
        }

        private void groupTools_Enter(object sender, EventArgs e)
        {

        }

        private void radioButtonFurniture_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonFurniture.Checked)
            {
                listViewFurniture.Visible = true;
            }
            else
            {
                listViewFurniture.Visible = false;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void listViewFurniture_MouseClick(object sender, MouseEventArgs e)
        {
            
        }


        private void FurnitureListViewPlace()
        {
            furniturePlaced = false;
            string focused = listViewFurniture.FocusedItem.Text;
            string focusedImgSource = "error";

            Console.WriteLine("Stawiam {0}", focused);

            foreach (Furniture f in r.getFurnitureList())
            {
                if (f.getName() == focused)
                {
                    focusedImgSource = f.getImgSource();
                    activeFurniture = f;

                    break;
                }
            }

            if (focusedImgSource == "error")
            {
                Console.WriteLine("Nie znaleziono sciezki do wybranego mebla.");
                furniturePlaced = true;
            }
            else
            {
                r.Dodaj(new Furniture(activeFurniture));

                activeFurniture = r.furnitureListToDraw[r.furnitureListToDraw.Count - 1];

                activeFurnitureImageSource = focusedImgSource;
                Console.WriteLine("Sciezka {0} to: {1}", focused, activeFurnitureImageSource);
            }
            
            artboard.Focus();
        }

        private void listViewFurniture_MouseUp(object sender, MouseEventArgs e)
        {
            /*
            ContextMenu furnitureContextMenu = new ContextMenu();
            furnitureContextMenu.MenuItems.Add("Dodaj własny mebel.");

            if (MouseButtons.Right == e.Button)
            {
                Console.WriteLine("Kliknieto prawym");


                furnitureContextMenu.Show(this, MousePosition);
            }*/

        }

        void furnitureContextMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void contextMenuStripFurnitureViewList_Click(object sender, EventArgs e)
        {
            
        }

        private void listViewFurniture_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            string focused = listViewFurniture.FocusedItem.Text;

            foreach (Furniture f in r.getFurnitureList())
            {
                if (f.getName() == focused)
                {
                    f.setName(e.Label);

                    break;
                }
            }

            Console.WriteLine("Changing furniture name FROM: \"{0}\", TO \"{1}\".", focused, e.Label);
        }

        private void dodajWłasnyMebelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFurnitureByUser();
        }

        private void deleteFurnitureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFurniture.SelectedItems.Count == 1)
            {
                string focused = listViewFurniture.FocusedItem.Text;
                int licznik = 0;

                foreach (Furniture f in r.getFurnitureList())
                {
                    if (f.getName() == focused)
                    {
                        r.getFurnitureList().RemoveAt(licznik);
                        listViewFurniture.FocusedItem.Remove();

                        break;
                    }
                    licznik++;
                }
            }
            else if (listViewFurniture.SelectedItems.Count > 1)
            {
                List<string> itemsToDelete = new List<string>();

                foreach (ListViewItem f in listViewFurniture.SelectedItems)
                {
                    itemsToDelete.Add(f.Text);
                }

                
                for (int i = 0; i < r.getFurnitureList().Count(); i++)
                {
                    foreach (string execution in itemsToDelete)
                    {
                        if (r.getFurnitureList()[i].getName() == execution)
                        {
                            listViewFurniture.FindItemWithText(execution).Remove();
                            r.getFurnitureList().RemoveAt(i);
                            i--;

                            break;
                        }
                    }
                }
            }
        }

        private bool doesWallColideWithFurniture(Wall wall, PointF furniturePosition, PointF furnitureSize, Furniture f, PointF mouseScaledLocation)
        {
            
            if(wall.getStart().X < furniturePosition.X + furnitureSize.X && wall.getEnd().X > furniturePosition.X
                && wall.getStart().Y < furniturePosition.Y + furnitureSize.Y && wall.getEnd().Y > furniturePosition.Y)
            {
                return true;
            }
            return false;
            

            /*
            Color Transparent = new Color();
            Transparent = Color.FromArgb(255, 0, 0, 0);

            g.Clear(Transparent);
            r.DrawWithoutBackground(g);

            int MPosX = (int)mouseScaledLocation.X;
            int MPosY = (int)mouseScaledLocation.Y;


            //artboard.Refresh();

            //Image tmp = artboard.Image;

            Bitmap Badany = (Bitmap)Bitmap.FromFile(f.getImgSource());
            //Bitmap tmpBackground = (Bitmap)tmp;

            Bitmap tmpBackground = new Bitmap(artboard.Image);

            -
            for (int i = 1; i < Badany.Width; i++)
            {
                for (int j = 1; j < Badany.Height; j++)
                {
                    if (Badany.GetPixel(i, j) != Color.Transparent)
                    {
                        if (MPosX + i < tmpBackground.Width)
                        {
                            if (MPosY + j < tmpBackground.Height)
                            {
                                if (tmpBackground.GetPixel(MPosX + i, MPosY + j) != Color.Transparent)
                                {
                                    Console.WriteLine("SPRAWDZA");
                                    Console.WriteLine("MPosX: {0}, tmpX: {1}", MPosX, MPosX + i);
                                    Console.WriteLine("MPosY: {0}, tmpY: {1}", MPosY, MPosY + j);

                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
            */
        }

        private void contextMenuStripFurnitureViewList_Opening(object sender, CancelEventArgs e)
        {
            if (listViewFurniture.SelectedItems.Count < 1)
            {
                contextMenuStripFurnitureViewList.Items[1].Enabled = false;
            }
            else
            {
                contextMenuStripFurnitureViewList.Items[1].Enabled = true;
            }
            contextMenuStripFurnitureViewList.Refresh();
        }

        private void zmieńNazwęZaznaczonegoMeblaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewFurniture.SelectedItems.Count == 1)
            {
                listViewFurniture.FocusedItem.BeginEdit();
            }
        }


        /// Niechcaacy dodalem ale musi to zostac chyba ze umiesz usunac
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (furniturePlaced == false)
            {

                switch (e.KeyCode)
                {
                    case Keys.R:
                        
                        if (activeFurniture != null && furniturePlaced == false)
                        {
                            Console.WriteLine("Rotating by 90 degrees.");
                            r.getFurnitureListToDraw().Last().rotate(90);
                            r.getFurnitureListToDraw().Last().flipSizes();

                            r.getWrongPossitionFurnitureImages().Last().RotateFlip(RotateFlipType.Rotate90FlipNone);



                            /// Sprawdzanie czy mebel jest w scianie
                            bool furnitureColidesWithWall = false;
                            for (int i = 0; i < r.wallList.Count; i++)
                            {
                                /*
                                if (doesWallColideWithFurniture(r.wallList[i], activeFurniture.getLocation(), activeFurniture.getSize(), activeFurniture, mouseScaledLocation))
                                {
                                    furnitureColidesWithWall = true;
                                    break;
                                }*/
                            }
                            if (furnitureColidesWithWall) activeFurniture.setWrongPosition(true);
                            else activeFurniture.setWrongPosition(false);
                        }
                        break;

                    case Keys.M:
                        if (activeFurniture != null && furniturePlaced == false)
                        {
                            Console.WriteLine("Mirrored the image in Y-axis.");
                            r.getFurnitureListToDraw().Last().switchMirrorTransformation();

                            r.getWrongPossitionFurnitureImages().Last().RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        break;

                    case Keys.Left:
                        if (activeFurniture != null && furniturePlaced == false)
                        {
                            Console.WriteLine("Shrinking the image in Y-axis.");
                            r.getFurnitureListToDraw().Last().changeImageScaleWidthBy(-0.1f);
                        }
                        break;

                    case Keys.Right:
                        if (activeFurniture != null && furniturePlaced == false)
                        {
                            Console.WriteLine("Widening the image in Y-axis.");
                            r.getFurnitureListToDraw().Last().changeImageScaleWidthBy(+0.1f);
                        }
                        break;

                    case Keys.Up:
                        if (activeFurniture != null && furniturePlaced == false)
                        {
                            Console.WriteLine("Shrinking the image in X-axis.");
                            r.getFurnitureListToDraw().Last().changeImageScaleHeightBy(-0.1f);
                        }
                        break;

                    case Keys.Down:
                        if (activeFurniture != null && furniturePlaced == false)
                        {
                            Console.WriteLine("Widening the image in X-axis.");
                            r.getFurnitureListToDraw().Last().changeImageScaleHeightBy(+0.1f);
                        }
                        break;

                }

            }
            r.Rysuj(g);
            artboard.Refresh();
        }

        
        private void zapiszToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveProject();
        }
        private void wczytajToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadProject();
        }

        private void przybliżToolStripMenuItem_Click(object sender, EventArgs e)
        {
            artboardZoomIn();
        }
        private void oddalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            artboardZoomOut();
        }

    }
}
