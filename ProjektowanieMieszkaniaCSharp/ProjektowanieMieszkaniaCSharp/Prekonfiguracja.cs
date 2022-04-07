using System;
using System.Windows.Forms;

namespace ProjektowanieMieszkaniaCSharp
{
    public partial class Prekonfiguracja : Form
    {
        public static int prefferedArtboardHeight;
        public static int prefferedArtboardWidth;
        public static string prefferedMeasuringUnit = "Metry(m)";
        public static bool isSaveFileInitialized = false;
        public static bool isAppClosingInitialized = false;
        public Prekonfiguracja()
        {
            InitializeComponent();

            if(prefferedMeasuringUnit == "Metry(m)")
            {
                prefferedArtboardHeight = (int)numericUpDownHeight.Value * 50;
                prefferedArtboardWidth = (int)numericUpDownWidth.Value * 50;
            }
            else
            {
                prefferedArtboardHeight = (int)((float)numericUpDownHeight.Value * 16.6f);
                prefferedArtboardWidth = (int)((float)numericUpDownWidth.Value * 16.6f);
            }
        }

        private void numericUpDownWidth_ValueChanged(object sender, EventArgs e)
        {
            if (prefferedMeasuringUnit == "Metry(m)")
            {
                prefferedArtboardWidth = (int)numericUpDownWidth.Value * 50;
            }
            else
            {
                prefferedArtboardWidth = (int)((float)numericUpDownWidth.Value * 16.6f);
            }
        }

        private void numericUpDownHeight_ValueChanged(object sender, EventArgs e)
        {
            if (prefferedMeasuringUnit == "Metry(m)")
            {
                prefferedArtboardHeight = (int)numericUpDownHeight.Value * 50;
            }
            else
            {
                prefferedArtboardHeight = (int)((float)numericUpDownHeight.Value * 16.6f);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            prefferedMeasuringUnit = comboBox1.SelectedItem.ToString();
        }

        private void buttonLoadSaveFile_Click(object sender, EventArgs e)
        {
            isSaveFileInitialized = true;
            Close();
        }

        private void buttonCreateNewProject_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCloseApp_Click(object sender, EventArgs e)
        {
            isAppClosingInitialized = true;
            Close();
        }
    }
}
