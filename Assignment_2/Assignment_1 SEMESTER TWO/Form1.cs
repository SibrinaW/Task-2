using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment_1_SEMESTER_TWO
{
    public partial class Form1 : Form
    {
        int timeMs, timeSec, timeMin; //variables for the time controler. Stopwatch.
        bool isActive;
        string[,] arrayMap = new string[20, 20];
        GameEngine MainGame;
        //Function to generate the map
        public void generateMap()
        {

            

            //Belowe line clears the map on the grid
            this.Grid.Controls.Clear();

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Button btn = new Button();
                    arrayMap[i, j] = "E";
                    /*
                * I got the Melee unit image from the site belowe 
               https://thecodexonline.com/cropped-1157b6fd85f1ff2-1-jpg/
               */
                    
                    btn.BackgroundImage = Assignment_1_SEMESTER_TWO.Properties.Resources.background; // Nice bit of code to fill my buttons with my background image
                    btn.Height = 65;
                    btn.Width = 65;
                    this.Grid.Controls.Add(btn);
                    
                }
            }
        }

        //Function to randomly place Units in Map
        public void generateUnits()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            Random rnd2 = new Random(Guid.NewGuid().GetHashCode());
            int i = rand.Next(0,19);
            int j = rnd2.Next(0, 19);
            Unit unit;

            int unitSelect = rand.Next(0, 2);

            if (unitSelect == 0)
            {
                unit = new MeleeUnit();
                arrayMap[i,j] = "M";
                /*
                * I got the Melee unit image from the site belowe 
               https://gameartpartners.com/chibi-collection/
               */
                unit.BackgroundImage = Assignment_1_SEMESTER_TWO.Properties.Resources.Melee; // Another bit op code for when unit is placed use allocated image
                unit.Click += new EventHandler(MeleeUnit_Click);
            }

            else
            {
                unit = new RangedUnit();
                arrayMap[i, j] = "R";
                /*
                 * I got the Range unit image from the site belowe 
                https://gameartpartners.com/downloads/evil-bot/
                */
                unit.BackgroundImage = Assignment_1_SEMESTER_TWO.Properties.Resources.Range; // Another bit op code for when unit is placed use allocated image
                unit.Click += new EventHandler(RangedUnit_Click);
            }

            unit.Height = 65;
            unit.Width = 65;
            unit.AutoSizeMode = AutoSizeMode; //Tried to fit picture to buttons

            //Random position
            int x = rand.Next(1, 20);
            int y = rand.Next(1, 20);
            Grid.Controls.Add((unit), x, y);

           
            
            
        }

        //Code to gererate the Factory Buildings
        public void generateFactoryBuilding()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            FactoryBuilding FB = new FactoryBuilding();
            Building building;
            /*
            arrMap[x, y] = "FB";
            FB.pos = x;
            FB.YPos = y;
            FB.Fraction = " 1";
            */

            /*int buildingSelect = rand.Next(0, 2);

            if (buildingSelect == 0 )
               {*/
            building = new Building();
            /*
             * I got the Factory building image from the site belowe 
            https://twitter.com/dokodonmystery
            */
            building.BackgroundImage = Assignment_1_SEMESTER_TWO.Properties.Resources.FactoryBuilding;
            building.Click += new EventHandler(FactoryBuilding_Click);
            //}

            building.Height = 65;
            building.Width = 65;
            building.AutoSizeMode = AutoSizeMode; //Tried to fit picture to buttons

            //Random position
            int x = rand.Next(1, 20);
            int y = rand.Next(1, 20);
            Grid.Controls.Add((building), x, y);
        }

        //Code to gererate the Resource Buildings
        public void generateResourceBuilding()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            Building building;

            /*int buildingSelect = rand.Next(0, 2);

            if (buildingSelect == 0 )
             {*/
                building = new Building();
            /*
            * I got the Resource building  image from the site belowe 
            https://aminoapps.com/c/cartoon/page/blog/the-phineas-and-ferb-formula/g0JU_6uqjQRZx6aNbWDGWEMwd34EJap
            */
            building.BackgroundImage = Assignment_1_SEMESTER_TWO.Properties.Resources.ResourceBuilding;
            building.Click += new EventHandler(ResourcesBuilding_Click);
            //}

            building.Height = 65;
            building.Width = 65;
            building.AutoSizeMode = AutoSizeMode; //Tried to fit picture to buttons

            //Random position
            int x = rand.Next(1, 20);
            int y = rand.Next(1, 20);
            Grid.Controls.Add((building), x, y);
        }

        public Form1()
        {
            InitializeComponent();
            generateMap(); // Initializing Map

            //Generate Units after map has been rest
            for (int i = 0; i < 30; i++)
            {
                generateUnits();  
            }
            //Generates Buildings
            for (int j = 0; j < 2; j++)
            {
                generateFactoryBuilding();
                generateResourceBuilding();

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainGame = new GameEngine(this);

            ResetTime(); //Rest Function
            isActive = false;
        }

        private void start_btn_Click(object sender, EventArgs e)
        {
            isActive = true; 
        }

        private void stop_btn_Click(object sender, EventArgs e)
        {
            isActive = false;
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {
            isActive = false;
            generateMap();// Generate new map
            ResetTime();
            MainGame.ResourceB.I = 1;
            scrapCount_lbl.Text = "00";
            //GenerateUnits units
            for (int i = 0; i < 30; i++)
            {
                generateUnits();
            }
            //Gereates Buildings
            for (int j = 0; j < 2; j++)
            {
                generateFactoryBuilding();
                generateResourceBuilding();
            }
        }

        private void ResetTime()
        {
            timeMs = 0;
            timeSec = 0;
            timeMin = 0;
        }
        ResourceBuilding r = new ResourceBuilding();
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            //timeSec++;
            // second_lbl.Text = timeSec.ToString();
            // MainEngine.ResourceB.generateResources();

            //Code to activate stopwatch to show correct time
            if (isActive)
            {
                timeMs++;
                
                if (timeMs >= 100)
                {
                    timeSec++;
                    timeMs = 0;
                    MainGame.ResourceB.generateResources();
                    if (timeSec >= 60 )
                    {
                        timeMin++;
                        timeSec = 0;
                       
                    }
                }
            }
            
            DrawTime();
        }

        private void Grid_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        void MeleeUnit_Click(object sender, EventArgs e)
        {
            MeleeUnit melee = new MeleeUnit();
            info_txb.Text = melee.ToString();
        }

        private void titan_pic_Click(object sender, EventArgs e)
        { 
            
        }

        void RangedUnit_Click(object sender, EventArgs e)
        {
            RangedUnit ranged = new RangedUnit();
            info_txb.Text = ranged.ToString();
        }

        private void nano_pic_Click(object sender, EventArgs e)
        {

        }

        void FactoryBuilding_Click(object sender, EventArgs e)
        {
            FactoryBuilding factory = new FactoryBuilding();
            info_txb.Text = factory.ToString();
        }

        void ResourcesBuilding_Click(object sender, EventArgs e)
        {
            ResourceBuilding resource = new ResourceBuilding();
            info_txb.Text = resource.ToString();
        }

        private void save_btn_Click(object sender, EventArgs e)
        {

        }

        private void read_btn_Click(object sender, EventArgs e)
        {

        }

        private void scrapCount_lbl_Click(object sender, EventArgs e)
        {

        }

        private void DrawTime()
        { //Timer Display
            mili_lbl.Text = String.Format("{0:00}", timeMs);
            second_lbl.Text = String.Format("{0:00}", timeSec);
            min_lbl.Text = String.Format("{0:00}", timeMin);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

    public abstract class Unit : Button
    {
        private int positionX { get; set; }
        private int positionY { get; set; }
        private int health { get; set; }
        private int speed { get; set; }
        private int attack { get; set; }
        private int attackRange { get; set; }
        private int team { get; set; }
        private char image { get; set; }
        protected string name { get; set; }

        public Unit()
        {


        }


        public virtual void move()
        {

        }

        public virtual void combat()
        {

        }

        public virtual void isRange()
        {

        }

        public virtual void returnUnit()
        {

        }

        public virtual void isDead()
        {

        }

        public virtual string ToString()
        {
            return " ";
        }

        public abstract void Save();
    }

    public class MeleeUnit : Unit
    {
        Form1 fm;

        public MeleeUnit()
        {
            name = "Malee Unit";
        }

        public override void move()
        {//Math I tried to calculate distance to move with Pythagoras
           // Y = m(d / (1 + m ^ 2) ^ (1 / 2) + x) - mx + y, x = 0,y = 0, d = 2.5, m = 17 / 11

            /*    new_x = x1 + Math.cos(angle) * distance;
            new_y = y1 + Math.sin(angle) * distance;
            angle = (y1 - y2) / (x1 - x2) */
        }

        public override void combat()
        {
            /*
             *   if (combat)
            {
                isRange(this.unitsList);
            }
            else
            {
                combat = true;
                isRange(this.unitsList);
            }

            /*Random random = new Random();
            int randomNumber;

            while (this.health <= 0 && health <= 0)
            {
                randomNumber = random.Next(0, 2);
                if(randomNumber == 0)
                {
                    this.health -= attack;
                }
                else
                {
                    health -= this.attack;
                }
            }*/

        }

        public override void isRange()
        {
            //implement search diagonal pitagoras. Internet is my friend.

            /* for (int i = 1; i <= this.attackRange; i++)
             {
                 //front
                 if(map[this.positionX][this.PositionY + i] != null)
                 {
                     return true;
                 }
                 //left
                 if (map[this.positionX - i][this.positionY] != null)
                 {
                     return true;
                 }
                 //back
                 if (map[this.positionX][this.positionY - i] != null)
                 {
                     return true;
                 }
                 //right
                 if (map[this.positionX + i][this.positionY] != null)
                 {
                     return true;
                 }
             }*/
        }

        public override void returnUnit()
        {

        }

        public override void isDead()
        {

        }

        public override string ToString()
        {   
            string str = "Team: Titans" + Environment.NewLine + "Health: 100"  + Environment.NewLine + "Attack: 3" + Environment.NewLine + "Speed: 2" + Environment.NewLine + "Range: 1";
            return str;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class RangedUnit : Unit
    {
        public RangedUnit()
        {
            name = "Ranged Unit";
        }

        public override void move()
        {
            //Math I tried to calculate distance to move with Pythagoras
            // Y = m(d / (1 + m ^ 2) ^ (1 / 2) + x) - mx + y, x = 0,y = 0, d = 2.5, m = 17 / 11

            /*    new_x = x1 + Math.cos(angle) * distance;
            new_y = y1 + Math.sin(angle) * distance;
            angle = (y1 - y2) / (x1 - x2) */
        }

        public override void combat()
        {
            /*
            *   if (combat)
           {
               isRange(this.unitsList);
           }
           else
           {
               combat = true;
               isRange(this.unitsList);
           }

           /*Random random = new Random();
           int randomNumber;

           while (this.health <= 0 && health <= 0)
           {
               randomNumber = random.Next(0, 2);
               if(randomNumber == 0)
               {
                   this.health -= attack;
               }
               else
               {
                   health -= this.attack;
               }
           }*/
        }

        public override void isRange()
        {
            //implement search diagonal pitagoras. Internet is my friend.

            /* for (int i = 1; i <= this.attackRange; i++)
             {
                 //front
                 if(map[this.positionX][this.PositionY + i] != null)
                 {
                     return true;
                 }
                 //left
                 if (map[this.positionX - i][this.positionY] != null)
                 {
                     return true;
                 }
                 //back
                 if (map[this.positionX][this.positionY - i] != null)
                 {
                     return true;
                 }
                 //right
                 if (map[this.positionX + i][this.positionY] != null)
                 {
                     return true;
                 }
             }*/
        }

        public override void returnUnit()
        {

        }

        public override void isDead()
        {
           
        }

        public override string ToString()
        {
           string str = "Team: Nanobots" + Environment.NewLine + "Health: 100" + Environment.NewLine + "Attack: 2" + Environment.NewLine + "Speed: 1" + Environment.NewLine + "Range: 4";
            return str;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }

    public abstract class Buildings
    {
        protected int positionX { get; set; }
        protected int positionY { get; set; }
        protected int health { get; set; }
        protected int team { get; set; }
        protected char image { get; set; }
        protected string name { get; set; }

        public Buildings()
        {
          
        }

        public virtual void death()
        {
            /* if ( health == 0)
             {
               building.Clear();
             }

             else
             {
               
             }
            */
        }

        public virtual string ToString()
        {
            return " ";
        }

        public abstract void Save();
    }

    public class ResourceBuilding : Buildings
    {
        Form1 fm;

        private string resourceType;
        private int resourcePerTick = 0;
        private int resourceRemaining = 0;
        private int i = 1;

        public int ResourcePerTick
        {
            get { return resourcePerTick; }
            set { resourcePerTick = value; }
        }

        public int I
        {
            get { return i; }
            set { i = value; }
        }

        public ResourceBuilding(Form1 fm)
        {
            this.fm = fm;
        }

        public ResourceBuilding()
        {
        }

        public override void death()
        {

        }

        public override string ToString()
        {
            string str = "Team: Nanobots" + team + Environment.NewLine + "Health: 100" + health + Environment.NewLine + "Name: Resource Building" + name;
            return str;
        }

        public void generateResources()
        {
            ResourcePerTick = (i++) * 5;
            fm.scrapCount_lbl.Text = String.Format("{0:00}", ResourcePerTick);
        }

        public void removeResource()
        {
            if (ResourcePerTick == 5)
            {
                //remove resources. Unit purchased.
            }
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class FactoryBuilding : Buildings
    {
        protected int positionX { get; set; }
        protected int positionY { get; set; }
        private int unitsProduce { get; set; }
        private int ticksPerProduction { get; set; }
        private int spawnPoint { get; set; }

        public override void death()
        {

        }

        public override string ToString()
        {
            string str = "Team: Titans" + team + Environment.NewLine + "Health: 100" + health + Environment.NewLine + "Name: Factory Building" + name;
            return str;
        }

        public void spawnUnits()
        {

        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }

    public class Building : Unit
    {
        public Building()
        {

        }
        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
   
    public class Map
    {
        public void randomMap()
        {

        }

        public void movePosition()
        {
            /*
            Random random = new Random();
            int randomNumber = random.Next(0, 5);
            for (int i = 0; i < this.Speed; i++)
            {
                move(randomNumber);
            }
            */
        }

        public void updatePosition()
        {

        }

        public void read()
        {

        }


    }

    public class GameEngine 
    {
        Form1 fm;

        public ResourceBuilding ResourceB
        {
            get; set;
        }

        public GameEngine(Form1 fm)
        {
            this.fm = fm;
            ResourceB = new ResourceBuilding(fm);
        }


        //public override void Save()
        //{
        //    throw new NotImplementedException();
        //}
    }
}

