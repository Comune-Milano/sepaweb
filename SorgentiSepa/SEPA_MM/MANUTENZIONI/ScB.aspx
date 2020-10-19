<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScB.aspx.vb" Inherits="ScC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">

<style id="SCHEDE RILIEVO TUTTE_6175_Styles">
<!--table
	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl156175
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
.xl656175
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
.xl666175
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
.xl676175
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
.xl686175
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
.xl696175
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
.xl706175
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
.xl716175
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
.xl726175
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
.xl736175
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
.xl746175
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
.xl756175
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
.xl766175
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
.xl776175
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
.xl786175
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
.xl796175
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
.xl806175
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
.xl816175
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
.xl826175
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
.xl836175
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
.xl846175
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
.xl856175
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
.xl866175
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl876175
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
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl886175
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
.xl896175
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl906175
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl916175
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
.xl926175
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
.xl936175
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
.xl946175
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
.xl956175
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
.xl966175
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
.xl976175
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
.xl986175
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
.xl996175
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
.xl1006175
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
.xl1016175
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
.xl1026175
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
.xl1036175
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
.xl1046175
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
.xl1056175
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
.xl1066175

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
.xl1076175
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1086175
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1096175
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1106175
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
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl1116175
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl1126175
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
	vertical-align:bottom;
	border:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1136175
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
	vertical-align:bottom;
	border:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1146175
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl1156175
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
	white-space:nowrap;}
.xl1166175
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl1176175
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
.xl1186175
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
.xl1196175
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
.xl1206175
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
.xl1216175
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
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl1226175
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
	border-right:none;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl1236175
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
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl1246175
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl1256175
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
-->
</style>

    <title>Scheda B</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align=center x:publishsource="Excel">
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Constantia"
            Font-Size="9pt" ForeColor="#8080FF" Width="504px">Pagina ottimizzata per Internet Explorer 7</asp:Label><br />
        <table style="width: 100%; background-color: gainsboro">
            <tr>
                <td style="width: 223px; height: 57px; text-align: center;">
                    &nbsp;<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                        Style="left: 120px; top: 24px" ToolTip="SALVA" /></td>
                <td style="height: 57px; text-align: center;">
                    &nbsp;</td>
                <td style="width: 164px; height: 57px; text-align: center;">
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
 <tr height=40 style='mso-height-source:userset;height:30.0pt'>
  <td colspan=9 height=40 class=xl916175 width=683 style='height:30.0pt;
  width:514pt'>U. T.CHIUSURE - SCHEDA RILIEVO CHIUSURE</td>

  <td colspan=2 class=xl1126175 width=170 style="border-left:none;width:128pt; text-align: center;">B</td>
 </tr>
 <tr height=53 style='mso-height-source:userset;height:39.95pt'>
  <td height=53 class=xl886175 width=58 style='height:39.95pt;border-top:none;
  width:44pt'>Scheda B</td>
  <td class=xl886175 style="border-top:none;border-left:none;
  width:83pt">ELEMENTO TECNICO</td>
  <td class=xl886175 style="border-top:none;border-left:none;
  width:24pt"></td>
  <td colspan=2 class=xl886175 width=153 style='border-left:none;width:115pt'>MATERIALI
  E TIPOLOGIE COSTRUTTIVE (x)<br>

    (solo il prevalente)</td>
  <td class=xl886175 width=143 style='border-top:none;border-left:none;
  width:107pt'>ANALISI PRESTAZIONALE (X)</td>
  <td class=xl886175 width=26 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td colspan=2 class=xl886175 width=166 style='border-left:none;width:125pt'>ANOMALIE
  (%)</td>
  <td colspan=2 class=xl886175 width=170 style='border-left:none;width:128pt'>INTERVENTI</td>
 </tr>
 <tr height=34 style='height:25.5pt'>

  <td rowspan=13 height=358 class=xl946175 width=58 style='border-bottom:1.0pt solid black;
  height:268.5pt;border-top:none;width:44pt'>B 1</td>
  <td rowspan=13 class=xl946175 style="border-bottom:1.0pt solid black;
  border-top:none;width:83pt">FACCIATE ESTRENE<br />
      mq<asp:TextBox ID="Text_mq_B1" runat="server" MaxLength="9" Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
          ControlToValidate="Text_mq_B1" ErrorMessage="!" Font-Bold="True" Height="1px"
          Style="left: 432px; top: 264px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
          Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=13 class=xl966175 style="border-bottom:1.0pt solid black;
  border-top:none;width:24pt"><br />
      </td>
  <td class=xl696175 style='border-top:none;border-left:none;
  width:95pt'>blocchi laterizio</td>
  <td class=xl696175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B1_blocchi0laterizio" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl666175 style='border-top:none;border-left:none'><asp:CheckBox ID="C_ANA_B1_nuovo" runat="server" Text="." /></td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt">distacchi intonaco / rivestimento esterno</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B1_distacchi0intonaco" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt">risanamento conservativo di facciata</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">
      <asp:CheckBox ID="C_INT_B1_risanamento0facciata" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl696175 style="height:33pt;border-top:none;
  border-left:none;width:95pt">blocchi vibrocompressi</td>

  <td class=xl696175 style="border-top:none;border-left:none;
  width:20pt; height: 33pt;">
      <asp:CheckBox ID="C_MAT_B1_blocchi0vibrocompressi" runat="server" Text="." /></td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 33pt;">2. lieve
  obsolescenza</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 33pt;"><asp:CheckBox ID="C_ANA_B1_lieve" runat="server" Text="." /></td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 33pt;">alterazioni cromatiche</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:70pt; height: 33pt;">
      <asp:TextBox ID="T_ANO_B1_alterazioni0cromatiche" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 33pt;">rifacimento di finitura facciata<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 33pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B1_rifacimento0facciata" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:41pt;border-top:none;
  border-left:none;width:95pt">pannelli prefabbricati</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 41pt;">
      <asp:CheckBox ID="C_MAT_B1_pannelli0prefabbricati" runat="server" Text="." /></td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 41pt;">3. forte
  obsolescenza</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 41pt;"><asp:CheckBox ID="C_ANA_B1_forte" runat="server" Text="." /></td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt; height: 41pt;">fessurazioni</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 41pt;">
      <asp:TextBox ID="T_ANO_B1_fessurazioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 41pt;">contenimento energetico su facciata</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 41pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B1_contenimento0energetico" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:37pt;border-top:none;
  border-left:none;width:95pt">vetro-cemento</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 37pt;">
      <asp:CheckBox ID="C_MAT_B1_vetroàcemento" runat="server" Text="." /></td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 37pt;">4. totale
  obsolescenza</td>

  <td class=xl666175 style="border-top:none;border-left:none; height: 37pt;"><asp:CheckBox ID="C_ANA_B1_TOT" runat="server" Text="." /></td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt; height: 37pt;">rigonfiamneti</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 37pt;">
      <asp:TextBox ID="T_ANO_B1_rigonfiamenti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 37pt;">pulizia</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 37pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B1_pulizia" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:28pt;border-top:none;
  border-left:none;width:95pt">pietra</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_B1_pietra" runat="server" Text="." /></td>
  <td rowspan=9 class=xl1036175 style='border-bottom:1.0pt solid black;
  border-top:none'>&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt; height: 28pt;">distacchi di zoccolatura</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_B1_distacchi0zoccolatura" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 28pt;">ricostruzione zoccolatura</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 28pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B1_ricostruzione0zoccoloatura" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:33pt;border-top:none;
  border-left:none;width:95pt">con cappotto esterno</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 33pt;">
      <asp:CheckBox ID="C_MAT_B1_cappotto0esterno" runat="server" Text="." /></td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 33pt;">&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt; height: 33pt;">graffiti</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 33pt;">
      <asp:TextBox ID="T_ANO_B1_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td rowspan=8 class=xl966175 style="border-bottom:1.0pt solid black;
  border-top:none;width:97pt">&nbsp;</td>
  <td rowspan=8 class=xl1146175 style="border-bottom:1.0pt solid black;
  border-top:none; width: 21pt;">&nbsp;</td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:31pt;border-top:none;
  border-left:none;width:95pt">con intonaco esterno<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:CheckBox ID="C_MAT_B1_intonaco0esterno" runat="server" Text="." /></td>
  <td rowspan=7 class=xl1036175 style='border-bottom:1.0pt solid black;
  border-top:none'>&nbsp;</td>
  <td rowspan=7 class=xl1016175 style="border-bottom:1.0pt solid black;
  border-top:none;width:100pt">&nbsp;</td>
  <td rowspan=7 class=xl1016175 style="border-bottom:1.0pt solid black;
  border-top:none;width:70pt">&nbsp;</td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:32pt;border-top:none;
  border-left:none;width:95pt">intonaco esterno plastico</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 32pt;">
      <asp:CheckBox ID="C_MAT_B1_interno0plastico" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:30pt;border-top:none;
  border-left:none;width:95pt">a vista</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_B1_a0vista" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl696175 style="height:29pt;border-top:none;
  border-left:none;width:95pt">tinteggiato</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_B1_tinteggiato" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:29pt;border-top:none;
  border-left:none;width:95pt">con zoccolatura in pietra</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_B1_zoccolatura0pietra" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:29pt;border-top:none;
  border-left:none;width:95pt">singolo paramento</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_B1_singolo0paramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl716175 style='height:13.5pt;border-top:none;
  border-left:none;width:95pt'>doppio paramento</td>
  <td class=xl716175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B1_doppio0paramento" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=68 style='height:51.0pt'>
  <td height=68 class=xl786175 width=58 style='height:51.0pt;width:44pt'>B 2</td>
  <td class=xl786175 style="border-left:none;width:83pt">CHIUSURE
  PERIMETRALI INFISSI ESTERNI</td>
  <td class=xl746175 style="border-left:none;width:24pt">
      </td>
  <td class=xl766175 style="border-left:none; width: 95pt;">&nbsp;</td>
  <td class=xl766175 style="border-left:none; width: 20pt;">&nbsp;</td>
  <td class=xl766175 style='border-left:none'>&nbsp;</td>

  <td class=xl766175 style='border-left:none'>&nbsp;</td>
  <td class=xl766175 style="border-left:none; width: 100pt;">&nbsp;</td>
  <td class=xl766175 style="border-left:none; width: 70pt;">&nbsp;</td>
  <td class=xl766175 style="border-left:none; width: 97pt;">&nbsp;</td>
  <td class=xl766175 style="border-left:none; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=8 height=204 class=xl966175 width=58 style='border-bottom:.5pt solid black;
  height:153.0pt;border-top:none;width:44pt'>B2.1</td>

  <td rowspan=8 class=xl966175 style="border-bottom:.5pt solid black;
  border-top:none;width:83pt">a luce fissa<br />
      <strong>mq</strong><asp:TextBox ID="Text_mq_B21" runat="server" MaxLength="9" Width="68px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_B21"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=8 class=xl966175 style="border-bottom:.5pt solid black;
  border-top:none;width:24pt">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:95pt; height: 28pt;">telaio il legno</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_B21_telaio0legno" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;"><asp:CheckBox ID="C_ANA_B21_nuovo" runat="server" Text="." /></td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 28pt;">corrosione profili</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_B21_corrosione0profili" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 28pt;">sostituzione serramenti</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 28pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B21_sostituzione0serramenti" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl696175 style="height:30pt;border-top:none;
  border-left:none;width:95pt">telaio in alluminio</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_B21_telaio0alluminio" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 30pt;">2. lieve
  obsolescenza</td>

  <td class=xl666175 style="border-top:none;border-left:none; height: 30pt;"><asp:CheckBox ID="C_ANA_B21_lieve" runat="server" Text="." /></td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 30pt;">rottura lastra</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_B21_rottura0lastra" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 30pt;">pulizia</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 30pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B21_pulizia" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl696175 style='height:25.5pt;border-top:none;
  border-left:none;width:95pt'>teleaio in ferro / acciaio</td>

  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B21_telaio0ferro" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>3. forte
  obsolescenza</td>
  <td class=xl666175 style='border-top:none;border-left:none'><asp:CheckBox ID="C_ANA_B21_forte" runat="server" Text="." /></td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt">graffiti</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B21_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt">sostituzione lastra</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">
      <asp:CheckBox ID="C_INT_B21_sostituzione0lastra" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:32pt;border-top:none;
  border-left:none;width:95pt">telaio in pvc</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 32pt;">
      <asp:CheckBox ID="C_MAT_B21_telaio0pvc" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 32pt;">4. totale
  obsolescenza</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 32pt;"><asp:CheckBox ID="C_ANA_B21_tot" runat="server" Text="." /></td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 32pt;">degrado guarnizioni</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 32pt;">
      <asp:TextBox ID="T_ANO_B21_degrado0guarnizioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 32pt;">revisione integrale serramento</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 32pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B21_integrale0serramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:32pt;border-top:none;
  border-left:none;width:95pt">telaio misto</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 32pt;">
      <asp:CheckBox ID="C_MAT_B21_telaio0misto" runat="server" Text="." />&nbsp;</td>
  <td rowspan=4 class=xl1036175 style='border-bottom:.5pt solid black;
  border-top:none'>&nbsp;</td>

  <td rowspan=4 class=xl1036175 style='border-bottom:.5pt solid black;
  border-top:none'>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 32pt;">sporco</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 32pt;">
      <asp:TextBox ID="T_ANO_B21_sporco" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 32pt;">revisione semplice serramento</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 32pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B21_semplice0serramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:30pt;border-top:none;
  border-left:none;width:95pt">vetro singolo</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_B21_vetro0singolo" runat="server" Text="." />&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 30pt;">distacchi da muratura</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_B21_distacchi0muratura" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 30pt;">&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 30pt; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:30pt;border-top:none;
  border-left:none;width:95pt">vetro doppio</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_B21_vetro0doppio" runat="server" Text="." />&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 30pt;">&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 30pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 30pt;">&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 30pt; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:29pt;border-top:none;
  border-left:none;width:95pt">vetro retinato</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_B21_vetro0retinato" runat="server" Text="." />&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 29pt;">&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 29pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 29pt;">&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 29pt; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='page-break-before:always;height:25.5pt'>
  <td rowspan=14 height=494 class=xl966175 width=58 style='border-bottom:1.0pt solid black;
  height:370.5pt;border-top:none;width:44pt'>B2.2</td>

  <td rowspan=14 class=xl966175 style="border-bottom:1.0pt solid black;
  border-top:none;width:83pt">a luce con apertura<br />
      <strong>mq</strong><asp:TextBox ID="Text_mq_B22" runat="server" MaxLength="9" Width="70px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_B22"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=14 class=xl966175 style="border-bottom:1.0pt solid black;
  border-top:none;width:24pt">
      &nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:95pt; height: 26pt;">telaio il legno</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:CheckBox ID="C_MAT_B22_telaio0legno" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 26pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 26pt;"><asp:CheckBox ID="C_ANA_B22_nuovo" runat="server" Text="." /></td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 26pt;">corrosione profili</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_B22_corrosione0profili" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 26pt;">sostituzione serramento</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 26pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B22_sostituzione0serramento" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl696175 style="height:29pt;border-top:none;
  border-left:none;width:95pt">telaio in alluminio</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_B22_telaio0alluminio" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 29pt;">2. lieve
  obsolescenza</td>

  <td class=xl666175 style="border-top:none;border-left:none; height: 29pt;"><asp:CheckBox ID="C_ANA_B22_lieve" runat="server" Text="." /></td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 29pt;">rottura lastra</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 29pt;">
      <asp:TextBox ID="T_ANO_B22_rottura0lastra" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 29pt;">pulizia</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt; height: 29pt;">
      <asp:CheckBox ID="C_INT_B22_pulizia" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl696175 style="height:31pt;border-top:none;
  border-left:none;width:95pt">teleaio in ferro / acciaio</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:CheckBox ID="C_MAT_B22_telaio0acciaio" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 31pt;">3. forte
  obsolescenza</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 31pt;"><asp:CheckBox ID="C_ANA_B22_forte" runat="server" Text="." /></td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt; height: 31pt;">graffiti</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 31pt;">
      <asp:TextBox ID="T_ANO_B22_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 31pt;">sostituzione lastra</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 31pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B22_sostituzione0lastra" runat="server" Text="." /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:30pt;border-top:none;
  border-left:none;width:95pt">telaio in pvc</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_B22_telaio0pvc" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 30pt;">4. totale
  obsolescenza</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 30pt;"><asp:CheckBox ID="C_ANA_B22_tot" runat="server" Text="." /></td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 30pt;">degrado guarnizioni</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_B22_degrado0guarnizioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 30pt;">revisione integrale serramento</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 30pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B22_integrale0serramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:28pt;border-top:none;
  border-left:none;width:95pt">telaio misto</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_B22_telaio0misto" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>

  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 28pt;">sporco</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_B22_sporco" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 28pt;">revisione semplice serramento</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 28pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B22_semplice0serramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:28pt;border-top:none;
  border-left:none;width:95pt">vetro singolo</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_B22_vetro0singolo" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 28pt;">distacchi da muratura</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_B22_distacchi0muratura" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 28pt;">sostituzione sistemi oscuramento</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 28pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B22_sostituzione0oscuramento" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=68 style='height:51.0pt'>
  <td class=xl676175 style="height:53pt;border-top:none;
  border-left:none;width:95pt">vetro doppio</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 53pt;">
      <asp:CheckBox ID="C_MAT_B22_vetro0doppio" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 53pt;">&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 53pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 53pt;">malfunzionamento organi di apertura / chiusura / movimento dei
  serramenti<span style='mso-spacerun:yes'> </span></td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 53pt;">
      <asp:TextBox ID="T_ANO_B22_organi0serramenti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>

  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 53pt;">revisione integrale sistemi oscuramento</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 53pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B22_integrale0oscuramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:29pt;border-top:none;
  border-left:none;width:95pt">vetro retinato</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_B22_vetro0retinato" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 29pt;">&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 29pt;">&nbsp;</td>

  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 29pt;">instabilità sostegno sistemi oscuramento</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 29pt;">
      <asp:TextBox ID="T_ANO_B22_sostegno0oscuramento" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 29pt;">revisione semplice sistemi oscuramento</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 29pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B22_semplice0oscuramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=85 style='height:63.75pt'>
  <td class=xl676175 style="height:61pt;border-top:none;
  border-left:none;width:95pt">a battente</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 61pt;">
      <asp:CheckBox ID="C_MAT_B22_a0battente" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 61pt;">&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 61pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 61pt;">malfunzionamento organi di apertura / chiusura / movimento dei
  sistemi di oscuramento</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 61pt;">
      <asp:TextBox ID="T_ANO_B22_organi0oscuramento" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 61pt;"></td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 61pt; width: 21pt;">
      &nbsp;</td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:40pt;border-top:none;
  border-left:none;width:95pt">a vasistas</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 40pt;">
      <asp:CheckBox ID="C_MAT_B22_a0vesitas" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 40pt;">&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 40pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:100pt; height: 40pt;">degrado sistemi oscuramento</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 40pt;">
      <asp:TextBox ID="T_ANO_B22_degrado0oscuramento" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 40pt;">&nbsp;</td>

  <td class=xl736175 style="border-top:none;border-left:none; height: 40pt; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:27pt;border-top:none;
  border-left:none;width:95pt">scorrevole</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 27pt;">
      <asp:CheckBox ID="C_MAT_B22_scorrevole" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 27pt;">&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 27pt;">&nbsp;</td>
  <td rowspan=4 class=xl966175 style="border-bottom:1.0pt solid black;
  border-top:none;width:100pt">&nbsp;</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 27pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 27pt;">&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 27pt; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:27pt;border-top:none;
  border-left:none;width:95pt">con avvolgibili</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 27pt;">
      <asp:CheckBox ID="C_MAT_B22_con0avvolgibili" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 27pt;">&nbsp;</td>

  <td class=xl666175 style="border-top:none;border-left:none; height: 27pt;">&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 27pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 27pt;">&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 27pt; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl676175 style="height:28pt;border-top:none;
  border-left:none;width:95pt">con persiane</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_B22_con0persiane" runat="server" Text="." />&nbsp;</td>

  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 28pt;">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 28pt;">&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; height: 28pt; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=35 style='height:26.25pt'>
  <td height=35 class=xl676175 style='height:26.25pt;border-top:none;
  border-left:none;width:95pt'>con ante a libro o scorrevoli</td>

  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B22_ante0scorrevoli" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt">&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=51 style='page-break-before:always;height:38.25pt'>
  <td rowspan=11 height=425 class=xl956175 width=58 style='height:318.75pt;
  width:44pt'>B 3</td>

  <td rowspan=11 class=xl956175 style="width:83pt">PILOTIS O
  PORTICATO<br />
      mq<asp:TextBox ID="Text_mq_b3" runat="server" MaxLength="9" Width="68px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Text_mq_b3"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=11 class=xl976175 style="width:24pt"><br />
      </td>
  <td class=xl756175 style='border-left:none;width:95pt'>struttura in
  cls armato</td>
  <td class=xl756175 style='border-left:none;width:20pt'>
      <asp:CheckBox ID="C_MAT_B3_struttura0cls0armato" runat="server" Text="." />&nbsp;</td>
  <td class=xl706175 style='border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl706175 style="border-left:none; text-align: center;"><asp:CheckBox ID="C_ANA_B3_nuovo" runat="server" Text="." /></td>

  <td class=xl756175 style="border-top:none;border-left:none;
  width:100pt">riconducibili a cedimenti strutturali</td>
  <td class=xl756175 style="border-left:none;width:70pt">
      <asp:TextBox ID="T_ANO_B3_cedim0strutt" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl756175 style="border-left:none;width:97pt">intervento
  di risanamento conservativo</td>
  <td class=xl726175 style="border-left:none; width: 21pt;">
      <asp:CheckBox ID="C_INT_B3_risanamento0conservativo" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:30pt;border-top:none;
  border-left:none;width:95pt">struttura in blocchi laterizio</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_B3_blocchi0laterzio" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 30pt;">2. lieve
  obsolescenza</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 30pt;"><asp:CheckBox ID="C_ANA_B3_lieve" runat="server" Text="." /></td>
  <td class=xl776175 style="border-top:none;border-left:none;
  width:100pt; height: 30pt;">mancanza pannelli di controsoffitto</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_B3_mancanza0controsoffitto" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 30pt;">intervento di rifacimento finitura</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt; height: 30pt;">
      <asp:CheckBox ID="C_INT_B3_rifacimento0finitura" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=51 style='height:38.25pt'>
  <td class=xl676175 style="height:40pt;border-top:none;
  border-left:none;width:95pt">struttura in pannelli prefabbricati</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 40pt;">
      <asp:CheckBox ID="C_MAT_B3_struttura0prefabbricati" runat="server" Text="." /></td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 40pt;">3. forte
  obsolescenza</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 40pt;"><asp:CheckBox ID="C_ANA_B3_forte" runat="server" Text="." /></td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt; height: 40pt;">deformazione struttura</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 40pt;">
      <asp:TextBox ID="T_ANO_B3_deformazione0struttura" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:97pt; height: 40pt;">intervento di contenimento energetico</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt; height: 40pt;">
      <asp:CheckBox ID="C_INT_B3_int0contenimento0energetico" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl676175 style='height:25.5pt;border-top:none;
  border-left:none;width:95pt'>con cappotto esterno</td>
  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B3_cappotto0esterno" runat="server" Text="." /></td>
  <td class=xl666175 style='border-top:none;border-left:none'>4. totale
  obsolescenza</td>

  <td class=xl666175 style='border-top:none;border-left:none'><asp:CheckBox ID="C_ANA_B3_tot" runat="server" Text="." /></td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt">graffiti</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B3_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt">pulizia</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">
      <asp:CheckBox ID="C_INT_B3_pulizia" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=51 style='height:38.25pt'>
  <td height=51 class=xl676175 style='height:38.25pt;border-top:none;
  border-left:none;width:95pt'>con intonaco esterno<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B3_intonaco0esterno" runat="server" Text="." />&nbsp;</td>
  <td rowspan=7 class=xl1036175 style='border-top:none'>&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt">presenza di distacchi intonco / rivestimento esterno</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B3_di0distacchi" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt">fissaggio elementi finitura</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">
      <asp:CheckBox ID="C_INT_B3_fissaggio0finitura" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl676175 style="height:28pt;border-top:none;
  border-left:none;width:95pt">a vista</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_B3_a0vista" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt; height: 28pt;">alterazioni cromatiche</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_B3_alterazioni0cromatiche" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl696175 style="border-top:none;border-left:none;
  width:97pt; height: 28pt;">sistemazione pavimento</td>

  <td class=xl736175 style="border-top:none;border-left:none; height: 28pt; width: 21pt;">
      <asp:CheckBox ID="C_INT_B3_sistemazione0pavimento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl676175 style='height:25.5pt;border-top:none;
  border-left:none;width:95pt'>con controsoffitto in metallo</td>
  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B3_centrosoffitto0metallo" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt">fessurazioni</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B3_fessurazioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>

  <td rowspan=5 class=xl966175 style="border-top:none;width:97pt">&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl676175 style='height:25.5pt;border-top:none;
  border-left:none;width:95pt'>con controsoffitto in gesso</td>
  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B3_controsoffitto0gesso" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt">rigonfiamenti</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B3_rigonfiamenti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl676175 style='height:25.5pt;border-top:none;
  border-left:none;width:95pt'>con pavimento in battuto di cemento</td>
  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B3_pavimento0cemento" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt">distacchi zoccolatura</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B3_distacchi0zoccolatura" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl676175 style='height:25.5pt;border-top:none;
  border-left:none;width:95pt'>con pavimento in pietra</td>
  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B3_pavimento0pietra" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt">disconnessioni nel pavimento</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B3_disconn0pavimento" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl676175 style='height:25.5pt;border-top:none;
  border-left:none;width:95pt'>con pavimento in piastrelle</td>
  <td class=xl676175 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_B3_pavimento0piastrelle" runat="server" Text="." />&nbsp;</td>
  <td class=xl666175 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl676175 style="border-top:none;border-left:none;
  width:100pt">rottura / mancanza parti pavimento</td>

  <td class=xl676175 style="border-top:none;border-left:none;
  width:70pt">
      <asp:TextBox ID="T_ANO_B3_parti0pavimento" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl736175 style="border-top:none;border-left:none; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=2 rowspan=2 height=34 class=xl1076175 style='height:25.5pt'>STATO
  DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl1086175>Stato 1</td>
  <td rowspan=2 class=xl906175 style="border-top:none; width: 20pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl896175 style="border-left:none; height: 22pt;">Stato 2.1</td>

  <td class=xl906175 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 22pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl896175 style="border-top:none;border-left:none; width: 100pt; height: 22pt;">Stato 3.1</td>
  <td class=xl906175 style="border-top:none;border-left:none; width: 70pt; vertical-align: top; text-align: left; height: 22pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl1086175 style="width: 97pt">Stato 3.3</td>
  <td class=xl906175 style="border-top:none;border-left:none; width: 21pt; height: 22pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl896175 style="height:22pt;border-top:none;
  border-left:none">Stato 2.2</td>

  <td class=xl906175 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 22pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl896175 style="border-top:none;border-left:none; width: 100pt; height: 22pt;">Stato 3.2</td>
  <td class=xl906175 style="border-top:none;border-left:none; width: 70pt; vertical-align: top; text-align: left; height: 22pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl906175 style="border-top:none;border-left:none; width: 21pt; vertical-align: top; text-align: left; height: 22pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl1216175 width=853 style='border-right:.5pt solid black;
  height:12.75pt;width:642pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl796175 colspan=4 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl686175 style="width: 20pt"></td>
  <td class=xl686175></td>
  <td class=xl686175></td>
  <td class=xl686175 style="width: 100pt"></td>
  <td class=xl686175 style="width: 70pt"></td>

  <td class=xl686175 style="width: 97pt"></td>
  <td class=xl806175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl156175 style="width: 83pt"></td>
  <td class=xl156175 style="width: 24pt"></td>
  <td class=xl156175 style="width: 95pt"></td>
  <td class=xl156175 style="width: 20pt"></td>

  <td class=xl156175></td>
  <td class=xl156175></td>
  <td class=xl156175 style="width: 100pt"></td>
  <td class=xl156175 style="width: 70pt"></td>
  <td class=xl156175 style="width: 97pt"></td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 colspan=7 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>

  <td class=xl156175 style="width: 100pt"></td>
  <td class=xl156175 style="width: 70pt"></td>
  <td class=xl156175 style="width: 97pt"></td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=32 style='mso-height-source:userset;height:24.0pt'>
  <td colspan=11 height=32 class=xl1246175 width=853 style='border-right:.5pt solid black;
  height:24.0pt;width:642pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola anomalia&gt;= 60%)<span
  style='mso-spacerun:yes'> </span></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 height=17 class=xl1176175 width=853 style='border-right:.5pt solid black;
  height:12.75pt;width:642pt'>Per alcuni El. Tecnici potrebbe risultare
  necessario inserire diverse dimensioni in relazione alle diverse tipologie
  costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl816175 style="height:13pt">&nbsp;</td>
  <td class=xl156175 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="464px" Width="768px" Font-Size="12pt" TextMode="MultiLine"></asp:TextBox></td>
  <td class=xl826175 style="height: 13pt; width: 21pt;">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl816175 style="height:13pt">&nbsp;</td>
  <td class=xl826175 style="height: 13pt; width: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>

  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>

  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>

  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl866175 style='height:13.5pt'>&nbsp;</td>
  <td class=xl656175 style="width: 83pt">&nbsp;</td>
  <td class=xl656175 style="width: 24pt">&nbsp;</td>
  <td class=xl656175 style="width: 95pt">&nbsp;</td>
  <td class=xl656175 style="width: 20pt">&nbsp;</td>
  <td class=xl656175>&nbsp;</td>
  <td class=xl656175>&nbsp;</td>

  <td class=xl656175 style="width: 100pt">&nbsp;</td>
  <td class=xl656175 style="width: 70pt">&nbsp;</td>
  <td class=xl656175 style="width: 97pt">&nbsp;</td>
  <td class=xl876175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl816175 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl156175 style="width: 24pt"></td>

  <td class=xl156175 style="width: 95pt"></td>
  <td class=xl156175 style="width: 20pt"></td>
  <td class=xl156175></td>
  <td class=xl156175></td>
  <td class=xl156175 style="width: 100pt"></td>
  <td class=xl156175 style="width: 70pt"></td>
  <td class=xl156175 style="width: 97pt"></td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl1206175 width=853
  style='height:25.5pt;width:642pt'>Inserire il repertorio fotografico (con
  precisazione della postazione e collegamento con el.tecnico/anomalia) e le
  note inerenti ad anomalie non previste nella scheda di rilievo, nonché le
  segnalazioni di eventuali difficoltà logistiche (ad esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl816175 style="height:13pt">&nbsp;</td>
  <td class=xl156175 colspan="9" rowspan="30">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="464px" Width="768px" Font-Size="12pt" TextMode="MultiLine"></asp:TextBox>
      &nbsp;&nbsp;
      </td>
  <td class=xl826175 style="height: 13pt; width: 21pt;">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>

  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>

  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>

  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl816175 style='height:12.75pt'>&nbsp;</td>
  <td class=xl826175 style="width: 21pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl836175 style="height:43pt">&nbsp;</td>
  <td class=xl846175 style="height: 43pt; width: 83pt;">
      &nbsp;</td>
  <td class=xl846175 style="height: 43pt; width: 24pt;">&nbsp;</td>
  <td class=xl846175 style="width: 95pt; height: 43pt;">&nbsp;</td>
  <td class=xl846175 style="width: 20pt; height: 43pt;">&nbsp;</td>
  <td class=xl846175 style="height: 43pt">&nbsp;<asp:Button ID="Crea" runat="server" Text="Crea" Visible="False" /></td>
  <td class=xl846175 style="height: 43pt">&nbsp;</td>
  <td class=xl846175 style="height: 43pt; width: 100pt;">&nbsp;</td>

  <td class=xl846175 style="height: 43pt; width: 70pt;">&nbsp;</td>
  <td class=xl846175 style="height: 43pt; width: 97pt;">
      &nbsp;</td>
  <td class=xl856175 style="height: 43pt; width: 21pt;">&nbsp;</td>
 </tr>

 <tr height=0 style='display:none'>
  <td width=58 style="width:44pt; height: 20px;"></td>
  <td style="width:83pt; height: 20px;"></td>
  <td style="width:24pt; height: 20px;"></td>

  <td style="width:95pt; height: 20px;"></td>
  <td style="width:20pt; height: 20px;"></td>
  <td width=143 style="width:107pt; height: 20px;"></td>
  <td width=26 style="width:20pt; height: 20px;"></td>
  <td style="width:100pt; height: 20px;"></td>
  <td style="width:70pt; height: 20px;"></td>
  <td style="width:97pt; height: 20px;"></td>
  <td style="width:21pt; height: 20px;"></td>
 </tr>


</table>

    
    </div>
        &nbsp;<br />
    </form>
</body>
</html>
