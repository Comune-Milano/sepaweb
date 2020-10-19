<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScL.aspx.vb" Inherits="ScL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">

    <title>Scheda L</title>
    <style id="SCHEDE RILIEVO TUTTE_13122_Styles">
    
	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1513122
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
.xl6513122
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
.xl6613122
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
.xl6713122
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
.xl6813122
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
.xl6913122
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
.xl7013122
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
.xl7113122
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
.xl7213122
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
.xl7313122
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
.xl7413122
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
.xl7513122
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
.xl7613122
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
.xl7713122
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
.xl7813122
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
.xl7913122
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
.xl8013122
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
.xl8113122
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
.xl8213122
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8313122
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
.xl8413122
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
.xl8513122
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
.xl8613122
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
.xl8713122
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
.xl8813122
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
.xl8913122
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
.xl9013122
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
.xl9113122
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
.xl9213122
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9313122
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
.xl9413122
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9513122
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
.xl9613122
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
.xl9713122
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
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl9813122
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
.xl9913122
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
.xl10013122
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
.xl10113122
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
	white-space:normal;}
.xl10213122
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
.xl10313122
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
	vertical-align:middle;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10413122
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
	vertical-align:middle;
	border:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10513122
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
	vertical-align:middle;
	border-top:none;
	border-right:.5pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10613122
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
	vertical-align:middle;
	border-top:1.0pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10713122
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl10813122
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10913122
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
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11013122
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
	vertical-align:middle;
	border-top:none;
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11113122
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
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11213122
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
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11313122
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
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11413122
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
	vertical-align:middle;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11513122
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
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl11613122
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl11713122
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
.xl11813122
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
.xl11913122
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
	border-right:1.0pt solid windowtext;
	border-bottom:.5pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12013122
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
	border-right:1.0pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12113122
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
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12213122
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
.xl12313122
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
.xl12413122
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
.xl12513122
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
.xl12613122
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
.xl12713122
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
.xl12813122
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
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12913122
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
	border-bottom:none;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl13013122
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl13113122
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
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl13213122
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
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl13313122
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
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl13413122
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
.xl13513122
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
.xl13613122
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
.xl13713122
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
.xl13813122
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
.xl13913122
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
.xl14013122
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
.xl14113122
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
.xl14213122
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
.xl14313122
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
.xl14413122
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
.xl14513122
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
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl14613122
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
	border-bottom:none;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl14713122
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
	border-right:none;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl14813122
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
	border-bottom:none;
	border-left:none;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl14913122
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
	border-right:1.0pt solid windowtext;
	border-bottom:none;
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl15013122
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl15113122
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
	border-top:1.0pt solid windowtext;
	border-right:1.0pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}

</style>

</head>
<body>
    <form id="form1" runat="server">
    <div align=center x:publishsource="Excel">
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Constantia"
            Font-Size="9pt" ForeColor="#8080FF" Width="504px">Pagina ottimizzata per Internet Explorer 7</asp:Label><br />
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
    <table border=0 cellpadding=0 cellspacing=0 width=884 style='border-collapse:
 collapse;table-layout:fixed;width:666pt'>
 <col width=54 style='mso-width-source:userset;mso-width-alt:1974;width:41pt'>
 <col width=131 style='mso-width-source:userset;mso-width-alt:4790;width:98pt'>
 <col width=29 style='mso-width-source:userset;mso-width-alt:1060;width:22pt'>
 <col width=131 style='mso-width-source:userset;mso-width-alt:4790;width:98pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>

 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <tr height=34 style='mso-height-source:userset;height:26.1pt'>
  <td colspan=9 height=34 class=xl12813122 width=713 style='height:26.1pt;
  width:537pt'>U. T. IMPIANO IDRICO SANITARI - SCHEDA RILIEVO IMPIANTI IDRICO
  SANITARI</td>
  <td colspan=2 class=xl15013122 width=171 style='border-right:1.0pt solid black;
  width:129pt'>L</td>

 </tr>
 <tr height=53 style='mso-height-source:userset;height:39.95pt'>
  <td height=53 class=xl11513122 width=54 style='height:39.95pt;width:41pt'>Scheda
  L</td>
  <td class=xl11613122 width=131 style='border-left:none;width:98pt'>ELEMENTO
  TECNICO</td>
  <td class=xl11613122 style='border-left:none;width:22pt'></td>
  <td colspan=2 class=xl13013122 width=157 style='border-right:.5pt solid black;
  border-left:none;width:118pt'>MATERIALI E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td colspan=2 class=xl13013122 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl13013122 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANOMALIE (%)</td>
  <td colspan=2 class=xl13013122 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=4 height=86 class=xl13413122 width=54 style='border-bottom:1.0pt solid black;
  height:64.5pt;border-top:none;width:41pt'>L 1</td>
  <td rowspan=4 class=xl12413122 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>ALIMETAZIONE<br />
      cad<asp:TextBox ID="Text_mq_L1" runat="server" MaxLength="9" Width="90px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_L1"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>

  <td class=xl9013122 style="border-left:none;width:22pt; height: 33pt;"></td>
  <td class=xl10613122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 33pt;">contatore</td>
  <td class=xl8813122 width=26 style="border-left:none;width:20pt; height: 33pt;">
      <asp:CheckBox ID="C_MAT_L1_contatore" runat="server" Text="." /></td>
  <td class=xl8213122 style="height: 33pt">1. nuovo<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8913122 style="height: 33pt">
      <asp:CheckBox ID="C_ANA_L1_nuovo" runat="server" Text="." /></td>
  <td class=xl10613122 style="border-top:none;border-left:none;
  width:107pt; height: 33pt;">perdita da rete</td>

  <td class=xl8813122 width=26 style="border-left:none;width:20pt; height: 33pt;">
      <asp:TextBox ID="T_ANO_L1_perdita0rete" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10613122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">eliminazione perdite</td>
  <td class=xl9213122 style="border-left:none; height: 33pt;">
      <asp:CheckBox ID="C_INT_L1_eliminazione0perdite" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713122 style="height:44pt;border-left:none;
  width:22pt">&nbsp;</td>
  <td class=xl10413122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 44pt;">linee di adduzione</td>
  <td class=xl7913122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 44pt;">
      <asp:CheckBox ID="C_MAT_L1_linee0adduzione" runat="server" Text="." /></td>

  <td class=xl8913122 style="border-left:none; height: 44pt;">2. lieve obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 44pt;">
      <asp:CheckBox ID="C_ANA_L1_lieve" runat="server" Text="." /></td>
  <td class=xl10413122 style="border-top:none;border-left:none;
  width:107pt; height: 44pt;">accesso contatori difficoltoso</td>
  <td class=xl7913122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 44pt;">
      <asp:TextBox ID="T_ANO_L1_contatori0difficoltoso" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 44pt;">adeguamento gruppo contatore</td>
  <td class=xl8013122 style="border-top:none;border-left:none; height: 44pt;">
      <asp:CheckBox ID="C_INT_L1_adeguamento0contatore" runat="server" Text="." /></td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl6713122 style="height:24pt;border-left:
  none;width:22pt">&nbsp;</td>
  <td class=xl10913122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 24pt;">&nbsp;</td>
  <td class=xl9113122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">&nbsp;</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 24pt;">3. forte
  obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 24pt;">
      <asp:CheckBox ID="C_ANA_L1_forte" runat="server" Text="." /></td>
  <td class=xl10913122 style="border-top:none;border-left:none;
  width:107pt; height: 24pt;">&nbsp;</td>
  <td class=xl9113122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">&nbsp;</td>

  <td class=xl10913122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 24pt;">&nbsp;</td>
  <td class=xl9313122 style="border-top:none;border-left:none; height: 24pt;"></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl6713122 style="height:25pt;border-left:none;
  width:22pt">&nbsp;</td>
  <td class=xl10513122 width=131 style="border-left:none;width:98pt; height: 25pt;">&nbsp;</td>
  <td class=xl9613122 width=26 style="border-left:none;width:20pt; height: 25pt;">&nbsp;</td>
  <td class=xl10913122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 25pt;">4. totale obsolescenza</td>

  <td class=xl8313122 style="border-top:none;border-left:none; height: 25pt;">
      <asp:CheckBox ID="C_ANA_L1_tot" runat="server" Text="." /></td>
  <td class=xl10513122 style="border-left:none;width:107pt; height: 25pt;">&nbsp;</td>
  <td class=xl9613122 width=26 style="border-left:none;width:20pt; height: 25pt;">&nbsp;</td>
  <td class=xl10513122 width=145 style="border-left:none;width:109pt; height: 25pt;">&nbsp;</td>
  <td class=xl10713122 style="border-left:none; height: 25pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=4 height=120 class=xl13413122 width=54 style='border-bottom:1.0pt solid black;
  height:90.0pt;border-top:none;width:41pt'>L 2</td>

  <td rowspan=4 class=xl12413122 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>MACCHINE IDRAULICHE<br />
      cad<asp:TextBox ID="Text_mq_L2" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_L2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8413122 style="border-left:none;width:22pt; height: 30pt;"></td>
  <td class=xl10613122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 30pt;">autoclave</td>
  <td class=xl10013122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_L2_autoclave" runat="server" Text="." /></td>
  <td class=xl8113122 style="border-left:none; height: 30pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8113122 style="border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_ANA_L2_nuovo" runat="server" Text="." /></td>

  <td class=xl10813122 style="border-top:none;border-left:none;
  width:107pt; height: 30pt;">scarsa pressione di uscita</td>
  <td class=xl10013122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_L2_scarsa0pressione0uscita" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10613122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">verifica funzionamento compressore</td>
  <td class=xl9513122 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_L2_verifica0compressore" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713122 style="height:30pt;border-left:none;
  width:22pt">
      &nbsp;</td>
  <td class=xl10413122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 30pt;">compressori</td>

  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_L2_compressori" runat="server" Text="." /></td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 30pt;">2. lieve
  obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_ANA_L2_lieve" runat="server" Text="." /></td>
  <td class=xl10113122 style="border-top:none;border-left:none;
  width:107pt; height: 30pt;">perdita di acqua da pompe</td>
  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_L2_perdita0acqua0pompe" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">prova di tenuta serbatoio</td>
  <td class=xl8013122 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_L2_tenuta0serbatoio" runat="server" Text="." /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713122 style="height:29pt;border-left:none;
  width:22pt">&nbsp;</td>
  <td class=xl10413122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 29pt;">pompe</td>
  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_L2_pompe" runat="server" Text="." /></td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 29pt;">3. forte
  obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 29pt;">
      <asp:CheckBox ID="C_ANA_L2_forte" runat="server" Text="." /></td>
  <td class=xl8713122 style="border-top:none;border-left:none; height: 29pt; width: 107pt;">perdita da
  serbatoio</td>

  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:TextBox ID="T_ANO_L2_perdita0serbatoio" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 29pt;">sostituzione/revisione pompa</td>
  <td class=xl8013122 style="border-top:none;border-left:none; height: 29pt;">
      <asp:CheckBox ID="C_INT_L2_revisione0pompa" runat="server" Text="." /></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl8513122 style="height:24pt;border-left:none;
  width:22pt">&nbsp;</td>
  <td class=xl11013122 width=131 style="width:98pt; height: 24pt;">&nbsp;</td>
  <td class=xl8613122 width=26 style="border-top:none;width:20pt; height: 24pt;">&nbsp;</td>

  <td class=xl9813122 style="border-top:none;border-left:none; height: 24pt;">4. totale
  obsolescenza</td>
  <td class=xl9813122 style="border-top:none;border-left:none; height: 24pt;">
      <asp:CheckBox ID="C_ANA_L2_tot" runat="server" Text="." /></td>
  <td class=xl10213122 style="border-top:none;border-left:none; height: 24pt; width: 107pt;">rumorosità</td>
  <td class=xl8613122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">
      <asp:TextBox ID="T_ANO_L2_rumorosita" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl11013122 width=145 style="width:109pt; height: 24pt;">&nbsp;</td>
  <td class=xl9913122 style="border-top:none; height: 24pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td rowspan=4 height=103 class=xl13513122 width=54 style='border-bottom:1.0pt solid black;
  height:77.25pt;width:41pt'>L 3</td>
  <td rowspan=4 class=xl12213122 width=131 style='border-bottom:1.0pt solid black;
  width:98pt'>RETE DI DISTRIBUZIONE<br />
      ml<asp:TextBox ID="Text_mq_L3" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Text_mq_L3"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl9013122 style="border-left:none;width:22pt; height: 23pt;"></td>
  <td class=xl10313122 width=131 style="border-left:none;width:98pt; height: 23pt;">interrata/immurata</td>
  <td class=xl8813122 width=26 style="border-left:none;width:20pt; height: 23pt;">
      <asp:CheckBox ID="C_MAT_L3_immuratura" runat="server" Text="." /></td>
  <td class=xl8913122 style="border-left:none; height: 23pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl8913122 style="border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_ANA_L3_nuovo" runat="server" Text="." /></td>
  <td class=xl10313122 style="border-left:none;width:107pt; height: 23pt;">perdita
  da rete</td>
  <td class=xl8813122 width=26 style="border-left:none;width:20pt; height: 23pt;">
      <asp:TextBox ID="T_ANO_L3_perdita0rete" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10313122 width=145 style="border-left:none;width:109pt; height: 23pt;">analisi
  amianto</td>
  <td class=xl9213122 style="border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_INT_L3_analisi0amianto" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713122 style="height:23pt;border-left:
  none;width:22pt">&nbsp;</td>

  <td class=xl10413122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 23pt;">a vista</td>
  <td class=xl7913122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">
      <asp:CheckBox ID="C_MAT_L3_a0vista" runat="server" Text="." /></td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 23pt;">2. lieve
  obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_ANA_L3_lieve" runat="server" Text="." /></td>
  <td class=xl10413122 style="border-top:none;border-left:none;
  width:107pt; height: 23pt;">distacchi coibentazione</td>
  <td class=xl7913122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">
      <asp:TextBox ID="T_ANO_L3_distacchi0coibentazione" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">eliminazione perdite</td>

  <td class=xl8013122 style="border-top:none;border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_INT_L3_eliminazione0perdite" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6713122 style="height:34pt;border-left:none;
  width:22pt">
      &nbsp;</td>
  <td class=xl10913122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 34pt;">&nbsp;</td>
  <td class=xl9113122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">&nbsp;</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 34pt;">3. forte
  obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 34pt;">
      <asp:CheckBox ID="C_ANA_L3_forte" runat="server" Text="." /></td>

  <td class=xl10413122 style="border-top:none;border-left:none;
  width:107pt; height: 34pt;">ruggine</td>
  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">
      <asp:TextBox ID="T_ANO_L3_ruggine" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">ripristino coibentazione tubazioni</td>
  <td class=xl8013122 style="border-top:none;border-left:none; height: 34pt;">
      <asp:CheckBox ID="C_INT_L3_ripristino0coibentazione" runat="server" Text="." /></td>
 </tr>
 <tr height=35 style='height:26.25pt'>
  <td class=xl6713122 style="height:29pt;border-left:
  none;width:22pt">&nbsp;</td>
  <td class=xl10513122 width=131 style="border-left:none;width:98pt; height: 29pt;">&nbsp;</td>

  <td class=xl9613122 width=26 style="border-left:none;width:20pt; height: 29pt;">&nbsp;</td>
  <td class=xl11113122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 29pt;">4. totale obsolescenza</td>
  <td class=xl6613122 style="border-top:none; height: 29pt;">
      <asp:CheckBox ID="C_ANA_L3_tot" runat="server" Text="." /></td>
  <td class=xl11213122 style="border-top:none;width:107pt; height: 29pt;">sospetta
  presenza amianto</td>
  <td class=xl7913122 width=26 style="border-top:none;width:20pt; height: 29pt;">
      <asp:TextBox ID="T_ANO_L3_sospetta0amianto" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl11313122 width=145 style="border-top:none;width:109pt; height: 29pt;">&nbsp;</td>
  <td class=xl9313122 style="border-top:none;border-left:none; height: 29pt;">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td rowspan=5 height=86 class=xl13413122 width=54 style='border-bottom:1.0pt solid black;
  height:64.5pt;border-top:none;width:41pt'>L 4</td>
  <td rowspan=5 class=xl12413122 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>TERMINALI<br />
      cad<asp:TextBox ID="Text_mq_L4" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="Text_mq_L4"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8413122 style="border-left:none;width:22pt; height: 23pt;"></td>
  <td class=xl10613122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 23pt;">rubinetti</td>
  <td class=xl10013122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">
      <asp:CheckBox ID="C_MAT_L4_rubinetti" runat="server" Text="." /></td>
  <td class=xl8113122 style="border-left:none; height: 23pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl8113122 style="border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_ANA_L4_nuovo" runat="server" Text="." /></td>
  <td class=xl10813122 style="border-left:none;width:107pt; height: 23pt;">perdita
  da terminali</td>
  <td class=xl10013122 width=26 style="border-left:none;width:20pt; height: 23pt;">
      <asp:TextBox ID="T_ANO_L4_perdita0terminali" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10613122 width=145 style="border-left:none;width:109pt; height: 23pt;">eliminazione
  perdite</td>
  <td class=xl9513122 style="border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_INT_L4_eliminazione0perdite" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713122 style="height:29pt;border-left:
  none;width:22pt">
      &nbsp;</td>

  <td class=xl10413122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 29pt;">portagomma</td>
  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_L4_portagomma" runat="server" Text="." /></td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 29pt;">2. lieve
  obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 29pt;">
      <asp:CheckBox ID="C_ANA_L4_lieve" runat="server" Text="." /></td>
  <td class=xl10113122 style="border-top:none;border-left:none;
  width:107pt; height: 29pt;">rottura<span style='mso-spacerun:yes'> </span></td>
  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:TextBox ID="T_ANO_L4_rottura" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 29pt;">sostituzione sanitari</td>

  <td class=xl8013122 style="border-top:none;border-left:none; height: 29pt;">
      <asp:CheckBox ID="C_INT_L4_sastituzione0sanitari" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713122 style="height:22pt;border-left:
  none;width:22pt">&nbsp;</td>
  <td class=xl10413122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 22pt;">allacciamenti vari</td>
  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 22pt;">
      <asp:CheckBox ID="C_MAT_L4_allacciamenti0vari" runat="server" Text="." /></td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 22pt;">3. forte
  obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_ANA_L4_forte" runat="server" Text="." /></td>

  <td class=xl8713122 style="border-top:none;border-left:none; height: 22pt; width: 107pt;">sporco</td>
  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 22pt;">
      <asp:TextBox ID="T_ANO_L4_sporco" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 22pt;">sostituzione elemento</td>
  <td class=xl8013122 style="border-top:none;border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_INT_L4_sostituzione0elemento" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6713122 style="height:25pt;border-left:
  none;width:22pt">&nbsp;</td>
  <td class=xl10413122 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 25pt;">sanitari</td>

  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 25pt;">
      <asp:CheckBox ID="C_MAT_L4_sanitari" runat="server" Text="." /></td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 25pt;">4. totale
  obsolescenza</td>
  <td class=xl6613122 style="border-top:none;border-left:none; height: 25pt;">
      <asp:CheckBox ID="C_ANA_L4_tot" runat="server" Text="." /></td>
  <td class=xl8713122 style="border-top:none;border-left:none; height: 25pt; width: 107pt;">disuso</td>
  <td class=xl6813122 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 25pt;">
      <asp:TextBox ID="T_ANO_L4_disuso" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 25pt;">pulizia</td>
  <td class=xl8013122 style="border-top:none;border-left:none; height: 25pt;">
      <asp:CheckBox ID="C_INT_L4_pulizia" runat="server" Text="." /></td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl8513122 style="height:22pt;border-left:none;
  width:22pt">&nbsp;</td>
  <td class=xl11013122 width=131 style="width:98pt; height: 22pt;">&nbsp;</td>
  <td class=xl8513122 width=26 style="width:20pt; height: 22pt;">&nbsp;</td>
  <td class=xl9713122 style="height: 22pt">&nbsp;</td>
  <td class=xl9413122 style="height: 22pt">&nbsp;</td>
  <td class=xl6513122 style="height: 22pt; width: 107pt;">&nbsp;</td>
  <td class=xl8513122 width=26 style="width:20pt; height: 22pt;">&nbsp;</td>

  <td class=xl11413122 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 22pt;">disinfestazioni</td>
  <td class=xl9913122 style="border-top:none;border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_INT_L4_disinfestazioni" runat="server" Text="." /></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td colspan=2 rowspan=2 height=36 class=xl14513122 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black;height:27.0pt'>STATO DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl14713122 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black'>Stato 1</td>
  <td rowspan=2 class=xl12113122 style="border-bottom:1.0pt solid black; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>

  <td class=xl11913122 style="border-top:none;border-left:none; height: 21pt;">Stato 2.1</td>
  <td class=xl11813122 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl11913122 style="border-top:none;border-left:none; height: 21pt; width: 107pt;">Stato 3.1</td>
  <td class=xl11813122 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl14913122 style='border-bottom:1.0pt solid black'>Stato
  3.3</td>
  <td class=xl12113122 style="border-left:none; height: 21pt;">&nbsp;</td>
 </tr>

 <tr height=18 style='height:13.5pt'>
  <td class=xl12013122 style="height:21pt;border-top:none;
  border-left:none">Stato 2.2</td>
  <td class=xl11713122 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl12013122 style="border-top:none;border-left:none; height: 21pt; width: 107pt;">Stato 3.2</td>
  <td class=xl11713122 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl11813122 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>

  <td colspan=11 height=17 class=xl13713122 width=884 style='border-right:1.0pt solid black;
  height:12.75pt;width:666pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7413122 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7513122></td>
  <td class=xl7513122></td>
  <td class=xl7513122></td>

  <td class=xl7513122></td>
  <td class=xl7513122 style="width: 107pt"></td>
  <td class=xl7513122></td>
  <td class=xl7513122></td>
  <td class=xl7613122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7413122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7513122></td>

  <td class=xl7513122 style="width: 22pt"></td>
  <td class=xl7513122></td>
  <td class=xl7513122></td>
  <td class=xl7513122></td>
  <td class=xl7513122></td>
  <td class=xl7513122 style="width: 107pt"></td>
  <td class=xl7513122></td>
  <td class=xl7513122></td>
  <td class=xl7613122>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1513122></td>
  <td class=xl1513122 style="width: 107pt"></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl7813122>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 colspan=11 style='height:12.75pt;border-right:
  1.0pt solid black'>In particolare inserire gli elementi tecnici riportati,
  dimensioni generali ( U.M.) ubicazioni di anomalie più significative (
  percentuale per singola anomalia<span style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&#8805; 60%)</td>
  <td class=xl1513122></td>

  <td class=xl1513122 style="width: 22pt"></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122 style="width: 107pt"></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl7813122>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 colspan=11 style='height:12.75pt;border-right:
  1.0pt solid black'>Per alcuni El. Tecnici potrebbe risultare necessario
  inserire diverse dimensioni in relazione alle diverse tipologie costruttive
  che sono state catalogate.<span style='mso-spacerun:yes'>  </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1513122></td>
  <td class=xl1513122 style="width: 22pt"></td>

  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122 style="width: 107pt"></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl7813122>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1513122 colspan="9" rowspan="19">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="336px" TextMode="MultiLine" Width="800px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7213122 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122 style="width: 22pt">&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>

  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122 style="width: 107pt">&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl7313122>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>

  <td height=18 class=xl7013122 style='height:13.5pt;border-top:none'>&nbsp;</td>
  <td class=xl1513122></td>
  <td class=xl1513122 style="width: 22pt"></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122 style="width: 107pt"></td>
  <td class=xl1513122></td>

  <td class=xl1513122></td>
  <td class=xl7013122 style='border-top:none'>&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl6913122 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl7013122 style="width: 22pt">&nbsp;</td>
  <td class=xl7013122>&nbsp;</td>
  <td class=xl7013122>&nbsp;</td>

  <td class=xl7013122>&nbsp;</td>
  <td class=xl7013122>&nbsp;</td>
  <td class=xl7013122 style="width: 107pt">&nbsp;</td>
  <td class=xl7013122>&nbsp;</td>
  <td class=xl7013122>&nbsp;</td>
  <td class=xl7113122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl12713122 width=884
  style='border-right:1.0pt solid black;height:25.5pt;width:666pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1513122></td>
  <td class=xl1513122 style="width: 22pt"></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>

  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl1513122 style="width: 107pt"></td>
  <td class=xl1513122></td>
  <td class=xl1513122></td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>

  <td class=xl1513122 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="456px" TextMode="MultiLine" Width="800px" Font-Size="12pt"></asp:TextBox></td>

  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7713122 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7813122>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl7713122 style="height:13pt">&nbsp;</td>
  <td class=xl7813122 style="height: 13pt">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7213122 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122 style="width: 22pt">&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>

  <td class=xl6513122>&nbsp;<asp:Button ID="Button1" runat="server" Text="Button" Visible="False" /></td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122 style="width: 107pt">&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl6513122>&nbsp;</td>
  <td class=xl7313122>&nbsp;</td>
 </tr>

 <tr height=0 style='display:none'>

  <td width=54 style='width:41pt'></td>
  <td width=131 style='width:98pt'></td>
  <td style='width:22pt'></td>
  <td width=131 style='width:98pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
  <td style="width:107pt"></td>
  <td width=26 style='width:20pt'></td>

  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
 </tr>

</table>


    
    </div>
    </form>
</body>
</html>
