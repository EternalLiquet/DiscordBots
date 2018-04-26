using System.Drawing;
using System.Net;
using System.IO;


namespace MegumiBot
{
    class IntroPic
    {

        //Declare variable for username
        private string userName;

        //Declare variable for the top text on the picture
        private string welcomeText;

        //Declare variale for the bottom text of the picture
        private string nameText;

        //Declare variable for the name of the server
        private string serverName;

        //Declare variable for the url of the user's avatar
        private string avatarURL;

        //Declare variable for the filepath of the base intro image
        private string imageFilePath;

        //Declare variable for the path of the output image
        private string outputPath;

        //Declare variable for the location of the first line of text on the picture
        private PointF textLocation;

        //Declare variable for the location of the second line of text on the picture
        private PointF textLocation2;

        //Declare bitmap variable to hold the base intro image
        private Bitmap introImage;

        //Declare bitmap variable to hold the user's avatar
        private Bitmap userIcon;

        //Declare graphics variable to manipulate the image
        private Graphics graphics;

        //Declare font variable to hold the font settings for the text
        private Font textFont;

        //Declare StringFormat variable to hold settings for how to format the string
        private StringFormat middleAlign;

        //Declare Webclient 
        private WebClient wc;

        //Declare array of bytes
        private byte[] bytes;

        //Declare a memory stream
        private MemoryStream ms;

        private string[] introPics;



        //Function to create the picture. Returns a string which is the filepath of the picture created
        public string createPic(string userName, string serverName, string avatarURL, int picToUse)
        {

            introPics = new string[]
            {
                "Images/IntroPics/megumiKato.png",
                "Images/IntroPics/megumiKato2.png",
                "Images/IntroPics/megumiKato3.png",
                "Images/IntroPics/megumiKato4.png"
            };
            int megumiPictureToIndex = picToUse;
            string megumiToUse = introPics[megumiPictureToIndex];
            //this keywords means this class, which means it is THIS CLASS'S userName property that is being manipulated. So even if the name of the parameter in the function is also userName, 
            //the program knows that when the this keyword is being used, it is the class's userName property being assigned the value of userName from the function's parameter named userName
            this.userName = userName;
            this.serverName = serverName;
            this.avatarURL = avatarURL;

            //Creates the text that says "Welcome to (Server Name Here), the first line on the picture"
            welcomeText = "Welcome to " + serverName + ",";

            //Creates the text that says "(insert nickname OR name here)!, the second line on the picture"
            nameText = userName + "!";

            //Defines two points on the picture to place the first and second line of text
            textLocation = new PointF(440f, 400f);
            textLocation2 = new PointF(440, 450f);

            //The file path for the base image 
            imageFilePath = megumiToUse;

            //Assigns the bitmap object named introImage a new image from the file path stated above, explicitly convert to bitmap
            introImage = (Bitmap)Image.FromFile(imageFilePath);

            //Graphics image uses the bitmap introImage
            graphics = Graphics.FromImage(introImage);

            //Simply defines the font that the picture will use. In this case, it will be Helvetica, font size 50, and bolded text
            textFont = new Font("Helvetica", 50, FontStyle.Bold);

            //Instantiates a new StringFormat object
            middleAlign = new StringFormat();

            //Set Alignment property to be center alignment
            middleAlign.Alignment = StringAlignment.Center;


            //graphics draws the text onto the image using the text, the font, the brush color, the location of the text, and the Alignment
            graphics.DrawString(welcomeText, textFont, Brushes.White, textLocation, middleAlign);
            graphics.DrawString(nameText, textFont, Brushes.White, textLocation2, middleAlign);

            //Instantiates a new WebClient object 
            wc = new WebClient();

            //WebClient object named wc downloads the data from the avatarURL of the user. Assigns this to a byte array. 
            bytes = wc.DownloadData(avatarURL);

            //Assigns the bytearray to an instance of MemoryStream named ms
            ms = new MemoryStream(bytes);

            //Assigns the userIcon bitmap variable to an Image from the earlier MemoryStream, explicitly converted to bitmap
            userIcon = (Bitmap)Image.FromStream(ms);

            //Corrects the resolution of the avatar so that it fits well onto the picture. 
            userIcon.SetResolution(50f, 50f);

            //Draws the userIcon image on top of the introImage picture
            graphics.DrawImage(userIcon, introImage.Width / 2.5f, 200f);

            //Assigns the final image's outputPath
            outputPath = "intro.png";

            //Saves the image to the outputPath mentioned above
            introImage.Save(outputPath);

            //returns the outputPath, gets passed into the "SendFile" function in KatoBot.cs. The function looks for the file at the specified output path and will find the picture
            //that this fucntion has just created. Sends that to the channel. 
            return outputPath;
        }

        public int megumiPicsLength()
        {
            return introPics.Length;
        }
    }
}
