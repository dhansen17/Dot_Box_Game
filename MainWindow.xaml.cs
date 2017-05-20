using System;
using System.Collections.Generic;
using System.Linq;
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


namespace Dot_and_Box_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Many different things
        //scores and how many boxes players have
        int count_1, count_2; 
        bool found = false; // check to see if box was made in the end I didn't use this
        const double Board_Size = 10; // creating board
        const double sq_size = 30; //creating board
        List<Shape> dots = new List<Shape>(); // list of all the ellipses or dots
        static Point first_selection; // the first selection dot if we have only selected one dot
        static Ellipse first_selection_dot; // the ellipse it self that changes color if first selected
        bool is_true = false; // bool to see if we can start the game
        double[,] square = new double[20, 20]; // 2D array that contains all points and has their value set at 0
        bool player_1 = true; // player turns
        bool player_2 = false;
        bool is_square = false; // if its a square change to true
        double[,] square_h = new double[20, 20]; // horizontal 2D array
        double[,] square_v = new double[20, 20]; // Vertical 2D array
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            this.Players.Text = "Welcome to the Dot and Box game";
            this.Player_Score.Text = "Scores";
            Game_Board.Background = new SolidColorBrush(Colors.DarkCyan);
        }
        #region Begin game
        public bool begin_game_click // check to see if we have started the game then execute the build game
        {
            get { return is_true; }
            set
            {
                is_true = value;
            }
        }
      
        private void begin_game_Click(object sender, RoutedEventArgs e)
        {
            Game_Board.Background = new SolidColorBrush(Colors.White);
            //here we create the game_board
            for (int x = 0; x < Board_Size; x++)
            {

                for (int y = 0; y < Board_Size; y++)
                {
                   
                    square[x, y] = 0; // we set our 2D array values to zero here
                    //create an ellipse for every spot on the game board and add that to he cavas then we add a button click event for eack of those ellipses
                    Ellipse e_1 = new Ellipse();
                    e_1.Width = 10;
                    e_1.Height = 10;
                    Color c = new Color();
                    c.A = 255;
                    c.B = 0;
                    c.R = 255;
                    c.G = 0;
                    e_1.Fill = new SolidColorBrush(c);
                    Canvas.SetTop(e_1, y * sq_size);
                    Canvas.SetLeft(e_1, x * sq_size);
                    Game_Board.Children.Add(e_1);
                    dots.Add(e_1);
                    e_1.MouseDown += button_Click;
                    e_1.Resources.Add("position", new Point(x, y));

                }
            }
        }
        #endregion
        #region Game_Board and event handlers
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Players.Text = "";
            if (sender is Ellipse)
            {
                //for the ellipse sender create a dot then give it a position 
                Ellipse dot = sender as Ellipse;
                Point p = (Point)dot.Resources["position"];

                if (first_selection.X <= 0 && first_selection.Y <= 0) 
                    //if we have only clicked one dot give it the first selection dot so that we can click a second dot to draw the line to given the two x and y cordinates
                {
                    first_selection_dot = dot;

                    first_selection = p;
                    //change color of first selected dot to know where you are at on game board
                    Color c = new Color();
                    c.A = 255;
                    c.B = 255;
                    c.R = 0;
                    c.G = 0;
                    dot.Fill = new SolidColorBrush(c);
                    int x;
                    int y;
                    x = (int)first_selection.X;
                    y = (int)first_selection.Y;
                   //cast the points to ints to use in array
                    Console.WriteLine(square[y, x]);
                    square[y, x] = 1;
                    Console.WriteLine(square);
                 
                }
                else
                {
                    //call the funciton of Player turns so that two poeople can play
                    Players_Turn(p);
                   // player_count = ("Player 1 has" + count_1 +" player 2 has" +  count_2);
                    //this.Players.Text += player_count;
                }
            }
        }
        #endregion
        #region TextBox Change
        private void Players_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        #endregion
        #region Players Turns
        private void Players_Turn(Point p)
        {
            //it is player ones turn
            if (player_1 == true)
            {
                //create the line betweent the dots that player one selects make that line green
                Players.Text = "Player 1 please take your turn";
                Line myLine = new Line();
                myLine.Stroke = System.Windows.Media.Brushes.ForestGreen; 
                myLine.StrokeThickness = 2;
                myLine.X1 = first_selection.X * sq_size + 4;
                myLine.Y1 = first_selection.Y * sq_size + 4;
                myLine.X2 = p.X * sq_size + 4;
                myLine.Y2 = p.Y * sq_size + 4;
                //check and make sure that the player is not cheating and is only going as far as one dot away
                if ((((first_selection.X) + 1 == p.X) || ((first_selection.X - 1) == p.X)) && ((first_selection.Y == p.Y)) || ((first_selection.Y + 1 == p.Y) || (first_selection.Y - 1 == p.Y) && (first_selection.X == p.X)))
                {
                    int x1;
                    int y1;
                    x1 = (int)first_selection.X;
                    y1 = (int)first_selection.Y;
                    //add line to game board, also cast the points to ints for array
                    Game_Board.Children.Add(myLine);
                    first_selection = new Point(0, 0);
                    Color c = new Color();
                    c.A = 255;
                    c.R = 0;
                    c.B = 100;
                    c.G = 255;
                    first_selection_dot.Fill = new SolidColorBrush(c);
                    int x;
                    int y;
                    x = (int)p.X;
                    y = (int)p.Y;
                    //This is were we assign the second points to ints, and then we start to play
                    square[y, x] = 1;
                    //change the value from 0 to 1 if cliked then if the value of the first y and second y are the same we know its a horizontal line
                    if (y == y1)
                    {
                        //change the horizontal arrays values to 1
                        square_h[y, x] = 1;
                        square_h[y1, x1] = 1;
                    }
                    else
                    {
                        //else we are doing a vertical line change those values
                        square_v[y, x] = 1;
                        square_v[y1, x1] = 1;
                    }
                    //simple find function
                    square[y, x] = 1;
                    /*find(y, x, p);
                   find1(y, x, y1, x1);
                    if (found == true)
                    {
                        count_1++;
                        player_1 = true;
                    }
                    else
                    {
                        player_1 = false;
                        player_2 = true;
                    }
                    */
                    //bool find this is my final find functions the others just helped me work to here, if this is true we can allow the player to go again and it gives that player a +1 in how many squares they have
                    if(find2(y, x, y1, x1))
                    {
                        count_1++;
                        player_1 = true;
                    }
                    else
                    {
                        player_1 = false;
                        player_2 = true;
                    }
                    this.Player_Score.Text = "Player 1 has: " + count_2 +"\nPlaeyer 2 has: "+ count_1;
                }
                else
                {

                    MessageBox.Show("You can only go one circle away");
                }
            }
            else if (player_2 == true) // Player two has basicaly the same things as player 1 just with some minor color differences and since its player 2 it goes to player 2 and not 1

            {
                Players.Text = "Player 2 please take your turn";
                Line myLine = new Line();
                myLine.Stroke = System.Windows.Media.Brushes.BlueViolet;
                myLine.StrokeThickness = 2;
                myLine.X1 = first_selection.X * sq_size + 4;
                myLine.Y1 = first_selection.Y * sq_size + 4;
                myLine.X2 = p.X * sq_size + 4;
                myLine.Y2 = p.Y * sq_size + 4;
                if ((((first_selection.X) + 1 == p.X) || ((first_selection.X - 1) == p.X)) && ((first_selection.Y == p.Y)) || ((first_selection.Y + 1 == p.Y) || (first_selection.Y - 1 == p.Y) && (first_selection.X == p.X)))
                {
                    int x1;
                    int y1;
                    x1 = (int)first_selection.X;
                    y1 = (int)first_selection.Y;
                    Game_Board.Children.Add(myLine);
                    first_selection = new Point(0, 0);
                    Color c = new Color();
                    c.A = 255;
                    c.R = 255;
                    c.B = 51;
                    c.G = 255;
                    first_selection_dot.Fill = new SolidColorBrush(c);
                    int x;
                    int y;
                    x = (int)p.X;
                    y = (int)p.Y;
                   
                    square[y, x] = 1;
                    if (y == y1)
                    {
                        square_h[y, x] = 1;
                        square_h[y1, x1] = 1;
                    }
                    else
                    {
                        square_v[y, x] = 1;
                        square_v[y1, x1] = 1;
                    }
                    /* find(y, x, p);
                     find1(y, x, y1, x1);
                     if (found == true)
                     {
                         count_2++;
                         player_2 = true;
                     }
                     else
                     {
                         player_1 = true;
                         player_2 = false;
                     }
                     */
                    if (find2(y, x, y1, x1))
                    {
                        count_2++;
                        player_2 = true;
                    }
                    else
                    {
                        player_1 = true;
                        player_2 = false;
                    }
                    this.Player_Score.Text = "Player 1 has: " + count_2 + "\nPlaeyer 2 has: " + count_1;
                    // for some reason i had to switch count around cause it was outputting to the text box backwards other wise
                }
                else
                {

                    MessageBox.Show("You can only go one circle away");
                }
            }
        }

        #endregion
        #region Check to find Square
        private void find(int y, int x, Point p) // simple find this only checks to see if there was four consecutive clicks on different circles
        {
            //checks to see if the value of the points are 1 and if they are continue
            if ((square[y - 1, x] == 1) && (square[y-1, x - 1] == 1) && (square[y, x-1] == 1)) 
            {
                int x_1;
                int y_1;
                x_1 = (int)p.X;
                y_1 = (int)p.Y;
                Console.WriteLine("x, y {0}, {1}", x_1, y_1);
                //so far so good lets check the last point in our nested if and if this is true than we have a square yay
                if (square[y_1, x_1] == 1)
                {
                    is_square = true;
                    Rectangle rex = new Rectangle(); // tyring to color the square made with this idea, didn't work like I thought it would but perhaps can make it with time
                    Canvas.SetLeft(rex, x * sq_size);
                    Canvas.SetTop(rex, y * sq_size);
                    Console.WriteLine("Square is made");
                    found = true;
                }
               // Console.WriteLine("Square is made but... ", square[y_1, x_1]);
            }
            // all other else ifs are to check for direction of the dots that have been clicked trying to find a square
          else  if ((square[y + 1, x] == 1) && (square[y + 1, x + 1] == 1) && (square[y, x + 1] == 1))   
            {
                int x_1;
                int y_1;
                x_1 = (int)p.X;
                y_1 = (int)p.Y;
                Console.WriteLine("x, y {0}, {1}", x_1, y_1);
                if (square[y_1, x_1] == 1)
                {
                    is_square = true;
                    Rectangle rex = new Rectangle();
                    Canvas.SetLeft(rex, x * sq_size);
                    Canvas.SetTop(rex, y * sq_size);
                    Console.WriteLine("Square is made");
                    found = true;
                }
                // Console.WriteLine("Square is made but... ", square[y_1, x_1]);
            }
           else if ((square[y - 1, x] == 1) && (square[y - 1, x + 1] == 1) && (square[y, x + 1] == 1))
            {
                int x_1;
                int y_1;
                x_1 = (int)p.X;
                y_1 = (int)p.Y;
                Console.WriteLine("x, y {0}, {1}", x_1, y_1);
                if (square[y_1, x_1] == 1)
                {
                    is_square = true;
                    Rectangle rex = new Rectangle();
                    Canvas.SetLeft(rex, x * sq_size);
                    Canvas.SetTop(rex, y * sq_size);
                    Console.WriteLine("Square is made");
                    found = true;
                }
                // Console.WriteLine("Square is made but... ", square[y_1, x_1]);
            }
           else if ((square[y + 1, x] == 1) && (square[y + 1, x - 1] == 1) && (square[y, x - 1] == 1))
            {
                int x_1;
                int y_1;
                x_1 = (int)p.X;
                y_1 = (int)p.Y;
                Console.WriteLine("x, y {0}, {1}", x_1, y_1);
                if (square[y_1, x_1] == 1)
                {
                    is_square = true;
                    Rectangle rex = new Rectangle();
                    Canvas.SetLeft(rex, x * sq_size);
                    Canvas.SetTop(rex, y * sq_size);
                    Console.WriteLine("Square is made");
                    found = true;
                }
                // Console.WriteLine("Square is made but... ", square[y_1, x_1]);
            }

        }
    
        private void find1(int y, int x, int y1, int x1) // this is my complex square finder
        {
            Console.WriteLine("{0}, {1}", y, y1);
            // if the first point's y value is the same as second its a horizontal line
            if (y == y1)
            {
                Console.WriteLine("Horizonatal wall");
                if ((square_h[y1 + 1, x1] == 1) && (square_h[y + 1, x] == 1))
                    // ok here we go, so check the horizontal location and then see if you can find another horizontal line under it
                {
                    Console.WriteLine("Found horizontal lower line");
                    //sweet we found a lower line, now I want to check if there are two vertical lines as well that would complete the square
                    if ((square_v[y1, x1] == 1) && (square_v[y1 + 1, x1] == 1) && (square_v[y, x] == 1) && (square_v[y + 1, x] == 1))
                    {
                        Console.WriteLine("FOUND SQUARE");

                    }
                }
                //do the same thing as we did with the lower horizontal just with an upper one now
                else if ((square_h[y1 - 1, x1] == 1) && (square_h[y - 1, x] == 1))
                {
                    Console.WriteLine("Found horizontal upper line");
                    if ((square_v[y1, x1] == 1) && (square_v[y1 - 1, x1] == 1) && (square_v[y, x] == 1) && (square_v[y - 1, x] == 1))
                    {
                        Console.WriteLine("FOUND SQUARE");

                    }
                }
            }
            else {
                //if its not a horizontal its vertical 
                Console.WriteLine("Vertical wall");
                //we have a vertical line, check if there is another one on the right of this one
                if ((square_v[y1, x1 + 1] == 1) && (square_v[y, x + 1] == 1))
                {
                    //if we found a right line check and see if there is two horizontal ones as well and if there are we have a square
                    Console.WriteLine("Found vertical right line");
                    if ((square_h[y1, x1] == 1) && (square_h[y1, x1 + 1] == 1) && (square_h[y, x] == 1) && (square_h[y, x + 1] == 1))
                    {
                        Console.WriteLine("FOUND SQUARE");

                    }
                }
                //do the same things as the first vertical wall, now just for the left
                else if ((square_v[y1, x1 - 1] == 1) && (square_v[y, x - 1] == 1))
                {
                    Console.WriteLine("Found vertical left line");
                    if ((square_h[y1, x1] == 1) && (square_h[y1, x1 - 1] == 1) && (square_h[y, x] == 1) && (square_h[y, x - 1] == 1))
                    {
                        Console.WriteLine("FOUND SQUARE");

                    }
                }
            }
        }

        //This last find function is the same as the complex its just a bool that way I can see if its true we have a square or not, if it is true then tell me and i can give that player a score value.
        private bool find2(int y, int x, int y1, int x1) 
        {
            Console.WriteLine("{0}, {1}", y, y1);
            if (y == y1)
            {
                Console.WriteLine("Horizonatal wall");
                if ((square_h[y1 + 1, x1] == 1) && (square_h[y + 1, x] == 1))
                {
                    Console.WriteLine("Found horizontal lower line");
                    if ((square_v[y1, x1] == 1) && (square_v[y1 + 1, x1] == 1) && (square_v[y, x] == 1) && (square_v[y + 1, x] == 1))
                    {
                        Console.WriteLine("FOUND SQUARE");
                        return true;
                    }
                }
                else if ((square_h[y1 - 1, x1] == 1) && (square_h[y - 1, x] == 1))
                {
                    Console.WriteLine("Found horizontal upper line");
                    if ((square_v[y1, x1] == 1) && (square_v[y1 - 1, x1] == 1) && (square_v[y, x] == 1) && (square_v[y - 1, x] == 1))
                    {
                        Console.WriteLine("FOUND SQUARE");
                        return true;
                    }
                }
            }
            else {
                Console.WriteLine("Vertical wall");
                if ((square_v[y1, x1 + 1] == 1) && (square_v[y, x + 1] == 1))
                {
                    Console.WriteLine("Found vertical right line");
                    if ((square_h[y1, x1] == 1) && (square_h[y1, x1 + 1] == 1) && (square_h[y, x] == 1) && (square_h[y, x + 1] == 1))
                    {
                        Console.WriteLine("FOUND SQUARE");
                        return true;
                    }
                }
                else if ((square_v[y1, x1 - 1] == 1) && (square_v[y, x - 1] == 1))
                {
                    Console.WriteLine("Found vertical left line");
                    if ((square_h[y1, x1] == 1) && (square_h[y1, x1 - 1] == 1) && (square_h[y, x] == 1) && (square_h[y, x - 1] == 1))
                    {
                        Console.WriteLine("FOUND SQUARE");
                        return true;
                    }

                }

            }
            return false;
        }
        #endregion
    }
}

