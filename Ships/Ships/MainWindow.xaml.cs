using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media.Animation;
using Ships.Entity;
using System.Linq;

namespace Ships
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button[,] SeaSpots = new Button[12, 22];
        int[] placeShips = { 4, 3, 3, 2, 2, 2, 1, 1, 1, 1 };
        int[,] eShips = new int[10, 3];
        int[,] pShips = new int[10, 3];
        bool curRot = false;
        bool firstTurn = true;
        bool turn = false;
        Random rnd = new Random();
        int randcol, randrow;
        int id = 0;
        int idE = 0;
        int temprow = 0;
        int tempcol = 0;
        int temprow2 = 0;
        int tempcol2 = 0;
        bool vertical = false;
        bool horizontal = false;
        bool shipEnded = false;
        private readonly ShipsDbContext _dbContext;

        public MainWindow()
        {
            _dbContext = new ShipsDbContext();

            InitializeComponent();

            KeyDown += MainWindow_KeyDown;

            GenerateBoard();

            if (_dbContext.LastGame.Count() > 0)
            {
                MessageBoxResult temp = MessageBox.Show("Do you want to continue", "Unfinished game", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (temp == MessageBoxResult.Yes)
                {
                    LoadBoard();
                    firstTurn = false;
                } 
                else
                {
                    _dbContext.LastGame.RemoveRange(_dbContext.LastGame);
                    _dbContext.EnemyShips.RemoveRange(_dbContext.EnemyShips);
                    _dbContext.PlayerShips.RemoveRange(_dbContext.PlayerShips);
                    _dbContext.SaveChanges();

                    GenerateEShips(4, 1);
                    GenerateEShips(3, 2);
                    GenerateEShips(2, 3);
                    GenerateEShips(1, 4);
                }
            }
            else
            {
                GenerateEShips(4, 1);
                GenerateEShips(3, 2);
                GenerateEShips(2, 3);
                GenerateEShips(1, 4);
            }
        }

        private void GenerateBoard()
        {
            for (int i = 0; i < SeaSpots.GetLength(0); i++)
                for (int j = 0; j < SeaSpots.GetLength(1); j++)
                {
                    if (j == 11 || i == 0 || i == 11 || j == 22 || j == 0)
                    {
                        SeaSpots[i, j] = new Button();
                        SeaSpots[i, j].Name = "border";
                    }
                    else
                    {
                        SeaSpots[i, j] = new Button();
                        SeaSpots[i, j].Name = "blank";
                    }

                    if (j <= 10)
                    {
                        SeaSpots[i, j].MouseEnter += MouseOverButton;
                        SeaSpots[i, j].Click += PlayerSiteClick;
                    }

                    if (j > 11)
                        SeaSpots[i, j].Click += PcSiteClick;

                    board.Children.Add(SeaSpots[i, j]);
                    Grid.SetRow(SeaSpots[i, j], i);
                    Grid.SetColumn(SeaSpots[i, j], j);
                }
        }
        private bool Sunk(string rotation, int chRow, int chCol)
        {
            int? foundId = null;
            if (!turn)
            {
                if (rotation == "ver")
                {
                    for (int i = 0; i < eShips.GetLength(0); i++)
                    {
                        if (chCol == eShips[i, 2])
                        {
                            if (chRow != eShips[i, 1])
                            {
                                int temp = eShips[i, 1];

                                for (int j = 1; j < eShips[i, 0]; j++)
                                {
                                    temp++;
                                    if (temp == chRow)
                                        foundId = i;
                                }
                            }
                            else
                            {
                                foundId = i;
                                break;
                            }
                        }
                        if (foundId != null)
                            break;
                    }

                    if (foundId != null)
                    {
                        int counter = 0;
                        if (eShips[(int)foundId, 1] + eShips[(int)foundId, 0] - 1 < 11)
                        {
                            for (int i = 0; i < eShips[(int)foundId, 0]; i++)
                                if (SeaSpots[eShips[(int)foundId, 1] + i, eShips[(int)foundId, 2]].Name == "eSunk")
                                    counter++;
                            if (counter == eShips[(int)foundId, 0])
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;  
                    }  
                }
                else if (rotation == "hor")
                {
                    for (int i = 0; i < eShips.GetLength(0); i++)
                    {
                        if (chRow == eShips[i, 1])
                        {
                            if (chCol != eShips[i, 2])
                            {
                                int temp = eShips[i, 2];

                                for (int j = 1; j < eShips[i, 0]; j++)
                                {
                                    temp++;
                                    if (temp == chCol)
                                        foundId = i;
                                }  
                            }
                            else
                            {
                                foundId = i;
                                break;
                            }
                        }
                        if (foundId != null)
                            break;
                    }

                    if (foundId != null)
                    {
                        int counter = 0;
                        if (eShips[(int)foundId, 2] + eShips[(int)foundId, 0] - 1 < 22)
                        {
                            for (int i = 0; i < eShips[(int)foundId, 0]; i++)
                                if (SeaSpots[eShips[(int)foundId, 1], eShips[(int)foundId, 2] + i].Name == "eSunk")
                                    counter++;
                            if (counter == eShips[(int)foundId, 0])
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }
                }
            }
            else
            {
                if (rotation == "ver")
                {
                    for (int i = 0; i < pShips.GetLength(0); i++)
                    {
                        if (chCol == pShips[i, 2])
                        {
                            if (chRow != pShips[i, 1])
                            {
                                int temp = pShips[i, 1];

                                for (int j = 1; j < eShips[i, 0]; j++)
                                {
                                    temp++;
                                    if (temp == chRow)
                                        foundId = i;
                                }
                            }
                            else
                            {
                                foundId = i;
                                break;
                            }
                        }
                        if (foundId != null)
                            break;
                    }

                    if (foundId != null)
                    {
                        int counter = 0;
                        if (pShips[(int)foundId, 1] + pShips[(int)foundId, 0] - 1 < 11)
                        {
                            for (int i = 0; i < pShips[(int)foundId, 0]; i++)
                                if (SeaSpots[pShips[(int)foundId, 1] + i, pShips[(int)foundId, 2]].Name == "pSunk")
                                    counter++;
                            if (counter == pShips[(int)foundId, 0])
                            {
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                            return false;
                    }

                }
                else if (rotation == "hor")
                    for (int i = 0; i < pShips.GetLength(0); i++)
                    {
                        if (chRow == pShips[i, 1])
                        {
                            if (chCol != pShips[i, 2])
                            {
                                int temp = pShips[i, 2];

                                for (int j = 1; j < eShips[i, 0]; j++)
                                {
                                    temp++;
                                    if (temp == chCol)
                                        foundId = i;
                                }
                            }
                            else
                            {
                                foundId = i;
                                break;
                            }
                        }
                        if (foundId != null)
                            break;
                    }

                if (foundId != null)
                {
                    int counter = 0;
                    if (pShips[(int)foundId, 2] + pShips[(int)foundId, 0] - 1 < 11)
                    {
                        for (int i = 0; i < pShips[(int)foundId, 0]; i++)
                            if (SeaSpots[pShips[(int)foundId, 1], pShips[(int)foundId, 2] + i].Name == "pSunk")
                                counter++;
                        if (counter == pShips[(int)foundId, 0])
                        {
                            return true;
                        }
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            
            return false;
        }
        private void randShoot()
        {
            do
            {
                randcol = rnd.Next(1, 11);
                randrow = rnd.Next(1, 11);
            } while (SeaSpots[randrow, randcol].Name == "miss" || SeaSpots[randrow, randcol].Name == "pSunk");

            if (SeaSpots[randrow, randcol].Name == "pShip")
            {
                SeaSpots[randrow, randcol].Name = "pSunk";
                if (!Sunk("ver", randrow, randcol) && !Sunk("hor", randrow, randcol))
                {
                    tempcol = randcol;
                    temprow = randrow;
                    tempcol2 = randcol;
                    temprow2 = randrow;
                }
                else
                {
                    MessageBox.Show("Player's ship sunk");
                    tempcol = 0;
                    temprow = 0;
                }
                    
            }
            else if (SeaSpots[randrow, randcol].Name == "blank")
            {
                SeaSpots[randrow, randcol].Name = "miss";
                turn = false;
            }
        }
        private void enemyMove()
        {
            if (temprow == 0 && tempcol == 0)
            {
                randShoot();
            }
            else
            {
                bool randHit = false;
                for (int i = 0; i < 3; i++)
                {
                    if (!vertical && !horizontal)
                    {
                        if (SeaSpots[temprow - 1 + i, tempcol].Name == "pShip")
                        {
                            SeaSpots[temprow - 1 + i, tempcol].Name = "pSunk";
                            temprow = temprow - 1 + i;
                            vertical = true;
                            break;
                        }
                        else if (SeaSpots[temprow - 1 + i, tempcol].Name == "blank")
                        {
                            SeaSpots[temprow - 1 + i, tempcol].Name = "miss";
                            turn = false;
                            break;
                        }

                        if (SeaSpots[temprow, tempcol - 1 + i].Name == "pShip")
                        {
                            SeaSpots[temprow, tempcol - 1 + i].Name = "pSunk";
                            tempcol = tempcol - 1 + i;
                            horizontal = true;
                            break;
                        }
                        else if (SeaSpots[temprow, tempcol - 1 + i].Name == "blank")
                        {
                            SeaSpots[temprow, tempcol - 1 + i].Name = "miss";
                            turn = false;
                            break;
                        }
                    }

                    if (vertical)
                    {
                        if (!shipEnded)
                        {
                            if (SeaSpots[temprow - 1, tempcol].Name == "pShip")
                            {
                                SeaSpots[temprow - 1, tempcol].Name = "pSunk";
                                temprow -= 1;
                                break;
                            }
                            else
                            {
                                if (SeaSpots[temprow - 1, tempcol].Name == "blank")
                                {
                                    SeaSpots[temprow - 1, tempcol].Name = "miss";
                                    shipEnded = true;
                                    turn = false;
                                    break;
                                }
                                if (SeaSpots[temprow - 1, tempcol].Name == "pSunk")
                                    temprow2 = temprow;
                                shipEnded = true;
                            }
                        }
                        else
                        {
                            if (SeaSpots[temprow2 + 1, tempcol].Name == "pShip")
                            {
                                SeaSpots[temprow2 + 1, tempcol].Name = "pSunk";
                                temprow2 += 1;
                                break;
                            }
                            else
                            {
                                if (SeaSpots[temprow2 + 1, tempcol].Name == "blank")
                                {
                                    SeaSpots[temprow2 + 1, tempcol].Name = "miss";
                                    turn = false;
                                    break;
                                }
                                if (SeaSpots[temprow2 + 1, tempcol].Name == "miss")
                                {
                                    randShoot();
                                    if (SeaSpots[temprow, tempcol].Name == "pSunk")
                                        randHit = true;
                                }
                                break;
                            }
                        }
                    }
                    if (horizontal)
                    {
                        if (!shipEnded)
                        {
                            if (SeaSpots[temprow, tempcol - 1].Name == "pShip")
                            {
                                SeaSpots[temprow, tempcol - 1].Name = "pSunk";
                                tempcol -= 1;
                                break;
                            }
                            else
                            {
                                if (SeaSpots[temprow, tempcol - 1].Name == "blank")
                                {
                                    SeaSpots[temprow, tempcol - 1].Name = "miss";
                                    turn = false;
                                    shipEnded = true;
                                    break;
                                }
                                if (SeaSpots[temprow, tempcol - 1].Name == "pSunk")
                                    tempcol2 = tempcol;
                                shipEnded = true;
                            }
                        }
                        else
                        {
                            if (SeaSpots[temprow, tempcol2 + 1].Name == "pShip")
                            {
                                SeaSpots[temprow, tempcol2 + 1].Name = "pSunk";
                                tempcol2 += 1;
                                break;
                            }
                            else
                            {
                                if (SeaSpots[temprow, tempcol2 + 1].Name == "blank")
                                {
                                    SeaSpots[temprow, tempcol2 + 1].Name = "miss";
                                    turn = false;
                                    break;
                                }
                                if (SeaSpots[temprow, tempcol2 + 1].Name == "miss")
                                {
                                    randShoot();
                                    if (SeaSpots[temprow, tempcol].Name == "pSunk")
                                        randHit = true;
                                }
                                break;
                            }
                        }
                    }
                }
                if (vertical)
                {
                    if (Sunk("ver", temprow, tempcol))
                    {
                        vertical = false;
                        shipEnded = false;
                        MessageBox.Show("Player's ship sunk");
                    }
                    if (!randHit && Sunk("ver", temprow, tempcol))
                    {
                        tempcol = 0;
                        temprow = 0;
                        vertical = false;
                        shipEnded = false;
                    }
                }
                else if (horizontal)
                {
                    if (Sunk("hor", temprow, tempcol))
                    {
                        horizontal = false;
                        shipEnded = false;
                        MessageBox.Show("Player's ship sunk");
                    }
                    if (!randHit && Sunk("hor", temprow, tempcol))
                    {
                        tempcol = 0;
                        temprow = 0;
                        horizontal = false;
                        shipEnded = false;
                    }
                }
                else 
                {
                    if (Sunk("ver", temprow, tempcol) || Sunk("hor", temprow, tempcol))
                        MessageBox.Show("Player's ship sunk");
                    if (!randHit && (Sunk("ver", temprow, tempcol) || Sunk("hor", temprow, tempcol)))
                    {
                        tempcol = 0;
                        temprow = 0;
                    }
                }
            }

            SaveBoard();

            if (gameOver(false))
            {
                turn = false;
                MessageBox.Show("Game Over, PC Wins");
                Close();
            }
        }

        private void GenerateEShips(int shipLength, int shipCount)
        {
            for (int i = 0; i < shipCount; i++)
            {
                bool shipSet = false;

                do
                {
                    randcol = rnd.Next(12, 21);
                    randrow = rnd.Next(1, 11);

                    if (SeaSpots[randrow, randcol].Name != "eShip")
                    {
                        if (freeSpace(randrow, randcol, shipLength, false, "eShip"))
                        {
                            for (int j = 0; j < shipLength; j++)
                                SeaSpots[randrow, (randcol + j)].Name = "eShip";
                            shipSet = true;
                        }

                        else if (freeSpace(randrow, randcol, shipLength, true, "eShip"))
                        {
                            for (int j = 0; j < shipLength; j++)
                                SeaSpots[(randrow + j), randcol].Name = "eShip";
                            shipSet = true;
                        }
                    }
                } while (!shipSet);

                eShips[idE, 0] = shipLength;
                eShips[idE, 1] = randrow;
                eShips[idE, 2] = randcol;

                Cords2 cords2 = new Cords2()
                {
                    Lenght = shipLength,
                    X = randrow,
                    Y = randcol
                };
                _dbContext.EnemyShips.Add(cords2);
                _dbContext.SaveChanges();
                idE++;
            }
        }

        private bool freeSpace(int row, int col, int length, bool rot, string name)
        {
            bool possibleHor = true;
            bool possibleVer = true;

            for (int i = 0; i < 3; i++)
            {
                if (col + length - 1 < 21)
                {
                    if (SeaSpots[row - 1 + i, col - 1].Name == name || SeaSpots[row - 1 + i, col + length].Name == name)
                        possibleHor = false;
                }
                else
                    possibleHor = false;

                if (row + length - 1 < 11)
                {
                    if (SeaSpots[row - 1, col - 1 + i].Name == name || SeaSpots[row + length, col - 1 + i].Name == name)
                        possibleVer = false;
                }
                else
                    possibleVer = false;
            }

            for (int i = 0; i < length; i++)
            {
                if (col + length - 1 < 21)
                {
                    if (SeaSpots[row - 1, col + i].Name == name || SeaSpots[row + 1, col + i].Name == name)
                        possibleHor = false;
                }
                else
                    possibleHor = false;

                if (row + length - 1 < 11)
                {
                    if (SeaSpots[row + i, col - 1].Name == name || SeaSpots[row + i, col + 1].Name == name)
                        possibleVer = false;
                }
                else
                    possibleVer = false;
            }

            if (!rot)
                return possibleHor;
            else 
                return possibleVer;

        }
        
        private bool gameOver(bool who)
        {
            int pShipSpotCounter = 0;
            int eShipSpotCounter = 0;

            for (int i = 0; i < SeaSpots.GetLength(0); i++)
                for (int j = 0; j < SeaSpots.GetLength(1); j++)
                {
                    if (SeaSpots[i, j].Name == "pShip")
                        pShipSpotCounter++;

                    if (SeaSpots[i, j].Name == "eShip")
                        eShipSpotCounter++;
                }

            if (pShipSpotCounter == 0 && !who)
            {
                _dbContext.LastGame.RemoveRange(_dbContext.LastGame);
                _dbContext.EnemyShips.RemoveRange(_dbContext.EnemyShips);
                _dbContext.PlayerShips.RemoveRange(_dbContext.PlayerShips);
                _dbContext.SaveChanges();
                return true;
            }
                
            if (eShipSpotCounter == 0 && who)
            {
                _dbContext.LastGame.RemoveRange(_dbContext.LastGame);
                _dbContext.EnemyShips.RemoveRange(_dbContext.EnemyShips);
                _dbContext.PlayerShips.RemoveRange(_dbContext.PlayerShips);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.R)
                if (curRot)
                    curRot = false;
                else
                    curRot = true;
        }

        private void PcSiteClick(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            int row = Grid.GetRow(btn);
            int col = Grid.GetColumn(btn);

            if (!firstTurn && !turn)
            {
                if(btn.Name != "miss" && btn.Name != "eSunk")
                {
                    if (btn.Name == "eShip")
                    {
                        btn.Name = "eSunk";
                        if(Sunk("ver", row, col) || Sunk("hor", row, col))
                            MessageBox.Show("Enemy's ship sunk");
                    }
                    else if (btn.Name == "blank")
                    {
                        btn.Name = "miss";
                        turn = true;
                    }

                    SaveBoard();

                    if (gameOver(true))
                    {
                        MessageBox.Show("Game Over, Player Wins");
                        Close();
                    } 
                        
                    while(turn)
                        enemyMove();
                }
            }
        }

        private void PlayerSiteClick(object sender, RoutedEventArgs e)
        {
            bool set = false;
            int playerSpotCounter = 0;
            if (firstTurn)
            {
                bool first = false;
                for (int i = 1; i < 11; i++)
                    for (int j = 1; j < 11; j++)
                        if (SeaSpots[i, j].Name == "over")
                        {
                            SeaSpots[i, j].Name = "pShip";
                            if (!first)
                            {
                                pShips[id, 0] = placeShips[id];
                                pShips[id, 1] = i;
                                pShips[id, 2] = j;
                                first = true;

                                Cords cords = new Cords()
                                {
                                    Lenght = placeShips[id],
                                    X = i,
                                    Y = j
                                };
                                _dbContext.PlayerShips.Add(cords);
                                _dbContext.SaveChanges();

                            }
                            set = true;
                        }
                            
                for (int i = 1; i < 11; i++)
                    for (int j = 1; j < 11; j++)
                        if (SeaSpots[i, j].Name == "pShip")
                            playerSpotCounter++;

                if (playerSpotCounter == 20)
                    firstTurn = false;

                if(set)
                    id++;
            }
        }

        private void MouseOverButton(object sender, MouseEventArgs e)
        {
            var btn = sender as Button;

            int row = Grid.GetRow(btn);
            int col = Grid.GetColumn(btn);

            for (int i = 1; i < 11; i++)
                for (int j = 0; j < 11; j++)
                    if (SeaSpots[i, j].Name == "over")
                        SeaSpots[i, j].Name = "blank";

            if (firstTurn)
            {
                if (!curRot)
                {
                    if(freeSpace(row, col, placeShips[id], false, "pShip") && btn.Name != "pShip" 
                        && col + (placeShips[id] - 1) < 11)
                        for (int i = 0; i < placeShips[id]; i++)
                            if (col + i < 11)
                                if (SeaSpots[row, (col + i)].Name != "pShip")
                                    SeaSpots[row, (col + i)].Name = "over";
                }
                else
                {
                    if (freeSpace(row, col, placeShips[id], true, "pShip") && btn.Name != "pShip" 
                        && row + (placeShips[id] - 1) < 11)
                        for (int i = 0; i < placeShips[id]; i++)
                            if (row + i < 11)
                                if (SeaSpots[(row + i), col].Name != "pShip")
                                    SeaSpots[(row + i), col].Name = "over";
                }
            }
        }

        private void SaveBoard()
        {
            if (_dbContext.LastGame.Count() > 0)
                _dbContext.LastGame.RemoveRange(_dbContext.LastGame);

            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 22; j++)
                    if (SeaSpots[i, j].Name != "border")
                    {
                        Board curBoard = new Board()
                        {
                            Name = SeaSpots[i, j].Name,
                            X = i,
                            Y = j
                        };
                        _dbContext.LastGame.Add(curBoard);
                        
                    }
            _dbContext.SaveChanges();
        }

        private void LoadBoard()
        {
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 22; j++)
                {
                    var spot = _dbContext.LastGame
                    .FirstOrDefault(x => x.X == i && x.Y == j);
                    if (spot != null)
                        SeaSpots[i, j].Name = spot.Name;
                }

            int idp = 0;

            foreach(var i in _dbContext.PlayerShips)
            {
                pShips[idp, 0] = i.Lenght;
                pShips[idp, 1] = i.X;
                pShips[idp, 2] = i.Y;

                idp++;
            }

            int ide = 0;

            foreach (var i in _dbContext.EnemyShips)
            {
                eShips[ide, 0] = i.Lenght;
                eShips[ide, 1] = i.X;
                eShips[ide, 2] = i.Y;

                ide++;
            }
        }
    }
}