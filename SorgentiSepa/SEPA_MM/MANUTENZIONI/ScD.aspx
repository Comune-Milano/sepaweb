<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScD.aspx.vb" Inherits="ScD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">
    <title>Scheda D</title>
    
  <style id="SCHEDE RILIEVO TUTTE_9167_Styles">

	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl159167
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
.xl659167
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
.xl669167
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
.xl679167
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
.xl689167
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
.xl699167
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
.xl709167
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
.xl719167
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
.xl729167
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
.xl739167
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
.xl749167
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
.xl759167
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
.xl769167
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
.xl779167
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
.xl789167
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
.xl799167
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
.xl809167
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
.xl819167
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
.xl829167
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
	border-bottom:.5pt dashed windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl839167
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
.xl849167
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
.xl859167
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
.xl869167
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
.xl879167
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
.xl889167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl899167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl909167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl919167
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
.xl929167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl939167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl949167
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
.xl959167
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
.xl969167
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
.xl979167
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
.xl989167
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
.xl999167
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
.xl1009167
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
.xl1019167
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
.xl1029167
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
.xl1039167
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
.xl1049167
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
.xl1059167
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
.xl1069167
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
.xl1079167
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
.xl1089167
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
.xl1099167
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
.xl1109167
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
.xl1119167
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
.xl1129167
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
.xl1139167
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
.xl1149167
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
.xl1159167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1169167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1179167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1189167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1199167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1209167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1219167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1229167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1239167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1249167
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1259167
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl1269167
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl1279167
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl1289167
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
.xl1299167
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
.xl1309167
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
.xl1319167
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

</style>
  
</head>
<body>
    <form id="form1" runat="server">
    <div align=center x:publishsource="Excel">
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Italic="False" Font-Names="Constantia"
            Font-Size="9pt" ForeColor="#8080FF" Width="504px">Pagina ottimizzata per Internet Explorer 7</asp:Label><br />
        <table style="width: 100%; background-color: gainsboro; text-align: center">
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
    <table border=0 cellpadding=0 cellspacing=0 width=937 style='border-collapse:
 collapse;table-layout:fixed;width:707pt' id="TABLE1">
 <col width=54 style='mso-width-source:userset;mso-width-alt:1974;width:41pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=54 style='mso-width-source:userset;mso-width-alt:1974;width:41pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>

 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <tr height=40 style='mso-height-source:userset;height:30.0pt'>
  <td colspan=9 height=40 class=xl1059167 width=766 style='height:30.0pt;
  width:578pt'>U. T. PAVIMENTAZIONI INTERNE - SCHEDA RILIEVO PAVIMENTAZIONI
  INTERNE</td>
  <td colspan=2 class=xl1079167 width=171 style='border-right:1.0pt solid black;
  width:129pt'>D</td>

 </tr>
 <tr height=66 style='mso-height-source:userset;height:50.1pt'>
  <td height=66 class=xl949167 width=54 style='height:50.1pt;width:41pt'>Scheda
  D</td>
  <td class=xl919167 width=145 style='border-left:none;width:109pt'>ELEMENTO
  TECNICO</td>
  <td class=xl919167 width=54 style='border-left:none;width:41pt'></td>
  <td colspan=2 class=xl1289167 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>MATERIALI E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td colspan=2 class=xl1289167 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl1289167 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANOMALIE (%)</td>
  <td colspan=2 class=xl1289167 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=9 height=222 class=xl1099167 width=54 style='border-bottom:1.0pt solid black;
  height:166.5pt;width:41pt'>D 1</td>
  <td rowspan=9 class=xl999167 width=145 style='border-bottom:1.0pt solid black;
  width:109pt'>PAVIMENTAZIONI INTERNE PERCORSI ORIZZONTALI<br />
      mq<asp:TextBox ID="Text_mq_D1" runat="server" MaxLength="9" Width="106px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
          ControlToValidate="Text_mq_D1" ErrorMessage="!" Font-Bold="True" Height="1px"
          Style="left: 432px; top: 264px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
          Width="1px"></asp:RegularExpressionValidator></td>

  <td rowspan=9 class=xl1009167 width=54 style='border-bottom:1.0pt solid black;
  width:41pt'></td>
  <td class=xl839167 width=145 style="border-left:none;width:109pt; height: 13pt;">
      pavimento
  galleggiante</td>
  <td class=xl839167 style="border-left:none;width:19pt; height: 13pt;">
      <asp:CheckBox ID="C_MAT_D1_pavimento0galleggia" runat="server" Text="." /></td>
  <td class=xl809167 style="border-left:none; height: 13pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl809167 style="border-left:none; width: 20pt; height: 13pt;">&nbsp;<asp:CheckBox ID="C_ANA_D1_nuovo" runat="server" Text="." /></td>
  <td class=xl839167 style="border-left:none;width:109pt; height: 13pt;">distacchi
  zoccolino</td>

  <td class=xl839167 style="border-left:none;width:34pt; height: 13pt;">&nbsp;<asp:TextBox ID="T_ANO_D1_distacchi0zoccolino" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl839167 width=145 style="border-left:none;width:109pt; height: 13pt;">pulizia</td>
  <td class=xl859167 style="border-left:none; height: 13pt;">
      <asp:CheckBox ID="C_INT_D1_pulizia" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl789167 width=145 style="height:28pt;border-top:none;
  border-left:none;width:109pt">ceramica</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_D1_ceramica" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 28pt;">2. lieve
  obsolescenza</td>

  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 28pt;">&nbsp;<asp:CheckBox ID="C_ANA_D1_lieve" runat="server" Text="." /></td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">graffiti</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_D1_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">sostituzione parti pavimento</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_INT_D1_sostituzione0pavimento" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl679167 width=145 style="height:21pt;border-top:none;
  border-left:none;width:109pt">pietra</td>

  <td class=xl679167 style="border-top:none;border-left:none;
  width:19pt; height: 21pt;">
      <asp:CheckBox ID="C_MAT_D1_pietra" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 21pt;">3. forte
  obsolescenza</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 21pt;">&nbsp;<asp:CheckBox ID="C_ANA_D1_forte" runat="server" Text="." /></td>
  <td class=xl679167 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">fessurazioni</td>
  <td class=xl679167 style="border-top:none;border-left:none;
  width:34pt; height: 21pt;">
      <asp:TextBox ID="T_ANO_D1_fessurazioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">fissaggio elementi</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 21pt;">
      <asp:CheckBox ID="C_INT_D1_fissaggio0elementi" runat="server" Text="." /></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl789167 width=145 style="height:32pt;border-top:none;
  border-left:none;width:109pt">linoleum/pvc</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 32pt;">
      <asp:CheckBox ID="C_MAT_D1_linoleum" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 32pt;">4. totale
  obsolescenza</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 32pt;">&nbsp;<asp:CheckBox ID="C_ANA_D1_tot" runat="server" Text="." /></td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">rotture</td>

  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 32pt;">&nbsp;<asp:TextBox ID="T_ANO_D1_rotture" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">pavimentazione</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 32pt;">
      <asp:CheckBox ID="C_INT_D1_pavimentazione" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl789167 width=145 style="height:25pt;border-top:none;
  border-left:none;width:109pt">legno</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 25pt;">
      <asp:CheckBox ID="C_MAT_D1_legno" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 25pt;">&nbsp;</td>

  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 25pt;">&nbsp;</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 25pt;">distacchi di pavimentazione</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 25pt;">
      <asp:TextBox ID="T_ANO_D1_distacchi0pavimentazione" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 25pt;">fissaggio elementi zoccolino</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 25pt;">
      <asp:CheckBox ID="C_INT_D1_fissaggio0zoccolino" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl829167 width=145 style="height:30pt;border-top:none;
  border-left:none;width:109pt">battuto di cemento</td>

  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_D1_battuto0cemento" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 30pt;">&nbsp;</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 30pt;">&nbsp;</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">instabilità pavimentazione</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 30pt;">&nbsp;<asp:TextBox ID="T_ANO_D1_instabilità0pavimentazione" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">sostituzione zoccolino</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_D1_sostituzione0zoccolino" runat="server" Text="." /></td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl819167 width=145 style="height:28pt;border-left:none;
  width:109pt">zoccolino in ceramica/pietra</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_D1_zoccolino0ceramica" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 28pt;">&nbsp;</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">distacchi zoccolino</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 28pt;">&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">rifacimento sottofondo</td>

  <td class=xl799167 style="border-top:none;border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_INT_D1_rifacimento0sottofondo" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl789167 width=145 style="height:16pt;border-top:none;
  border-left:none;width:109pt">zoccolino in legno</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 16pt;">
      <asp:CheckBox ID="C_MAT_D1_zoccolino0legno" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 16pt;">&nbsp;</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 16pt;">&nbsp;</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 16pt;">&nbsp;</td>

  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 16pt;">&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 16pt;">&nbsp;</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 16pt;"></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl849167 width=145 style="height:18pt;border-top:none;
  border-left:none;width:109pt">zoccolini in pvc</td>
  <td class=xl849167 style="border-top:none;border-left:none;
  width:19pt; height: 18pt;">
      <asp:CheckBox ID="C_MAT_D1_zoccolino0pvc" runat="server" Text="." /></td>
  <td class=xl869167 style="border-top:none;border-left:none; height: 18pt;">&nbsp;</td>

  <td class=xl869167 style="border-top:none;border-left:none; width: 20pt; height: 18pt;">&nbsp;</td>
  <td class=xl849167 style="border-top:none;border-left:none;
  width:109pt; height: 18pt;">&nbsp;</td>
  <td class=xl849167 style="border-top:none;border-left:none;
  width:34pt; height: 18pt;">&nbsp;</td>
  <td class=xl849167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 18pt;">&nbsp;</td>
  <td class=xl879167 style="border-top:none;border-left:none; height: 18pt;"></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=9 height=222 class=xl1259167 width=54 style='border-bottom:1.0pt solid black;
  height:166.5pt;border-top:none;width:41pt'>D 1.1</td>

  <td rowspan=9 class=xl1259167 width=145 style='border-bottom:1.0pt solid black;
  border-top:none;width:109pt'>PAVIMENTAZIONI INTERNE PERCORSI CANTINE<br />
      mq<asp:TextBox ID="Text_mq_D2" runat="server" MaxLength="9" Width="106px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_D2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=9 class=xl1259167 width=54 style='border-bottom:1.0pt solid black;
  border-top:none;width:41pt'></td>
  <td class=xl839167 width=145 style="border-top:none;width:109pt; height: 30pt;">Pavimento
  galleggiante</td>
  <td class=xl839167 style="border-top:none;border-left:none;
  width:19pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_D2_pavimento0galleggia" runat="server" Text="." /></td>
  <td class=xl809167 style="border-top:none;border-left:none; height: 30pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl809167 style="border-top:none;border-left:none; width: 20pt; height: 30pt;">&nbsp;<asp:CheckBox ID="C_ANA_D2_nuovo" runat="server" Text="." /></td>

  <td class=xl839167 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">distacchi zoccolino</td>
  <td class=xl839167 style="border-top:none;border-left:none;
  width:34pt; height: 30pt;">&nbsp;<asp:TextBox ID="T_ANO_D2_distacchi0zoccolino" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl839167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">pulizia</td>
  <td class=xl859167 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_D2_pulizia" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl789167 width=145 style="height:34pt;border-top:none;
  width:109pt">ceramica</td>

  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 34pt;">
      <asp:CheckBox ID="C_MAT_D2_ceramica" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 34pt;">2. lieve
  obsolescenza</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 34pt;">&nbsp;<asp:CheckBox ID="C_ANA_D2_lieve0obsolescenza" runat="server" Text="." /></td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">graffiti</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 34pt;">
      <asp:TextBox ID="T_ANO_D2_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">sostituzione parti pavimento</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 34pt;">
      <asp:CheckBox ID="C_INT_D2_sostituzione0pavimento" runat="server" Text="." /></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl679167 width=145 style="height:22pt;border-top:none;
  width:109pt">pietra</td>
  <td class=xl679167 style="border-top:none;border-left:none;
  width:19pt; height: 22pt;">
      <asp:CheckBox ID="C_MAT_D2_pietra" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 22pt;">3. forte
  obsolescenza</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 22pt;">&nbsp;<asp:CheckBox ID="C_ANA_D2_forte" runat="server" Text="." /></td>
  <td class=xl679167 style="border-top:none;border-left:none;
  width:109pt; height: 22pt;">fessurazioni</td>

  <td class=xl679167 style="border-top:none;border-left:none;
  width:34pt; height: 22pt;">
      <asp:TextBox ID="T_ANO_D2_fessurazioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 22pt;">fissaggio elementi</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_INT_D2_fissaggio0elementi" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl789167 width=145 style="height:30pt;border-top:none;
  width:109pt">linoleum/pvc</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_D2_linoleum" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 30pt;">4. totale
  obsolescenza</td>

  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 30pt;">&nbsp;<asp:CheckBox ID="C_ANA_D2_tot" runat="server" Text="." /></td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">rotture</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 30pt;">&nbsp;<asp:TextBox ID="T_ANO_D2_rotture" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">pavimentazione</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_D2_pavimentazione" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl789167 width=145 style="height:28pt;border-top:none;
  width:109pt">legno</td>

  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_D2_legno" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 28pt;">&nbsp;</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 28pt;">&nbsp;</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">distacchi di pavimentazione</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_D2_distacchi0pavimentazione" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">fissaggio elementi zoccolino</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_INT_D2_fissaggio0zoccolino" runat="server" Text="." /></td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl829167 width=145 style='height:25.5pt;border-top:none;
  width:109pt'>battuto di cemento</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt">
      <asp:CheckBox ID="C_MAT_D2_battuto_cemento" runat="server" Text="." /></td>
  <td class=xl669167 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt;">&nbsp;</td>
  <td class=xl789167 style='border-top:none;border-left:none;
  width:109pt'>instabilità pavimentazione</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt">&nbsp;<asp:TextBox ID="T_ANO_D2_instabilità0pavimentazione" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl789167 width=145 style='border-top:none;border-left:none;
  width:109pt'>sostituzione zoccolino</td>

  <td class=xl799167 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_D2_sostituzione0zoccolino" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl819167 width=145 style='height:25.5pt;width:109pt'>zoccolino
  in ceramica/pietra</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt">
      <asp:CheckBox ID="C_MAT_D2_zoccolino_ceramica" runat="server" Text="." />&nbsp;</td>
  <td class=xl669167 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt;">&nbsp;</td>
  <td class=xl789167 style='border-top:none;border-left:none;
  width:109pt'>distacchi zoccolino</td>

  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt">&nbsp;</td>
  <td class=xl789167 width=145 style='border-top:none;border-left:none;
  width:109pt'>rifacimento sottofondo</td>
  <td class=xl799167 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_D2_rifacimento0sottofondo" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl789167 width=145 style="height:13pt;border-top:none;
  width:109pt">zoccolino in legno</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:19pt; height: 13pt;">
      <asp:CheckBox ID="C_MAT_D2_zoccolino0legno" runat="server" Text="." /></td>
  <td class=xl669167 style="border-top:none;border-left:none; height: 13pt;">&nbsp;</td>

  <td class=xl669167 style="border-top:none;border-left:none; width: 20pt; height: 13pt;">&nbsp;</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:109pt; height: 13pt;">&nbsp;</td>
  <td class=xl789167 style="border-top:none;border-left:none;
  width:34pt; height: 13pt;">&nbsp;</td>
  <td class=xl789167 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 13pt;">&nbsp;</td>
  <td class=xl799167 style="border-top:none;border-left:none; height: 13pt;"></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl849167 width=145 style='height:13.5pt;border-top:none;
  width:109pt'>zoccolini in pvc</td>

  <td class=xl849167 style="border-top:none;border-left:none;
  width:19pt">
      <asp:CheckBox ID="C_MAT_D2_zoccolino0pvc" runat="server" Text="." /></td>
  <td class=xl869167 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl869167 style="border-top:none;border-left:none; width: 20pt;">&nbsp;</td>
  <td class=xl849167 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl849167 style="border-top:none;border-left:none;
  width:34pt">&nbsp;</td>
  <td class=xl849167 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl879167 style='border-top:none;border-left:none'>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>

  <td colspan=2 rowspan=2 height=36 class=xl1159167 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black;height:27.0pt'>STATO DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl1199167 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black'>Stato 1</td>
  <td rowspan=2 class=xl899167 style="border-bottom:1.0pt solid black; width: 19pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl929167 style="height: 22pt">Stato 2.1</td>
  <td class=xl889167 style="width: 20pt; vertical-align: top; height: 22pt; text-align: left;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl929167 style="width: 109pt; height: 22pt;">Stato 3.1</td>
  <td class=xl889167 style="width: 34pt; vertical-align: top; height: 22pt; text-align: left;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td rowspan=2 class=xl1239167 style='border-bottom:1.0pt solid black'>Stato
  3.3</td>
  <td class=xl899167 style="border-left:none; height: 22pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl939167 style="height:21pt">Stato 2.2</td>
  <td class=xl909167 style="border-top:none; width: 20pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl939167 style="width: 109pt; height: 21pt;">Stato 3.2</td>

  <td class=xl909167 style="border-top:none; width: 34pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl889167 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl1129167 width=937 style='border-right:1.0pt solid black;
  height:12.75pt;width:707pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl739167 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl749167></td>
  <td class=xl749167 style="width: 19pt"></td>
  <td class=xl749167></td>
  <td class=xl749167 style="width: 20pt"></td>
  <td class=xl749167 style="width: 109pt"></td>
  <td class=xl749167 style="width: 34pt"></td>
  <td class=xl749167></td>

  <td class=xl759167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl159167></td>
  <td class=xl159167></td>
  <td class=xl159167></td>
  <td class=xl159167 style="width: 19pt"></td>
  <td class=xl159167></td>

  <td class=xl159167 style="width: 20pt"></td>
  <td class=xl159167 style="width: 109pt"></td>
  <td class=xl159167 style="width: 34pt"></td>
  <td class=xl159167></td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>

  <td class=xl159167 style="width: 20pt"></td>
  <td class=xl159167 style="width: 109pt"></td>
  <td class=xl159167 style="width: 34pt"></td>
  <td class=xl159167></td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 height=17 class=xl1019167 width=937 style='border-right:1.0pt solid black;
  height:12.75pt;width:707pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola<br>

    <span style='mso-spacerun:yes'> </span>anomalia&gt;= 60%) –</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 colspan=10 style='height:12.75pt'>Per alcuni El.
  Tecnici potrebbe risultare necessario inserire diverse dimensioni in
  relazione alle diverse tipologie costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
  <td class=xl779167>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl159167 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="456px" TextMode="MultiLine" Width="856px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>

  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>

  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>

  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=18 style='page-break-before:always;height:13.5pt'>
  <td height=18 class=xl719167 style='height:13.5pt'>&nbsp;</td>
  <td class=xl659167>&nbsp;</td>
  <td class=xl659167>&nbsp;</td>

  <td class=xl659167>&nbsp;</td>
  <td class=xl659167 style="width: 19pt">&nbsp;</td>
  <td class=xl659167>&nbsp;</td>
  <td class=xl659167 style="width: 20pt">&nbsp;</td>
  <td class=xl659167 style="width: 109pt">&nbsp;</td>
  <td class=xl659167 style="width: 34pt">&nbsp;</td>
  <td class=xl659167>&nbsp;</td>
  <td class=xl729167>&nbsp;</td>
 </tr>

 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl689167 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl699167 style='border-top:none'>&nbsp;</td>
  <td class=xl699167 style='border-top:none'>&nbsp;</td>
  <td class=xl699167 style="border-top:none; width: 19pt;">&nbsp;</td>
  <td class=xl699167 style='border-top:none'>&nbsp;</td>
  <td class=xl699167 style="border-top:none; width: 20pt;">&nbsp;</td>
  <td class=xl699167 style="border-top:none; width: 109pt;">&nbsp;</td>

  <td class=xl699167 style="border-top:none; width: 34pt;">&nbsp;</td>
  <td class=xl699167 style='border-top:none'>&nbsp;</td>
  <td class=xl709167 style='border-top:none'>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 class=xl1049167 width=937
  style="border-right:1.0pt solid black;height:26pt;width:707pt">Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl159167 colspan="9" rowspan="30">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="536px" TextMode="MultiLine" Width="856px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>

  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>

  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>

  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl769167 style='height:12.75pt'>&nbsp;</td>
  <td class=xl779167>&nbsp;</td>
 </tr>

 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td class=xl769167 style="height:1pt">&nbsp;</td>
  <td class=xl779167 style="height: 1pt">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl719167 style="height:21pt">&nbsp;</td>
  <td class=xl659167 style="height: 21pt">&nbsp;</td>
  <td class=xl659167 style="height: 21pt">&nbsp;</td>
  <td class=xl659167 style="height: 21pt">&nbsp;</td>

  <td class=xl659167 style="width: 19pt; height: 21pt;">&nbsp;</td>
  <td class=xl659167 style="height: 21pt">&nbsp;<asp:Button ID="Button1" runat="server" Text="Button" Visible="False" /></td>
  <td class=xl659167 style="width: 20pt; height: 21pt;">&nbsp;</td>
  <td class=xl659167 style="width: 109pt; height: 21pt;">&nbsp;</td>
  <td class=xl659167 style="width: 34pt; height: 21pt;">&nbsp;</td>
  <td class=xl659167 style="height: 21pt">&nbsp;</td>
  <td class=xl729167 style="height: 21pt">&nbsp;</td>
 </tr>


 <tr height=0 style='display:none'>
  <td width=54 style='width:41pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=54 style='width:41pt'></td>
  <td width=145 style='width:109pt'></td>
  <td style="width:19pt"></td>
  <td width=145 style='width:109pt'></td>
  <td style='width:20pt'></td>
  <td style='width:109pt'></td>

  <td style="width:34pt"></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
 </tr>

</table>


    </div>
        <br />
    </form>
</body>
</html>
