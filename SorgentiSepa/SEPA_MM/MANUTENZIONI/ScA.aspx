<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScA.aspx.vb" Inherits="ScA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Scheda A</title>
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">
    <style id="SCHEDE RILIEVO TUTTE_13694_Styles">
	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1513694
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
.xl6513694
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
.xl6613694
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
.xl6713694
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
.xl6813694
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
.xl6913694
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
.xl7013694
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
.xl7113694
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
.xl7213694
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
.xl7313694
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
.xl7413694
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
.xl7513694
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
.xl7613694
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
.xl7713694
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
.xl7813694
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl7913694
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8013694
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
.xl8113694
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8213694
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
.xl8313694
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8413694
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
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8513694
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
.xl8613694
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
.xl8713694
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
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8813694
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
.xl8913694
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9013694
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
	mso-protection:unlocked visible;
	white-space:normal;}
.xl9113694
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
	white-space:nowrap;}
.xl9213694
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
.xl9313694
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:12.0pt;
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
.xl9413694
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
.xl9513694
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
	text-align:center;
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9613694
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
	text-align:center;
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl9713694
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl9813694
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
	border-bottom:.5pt solid windowtext;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl9913694
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
.xl10013694
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
.xl10113694
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
.xl10213694
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
.xl10313694
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
.xl10413694
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
.xl10513694
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
.xl10613694
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
.xl10713694
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
.xl10813694
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
.xl10913694
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11013694
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11113694
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
.xl11213694
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
.xl11313694
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
.xl11413694
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
	text-align:center;
	vertical-align:middle;
	border:.5pt solid windowtext;
	background:#969696;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11513694
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:12.0pt;
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
.xl11613694
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
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl11713694
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
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl11813694
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
.xl11913694
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
.xl12013694
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
.xl12113694
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
.xl12213694
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
.xl12313694
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
.xl12413694
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
        &nbsp;
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Constantia"
            Font-Size="9pt" ForeColor="#8080FF" Width="504px">Pagina ottimizzata per Internet Explorer 7</asp:Label>
        <br />
        <table style="width: 100%; background-color: gainsboro">
            <tr>
                <td style="width: 223px; height: 57px; text-align: center;">
                    &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png" style="left: 120px; top: 24px" ToolTip="SALVA" /></td>
                <td style="height: 57px; text-align: center;">
                    &nbsp;&nbsp;
                </td>
                <td style="width: 164px; height: 57px; text-align: center;">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" style="left: 120px; top: 24px" ToolTip="ESCI" />&nbsp;
                </td>
            </tr>
        </table>
        <asp:Label ID="Label1" runat="server" Visible="False" Width="652px" style="text-align: left" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"></asp:Label><br />
    <table border=0 cellpadding=0 cellspacing=0 width=944 style="border-collapse:
 collapse;table-layout:fixed;width:711pt; border-right: black thin solid; border-top: black thin solid; border-left: black thin solid; border-bottom: black thin solid;" id="TABLE1" onclick="return TABLE1_onclick()">
 <col width=40 style='mso-width-source:userset;mso-width-alt:1462;width:30pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=40 style='mso-width-source:userset;mso-width-alt:1462;width:30pt'>
 <col width=180 style='mso-width-source:userset;mso-width-alt:6582;width:135pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <tr height=40 style='mso-height-source:userset;height:30.0pt'>
  <td colspan=9 class=xl9413694 width=773 style='height:30.0pt;
  width:582pt'>U. T. STRUTTURE - SCHEDA RILIEVO STRUTTURE</td>
  <td colspan=2 class=xl9513694 width=171 style="border-right:.5pt solid black;
  border-left:none;width:129pt; height: 30pt;">A</td>

 </tr>
 <tr height=34 style='mso-height-source:userset;height:26.1pt'>
  <td class=xl9213694 style="height:39pt;border-top:none; width: 12273pt;">Sc. A</td>
  <td class=xl9213694 style="border-top:none;border-left:none;
  width:109pt; height: 39pt;">ELEMENTO TECNICO</td>
  <td class=xl9213694 style="border-top:none;border-left:none;
  width:366pt; height: 39pt;">
      </td>
  <td colspan=2 class=xl9213694 width=206 style="border-left:none;width:155pt; height: 39pt;">MATERIALI
  E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td colspan=2 class=xl9713694 width=171 style="border-right:.5pt solid black;
  border-left:none;width:129pt; height: 39pt;">ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl9213694 width=171 style="border-left:none;width:129pt; height: 39pt;">ANOMALIE
  (%)</td>
  <td colspan=2 class=xl9213694 width=171 style="border-left:none;width:129pt; height: 39pt;">
      INTERVENTI(X)</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=4 height=86 class=xl10113694 style="border-bottom:1.0pt solid black;
  height:64.5pt;border-top:none; width: 12273pt;">
      A1</td>
  <td rowspan=4 class=xl10113694 style='border-bottom:1.0pt solid black;
  border-top:none;width:109pt'>FONDAZIONI<br />
  </td>

  <td rowspan=4 class=xl10413694 style="border-bottom:1.0pt solid black;
  border-top:none;width:366pt">&nbsp;</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:125pt; height: 33pt;">continue</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:177pt; height: 33pt; text-align: left;">
      <asp:CheckBox ID="ChkA1_MAT_Continue" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" ToolTip="a1_materiali&continue" /></td>
  <td class=xl6613694 style="border-top:none;border-left:none; height: 33pt; width: 109pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl6613694 style="border-top:none;border-left:none; height: 33pt; width: 75pt;"><asp:CheckBox ID="ChkA1_ANA_Nuovo" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:123pt; height: 33pt;">riconducibili a cedimenti strutturali</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:10pt; height: 33pt; text-align: left;">&nbsp;
      <asp:TextBox ID="Txt_A1_Cedim_Strutt" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl7713694 style="border-top:none;border-left:none;
  width:112pt; height: 33pt;">monitoraggio</td>
  <td class=xl8213694 style="border-top:none;border-left:none; height: 33pt; width:10107pt; position: static; text-align: left;" align="center" valign="middle" rowspan=""><asp:CheckBox ID="ChkA1_INT_monitoraggio" runat="server" Height="24px" Width="1px" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7713694 style="height:20pt;border-top:
  none;border-left:none;width:125pt">discontinue</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkA1_MATDiscont" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" ToolTip="a1_materiali&discontinue" /></td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 109pt; height: 20pt;">2. lieve
  obsolescenza</td>

  <td class=xl6613694 style="border-top:none;border-left:none; width: 75pt; height: 20pt;"><asp:CheckBox ID="ChkA1_ANA_LieveObsolescenza" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:123pt; height: 20pt;">&nbsp;</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:10pt; height: 20pt;">&nbsp;</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:112pt; height: 20pt;">&nbsp;</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 20pt;" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:20pt;border-top:
  none;border-left:none;width:125pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 20pt;">
      </td>

  <td class=xl6613694 style="border-top:none;border-left:none; width: 109pt; height: 20pt;">3. forte
  obsolescenza</td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 75pt; height: 20pt;"><asp:CheckBox ID="ChkA1_ANA_ForteObsolescenza" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 20pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 20pt;">&nbsp;</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:112pt; height: 20pt;">&nbsp;</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 20pt;" rowspan="">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>

  <td class=xl8513694 style="height:22pt;border-top:none;
  border-left:none;width:125pt">&nbsp;</td>
  <td class=xl8513694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 22pt;"></td>
  <td class=xl8613694 style="border-top:none;border-left:none; width: 109pt; height: 22pt;">4. totale
  obsolescenza</td>
  <td class=xl8613694 style="border-top:none;border-left:none; width: 75pt; height: 22pt;"><asp:CheckBox ID="ChkA1_ANA_TotObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8513694 style="border-top:none;border-left:none;
  width:123pt; height: 22pt;">&nbsp;</td>
  <td class=xl8513694 style="border-top:none;border-left:none;
  width:10pt; height: 22pt;">&nbsp;</td>
  <td class=xl8513694 style="border-top:none;border-left:none;
  width:112pt; height: 22pt;">&nbsp;</td>
  <td class=xl8813694 style="border-top:none;border-left:none; width: 10107pt; height: 22pt;" rowspan="">&nbsp;</td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=5 height=137 class=xl10713694 style="border-bottom:1.0pt solid black;
  height:102.75pt;border-top:none; width: 12273pt;">
      A2</td>
  <td rowspan=5 class=xl10713694 style='border-bottom:1.0pt solid black;
  border-top:none;width:109pt'>STRUTTURE IN ELEVAZIONE (PILASTRI, MURATURE,
  SETTI)<br />
      mc<asp:TextBox ID="Text_mq_A2" runat="server" MaxLength="9" Width="102px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_A2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator>
  </td>
  <td rowspan=5 class=xl10813694 style="border-bottom:1.0pt solid black;
  border-top:none;width:366pt">
      &nbsp;</td>
  <td class=xl8313694 style="border-left:none;width:125pt; height: 32pt;">muratura
  portante</td>
  <td class=xl8113694 style="border-left:none;width:177pt; text-align: left; height: 32pt;"><asp:CheckBox ID="ChkA2_MAT_MurPort" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td class=xl8413694 style="border-left:none; width: 109pt; height: 32pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8413694 style="border-left:none; width: 75pt; height: 32pt;"><asp:CheckBox ID="ChkA2_ANA_Nuovo" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8313694 style="border-left:none;width:123pt; height: 32pt;">riconducibili
  a cedimenti strutturali</td>
  <td class=xl8113694 style="border-left:none;width:10pt; height: 32pt;">&nbsp;
      <asp:TextBox ID="Txt_A2_Cedim_Strutt" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl8313694 style="border-left:none;width:112pt; height: 32pt;">demolizione</td>
  <td class=xl8913694 style="border-left:none; width: 10107pt; height: 32pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA2_INT_demolizione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7713694 style="height:32pt;border-top:
  none;border-left:none;width:125pt">cls armato</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 32pt; text-align: left;"><asp:CheckBox ID="ChkA2_MAT_ClsArmato" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 109pt; height: 32pt;">2. lieve
  obsolescenza</td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 75pt; height: 32pt;"><asp:CheckBox ID="ChkA2_ANA_LieveObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:123pt; height: 32pt;">distacchi e/o rotture</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 32pt;">&nbsp;
      <asp:TextBox ID="Txt_A2_Distacchi_rotture" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl7713694 style="border-top:none;border-left:none;
  width:112pt; height: 32pt;">rimozione cls</td>
  <td class=xl8213694 style="border-top:none;border-left:none; height: 32pt; width: 10107pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA2_INT_Rimoz_Cls" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7713694 style="height:32pt;border-top:
  none;border-left:none;width:125pt">prefabbricato</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 32pt; text-align: left;"><asp:CheckBox ID="ChkA2_MAT_Prefabb" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 109pt; height: 32pt;">3. forte
  obsolescenza</td>

  <td class=xl6613694 style="border-top:none;border-left:none; width: 75pt; height: 32pt;"><asp:CheckBox ID="ChkA2_ANA_ForteObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 32pt;">corrosione armatura</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 32pt;">&nbsp;
      <asp:TextBox ID="Txt_A2_Corrosione_Armat" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:112pt; height: 32pt;">ristrutturazione</td>
  <td class=xl8213694 style="border-top:none;border-left:none; height: 32pt; width: 10107pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA2_INT_Ristrutturazione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:30pt;border-top:none;
  border-left:none;width:125pt">&nbsp;</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 30pt;"></td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 109pt; height: 30pt;">4. totale
  obsolescenza</td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 75pt; height: 30pt;"><asp:CheckBox ID="ChkA2_ANA_TotObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:123pt; height: 30pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 30pt;">&nbsp;</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:112pt; height: 30pt;">consolidamento strutturale</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 30pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA2_INT_Consolidamento_Strut" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>

 <tr height=35 style='height:26.25pt'>
  <td class=xl8013694 style="height:32pt;border-top:
  none;border-left:none;width:125pt">&nbsp;</td>
  <td class=xl8013694 style="border-top:none;border-left:none;
  width:177pt; height: 32pt;">&nbsp;</td>
  <td class=xl8613694 style="border-top:none;border-left:none; width: 109pt; height: 32pt;">&nbsp;</td>
  <td class=xl8613694 style="border-top:none;border-left:none; width: 75pt; height: 32pt;">&nbsp;</td>
  <td class=xl8513694 style="border-top:none;border-left:none;
  width:123pt; height: 32pt;">&nbsp;</td>
  <td class=xl8013694 style="border-top:none;border-left:none;
  width:10pt; height: 32pt;">&nbsp;</td>
  <td class=xl8513694 style="border-top:none;border-left:none;
  width:112pt; height: 32pt;">trattamento protettivo dei ferri</td>

  <td class=xl8813694 style="border-top:none;border-left:none; width: 10107pt; height: 32pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA2_INT_trattamento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=7 height=187 class=xl10713694 style="border-bottom:.5pt solid black;
  height:140.25pt;border-top:none; width: 12273pt;">A 3</td>
  <td rowspan=7 class=xl10713694 style='border-bottom:.5pt solid black;
  border-top:none;width:109pt'>STRUTTURE ORIZZONTALI i(solai, travi, volte)<br />
      mq<asp:TextBox ID="Text_mq_A3" runat="server" MaxLength="9" Width="100px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_A3"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=7 class=xl11013694 style="border-bottom:.5pt solid black;
  border-top:none;width:366pt"><br />
      </td>
  <td class=xl8113694 style="border-left:none;width:125pt; height: 29pt;">legno</td>

  <td class=xl8113694 style="border-left:none;width:177pt; text-align: left; height: 29pt;"><asp:CheckBox ID="ChkA3_MAT_legno" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8413694 style="border-left:none; width: 109pt; height: 29pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8413694 style="border-left:none; width: 75pt; height: 29pt;"><asp:CheckBox ID="ChkA3_ANA_Nuovo" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8113694 style="border-left:none;width:123pt; height: 29pt;">riconducibili
  a cedimenti strutturali</td>
  <td class=xl8113694 style="border-left:none;width:10pt; height: 29pt;">
      &nbsp;<asp:TextBox ID="Txt_A3_Cedim_Strutt" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl8113694 style="border-left:none;width:112pt; height: 29pt;">demolizione</td>
  <td class=xl8913694 style="border-left:none; width: 10107pt; height: 29pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA3_INT_demolizione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:37pt;border-top:none;
  border-left:none;width:125pt">acciaio</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 37pt;"><asp:CheckBox ID="ChkA3_MAT_Acciaio" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 109pt; height: 37pt;">2. lieve
  obsolescenza</td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 75pt; height: 37pt;"><asp:CheckBox ID="ChkA3_ANA_LieveObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9013694 style="border-top:none;border-left:none;
  width:123pt; height: 37pt;">
      instabilità dei singoli elementi</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 37pt;">&nbsp;<asp:TextBox ID="Txt_A3_InstabSingElem" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:112pt; height: 37pt;">ristrutturazione</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 37pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA3_INT_ristrutturazione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:21pt;border-top:none;
  border-left:none;width:125pt"><span style='mso-spacerun:yes'> </span>cls
  armato</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 21pt; text-align: left;"><asp:CheckBox ID="ChKA3_MAT_ClsArmato" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td class=xl6613694 style="border-top:none;border-left:none; width: 109pt; height: 21pt;">3. forte
  obsolescenza</td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 75pt; height: 21pt;"><asp:CheckBox ID="ChkA3_ANA_ForteObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 21pt;">deformazione singolo elemento</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 21pt;">
      <br />
      <asp:TextBox ID="Txt_A3_DeformSingElem" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 21pt;">consolidamento strutturale</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 21pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA3_INT_Consolidamento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:37pt;border-top:none;
  border-left:none;width:125pt">miste</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 37pt; text-align: left;"><asp:CheckBox ID="ChkA3_MAT_miste" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 109pt; height: 37pt;">4. totale
  obsolescenza</td>
  <td class=xl6613694 style="border-top:none;border-left:none; width: 75pt; height: 37pt;"><asp:CheckBox ID="ChkA3_ANA_TotObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 37pt;">corrosione armatura</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 37pt;">
      <asp:TextBox ID="Txt_A3_CorrosioneArm" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>

  <td class=xl7713694 style="border-top:none;border-left:none;
  width:112pt; height: 37pt;">trattamento protettivo dei ferri</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 37pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA3_INT_Trattamento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:31pt;border-top:
  none;border-left:none;width:125pt">mattoni / pietra</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 31pt; text-align: left;"><asp:CheckBox ID="ChkA3_MAT_Mattoni" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td rowspan=3 class=xl11613694 style="border-bottom:.5pt solid black;
  border-top:none; width: 109pt;">&nbsp;</td>
  <td rowspan=3 class=xl11613694 style="border-bottom:.5pt solid black;
  border-top:none; width: 75pt;">&nbsp;</td>

  <td rowspan=3 class=xl11213694 style="border-bottom:.5pt solid black;
  border-top:none;width:123pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 31pt;">&nbsp;</td>
  <td rowspan=3 class=xl10413694 style="border-bottom:.5pt solid black;
  border-top:none;width:112pt">&nbsp;</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 31pt;" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:26pt;border-top:
  none;border-left:none;width:125pt">laterocemento</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 26pt; text-align: left;"><asp:CheckBox ID="ChkA3_MAT_Laterocemento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 26pt;">&nbsp;</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 26pt;" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:37pt;border-top:
  none;border-left:none;width:125pt">prefabbricato</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 37pt; text-align: left;"><asp:CheckBox ID="ChkA3_MAT_Prefab" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 37pt;">&nbsp;</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 37pt;" rowspan="">&nbsp;</td>

 </tr>
 <tr height=34 style='page-break-before:always;height:25.5pt'>
  <td rowspan=12 height=392 class=xl10113694 style="border-bottom:
  1.0pt solid black;height:294.0pt;border-top:none; width: 12273pt;">A 4</td>
  <td rowspan=12 class=xl10113694 style='border-bottom:1.0pt solid black;
  border-top:none;width:109pt'>percorsi verticali (vani scala comuni,
  pianerottoli)<br />
      mq<asp:TextBox ID="Text_mq_A4" runat="server" MaxLength="7" Width="100px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Text_mq_A4"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=12 class=xl11213694 style="border-bottom:1.0pt solid black;
  border-top:none;width:366pt"><br />
      </td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 36pt;">cls armato</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 36pt;"><asp:CheckBox ID="ChkA4_MAT_clsArmato" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 36pt;">1. nuovo</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 36pt;"><asp:CheckBox ID="ChkA4_ANA_Nuovo" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 36pt;">riconducibili a cedimenti strutturali</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 36pt;">
      <asp:TextBox ID="Txt_A4_Cedim_Strutt" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 36pt;">demolizione</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 36pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_Demolizione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:27pt;border-top:none;
  border-left:none;width:125pt">legno</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 27pt; text-align: left;"><asp:CheckBox ID="ChkA4_MAT_Legno" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 27pt;">2. lieve obsolescenza</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 27pt;"><asp:CheckBox ID="ChkA4_ANA_LieveObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 27pt;">deformazioni parti strutturali</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 27pt;">
      <asp:TextBox ID="Txt_A4_DeformPartStru" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 27pt;">ristrutturazione</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 27pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_Ristrutturazione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:38pt;border-top:none;
  border-left:none;width:125pt">acciaio</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 38pt;"><asp:CheckBox ID="ChkA4_MAT_Acciaio" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 38pt;">3. forte obsolescenza</td>

  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 38pt;"><asp:CheckBox ID="ChkA4_ANA_ForteObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 38pt;">distacchi / rottura parte portante</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 38pt;">&nbsp;<asp:TextBox ID="Txt_A4_RotturePortante" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 38pt;">consolidamento strutturale</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 38pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_Consolidamento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:33pt;border-top:none;
  border-left:none;width:125pt">miste</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 33pt;"><asp:CheckBox ID="ChkA4_MAT_Miste" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">4. totale obsolescenza</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 33pt;"><asp:CheckBox ID="ChkA4_ANA_TotObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 33pt;">graffiti</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 33pt;">
      <asp:TextBox ID="Txt_A4_Graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 33pt;">ricostruzione parte portante</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 33pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_Ricostruzione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:34pt;border-top:
  none;border-left:none;width:125pt">mattoni / pietra</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 34pt; text-align: left;"><asp:CheckBox ID="ChkA4_MAT_Mattoni" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 34pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 34pt;">rottura lastra pavimento</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 34pt;">&nbsp;<asp:TextBox ID="Txt_A4_Pavimento" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 34pt;">rifacimento pavimenti</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 34pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_Rifacimento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:32pt;border-top:none;
  border-left:none;width:125pt">prefabbricato</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 32pt; text-align: left;"><asp:CheckBox ID="ChkA4_MAT_Prefab" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 32pt;">&nbsp;</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 32pt;">instabilità parapetto</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 32pt;">
      <asp:TextBox ID="Txt_A4_Instab_Parapett" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 32pt;">adeguamneto altezza parapetto</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 32pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_Adeguamento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="border-top:none;
  border-left:none;width:125pt; height: 37pt;">
      &nbsp;&nbsp;
      parapetto &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
      &nbsp;&nbsp;
  </td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 37pt;"><asp:CheckBox ID="ChkA4_MAT_Parapetto" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 37pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 37pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 37pt;">altezza parapetto inferiore a m 1,10</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 37pt;">&nbsp;<asp:TextBox ID="Txt_A4_Altezza_inferiore" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 37pt;">pulizia</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 37pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_Pulizia" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:37pt;border-top:none;
  border-left:none;width:125pt">ferro</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 37pt;"><asp:CheckBox ID="ChkA4_MAT_Ferro" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 37pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 37pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 37pt;">presenza di infiltrazioni<span style='mso-spacerun:yes'> </span></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 37pt;">&nbsp;<asp:TextBox ID="Txt_A4_Infiltraz" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 37pt;">sostituzione elementi parapetto</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 37pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_Sostituzione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:35pt;border-top:none;
  border-left:none;width:125pt">
      corrimano in ferro</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 35pt;"><asp:CheckBox ID="ChkA4_MAT_CorrimFerro" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 35pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 35pt;">&nbsp;</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 35pt;">corrosione armatura</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 35pt;">&nbsp;<asp:TextBox ID="Txt_A4_CorrosioneArmat" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 35pt;">sostituzione lastre vetro</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 35pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_SostLastVetr" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:34pt;border-top:none;
  border-left:none;width:125pt">pvc</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 34pt;"><asp:CheckBox ID="ChkA4_MAT_Pvc" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 34pt;">&nbsp;</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:123pt; height: 34pt;">distacchi finitura</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 34pt;">&nbsp;<asp:TextBox ID="Txt_A4_DistacFinitura" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 34pt;">trattamento protettivo ferri</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 34pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_TratProtFerri" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:31pt;border-top:none;
  border-left:none;width:125pt">muratura / cls</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 31pt; text-align: left;"><asp:CheckBox ID="ChkA4_MAT_MuratCls" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 31pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 31pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 31pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 31pt;">fissaggio elementi parapetto</td>

  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 31pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_FissaggParapetto" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=35 style='height:26.25pt'>
  <td class=xl8013694 style="height:30pt;border-top:
  none;border-left:none;width:125pt">con lastre di vetro</td>
  <td class=xl8013694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 30pt;"><asp:CheckBox ID="ChkA4_MAT_Vetro" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7913694 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">&nbsp;</td>
  <td class=xl7913694 style="border-top:none;border-left:none;
  width:75pt; height: 30pt;">&nbsp;</td>
  <td class=xl8013694 style="border-top:none;border-left:none;
  width:123pt; height: 30pt;">&nbsp;</td>

  <td class=xl8013694 style="border-top:none;border-left:none;
  width:10pt; height: 30pt;">&nbsp;</td>
  <td class=xl8013694 style="border-top:none;border-left:none;
  width:112pt; height: 30pt;">eliminazione infiltrazioni</td>
  <td class=xl8813694 style="border-top:none;border-left:none; width: 10107pt; height: 30pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA4_INT_ElimInfiltraz" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='page-break-before:always;height:25.5pt'>
  <td rowspan=12 height=324 class=xl10713694 style="border-bottom:
  .5pt solid black;height:243.0pt;border-top:none; width: 12273pt;">A 5</td>
  <td rowspan=12 class=xl10713694 style='border-bottom:.5pt solid black;
  border-top:none;width:109pt'>coperture<br />
      mq<asp:TextBox ID="Text_mq_A5" runat="server" MaxLength="9" Width="100px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="Text_mq_A5"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>

  <td rowspan=12 class=xl11013694 style="border-bottom:.5pt solid black;
  border-top:none;width:366pt"><br />
      </td>
  <td class=xl8113694 style="border-left:none;width:125pt; height: 28pt;">solaio
  laterocemento</td>
  <td class=xl8113694 style="border-left:none;width:177pt; text-align: left; height: 28pt;"><asp:CheckBox ID="ChkA5_MAT_Solai" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8713694 style="border-left:none;width:109pt; height: 28pt;">1. nuovo</td>
  <td class=xl8713694 style="border-left:none;width:75pt; height: 28pt;"><asp:CheckBox ID="ChkA5_ANA_Nuovo" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl8113694 style="border-left:none;width:123pt; height: 28pt;">riconducibili
  a cedimenti strutturali</td>
  <td class=xl8113694 style="border-left:none;width:10pt; height: 28pt;">
      <asp:TextBox ID="Txt_A5_Cedim_Strutt" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>

  <td class=xl8113694 style="border-left:none;width:112pt; height: 28pt;">demolizione</td>
  <td class=xl8913694 style="border-left:none; width: 10107pt; height: 28pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_demolizione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:31pt;border-top:
  none;border-left:none;width:125pt">pannelli portanti prefabbricati</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 31pt;"><asp:CheckBox ID="ChkA5_MAT_Prefab" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">2. lieve obsolescenza</td>

  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 31pt;"><asp:CheckBox ID="ChkA5_ANA_LieveObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 31pt;">deformazioni struttura</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 31pt;">&nbsp;<asp:TextBox ID="Txt_A5_DeformStrutt" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 31pt;">ristrutturazione</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 31pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_ristrutturazione" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:33pt;border-top:none;
  border-left:none;width:125pt">strutture in legno</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 33pt;"><asp:CheckBox ID="ChkA5_MAT_Legno" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">3. forte obsolescenza</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 33pt;"><asp:CheckBox ID="ChkA5_ANA_ForteObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 33pt;">distacchi / rotture parti portanti</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 33pt;">&nbsp;<asp:TextBox ID="Txt_A5_RuotePartiPort" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 33pt;">consolidamento strutturale</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 33pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_consolidStrutt" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:33pt;border-top:none;
  border-left:none;width:125pt">struttura cls armato in opera</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 33pt;"><asp:CheckBox ID="ChkA5_MAT_ClsArmato" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">4. totale obsolescenza</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 33pt;"><asp:CheckBox ID="ChkA5_ANA_TotObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 33pt;">slittamento manto copertura</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 33pt;">&nbsp;<asp:TextBox ID="Txt_A5_Manto_Copert" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 33pt;">sistemazione terreno</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 33pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_sistTerreno" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:36pt;border-top:none;
  border-left:none;width:125pt">struttura in acciaio</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 36pt;"><asp:CheckBox ID="ChkA5_MAT_Acciaio" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 36pt;">&nbsp;</td>

  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 36pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 36pt;">degardo di impermeabilizzazione</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 36pt;">&nbsp;<asp:TextBox ID="Txt_A5_Degrad_imperm" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 36pt;">ricostruzione parte portante</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 36pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_ricostportante" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:33pt;border-top:
  none;border-left:none;width:125pt">paina</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 33pt; text-align: left;"><asp:CheckBox ID="ChkA5_MAT_Paina" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 33pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 33pt;">distacchi da grande</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 33pt;">&nbsp;<asp:TextBox ID="Txt_A5_Distacchi_Grande" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 33pt;">rifacimento<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 33pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_rifacimento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:36pt;border-top:none;
  border-left:none;width:125pt">a falde</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 36pt;"><asp:CheckBox ID="ChkA5_MAT_Falde" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 36pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 36pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 36pt;">otturazione grande / pluviali</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 36pt;">&nbsp;<asp:TextBox ID="Txt_A5_Otturaz_pluviali" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 36pt;">impermeabilizzazione</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 36pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_impermeabiliz" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:38pt;border-top:none;
  border-left:none;width:125pt">gronde in aggetto</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 38pt;"><asp:CheckBox ID="ChkA5_MAT_GrondeAggetto" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 38pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 38pt;">&nbsp;</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 38pt;">presenza di macerie nel sottotetto</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 38pt;">
      <asp:TextBox ID="Txt_A5_MacerieSottotetto" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 38pt;">ricostruzione gronde</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 38pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_ricostGronde" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:24pt;border-top:
  none;border-left:none;width:125pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 24pt;">&nbsp;</td>

  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 24pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 24pt;">&nbsp;</td>
  <td class=xl7713694 style="border-top:none;border-left:none;
  width:123pt; height: 24pt;">infiltarzione gronda</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 24pt;">
      <asp:TextBox ID="Txt_A5_InfiltrGronda" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 24pt;">pulizia gronde</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 24pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_pulizGronde" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl6713694 style="height:22pt;border-top:
  none;border-left:none;width:125pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 22pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 22pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 22pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 22pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 22pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 22pt;">pulizia sottotetto</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 22pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_puliziaSottet" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:21pt;border-top:
  none;border-left:none;width:125pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 21pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 21pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 21pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 21pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 21pt;">ricorsa tetto</td>

  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 21pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_ricorstett" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=35 style='height:26.25pt'>
  <td class=xl6713694 style="height:32pt;border-top:
  none;border-left:none;width:125pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 32pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 32pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 32pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 32pt;">&nbsp;</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 32pt;">rifacimento manto copertura in tegole</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 32pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA5_INT_mantoCopert" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=10 height=340 class=xl10713694 style="height:255.0pt; width: 12273pt;">A 6</td>
  <td rowspan=10 class=xl10713694 style='width:109pt'>balconi /
  ballatoi esterni<br />
      mq<asp:TextBox ID="Text_mq_A6" runat="server" MaxLength="9" Width="100px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="Text_mq_A6"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:366pt; height: 44pt;"><br />
      </td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 44pt;">laterocemento</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 44pt;"><asp:CheckBox ID="ChkA6_MAT_LaterCemento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 44pt;">1. nuovo</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 44pt;"><asp:CheckBox ID="ChkA6_ANA_Nuovo" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 44pt;">riconducibili a cedimenti strutturali</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 44pt;">
      <asp:TextBox ID="Txt_A6_CedimStrutturali" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 44pt;">ricostruzione parte portante</td>

  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 44pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_ricostPortante" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:36pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 36pt;">cls armato in opera</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 36pt;"><asp:CheckBox ID="ChkA6_MAT_ClsArmato" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 36pt;">2. lieve obsolescenza</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 36pt;"><asp:CheckBox ID="ChkA6_ANA_LieveObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 36pt;">distacchi finitura</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 36pt;">
      <asp:TextBox ID="Txt_A6_Distacchi_Finiture" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 36pt;">strutture di rinforzo parte portante</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 36pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_rinforzPortante" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=51 style='height:38.25pt'>
  <td class=xl6713694 style="height:46pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 46pt;">pietra</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 46pt;"><asp:CheckBox ID="ChkA6_MAT_Pietra" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 46pt;">3. forte obsolescenza</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 46pt;"><asp:CheckBox ID="ChkA6_ANA_ForteObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 46pt;">instabilità / deformazione parte portante</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 46pt;">&nbsp;<asp:TextBox ID="Txt_A6_Instabil_deform" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 46pt;">applicazione trattamenti protettivi su parte portante</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 46pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_protettPareti" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:33pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 33pt;">acciaio</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 33pt;"><asp:CheckBox ID="ChkA6_MAT_Acciaio" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" />&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">4. totale obsolescenza</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 33pt;"><asp:CheckBox ID="ChkA6_ANA_TotObs" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 33pt;">corrosione armatura</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 33pt;">
      <asp:TextBox ID="Txt_A6_Corros_arm" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 33pt;">rifacimento manto superiore</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 33pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_RifaciMantoSup" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:30pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 30pt;">parapetto in ferro</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 30pt;"><asp:CheckBox ID="ChkA6_MAT_ParapFerro" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 30pt;"></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 30pt;">distacchi / rottura parte portante</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 30pt;">
      <asp:TextBox ID="Txt_A6_Distacchi_portante" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 30pt;">consolidamento<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 30pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_consolidamento" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:30pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 30pt;">parapetto in legno</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 30pt;"><asp:CheckBox ID="ChkA6_MAT_ParapLegno" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 30pt;"></td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 30pt;">rottura lastra</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 30pt;">
      <asp:TextBox ID="Txt_A6_Rottura_Lastra" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 30pt;">sostituzione elementi portanti</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 30pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_sostElem" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713694 style="height:22pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 22pt;">parapetto in muratura / cls</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 22pt;"><asp:CheckBox ID="ChkA6_MAT_ParapMuratura" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 22pt;">&nbsp;</td>

  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 22pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 22pt;">instabilità parapetto</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 22pt;">
      <asp:TextBox ID="Txt_A6_Instab_Parapetto" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 22pt;">sostituzione lastra</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 22pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_sostLastra" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:29pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 29pt;">con lastre di vetro</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; text-align: left; height: 29pt;"><asp:CheckBox ID="ChkA6_MAT_Vetro" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 29pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 29pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 29pt;">altezza parapetto inferiore a m 1,10</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 29pt;">
      <asp:TextBox ID="Txt_A6_Parap_inferior" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 29pt;">adeguamneto altezza parapetto</td>

  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 29pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_adegAltezza" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:27pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 27pt;">
      parapetto in pvc&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 27pt; text-align: left;"><asp:CheckBox ID="ChkA6_MAT_ParapetPVC" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 27pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 27pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 27pt;">&nbsp;</td>

  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 27pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 27pt;">fissaggio elementi parapetto</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 27pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_fissParapetto" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713694 style="height:30pt;border-top:none;
  border-left:none;width:366pt">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:125pt; height: 30pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:177pt; height: 30pt;">&nbsp;</td>

  <td class=xl7813694 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">&nbsp;</td>
  <td class=xl7813694 style="border-top:none;border-left:none;
  width:75pt; height: 30pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:123pt; height: 30pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:10pt; height: 30pt;">&nbsp;</td>
  <td class=xl6713694 style="border-top:none;border-left:none;
  width:112pt; height: 30pt;">sostituzione elementti parapetto</td>
  <td class=xl8213694 style="border-top:none;border-left:none; width: 10107pt; height: 30pt; text-align: left;" rowspan=""><asp:CheckBox ID="ChkA6_INT_sostParapetto" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=20 style='height:15.0pt'>

  <td colspan=2 rowspan=2 height=40 class=xl11413694 style='height:30.0pt'>STATO
  DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl11513694>Stato 1</td>
  <td rowspan=2 class=xl9313694 style="border-top:none; width: 177pt; vertical-align: top; text-align: left;"><asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9313694 style="border-top:none;border-left:none; width: 109pt; height: 22pt;">Stato 2.1</td>
  <td class=xl9313694 style="border-top:none;border-left:none; width: 75pt; vertical-align: top; height: 22pt; text-align: left;"><asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9313694 style="border-top:none;border-left:none; width: 123pt; height: 22pt;">Stato 3.1</td>
  <td class=xl9313694 style="border-top:none;border-left:none; width: 10pt; height: 22pt; vertical-align: top; text-align: left;"><asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td rowspan=2 class=xl11513694 style="border-top:none; width: 112pt;">Stato 3.3</td>
  <td class=xl9313694 style="width: 10107pt; height: 22pt; border-top-style: none; border-left-style: none; text-align: left; border-bottom-style: none;" rowspan=""></td>
 </tr>
 <tr height=20 style='height:15.0pt'>
  <td class=xl9313694 style="border-top:none;
  border-left:none; width: 109pt; height: 22pt;">Stato 2.2</td>
  <td class=xl9313694 style="border-top:none;border-left:none; width: 75pt; vertical-align: top; text-align: left; height: 22pt;"><asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9313694 style="border-top:none;border-left:none; width: 123pt; height: 22pt;">Stato 3.2</td>

  <td class=xl9313694 style="border-top:none;border-left:none; width: 10pt; vertical-align: top; text-align: left; height: 22pt;"><asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9313694 style="border-top:none;border-left:none; width: 10107pt; vertical-align: top; text-align: left; height: 22pt;" rowspan=""><asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl11813694 width=944 style='border-right:1.0pt solid black;
  height:12.75pt;width:711pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7213694 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7313694 style="width: 125pt"></td>
  <td class=xl7313694 style="width: 177pt"></td>
  <td class=xl7313694 style="width: 109pt"></td>
  <td class=xl7313694 style="width: 75pt"></td>
  <td class=xl7313694 style="width: 123pt"></td>
  <td class=xl7313694 style="width: 10pt"></td>
  <td class=xl7313694 style="width: 112pt"></td>

  <td class=xl7413694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7513694 style="height:12.75pt; width: 12273pt;">&nbsp;</td>
  <td class=xl1513694 style="width: 109pt"></td>
  <td class=xl1513694 style="width: 366pt"></td>
  <td class=xl1513694 style="width: 125pt"></td>
  <td class=xl1513694 style="width: 177pt"></td>
  <td class=xl1513694 style="width: 109pt"></td>

  <td class=xl1513694 style="width: 75pt"></td>
  <td class=xl1513694 style="width: 123pt"></td>
  <td class=xl1513694 style="width: 10pt"></td>
  <td class=xl1513694 style="width: 112pt"></td>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7513694 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>

  <td class=xl1513694 style="width: 75pt"></td>
  <td class=xl1513694 style="width: 123pt"></td>
  <td class=xl1513694 style="width: 10pt"></td>
  <td class=xl1513694 style="width: 112pt"></td>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 height=17 class=xl12113694 width=944 style='border-right:1.0pt solid black;
  height:12.75pt;width:711pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola<br>

    <span style='mso-spacerun:yes'> </span>anomalia&gt;= 60%) –</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7513694 colspan=10 style='height:12.75pt'>Per alcuni
  El. Tecnici potrebbe risultare necessario inserire diverse dimensioni in
  relazione alle diverse tipologie costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7513694 colspan="10" rowspan="20">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="344px" Width="920px" Font-Size="12pt" TextMode="MultiLine"></asp:TextBox></td>
  <td class=xl7613694 style="height: 13pt; width: 10107pt;" rowspan=""></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="height: 8pt; width: 10107pt;" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="height: 4pt; width: 10107pt;" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="height: 8pt; width: 10107pt;" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl7113694 style="height: 6pt; width: 10107pt;" rowspan="">&nbsp;</td>
 </tr>

 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl9113694 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl6813694 style="border-top:none; width: 366pt;">&nbsp;</td>
  <td class=xl6813694 style="border-top:none; width: 125pt;">&nbsp;</td>
  <td class=xl6813694 style="border-top:none; width: 177pt;">&nbsp;</td>
  <td class=xl6813694 style="border-top:none; width: 109pt;">&nbsp;</td>
  <td class=xl6813694 style="border-top:none; width: 75pt;">&nbsp;</td>
  <td class=xl6813694 style="border-top:none; width: 123pt;">&nbsp;</td>

  <td class=xl6813694 style="border-top:none; width: 10pt;">&nbsp;</td>
  <td class=xl6813694 style="border-top:none; width: 112pt;">&nbsp;</td>
  <td class=xl6913694 style="border-top:none; width: 10107pt;" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 height=34 class=xl12413694 width=944
  style='border-right:1.0pt solid black;height:25.5pt;width:711pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
     <td class="xl7013694" colspan="10" rowspan="30" style="text-align: right">
         <asp:TextBox ID="TxtNote_2" runat="server" Height="504px" Width="920px" Font-Size="12pt" TextMode="MultiLine"></asp:TextBox>&nbsp;</td>
      <td class=xl826175 
         style="height: 13pt; width: 10107pt; border-right: black 1px solid;">&nbsp;</td>
</tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl826175 style="width: 10107pt; border-right: black 1px solid;" rowspan="">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
    <td class=xl826175>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl7613694 style="width: 10107pt" rowspan="">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
   <td class=xl7613694 style="height: 12pt; width: 12273pt; border-bottom: black 1px solid;" rowspan="">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl836175 style="height:37pt; width: 12273pt;">&nbsp;</td>
  <td class=xl846175 style="height: 37pt; width: 109pt;">
      &nbsp;</td>
  <td class=xl846175 style="height: 37pt; width: 366pt;">&nbsp;</td>
  <td class=xl846175 style="width: 125pt; height: 37pt;">&nbsp;</td>
  <td class=xl846175 style="width: 177pt; height: 37pt;">&nbsp;</td>
  <td class=xl846175 style="height: 37pt">
      &nbsp;</td>
  <td class=xl846175 style="height: 37pt">&nbsp;</td>
  <td class=xl846175 style="height: 37pt; width: 123pt;">&nbsp;</td>

  <td class=xl846175 style="height: 37pt; width: 10pt;">&nbsp;</td>
  <td class=xl846175 style="height: 37pt; width: 112pt;">
      &nbsp;</td>
  <td class=xl856175 style="height: 37pt; width: 10107pt;" rowspan="">&nbsp;</td>
 </tr>

 <tr height=0 style='display:none'>
  <td style="height: 20px; width: 12273pt;"></td>
  <td style="width:109pt; height: 20px;"></td>
  <td style="width:366pt; height: 20px;"></td>

  <td style="width:125pt; height: 20px;"></td>
  <td style="width:177pt; height: 20px;"></td>
  <td width=143 style="width:107pt; height: 20px;"></td>
  <td width=26 style="width:20pt; height: 20px;"></td>
  <td style="width:123pt; height: 20px;"></td>
  <td style="width:10pt; height: 20px;"></td>
  <td style="width:112pt; height: 20px;"></td>
  <td style="width:10107pt; height: 20px;" rowspan=""></td>
 </tr>
</table>
    </div>
        &nbsp;<br />
        &nbsp;
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
