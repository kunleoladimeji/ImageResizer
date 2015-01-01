<%@ Page Language="C#" Inherits="ImageResizer.UploadImage" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Upload Image</title>
<link rel="stylesheet" href="css/mycss.css" />
	<meta name="viewport" content="initial-scale=1.0; maximum-scale=1.0; width=device-width;">
</head>
<body align="center">
	<div class="tab-pane fade active in" id="elegant-aero-demo">
	<form id="form1" class="elegant-aero" runat="server">
	<h1>Upload Image!
	<span>Please fill in a valid caption and image</span></h1>
	<p><label for="imgCaption"><span>Image Caption</span></label><asp:TextBox id="imgCaption" runat="server" placeholder="Enter an image caption!" /></p>
	<p><label for="img"><span>Image</span></label><asp:FileUpload id="img" runat="server" placeholder="Valid images only!" /></p>
	<p><asp:Button Text="Submit Image" runat="server" OnClick="submitClick" />   <asp:Button runat="server" Text="Reset" OnClick="resetClick" /></p>
	<p><label><span><asp:Label id="lblStatus" runat="server" Visible="true" Text="" /></span></label></p>
	</form>
	<p align='center'><a href="Default.aspx">Go Home</a></p>
	</div>
</body>
</html>

