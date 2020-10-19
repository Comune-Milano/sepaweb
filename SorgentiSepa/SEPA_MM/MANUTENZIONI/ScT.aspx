<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScT.aspx.vb" Inherits="ScT" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">
        <title>Scheda T</title>
    <style id="SCHEDE RILIEVO TUTTE_24601_Styles">

	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1524601
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
.xl6524601
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
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6624601
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
.xl6724601
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
.xl6824601
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl6924601
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7024601
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
	border-top:1.0pt solid windowtext;
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7124601
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
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7224601
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
	border-right:1.0pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7324601
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7424601
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
.xl7524601
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
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7624601
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7724601
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
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl7824601
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
.xl7924601
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
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8024601
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
.xl8124601
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
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8224601
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
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8324601
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8424601
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
.xl8524601
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
.xl8624601
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
.xl8724601
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
.xl8824601
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
	border:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl8924601
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
.xl9024601
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
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9124601
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
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9224601
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
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9324601
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
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9424601
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9524601
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
	white-space:normal;}
.xl9624601
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
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9724601
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
	border-right:none;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9824601
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
.xl9924601
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
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10024601
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10124601
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
.xl10224601
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
.xl10324601
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
.xl10424601
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
	text-align:right;
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10524601
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
	text-align:general;
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}

</style>

    
</head>
<body>
    <form id="form1" runat="server">
    <div align=center x:publishsource="Excel" id="DIV1">
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
                            Style="left: 120px; top: 24px" ToolTip="ESCI" />&nbsp;
                    </td>
                </tr>
            </table>
        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
            Style="text-align: left" Visible="False" Width="652px"></asp:Label><br />
    <table border=0 cellpadding=0 cellspacing=0 width=905 style='border-collapse:
 collapse;table-layout:fixed;width:682pt'>
 <col width=61 style='mso-width-source:userset;mso-width-alt:2230;width:46pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=51 style='mso-width-source:userset;mso-width-alt:1865;width:38pt'>
 <col width=113 style='mso-width-source:userset;mso-width-alt:4132;width:85pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=141 style='mso-width-source:userset;mso-width-alt:5156;width:106pt'>

 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <tr height=40 style='mso-height-source:userset;height:30.0pt'>
  <td colspan=9 height=40 class=xl8824601 width=734 style='height:30.0pt;
  width:553pt'>U. T. IMPIANTI DI TELECOMUNICAZIONE- SCHEDA RILIEVO IMPIANTI DI
  TELECOMUNICAZIONE</td>
  <td colspan=2 class=xl10424601 width=171 style="border-right:.5pt solid black;
  border-left:none;width:129pt; text-align: center;">T</td>

 </tr>
 <tr height=70 style='mso-height-source:userset;height:52.5pt'>
  <td height=70 class=xl8524601 width=61 style='height:52.5pt;border-top:none;
  width:46pt'>Scheda T</td>
  <td class=xl8524601 width=145 style='border-top:none;border-left:none;
  width:109pt'>ELEMENTO TECNICO</td>
  <td class=xl8524601 width=51 style='border-top:none;border-left:none;
  width:38pt'></td>
  <td colspan=2 class=xl8524601 width=139 style='border-left:none;width:105pt'>MATERIALI
  E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td class=xl8524601 width=141 style='border-top:none;border-left:none;
  width:106pt'>ANALISI PRESTAZIONALE (X)</td>
  <td class=xl8524601 width=26 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td colspan=2 class=xl8524601 width=171 style='border-left:none;width:129pt'>ANOMALIE
  (%)</td>
  <td colspan=2 class=xl8524601 width=171 style='border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl8424601 width=61 style='height:25.5pt;border-top:none;
  width:46pt'>T 1</td>

  <td rowspan=4 class=xl9024601 width=145 style='border-bottom:1.0pt solid black;
  border-top:none;width:109pt'>IMPIANTI DI TELECOMUNICAZIONE<br />
      cad<asp:TextBox ID="Text_mq_T1" runat="server" MaxLength="9" Width="104px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
          ControlToValidate="Text_mq_T1" ErrorMessage="!" Font-Bold="True" Height="1px"
          Style="left: 432px; top: 264px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
          Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=4 class=xl9124601 width=51 style='border-bottom:1.0pt solid black;
  border-top:none;width:38pt'></td>
  <td class=xl7824601 width=113 style='border-top:none;border-left:none;
  width:85pt'>telefono</td>
  <td class=xl7824601 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_T1_telefono" runat="server" Text="." />&nbsp;</td>
  <td class=xl6624601 style='border-top:none;border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl6624601 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_T1_nuovo" runat="server" Text="." /></td>

  <td class=xl7824601 width=145 style='border-top:none;border-left:none;
  width:109pt'>Cavia a vista</td>
  <td class=xl7824601 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_T1_cavi0vista" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl7824601 width=145 style='border-top:none;border-left:none;
  width:109pt'>ogni intervento a carico dell'operatore</td>
  <td class=xl8024601 style="border-top:none;border-left:none; width: 20pt;">
      <asp:CheckBox ID="C_INT_T1_intervento0carico0operatore" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6724601 width=61 style="height:13pt;border-top:none;
  width:46pt">&nbsp;</td>
  <td class=xl7824601 width=113 style="border-top:none;border-left:none;
  width:85pt; height: 13pt;">fastweb</td>

  <td class=xl7824601 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 13pt;">
      <asp:CheckBox ID="C_MAT_T1_fastweb" runat="server" Text="." /></td>
  <td class=xl6624601 style="border-top:none;border-left:none; height: 13pt;">2. lieve
  obsolescenza</td>
  <td class=xl6624601 style="border-top:none;border-left:none; height: 13pt;">
      <asp:CheckBox ID="C_ANA_T1_lieve" runat="server" Text="." /></td>
  <td class=xl7824601 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 13pt;">&nbsp;</td>
  <td class=xl7824601 style="border-top:none;border-left:none;
  width:20pt; height: 13pt;">&nbsp;</td>
  <td class=xl7824601 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 13pt;">ripristino cavi</td>
  <td class=xl8024601 style="border-top:none;border-left:none; width: 20pt; height: 13pt;">
      <asp:CheckBox ID="C_INT_T1_ripristino0cavi" runat="server" Text="." /></td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl6724601 width=61 style="height:31pt;border-top:none;
  width:46pt">&nbsp;</td>
  <td class=xl6724601 width=113 style="border-top:none;border-left:none;
  width:85pt; height: 31pt;">altro</td>
  <td class=xl6724601 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:CheckBox ID="C_MAT_T1_altro" runat="server" Text="." /></td>
  <td class=xl6624601 style="border-top:none;border-left:none; height: 31pt;">3. forte
  obsolescenza</td>
  <td class=xl6624601 style="border-top:none;border-left:none; height: 31pt;">
      <asp:CheckBox ID="C_ANA_T1_forte" runat="server" Text="." /></td>
  <td class=xl6724601 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">&nbsp;</td>
  <td class=xl6724601 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">&nbsp;</td>

  <td class=xl7824601 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">&nbsp;</td>
  <td class=xl8024601 style="border-top:none;border-left:none; width: 20pt; height: 31pt;">&nbsp;</td>
 </tr>
 <tr height=50 style='mso-height-source:userset;height:37.5pt'>
  <td height=50 class=xl7924601 width=61 style='height:37.5pt;border-top:none;
  width:46pt'>&nbsp;</td>
  <td class=xl7924601 width=113 style='border-top:none;border-left:none;
  width:85pt'>&nbsp;</td>
  <td class=xl8124601 width=26 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td class=xl8224601 style='border-top:none;border-left:none'>4. totale
  obsolescenza</td>

  <td class=xl8224601 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_T1_tot" runat="server" Text="." /></td>
  <td class=xl8124601 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl8124601 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td class=xl8124601 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl8324601 style="border-top:none;border-left:none; width: 20pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=2 rowspan=2 height=34 class=xl10124601 style='height:25.5pt'>STATO
  DI DEGRADO</td>

  <td colspan=2 rowspan=2 class=xl10224601>Stato 1</td>
  <td rowspan=2 class=xl8724601 style="vertical-align: top; text-align: left">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl8624601 style="border-left:none; height: 21pt;">Stato 2.1</td>
  <td class=xl8724601 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8624601 style="border-left:none; height: 21pt;">Stato 3.1</td>
  <td class=xl8724601 style="border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl10224601>Stato 3.3</td>

  <td class=xl8724601 style="border-left:none; width: 20pt; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl8624601 style="height:20pt;border-top:none;
  border-left:none">Stato 2.2</td>
  <td class=xl8724601 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8624601 style="border-top:none;border-left:none; height: 20pt;">Stato 3.2</td>
  <td class=xl8724601 style="border-top:none;border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8724601 style="border-top:none;border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl9424601 width=905 style='border-right:1.0pt solid black;
  height:12.75pt;width:682pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7324601 colspan=2 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7424601></td>

  <td class=xl7424601></td>
  <td class=xl7424601></td>
  <td class=xl7424601></td>
  <td class=xl7424601></td>
  <td class=xl7424601></td>
  <td class=xl7424601 style="width: 20pt"></td>
  <td class=xl7424601></td>
  <td class=xl7524601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>

  <td class=xl1524601 style="width: 20pt"></td>
  <td class=xl1524601></td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>

  <td class=xl1524601 style="width: 20pt"></td>
  <td class=xl1524601></td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=34 style='mso-height-source:userset;height:25.5pt'>
  <td colspan=11 height=34 class=xl9724601 width=905 style='border-right:1.0pt solid black;
  height:25.5pt;width:682pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola<br>
    <span style='mso-spacerun:yes'> </span>anomalia&gt;= 60%) –</td>

 </tr>
 <tr height=24 style='mso-height-source:userset;height:18.0pt'>
  <td colspan=11 height=24 class=xl10024601 width=905 style='border-right:1.0pt solid black;
  height:18.0pt;width:682pt'>Per alcuni El. Tecnici potrebbe risultare
  necessario inserire diverse dimensioni in relazione alle diverse tipologie
  costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>

  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601 style="width: 20pt"></td>
  <td class=xl1524601></td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1524601 colspan="9" rowspan="25">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="440px" TextMode="MultiLine" Width="816px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7124601 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>

  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601 style="width: 20pt">&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl7224601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl6824601 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl6924601 style='border-top:none'>&nbsp;</td>

  <td class=xl6924601 style='border-top:none'>&nbsp;</td>
  <td class=xl6924601 style='border-top:none'>&nbsp;</td>
  <td class=xl6924601 style='border-top:none'>&nbsp;</td>
  <td class=xl6924601 style='border-top:none'>&nbsp;</td>
  <td class=xl6924601 style='border-top:none'>&nbsp;</td>
  <td class=xl6924601 style="border-top:none; width: 20pt;">&nbsp;</td>
  <td class=xl6924601 style='border-top:none'>&nbsp;</td>
  <td class=xl7024601 style="border-top:none; width: 20pt;">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl10024601 width=905
  style='border-right:1.0pt solid black;height:25.5pt;width:682pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1524601></td>

  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601></td>
  <td class=xl1524601 style="width: 20pt"></td>
  <td class=xl1524601></td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1524601 colspan="9" rowspan="29">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="512px" TextMode="MultiLine" Width="816px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7624601 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7724601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7124601 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;<asp:Button ID="Button1" runat="server" Text="Button" Visible="False" /></td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>

  <td class=xl6524601 style="width: 20pt">&nbsp;</td>
  <td class=xl6524601>&nbsp;</td>
  <td class=xl7224601 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=0 style='display:none'>
  <td width=61 style='width:46pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=51 style='width:38pt'></td>

  <td width=113 style='width:85pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=141 style='width:106pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td style='width:20pt'></td>
 </tr>


</table>

    </div>
    </form>
</body>
</html>
