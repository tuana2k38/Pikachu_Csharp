using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pikachu
{
    class GameModel
    {
        private int[, ] table;
        private int width, height;

        public GameModel(int _width, int _height, int _numOfType)
        {
            width = _width;
            height = _height;

            table = new int[height, width];

            // su dung de luu cac o da sinh ra pokemon
            HashSet<int> cellIndex = new HashSet<int>();

            // su dung de sinh so ngau nhien
            Random random = new Random();

            for (int i=0; i<width*height/2; i++)
            {
                // sinh ngau nhien 1 loai pokemon, nam trong khoang tu 1 -> _numOfType
                int typeOfPokemon = random.Next(1, _numOfType + 1);
                Console.WriteLine(i);
                // sinh ra o thu 1 chua pokemon, yeu cau la khong dc nam trong cellIndex
                int cell1 = random.Next(random.Next(0, width * height+1));
                while (cellIndex.Contains(cell1))
                    cell1 = random.Next(random.Next(0, width * height+1));
                table[cell1 / width, cell1 % width] = typeOfPokemon;
                cellIndex.Add(cell1);

                // sinh ra o thu 2 chua pokemon, yeu cau la khong dc nam trong cellIndex
                int cell2 = random.Next(random.Next(0, width * height+1));
                while (cellIndex.Contains(cell2))
                    cell2 = random.Next(random.Next(0, width * height+1));
                table[cell2 / width, cell2 % width] = typeOfPokemon;
                cellIndex.Add(cell2);

                Console.WriteLine(i);
            }    
        }
        public int getCell(int row, int col)
        {
            int _num = table[row,col];

            return _num;
        }
        public int _width
        {
            get { return this.width; }
            set { this.width = value; }

        }

        public int _height
        {
            get { return this.height; }
            set { this.height = value; }
        }
        public bool CheckWid(int row1, int col1, int row2, int col2)
        {
            if (table[row1, col1] != table[row2, col2])
                return false;
            else
            {
                if (row1 == 0 && row2 == 0)
                {
                    return true;
                }
                if (row1 == height - 1 && row2 == height - 1)
                    return true;
                if (col1 == 0 && col1 == 0)
                {
                    return true;
                }
                if (col1 == width -1 && col1 == width - 1)
                    return true;

                if (col1 < col2)
                    for (int i = col1; i < col2; i++)
                    {
                        if (table[row1, i + 1] != 0 && table[row1, i + 1] != table[row2, col2])
                            return false;
                        else if (table[row1, i + 1] == table[row2, col2])
                        {
                            table[row1, col1] = table[row2, col2] = 0;
                            return true;
                        }
                    }
                else
                    for (int i = col1; i > col2; i--)
                    {
                        if (table[row1, i - 1] != 0 && table[row1, i - 1] != table[row2, col2])
                            return false;
                        else if (table[row1, i - 1] == table[row2, col2])
                        {
                            table[row1, col1] = table[row2, col2] = 0;
                            return true;
                        }
                    }
                if (row1 == 0 && row2 == 0)
                {
                    return true;
                }
                if (row1 == height && row2 == height)
                    return true;
                if (col1 == 0 && col1 == 0)
                {
                    return true;
                }
                if (col1 == height && col1 == height)
                    return true;

                return false;
            }
                
        }
        public bool CheckHei(int row1, int col1, int row2, int col2)
        {
            if (table[row1, col1] != table[row2, col2])
                return false;
            else
            {
                if (row1 < row2)
                    for (int i = row1; i < row2; i++)
                    {
                        if (table[i + 1, col1] != 0 && table[i + 1, col1] != table[row2, col2])
                            return false;
                        else if (table[i + 1, col1] == table[row2, col2])
                        {
                            table[row1, col1] = table[row2, col2] = 0;
                            return true;
                        }
                    }
                else
                    for (int i = row1; i > row2; i--)
                    {
                        if (table[i - 1, col1] != 0 && table[i - 1, col1] != table[row2, col2])
                            return false;
                        else if (table[i - 1, col1] == table[row2, col2])
                        {
                            table[row1, col1] = table[row2, col2] = 0;
                            return true;
                        }
                    }
                return false;
            }
        }
        public bool CheckCom(int row1, int col1, int row2, int col2)
        {
            if (table[row1, col1] != table[row2, col2])
                return false;
            else
            {
                if (row1 < row2)
                {
                    if (col1 < col2)
                    {
                        /* check like as Z char */
                        for (int i = row1 + 1; i < row2; i++) 
                        {
                            if (table[i, col1] != 0)
                            {
                                return false;
                            }
                            if(table[i, col1 + 1] == 0)
                            {
                                for(int j = col1 + 1; j < col2; j++)
                                {
                                    if (table[i, j] != 0 && table[i, j + 1] != table[row2, col2])
                                        return false;
                                    if (table[i, j + 1] == table[row2, col2])
                                        return true;
                                    if (table[i + 1, j] == 0 || table[i + 1, j] == table[row2, col2])
                                    {
                                        for (int k = i + 1; k <= row2; k++)
                                        {
                                            if (table[k, j] != 0 || table[k, j] != table[row2, col2])
                                            {
                                                return false;
                                            }
                                            if (table[k, j] == table[row2, col2])
                                                return true;
                                        }
                                    }
                                }
                            }
                        }
                        /*check like as L char*/
                        for(int i = row1 + 1; i <= row2; i++)
                        {
                            if (table[i, col1] != 0)
                            {
                                return false;
                            }
                            else
                            {
                                if (table[i, col1 + 1] == table[row2, col2])
                                    return true;
                                if(table[i, col1 + 1] == 0)
                                {
                                    for(int j=col1 + 2; j <= col2; j++)
                                    {
                                        if (table[i, j] == table[row2, col2])
                                            return true;
                                        if (table[i, j] != 0)
                                            return false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = row1 + 1; i < row2; i++)
                        {
                            if (table[i, col1] != 0)
                            {
                                return false;
                            }
                            if (table[i, col1 - 1] == 0)
                            {
                                for (int j = col1 - 1; j > col2; j--)
                                {
                                    if (table[i, j] != 0)
                                        return false;
                                    if (table[i + 1, j] == 0 || table[i + 1, j] == table[row2, col2])
                                    {
                                        for (int k = i + 1; k <= row2; k++)
                                        {
                                            if (table[k, j] != 0 || table[k, j] != table[row2, col2])
                                            {
                                                return false;
                                            }
                                            if (table[k, j] == table[row2, col2])
                                                return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
        
    }
}
