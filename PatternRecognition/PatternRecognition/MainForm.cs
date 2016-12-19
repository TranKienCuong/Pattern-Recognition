using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace PatternRecognition
{
    public partial class MainForm : Form
    {
        int[,] binaryMatrix = new int[15, 10];
        int bias = 30;
        Bitmap imageRecognition;
        Bitmap[] samples = new Bitmap[10];
        int epochs = 100;
        double E = 0;
        double EMax = 0.01;

        int[] neuralsInputLayer;
        double[] neuralsHiddenLayer;
        double[] neuralsOutputLayer;

        double[,] weightsInputAndHiddenLayer;
        double[,] weightsHiddenAndOutputLayer;

        public MainForm()
        {
            InitializeComponent();

            neuralsInputLayer = new int[150];
            neuralsHiddenLayer = new double[500];
            neuralsOutputLayer = new double[10];

            weightsInputAndHiddenLayer = new double[150, 500];
            for (int i = 0; i < 150; ++i)
            {
                for (int j = 0; j < 500; ++j)
                {
                    weightsInputAndHiddenLayer[i, j] = 0;
                }
            } // Khởi tạo mảng


            weightsHiddenAndOutputLayer = new double[500, 10];
            for (int i = 0; i < 500; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    weightsHiddenAndOutputLayer[i, j] = 0;
                }
            } // Khởi tạo mảng
        }

        /// <summary>
        /// nạp từ ma trận ảnh vào tầng input
        /// </summary>
        void ImportBinaryMatrixToInputLayer()
        {
            int k = 0;

            for (int i = 0; i < 15; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    neuralsInputLayer[k] = binaryMatrix[i, j];
                    k++;
                }
            }
        }

        /// <summary>
        /// Nạp từ tầng input sang hidden
        /// </summary>
        void ImportInputToHiddenLayer()
        {
            for (int j = 0; j < 500; ++j)
            {
                double sum = 0;
                for (int i = 0; i < 150; ++i)
                {
                    sum += weightsInputAndHiddenLayer[i, j] * neuralsInputLayer[i];
                }
                sum -= bias;
                neuralsHiddenLayer[j] = 2 / (1 + Math.Exp(-0.014 * sum)) - 1;
            }
        }

        /// <summary>
        /// Nạp từ tầng hidden sang output
        /// </summary>
        void ImportHiddenToOutputLayer(bool debug = false)
        {
            for (int j = 0; j < 10; ++j)
            {
                double sum = 0;
                for (int i = 0; i < 500; ++i)
                {
                    sum += weightsHiddenAndOutputLayer[i, j] * neuralsHiddenLayer[i];
                }
                sum -= bias;
                neuralsOutputLayer[j] = 2 / (1 + Math.Exp(-0.014 * sum)) - 1;

                if (debug)
                    Console.Write(neuralsOutputLayer[j].ToString("N2") + "\t");

                //if (neuralsOutputLayer[j] < 0.5f) // so sánh với giá trị ngưỡng
                //    neuralsOutputLayer[j] = 0;
                //else
                //    neuralsOutputLayer[j] = 1;
            }

            if (debug)
                Console.WriteLine();
        }

        void TrainNumber(int number)
        {
            ImportBinaryMatrixToInputLayer();

            ImportInputToHiddenLayer();

            ImportHiddenToOutputLayer(true);

            //for (int i = 0; i < 10; ++i)
            //{
            //    Console.Write(neuralsOutputLayer[i] + "\t");
            //}
            //Console.WriteLine();

            double[] delta = new double[10]; // Mảng lưu sai số

            int[] binaryResult = new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // Nhận diện số nào thì vị trí đó bật lên
            binaryResult[number] = 1;

            for (int i = 0; i < 10; ++i)
            {
                delta[i] = double.Parse(binaryResult[i].ToString()) - neuralsOutputLayer[i];
                E += (delta[i] * delta[i]);
            }

            for (int i = 0; i < 500; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    weightsHiddenAndOutputLayer[i, j] += 0.05 * delta[j] * neuralsHiddenLayer[i] * (1 - Math.Pow(neuralsOutputLayer[j], 2)) * 0.5f;
                }
            }

            for (int i = 0; i < 150; ++i)
            {
                for (int j = 0; j < 500; ++j)
                {
                    double sum = 0;
                    for (int k = 0; k < 10; ++k)
                    {
                        sum += weightsHiddenAndOutputLayer[j, k] * delta[k] * (1 - Math.Pow(neuralsOutputLayer[k], 2)) * 0.5f;
                    }
                    weightsInputAndHiddenLayer[i, j] += 0.05 * neuralsInputLayer[i] * (1 - Math.Pow(neuralsHiddenLayer[j], 2)) * 0.5f * sum;
                }
            }
        }

        void Recognize()
        {
            ImportBinaryMatrixToInputLayer();

            ImportInputToHiddenLayer();

            ImportHiddenToOutputLayer();

            int number = 0;
            for (int i = 0; i < 10; ++i)
            {
                if (neuralsOutputLayer[i] > neuralsOutputLayer[number])
                {
                    number = i;
                }
            }
            MessageBox.Show("This is " + number.ToString());

        }

        private void recognizeButton_Click(object sender, EventArgs e)
        {
            imageRecognition = (Bitmap)pictureRecognition.Image;
            Tuple<int, int, int, int> boundary = Utilities.FindSentenceBoundary(imageRecognition);

            imageRecognition = Utilities.CropBitmap(imageRecognition, boundary.Item1 + 1, boundary.Item2 + 1, boundary.Item3 - boundary.Item1 - 1, boundary.Item4 - boundary.Item2 - 1);

            imageRecognition = Utilities.ResizeImage(imageRecognition, 10, 15);

            //imageRecognition.Save("simplify3.png");

            Utilities.ConvertImageToBinaryMatrix(imageRecognition, binaryMatrix);

            Recognize();
        }

        private void browsingButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open image need to recognize";
            openFileDialog1.FileName = "";
            //openFileDialog1.InitialDirectory = @"C:\Users\Wiz\Desktop";
            openFileDialog1.Filter = "Image Files (.jpg, .png)|*.jpg;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imageRecognition = new Bitmap(openFileDialog1.FileName);
                pictureRecognition.Image = (Image)imageRecognition;
            }
        }

        private void learningButton_Click(object sender, EventArgs e)
        {
            for (int l = 0; l < epochs; l++)
            {
                E = 0;
                for (int i = 0; i < 10; i++)
                {
                    Bitmap imageLearning = samples[i];
                    Tuple<int, int, int, int> boundary = Utilities.FindSentenceBoundary(imageLearning);

                    imageLearning = Utilities.CropBitmap(imageLearning, boundary.Item1 + 1, boundary.Item2 + 1, boundary.Item3 - boundary.Item1 - 1, boundary.Item4 - boundary.Item2 - 1);
                    imageLearning = Utilities.ResizeImage(imageLearning, 10, 15);

                    //imageLearning.Save("newImage.png");

                    Utilities.ConvertImageToBinaryMatrix(imageLearning, binaryMatrix);

                    TrainNumber(i);
                }
                E /= 2;
                Console.WriteLine(E.ToString());
                if (E < EMax)
                    break;
            }
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderBrowserDialog1.SelectedPath;
                for (int i = 0; i < 10; i++)
                    samples[i] = (Bitmap)Image.FromFile(pathTextBox.Text + "/" + i + ".png");
            }
        }


        //double ConvertBinaryStringToInteger(string input)
        //{
        //    double sum = 0;
        //    for (int i = 0; i < input.Length; ++i)
        //    {
        //        if (input[i] == '1')
        //        {
        //            sum += Math.Pow(2, i);
        //        }
        //    }
        //    return sum;
        //}

        //string ConvertIntegerToBinaryString(int input)
        //{
        //    string binaryString = "";

        //    while (input != 0)
        //    {
        //        binaryString += (input % 2);
        //        input /= 2;
        //    }

        //    return binaryString;
        //}

    }
}
