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
using System.IO;

namespace PatternRecognition
{
    public partial class MainForm : Form
    {
        static int NUMBER_SAMPLES = 140;
        const int BIAS = 30;
        const int EPOCHS = 300;
        const double E_MAX = 0.001;
        const double LEARNING_RATE = 0.2;

        double EValue = 0;

        Bitmap imageRecognition;
        Bitmap[] samples;

        int[,] binaryMatrix;

        int[] neuralsInputLayer;
        double[] neuralsHiddenLayer;
        double[] neuralsOutputLayer;

        double[,] weightsInputAndHiddenLayer;
        double[,] weightsHiddenAndOutputLayer;

        double[] errorsHiddenLayer;
        double[] errorsOutputLayer;

        public MainForm()
        {
            InitializeComponent();

            binaryMatrix = new int[15, 10];

            neuralsInputLayer = new int[150];
            neuralsHiddenLayer = new double[250];
            neuralsOutputLayer = new double[4];

            weightsInputAndHiddenLayer = new double[150, 250];
            weightsHiddenAndOutputLayer = new double[250, 4];

            errorsOutputLayer = new double[4];
            errorsHiddenLayer = new double[250];

            InitializeWeights();
        }

        /// <summary>
        /// hàm sigmoid
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        double Sigmoid(double x)
        {
            return 2 / (1 + Math.Exp(-0.014 * x)) - 1;
        }

        /// <summary>
        /// đạo hàm sigmoid
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        double DerivativeSigmoid(double x)
        {
            return (1 - x * x) / 2;
        }

        /// <summary>
        /// Khởi tạo trọng số từ bộ trọng số hoặc tạo bộ mới random từ -bias đến bias
        /// </summary>
        void InitializeWeights(bool reset = false)
        {
            if (reset)
            {
                Random r = new Random();
                for (int i = 0; i < 150; i++)
                {
                    for (int j = 0; j < 250; j++)
                    {
                        weightsInputAndHiddenLayer[i, j] = r.Next(-BIAS, BIAS);
                    }
                }

                for (int i = 0; i < 250; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        weightsHiddenAndOutputLayer[i, j] = r.Next(-BIAS, BIAS);
                    }
                }
            }
            else
            {
                string[] weights = Properties.Resources.default_weight.Split('\n');
                int k = 0;
                for (int i = 0; i < 150; i++)
                    for (int j = 0; j < 250; j++)
                        weightsInputAndHiddenLayer[i, j] = double.Parse(weights[k++]);
                for (int i = 0; i < 250; i++)
                    for (int j = 0; j < 4; j++)
                        weightsHiddenAndOutputLayer[i, j] = double.Parse(weights[k++]);
            }
        }

        /// <summary>
        /// nạp từ ma trận ảnh vào tầng input
        /// </summary>
        void ImportBinaryMatrixToInputLayer()
        {
            int k = 0;

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 10; j++)
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
            for (int j = 0; j < 250; j++)
            {
                double sum = 0;
                for (int i = 0; i < 150; i++)
                {
                    sum += weightsInputAndHiddenLayer[i, j] * neuralsInputLayer[i];
                }
                neuralsHiddenLayer[j] = Sigmoid(sum);
            }
        }

        /// <summary>
        /// Nạp từ tầng hidden sang output
        /// </summary>
        void ImportHiddenToOutputLayer()
        {
            for (int j = 0; j < 4; j++)
            {
                double sum = 0;
                for (int i = 0; i < 250; i++)
                {
                    sum += weightsHiddenAndOutputLayer[i, j] * neuralsHiddenLayer[i];
                }
                neuralsOutputLayer[j] = Sigmoid(sum);
            }
        }

        /// <summary>
        /// cập nhật lỗi
        /// </summary>
        /// <param name="delta"></param>
        void SetErrors(double[] delta)
        {
            for (int i = 0; i < 4; i++)
            {
                errorsOutputLayer[i] = delta[i] * DerivativeSigmoid(neuralsOutputLayer[i]);
            }

            for (int i = 0; i < 150; i++)
            {
                for (int j = 0; j < 250; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < 4; k++)
                    {
                        sum += (weightsHiddenAndOutputLayer[j, k] * errorsOutputLayer[k]);
                    }
                    errorsHiddenLayer[j] = DerivativeSigmoid(neuralsHiddenLayer[j]) * sum;
                }
            }
        }

        /// <summary>
        /// tính lỗi trung bình
        /// </summary>
        /// <returns></returns>
        double AverageErrors()
        {
            double sum = 0;
            for (int i = 0; i < 4; i++)
            {
                sum += errorsOutputLayer[i];
            }
            return Math.Abs(sum) / 4;
        }

        /// <summary>
        /// điều chỉnh trọng số
        /// </summary>
        void FixWeights()
        {
            for (int i = 0; i < 150; i++)
            {
                for (int j = 0; j < 250; j++)
                {
                    weightsInputAndHiddenLayer[i, j] += (LEARNING_RATE * neuralsInputLayer[i] * errorsHiddenLayer[j]);
                }
            }

            for (int i = 0; i < 250; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    weightsHiddenAndOutputLayer[i, j] += (LEARNING_RATE * neuralsHiddenLayer[i] * errorsOutputLayer[j]);
                }
            }
        }

        /// <summary>
        /// train một chữ số
        /// </summary>
        /// <param name="number">số được train</param>
        void TrainNumber(int number)
        {
            ImportBinaryMatrixToInputLayer();

            ImportInputToHiddenLayer();

            ImportHiddenToOutputLayer();

            double[] delta = new double[4]; // Mảng lưu sai số

            string binaryString = ConvertIntegerToBinaryString(number);

            for (int i = 0; i < 4; i++)
            {
                delta[i] = double.Parse(binaryString[i].ToString()) - neuralsOutputLayer[i];
            }

            SetErrors(delta);

            FixWeights();

            EValue += AverageErrors();
        }

        /// <summary>
        /// chuẩn hóa tấm hình
        /// </summary>
        /// <param name="image"></param>
        Bitmap SimplifyImage(Bitmap image)
        {
            Tuple<int, int, int, int> boundary = Utilities.FindSentenceBoundary(image);
            image = Utilities.CropBitmap(image, boundary.Item1 + 1, boundary.Item2 + 1, boundary.Item3 - boundary.Item1 - 1, boundary.Item4 - boundary.Item2 - 1);
            image = Utilities.ResizeImage(image, 10, 15);

            return image;
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

        /// <summary>
        /// nhận diện một số
        /// </summary>
        int Recognize()
        {
            ImportBinaryMatrixToInputLayer();

            ImportInputToHiddenLayer();

            ImportHiddenToOutputLayer();

            string result = "";
            for (int i = 0; i < 4; i++)
                result += Threshold(neuralsOutputLayer[i]).ToString();
            int number = ConvertBinaryStringToInteger(result);

            return number;
        }

        /// <summary>
        /// chuyển đổi chuỗi nhị phân sang số nguyên
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        int ConvertBinaryStringToInteger(string input)
        {
            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '1')
                {
                    sum += (int)Math.Pow(2, input.Length - i - 1);
                }
            }
            return sum;
        }

        /// <summary>
        /// chuyển đổi số nguyên sang chuỗi nhị phân
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string ConvertIntegerToBinaryString(int input)
        {
            string binaryString = "";

            for (int i = 3; i >= 0; i--)
            {
                binaryString = (input % 2).ToString() + binaryString;
                input /= 2;
            }

            return binaryString;
        }

        private void recognizeButton_Click(object sender, EventArgs e)
        {
            learningInformlbl.Visible = false;
            Bitmap rawImage = (Bitmap)recognitionPictureBox.Image;
            List<Bitmap> bitmaps = Utilities.SegmentCharacters(rawImage);

            string result = "";
            foreach (Bitmap bitmap in bitmaps)
            {
                imageRecognition = SimplifyImage(bitmap);
                Utilities.ConvertImageToBinaryMatrix(imageRecognition, binaryMatrix);

                result += Recognize().ToString();
            }

            MessageBox.Show("This is: " + result, "Answer", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void browsingButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Open image need to recognize";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Image Files (.jpg, .png)|*.jpg;*.png";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imageRecognition = new Bitmap(openFileDialog1.FileName);
                recognitionPictureBox.Image = (Image)imageRecognition;
            }
        }

        private void learningButton_Click(object sender, EventArgs e)
        {
 
            learningInformlbl.Visible = true;
            learningInformlbl.Text = "Tranning... Wait a moment";
            Application.DoEvents();
            NUMBER_SAMPLES = (int)numericUpDown1.Value;
            for (int l = 0; l < EPOCHS; l++)
            {
                EValue = 0;
                for (int i = 0; i < NUMBER_SAMPLES; i++)
                {
                    Bitmap imageLearning = samples[i];

                    //if (l == 0)
                    //    imageLearning.Save(i + "simplify.png");

                    Utilities.ConvertImageToBinaryMatrix(imageLearning, binaryMatrix);

                    TrainNumber(i % 10);
                }
                EValue /= NUMBER_SAMPLES;
                Console.WriteLine(l.ToString() + ": " + EValue.ToString());
                if (EValue < E_MAX)
                    break;
            }
            learningInformlbl.Text = "Done!";
        }

        private void browseFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pathTextBox.Text = folderBrowserDialog1.SelectedPath;

                NUMBER_SAMPLES = (int)numericUpDown1.Value;
                samples = new Bitmap[NUMBER_SAMPLES];

                for (int i = 0; i < NUMBER_SAMPLES; i++)
                {
                    samples[i] = (Bitmap)Image.FromFile(pathTextBox.Text + "/" + i + ".png");
                    samples[i] = SimplifyImage(samples[i]);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Save weights";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Text Files (.txt)|*.txt";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                for (int i = 0; i < 150; i++)
                    for (int j = 0; j < 250; j++)
                        writer.WriteLine(weightsInputAndHiddenLayer[i, j]);
                for (int i = 0; i < 250; i++)
                    for (int j = 0; j < 4; j++)
                        writer.WriteLine(weightsHiddenAndOutputLayer[i, j]);
                writer.Close();

                learningInformlbl.Visible = true;
                learningInformlbl.Text = "Saved weights";
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Import weights for training";
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Text Files (.txt)|*.txt";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader reader = new StreamReader(openFileDialog1.FileName);
                for (int i = 0; i < 150; i++)
                    for (int j = 0; j < 250; j++)
                        weightsInputAndHiddenLayer[i, j] = double.Parse(reader.ReadLine());
                for (int i = 0; i < 250; i++)
                    for (int j = 0; j < 4; j++)
                        weightsHiddenAndOutputLayer[i, j] = double.Parse(reader.ReadLine());
                reader.Close();

                learningInformlbl.Visible = true;
                learningInformlbl.Text = "Imported weights";
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            InitializeWeights(true);
            learningInformlbl.Visible = true;
            learningInformlbl.Text = "Reseted weights";
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
