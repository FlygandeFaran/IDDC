using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IDDC
{
    public partial class MainWindow : Form
    {
        private DataTable dt;
        private List<MeasurementManager> listOfFiles;

        public MainWindow()
        {
            InitializeComponent();
            listOfFiles = new List<MeasurementManager>();
            cbEnableAll.Enabled = false;
            InitializeChartArea();
        }
        private void InitializeChartArea()
        {
            DepthDoseCurve.ChartAreas.First().AxisY.Title = "Dose [%]";
            DepthDoseCurve.ChartAreas.First().AxisX.Title = "Depth [mm]";

            ChartArea CA = DepthDoseCurve.ChartAreas[0];  // quick reference
            CA.AxisX.ScaleView.Zoomable = true;
            CA.CursorX.AutoScroll = true;
            CA.CursorX.IsUserSelectionEnabled = true;
            CA.AxisY.ScaleView.Zoomable = true;
            CA.CursorY.AutoScroll = true;
            CA.CursorY.IsUserSelectionEnabled = true;
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Multiselect = true;
            fdlg.Title = "Select one ore multiple OPGs";
            fdlg.InitialDirectory = @"G:\Gemensam\03 QA Program\QA periodiska kontroller\06 Årskontroller\01 Energikontroll";
            fdlg.Filter = "All files (*.*)|*.*|ASCII files (*.asc)|*.asc";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                ImportOPG(fdlg.FileNames, fdlg.SafeFileNames);
                lblFilename.Text = fdlg.SafeFileName;
            }
        }
        private void UpdateMeasurementsSelection()
        {
            clsbMeasurements.Items.Clear();
            foreach (MeasurementManager mm_ in clsbFiles.CheckedItems)
            {
                foreach (var meas in mm_.ListOfMeasurements)
                    clsbMeasurements.Items.Add(meas);
            }
            clsbMeasurements.Sorted = true;
            if (clsbMeasurements.Items.Count > 0)
                cbEnableAll.Enabled = true;
            else
                cbEnableAll.Enabled = false;
        }
        private void UpdateFilesSelection()
        {
            clsbFiles.Items.Clear();
            foreach (var mm_ in listOfFiles)
                clsbFiles.Items.Add(mm_);
        }
        private void UpdateTable()
        {
            dataGridView.DataSource = null;
            dataGridView.DataSource = dt;
        }
        private void UpdateSeleceted()
        {
            if (cbEnableAll.Checked == true)
            {
                for (int i = 0; i < clsbMeasurements.Items.Count; i++)
                    clsbMeasurements.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < clsbMeasurements.Items.Count; i++)
                    clsbMeasurements.SetItemChecked(i, false);
            }
        }
        private void Plot()
        {

            this.DepthDoseCurve.Series.Clear();
            foreach (measurement meas in clsbMeasurements.CheckedItems)
            {
                try
                {
                    Series series;
                    series = this.DepthDoseCurve.Series.Add("");
                    for (int i = 0; i < meas.Z.Count; i++)
                    {
                        series.Points.AddXY(meas.Z[i], meas.Dose[i]);
                    }
                    series.ChartType = SeriesChartType.Spline;
                    series.Name = meas.ToString();
                }
                catch { }

            }

        }
        private void ImportOPG(string[] filenames, string[] fileNameNoPath)
        {
            InitializeTable();
            for (int j = 0; j < filenames.Length; j++)
            {
                List<string> lines = new List<string>();
                lines.AddRange(System.IO.File.ReadAllLines(filenames[j]));

                measurement measurement = new measurement();
                MeasurementManager mm = new MeasurementManager(fileNameNoPath[j]);
                bool ok = false;
                bool okEclipse = false;
                string sep = "\t";
                for (int i = 0; i < lines.Count(); i++)
                {
                    if ((lines[i].Contains("Dose") && lines[i].Contains("Z")))
                    {
                        //Messagebox.show("Hej");
                        ok = true;
                        string[] values = lines[i + 2].Substring(2).Split(sep.ToCharArray());
                        string[] SecondValues = lines[i + 10].Substring(2).Split(sep.ToCharArray());
                        double firstZ = 0;
                        double secondZ = 0;
                        try
                        {
                            firstZ = double.Parse(values[3].Trim());
                            secondZ = double.Parse(SecondValues[3].Trim());
                        }
                        catch
                        {
                            firstZ = double.Parse(values[3].Trim().Replace(".", ","));
                            secondZ = double.Parse(SecondValues[3].Trim().Replace(".", ","));
                        }
                        if (firstZ > secondZ)
                            measurement.UpsideDown = true;
                    }
                    else if (lines[i].Contains("ELE"))
                    {
                        string[] values = lines[i].Substring(2).Split(sep.ToCharArray());
                        try
                        {
                            measurement.Name = double.Parse(values.Last().Trim());
                        }
                        catch
                        {
                            measurement.Name = double.Parse(values.Last().Trim().Replace(".", ","));
                        }
                        measurement.Filename = fileNameNoPath[j];
                    }
                    if (i < lines.Count() - 2)
                        if (lines[i + 2].Contains("End of Measurement") || lines[i + 2].Contains("NOM"))
                        {
                            ok = false;
                            okEclipse = false;
                            mm.ListOfMeasurements.Add(measurement);
                            measurement = new measurement();
                        }
                    if (ok)
                    {
                        string[] values = lines[i + 2].Substring(2).Split(sep.ToCharArray());

                        double dose = 0;
                        double z = 0;
                        try
                        {
                            dose = double.Parse(values[4].Trim());
                            z = double.Parse(values[3].Trim());
                        }
                        catch
                        {
                            dose = double.Parse(values[4].Trim().Replace(".", ","));
                            z = double.Parse(values[3].Trim().Replace(".", ","));
                        }

                        measurement.Add(dose, z);
                    }

                    if (lines[i].Contains("NET"))
                    {
                        okEclipse = true;

                        string[] values = lines[i + 2].Substring(2).Split("+".ToCharArray());
                        string[] SecondValues = lines[i + 10].Substring(2).Split("+".ToCharArray());

                        double firstZ = 0;
                        double secondZ = 0;
                        try
                        {
                            firstZ = double.Parse(values[3].Trim().Remove(values[3].Count() - 1, 1));
                            secondZ = double.Parse(SecondValues[3].Trim().Remove(values[3].Count() - 1, 1));
                        }
                        catch
                        {
                            firstZ = double.Parse(values[3].Trim().Remove(values[3].Count() - 1, 1).Replace(".", ","));
                            secondZ = double.Parse(SecondValues[3].Trim().Remove(values[3].Count() - 1, 1).Replace(".", ","));
                        }
                        if (firstZ > secondZ)
                            measurement.UpsideDown = true;
                    }

                    if (lines[i].Contains("ENERGY"))
                    {
                        string values = lines[i].Substring(8);
                        try
                        {
                            measurement.Name = double.Parse(values.Trim());
                        }
                        catch
                        {
                            measurement.Name = double.Parse(values.Trim().Replace(".", ","));
                        }
                        measurement.Filename = fileNameNoPath[j];
                    }
                    if (okEclipse)
                    {
                        string[] values = lines[i + 2].Substring(2).Split("+".ToCharArray());

                        string dStr = values[3].Trim();

                        double z = 0;

                        try
                        {
                            dStr = dStr.Remove(dStr.Count() - 1, 1);
                            z = double.Parse(values[2].Trim());
                        }
                        catch
                        {
                            dStr = dStr.Remove(dStr.Count() - 1, 1).Replace(".", ",");
                            z = double.Parse(values[2].Trim().Replace(".", ","));
                        }
                        double dose = double.Parse(dStr);
                        measurement.Add(dose, z);
                    }
                }

                foreach (var meas in mm.ListOfMeasurements)
                {
                    object[] measurementData = new object[16];
                    measurementData = meas.CalculateData(measurementData);
                    dt.Rows.Add(measurementData);
                }
                if (listOfFiles.FirstOrDefault(x => x.Filename.Equals(mm.Filename)) == null)
                    listOfFiles.Add(mm);
            }
            UpdateTable();
            UpdateFilesSelection();
        }
        
        private void InitializeTable()
        {
            dt = new DataTable();
            dt.Columns.Add("Energy [MeV]", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R100", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R90 Left", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R90 Right", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R80 Left", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R80 Right", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R50 Left", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R50 Right", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R20", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R10", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("FWHM", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R80-R10", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("R80-R20", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("D20", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("Max/plateau", typeof(double)); // lägger till plan som rubrik
            dt.Columns.Add("Dmax", typeof(double)); // lägger till plan som rubrik
        }

        private void clsbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateMeasurementsSelection();
        }

        private void clsbMeasurements_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void clsbFiles_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private void clsbMeasurements_SelectedIndexChanged(object sender, EventArgs e)
        {
            Plot();
        }

        private void DepthDoseCurve_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoint = new Point(e.X, e.Y);
            /*
            DepthDoseCurve.ChartAreas[0].CursorX.SetCursorPixelPosition(mousePoint, true);
            DepthDoseCurve.ChartAreas[0].CursorY.SetCursorPixelPosition(mousePoint, true);*/

            var result = DepthDoseCurve.HitTest(e.X, e.Y);

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                DepthDoseCurve.Annotations.Clear();

                

                //float X = (float)listOfFiles.First().ListOfMeasurements.First().Z.Where(x => x.Equals(DepthDoseCurve.Series[0].Points[result.PointIndex].XValue)).Last();
                //float Y = (float)listOfFiles.First().ListOfMeasurements.First().Dose.Where(x => x.Equals(DepthDoseCurve.Series[0].Points[result.PointIndex].YValues[0])).Last();
                //var thisPt = new PointF(X, Y);
                CalloutAnnotation ca = new CalloutAnnotation();
                ca.AnchorDataPoint = result.Series.Points[result.PointIndex];
                double X = result.Series.Points[result.PointIndex].XValue;
                double Y = result.Series.Points[result.PointIndex].YValues[0];
                ca.X = X;
                ca.Y = Y;
                ca.Text = "Depth: " + X.ToString() + "   Dose: " + Y.ToString() + "\n" + result.Series.Name;
                ca.CalloutStyle = CalloutStyle.Rectangle;

                DepthDoseCurve.Annotations.Add(ca);
                DepthDoseCurve.Invalidate();
            }
        }

        private void cbEnableAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnableAll.Checked)
            {
                for (int i = 0; i < clsbMeasurements.Items.Count; i++)
                {
                    clsbMeasurements.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < clsbMeasurements.Items.Count; i++)
                {
                    clsbMeasurements.SetItemChecked(i, false);
                }
            }
            Plot();
        }
    }
}
