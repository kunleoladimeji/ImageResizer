using System;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Xml;

namespace ImageResizer
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
                XmlDocument imgCatalog = new XmlDocument();
                imgCatalog.Load(MapPath("Images/imageDB.xml"));
                XmlNodeList nodes = imgCatalog.SelectNodes("images/record");
                imgList.DataSource = nodes;
                imgList.DataBind();
		}

	}
}