using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CsharpGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        RGBPixel[,] ImageMatrix2;
        //RGBPixel[] D_Image_Matrix;

        [DllImport("Project.dll")]
        private static extern void Median_Filter([In, Out]byte[] rgb_arr, int width, int hight, [In, Out] byte[] window_arr, int window_width);

        [DllImport("Project.dll")]
        private static extern void Brightness([In, Out] byte[] rgb_arr, int size, int brightness_value);


        [DllImport("Project.dll")]
        private static extern void GrayScale([In, Out] byte[] rgb_arr, int size);

        [DllImport("Project.dll")]
        private static extern void GrayScale2([In, Out] byte[] rgb_arr, int size);

        [DllImport("Project.dll")]
        private static extern void Inverse([In, Out] byte[] rgb_arr, int size);

        [DllImport("Project.dll")]
        private static extern void Substraction([In, Out] byte[] rgb_arr1, int Size1Of_1D , [In, Out] byte[] rgb_arr2 , int Size2Of_1D);

        [DllImport("Project.dll")]
        private static extern void Addition([In, Out] byte[] rgb_arr1, int Size1Of_1D, [In, Out] byte[] rgb_arr2, int Size2Of_1D);
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);

            }

            int width = ImageOperations.GetWidth(ImageMatrix);
            int hight = ImageOperations.GetHeight(ImageMatrix);
            txtWidth.Text = width.ToString();
            txtHeight.Text = Height.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int window_width = int.Parse(textBox1.Text);
            byte[] window_arr = new byte[window_width * window_width];
            int width = ImageOperations.GetWidth(ImageMatrix);
            int hight = ImageOperations.GetHeight(ImageMatrix);

            int width_of_1D_arr = (width + (window_width - 1));
            int hight_of_1D_arr = (hight + (window_width - 1));
            int size = hight_of_1D_arr * width_of_1D_arr;
            byte []red_arr = new byte[size];
            byte []green_arr = new byte[size];
            byte []blue_arr = new byte[size];
            
            ImageOperations.convert_to_RGB_1D(ImageMatrix, width, hight, window_width ,red_arr , green_arr , blue_arr);
            Median_Filter(red_arr, width_of_1D_arr, hight_of_1D_arr, window_arr, window_width);
            Median_Filter(green_arr, width_of_1D_arr, hight_of_1D_arr, window_arr, window_width);
            Median_Filter(blue_arr, width_of_1D_arr, hight_of_1D_arr, window_arr, window_width);
            RGBPixel [,]img_matrix= ImageOperations.convert_to_RGB_2D(red_arr, green_arr, blue_arr, width, hight, window_width);
            ImageOperations.DisplayImage(img_matrix, pictureBox2);

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            int Brightness_value = int.Parse( textBox2.Text);
            int width = ImageOperations.GetWidth(ImageMatrix);
            int hight = ImageOperations.GetHeight(ImageMatrix);
            int SizeOf_1D = 3 * width * hight;
            byte []rgb_arr;

            rgb_arr= ImageOperations.convert_to_1D(ImageMatrix, width, hight);
            Brightness(rgb_arr, SizeOf_1D, Brightness_value);
            RGBPixel[,] img_matrix = ImageOperations.convert_to_2D(rgb_arr, width, hight);
            ImageOperations.DisplayImage(img_matrix, pictureBox2);

            
            
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int width = ImageOperations.GetWidth(ImageMatrix);
            int hight = ImageOperations.GetHeight(ImageMatrix);
            int SizeOf_1D = 3 * width * hight;
            byte[] rgb_arr;

            rgb_arr = ImageOperations.convert_to_1D(ImageMatrix, width, hight);
            GrayScale(rgb_arr, SizeOf_1D);
            RGBPixel[,] img_matrix = ImageOperations.convert_to_2D(rgb_arr, width, hight);
            ImageOperations.DisplayImage(img_matrix, pictureBox2);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int width = ImageOperations.GetWidth(ImageMatrix);
            int hight = ImageOperations.GetHeight(ImageMatrix);
            int SizeOf_1D = 3 * width * hight;
            byte[] rgb_arr;

            rgb_arr = ImageOperations.convert_to_1D(ImageMatrix, width, hight);
            GrayScale2(rgb_arr, SizeOf_1D);
            RGBPixel[,] img_matrix = ImageOperations.convert_to_2D(rgb_arr, width, hight);
            ImageOperations.DisplayImage(img_matrix, pictureBox2);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int width = ImageOperations.GetWidth(ImageMatrix);
            int hight = ImageOperations.GetHeight(ImageMatrix);
            int SizeOf_1D = 3 * width * hight;
            byte[] rgb_arr;

            rgb_arr = ImageOperations.convert_to_1D(ImageMatrix, width, hight);
            Inverse(rgb_arr, SizeOf_1D);
            RGBPixel[,] img_matrix = ImageOperations.convert_to_2D(rgb_arr, width, hight);
            ImageOperations.DisplayImage(img_matrix, pictureBox2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int width1 = ImageOperations.GetWidth(ImageMatrix);
            int hight1 = ImageOperations.GetHeight(ImageMatrix);
            int Size1Of_1D = 3 * width1 * hight1;
            byte[] rgb_arr1;

            rgb_arr1 = ImageOperations.convert_to_1D(ImageMatrix, width1, hight1);

            int width2 = ImageOperations.GetWidth(ImageMatrix2);
            int hight2 = ImageOperations.GetHeight(ImageMatrix2);
            int Size2Of_1D = 3 * width2 * hight2;
            byte[] rgb_arr2;

            rgb_arr2 = ImageOperations.convert_to_1D(ImageMatrix2, width2, hight2);


            Substraction(rgb_arr1, Size1Of_1D , rgb_arr2 , Size2Of_1D);
            RGBPixel[,] img_matrix = ImageOperations.convert_to_2D(rgb_arr1, width1, hight1);
            ImageOperations.DisplayImage(img_matrix, pictureBox2);

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix2 = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix2, pictureBox2);

            }

            int width2 = ImageOperations.GetWidth(ImageMatrix2);
            int hight2 = ImageOperations.GetHeight(ImageMatrix2);
            txtWidth.Text = width2.ToString();
            txtHeight.Text = hight2.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int width1 = ImageOperations.GetWidth(ImageMatrix);
            int hight1 = ImageOperations.GetHeight(ImageMatrix);
            int Size1Of_1D = 3 * width1 * hight1;
            byte[] rgb_arr1;

            rgb_arr1 = ImageOperations.convert_to_1D(ImageMatrix, width1, hight1);

            int width2 = ImageOperations.GetWidth(ImageMatrix2);
            int hight2 = ImageOperations.GetHeight(ImageMatrix2);
            int Size2Of_1D = 3 * width2 * hight2;
            byte[] rgb_arr2;

            rgb_arr2 = ImageOperations.convert_to_1D(ImageMatrix2, width2, hight2);


            Addition(rgb_arr1, Size1Of_1D, rgb_arr2, Size2Of_1D);
            RGBPixel[,] img_matrix = ImageOperations.convert_to_2D(rgb_arr1, width1, hight1);
            ImageOperations.DisplayImage(img_matrix, pictureBox2);

        }
    }
}
