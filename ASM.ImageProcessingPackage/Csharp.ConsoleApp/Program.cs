using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Csharp.ConsoleApp
{
    class Program
    {
        [DllImport("Project.dll")]
        private static extern void Median_Filter([In, Out]byte[] rgb_arr, int width, int hight, [In, Out] byte[] window_arr, int window_width);

          [DllImport("Project.dll")]
        private static extern void Brightness([In, Out] byte[] rgb_arr ,int size , int brightness_value);
       
        [DllImport("Project.dll")]
        private static extern void GrayScale([In, Out] byte[]rgb_arr, int size);

        [DllImport("Project.dll")]
        private static extern void GrayScale2([In, Out] byte[] rgb_arr, int size);

        static void Main(string[] args)
        {
            //  byte[] red_arr = {0,0,0,0,0,0, 2, 5, 3,0,0, 7, 10, 32,0, 0,0, 1, 6,0,0, 8, 11, 16,0,0,0,0,0,0};
            byte[] red_arr = { 100, 200, 250, 255, 20, 3, 4 , 55 , 60};
            
            /*
            int width_of_pic = 5;
            int hight_of_pic = 6;
            //int size = width_of_pic * hight_of_pic;
            int width_of_window = 3;
            byte[] window_arr = new byte [9];
            */
            byte brightness_value =100 ;
            int size = 9;
            GrayScale2(red_arr, size);
           // Brightness(red_arr, size,brightness_value);

         //   Median_Filter(red_arr, width_of_pic, hight_of_pic, window_arr, width_of_window);
            for (int i = 0; i < size; i++)
            {
                Console.Write(red_arr[i] + " ");
            }
            Console.WriteLine();
           /* for (int i = 0; i < width_of_window * width_of_window; i++)
            {
                Console.Write(window_arr[i] + " ");
            }
            Console.WriteLine();
            //   window( red_arr, width_of_pic , hight_of_pic );
            */


        }
    }
}
