using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ia_morpion
{
    class Program
    {
        static int Win(string[,] matrix) //Fitness
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    bool sameCln = true;
                    bool sameLgn = true;
                    if (j < matrix.GetLength(1) - 3)
                    {
                        for (int k = j; k < j + 3; k++)
                        {
                            if (matrix[i, k] != matrix[i, k + 1]) { sameLgn = false; } //Parcours la matrice en ligne pour voir si une ligne gagnante existe

                        }

                        if (sameLgn == true && matrix[i, j] == "X") { /*Console.WriteLine("Same line : " + i + ", " + j);*/ return 1; }
                        else if (sameLgn == true && matrix[i, j] == "O") { /*Console.WriteLine("Same line : " + i + ", " + j);*/ return -1; }
                    }

                    if (i < matrix.GetLength(0) - 3)
                    {
                        for (int k = i; k < i + 3; k++)
                        {
                            if (k < matrix.GetLength(0) - 1 && matrix[k, j] != matrix[k + 1, j]) { sameCln = false; } //Parcours la matrice en colonne pour voir si une colonne gagnante existe
                        }

                        if (sameCln == true && matrix[i, j] == "X") { Console.WriteLine("Same column : " + i + ", " + j); return 1; }
                        else if (sameCln == true && matrix[i, j] == "O") { Console.WriteLine("Same column : " + i + ", " + j); return -1; }
                    }

                    if (i < matrix.GetLength(0) - 3)
                    {
                        if (j < matrix.GetLength(1) - 3)
                        {
                            bool sameDiaLft = true;
                            for (int k = 0; k < 4; k++)
                            {
                                if (matrix[i, j] != matrix[i + k, j + k]) { sameDiaLft = false; }
                            }
                            if (sameDiaLft == true && matrix[i, j] == "X") { /*Console.WriteLine("Same left diag : " + i + ", " + j);*/ return 1; }
                            else if (sameDiaLft == true && matrix[i, j] == "O") { /*Console.WriteLine("Same left diag : " + i + ", " + j);*/ return -1; }
                        }
                        if (j > 2)
                        {
                            bool sameDiaRgt = true;
                            for (int k = 0; k < 4; k++)
                            {
                                if (matrix[i, j] != matrix[i + k, j - k]) { sameDiaRgt = false; }
                            }
                            if (sameDiaRgt == true && matrix[i, j] == "X") { /*Console.WriteLine("Same right diag : " + i + ", " + j);*/ return 1; }
                            else if (sameDiaRgt == true && matrix[i, j] == "O") { /*Console.WriteLine("Same right diag : " + i + ", " + j);*/ return -1; }
                        }
                    }
                }
            }
            return 0;
        }

        static int WinCondition(string[,] matrix)
        {
            if (Win(matrix) != 0 || IsMatrixFull(matrix))
            {
                return Win(matrix) * 1000;
            }
            else
            {
                return CheckAround(matrix);
            }
        }
        static int CheckAround(string[,] matrix)
        {
            int sum = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    int compteurX = 0;
                    int compteurO = 0;

                    if (i - 2 > -1 && i + 2 < matrix.GetLength(0))
                    {
                        for (int k = i - 2; k < i + 3; k++)
                        {
                            if (matrix[k, j] == "X") compteurX++;
                            if (matrix[k, j] == "O") compteurO++;
                        }
                    }
                    if (j - 2 > -1 && j + 2 < matrix.GetLength(1))
                    {
                        for (int k = j - 2; k < j + 3; k++)
                        {
                            if (matrix[i, k] == "X") compteurX++;
                            if (matrix[i, k] == "O") compteurO++;
                        }
                    }
                    if (i - 2 > -1 && i + 2 < matrix.GetLength(0) && j - 2 > -1 && j + 2 < matrix.GetLength(1))
                    {
                        for (int k = 1; k < 3; k++)
                        {
                            if (matrix[i - 1, j - k] == "X") compteurX++;
                            if (matrix[i - 1, j - k] == "O") compteurO++;
                            if (matrix[i - 2, j - k] == "X") compteurX++;
                            if (matrix[i - 2, j - k] == "O") compteurO++;

                            if (matrix[i + 1, j + k] == "X") compteurX++;
                            if (matrix[i + 1, j + k] == "O") compteurO++;
                            if (matrix[i + 2, j + k] == "X") compteurX++;
                            if (matrix[i + 2, j + k] == "O") compteurO++;

                            if (matrix[i + 1, j - k] == "X") compteurX++;
                            if (matrix[i + 1, j - k] == "O") compteurO++;
                            if (matrix[i + 2, j - k] == "X") compteurX++;
                            if (matrix[i + 2, j - k] == "O") compteurO++;

                            if (matrix[i - 1, j + k] == "X") compteurX++;
                            if (matrix[i - 1, j + k] == "O") compteurO++;
                            if (matrix[i - 2, j + k] == "X") compteurX++;
                            if (matrix[i - 2, j + k] == "O") compteurO++;
                        }
                    }
                    if (compteurX > compteurO) sum += 10 + compteurX * 2;
                    if (compteurX < compteurO) sum -= 10 + compteurO * 3;
                    if (compteurX == compteurO) sum -= 5;
                }
            }

            return sum;
        }
        static string[,] SameMatrix(string[,] matrix)
        {
            string[,] same = new string[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    same[i, j] = matrix[i, j];
                }
            }
            return same;
        }
        static bool IsMatrixFull(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != "X" && matrix[i, j] != "O") { return false; }
                }
            }
            return true;
        }
        public static string PrintMatrix(string[,] matrix)
        {
            string str = "1 2 3 4 5 6 7 8 9 10 11 12\n__________________________\n";
            int count = 1;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                str += "|";
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    str += matrix[i, j] + " ";
                }
                str += "|" + count;
                count++;
                str += "\n";
            }
            str += "___________________________";
            return str;
        }
        static List<string[,]> NextGeneration(string[,] matrix, string cara)
        {
            List<string[,]> generationList = new List<string[,]> { };
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != "X" && matrix[i, j] != "O")
                    {
                        matrix[i, j] = cara;
                        generationList.Add(SameMatrix(matrix));
                        matrix[i, j] = " ";
                    }
                }
            }

            return generationList;
        }
        static AssociationCoupScore Minimax(string[,] matrix, int profondeur, int alpha, int beta, bool tourIA)
        {
            if (profondeur == 0 || IsMatrixFull(matrix) || Math.Abs(WinCondition(matrix)) == 1000) { return new AssociationCoupScore(matrix, WinCondition(matrix)); }

            if (tourIA)
            {
                AssociationCoupScore coup = new AssociationCoupScore(new string[,] { }, int.MinValue);
                List<string[,]> generationList = NextGeneration(matrix, "X");
                foreach (string[,] mat in generationList)
                {
                    AssociationCoupScore round = Minimax(mat, profondeur - 1, alpha, beta, false);
                    if (round.score > coup.score)
                    {
                        coup.score = round.score;
                        coup.matrix = mat;
                    }
                    alpha = Math.Max(alpha, round.score);
                    if (beta <= alpha) { break; }
                }
                return coup;
            }

            else
            {
                AssociationCoupScore coup = new AssociationCoupScore(new string[,] { }, int.MaxValue);
                List<string[,]> generationList = NextGeneration(matrix, "O");
                foreach (string[,] mat in generationList)
                {
                    AssociationCoupScore round = Minimax(mat, profondeur - 1, alpha, beta, true);
                    if (round.score < coup.score)
                    {
                        coup.score = round.score;
                        coup.matrix = mat;
                    }
                    beta = Math.Min(beta, round.score);
                    if (beta <= alpha) { break; }
                }
                return coup;
            }
        }
        static void Main(string[] args)
        {
            string[,] matrix = new string[12, 12];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = " ";
                }
            }
            Console.WriteLine(PrintMatrix(matrix));
            while (!IsMatrixFull(matrix) && Math.Abs(WinCondition(matrix)) != 1000)
            {
                Console.WriteLine("colonne (entre 1 et 12)");
                int.TryParse(Console.ReadLine(), out int column);
                Console.WriteLine("ligne (entre 1 et 12)");
                int.TryParse(Console.ReadLine(), out int line);
                if (matrix[line - 1, column - 1] == " ")
                {
                    matrix[line - 1, column - 1] = "O";

                    Console.WriteLine(PrintMatrix(matrix));

                    AssociationCoupScore coup = Minimax(matrix, 4, -2, 2, true);
                    Console.WriteLine(coup.score);

                    matrix = coup.matrix;

                    Console.WriteLine(PrintMatrix(matrix));
                }
                else
                {
                    Console.WriteLine("Entrée incorrecte");
                }
            }


        }
    }
}
