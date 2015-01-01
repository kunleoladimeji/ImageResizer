using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Xml;

namespace ImageResizer
{
	
	public partial class UploadImage : System.Web.UI.Page
	{
        String saveHiRes = "Images/HiRes/";
        String saveLowRes = "Images/LowRes/";
        public String errMsg = "";

        public String caption, originalImageURL, originalFileSize, compressedImageURL, compressedFileSize = "";

        public int index;

		public void submitClick(object sender, EventArgs args)
		{   
            if (checkText() == true)
            {
                if (validateImage() == true)
                {
                    saveImage();
                    ClientScript.RegisterStartupScript(this.GetType(), "Success!", "alert('Image Uploaded!')", true);
                    System.Console.Write("Image successfully uploaded!");
                    try
                    {
                        storeInDB();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Error!", "alert('Uploaded file is not valid!')", true);
                    System.Console.Write(errMsg);
                    lblStatus.Text = errMsg;
                    return;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Error!", "alert('Please insert a caption!')", true);
                System.Console.Write(errMsg);
                lblStatus.Text = errMsg;
                return;
            }
		}

		public void resetClick(object sender, EventArgs args)
		{
			imgCaption.Text = "";
			img.Attributes.Clear();
            errMsg = "";
            return;
		}
		
		public bool validateImage()
		{
			HttpPostedFile uploadedImage = (HttpPostedFile)(img.PostedFile);
			if ((uploadedImage != null) && (uploadedImage.ContentLength > 0))
			{
				if (IsImage(uploadedImage) == false)
				{
					return false;
				}
				else
				{
                    errMsg = "Uploaded file is not an image! Please upload an image.";
					return true;
				}
			}
			else
			{
				return false;
			}
		}

		public bool IsImage(HttpPostedFile posted)
		{
			return ((posted != null) && System.Text.RegularExpressions.Regex.IsMatch(posted.ContentType, "image/\\S+") && (posted.ContentLength > 0));
		}

		public bool checkText()
		{
			if (imgCaption.Text == "")
			{
				//Put an error message here
                errMsg = "Please enter a caption here";
				return false;
			}
			else
			{
				return true;
			}
		}
            
		public void saveImage()
		{
            string filename = img.PostedFile.FileName;
            string pathToCheck = saveHiRes + filename;
            string tempfileName = "";
            Stream strm = img.PostedFile.InputStream;

            if (System.IO.File.Exists(pathToCheck)) 
            {
                int counter = 2;
                while (System.IO.File.Exists(pathToCheck))
                {
                    // if a file with this name already exists,
                    // prefix the filename with a number.
                    tempfileName =  filename + counter.ToString();
                    pathToCheck = saveHiRes + tempfileName;
                    counter ++;
                }

                filename = tempfileName;

                // Notify the user that the file name was changed.
                lblStatus.Text = "A file with the same name already exists." + 
                    "<br />Your file was saved as " + filename;
            }
            else
            {
                // Notify the user that the file was saved successfully.
                lblStatus.Text = "Your file was uploaded successfully.";
            }

            // Append the name of the file to upload to the path.
            saveHiRes += filename;
            originalImageURL = saveHiRes;
            originalFileSize = ((img.PostedFile.ContentLength) / 1000).ToString();
            // Call the SaveAs method to save the uploaded
            // file to the specified directory.
            img.SaveAs(saveHiRes);

            compressImage(0.5, strm, saveLowRes += filename);
		}

        public void compressImage(double scaleFactor, Stream sourcePath, string targetPath)
        {
            using (var image = Image.FromStream(sourcePath))
            {
                var newWidth = (int)(image.Width * scaleFactor);
                var newHeight = (int)(image.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(targetPath, image.RawFormat);
                compressedImageURL = targetPath;
                FileInfo compFile = new FileInfo(targetPath);
                long cSize = compFile.Length;
                compressedFileSize = (cSize / 1000).ToString() + "KB";
            }
        }

        public bool storeInDB()
        {
            index = Directory.GetFiles("Images/HiRes/", "*").Length;
            XmlDocument myDB = new XmlDocument();
            myDB.Load("Images/imageDB.xml");
            XmlNode dbRootNode = myDB.SelectSingleNode("images");
            XmlNode dbRecordNode =     dbRootNode.AppendChild(
                myDB.CreateNode(XmlNodeType.Element,"record",""));
            XmlAttribute indexNum = myDB.CreateAttribute("num");
            indexNum.Value = index.ToString();
            dbRecordNode.Attributes.Append(indexNum);
            dbRecordNode.AppendChild(myDB.CreateNode(XmlNodeType.Element,
                "caption", "")).InnerText = imgCaption.Text;
            dbRecordNode.AppendChild(myDB.CreateNode(XmlNodeType.Element,
                "originalImageURL", "")).InnerText = originalImageURL;
            dbRecordNode.AppendChild(myDB.CreateNode(XmlNodeType.Element,
                "originalFileSize", "")).InnerText = originalFileSize + "KB";
            dbRecordNode.AppendChild(myDB.CreateNode(XmlNodeType.Element, "compressedImageURL", ""))
                .InnerText = compressedImageURL;
            dbRecordNode.AppendChild(myDB.CreateNode(XmlNodeType.Element, "compressedFileSize", "")).InnerText
            = compressedFileSize;

            myDB.Save("Images/imageDB.xml");
            return true;
        }
	}
}
