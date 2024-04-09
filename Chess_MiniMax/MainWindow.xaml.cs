using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Chess_MiniMax
{
    public partial class MainWindow : Window
    {
        int N_depth = 9;
        public static int[,] board = {
                                { -50, -30, -33, -90, -1000, -33, -30, -50 },
                                { -10, -10, -10, -10, -10, -10, -10, -10 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 0, 0, 0, 0, 0, 0, 0, 0 },
                                { 10, 10, 10, 10, 10, 10, 10, 10 },
                                { 50, 30, 33, 90, 1000, 33, 30, 50 }
        };

        bool WhiteP = false, WhiteK = false, WhiteQ = false, WhiteR = false, WhiteB = false, WhiteKN = false;
        double DeltaX, DeltaY;
        int Kindex1 = 7, Kindex2 = 4, top = 0, left = 0;
        int BKlocT = 0, BKlocL = 6, BBlocT = 0, BBlocL = 5, BRlocT = 0, BRlocL = 7, BKinglocT = 0, BKinglocL = 4;
        int BKlocTtemp = 0, BKlocLtemp = 6, BBlocTtemp = 0, BBlocLtemp = 5, BRlocTtemp = 0, BRlocLtemp = 7;
        bool BishopCheker = false, RookChecker = false, BishopChekersec = false, RookCheckersec = false, BishopChekertrd = false, RookCheckertrd = false, KingChecker;
        bool RandMoveChecker = false;
        public MainWindow()
        {
            InitializeComponent();
        }
        void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var mouseWasDownOn = e.Source as FrameworkElement;
            if (WhiteP)
            {
                mouseWasDownOn.Margin = new Thickness(e.GetPosition(this).X - DeltaX, e.GetPosition(this).Y - DeltaY, 0, 0);
            }
            if (WhiteK)
            {
                mouseWasDownOn.Margin = new Thickness(e.GetPosition(this).X - DeltaX, e.GetPosition(this).Y - DeltaY, 0, 0);
            }
            if (WhiteQ)
            {
                mouseWasDownOn.Margin = new Thickness(e.GetPosition(this).X - DeltaX, e.GetPosition(this).Y - DeltaY, 0, 0);
            }
            if (WhiteKN)
            {
                mouseWasDownOn.Margin = new Thickness(e.GetPosition(this).X - DeltaX, e.GetPosition(this).Y - DeltaY, 0, 0);
            }
            if (WhiteB)
            {
                mouseWasDownOn.Margin = new Thickness(e.GetPosition(this).X - DeltaX, e.GetPosition(this).Y - DeltaY, 0, 0);
            }
            if (WhiteR)
            {
                mouseWasDownOn.Margin = new Thickness(e.GetPosition(this).X - DeltaX, e.GetPosition(this).Y - DeltaY, 0, 0);
            }
        }

        void WhitePawn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                var mouseWasDownOn = e.Source as FrameworkElement;
                StackPanel.SetZIndex(mouseWasDownOn, 1);
                if (!WhiteP)
                {
                    top = (int)mouseWasDownOn.Margin.Top;
                    left = (int)mouseWasDownOn.Margin.Left;
                }
                WhiteP = true;
                DeltaX = e.GetPosition(this).X - mouseWasDownOn.Margin.Left;
                DeltaY = e.GetPosition(this).Y - mouseWasDownOn.Margin.Top;
            }
        }

        void WhitePawn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var mouseWasDownOn = e.Source as FrameworkElement;
            WhiteP = false;
            if (top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == 50 && ((left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 0 && board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] == 0) || (left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 50 && board[top / 50 - 1, left / 50 - 1] < 0) || (left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == -50 && board[top / 50 - 1, left / 50 + 1] < 0)))
            {
                for (int i = myGrid.Children.Count - 1; i >= 0; i--)
                {
                    UIElement child = myGrid.Children[i];
                    Thickness marginChild = (Thickness)child.GetValue(FrameworkElement.MarginProperty);
                    if (marginChild.Top == (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 && marginChild.Left == (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50)
                    {
                        myGrid.Children.Remove(child);
                    }
                }
                mouseWasDownOn.Margin = new Thickness((int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50, (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50, 0, 0);
                board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] = 10;
                board[top / 50, left / 50] = 0;
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                BlackMoves(board);
            }
            else if (top == 300 && top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == 100 && left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 0 && board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] == 0)
            {
                mouseWasDownOn.Margin = new Thickness((int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50, (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50, 0, 0);
                board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] = 10;
                board[top / 50, left / 50] = 0;
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                BlackMoves(board);
            }
            else
            {
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                mouseWasDownOn.Margin = new Thickness(left, top, 0, 0);
            }
        }

        void WhiteKing_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                var mouseWasDownOn = e.Source as FrameworkElement;
                StackPanel.SetZIndex(mouseWasDownOn, 1);
                if (!WhiteK)
                {
                    top = (int)mouseWasDownOn.Margin.Top;
                    left = (int)mouseWasDownOn.Margin.Left;
                }
                WhiteK = true;
                DeltaX = e.GetPosition(this).X - mouseWasDownOn.Margin.Left;
                DeltaY = e.GetPosition(this).Y - mouseWasDownOn.Margin.Top;
            }
        }

        void WhiteKing_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WhiteK = false;
            var mouseWasDownOn = e.Source as FrameworkElement;
            if ((top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == 50 || top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == -50 || top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == 0) && (left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == -50 || left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 0 || left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 50) && board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] <= 0)
            {
                for (int i = myGrid.Children.Count - 1; i >= 0; i--)
                {
                    UIElement child = myGrid.Children[i];
                    Thickness marginChild = (Thickness)child.GetValue(FrameworkElement.MarginProperty);
                    if (marginChild.Top == (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 && marginChild.Left == (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50)
                    {
                        myGrid.Children.Remove(child);
                    }
                }
                mouseWasDownOn.Margin = new Thickness((int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50, (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50, 0, 0);
                board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] = 1000;
                board[top / 50, left / 50] = 0;
                Kindex2 = left / 50;
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                BlackMoves(board);
            }
            else
            {
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                mouseWasDownOn.Margin = new Thickness(left, top, 0, 0);
            }
        }

        void WhiteQueen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                var mouseWasDownOn = e.Source as FrameworkElement;
                StackPanel.SetZIndex(mouseWasDownOn, 1);
                if (!WhiteQ)
                {
                    top = (int)mouseWasDownOn.Margin.Top;
                    left = (int)mouseWasDownOn.Margin.Left;
                }
                WhiteQ = true;
                DeltaX = e.GetPosition(this).X - mouseWasDownOn.Margin.Left;
                DeltaY = e.GetPosition(this).Y - mouseWasDownOn.Margin.Top;
            }
        }

        void WhiteQueen_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WhiteQ = false;
            var mouseWasDownOn = e.Source as FrameworkElement;
            if (!(top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == 0 && left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 0))
            {
                for (int i = myGrid.Children.Count - 1; i >= 0; i--)
                {
                    UIElement child = myGrid.Children[i];
                    Thickness marginChild = (Thickness)child.GetValue(FrameworkElement.MarginProperty);
                    if (marginChild.Top == (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 && marginChild.Left == (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50)
                    {
                        myGrid.Children.Remove(child);
                    }
                }
                mouseWasDownOn.Margin = new Thickness((int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50, (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50, 0, 0);
                board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] = 90;
                board[top / 50, left / 50] = 0;
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                BlackMoves(board);
            }
            else
            {
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                mouseWasDownOn.Margin = new Thickness(left, top, 0, 0);
            }
        }

        void WhiteRook_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                var mouseWasDownOn = e.Source as FrameworkElement;
                StackPanel.SetZIndex(mouseWasDownOn, 1);
                if (!WhiteR)
                {
                    top = (int)mouseWasDownOn.Margin.Top;
                    left = (int)mouseWasDownOn.Margin.Left;
                }
                WhiteR = true;
                DeltaX = e.GetPosition(this).X - mouseWasDownOn.Margin.Left;
                DeltaY = e.GetPosition(this).Y - mouseWasDownOn.Margin.Top;
            }
        }

        void WhiteRook_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WhiteR = false;
            var mouseWasDownOn = e.Source as FrameworkElement;
            if (!(top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == 0 && left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 0))
            {
                for (int i = myGrid.Children.Count - 1; i >= 0; i--)
                {
                    UIElement child = myGrid.Children[i];
                    Thickness marginChild = (Thickness)child.GetValue(FrameworkElement.MarginProperty);
                    if (marginChild.Top == (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 && marginChild.Left == (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50)
                    {
                        myGrid.Children.Remove(child);
                    }
                }
                mouseWasDownOn.Margin = new Thickness((int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50, (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50, 0, 0);
                board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] = 50;
                board[top / 50, left / 50] = 0;
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                BlackMoves(board);
            }
            else
            {
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                mouseWasDownOn.Margin = new Thickness(left, top, 0, 0);
            }
        }

        void WhiteKnight_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                var mouseWasDownOn = e.Source as FrameworkElement;
                StackPanel.SetZIndex(mouseWasDownOn, 1);
                if (!WhiteKN)
                {
                    top = (int)mouseWasDownOn.Margin.Top;
                    left = (int)mouseWasDownOn.Margin.Left;
                }
                WhiteKN = true;
                DeltaX = e.GetPosition(this).X - mouseWasDownOn.Margin.Left;
                DeltaY = e.GetPosition(this).Y - mouseWasDownOn.Margin.Top;
            }
        }

        void WhiteKnight_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WhiteKN = false;
            var mouseWasDownOn = e.Source as FrameworkElement;
            if (!(top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == 0 && left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 0))
            {
                for (int i = myGrid.Children.Count - 1; i >= 0; i--)
                {
                    UIElement child = myGrid.Children[i];
                    Thickness marginChild = (Thickness)child.GetValue(FrameworkElement.MarginProperty);
                    if (marginChild.Top == (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 && marginChild.Left == (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50)
                    {
                        myGrid.Children.Remove(child);
                    }
                }
                mouseWasDownOn.Margin = new Thickness((int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50, (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50, 0, 0);
                board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] = 30;
                board[top / 50, left / 50] = 0;
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                BlackMoves(board);
            }
            else
            {
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                mouseWasDownOn.Margin = new Thickness(left, top, 0, 0);
            }
        }

        void WhiteBishop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState == e.LeftButton)
            {
                var mouseWasDownOn = e.Source as FrameworkElement;
                StackPanel.SetZIndex(mouseWasDownOn, 1);
                if (!WhiteB)
                {
                    top = (int)mouseWasDownOn.Margin.Top;
                    left = (int)mouseWasDownOn.Margin.Left;
                }
                WhiteB = true;
                DeltaX = e.GetPosition(this).X - mouseWasDownOn.Margin.Left;
                DeltaY = e.GetPosition(this).Y - mouseWasDownOn.Margin.Top;
            }
        }

        void WhiteBishop_MouseUp(object sender, MouseButtonEventArgs e)
        {
            WhiteB = false;
            var mouseWasDownOn = e.Source as FrameworkElement;
            if (!(top - (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 == 0 && left - (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50 == 0))
            {
                for (int i = myGrid.Children.Count - 1; i >= 0; i--)
                {
                    UIElement child = myGrid.Children[i];
                    Thickness marginChild = (Thickness)child.GetValue(FrameworkElement.MarginProperty);
                    if (marginChild.Top == (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50 && marginChild.Left == (int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50)
                    {
                        myGrid.Children.Remove(child);
                    }
                }
                mouseWasDownOn.Margin = new Thickness((int)(mouseWasDownOn.Margin.Left + 25) / 50 * 50, (int)(mouseWasDownOn.Margin.Top + 25) / 50 * 50, 0, 0);
                board[(int)(mouseWasDownOn.Margin.Top + 25) / 50, (int)(mouseWasDownOn.Margin.Left + 25) / 50] = 33;
                board[top / 50, left / 50] = 0;
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                BlackMoves(board);
            }
            else
            {
                StackPanel.SetZIndex(mouseWasDownOn, 0);
                mouseWasDownOn.Margin = new Thickness(left, top, 0, 0);
            }
        }

 

        int depthchecker = 0;
        int tempsum = 1000000;
        int real_black_moveT = 0;
        int real_black_moveL = 0;
        UIElement real_whois = null;

        int temp_black_moveT = 0;
        int temp_black_moveL = 0;
        UIElement temp_whois = null;
        void Max_W(int[,] boardcopy, int depthchecker)
        {
            int sumboardcopy = 0;
            if (depthchecker == N_depth)
            {
                for (int f = 0; f < 8; f++)
                {
                    for (int g = 0; g < 8; g++)
                    {
                        sumboardcopy += boardcopy[f, g];
                    }
                }
                if (sumboardcopy < tempsum)
                {
                    tempsum = sumboardcopy;
                    real_black_moveT = temp_black_moveT;
                    real_black_moveL = temp_black_moveL;
                    real_whois = temp_whois;
                }
                return;
            }
            depthchecker++;

            // second part
            int max_board = -100000;
            int[,] boardcheck = (int[,])boardcopy.Clone();
            List<int[,]> max_index = new List<int[,]>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        for (int t = 0; t < 8; t++)
                        {
                            if (10 == boardcopy[k, t])
                            {
                                if (i - k == -1 && j - t == 0 && boardcopy[i, j] == 0)
                                {
                                    boardcopy = (int[,])boardcheck.Clone();
                                    boardcopy[i, j] = 10;
                                    boardcopy[k, t] = 0;
                                    int board_check = board_checker(boardcopy);
                                    if (board_check > max_board)
                                    {
                                        max_board = board_check;
                                        max_index.Clear();
                                        max_index.Add(boardcopy);
                                    }
                                    else if (board_check == max_board)
                                    {
                                        max_index.Add(boardcopy);
                                    }
                                }
                                else if (i - k == -1 && Math.Abs(j - t) == 1 && boardcopy[i, j] < 0)
                                {
                                    boardcopy = (int[,])boardcheck.Clone();
                                    boardcopy[i, j] = 10;
                                    boardcopy[k, t] = 0;
                                    int board_check = board_checker(boardcopy);
                                    if (board_check > max_board)
                                    {
                                        max_board = board_check;
                                        max_index.Clear();
                                        max_index.Add(boardcopy);
                                    }
                                    else if (board_check == max_board)
                                    {
                                        max_index.Add(boardcopy);
                                    }
                                }
                            }
                            if (33 == boardcopy[k, t])
                            {
                                if (Math.Abs(k - i) == Math.Abs(t - j) && boardcopy[i, j] <= 0)
                                {
                                    boardcopy = (int[,])boardcheck.Clone();
                                    bool bishop_checker = true;
                                    for (int l = 1; l < Math.Abs(i - k); l++)
                                    {
                                        if (i > k && j < t && board[k + l, t - l] != 0)
                                        {
                                            bishop_checker = false;
                                        }
                                        else if (i > k && j > t && board[k + l, t + l] != 0)
                                        {
                                            bishop_checker = false;
                                        }
                                        else if (i < k && j < t && board[k - l, t - l] != 0)
                                        {
                                            bishop_checker = false;
                                        }
                                        else if (i < k && j > t && board[k - l, t + l] != 0)
                                        {
                                            bishop_checker = false;
                                        }
                                    }
                                    if (bishop_checker)
                                    {
                                        boardcopy[i, j] = 33;
                                        boardcopy[k, t] = 0;
                                        int board_check = board_checker(boardcopy);
                                        if (board_check > max_board)
                                        {
                                            max_board = board_check;
                                            max_index.Clear();
                                            max_index.Add(boardcopy);
                                        }
                                        else if (board_check == max_board)
                                        {
                                            max_index.Add(boardcopy);
                                        }
                                    }
                                }
                            }
                            if (30 == boardcopy[k, t])
                            {
                                if (((Math.Abs(k - i) == 2 && Math.Abs(t - j) == 1) || (Math.Abs(k - i) == 1 && Math.Abs(t - j) == 2)) && board[i, j] <= 0)
                                {
                                    boardcopy = (int[,])boardcheck.Clone();
                                    boardcopy[i, j] = 30;
                                    boardcopy[k, t] = 0;
                                    int board_check = board_checker(boardcopy);
                                    if (board_check > max_board)
                                    {
                                        max_board = board_check;
                                        max_index.Clear();
                                        max_index.Add(boardcopy);
                                    }
                                    else if (board_check == max_board)
                                    {
                                        max_index.Add(boardcopy);
                                    }
                                }
                            }
                            if (50 == boardcopy[k, t])
                            {
                                boardcopy = (int[,])boardcheck.Clone();
                                bool rook_checker = true;
                                if ((j == t || i == k) && board[i, j] <= 0)
                                {
                                    if (j == t)
                                    {
                                        for (int l = 1; l < Math.Abs(i - k); l++)
                                        {
                                            if (i > k && board[k + l, j] != 0)
                                            {
                                                rook_checker = false;
                                            }
                                            if (i < k && board[k - l, j] != 0)
                                            {
                                                rook_checker = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int l = 1; l < Math.Abs(j - t); l++)
                                        {
                                            if (j > t && board[i, t + l] != 0)
                                            {
                                                rook_checker = false;
                                            }
                                            if (j < t && board[i, t - l] != 0)
                                            {
                                                rook_checker = false;
                                            }
                                        }
                                    }
                                    if (rook_checker)
                                    {
                                        boardcopy[i, j] = 50;
                                        boardcopy[k, t] = 0;
                                        int board_check = board_checker(boardcopy);
                                        if (board_check > max_board)
                                        {
                                            max_board = board_check;
                                            max_index.Clear();
                                            max_index.Add(boardcopy);
                                        }
                                        else if (board_check == max_board)
                                        {
                                            max_index.Add(boardcopy);
                                        }
                                    }
                                }
                            }
                            if (90 == boardcopy[k, t])
                            {
                                boardcopy = (int[,])boardcheck.Clone();
                                bool queen_checker = true;
                                if ((Math.Abs(k - i) == Math.Abs(t - j) || (j == t || i == k)) && board[i, j] <= 0)
                                {
                                    if (j == t)
                                    {
                                        for (int l = 1; l < Math.Abs(i - k); l++)
                                        {
                                            if (i > k && board[k + l, j] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            if (i < k && board[k - l, j] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                        }
                                    }
                                    else if (i == k)
                                    {
                                        for (int l = 1; l < Math.Abs(j - t); l++)
                                        {
                                            if (j > t && board[i, t + l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            if (j < t && board[i, t - l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int l = 1; l < Math.Abs(i - k); l++)
                                        {
                                            if (i > k && j < t && board[k + l, t - l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            else if (i > k && j > t && board[k + l, t + l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            else if (i < k && j < t && board[k - l, t - l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            else if (i < k && j > t && board[k - l, t + l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                        }
                                    }
                                    if (queen_checker)
                                    {
                                        boardcopy[i, j] = 90;
                                        boardcopy[k, t] = 0;
                                        int board_check = board_checker(boardcopy);
                                        if (board_check > max_board)
                                        {
                                            max_board = board_check;
                                            max_index.Clear();
                                            max_index.Add(boardcopy);
                                        }
                                        else if (board_check == max_board)
                                        {
                                            max_index.Add(boardcopy);
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }
            }
            Random rnd = new Random();
            int random = rnd.Next(0, max_index.Count);
            Mini_B(max_index[random], depthchecker);
        }

        int board_checker(int[,]temp_board)
        {
            int temp_calc = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    temp_calc += temp_board[i, j];
                }
            }
            return temp_calc;
        }

        void White_checker(int black_moveT, int black_moveL, UIElement whois)
        {
            temp_black_moveT = black_moveT;
            temp_black_moveL = black_moveL;
            temp_whois = whois;
        }

        void BlackMoves(int[,] boardcopy)
        {
            tempsum = 1000000;
            int[,] boardcheck = (int[,])boardcopy.Clone();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = myGrid.Children.Count - 1; k >= 0; k--)
                    {
                        UIElement child = myGrid.Children[k];
                        Thickness marginChild = (Thickness)child.GetValue(FrameworkElement.MarginProperty);
                        if (-10 == boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50])
                        {
                            if (i - (int)marginChild.Top / 50 == 1 && j - (int)marginChild.Left / 50 == 0 && boardcopy[i, j] == 0)
                            {
                                depthchecker = 0;
                                boardcopy = (int[,])boardcheck.Clone();
                                White_checker(i, j, child);
                                boardcopy[i, j] = -10;
                                boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50] = 0;
                                Max_W(boardcopy, depthchecker);
                            }
                            else if (i - (int)marginChild.Top / 50 == 1 && Math.Abs(j - (int)marginChild.Left / 50) == 1 && boardcopy[i, j] > 0)
                            {
                                depthchecker = 0;
                                boardcopy = (int[,])boardcheck.Clone();
                                White_checker(i, j, child);
                                boardcopy[i, j] = -10;
                                boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50] = 0;
                                Max_W(boardcopy, depthchecker);
                            }
                        }
                        if (-33 == boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50])
                        {
                            if (Math.Abs((int)marginChild.Top / 50 - i) == Math.Abs((int)marginChild.Left / 50 - j) && boardcopy[i, j] >= 0)
                            {
                                boardcopy = (int[,])boardcheck.Clone();
                                bool bishop_checker = true;
                                for (int l = 1; l < Math.Abs(i - (int)marginChild.Top / 50); l++)
                                {
                                    if (i > (int)marginChild.Top / 50 && j < (int)marginChild.Left / 50 && board[(int)marginChild.Top / 50 + l, (int)marginChild.Left / 50 - l] != 0)
                                    {
                                        bishop_checker = false;
                                    }
                                    else if (i > (int)marginChild.Top / 50 && j > (int)marginChild.Left / 50 && board[(int)marginChild.Top / 50 + l, (int)marginChild.Left / 50 + l] != 0)
                                    {
                                        bishop_checker = false;
                                    }
                                    else if (i < (int)marginChild.Top / 50 && j < (int)marginChild.Left / 50 && board[(int)marginChild.Top / 50 - l, (int)marginChild.Left / 50 - l] != 0)
                                    {
                                        bishop_checker = false;
                                    }
                                    else if (i < (int)marginChild.Top / 50 && j > (int)marginChild.Left / 50 && board[(int)marginChild.Top / 50 - l, (int)marginChild.Left / 50 + l] != 0)
                                    {
                                        bishop_checker = false;
                                    }
                                }
                                if (bishop_checker)
                                {
                                    depthchecker = 0;
                                    White_checker(i, j, child);
                                    boardcopy[i, j] = -33;
                                    boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50] = 0;
                                    Max_W(boardcopy, depthchecker);
                                }
                            }
                        }
                        if (-30 == boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50])
                        {
                            if (((Math.Abs((int)marginChild.Top / 50 - i) == 2 && Math.Abs((int)marginChild.Left / 50 - j) == 1) || (Math.Abs((int)marginChild.Top / 50 - i) == 1 && Math.Abs((int)marginChild.Left / 50 - j) == 2)) && board[i, j] >= 0)
                            {
                                depthchecker = 0;
                                boardcopy = (int[,])boardcheck.Clone();
                                White_checker(i, j, child);
                                boardcopy[i, j] = -30;
                                boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50] = 0;
                                Max_W(boardcopy, depthchecker);
                            }
                        }
                        if (-50 == boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50])
                        {
                            boardcopy = (int[,])boardcheck.Clone();
                            bool rook_checker = true;
                            if ((j == (int)marginChild.Left / 50 || i == (int)marginChild.Top / 50) && board[i, j] >= 0)
                            {
                                if (j == (int)marginChild.Left / 50)
                                {
                                    for (int l = 1; l < Math.Abs(i - (int)marginChild.Top / 50); l++)
                                    {
                                        if (i > (int)marginChild.Top / 50 && board[(int)marginChild.Top / 50 + l, j] != 0)
                                        {
                                            rook_checker = false;
                                        }
                                        if (i < (int)marginChild.Top / 50 && board[(int)marginChild.Top / 50 - l, j] != 0)
                                        {
                                            rook_checker = false;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int l = 1; l < Math.Abs(j - (int)marginChild.Left / 50); l++)
                                    {
                                        if (j > (int)marginChild.Left / 50 && board[i, (int)marginChild.Left / 50 + l] != 0)
                                        {
                                            rook_checker = false;
                                        }
                                        if (j < (int)marginChild.Left / 50 && board[i, (int)marginChild.Left / 50 - l] != 0)
                                        {
                                            rook_checker = false;
                                        }
                                    }
                                }
                                if (rook_checker)
                                {
                                    depthchecker = 0;
                                    White_checker(i, j, child);
                                    boardcopy[i, j] = -50;
                                    boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50] = 0;
                                    Max_W(boardcopy, depthchecker);
                                }
                            }
                        }
                        if (-90 == boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50])
                        {
                            boardcopy = (int[,])boardcheck.Clone();
                            bool queen_checker = true;
                            if ((Math.Abs((int)marginChild.Top / 50 - i) == Math.Abs((int)marginChild.Left / 50 - j) || (j == (int)marginChild.Left / 50 || i == (int)marginChild.Top / 50)) && board[i, j] >= 0)
                            {
                                if (j == (int)marginChild.Left / 50)
                                {
                                    for (int l = 1; l < Math.Abs(i - (int)marginChild.Top / 50); l++)
                                    {
                                        if (i > (int)marginChild.Top / 50 && board[(int)marginChild.Top / 50 + l, j] != 0)
                                        {
                                            queen_checker = false;
                                        }
                                        if (i < (int)marginChild.Top / 50 && board[(int)marginChild.Top / 50 - l, j] != 0)
                                        {
                                            queen_checker = false;
                                        }
                                    }
                                }
                                else if (i == (int)marginChild.Top / 50)
                                {
                                    for (int l = 1; l < Math.Abs(j - (int)marginChild.Left / 50); l++)
                                    {
                                        if (j > (int)marginChild.Left / 50 && board[i, (int)marginChild.Left / 50 + l] != 0)
                                        {
                                            queen_checker = false;
                                        }
                                        if (j < (int)marginChild.Left / 50 && board[i, (int)marginChild.Left / 50 - l] != 0)
                                        {
                                            queen_checker = false;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int l = 1; l < Math.Abs(i - (int)marginChild.Top / 50); l++)
                                    {
                                        if (i > (int)marginChild.Top / 50 && j < (int)marginChild.Left / 50 && board[(int)marginChild.Top / 50 + l, (int)marginChild.Left / 50 - l] != 0)
                                        {
                                            queen_checker = false;
                                        }
                                        else if (i > (int)marginChild.Top / 50 && j > (int)marginChild.Left / 50 && board[(int)marginChild.Top / 50 + l, (int)marginChild.Left / 50 + l] != 0)
                                        {
                                            queen_checker = false;
                                        }
                                        else if (i < (int)marginChild.Top / 50 && j < (int)marginChild.Left / 50 && board[(int)marginChild.Top / 50 - l, (int)marginChild.Left / 50 - l] != 0)
                                        {
                                            queen_checker = false;
                                        }
                                        else if (i < (int)marginChild.Top / 50 && j > (int)marginChild.Left / 50 && board[(int)marginChild.Top / 50 - l, (int)marginChild.Left / 50 + l] != 0)
                                        {
                                            queen_checker = false;
                                        }
                                    }
                                }
                                if (queen_checker)
                                {
                                    depthchecker = 0;
                                    White_checker(i, j, child);
                                    boardcopy[i, j] = -90;
                                    boardcopy[(int)marginChild.Top / 50, (int)marginChild.Left / 50] = 0;
                                    Max_W(boardcopy, depthchecker);
                                }
                            }
                        }
                    }
                }
            }
            exchange(); //yete arajini verjn e ara texapoxutyun
        }
        void Mini_B(int[,] boardcopy, int depthchecker)
        {
            int min_board = 10000;
            int[,] boardcheck = boardcopy = (int[,])boardcopy.Clone(); ;
            List<int[,]> min_index = new List<int[,]>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        for (int t = 0; t < 8; t++)
                        {
                            if (-10 == boardcopy[k, t])
                            {
                                if (i - k == -1 && j - t == 0 && boardcopy[i, j] == 0)
                                {
                                    boardcopy = (int[,])boardcheck.Clone();
                                    boardcopy[i, j] = -10;
                                    boardcopy[k, t] = 0;
                                    int board_check = board_checker(boardcopy);
                                    if (board_check < min_board)
                                    {
                                        min_board = board_check;
                                        min_index.Clear();
                                        min_index.Add(boardcopy);
                                    }
                                    else if (board_check == min_board)
                                    {
                                        min_index.Add(boardcopy);
                                    }
                                }
                                else if (i - k == -1 && Math.Abs(j - t) == 1 && boardcopy[i, j] > 0)
                                {
                                    boardcopy = (int[,])boardcheck.Clone();
                                    boardcopy[i, j] = -10;
                                    boardcopy[k, t] = 0;
                                    int board_check = board_checker(boardcopy);
                                    if (board_check < min_board)
                                    {
                                        min_board = board_check;
                                        min_index.Clear();
                                        min_index.Add(boardcopy);
                                    }
                                    else if (board_check == min_board)
                                    {
                                        min_index.Add(boardcopy);
                                    }
                                }
                            }
                            if (-33 == boardcopy[k, t])
                            {
                                if (Math.Abs(k - i) == Math.Abs(t - j) && boardcopy[i, j] >= 0)
                                {
                                    boardcopy = boardcopy = (int[,])boardcheck.Clone(); ;
                                    bool bishop_checker = true;
                                    for (int l = 1; l < Math.Abs(i - k); l++)
                                    {
                                        if (i > k && j < t && board[k + l, t - l] != 0)
                                        {
                                            bishop_checker = false;
                                        }
                                        else if (i > k && j > t && board[k + l, t + l] != 0)
                                        {
                                            bishop_checker = false;
                                        }
                                        else if (i < k && j < t && board[k - l, t - l] != 0)
                                        {
                                            bishop_checker = false;
                                        }
                                        else if (i < k && j > t && board[k - l, t + l] != 0)
                                        {
                                            bishop_checker = false;
                                        }
                                    }
                                    if (bishop_checker)
                                    {
                                        boardcopy[i, j] = -33;
                                        boardcopy[k, t] = 0;
                                        int board_check = board_checker(boardcopy);
                                        if (board_check < min_board)
                                        {
                                            min_board = board_check;
                                            min_index.Clear();
                                            min_index.Add(boardcopy);
                                        }
                                        else if (board_check == min_board)
                                        {
                                            min_index.Add(boardcopy);
                                        }
                                    }
                                }
                            }
                            if (-30 == boardcopy[k, t])
                            {
                                if (((Math.Abs(k - i) == 2 && Math.Abs(t - j) == 1) || (Math.Abs(k - i) == 1 && Math.Abs(t - j) == 2)) && board[i, j] >= 0)
                                {
                                    boardcopy = (int[,])boardcheck.Clone();
                                    boardcopy[i, j] = -30;
                                    boardcopy[k, t] = 0;
                                    int board_check = board_checker(boardcopy);
                                    if (board_check < min_board)
                                    {
                                        min_board = board_check;
                                        min_index.Clear();
                                        min_index.Add(boardcopy);
                                    }
                                    else if (board_check == min_board)
                                    {
                                        min_index.Add(boardcopy);
                                    }
                                }
                            }
                            if (-50 == boardcopy[k, t])
                            {
                                boardcopy = boardcopy = (int[,])boardcheck.Clone(); ;
                                bool rook_checker = true;
                                if ((j == t || i == k) && board[i, j] >= 0)
                                {
                                    if (j == t)
                                    {
                                        for (int l = 1; l < Math.Abs(i - k); l++)
                                        {
                                            if (i > k && board[k + l, j] != 0)
                                            {
                                                rook_checker = false;
                                            }
                                            if (i < k && board[k - l, j] != 0)
                                            {
                                                rook_checker = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int l = 1; l < Math.Abs(j - t); l++)
                                        {
                                            if (j > t && board[i, t + l] != 0)
                                            {
                                                rook_checker = false;
                                            }
                                            if (j < t && board[i, t - l] != 0)
                                            {
                                                rook_checker = false;
                                            }
                                        }
                                    }
                                    if (rook_checker)
                                    {
                                        boardcopy[i, j] = -50;
                                        boardcopy[k, t] = 0;
                                        int board_check = board_checker(boardcopy);
                                        if (board_check < min_board)
                                        {
                                            min_board = board_check;
                                            min_index.Clear();
                                            min_index.Add(boardcopy);
                                        }
                                        else if (board_check == min_board)
                                        {
                                            min_index.Add(boardcopy);
                                        }
                                    }
                                }
                            }
                            if (-90 == boardcopy[k, t])
                            {
                                boardcopy = (int[,])boardcheck.Clone(); ;
                                bool queen_checker = true;
                                if ((Math.Abs(k - i) == Math.Abs(t - j) || (j == t || i == k)) && board[i, j] >= 0)
                                {
                                    if (j == t)
                                    {
                                        for (int l = 1; l < Math.Abs(i - k); l++)
                                        {
                                            if (i > k && board[k + l, j] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            if (i < k && board[k - l, j] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                        }
                                    }
                                    else if (i == k)
                                    {
                                        for (int l = 1; l < Math.Abs(j - t); l++)
                                        {
                                            if (j > t && board[i, t + l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            if (j < t && board[i, t - l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int l = 1; l < Math.Abs(i - k); l++)
                                        {
                                            if (i > k && j < t && board[k + l, t - l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            else if (i > k && j > t && board[k + l, t + l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            else if (i < k && j < t && board[k - l, t - l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                            else if (i < k && j > t && board[k - l, t + l] != 0)
                                            {
                                                queen_checker = false;
                                            }
                                        }
                                    }
                                    if (queen_checker)
                                    {
                                        boardcopy[i, j] = -90;
                                        boardcopy[k, t] = 0;
                                        int board_check = board_checker(boardcopy);
                                        if (board_check < min_board)
                                        {
                                            min_board = board_check;
                                            min_index.Clear();
                                            min_index.Add(boardcopy);
                                        }
                                        else if (board_check == min_board)
                                        {
                                            min_index.Add(boardcopy);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Random rnd = new Random();
            int random = rnd.Next(0, min_index.Count);
            Max_W(min_index[random], depthchecker);
        }

        void exchange()
        {
            var changing_item = real_whois as FrameworkElement;
            Thickness marginChild = (Thickness)changing_item.GetValue(FrameworkElement.MarginProperty);
            for (int k = myGrid.Children.Count - 1; k >= 0; k--)
            {
                UIElement child = myGrid.Children[k];
                Thickness marginChildtemp = (Thickness)child.GetValue(FrameworkElement.MarginProperty);
                if ((int)marginChildtemp.Top / 50 == real_black_moveT && (int)marginChildtemp.Left / 50 == real_black_moveL)
                {
                    myGrid.Children.Remove(child);
                }
            }
            board[real_black_moveT, real_black_moveL] = board[(int)marginChild.Top / 50, (int)marginChild.Left / 50];
            board[(int)marginChild.Top / 50, (int)marginChild.Left / 50] = 0;
            changing_item.Margin = new Thickness(real_black_moveL * 50, real_black_moveT * 50, 0, 0);
        }
    }
}