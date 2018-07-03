using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSFramework
{
    /// <summary>
    /// 
    /// </summary>
    public static class PrintSomething
    {
        static float f(float x, float y, float z)
        {
            float a = x * x + 9.0f / 4.0f * y * y + z * z - 1;
            return a * a * a - x * x * z * z * z - 9.0f / 80.0f * y * y * z * z * z;
        }

        static float h(float x, float z)
        {
            for (float y = 1.0f; y >= 0.0f; y -= 0.001f)
            {
                if (f(x, y, z) <= 0.0f)
                {
                    return y;
                }
            }
            return 0.0f;
        }

        /// <summary>
        /// 打印爱心
        /// </summary>
        public static void PrintHeart()
        {
            Console.WindowWidth = 180;
            Console.WindowHeight = 50;

            #region 圣诞树
            //int vec = 10;
            //int line = 5;
            //int tmp = 5;
            //for (int i = 0; i < line; i++)
            //{
            //    for (int j = tmp - 1 + vec; j > 0; j--)
            //    {
            //        Console.Write(" ");
            //    }
            //    for (int j = 0; j < i * 2 + 1; j++)
            //    {
            //        Console.Write("*");
            //    }
            //    Console.WriteLine();
            //    tmp--;
            //}

            //tmp = 5;
            //line = 4;

            //for (int i = 0; i < line; i++)
            //{
            //    for (int j = tmp - 4 + vec; j > 0; j--)
            //    {
            //        Console.Write(" ");
            //    }
            //    for (int j = 0; j < i * 2 + 7; j++)
            //    {
            //        Console.Write("*");
            //    }
            //    Console.WriteLine();
            //    tmp--;
            //}

            //tmp = 5;
            //line = 5;

            //for (int i = 0; i < line; i++)
            //{
            //    for (int j = tmp - 4 + vec; j > 0; j--)
            //    {
            //        Console.Write(" ");
            //    }
            //    for (int j = 0; j < i * 2 + 7; j++)
            //    {
            //        Console.Write("*");
            //    }
            //    Console.WriteLine();
            //    tmp--;
            //}

            //tmp = 5;
            //line = 5;

            //for (int i = 0; i < line; i++)
            //{
            //    for (int j = tmp - 2 + vec; j > 0; j--)
            //    {
            //        Console.Write(" ");
            //    }
            //    for (int j = 0; j < 3; j++)
            //    {
            //        Console.Write("*");
            //    }
            //    Console.WriteLine();
            //    //tmp--;
            //}

            //Console.WriteLine(); 
            #endregion

            #region 爱心
            char[] chars = ".:-=+*#%@".ToCharArray();
            for (float z = 1.5f; z > -1.5f; z -= 0.05f)
            {
                for (float x = -1.5f; x < 1.5f; x += 0.025f)
                {
                    float v = f(x, 0.0f, z);
                    if (v <= 0.0f)
                    {
                        float y0 = h(x, z);
                        float ny = 0.01f;
                        float nx = h(x + ny, z) - y0;
                        float nz = h(x, z + ny) - y0;
                        float nd = 1.0f / (float)Math.Sqrt(nx * nx + ny * ny + nz * nz);
                        float d = (nx + ny - nz) * nd * 0.5f + 0.5f;
                        int index = Convert.ToInt32(d * 5.0f);
                        if (index < 0)
                        {
                            index = 0;
                        }
                        else if (index > chars.Length - 1)
                        {
                            index = chars.Length - 1;
                        }
                        Console.Write(chars[index]);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            #endregion

        }
    }
}
