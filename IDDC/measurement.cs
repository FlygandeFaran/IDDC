using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDDC
{
    public class measurement
    {
        private List<double> m_dose;
        private List<double> m_z;
        private double name;
        private double r100;
        private double r90Left;
        private double r90Right;
        private double r80Left;
        private double r80Right;
        private double r50Left;
        private double r50Right;
        private double r20;
        private double r10;
        private double m_FWHM;
        private double r90r10;
        private double r80r20;
        private double d20;
        private double maxPlateau;
        private double dMax;
        private bool upsideDown;
        private string filename;
        
        #region properties

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        public bool UpsideDown
        {
            get { return upsideDown; }
            set { upsideDown = value; }
        }

        public double DMax
        {
            get { return dMax; }
            set { dMax = value; }
        }


        public double MaxPlateau
        {
            get { return maxPlateau; }
            set { maxPlateau = value; }
        }


        public double D20
        {
            get { return d20; }
            set { d20 = value; }
        }


        public double R80R20
        {
            get { return r80r20; }
            set { r80r20 = value; }
        }


        public double R80R10
        {
            get { return r90r10; }
            set { r90r10 = value; }
        }


        public double FWHM
        {
            get { return m_FWHM; }
            set { m_FWHM = value; }
        }


        public double R10
        {
            get { return r10; }
            set { r10 = value; }
        }


        public double R20
        {
            get { return r20; }
            set { r20 = value; }
        }


        public double R50Right
        {
            get { return r50Right; }
            set { r50Right = value; }
        }


        public double R50Left
        {
            get { return r50Left; }
            set { r50Left = value; }
        }


        public double R80Right
        {
            get { return r80Right; }
            set { r80Right = value; }
        }


        public double R80Left
        {
            get { return r80Left; }
            set { r80Left = value; }
        }


        public double R90Right
        {
            get { return r90Right; }
            set { r90Right = value; }
        }


        public double R90Left
        {
            get { return r90Left; }
            set { r90Left = value; }
        }


        public double R100
        {
            get { return r100; }
            set { r100 = value; }
        }


        public List<double> Z
        {
            get { return m_z; }
            set { m_z = value; }
        }

        public List<double> Dose
        {
            get { return m_dose; }
            set { m_dose = value; }
        }
        public double Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion

        public measurement()
        {
            m_dose = new List<double>();
            m_z = new List<double>();
        }
        public void Add(double dose, double z)
        {
            m_dose.Add(dose);
            m_z.Add(z);
        }
        public object[] CalculateData(object[] measurementData)
        {
            dMax = m_dose.Max();
            for (int i = 0; i < m_dose.Count; i++)
                m_dose[i] = Math.Round(m_dose[i] / dMax * 100, 2);
            if (upsideDown)
            {
                dMax = m_dose.Max();
                r100 = m_z.ElementAt(m_dose.IndexOf(dMax));
                r90Left = interpolateAfter(0.9);
                r90Right = interpolateBefore(0.9);
                r80Left = interpolateAfter(0.8);
                r80Right = interpolateBefore(0.8);
                r50Left = interpolateAfter(0.5);
                r50Right = interpolateBefore(0.5);
                r20 = interpolateBefore(0.2);
                r10 = interpolateBefore(0.1);
                m_FWHM = r50Right - r50Left;
                r90r10 = r10 - r90Right;
                r80r20 = r20 - r80Right;
                d20 = DoseinterpolateBefore();
                maxPlateau = dMax / d20;
            }
            else
            {
                dMax = m_dose.Max();
                r100 = m_z.ElementAt(m_dose.IndexOf(dMax));
                r90Left = interpolateBefore(0.9);
                r90Right = interpolateAfter(0.9);
                r80Left = interpolateBefore(0.8);
                r80Right = interpolateAfter(0.8);
                r50Left = interpolateBefore(0.5);
                r50Right = interpolateAfter(0.5);
                r20 = interpolateAfter(0.2);
                r10 = interpolateAfter(0.1);
                m_FWHM = r50Right - r50Left;
                r90r10 = r10 - r90Right;
                r80r20 = r20 - r80Right;
                d20 = DoseinterpolateAfter();
                maxPlateau = dMax / d20;
                
            }

            measurementData[0] = name;
            measurementData[1] = m_z.ElementAt(m_dose.IndexOf(dMax));
            measurementData[2] = Math.Round(r90Left, 2);
            measurementData[3] = Math.Round(r90Right, 2);
            measurementData[4] = Math.Round(r80Left, 2);
            measurementData[5] = Math.Round(r80Right, 2);
            measurementData[6] = Math.Round(r50Left, 2);
            measurementData[7] = Math.Round(r50Right, 2);
            measurementData[8] = Math.Round(r20, 2);
            measurementData[9] = Math.Round(r10, 2);
            measurementData[10] = Math.Round(m_FWHM, 2);
            measurementData[11] = Math.Round((r90r10), 2);
            measurementData[12] = Math.Round((r80r20), 2);
            measurementData[13] = Math.Round(d20, 2);
            measurementData[14] = Math.Round(maxPlateau, 2);
            measurementData[15] = m_dose.Max();
            
            return measurementData;
        }


        private double interpolateBefore(double percent)
        {
            double z_interpolated;
            try
            {
                double roundedDose = m_dose.Where(x => x >= (dMax * percent)).First();
                int lowerIndex = m_dose.IndexOf(roundedDose);

                z_interpolated = m_z.ElementAt(lowerIndex) + (dMax * percent - m_dose.ElementAt(lowerIndex)) * (m_z.ElementAt(lowerIndex + 1) - m_z.ElementAt(lowerIndex)) / (m_dose.ElementAt(lowerIndex + 1) - m_dose.ElementAt(lowerIndex));
                if (upsideDown)
                    z_interpolated = m_z.ElementAt(lowerIndex) + (dMax * percent - m_dose.ElementAt(lowerIndex)) * (m_z.ElementAt(lowerIndex) - m_z.ElementAt(lowerIndex - 1)) / (m_dose.ElementAt(lowerIndex) - m_dose.ElementAt(lowerIndex - 1));
            }
            catch
            {
                z_interpolated = 0;
            }
            return z_interpolated;
        }
        private double interpolateAfter(double percent)
        {
            double z_interpolated;
            try
            {
                double roundedDose = m_dose.Where(x => x >= (dMax * percent)).Last();
                int lowerIndex = m_dose.LastIndexOf(roundedDose);

                z_interpolated = m_z.ElementAt(lowerIndex) + (dMax * percent - m_dose.ElementAt(lowerIndex)) * (m_z.ElementAt(lowerIndex + 1) - m_z.ElementAt(lowerIndex)) / (m_dose.ElementAt(lowerIndex + 1) - m_dose.ElementAt(lowerIndex));
                if (upsideDown)
                    z_interpolated = m_z.ElementAt(lowerIndex) + (dMax * percent - m_dose.ElementAt(lowerIndex)) * (m_z.ElementAt(lowerIndex) - m_z.ElementAt(lowerIndex - 1)) / (m_dose.ElementAt(lowerIndex) - m_dose.ElementAt(lowerIndex - 1));
            }
            catch
            {
                z_interpolated = 0;
            }
            string hej;
            if (name.ToString() == "75" && percent == 0.2)
                hej = "";
            return z_interpolated;
        }
        private double DoseinterpolateAfter()
        {
            double dose_interpolated;
            try
            {
                double z_20 = m_z.FirstOrDefault(x => x == 20);
                if (z_20 != 0)
                {
                    int index = m_z.LastIndexOf(z_20);
                    dose_interpolated = m_dose[index];
                    return dose_interpolated;
                }
                double roundedDepth = m_z.Where(x => x >= 20).First();
                int lowerIndex = m_z.LastIndexOf(roundedDepth);

                dose_interpolated = m_dose.ElementAt(lowerIndex) + (20 - m_z.ElementAt(lowerIndex)) * (m_dose.ElementAt(lowerIndex + 1) - m_dose.ElementAt(lowerIndex)) / (m_z.ElementAt(lowerIndex + 1) - m_z.ElementAt(lowerIndex));
            }
            catch
            {
                dose_interpolated = 0;
            }
            return dose_interpolated;
        }
        private double DoseinterpolateBefore()
        {
            double dose_interpolated;
            try
            {
                double z_20 = m_z.FirstOrDefault(x => x == 20);
                if (z_20 != 0)
                {
                    int index = m_z.LastIndexOf(z_20);
                    dose_interpolated = m_dose[index];
                    return dose_interpolated;
                }
                double roundedDepth = m_z.Where(x => x >= 20).Last();
                int lowerIndex = m_z.LastIndexOf(roundedDepth);

                dose_interpolated = m_dose.ElementAt(lowerIndex) + (20 - m_z.ElementAt(lowerIndex)) * (m_dose.ElementAt(lowerIndex + 1) - m_dose.ElementAt(lowerIndex)) / (m_z.ElementAt(lowerIndex + 1) - m_z.ElementAt(lowerIndex));
            }
            catch
            {
                dose_interpolated = 0;
            }
            return dose_interpolated;
        }
        public override string ToString()
        {
            return Name.ToString() + " MeV     " + filename;
        }

    }
}
