using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab7._1PR
{
    public static class SlauMethod
    {
        public static double[][] resultX = new double[3][];

        public static double Determinant(double[,] matrix, int matrixSide)
        {
            if (matrixSide == 2)
            {
                return (matrix[0, 0] * matrix[1, 1] -
                  matrix[0, 1] * matrix[1, 0]);
            }
            else if (matrixSide == 1)
            {
                return matrix[0, 0];
            }
            else if (matrixSide >= 3)
            {
                double[,] MatrixForDeterminant = new double[matrixSide - 1, matrixSide - 1];
                double MatrixDeterminant = default;

                int MatrixForDeterminantRowIndex, MatrixForDeterminantColumnIndex;

                for (int RowElementsIndex = 0; RowElementsIndex < matrixSide; ++RowElementsIndex)
                {
                    MatrixForDeterminantRowIndex = default;

                    for (int RowIndex = 1; RowIndex < matrixSide; ++RowIndex)
                    {
                        MatrixForDeterminantColumnIndex = default;

                        for (int ColumnIndex = 0; ColumnIndex < matrixSide; ++ColumnIndex)
                        {
                            if (ColumnIndex != RowElementsIndex)
                            {
                                MatrixForDeterminant[MatrixForDeterminantRowIndex, MatrixForDeterminantColumnIndex] =
                                  matrix[RowIndex, ColumnIndex];
                                ++MatrixForDeterminantColumnIndex;
                            }
                        }

                        ++MatrixForDeterminantRowIndex;
                    }

                    MatrixDeterminant += Math.Pow(-1, RowElementsIndex + 2) * matrix[0, RowElementsIndex]
                      * Determinant(MatrixForDeterminant, matrixSide - 1);
                }

                return MatrixDeterminant;
            }
            else
            {
                throw new Exception("The side of a square matrix is zero! Incorrect side value!");
            }
        }

        public static double[] MethodKramer(double[,] matrixParam, double[] resSlau, int countParam)
        {
            resultX[0] = new double[countParam];

            double[,] matrix = matrixParam.Clone() as double[,];
            double[] res = resSlau.Clone() as double[];
            double[] det = new double[countParam];
            double delta = Determinant(matrix, countParam);

            if (delta == 0)
            {
                throw new Exception("У данного СЛАУ нет тривиального решения !!!");
            }

            for (int indexColumn = 0; indexColumn < countParam; ++indexColumn)
            {
                matrix = matrixParam.Clone() as double[,];

                for (int indexRow = 0; indexRow < countParam; ++indexRow)
                {
                    matrix[indexRow, indexColumn] = res[indexRow];
                }

                resultX[0][indexColumn] = Determinant(matrix, countParam) / delta;
            }

            return resultX[0];
        }

        public static double[] MethodGauss(double[,] matrixParam, double[] resSlau, int countParam)
        {
            resultX[1] = new double[countParam];

            double[,] matrix = matrixParam.Clone() as double[,];
            double[] res = resSlau.Clone() as double[];
            double[] det = new double[countParam];
            double delta = Determinant(matrix, countParam);

            if (delta == 0)
            {
                throw new Exception("У данного СЛАУ нет тривиального решения !!!");
            }

            for (int indexRow = 1; indexRow < countParam; ++indexRow)
            {
                if (matrix[indexRow - 1, indexRow - 1] != 1)
                {      // превращение опорного элемента в единицу
                    double del = matrix[indexRow - 1, indexRow - 1];

                    for (int index = 0; index < countParam; ++index)
                    {
                        matrix[indexRow - 1, index] /= del;
                    }

                    res[indexRow - 1] /= del;
                }

                for (int index = 0; index < indexRow - 1; ++index)
                {
                    double element = matrix[indexRow, index];

                    for (int indexColumn = 0; indexColumn < countParam; ++indexColumn)
                    {
                        matrix[indexRow, indexColumn] = matrix[index, indexColumn] * (-element) + matrix[indexRow, indexColumn];
                    }

                    res[indexRow] = res[index] * (-element) + res[indexRow];
                }

                double el = matrix[indexRow, indexRow - 1];

                for (int index = indexRow - 1; index < countParam; ++index)
                {
                    matrix[indexRow, index] = matrix[indexRow - 1, index] * (-el) + matrix[indexRow, index];
                }

                res[indexRow] = res[indexRow - 1] * (-el) + res[indexRow];
            }

            resultX[1][countParam - 1] = res[countParam - 1] / matrix[countParam - 1, countParam - 1];

            for (int indexRow = countParam - 2; indexRow >= 0; --indexRow)
            {
                double sum = 0;

                for (int indexRes = countParam - 1; indexRes >= indexRow + 1; --indexRes)
                {
                    sum += matrix[indexRow, indexRes] * resultX[1][indexRes];
                }

                resultX[1][indexRow] = (res[indexRow] - sum) / matrix[indexRow, indexRow];
            }

            return resultX[1];
        }

        public static double[] MethodGaussJordan(double[,] matrixParam, double[] resSlau, int countParam)
        {
            resultX[2] = new double[countParam];

            double[,] matrix = matrixParam.Clone() as double[,];
            double[] res = resSlau.Clone() as double[];
            double[] det = new double[countParam];
            double delta = Determinant(matrix, countParam);

            if (delta == 0)
            {
                throw new Exception("У данного СЛАУ нет тривиального решения !!!");
            }

            for (int indexRow = 1; indexRow < countParam; ++indexRow)
            {
                if (matrix[indexRow - 1, indexRow - 1] != 1)
                {      // превращение опорного элемента в единицу
                    double del = matrix[indexRow - 1, indexRow - 1];

                    for (int index = 0; index < countParam; ++index)
                    {
                        matrix[indexRow - 1, index] /= del;
                    }

                    res[indexRow - 1] /= del;
                }

                for (int index = 0; index < indexRow - 1; ++index)
                {
                    double element = matrix[indexRow, index];

                    for (int indexColumn = 0; indexColumn < countParam; ++indexColumn)
                    {
                        matrix[indexRow, indexColumn] = matrix[index, indexColumn] * (-element) + matrix[indexRow, indexColumn];
                    }

                    res[indexRow] = res[index] * (-element) + res[indexRow];
                }

                double el = matrix[indexRow, indexRow - 1];

                for (int index = indexRow - 1; index < countParam; ++index)
                {
                    matrix[indexRow, index] = matrix[indexRow - 1, index] * (-el) + matrix[indexRow, index];
                }

                res[indexRow] = res[indexRow - 1] * (-el) + res[indexRow];
            }

            if (matrix[countParam - 1, countParam - 1] != 1)
            {      // превращение опорного элемента в единицу
                double del = matrix[countParam - 1, countParam - 1];

                matrix[countParam - 1, countParam - 1] /= del;
                res[countParam - 1] /= del;
            }

            resultX[2][countParam - 1] = res[countParam - 1] / matrix[countParam - 1, countParam - 1];

            for (int indexRow = countParam - 2; indexRow >= 0; --indexRow)
            {
                if (matrix[indexRow + 1, indexRow + 1] != 1)
                {      // превращение опорного элемента в единицу
                    double del = matrix[indexRow + 1, indexRow + 1];

                    for (int index = 0; index < countParam; ++index)
                    {
                        matrix[indexRow + 1, index] /= del;
                    }

                    res[indexRow + 1] /= del;
                }

                for (int index = countParam - 1; index > indexRow; --index)
                {
                    double element = matrix[indexRow, index];

                    for (int indexColumn = countParam - 1; indexColumn >= 0; --indexColumn)
                    {
                        matrix[indexRow, indexColumn] = matrix[index, indexColumn] * (-element) + matrix[indexRow, indexColumn];
                    }

                    res[indexRow] = res[index] * (-element) + res[indexRow];
                }

                //double el = matrix[indexRow, indexRow + 1];

                //for (int index = countParam - 1; index >= indexRow + 1; --index) {
                //  matrix[indexRow, index] = matrix[indexRow + 1, index] * (-el) + matrix[indexRow, index];
                //}

                //res[indexRow] = res[indexRow + 1] * (-el) + res[indexRow];
            }

            for (int index = 0; index < countParam; ++index)
            {
                resultX[2][index] = res[index];
            }

            return resultX[2];
        }
    }
}