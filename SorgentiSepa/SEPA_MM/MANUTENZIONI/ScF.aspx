<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScF.aspx.vb" Inherits="ScF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">

    <title>Scheda F</title>
    <style id="SCHEDE RILIEVO TUTTE_31705_Styles">

	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1531705
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
.xl6531705
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
.xl6631705
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
.xl6731705
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
.xl6831705
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
.xl6931705
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
.xl7031705
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
.xl7131705
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
.xl7231705
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
.xl7331705
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
.xl7431705
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
.xl7531705
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
.xl7631705
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
.xl7731705
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
.xl7831705
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
.xl7931705
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
.xl8031705
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
.xl8131705
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
	border-right:1.0pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8231705
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
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8331705
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
	white-space:normal;}
.xl8431705
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
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8531705
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
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8631705
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
	border-top:1.0pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl8731705
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
	border-top:1.0pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl8831705
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl8931705
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
	border:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9031705
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
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9131705
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
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9231705
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
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9331705
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
.xl9431705
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
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9531705
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
.xl9631705
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
.xl9731705
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
.xl9831705
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
.xl9931705
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
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10031705
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10131705
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10231705
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10331705
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
	border-top:1.0pt solid windowtext;
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10431705
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl10531705
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
	border-top:1.0pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl10631705
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
	border-top:1.0pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl10731705
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
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10831705
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10931705
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
	border-top:1.0pt solid windowtext;
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11031705
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
	border-top:none;
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11131705
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
	border-top:none;
	border-right:1.0pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11231705
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11331705
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
	border-top:1.0pt solid windowtext;
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11431705
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
	border-top:none;
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11531705
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
	border-top:none;
	border-right:1.0pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11631705
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
	border-top:1.0pt solid windowtext;
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11731705
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
	border-top:none;
	border-right:1.0pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
	.xl1157561
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
	border-top:1.0pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}

</style>

<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div align=center x:publishsource="Excel">
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Constantia"
            Font-Size="9pt" ForeColor="#8080FF" Width="504px">Pagina ottimizzata per Internet Explorer 7</asp:Label>
        <table
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
 <table border=0 cellpadding=0 cellspacing=0 width=909 style="border-collapse:
 collapse;table-layout:fixed;width:685pt; border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; border-bottom: black thin solid;" id="TABLE1" onclick="return TABLE1_onclick()">
 <col width=54 style='mso-width-source:userset;mso-width-alt:1974;width:41pt'>
 <col width=131 style='mso-width-source:userset;mso-width-alt:4790;width:98pt'>
 <col width=54 style='mso-width-source:userset;mso-width-alt:1974;width:41pt'>
 <col width=131 style='mso-width-source:userset;mso-width-alt:4790;width:98pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>

 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
<tr height=40 style='mso-height-source:userset;height:30.0pt'>
  <td colspan=9 height=40 class=xl1157561 width=766 style="height:30.0pt;
  width:578pt; background-color: silver;">U. T. ATTREZZATURE E SPAZI INTERNI - SCHEDA RILIEVO ATTREZZATURE E SPAZI INTERNI</td>
  <td colspan=2 class=xl1157561 width=171 style="border-right:black 1pt solid;width:129pt; text-align: center; background-color: silver; border-top-style: solid; border-top-color: black; border-left-style: none;">
      F</td>

 </tr>
 <tr height=66 style='mso-height-source:userset;height:50.1pt'>
  <td height=66 class=xl8731705 style='height:50.1pt;width:41pt'>Scheda
  F</td>
  <td class=xl8631705 width=131 style='border-left:none;width:98pt'>ELEMENTO
  TECNICO</td>

  <td class=xl8631705 width=54 style='border-left:none;width:41pt'>U.M.</td>
  <td colspan=2 class=xl10431705 width=157 style='border-right:.5pt solid black;
  border-left:none;width:118pt'>MATERIALI E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>
  <td class=xl8631705 width=145 style='border-left:none;width:109pt'>ANALISI
  PRESTAZIONALE (X)</td>
  <td class=xl8631705 width=26 style='border-left:none;width:20pt'>&nbsp;</td>
  <td colspan=2 class=xl10431705 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANOMALIE (%)</td>

  <td colspan=2 class=xl10431705 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=34 style='mso-height-source:userset;height:25.5pt'>
  <td rowspan=4 height=102 class=xl10031705 style='height:76.5pt;
  width:41pt'>F 1</td>
  <td rowspan=4 class=xl9331705 width=131 style='width:98pt'>CAVEDI ED
  ALLOGGIAMENTI IMPIANTISTICI<br />
      cad<asp:TextBox ID="Text_mq_F1" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
          ControlToValidate="Text_mq_F1" ErrorMessage="!" Font-Bold="True" Height="1px"
          Style="left: 432px; top: 264px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
          Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8031705 width=54 style="border-left:none;width:41pt; height: 28pt;"></td>
  <td class=xl6631705 style="border-left:none; height: 28pt;">cavedi ispezionabili</td>

  <td class=xl8031705 style="border-left:none;width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_F1_cavedi0ispezionabili" runat="server" Text="." /></td>
  <td class=xl6631705 style="border-left:none; height: 28pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl6631705 style="border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_ANA_F1_nuovo" runat="server" Text="." /></td>
  <td class=xl8331705 width=145 style="border-left:none;width:109pt; height: 28pt;">sporco
  all'interno dai cavedi</td>
  <td class=xl8031705 style="border-left:none;width:40pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_F1_sporco0cavedi" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl8331705 width=145 style="border-left:none;width:109pt; height: 28pt;">pulizia
  cavedi</td>
  <td class=xl8131705 style="border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_INT_F1_pulizia0cavedi" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl8231705 width=54 style="height:32pt;border-top:none;
  border-left:none;width:41pt">&nbsp;</td>
  <td class=xl8331705 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 32pt;">cavedi non ispezionabili</td>
  <td class=xl8031705 style="border-top:none;border-left:none;
  width:20pt; height: 32pt;">
      <asp:CheckBox ID="C_MAT_F1_cavedi0non" runat="server" Text="." />&nbsp;</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 32pt;">2. lieve
  obsolescenza</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 32pt;">
      <asp:CheckBox ID="C_ANA_F1_lieve" runat="server" Text="." /></td>
  <td class=xl8331705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">mancanza protezione cavedi</td>

  <td class=xl8031705 style="border-top:none;border-left:none;
  width:40pt; height: 32pt;">&nbsp;<asp:TextBox ID="T_ANO_F1_mancanza0proetzione" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl8331705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">messa in sicurezza cavedi</td>
  <td class=xl8131705 style="border-top:none;border-left:none; height: 32pt;">
      <asp:CheckBox ID="C_INT_F1_sicurezza0cavedi" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6731705 width=54 style="height:22pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 22pt;">impianti a vista</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:20pt; height: 22pt;">
      <asp:CheckBox ID="C_MAT_F1_impianti0vista" runat="server" Text="." /></td>

  <td class=xl6631705 style="border-top:none;border-left:none; height: 22pt;">3. forte
  obsolescenza</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_ANA_F1_forte" runat="server" Text="." /></td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 22pt;">accessibili</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:40pt; height: 22pt;">
      <asp:TextBox ID="T_ANO_F1_accessibili" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 22pt;">&nbsp;</td>
  <td class=xl8131705 style="border-top:none;border-left:none; height: 22pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl6731705 width=54 style="height:21pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 21pt;">&nbsp;</td>
  <td class=xl8031705 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">&nbsp;</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 21pt;">4. totale
  obsolescenza</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 21pt;">
      <asp:CheckBox ID="C_ANA_F1_tot" runat="server" Text="." /></td>
  <td class=xl8031705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">&nbsp;</td>
  <td class=xl8031705 style="border-top:none;border-left:none;
  width:40pt; height: 21pt;">&nbsp;</td>
  <td class=xl8331705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">&nbsp;</td>

  <td class=xl8131705 style="border-top:none;border-left:none; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=6 height=119 class=xl10031705 style='height:89.25pt;
  width:41pt'>F2</td>
  <td rowspan=6 class=xl9331705 width=131 style='width:98pt'>CANNE DI CADUTA
  RIFIUTI<br />
      ml<asp:TextBox ID="Text_mq_F2" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_F2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8031705 width=54 style="border-left:none;width:41pt; height: 26pt;"></td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 26pt;">attiva</td>

  <td class=xl6831705 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:CheckBox ID="C_MAT_F2_attiva" runat="server" Text="." />&nbsp;</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 26pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_ANA_F2_nuovo" runat="server" Text="." /></td>
  <td class=xl8331705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">ostruzioni</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:40pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_F2_ostruzioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl8331705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">disostruzione canna di caduta</td>
  <td class=xl8131705 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_INT_F2_disostruzione0caduta" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl8231705 width=54 style="height:31pt;border-top:none;
  border-left:none;width:41pt">&nbsp;</td>
  <td class=xl8331705 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 31pt;">disattiva</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:CheckBox ID="C_MAT_F2_disattiva" runat="server" Text="." /></td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 31pt;">2. lieve
  obsolescenza</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 31pt;">
      <asp:CheckBox ID="C_ANA_F2_lieve" runat="server" Text="." /></td>
  <td class=xl8331705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">sporco</td>

  <td class=xl6831705 style="border-top:none;border-left:none;
  width:40pt; height: 31pt;">
      <asp:TextBox ID="T_ANO_F2_sporco" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 31pt;">pulizia
  /disinfestazione</td>
  <td class=xl8131705 style="border-top:none;border-left:none; height: 31pt;">
      <asp:CheckBox ID="C_INT_F2_pulizia0disinfestazione" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6731705 width=54 style="height:30pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl8031705 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 30pt;">&nbsp;</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">&nbsp;</td>

  <td class=xl6631705 style="border-top:none;border-left:none; height: 30pt;">3. forte
  obsolescenza</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_ANA_F2_forte" runat="server" Text="." /></td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 30pt;">atti vandalici</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:40pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_F2_atti0vandalici" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl8331705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">ripristino sigillature</td>
  <td class=xl8131705 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_F2_ripristino0sigillature" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl6731705 width=54 style="height:23pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl6831705 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 23pt;">&nbsp;</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">&nbsp;</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 23pt;">4. totale
  obsolescenza</td>
  <td class=xl6631705 style="border-top:none;border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_ANA_F2_tot" runat="server" Text="." /></td>
  <td class=xl8031705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">&nbsp;</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:40pt; height: 23pt;">&nbsp;</td>

  <td class=xl8031705 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">&nbsp;</td>
  <td class=xl8131705 style="border-top:none;border-left:none; height: 23pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl6731705 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl6831705 width=131 style='border-top:none;border-left:none;
  width:98pt'>&nbsp;</td>
  <td class=xl6831705 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td class=xl6631705 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl6631705 style='border-top:none;border-left:none'>&nbsp;</td>

  <td class=xl8031705 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl6831705 style="border-top:none;border-left:none;
  width:40pt">&nbsp;</td>
  <td class=xl8031705 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl8131705 style='border-top:none;border-left:none'>&nbsp;</td>
 </tr>
 <tr height=17 style='mso-height-source:userset;height:12.75pt'>
  <td height=17 class=xl6731705 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl8231705 width=131 style='border-top:none;border-left:none;
  width:98pt'>&nbsp;</td>
  <td class=xl8231705 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>

  <td class=xl8431705 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl8431705 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl7931705 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl8231705 style="border-top:none;border-left:none;
  width:40pt">&nbsp;</td>
  <td class=xl8231705 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl8531705 style='border-top:none;border-left:none'>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td colspan=2 rowspan=2 height=36 class=xl10831705 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black;height:27.0pt'>STATO DI DEGRADO</td>

  <td colspan=2 rowspan=2 class=xl11231705 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black'>Stato 1</td>
  <td rowspan=2 class=xl9031705 style="border-bottom:1.0pt solid black; width: 20pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" />&nbsp;</td>
  <td class=xl8831705 style="height: 21pt">Stato 2.1</td>
  <td class=xl8931705 style="vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8831705 style="height: 21pt">Stato 3.1</td>
  <td class=xl8931705 style="width: 40pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl11631705 style='border-bottom:1.0pt solid black'>Stato
  3.3</td>

  <td class=xl9031705 style="border-left:none; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl9131705 style="height:20pt">Stato 2.2</td>
  <td class=xl8931705 style="border-top:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9131705 style="height: 20pt">Stato 3.2</td>
  <td class=xl8931705 style="border-top:none; width: 40pt; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9231705 style="border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl10131705 width=909 style='border-right:1.0pt solid black;
  height:12.75pt;width:685pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7431705 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7531705></td>

  <td class=xl7531705 style="width: 20pt"></td>
  <td class=xl7531705></td>
  <td class=xl7531705></td>
  <td class=xl7531705></td>
  <td class=xl7531705 style="width: 40pt"></td>
  <td class=xl7531705></td>
  <td class=xl7631705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl1531705></td>
  <td class=xl1531705></td>
  <td class=xl1531705></td>
  <td class=xl1531705 style="width: 20pt"></td>
  <td class=xl1531705></td>
  <td class=xl1531705></td>
  <td class=xl1531705></td>
  <td class=xl1531705 style="width: 40pt"></td>

  <td class=xl1531705></td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1531705></td>
  <td class=xl1531705></td>
  <td class=xl1531705 style="width: 40pt"></td>

  <td class=xl1531705></td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=32 style='mso-height-source:userset;height:24.0pt'>
  <td colspan=11 height=32 class=xl9531705 width=909 style='border-right:1.0pt solid black;
  height:24.0pt;width:685pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola<br>
    <span style='mso-spacerun:yes'> </span>anomalia&gt;= 60%) –</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 colspan=10 style='height:12.75pt'>Per alcuni
  El. Tecnici potrebbe risultare necessario inserire diverse dimensioni in
  relazione alle diverse tipologie costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl1531705 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="464px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7831705>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7231705 style="height:13.5pt; width: 41pt;">&nbsp;</td>
  <td class=xl6531705>&nbsp;</td>
  <td class=xl6531705>&nbsp;</td>
  <td class=xl6531705>&nbsp;</td>
  <td class=xl6531705 style="width: 20pt">&nbsp;</td>
  <td class=xl6531705>&nbsp;</td>
  <td class=xl6531705>&nbsp;</td>

  <td class=xl6531705>&nbsp;</td>
  <td class=xl6531705 style="width: 40pt">&nbsp;</td>
  <td class=xl6531705>&nbsp;</td>
  <td class=xl7331705>&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl6931705 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl7031705 style='border-top:none'>&nbsp;</td>

  <td class=xl7031705 style='border-top:none'>&nbsp;</td>
  <td class=xl7031705 style="border-top:none; width: 20pt;">&nbsp;</td>
  <td class=xl7031705 style='border-top:none'>&nbsp;</td>
  <td class=xl7031705 style='border-top:none'>&nbsp;</td>
  <td class=xl7031705 style='border-top:none'>&nbsp;</td>
  <td class=xl7031705 style="border-top:none; width: 40pt;">&nbsp;</td>
  <td class=xl7031705 style='border-top:none'>&nbsp;</td>
  <td class=xl7131705 style='border-top:none'>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl9831705 width=909
  style='border-right:1.0pt solid black;height:25.5pt;width:685pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl1531705 colspan="9" rowspan="30">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="536px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7831705>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7731705 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7831705>&nbsp;</td>
 </tr>

 <tr height=18 style='height:13.5pt'>
  <td class=xl7231705 style="height:14pt; width: 41pt;">&nbsp;</td>
  <td class=xl6531705 style="height: 14pt">&nbsp;</td>
  <td class=xl6531705 style="height: 14pt">&nbsp;</td>
  <td class=xl6531705 style="height: 14pt">&nbsp;</td>
  <td class=xl6531705 style="width: 20pt; height: 14pt">&nbsp;</td>
  <td class=xl6531705 style="height: 14pt">&nbsp;<asp:Button ID="Button1" runat="server" Text="Button" Visible="False" /></td>
  <td class=xl6531705 style="height: 14pt">&nbsp;</td>
  <td class=xl6531705 style="height: 14pt">&nbsp;</td>

  <td class=xl6531705 style="width: 40pt; height: 14pt;">&nbsp;</td>
  <td class=xl6531705 style="height: 14pt">&nbsp;</td>
  <td class=xl7331705 style="height: 14pt">&nbsp;</td>
 </tr>
 
 <tr height=0 style='display:none'>
  <td style='width:41pt'></td>
  <td width=131 style='width:98pt'></td>
  <td width=54 style='width:41pt'></td>

  <td width=131 style='width:98pt'></td>
  <td style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td style="width:40pt"></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
 </tr>


</table>
        <br />

   
    </div>
    </form>
</body>
</html>
