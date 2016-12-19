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
        int epochs = 300;
        double E = 0;
        double EMax = 0.0001;
        double learningRate = 1;

        int[] neuralsInputLayer;
        double[] neuralsHiddenLayer;
        double[] neuralsOutputLayer;

        double[,] weightsInputAndHiddenLayer;
        double[,] weightsHiddenAndOutputLayer;

        double[] errors;

        public MainForm()
        {
            InitializeComponent();

            neuralsInputLayer = new int[150];
            neuralsHiddenLayer = new double[250];
            neuralsOutputLayer = new double[4];

            weightsInputAndHiddenLayer = new double[150, 250];
            weightsHiddenAndOutputLayer = new double[250, 4];
            InitializeWeights();

            errors = new double[4];
        }

        double Sigmoid(double x)
        {
            return 2 / (1 + Math.Exp(-0.014 * x)) - 1;
        }

        double DerivativeSigmoid(double x)
        {
            return (1 - x * x) / 2;
        }

        /// <summary>
        /// Khởi tạo trọng số random từ -bias đến bias
        /// </summary>
        void InitializeWeights()
        {
            Random r = new Random();
            for (int i = 0; i < 150; ++i)
            {
                for (int j = 0; j < 250; ++j)
                {
                    weightsInputAndHiddenLayer[i, j] = r.Next(-bias, bias);
                }
            }

            for (int i = 0; i < 250; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    weightsHiddenAndOutputLayer[i, j] = r.Next(-bias, bias);
                }
            }
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
            for (int j = 0; j < 250; ++j)
            {
                double sum = 0;
                for (int i = 0; i < 150; ++i)
                {
                    sum += weightsInputAndHiddenLayer[i, j] * neuralsInputLayer[i];
                }
                //sum -= bias;
                neuralsHiddenLayer[j] = Sigmoid(sum);
            }
        }

        /// <summary>
        /// Nạp từ tầng hidden sang output
        /// </summary>
        void ImportHiddenToOutputLayer(bool debug = false)
        {
            for (int j = 0; j < 4; ++j)
            {
                double sum = 0;
                for (int i = 0; i < 250; ++i)
                {
                    sum += weightsHiddenAndOutputLayer[i, j] * neuralsHiddenLayer[i];
                }
                //sum -= bias;
                neuralsOutputLayer[j] = Sigmoid(sum);

                if (debug)
                    Console.Write(neuralsOutputLayer[j].ToString("N2") + "\t");
            }

            if (debug)
                Console.WriteLine();
        }

        void SetErrors(double[] delta)
        {
            for (int i = 0; i < 4; ++i)
            {
                errors[i] = delta[i] * DerivativeSigmoid(neuralsOutputLayer[i]);
            }
        }

        double AverageErrors()
        {
            double sum = 0;
            for (int i = 0; i < 4; ++i)
            {
                sum += errors[i];
            }
            return Math.Abs(sum) / 4;
        }

        void FixWeights()
        {
            for (int i = 0; i < 250; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    weightsHiddenAndOutputLayer[i, j] += learningRate * neuralsHiddenLayer[i] * errors[j];
                }
            }

            for (int i = 0; i < 150; ++i)
            {
                for (int j = 0; j < 250; ++j)
                {
                    double sum = 0;
                    for (int k = 0; k < 4; ++k)
                    {
                        sum += weightsHiddenAndOutputLayer[j, k] * errors[k];
                    }
                    weightsInputAndHiddenLayer[i, j] += learningRate * neuralsInputLayer[i] * DerivativeSigmoid(neuralsHiddenLayer[j]) * sum;
                }
            }
        }

        void TrainNumber(int number)
        {
            ImportBinaryMatrixToInputLayer();

            ImportInputToHiddenLayer();

            ImportHiddenToOutputLayer(true);

            //for (int i = 0; i < 4; ++i)
            //{
            //    Console.Write(neuralsOutputLayer[i] + "\t");
            //}
            //Console.WriteLine();

            double[] delta = new double[4]; // Mảng lưu sai số

            string binaryString = ConvertIntegerToBinaryString(number);

            for (int i = 0; i < 4; ++i)
            {
                delta[i] = double.Parse(binaryString[i].ToString()) - neuralsOutputLayer[i];
            }

            SetErrors(delta);

            E += AverageErrors();

            FixWeights();
        }

        /// <summary>
        /// so sánh với giá trị ngưỡng để chuyển thành bit 0 hoặc 1
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        int Threshold(double value)
        {
            int result;
            if (value < 0.9f)
                result = 0;
            else
                result = 1;
            return result;
        }

        void Recognize()
        {
            ImportBinaryMatrixToInputLayer();

            ImportInputToHiddenLayer();

            ImportHiddenToOutputLayer();

            string result = "";
            for (int i = 0; i < 4; i++)
                result += Threshold(neuralsOutputLayer[i]).ToString();
            int number = ConvertBinaryStringToInteger(result);

            MessageBox.Show("This is " + number.ToString());
        }

        int ConvertBinaryStringToInteger(string input)
        {
            int sum = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                if (input[i] == '1')
                {
                    sum += (int)Math.Pow(2, i);
                }
            }
            return sum;
        }

        string ConvertIntegerToBinaryString(int input)
        {
            string binaryString = "";

            for (int i = 0; i < 4; i++)
            {
                binaryString += (input % 2);
                input /= 2;
            }

            return binaryString;
        }

        private void recognizeButton_Click(object sender, EventArgs e)
        {
            imageRecognition = (Bitmap)pictureRecognition.Image;
            Tuple<int, int, int, int> boundary = Utilities.FindSentenceBoundary(imageRecognition);

            imageRecognition = Utilities.CropBitmap(imageRecognition, boundary.Item1 + 1, boundary.Item2 + 1, boundary.Item3 - boundary.Item1 - 1, boundary.Item4 - boundary.Item2 - 1);

            imageRecognition = Utilities.ResizeImage(imageRecognition, 10, 15);

            imageRecognition.Save("simplify3.png");

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

                    if (l == 0)
                        imageLearning.Save(i + "newImage.png");

                    Utilities.ConvertImageToBinaryMatrix(imageLearning, binaryMatrix);

                    TrainNumber(i);
                }
                E /= 10;
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
    }
}
