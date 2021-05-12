using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class NotationConverter
    {
        private static char[] horizontal = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
        private static char[] vertical = { '8', '7', '6', '5', '4', '3', '2', '1' };
        private static string horizontalBack = "abcdefgh";
        private static string verticalBack = "87654321";

        private static char[] bfigure = { 'p', 'n', 'b', 'r', 'q', 'k' };
        private static char[] wfigure = { 'P', 'N', 'B', 'R', 'Q', 'K' };
        public static string ConvertToUCI(int fx, int fY, int sx, int sy)
        {
            return string.Empty + horizontal[fx] + vertical[fY] + horizontal[sx] + vertical[sy];
        }
        public static string ConvertToUCI(int fx, int fY, int sx, int sy, TypeFigure promotion, int team)
        {
            if (team == 0)
                return ConvertToUCI(fx, fY, sx, sy) + wfigure[(int)promotion];
            else
                return ConvertToUCI(fx, fY, sx, sy) + bfigure[(int)promotion];
            
        }
        public static int[] ConvertFromUCI(string move)
        {
            int[] indexes = new int[4];
            indexes[0] = horizontalBack.IndexOf(move[0]);
            indexes[1] = verticalBack.IndexOf(move[1]);
            indexes[2] = horizontalBack.IndexOf(move[2]);
            indexes[3] = verticalBack.IndexOf(move[3]);
            return indexes;
        }
    }
}
