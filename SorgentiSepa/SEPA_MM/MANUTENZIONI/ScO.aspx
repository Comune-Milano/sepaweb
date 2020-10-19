<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScO.aspx.vb" Inherits="ScO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
<meta name=ProgId content=Excel.Sheet>
<meta name=Generator content="Microsoft Excel 12">
    <title>Scheda O</title>
<style id="SCHEDE RILIEVO TUTTE_18873_Styles">
	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1518873
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
.xl6518873
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
.xl6618873
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
.xl6718873
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
.xl6818873
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
.xl6918873
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
.xl7018873
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
.xl7118873
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
.xl7218873
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
.xl7318873
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
.xl7418873
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
.xl7518873
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
.xl7618873
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
.xl7718873
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
.xl7818873
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
.xl7918873
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
.xl8018873
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
.xl8118873
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
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8218873
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
.xl8318873
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
.xl8418873
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8518873
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
.xl8618873
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
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8718873
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
.xl8818873
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
	border-top:1.0pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;

	white-space:normal;}
.xl8918873
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
.xl9018873
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9118873
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
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9218873
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
.xl9318873
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9418873
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9518873
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
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9618873
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
	white-space:normal;}
.xl9718873
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
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9818873
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
.xl9918873
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl10018873
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
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl10118873
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
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10218873
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
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10318873
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10418873
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
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl10518873
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl10618873
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
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10718873
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
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10818873
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl10918873
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
.xl11018873
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11118873
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11218873
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
	border-top:1.0pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11318873
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
.xl11418873
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
.xl11518873
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
.xl11618873
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
.xl11718873
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11818873
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
.xl11918873
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
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl12018873
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
.xl12118873
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
.xl12218873
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
.xl12318873
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
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12418873
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12518873
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12618873
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
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl12718873
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
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl12818873
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
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl12918873
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
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl13018873
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
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl13118873
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
	vertical-align:middle;
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl13218873
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
	vertical-align:middle;
	border-top:1.0pt solid windowtext;
	border-right:1.0pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl13318873
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl13418873
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
	white-space:normal;}
.xl13518873
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
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl13618873
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
	border-bottom:.5pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl13718873
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl13818873
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
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl13918873
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
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}

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
                            Style="left: 120px; top: 24px" ToolTip="ESCI" />&nbsp;
                    </td>
                </tr>
            </table>
        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
            Style="text-align: left" Visible="False" Width="652px"></asp:Label><br />
    <table border=0 cellpadding=0 cellspacing=0 width=909 style='border-collapse:
 collapse;table-layout:fixed;width:685pt'>
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
  <td colspan=11 height=40 class=xl12918873 width=909 style='border-right:1.0pt solid black;
  height:30.0pt;width:685pt'>U. T. SISTEMAZIONE ESTERNE - SCHEDA RILIEVO
  IMPIANTI SMALTIMENTO AERIFORMI<span
  style='mso-spacerun:yes'> &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
      &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span>O</td>

 </tr>
 <tr height=68 style='mso-height-source:userset;height:51.0pt'>
  <td height=68 class=xl10418873 width=54 style='height:51.0pt;border-top:none;
  width:41pt'>Scheda O</td>
  <td class=xl10518873 width=131 style='border-top:none;border-left:none;
  width:98pt'>ELEMENTO TECNICO</td>
  <td class=xl10518873 width=54 style='border-top:none;border-left:none;
  width:41pt'></td>
  <td colspan=2 class=xl10518873 width=157 style='border-left:none;width:118pt'>MATERIALI
  E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td colspan=2 class=xl12718873 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl10518873 width=171 style='border-left:none;width:129pt'>ANOMALIE
  (%)</td>
  <td colspan=2 class=xl10518873 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=6 height=122 class=xl11718873 width=54 style='border-bottom:1.0pt solid black;
  height:91.5pt;border-top:none;width:41pt'>O1</td>
  <td rowspan=6 class=xl11118873 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>CANNE/ CONDUTTURE DI ESALAZIONE<br />
      ml<asp:TextBox ID="Text_mq_O1" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_O1"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>

  <td rowspan=6 class=xl11218873 width=54 style='border-bottom:1.0pt solid black;
  border-top:none;width:41pt'></td>
  <td class=xl9518873 width=131 style='border-top:none;border-left:none;
  width:98pt'>singola<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8818873 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_O1_singola" runat="server" Text="." />&nbsp;</td>
  <td class=xl8118873 style='border-top:none;border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8118873 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_O1_nuovo" runat="server" Text="." /></td>
  <td class=xl9518873 width=145 style='border-top:none;border-left:none;
  width:109pt'>sospetta presenza amianto</td>

  <td class=xl8818873 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_O1_sospetta0amianto" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl9518873 width=145 style='border-top:none;border-left:none;
  width:109pt'>analisi amianto</td>
  <td class=xl9018873 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_O1_analisi0amianto" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl8718873 width=131 style="height:14pt;border-top:none;
  border-left:none;width:98pt">collettiva</td>
  <td class=xl7918873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 14pt;">
      <asp:CheckBox ID="C_MAT_O1_collettiva" runat="server" Text="." /></td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 14pt;">2. lieve
  obsolescenza</td>

  <td class=xl6618873 style="border-top:none;border-left:none; height: 14pt;">
      <asp:CheckBox ID="C_ANA_O1_lieve" runat="server" Text="." /></td>
  <td class=xl8718873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 14pt;">fessurazioni strutturali</td>
  <td class=xl7918873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 14pt;">
      <asp:TextBox ID="T_ANO_O1_fessurazioni0strutturali" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl8718873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 14pt;">riparazione fessure</td>
  <td class=xl8018873 style="border-top:none;border-left:none; height: 14pt;">
      <asp:CheckBox ID="C_INT_O1_riparazione0fessure" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7818873 width=131 style="height:26pt;border-top:
  none;border-left:none;width:98pt">&nbsp;</td>

  <td class=xl6718873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;"></td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 26pt;">3. forte
  obsolescenza</td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_ANA_O1_forte" runat="server" Text="." /></td>
  <td class=xl9518873 width=145 style="border-left:none;width:109pt; height: 26pt;">instabilità
  strutturale</td>
  <td class=xl6718873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_O1_instabilità0strutturale" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9518873 width=145 style="border-left:none;width:109pt; height: 26pt;">sostituzione
  elemento</td>
  <td class=xl8018873 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_INT_O1_sostituzione0elemento" runat="server" Text="." /></td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl8618873 width=131 style="height:26pt;border-left:
  none;width:98pt">&nbsp;</td>
  <td class=xl7918873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">&nbsp;</td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 26pt;">4. totale
  obsolescenza</td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_ANA_O1_totale" runat="server" Text="." /></td>
  <td class=xl8718873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">rottura camino</td>
  <td class=xl7918873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_O1_rottura0camino" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl8718873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">strutture di rinforzo</td>
  <td class=xl8018873 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_INT_O1_strutture0rinforzo" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl8618873 width=131 style="height:21pt;border-left:
  none;width:98pt">&nbsp;</td>
  <td class=xl7918873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">&nbsp;</td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 21pt;">&nbsp;</td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 21pt;">&nbsp;</td>

  <td class=xl7918873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">&nbsp;</td>
  <td class=xl7918873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">&nbsp;</td>
  <td class=xl9518873 width=145 style="border-left:none;width:109pt; height: 21pt;">monitoraggio/video</td>
  <td class=xl8018873 style="border-top:none;border-left:none; height: 21pt;">
      <asp:CheckBox ID="C_INT_O1_monitoraggio0video" runat="server" Text="." /></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl9118873 width=131 style="height:23pt;border-left:
  none;width:98pt">&nbsp;</td>
  <td class=xl8918873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">&nbsp;</td>

  <td class=xl9218873 style="border-top:none;border-left:none; height: 23pt;">&nbsp;</td>
  <td class=xl9218873 style="border-top:none;border-left:none; height: 23pt;">&nbsp;</td>
  <td class=xl8918873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">&nbsp;</td>
  <td class=xl8918873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">&nbsp;</td>
  <td class=xl9618873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">ispezione</td>
  <td class=xl9318873 style="border-top:none;border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_INT_O1_ispezione" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>

  <td rowspan=4 height=87 class=xl11718873 width=54 style='border-bottom:1.0pt solid black;
  height:65.25pt;border-top:none;width:41pt'>O 2</td>
  <td rowspan=4 class=xl11118873 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>TORRINI DI ESALAZIONE<br />
      cad<asp:TextBox ID="Text_mq_O2" runat="server" MaxLength="9" Width="90px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_O2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=4 class=xl11218873 width=54 style='border-bottom:1.0pt solid black;
  border-top:none;width:41pt'></td>
  <td class=xl9518873 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 28pt;">elettrici</td>
  <td class=xl9418873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_O2_elettrici" runat="server" Text="." /></td>
  <td class=xl8118873 style="border-top:none;border-left:none; height: 28pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl8118873 style="border-top:none;border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_ANA_O2_nuovo" runat="server" Text="." /></td>
  <td class=xl9518873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">malfunzionamento torrino<span style='mso-spacerun:yes'> </span></td>
  <td class=xl9418873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_O2_malfunzionamento0torrino" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl9518873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">revisione torrino</td>
  <td class=xl9018873 style="border-top:none;border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_INT_O2_revisione0torrino" runat="server" Text="." /></td>
 </tr>
 <tr height=18 style='height:13.5pt'>

  <td class=xl8718873 width=131 style="height:23pt;border-top:none;
  border-left:none;width:98pt">naturali</td>
  <td class=xl6718873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">
      <asp:CheckBox ID="C_MAT_O2_naturali" runat="server" Text="." /></td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 23pt;">2. lieve
  obsolescenza</td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_ANA_O2_lieve" runat="server" Text="." /></td>
  <td class=xl8718873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">rottura elementi</td>
  <td class=xl6718873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">
      <asp:TextBox ID="T_ANO_O2_rottura0elementi" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl8718873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">sostituzione torrino</td>

  <td class=xl8018873 style="border-top:none;border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_INT_O2_sostituzione0torrino" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7818873 width=131 style="height:36pt;border-top:
  none;border-left:none;width:98pt">&nbsp;</td>
  <td class=xl6718873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 36pt;">&nbsp;</td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 36pt;">3. forte
  obsolescenza</td>
  <td class=xl6618873 style="border-top:none;border-left:none; height: 36pt;">
      <asp:CheckBox ID="C_ANA_O2_forte" runat="server" Text="." /></td>
  <td class=xl9518873 width=145 style="border-left:none;width:109pt; height: 36pt;">instabilità
  torrini</td>

  <td class=xl6718873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 36pt;">
      <asp:TextBox ID="T_ANO_O2_instabilita0torrini" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9518873 width=145 style="border-left:none;width:109pt; height: 36pt;">strutture
  di rinforzo</td>
  <td class=xl8018873 style="border-top:none;border-left:none; height: 36pt;">
      <asp:CheckBox ID="C_INT_O2_strutture0rinforzo" runat="server" Text="." /></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl8218873 width=131 style="height:19pt;border-left:
  none;width:98pt">&nbsp;</td>
  <td class=xl8318873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 19pt;">&nbsp;</td>
  <td class=xl9218873 style="border-top:none;border-left:none; height: 19pt;">4. totale
  obsolescenza</td>

  <td class=xl9218873 style="border-top:none;border-left:none; height: 19pt;">
      <asp:CheckBox ID="C_ANA_O2_totale" runat="server" Text="." /></td>
  <td class=xl8918873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 19pt;">&nbsp;</td>
  <td class=xl8318873 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 19pt;">&nbsp;</td>
  <td class=xl8918873 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 19pt;">&nbsp;</td>
  <td class=xl9318873 style="border-top:none;border-left:none; height: 19pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=2 rowspan=2 height=35 class=xl13618873 style='border-bottom:1.0pt solid black;
  height:26.25pt'>STATO DI DEGRADO</td>

  <td colspan=2 rowspan=2 class=xl13818873 style='border-bottom:1.0pt solid black'>Stato
  1</td>
  <td rowspan=2 class=xl10718873 style="border-bottom:1.0pt solid black;
  border-top:none; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl10618873 style="border-top:none;border-left:none; height: 23pt;">Stato 2.1</td>
  <td class=xl10718873 style="border-top:none;border-left:none; height: 23pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl10618873 style="border-top:none;border-left:none; height: 23pt;">Stato 3.1</td>
  <td class=xl10718873 style="border-top:none;border-left:none; height: 23pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl13818873 style='border-bottom:1.0pt solid black;
  border-top:none'>Stato 3.3</td>

  <td class=xl10818873 style="border-top:none;border-left:none; height: 23pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl10118873 style="height:22pt;border-top:none;
  border-left:none">Stato 2.2</td>
  <td class=xl10218873 style="border-top:none;border-left:none; height: 22pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl10118873 style="border-top:none;border-left:none; height: 22pt;">Stato 3.2</td>
  <td class=xl10218873 style="border-top:none;border-left:none; height: 22pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl10318873 style="border-top:none;border-left:none; height: 22pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl12018873 width=909 style='border-right:1.0pt solid black;
  height:12.75pt;width:685pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7318873 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7418873></td>

  <td class=xl7418873></td>
  <td class=xl7418873></td>
  <td class=xl7418873></td>
  <td class=xl7418873></td>
  <td class=xl7418873></td>
  <td class=xl7418873></td>
  <td class=xl7518873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>

  <td class=xl1518873></td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>

  <td class=xl1518873></td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=30 style='mso-height-source:userset;height:22.5pt'>
  <td colspan=11 height=30 class=xl11318873 width=909 style='border-right:1.0pt solid black;
  height:22.5pt;width:685pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola<br>
    <span style='mso-spacerun:yes'> </span>anomalia&gt;= 60%) –</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl9918873 colspan=10 style='height:12.75pt'>Per alcuni
  El. Tecnici potrebbe risultare necessario inserire diverse dimensioni in
  relazione alle diverse tipologie costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
  <td class=xl10018873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>

  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl1518873></td>
  <td class=xl7718873>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1518873 colspan="9" rowspan="23">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="400px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7118873 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>

  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl7218873>&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl6818873 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>

  <td class=xl6918873 style='border-top:none'>&nbsp;</td>
  <td class=xl6918873 style='border-top:none'>&nbsp;</td>
  <td class=xl6918873 style='border-top:none'>&nbsp;</td>
  <td class=xl6918873 style='border-top:none'>&nbsp;</td>
  <td class=xl6918873 style='border-top:none'>&nbsp;</td>
  <td class=xl6918873 style='border-top:none'>&nbsp;</td>
  <td class=xl6918873 style='border-top:none'>&nbsp;</td>
  <td class=xl6918873 style='border-top:none'>&nbsp;</td>
  <td class=xl7018873 style='border-top:none'>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl11618873 width=909
  style='border-right:1.0pt solid black;border-bottom:.5pt solid black;
  height:25.5pt;width:685pt'>Inserire il repertorio fotografico (con
  precisazione della postazione e collegamento con el.tecnico/anomalia) e le
  note inerenti ad anomalie non previste nella scheda di rilievo, nonché le
  segnalazioni di eventuali difficoltà logistiche (ad esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl8418873 style='height:12.75pt;border-top:none'>&nbsp;</td>

  <td class=xl8518873 style='border-top:none'>&nbsp;</td>
  <td class=xl8518873 style='border-top:none'>&nbsp;</td>
  <td class=xl8518873 style='border-top:none'>&nbsp;</td>
  <td class=xl8518873 style='border-top:none'>&nbsp;</td>
  <td class=xl8518873 style='border-top:none'>&nbsp;</td>
  <td class=xl8518873 style='border-top:none'>&nbsp;</td>
  <td class=xl8518873 style='border-top:none'>&nbsp;</td>
  <td class=xl8518873 style='border-top:none'>&nbsp;</td>
  <td class=xl8518873 style='border-top:none'>&nbsp;</td>

  <td class=xl9718873 style='border-top:none'>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1518873 colspan="9" rowspan="29">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="512px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl7618873 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7718873>&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7118873 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>
      <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>

  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl6518873>&nbsp;</td>
  <td class=xl7218873>&nbsp;</td>
 </tr>

 <tr height=0 style='display:none'>
  <td width=54 style='width:41pt'></td>
  <td width=131 style='width:98pt'></td>

  <td width=54 style='width:41pt'></td>
  <td width=131 style='width:98pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>

 </tr>

</table>


    </div>
    </form>
</body>
</html>
