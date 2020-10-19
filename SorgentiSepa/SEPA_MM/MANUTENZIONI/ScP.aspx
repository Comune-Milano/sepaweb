<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScP.aspx.vb" Inherits="ScP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv=Content-Type content="text/html; charset=windows-1252">
<meta name=ProgId content=Excel.Sheet>
<meta name=Generator content="Microsoft Excel 12">
    <title>Scheda P</title>
    <style id="SCHEDE RILIEVO TUTTE_11121_Styles">

	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1511121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6511121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6611121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl6711121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6811121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl6911121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7011121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7111121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7211121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7311121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7411121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7511121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7611121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7711121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7811121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl7911121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8011121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8111121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl8211121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl8311121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl8411121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl8511121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8611121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:bottom;
	border-top:none;
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8711121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:bottom;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8811121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:bottom;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8911121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9011121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9111121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:center;
	vertical-align:middle;
	border:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9211121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9311121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9411121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9511121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9611121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:12.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9711121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:12.0pt;
	font-weight:700;
	font-style:normal;
	text-decoration:none;
	font-family:Arial, sans-serif;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:left;
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9811121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9911121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl10011121
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:normal;
	text-decoration:none;
	font-family:Arial;
	mso-generic-font-family:auto;
	mso-font-charset:0;
	mso-number-format:General;
	text-align:general;
	vertical-align:bottom;
	border-top:none;
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}

</style>


</head>
<body>
    <form id="form1" runat="server">
    <div align=center x:publishsource="Excel">
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Constantia"
            Font-Size="9pt" ForeColor="#8080FF" Width="504px">Pagina ottimizzata per Internet Explorer 7</asp:Label><table
                style="width: 100%; background-color: gainsboro; text-align: center">
                <tr>
                    <td style="width: 223px; height: 57px; text-align: center">
                        &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                            Style="left: 120px; top: 24px" ToolTip="SALVA" /></td>
                    <td style="height: 57px; text-align: center">
                        &nbsp;</td>
                    <td style="width: 164px; height: 57px; text-align: center">
                        &nbsp;<asp:ImageButton ID="BtnChiudi" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                            Style="left: 120px; top: 24px" ToolTip="ESCI" />
                    </td>
                </tr>
            </table>
        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
            Style="text-align: left" Visible="False" Width="652px"></asp:Label><br />
    <table border=0 cellpadding=0 cellspacing=0 width=853 style='border-collapse:
 collapse;table-layout:fixed;width:642pt'>
 <col width=58 style='mso-width-source:userset;mso-width-alt:2121;width:44pt'>
 <col width=100 style='mso-width-source:userset;mso-width-alt:3657;width:75pt'>
 <col width=37 style='mso-width-source:userset;mso-width-alt:1353;width:28pt'>
 <col width=127 style='mso-width-source:userset;mso-width-alt:4644;width:95pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=143 style='mso-width-source:userset;mso-width-alt:5229;width:107pt'>

 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=140 style='mso-width-source:userset;mso-width-alt:5120;width:105pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=142 style='mso-width-source:userset;mso-width-alt:5193;width:107pt'>
 <col width=28 style='mso-width-source:userset;mso-width-alt:1024;width:21pt'>
 <tr height=34 style='mso-height-source:userset;height:26.1pt'>
  <td colspan=11 height=34 class=xl9611121 width=853 style='border-right:.5pt solid black;
  height:26.1pt;width:642pt'>U. T. IMPIANTO DI DISTRIBUZIONE GAS : SCHEDA
  RILIEVO INPIANTO DI DISTRIBUZIONE GAS<span
  style='mso-spacerun:yes'> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span>P</td>

 </tr>
 <tr height=53 style='mso-height-source:userset;height:39.95pt'>
  <td height=53 class=xl8111121 width=58 style='height:39.95pt;border-top:none;
  width:44pt'>Scheda P</td>
  <td class=xl8111121 style='border-top:none;border-left:none;
  width:75pt'>ELEMENTO TECNICO</td>
  <td class=xl8111121 style="border-top:none;border-left:none;
  width:45pt"></td>
  <td colspan=2 class=xl8111121 width=153 style='border-left:none;width:115pt'>MATERIALI
  E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td class=xl8111121 width=143 style='border-top:none;border-left:none;
  width:107pt'>ANALISI PRESTAZIONALE (X)</td>
  <td class=xl8111121 width=26 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td colspan=2 class=xl8111121 width=166 style='border-left:none;width:125pt'>ANOMALIE
  (%)</td>
  <td colspan=2 class=xl8111121 width=170 style='border-left:none;width:128pt'>INTERVENTI</td>
 </tr>
 <tr height=85 style='height:63.75pt'>
  <td class=xl7811121 width=58 style="height:67pt;border-top:none;
  width:44pt">&nbsp;</td>

  <td class=xl7811121 style="border-top:none;border-left:none;
  width:75pt; height: 67pt;">
      <span style="font-size: 8pt; vertical-align: top; text-align: center">RETE DI DISTRIBUZIONE DEL CONTATORE ALL' UTENZA<br />
          <span style="font-size: 10pt">ml</span><asp:TextBox ID="Text_mq_P1" runat="server" MaxLength="9" Width="66px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_P1"
              ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
              ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></span></td>
  <td class=xl6611121 style="border-top:none;border-left:none;
  width:45pt; height: 67pt;"></td>
  <td class=xl6811121 width=127 style="border-top:none;border-left:none;
  width:95pt; height: 67pt;">a vista</td>
  <td class=xl6811121 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 67pt;">
      <asp:CheckBox ID="C_MAT_P1_a0vista" runat="server" Text="." /></td>
  <td class=xl6511121 style="border-top:none;border-left:none; height: 67pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl6511121 style="border-top:none;border-left:none; height: 67pt;">
      <asp:CheckBox ID="C_ANA_P1_nuovo" runat="server" Text="." /></td>

  <td class=xl6811121 width=140 style="border-top:none;border-left:none;
  width:105pt; height: 67pt;">odore di gas</td>
  <td class=xl6811121 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 67pt;">
      <asp:TextBox ID="T_ANO_P1_odore0gas" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6811121 width=142 style="border-top:none;border-left:none;
  width:107pt; height: 67pt;">prova di tenuta rete gas</td>
  <td class=xl6911121 style="border-top:none;border-left:none; height: 67pt;">
      <asp:CheckBox ID="C_INT_P1_tenuta0rete0gas" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6611121 width=58 style="height:30pt;border-top:none;
  width:44pt">&nbsp;</td>
  <td class=xl6611121 style="border-top:none;border-left:none;
  width:75pt; height: 30pt;">&nbsp;</td>

  <td class=xl6611121 style="border-top:none;border-left:none;
  width:45pt; height: 30pt;">&nbsp;</td>
  <td class=xl6811121 width=127 style="border-top:none;border-left:none;
  width:95pt; height: 30pt;">immurata / non accessibile</td>
  <td class=xl6811121 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_P1_immurata0non0accessibile" runat="server" Text="." /></td>
  <td class=xl6511121 style="border-top:none;border-left:none; height: 30pt;">2. lieve
  obsolescenza</td>
  <td class=xl6511121 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_ANA_P1_lieve" runat="server" Text="." /></td>
  <td class=xl6811121 width=140 style="border-top:none;border-left:none;
  width:105pt; height: 30pt;">mancanza fori di areazione unità</td>
  <td class=xl6811121 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_P1_mancanza0fori0areazione" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl6811121 width=142 style="border-top:none;border-left:none;
  width:107pt; height: 30pt;">creazione fori di areazione</td>
  <td class=xl6911121 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_P1_creazione0fori0areazione" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6611121 width=58 style="height:24pt;border-top:none;
  width:44pt">&nbsp;</td>
  <td class=xl6611121 style="border-top:none;border-left:none;
  width:75pt; height: 24pt;">&nbsp;</td>
  <td class=xl6611121 style="border-top:none;border-left:none;
  width:45pt; height: 24pt;">&nbsp;</td>
  <td class=xl6611121 width=127 style="border-top:none;border-left:none;
  width:95pt; height: 24pt;">&nbsp;</td>

  <td class=xl6611121 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">&nbsp;</td>
  <td class=xl6511121 style="border-top:none;border-left:none; height: 24pt;">3. forte
  obsolescenza</td>
  <td class=xl6511121 style="border-top:none;border-left:none; height: 24pt;">
      <asp:CheckBox ID="C_ANA_P1_forte" runat="server" Text="." /></td>
  <td class=xl6611121 width=140 style="border-top:none;border-left:none;
  width:105pt; height: 24pt;">&nbsp;</td>
  <td class=xl6611121 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">&nbsp;</td>
  <td class=xl6811121 width=142 style="border-top:none;border-left:none;
  width:107pt; height: 24pt;">&nbsp;</td>
  <td class=xl6911121 style="border-top:none;border-left:none; height: 24pt;">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl6611121 width=58 style="height:22pt;border-top:none;
  width:44pt">&nbsp;</td>
  <td class=xl6611121 style="border-top:none;border-left:none;
  width:75pt; height: 22pt;">&nbsp;</td>
  <td class=xl6611121 style="border-top:none;border-left:none;
  width:45pt; height: 22pt;">&nbsp;</td>
  <td class=xl6611121 width=127 style="border-top:none;border-left:none;
  width:95pt; height: 22pt;">&nbsp;</td>
  <td class=xl6611121 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 22pt;">&nbsp;</td>
  <td class=xl6511121 style="border-top:none;border-left:none; height: 22pt;">4. totale
  obsolescenza</td>
  <td class=xl6511121 style="border-top:none;border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_ANA_P1_totale" runat="server" Text="." /></td>

  <td class=xl6611121 width=140 style="border-top:none;border-left:none;
  width:105pt; height: 22pt;">&nbsp;</td>
  <td class=xl6611121 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 22pt;">&nbsp;</td>
  <td class=xl6811121 width=142 style="border-top:none;border-left:none;
  width:107pt; height: 22pt;">&nbsp;</td>
  <td class=xl6911121 style="border-top:none;border-left:none; height: 22pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=2 rowspan=2 height=34 class=xl9111121 style='height:25.5pt'>STATO
  DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl9211121>Stato 1</td>

  <td rowspan=2 class=xl8311121 style="border-top:none; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl8211121 style="border-top:none;border-left:none; height: 20pt;">Stato 2.1</td>
  <td class=xl8311121 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8211121 style="border-top:none;border-left:none; height: 20pt;">Stato 3.1</td>
  <td class=xl8311121 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl9211121 style='border-top:none'>Stato 3.3</td>
  <td class=xl8311121 style="border-top:none;border-left:none; height: 20pt;">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl8211121 style="height:20pt;border-top:none;
  border-left:none">Stato 2.2</td>
  <td class=xl8311121 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8211121 style="border-top:none;border-left:none; height: 20pt;">Stato 3.2</td>
  <td class=xl8311121 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8311121 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>

 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl9411121 width=853 style='height:12.75pt;
  width:642pt'><span style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA
  INDIVIDUAZIONE SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI
  INDAGINE<span style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7111121 colspan=4 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl6711121></td>
  <td class=xl6711121></td>

  <td class=xl6711121></td>
  <td class=xl6711121></td>
  <td class=xl6711121></td>
  <td class=xl6711121></td>
  <td class=xl7211121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1511121 style="width: 75pt"></td>

  <td class=xl1511121 style="width: 45pt"></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl7411121>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 colspan=7 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl7411121>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td colspan=11 height=17 class=xl9511121 width=853 style='height:12.75pt;
  width:642pt'>In particolare inserire per gli elementi tecnici riportati:
  dimensioni generali (U.M.) – ubicazione di anomalie più significative
  (percentuale per singola anomalia&gt;= 60%)<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 height=17 class=xl8611121 width=853 style='border-right:.5pt solid black;
  height:12.75pt;width:642pt'>Per alcuni El. Tecnici potrebbe risultare
  necessario inserire diverse dimensioni in relazione alle diverse tipologie
  costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1511121 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="456px" TextMode="MultiLine" Width="760px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7511121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7611121 style="width: 75pt">&nbsp;</td>
  <td class=xl7611121 style="width: 45pt">&nbsp;</td>

  <td class=xl7611121>&nbsp;</td>
  <td class=xl7611121>&nbsp;</td>
  <td class=xl7611121>&nbsp;</td>
  <td class=xl7611121>&nbsp;</td>
  <td class=xl7611121>&nbsp;</td>
  <td class=xl7611121>&nbsp;</td>
  <td class=xl7611121>&nbsp;</td>
  <td class=xl7711121>&nbsp;</td>
 </tr>

 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl7911121 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl7011121 style="border-top:none; width: 45pt;">&nbsp;</td>
  <td class=xl7011121 style='border-top:none'>&nbsp;</td>
  <td class=xl7011121 style='border-top:none'>&nbsp;</td>
  <td class=xl7011121 style='border-top:none'>&nbsp;</td>
  <td class=xl7011121 style='border-top:none'>&nbsp;</td>
  <td class=xl7011121 style='border-top:none'>&nbsp;</td>

  <td class=xl7011121 style='border-top:none'>&nbsp;</td>
  <td class=xl7011121 style='border-top:none'>&nbsp;</td>
  <td class=xl8011121 style='border-top:none'>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl10011121 width=853
  style='border-right:.5pt solid black;height:25.5pt;width:642pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1511121 style="width: 75pt"></td>
  <td class=xl1511121 style="width: 45pt"></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>

  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl1511121></td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1511121 colspan="9" rowspan="29">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="512px" TextMode="MultiLine" Width="760px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7411121>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7311121 style="height:13pt">&nbsp;</td>
  <td class=xl7411121 style="height: 13pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7311121 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7411121>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7311121 style="height:13pt">&nbsp;</td>
  <td class=xl7411121 style="height: 13pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7511121 style="height:13pt">&nbsp;</td>
  <td class=xl7611121 style="height: 13pt; width: 75pt;">&nbsp;</td>
  <td class=xl7611121 style="height: 13pt; width: 45pt;">&nbsp;</td>
  <td class=xl7611121 style="height: 13pt">&nbsp;</td>

  <td class=xl7611121 style="height: 13pt">&nbsp;</td>
  <td class=xl7611121 style="height: 13pt">&nbsp;<asp:Button ID="Button1" runat="server" Text="Button" Visible="False" /></td>
  <td class=xl7611121 style="height: 13pt">&nbsp;</td>
  <td class=xl7611121 style="height: 13pt">&nbsp;</td>
  <td class=xl7611121 style="height: 13pt">&nbsp;</td>
  <td class=xl7611121 style="height: 13pt">&nbsp;</td>
  <td class=xl7711121 style="height: 13pt">&nbsp;</td>
 </tr>
 

 <tr height=0 style='display:none'>
  <td width=58 style='width:44pt'></td>
  <td style='width:75pt'></td>
  <td style="width:45pt"></td>
  <td width=127 style='width:95pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=143 style='width:107pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=140 style='width:105pt'></td>

  <td width=26 style='width:20pt'></td>
  <td width=142 style='width:107pt'></td>
  <td width=28 style='width:21pt'></td>
 </tr>

</table>

    
    </div>
    </form>
</body>
</html>
