using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ia_morpion
{
    class AssociationCoupScore
    {
        public string[,] matrix = new string[12, 12];
        public int score = 0;

        public AssociationCoupScore(int score) { this.score = score; }
        public AssociationCoupScore(string[,] matrix, int score)
        {
            this.matrix = matrix;
            this.score = score;
        }
        //public int Score { get; set; }

        public override string ToString()
        {
            return Program.PrintMatrix(this.matrix) + "     " + this.score;
        }
    }
}
