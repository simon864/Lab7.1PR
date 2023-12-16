using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7._1PR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxes.Add(textBox1);
            textBoxes.Add(textBox2);

            labelXs.Add(label1);

            labelEquals.Add(label2);

            panel = panel1;
            panel1.SendToBack();

            this.Width = 1380;
            this.Height = 530;

            panel2.AutoScroll = true;
            panel3.AutoScroll = true;
            panel4.AutoScroll = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        List<TextBox> textBoxes = new List<TextBox>();
        List<TextBox> textBoxesResult = new List<TextBox>();
        List<Label> labelXs = new List<Label>();
        List<Label> labelPluses = new List<Label>();
        List<Label> labelEquals = new List<Label>();
        List<Label> labelResult = new List<Label>();
        Panel panel;

        int countX;

        double[,] matrix;
        double[] resultSlau;

        private void button1_Click(object sender, EventArgs e)
        {
            countX = int.Parse(textBoxN.Text);

            DeleteElement();

            if (countX == 0)
            {
                return;
            }

            panel = new Panel
            {
                Location = new Point(25, 12),
                Size = new Size(1100, 300),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true
            };

            panel.SendToBack();
            Controls.Add(panel);

            for (int indexLine = 0; indexLine < countX; ++indexLine)
            {
                for (int indexElement = 0; indexElement < countX; ++indexElement)
                {
                    TextBox textBox = new TextBox
                    {
                        Width = 38,
                        Height = 22,
                        Location = new Point
                        {
                            X = 28 + 80 * indexElement,
                            Y = 21 + 40 * indexLine
                        }
                    };

                    textBox.BringToFront();
                    textBoxes.Add(textBox);
                    panel.Controls.Add(textBox);

                    Label labelX = new Label
                    {
                        Text = "x" + $"{indexElement + 1}",
                        Location = new Point
                        {
                            X = 69 + 80 * indexElement,
                            Y = 27 + 40 * indexLine
                        },
                        AutoSize = true
                    };

                    labelX.BringToFront();
                    labelXs.Add(labelX);
                    panel.Controls.Add(labelX);

                    if (indexElement == countX - 1)
                    {
                        Label labelEqual = new Label
                        {
                            Text = "=",
                            Location = new Point
                            {
                                X = labelX.Location.X + labelX.Width + 3,
                                Y = 27 + 40 * indexLine
                            },
                            AutoSize = true
                        };

                        labelEqual.BringToFront();
                        labelEquals.Add(labelEqual);
                        panel.Controls.Add(labelEqual);

                        TextBox textBoxRes = new TextBox
                        {
                            Width = 38,
                            Height = 22,
                            Location = new Point
                            {
                                X = labelEqual.Location.X + labelEqual.Width + 5,
                                Y = 21 + 40 * indexLine
                            },
                            Parent = panel1
                        };

                        textBoxRes.BringToFront();
                        textBoxesResult.Add(textBoxRes);
                        panel.Controls.Add(textBoxRes);
                    }
                    else
                    {
                        Label labelPlus = new Label
                        {
                            Text = "+",
                            Location = new Point
                            {
                                X = labelX.Location.X + labelX.Width + 3,
                                Y = 27 + 40 * indexLine
                            },
                            AutoSize = true
                        };

                        labelPlus.BringToFront();
                        labelPluses.Add(labelPlus);
                        panel.Controls.Add(labelPlus);
                    }
                }
            }
        }

        private void DeleteElement()
        {
            Controls.Remove(panel);

            for (int indexTextBox = 0; indexTextBox < textBoxes.Count; ++indexTextBox)
            {
                textBoxes[indexTextBox].Visible = false;
                Controls.Remove(textBoxes[indexTextBox]);
            }

            textBoxes.Clear();

            for (int indexLabelX = 0; indexLabelX < labelXs.Count; ++indexLabelX)
            {
                labelXs[indexLabelX].Visible = false;
                Controls.Remove(labelXs[indexLabelX]);
            }

            labelXs.Clear();

            for (int indexLabelPlus = 0; indexLabelPlus < labelPluses.Count; ++indexLabelPlus)
            {
                labelPluses[indexLabelPlus].Visible = false;
                Controls.Remove(labelPluses[indexLabelPlus]);
            }

            labelPluses.Clear();

            for (int indexLabelEqual = 0; indexLabelEqual < labelEquals.Count; ++indexLabelEqual)
            {
                labelEquals[indexLabelEqual].Visible = false;
                Controls.Remove(labelEquals[indexLabelEqual]);
            }

            labelEquals.Clear();

            Controls.Remove(panel);
            panel2.Controls.Clear();
            panel3.Controls.Clear();
            panel4.Controls.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DeleteElement();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int left = int.Parse(textBoxLeft.Text);
            int right = int.Parse(textBoxRight.Text);

            Random random = new Random(Guid.NewGuid().GetHashCode());

            for (int indexElement = 0; indexElement < textBoxes.Count; ++indexElement)
            {
                textBoxes[indexElement].Text = (random.Next(left, right) + random.NextDouble()).ToString();
            }

            for (int indexElement = 0; indexElement < textBoxesResult.Count; ++indexElement)
            {
                textBoxesResult[indexElement].Text = (random.Next(left, right) + random.NextDouble()).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TextBoxesToDouble();

            if (checkBox1.Checked)
            {
                double[] resultKramer = SlauMethod.MethodKramer(matrix, resultSlau, countX);
                PrintResult(resultKramer, panel2);
            }
            if (checkBox2.Checked)
            {
                double[] resultGauss = SlauMethod.MethodGauss(matrix, resultSlau, countX);
                PrintResult(resultGauss, panel3);
            }
            if (checkBox3.Checked)
            {
                double[] resultGaussJordan = SlauMethod.MethodGaussJordan(matrix, resultSlau, countX);
                PrintResult(resultGaussJordan, panel4);
            }
        }

        private void PrintResult(double[] result, Panel panelResult)
        {
            for (int index = 0; index < result.Length; ++index)
            {
                Label label = new Label()
                {
                    Text = "x" + $"{index + 1} = {result[index]}",
                    Location = new Point
                    {
                        X = 25,
                        Y = 10 + 20 * index
                    },
                    AutoSize = true
                };

                label.BringToFront();
                labelResult.Add(label);
                panelResult.Controls.Add(label);
            }
        }

        private void TextBoxesToDouble()
        {
            matrix = new double[countX, countX];

            int newArrIndex = default;
            for (int indexRow = 0; indexRow < matrix.GetLength(0); ++indexRow)
            {
                for (int indexColumn = 0; indexColumn < matrix.GetLength(1); ++indexColumn)
                {
                    matrix[indexRow, indexColumn] = double.Parse(textBoxes[newArrIndex].Text);
                    ++newArrIndex;

                    if (newArrIndex >= countX * countX)
                    {
                        break;
                    }
                }

                if (newArrIndex >= countX * countX)
                {
                    break;
                }
            }

            resultSlau = new double[countX];

            for (int index = 0; index < textBoxesResult.Count; ++index)
            {
                resultSlau[index] = double.Parse(textBoxesResult[index].Text);
            }
        }

        private void textBoxN_TextChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
    }
}

        