<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScI.aspx.vb" Inherits="ScI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv=Content-Type content="text/html; charset=windows-1252">
<meta name=ProgId content=Excel.Sheet>
<meta name=Generator content="Microsoft Excel 12">

    <title>Scheda I</title>
   <style id="SCHEDE RILIEVO TUTTE_28804_Styles">

	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1528804
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
.xl6528804
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
.xl6628804
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
.xl6728804
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
.xl6828804
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
.xl6928804
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
.xl7028804
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
.xl7128804
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
.xl7228804
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
.xl7328804
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
.xl7428804
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
.xl7528804
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
.xl7628804
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
.xl7728804
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
.xl7828804
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
.xl7928804
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
.xl8028804
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
.xl8128804
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
.xl8228804
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
.xl8328804
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
.xl8428804
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
.xl8528804
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
.xl8628804
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
	white-space:nowrap;}
.xl8728804
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
.xl8828804
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
.xl8928804
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
.xl9028804
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
.xl9128804
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
.xl9228804
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
.xl9328804
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
.xl9428804
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
.xl9528804
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
.xl9628804
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
.xl9728804
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
.xl9828804
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
.xl9928804
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
.xl10028804
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
.xl10128804
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
.xl10228804
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
.xl10328804
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
.xl10428804
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
.xl10528804
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
.xl10628804
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10728804
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
.xl10828804
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
.xl10928804
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
	white-space:normal;}
.xl11028804
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
.xl11128804
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
.xl11228804
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
.xl11328804
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
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl11428804
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
	white-space:normal;}
.xl11528804
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
.xl11628804
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
.xl11728804
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
.xl11828804
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
.xl11928804
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
.xl12028804
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
.xl12128804
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
.xl12228804
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
.xl12328804
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
.xl12428804
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
.xl12528804
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
.xl12628804
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
.xl12728804
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
	border-top:1.0pt solid windowtext;
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12828804
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
	border-top:1.0pt solid windowtext;
	border-right:1.0pt solid windowtext;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12928804
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
.xl13028804
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
.xl13128804
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
.xl13228804
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
.xl13328804
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
.xl13428804
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
.xl13528804
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
.xl13628804
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
.xl13728804
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
.xl13828804
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
.xl13928804
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
.xl14028804
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
.xl14128804
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
.xl14228804
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
.xl14328804
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
.xl14428804
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
.xl14528804
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
.xl14628804
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
.xl14728804
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
.xl14828804
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
.xl14928804
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
	border-bottom:1.0pt solid windowtext;
	border-left:1.0pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:normal;}
.xl15028804
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
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:normal;}

</style>
 
    
</head>
<body>
    <form id="form1" runat="server">
    <div align=center x:publishsource="Excel" style="text-align: center">
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
 <tr height=46 style='mso-height-source:userset;height:35.1pt'>
  <td colspan=9 height=46 class=xl14928804 width=738 style='height:35.1pt;
  width:556pt'>U. T. IMPIANTI<span style='mso-spacerun:yes'> 
  </span>RISCALDAMENTO E PRODUZIONE H20 CENRALIZZATA - SCHEDA RILIEVO
  IMPIANTI<span style='mso-spacerun:yes'>  </span>RISCALDAMENTO E PRODUZIONE
  H2O CENTRALIZZATA</td>

  <td colspan=2 class=xl12728804 width=171 style="border-right:1.0pt solid black;
  width:129pt; text-align: center;">I</td>
 </tr>
 <tr height=53 style='mso-height-source:userset;height:39.95pt'>
  <td height=53 class=xl11528804 style='height:39.95pt;border-top:
  none;width:41pt'>Scheda I</td>
  <td class=xl11628804 width=131 style='border-top:none;border-left:none;
  width:98pt'>ELEMENTO TECNICO</td>
  <td class=xl11628804 width=54 style='border-top:none;border-left:none;
  width:41pt'></td>
  <td colspan=2 class=xl12928804 width=157 style='border-right:.5pt solid black;
  border-left:none;width:118pt'>MATERIALI E TIPOLOGIE COSTRUTTIVE (x)<br>

    (solo il prevalente)</td>
  <td colspan=2 class=xl12928804 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl12928804 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANOMALIE (%)</td>
  <td colspan=2 class=xl12928804 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=4 height=137 class=xl13328804 style='border-bottom:1.0pt solid black;
  height:102.75pt;border-top:none;width:41pt'>I 1</td>

  <td rowspan=4 class=xl12428804 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>ALIMENTAZIONE<br />
      cad<asp:TextBox ID="Text_mq_I1" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_I1"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl9028804 width=54 style='border-left:none;width:41pt'></td>
  <td class=xl10428804 width=131 style='border-top:none;border-left:none;
  width:98pt'>serbatoio</td>
  <td class=xl8828804 width=26 style='border-left:none;width:20pt'>
      <asp:CheckBox ID="C_MAT_I1_serbatoio" runat="server" Text="." />&nbsp;</td>
  <td class=xl8028804 style='border-top:none;border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8928804 style='border-left:none'>
      <asp:CheckBox ID="C_ANA_I1_nuovo" runat="server" Text="." /></td>

  <td class=xl10428804 width=145 style='border-top:none;border-left:none;
  width:109pt'>perdita da serbatoio</td>
  <td class=xl8828804 width=26 style='border-left:none;width:20pt'>
      <asp:TextBox ID="T_ANO_I1_perdita0serbatoio" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl10428804 width=145 style='border-top:none;border-left:none;
  width:109pt'>prova tenuta serbatoio</td>
  <td class=xl9228804 style='border-left:none'>
      <asp:CheckBox ID="C_INT_I1_tenuta0serbatoio" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=51 style='height:38.25pt'>
  <td class=xl6728804 width=54 style="height:41pt;border-left:
  none;width:41pt">
      &nbsp;</td>
  <td class=xl10328804 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 41pt;">linee di adduzione</td>

  <td class=xl7828804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 41pt;">
      <asp:CheckBox ID="C_MAT_I1_linee0adduzione" runat="server" Text="." /></td>
  <td class=xl6628804 style="border-top:none;border-left:none; height: 41pt;">2. lieve
  obsolescenza</td>
  <td class=xl6628804 style="border-top:none;border-left:none; height: 41pt;">
      <asp:CheckBox ID="C_ANA_I1_lieve" runat="server" Text="." /></td>
  <td class=xl10328804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 41pt;">odore gas</td>
  <td class=xl7828804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 41pt;">
      <asp:TextBox ID="T_ANO_I1_odore0gas" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl10328804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 41pt;">prova tenuta in pressione linea di adduzione</td>
  <td class=xl7928804 style="border-top:none;border-left:none; height: 41pt;">
      <asp:CheckBox ID="C_INT_I1_tenuta0pressione0adduzione" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl6728804 width=54 style='height:25.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl10328804 width=131 style='border-top:none;border-left:none;
  width:98pt'>contatore</td>
  <td class=xl6828804 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_I1_contatore" runat="server" Text="." /></td>
  <td class=xl6628804 style='border-top:none;border-left:none'>3. forte
  obsolescenza</td>
  <td class=xl6628804 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_I1_forte" runat="server" Text="." /></td>
  <td class=xl10328804 width=145 style='border-top:none;border-left:none;
  width:109pt'>accesso contatori difficoltoso</td>

  <td class=xl6828804 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_I1_contatori0difficoltoso" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10328804 width=145 style='border-top:none;border-left:none;
  width:109pt'>spostamento contatore</td>
  <td class=xl7928804 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_I1_spostamento0contatore" runat="server" Text="." /></td>
 </tr>
 <tr height=35 style='height:26.25pt'>
  <td class=xl6728804 width=54 style="height:26pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl10728804 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 26pt;">teleriscaldamento</td>
  <td class=xl7728804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:CheckBox ID="C_MAT_I1_teleriscaldamento" runat="server" Text="." /></td>

  <td class=xl10728804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">4. totale obsolescenza</td>
  <td class=xl8228804 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_ANA_I1_tot" runat="server" Text="." /></td>
  <td class=xl10728804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">presenza serbatoio in disuso</td>
  <td class=xl7728804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_I1_serbatoio0disuso" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10728804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">bonifica serbatoio</td>
  <td class=xl9328804 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_INT_I1_bonifica0serbatoio" runat="server" Text="." /></td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td rowspan=5 height=120 class=xl13328804 style='border-bottom:1.0pt solid black;
  height:90.0pt;border-top:none;width:41pt'>I 2</td>
  <td rowspan=5 class=xl12428804 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>RETE DI DISTRIBUZIONE<br />
      ml<asp:TextBox ID="Text_mq_I2" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_I2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8328804 width=54 style="border-left:none;width:41pt; height: 21pt;"></td>
  <td class=xl10428804 width=131 style="border-left:none;width:98pt; height: 21pt;">interrata/immurata</td>
  <td class=xl10028804 width=26 style="border-left:none;width:20pt; height: 21pt;">
      <asp:CheckBox ID="C_MAT_I2_immuratura" runat="server" Text="." /></td>
  <td class=xl8028804 style="border-left:none; height: 21pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl8028804 style="border-left:none; height: 21pt;">
      <asp:CheckBox ID="C_ANA_I2_nuovo" runat="server" Text="." /></td>
  <td class=xl8628804 style="border-left:none; height: 21pt;">perdite da rete</td>
  <td class=xl10028804 width=26 style="border-left:none;width:20pt; height: 21pt;">
      <asp:TextBox ID="T_ANO_I2_perdite0rete" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10428804 width=145 style="border-left:none;width:109pt; height: 21pt;">analisi
  amianto</td>
  <td class=xl9528804 style="border-left:none; height: 21pt;">
      <asp:CheckBox ID="C_INT_I2_analisi0amianto" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6728804 width=54 style="height:29pt;border-left:none;
  width:41pt">
      &nbsp;</td>

  <td class=xl10328804 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 29pt;">a vista</td>
  <td class=xl6828804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_I2_a0vista" runat="server" Text="." /></td>
  <td class=xl6628804 style="border-top:none;border-left:none; height: 29pt;">2. lieve
  obsolescenza</td>
  <td class=xl6628804 style="border-top:none;border-left:none; height: 29pt;">
      <asp:CheckBox ID="C_ANA_I2_lieve" runat="server" Text="." /></td>
  <td class=xl10128804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 29pt;">mancanza di coibentazione</td>
  <td class=xl6828804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:TextBox ID="T_ANO_I2_mancanza0coibentazione" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10328804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 29pt;">ripristino coibentazione tubazione</td>

  <td class=xl7928804 style="border-top:none;border-left:none; height: 29pt;">
      <asp:CheckBox ID="C_INT_I2_ripristino0coibentazione" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl6728804 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl10628804 width=131 style='width:98pt'></td>
  <td class=xl9128804 width=26 style='border-top:none;width:20pt'>&nbsp;</td>
  <td class=xl6628804 style='border-top:none;border-left:none'>3. forte
  obsolescenza</td>
  <td class=xl6628804 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_I2_forte" runat="server" Text="." /></td>

  <td class=xl8728804 style='border-top:none;border-left:none'>ruggine</td>
  <td class=xl6828804 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_I2_ruggine" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10328804 width=145 style='border-top:none;border-left:none;
  width:109pt'>eliminazione perdite</td>
  <td class=xl7928804 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_I2_eliminazione0perdite" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl6728804 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl10628804 width=131 style='width:98pt'></td>

  <td class=xl6728804 width=26 style='width:20pt'>&nbsp;</td>
  <td class=xl6628804 style='border-top:none;border-left:none'>4. totale
  obsolescenza</td>
  <td class=xl6628804 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_I2_tot" runat="server" Text="." /></td>
  <td class=xl8728804 style='border-top:none;border-left:none'>distacchi
  coibentazione</td>
  <td class=xl6828804 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_I2_distacchi0coibentazione" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10628804 width=145 style='width:109pt'></td>
  <td class=xl9328804 style='border-top:none'>&nbsp;</td>
 </tr>

 <tr height=35 style='height:26.25pt'>
  <td height=35 class=xl8428804 width=54 style='height:26.25pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl10828804 width=131 style='width:98pt'>&nbsp;</td>
  <td class=xl8428804 width=26 style='width:20pt'>&nbsp;</td>
  <td class=xl9728804>&nbsp;</td>
  <td class=xl8428804 width=26 style='width:20pt'>&nbsp;</td>
  <td class=xl10928804 width=145 style='width:109pt'>sospetta presenza amianto</td>
  <td class=xl8428804 width=26 style='width:20pt'>
      <asp:TextBox ID="T_ANO_I2_sospetta0amianto" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl10828804 width=145 style='width:109pt'>&nbsp;</td>
  <td class=xl10528804>&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=5 height=171 class=xl13328804 style='border-bottom:1.0pt solid black;
  height:128.25pt;border-top:none;width:41pt'>I 3</td>
  <td rowspan=5 class=xl12428804 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>TERMINALI<br />
      cad<asp:TextBox ID="Text_mq_I3" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Text_mq_I3"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl9028804 width=54 style='border-left:none;width:41pt'></td>

  <td class=xl10428804 width=131 style='border-top:none;border-left:none;
  width:98pt'>corpi scaldanti parti comuni</td>
  <td class=xl8828804 width=26 style='border-left:none;width:20pt'>
      <asp:CheckBox ID="C_MAT_I3_scaldanti0comuni" runat="server" Text="." /></td>
  <td class=xl8028804 style='border-top:none;border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8928804 style='border-left:none'>
      <asp:CheckBox ID="C_ANA_I3_nuovo" runat="server" Text="." /></td>
  <td class=xl10428804 width=145 style='border-top:none;border-left:none;
  width:109pt'>corpi scaldanti caldi *</td>
  <td class=xl8828804 width=26 style='border-left:none;width:20pt'>
      <asp:TextBox ID="T_ANO_I3_scaldanti0caldi" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10428804 width=145 style='border-top:none;border-left:none;
  width:109pt'>analisi amianto</td>

  <td class=xl9228804 style='border-left:none'>
      <asp:CheckBox ID="C_INT_I3_analisi0amianto" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6728804 width=54 style="height:33pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl10328804 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 33pt;">vaso espansione sotto-tetto</td>
  <td class=xl7828804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 33pt;">
      <asp:CheckBox ID="C_MAT_I3_espansione0sotto0tetto" runat="server" Text="." /></td>
  <td class=xl6628804 style="border-top:none;border-left:none; height: 33pt;">2. lieve
  obsolescenza</td>
  <td class=xl6628804 style="border-top:none;border-left:none; height: 33pt;">
      <asp:CheckBox ID="C_ANA_I3_lieve" runat="server" Text="." /></td>

  <td class=xl10328804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">perdite da corpi scaldanti</td>
  <td class=xl7828804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 33pt;">
      <asp:TextBox ID="T_ANO_I3_perdite0scaldanti" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10328804 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">eliminazione perdite</td>
  <td class=xl7928804 style="border-top:none;border-left:none; height: 33pt;">
      <asp:CheckBox ID="C_INT_I3_eliminazione0perdite" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl6728804 width=54 style='height:25.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl10328804 width=131 style='border-top:none;border-left:none;
  width:98pt'>vaso espansione in CT</td>

  <td class=xl6828804 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_I3_espansione0ct" runat="server" Text="." /></td>
  <td class=xl6628804 style='border-top:none;border-left:none'>3. forte
  obsolescenza</td>
  <td class=xl6628804 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_I3_forte" runat="server" Text="." /></td>
  <td class=xl10328804 width=145 style='border-top:none;border-left:none;
  width:109pt'>ruggine</td>
  <td class=xl6828804 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_I3_ruggine" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10328804 width=145 style='border-top:none;border-left:none;
  width:109pt'>distacco corpi scaldanti</td>
  <td class=xl7928804 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_I3_distacco0scaldanti" runat="server" Text="." /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl6728804 width=54 style='height:25.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl10728804 width=131 style='border-top:none;border-left:none;
  width:98pt'>&nbsp;</td>
  <td class=xl7728804 width=26 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td class=xl11028804 width=145 style='border-top:none;border-left:none;
  width:109pt'>4. totale obsolescenza</td>
  <td class=xl6628804 style='border-top:none'>
      <asp:CheckBox ID="C_ANA_I3_tot" runat="server" Text="." /></td>
  <td class=xl11128804 width=145 style='border-top:none;width:109pt'>sospetta
  presenza amianto</td>

  <td class=xl7828804 width=26 style='border-top:none;width:20pt'>
      <asp:TextBox ID="T_ANO_I3_sospetta0amianto" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl11228804 width=145 style='border-top:none;width:109pt'>&nbsp;</td>
  <td class=xl9328804 style='border-top:none;border-left:none'>&nbsp;</td>
 </tr>
 <tr height=35 style='height:26.25pt'>
  <td height=35 class=xl8428804 width=54 style='height:26.25pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl10628804 width=131 style='width:98pt'></td>
  <td class=xl9628804 width=26 style='width:20pt'>&nbsp;</td>
  <td class=xl10628804 width=145 style='width:109pt'></td>

  <td class=xl9828804 style='border-top:none'>&nbsp;</td>
  <td class=xl10628804 width=145 style='width:109pt'>perdite da vaso di
  espansione</td>
  <td class=xl9428804 width=26 style='border-top:none;width:20pt'>
      <asp:TextBox ID="T_ANO_I3_perdite0espansione" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10628804 width=145 style='width:109pt'></td>
  <td class=xl10528804>&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=4 height=103 class=xl13328804 style='border-bottom:1.0pt solid black;
  height:77.25pt;border-top:none;width:41pt'>I 4</td>

  <td rowspan=4 class=xl12428804 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>SISTEMA DI SMALTIMENTO FUMI<br />
      ml<asp:TextBox ID="Text_mq_I4" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="Text_mq_I4"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8328804 width=54 style="border-top:none;border-left:none;
  width:41pt; height: 33pt;"></td>
  <td class=xl10428804 width=131 style="border-left:none;width:98pt; height: 33pt;">canna
  fumaria esterna edificio</td>
  <td class=xl10028804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 33pt;">
      <asp:CheckBox ID="C_MAT_I4_fumaria0esterna" runat="server" Text="." /></td>
  <td class=xl11328804 style="height: 33pt">1. nuovo<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8028804 style="border-top:none; height: 33pt;">
      <asp:CheckBox ID="C_ANA_I4_nuovo" runat="server" Text="." /></td>

  <td class=xl11428804 width=145 style="width:109pt; height: 33pt;">sospetta presenza amianto</td>
  <td class=xl10028804 width=26 style="border-top:none;width:20pt; height: 33pt;">
      <asp:TextBox ID="T_ANO_I4_presenza0amianto" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10428804 width=145 style="border-left:none;width:109pt; height: 33pt;">analisi
  amianto</td>
  <td class=xl9528804 style="border-top:none;border-left:none; height: 33pt;">
      <asp:CheckBox ID="C_INT_I4_analisi0amianto" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6728804 width=54 style="height:41pt;border-left:none;
  width:41pt">
      &nbsp;</td>
  <td class=xl10328804 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 41pt;">canna fumaria interno edificio</td>

  <td class=xl6828804 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 41pt;">
      <asp:CheckBox ID="C_MAT_I4_fumaria0interna" runat="server" Text="." /></td>
  <td class=xl8128804 style="height: 41pt">2. lieve obsolescenza</td>
  <td class=xl6628804 style="border-top:none; height: 41pt;">
      <asp:CheckBox ID="C_ANA_I4_lieve" runat="server" Text="." /></td>
  <td class=xl10228804 width=145 style="width:109pt; height: 41pt;">fessurazioni strutturali</td>
  <td class=xl6828804 width=26 style="border-top:none;width:20pt; height: 41pt;">
      <asp:TextBox ID="T_ANO_I4_fessurazioni0strutturali" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl10628804 width=145 style="width:109pt; height: 41pt;">intervento di risanamento</td>
  <td class=xl7928804 style="border-top:none; height: 41pt;">
      <asp:CheckBox ID="C_INT_I4_intervento0risanamento" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6728804 width=54 style="height:20pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl10628804 width=131 style="width:98pt; height: 20pt;"></td>
  <td class=xl9128804 width=26 style="border-top:none;width:20pt; height: 20pt;">&nbsp;</td>
  <td class=xl8128804 style="height: 20pt">3. forte obsolescenza</td>
  <td class=xl6628804 style="border-top:none; height: 20pt;">
      <asp:CheckBox ID="C_ANA_I4_forte" runat="server" Text="." /></td>
  <td class=xl1528804 style="height: 20pt">instabilità strutturale</td>

  <td class=xl6828804 width=26 style="border-top:none;width:20pt; height: 20pt;">
      <asp:TextBox ID="T_ANO_I4_instab0strutturale" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10628804 width=145 style="width:109pt; height: 20pt;">sostituzione elemento</td>
  <td class=xl7928804 style="border-top:none; height: 20pt;">
      <asp:CheckBox ID="C_INT_I4_sostituzione0elemento" runat="server" Text="." /></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl8428804 width=54 style="height:24pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl10828804 width=131 style="width:98pt; height: 24pt;">&nbsp;</td>
  <td class=xl8428804 width=26 style="width:20pt; height: 24pt;">&nbsp;</td>

  <td class=xl9728804 style="height: 24pt">4. totale obsolescenza</td>
  <td class=xl9828804 style="border-top:none; height: 24pt;">
      <asp:CheckBox ID="C_ANA_I4_tot" runat="server" Text="." /></td>
  <td class=xl6528804 style="height: 24pt">rottura camino</td>
  <td class=xl8528804 width=26 style="border-top:none;width:20pt; height: 24pt;">
      <asp:TextBox ID="T_ANO_I4_rottura0camino" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10828804 width=145 style="width:109pt; height: 24pt;">strutture di rinforzo</td>
  <td class=xl9928804 style="border-top:none; height: 24pt;">
      <asp:CheckBox ID="C_INT_I4_strutture0rinforzo" runat="server" Text="." /></td>
 </tr>

 <tr height=18 style='height:13.5pt'>
  <td colspan=2 rowspan=2 height=36 class=xl14428804 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black;height:27.0pt'>STATO DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl14628804 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black'>Stato 1</td>
  <td rowspan=2 class=xl12128804 style="border-bottom:1.0pt solid black; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl11928804 style="border-top:none;border-left:none; height: 21pt;">Stato 2.1</td>
  <td class=xl11828804 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl11928804 style="border-top:none;border-left:none; height: 21pt;">Stato 3.1</td>

  <td class=xl11828804 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl14828804 style='border-bottom:1.0pt solid black'>Stato
  3.3</td>
  <td class=xl12128804 style="border-left:none; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl12028804 style="height:20pt;border-top:none;
  border-left:none">Stato 2.2</td>
  <td class=xl11728804 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl12028804 style="border-top:none;border-left:none; height: 20pt;">Stato 3.2</td>

  <td class=xl11728804 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl11828804 style="border-left:none; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl13628804 width=909 style='border-right:1.0pt solid black;
  height:12.75pt;width:685pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7228804 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>

  <td class=xl7428804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7228804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>

  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7328804></td>
  <td class=xl7428804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>

  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 colspan=10 style='height:12.75pt'>In
  particolare inserire gli elementi tecnici riportati, dimensioni generali (
  U.M.) ubicazioni di anomalie più significative ( percentuale per singola
  anomalia<span style='mso-spacerun:yes'> </span></td>

  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&#8805; 60%)</td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>

  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 colspan=10 style='height:12.75pt'>Per alcuni
  El. Tecnici potrebbe risultare necessario inserire diverse dimensioni in
  relazione alle diverse tipologie costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>

  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl1528804 colspan="9" rowspan="20">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="352px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7028804 style="height:13.5pt; width: 41pt;">&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>

  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl7128804>&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td class=xl7528804 style="height:13pt; width: 41pt;">NOTE</td>
  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>

  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>
  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>
  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>
  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>
  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>
  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>
  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>
  <td class=xl6928804 style="border-top:none; height: 13pt;">&nbsp;</td>
  <td class=xl7628804 style="height: 13pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl12628804 width=909
  style='border-right:1.0pt solid black;height:25.5pt;width:685pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>
  <td class=xl1528804></td>

  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl1528804 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="464px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>

  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7528804 style="height:12.75pt; width: 41pt;">&nbsp;</td>
  <td class=xl7628804>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7028804 style="height:13.5pt; width: 41pt;">&nbsp;</td>

  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>
      <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>
  <td class=xl6528804>&nbsp;</td>

  <td class=xl7128804>&nbsp;</td>
 </tr>

 <tr height=0 style='display:none'>
  <td style='width:41pt'></td>
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
