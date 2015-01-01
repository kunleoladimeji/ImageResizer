<%@ Page Language="C#" Inherits="ImageResizer.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>List of Uploaded Images</title>
	<script type="text/javascript" src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
	<script type="text/javascript" src="js/lightview.js"></script>
	<script type="text/javascript" src="js/spinners.min.js"></script>

	<link rel="stylesheet" type="text/css" href="css/mycss.css" />
	<link rel="stylesheet" type="text/css" href="css/lightview.css" />
	<meta name="viewport" content="initial-scale=1.0; maximum-scale=1.0; width=device-width;">
</head>
<body>
	<div class="table-title">
		<h3>Image List Table</h3>
	</div>
	<form id="form1" runat="server">
	<div class="CSSTableGenerator">
	<table border="1" class="table-fill">
		<thead>
		<tr>
				<th class="text-left">No.</th>
				<th class="text-left">Caption</th>
				<th class="text-left">Original Image</th>
				<th class="text-left">File Size (Original Image)</th>
				<th class="text-left">Compressed Image</th>
				<th class="text-left">File Size (Compressed Image)</th>
				</tr>
		</thead>
<tbody class="table-hover">
		<asp:Repeater id="imgList" runat="server">
			<ItemTemplate>
				<tr rowspan='3'>
					<td class="text-left"> <%#((System.Xml.XmlNode)Container.DataItem).Attributes["num"].Value %></td>
					<td class="text-left"><%# ((System.Xml.XmlNode)Container.DataItem)["caption"].InnerText %></td>
					<td class="text-left"><a href='<%# ((System.Xml.XmlNode)Container.DataItem)["originalImageURL"].InnerText %>' class="lightview" data-lightview-group="defaultOptions" data-lightview-group-options= "controls:'top',initialDimensions:{width: 500,height: 00}">
					<img src='<%# ((System.Xml.XmlNode)Container.DataItem)["originalImageURL"].InnerText %>' class="tableImg" /></a></td>
					<td class="text-left"><%# ((System.Xml.XmlNode)Container.DataItem)["originalFileSize"].InnerText %></td>
					<td class="text-left"><a href='<%# ((System.Xml.XmlNode)Container.DataItem)["compressedImageURL"].InnerText %>' class="lightview" data-lightview-group="defaultOptionsOther" data-lightview-group-options= "controls:'top', height:500, width:500">
					<img src='<%# ((System.Xml.XmlNode)Container.DataItem)["compressedImageURL"].InnerText %>' class="tableImg" /> </a></td>
					<td class="text-left"><%#((System.Xml.XmlNode)Container.DataItem)["compressedFileSize"].InnerText %></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
</tbody>
	</table>
		<p align='center'><a href="UploadImage.aspx">Go Upload! </a></p>
		</div>
	</form>
</body>
</html>

