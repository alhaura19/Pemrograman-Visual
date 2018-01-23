using System;
using System.Drawing;

namespace StegAES
{
    public class Metrics
    {
        public Metrics()
        {

        }

        public double MSE(Bitmap img1, Bitmap img2)
        {
            int M = img1.Height;
            int N = img1.Width;
            double MSE;

            double R = 0, G = 0, B = 0;
            double R2 = 0, G2 = 0, B2 = 0;
            double sum = 0, sum1, sum2, sum3, sum4 ;
           
            for (int j = 0; j < M; j++)
            {
                for (int i = 0; i < N; i++)
                {
                    R = img1.GetPixel(i, j).R;
                    G = img1.GetPixel(i, j).G;
                    B = img1.GetPixel(i, j).B;

                    R2 = img2.GetPixel(i, j).R;
                    G2 = img2.GetPixel(i, j).G;
                    B2 = img2.GetPixel(i, j).B;

                    sum1 = R - R2;
                    sum2 = G - G2;
                    sum3 = B - B2;
                    sum4 = (sum1 + sum2 + sum3) / 3;
                    sum += Math.Pow(sum4, 2);
                    //sum += Math.Pow(((R + G + B) - (R2 + G2 + B2)), 2);
                    //sum += (Math.Pow(R - R2, 2) + Math.Pow(G - G2, 2) + Math.Pow(B - B2, 2));
                }
            }
            MSE = sum / (M * N);

            return MSE;
        }

        public double PSNR(double MSE)
        {
            double PSNR;
            //PSNR = 20 * Math.Log10(255*255 / Math.Sqrt(MSE));
            PSNR = 10 * Math.Log10(255 * 255 / MSE);
            return PSNR;
        }

        private double Mean(Bitmap A)
        {
            double Mean;

            double R = 0, G = 0, B = 0;

            for (int j = 0; j < A.Height; j++)
            {
                for (int i = 0; i < A.Width; i++)
                {
                    Color c = A.GetPixel(i, j);
                    R += c.R;
                    G += c.G;
                    B += c.B;
                }
            }
            R /= (A.Height * A.Width);
            G /= (A.Height * A.Width);
            B /= (A.Height * A.Width);

            Mean = (R + G + B) / 3;

            return Mean;
        }

        private double Variance(Bitmap A, double Mean)
        {
            double Variance = 0;
            //double I = 0;

            double R = 0, G = 0, B = 0;

            for (int j = 0; j < A.Height; j++)
            {
                for (int i = 0; i < A.Width; i++)
                {
                    Color c = A.GetPixel(i, j);
                    R = c.R;
                    G = c.G;
                    B = c.B;
                    Variance += Math.Pow(((R + G + B) / 3) - Mean, 2);
                }
            }

            Variance = Variance / ((A.Height * A.Width) - 1);

            return Variance;
        }

        private double Covariance(Bitmap A, Bitmap B, double Mean1, double Mean2)
        {
            double Covariance = 0;

            //double I = 0;
            double R1 = 0, G1 = 0, B1 = 0;
            double R2 = 0, G2 = 0, B2 = 0;

            for (int j = 0; j < A.Height; j++)
            {
                for (int i = 0; i < A.Width; i++)
                {
                    Color c1 = A.GetPixel(i, j);
                    Color c2 = B.GetPixel(i, j);

                    R1 = c1.R;
                    G1 = c1.G;
                    B1 = c1.B;

                    R2 = c2.R;
                    G2 = c2.G;
                    B2 = c2.B;

                    Covariance += (((R1 + G1 + B1) / 3) - Mean1) * (((R2 + G2 + B2) / 3) - Mean2);
                }
            }
            Covariance = Covariance / ((A.Height * A.Width) - 1);

            return Covariance;
        }

        private double C(double k)
        {
            double C, L;

            L = Math.Pow(2, 8) - 1;
            C = Math.Pow((k * L), 2);

            return C;
        }

        public double SSIM(string[] imgArr1, string[] imgArr2)
        {
            double SSIM = 0;
            double m1, m2, c1, c2, cov, v1, v2;
            Bitmap img1, img2;

            for (int i = 0; i < imgArr1.Length; i++)
            {
                img1 = new Bitmap(imgArr1[i]);
                img2 = new Bitmap(imgArr2[i]);
                m1 = Mean(img1);
                m2 = Mean(img2);
                c1 = C(0.01);
                c2 = C(0.03);
                cov = Covariance(img1, img2, m1, m2);
                v1 = Variance(img1, m1);
                v2 = Variance(img2, m2);
                SSIM += (((2 * m1 * m2) + c1) * ((2 * cov) + c2)) / ((Math.Pow(m1, 2) + Math.Pow(m2, 2) + c1) * (v1 + v2 + c2));
                img1.Dispose();
                img2.Dispose();
            }

            SSIM = SSIM / imgArr1.Length;

            return SSIM;
        }


    }
}
