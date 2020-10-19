<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScQ.aspx.vb" Inherits="ScQ" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">
     <title>Scheda Q</title>
<style id="SCHEDE RILIEVO TUTTE_4998_Styles">

	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl154998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl654998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl664998
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
.xl674998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl684998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl694998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl704998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl714998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl724998
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
.xl734998
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
.xl744998
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
.xl754998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl764998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl774998
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
.xl784998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl794998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl804998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl814998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl824998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl834998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl844998
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
.xl854998
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
.xl864998
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
	white-space:normal;}
.xl874998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl884998
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
.xl894998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl904998
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
.xl914998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl924998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl934998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl944998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl954998
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
.xl964998
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
.xl974998
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
.xl984998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl994998
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
	border-left:1.0pt solid windowtext;
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:normal;}
.xl1004998
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
.xl1014998
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
.xl1024998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl1034998
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
.xl1044998
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
.xl1054998
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
.xl1064998
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
.xl1074998
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
.xl1084998
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
.xl1094998
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
.xl1104998
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
.xl1114998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl1124998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl1134998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl1144998
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
.xl1154998
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
.xl1164998
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
.xl1174998
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
.xl1184998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl1194998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl1204998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl1214998
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
.xl1224998
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
.xl1234998
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
.xl1244998
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
.xl1254998
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
.xl1264998
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
	border-left:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1274998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	background:#F2F2F2;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1284998
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
.xl1294998
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
.xl1304998
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl1314998
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl1324998
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
	border-bottom:.5pt solid windowtext;
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
    <table border=0 cellpadding=0 cellspacing=0 width=993 style='border-collapse:
 collapse;table-layout:fixed;width:748pt'>
 <col width=54 style='mso-width-source:userset;mso-width-alt:1974;width:41pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=75 style='mso-width-source:userset;mso-width-alt:2742;width:56pt'>
 <col width=180 style='mso-width-source:userset;mso-width-alt:6582;width:135pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>

 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <tr height=34 style='mso-height-source:userset;height:26.1pt'>
  <td colspan=9 height=34 class=xl1254998 width=822 style='height:26.1pt;
  width:619pt'>U. T. IMPIANTI ELETTRICI - SCHEDA RILIEVO IMPIANTI ELETTRICI</td>
  <td colspan=2 class=xl1314998 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>Q</td>

 </tr>
 <tr height=53 style='mso-height-source:userset;height:39.95pt'>
  <td class=xl994998 width=54 style="height:38pt;border-top:none;
  width:41pt">Sc. Q</td>
  <td class=xl964998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 38pt;">ELEMENTO TECNICO</td>
  <td class=xl964998 style="border-top:none;border-left:none;
  width:43pt; height: 38pt;"></td>
  <td colspan=2 class=xl964998 width=206 style="border-left:none;width:155pt; height: 38pt;">MATERIALI
  E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td colspan=2 class=xl1004998 width=171 style="border-right:.5pt solid black;
  border-left:none;width:129pt; height: 38pt;">ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl964998 width=171 style="border-left:none;width:129pt; height: 38pt;">ANOMALIE
  (%)</td>
  <td colspan=2 class=xl964998 width=171 style="border-right:1.0pt solid black;
  border-left:none;width:129pt; height: 38pt;">INTERVENTI</td>
 </tr>
 <tr height=51 style='height:38.25pt'>
  <td rowspan=4 height=103 class=xl1244998 width=54 style='border-bottom:1.0pt solid black;
  height:77.25pt;border-top:none;width:41pt'>Q 1</td>
  <td rowspan=4 class=xl1034998 width=145 style='border-bottom:1.0pt solid black;
  border-top:none;width:109pt'>ALIMENTAZIONE<br />
      cad<asp:TextBox ID="Text_mq_Q1" runat="server" MaxLength="9" Width="104px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_Q1"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>

  <td rowspan=4 class=xl1064998 style="border-bottom:1.0pt solid black;
  border-top:none;width:43pt">
  
      </td>
  <td class=xl774998 width=180 style='border-top:none;border-left:none;
  width:135pt'>contatore</td>
  <td class=xl774998 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_Q1_contatore" runat="server" Text="." />&nbsp;</td>
  <td class=xl664998 style='border-top:none;border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl664998 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_Q1_nuovo" runat="server" Text="." /></td>
  <td class=xl774998 width=145 style='border-top:none;border-left:none;
  width:109pt'>inadeguatezza posizione contatore elettrico</td>

  <td class=xl774998 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_Q1_inadeguatezza0contatore0elettrico" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl774998 width=145 style='border-top:none;border-left:none;
  width:109pt'>spostamento contatore</td>
  <td class=xl784998 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_Q1_spostamento0contatore" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl774998 width=180 style="height:23pt;border-top:none;
  border-left:none;width:135pt">&nbsp;</td>
  <td class=xl774998 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">&nbsp;</td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 23pt;">2. lieve
  obsolescenza</td>

  <td class=xl664998 style="border-top:none;border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_ANA_Q1_lieve" runat="server" Text="." /></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">&nbsp;</td>
  <td class=xl774998 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">&nbsp;</td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">&nbsp;</td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 23pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl674998 width=180 style="height:23pt;border-top:none;
  border-left:none;width:135pt">&nbsp;</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">&nbsp;</td>

  <td class=xl664998 style="border-top:none;border-left:none; height: 23pt;">3. forte
  obsolescenza</td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 23pt;">
      <asp:CheckBox ID="C_ANA_Q1_forte" runat="server" Text="." /></td>
  <td class=xl674998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">&nbsp;</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 23pt;">&nbsp;</td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 23pt;">&nbsp;</td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 23pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>

  <td class=xl884998 width=180 style="height:17pt;border-top:none;
  border-left:none;width:135pt">&nbsp;</td>
  <td class=xl884998 style="border-top:none;border-left:none;
  width:20pt; height: 17pt;">&nbsp;</td>
  <td class=xl904998 style="border-top:none;border-left:none; height: 17pt;">4. totale
  obsolescenza</td>
  <td class=xl904998 style="border-top:none;border-left:none; height: 17pt;">
      <asp:CheckBox ID="C_ANA_Q1_tot" runat="server" Text="." /></td>
  <td class=xl884998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 17pt;">&nbsp;</td>
  <td class=xl884998 style="border-top:none;border-left:none;
  width:20pt; height: 17pt;">&nbsp;</td>
  <td class=xl884998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 17pt;">&nbsp;</td>
  <td class=xl914998 style="border-top:none;border-left:none; height: 17pt;">&nbsp;</td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=5 height=137 class=xl1214998 width=54 style='border-bottom:1.0pt solid black;
  height:102.75pt;border-top:none;width:41pt'>Q2</td>
  <td rowspan=5 class=xl1094998 width=145 style="border-bottom:1.0pt solid black;
  border-top:none;width:109pt; vertical-align: top; text-align: center;">Q.E.<br />
      <table style="height: 141px" width="100%">
          <tr>
              <td style="height: 34px">
                  cad<asp:TextBox ID="Text_mq_Q21" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_Q21"
                      ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
                      ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td style="height: 37px">
                  cad<asp:TextBox ID="Text_mq_Q22" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Text_mq_Q22"
                      ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
                      ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td style="height: 28px">
                  cad<asp:TextBox ID="Text_mq_Q23" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="Text_mq_Q23"
                      ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
                      ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td style="height: 27px">
                  cad<asp:TextBox ID="Text_mq_Q24" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="Text_mq_Q24"
                      ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
                      ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
          </tr>
      </table>
  </td>
  <td rowspan=5 class=xl1104998 style="border-bottom:1.0pt solid black;
  border-top:none;width:43pt; vertical-align: top; text-align: left;">
      <table width="100%" style="height: 87px">

      </table>
  </td>
  
  <td class=xl844998 width=180 style="border-left:none;width:135pt; height: 41pt;">Q.E.
  generale unità comuni<span style='mso-spacerun:yes'> </span></td>

  <td class=xl834998 style="border-left:none;width:20pt; height: 41pt;">
      <asp:CheckBox ID="C_MAT_Q2_generale" runat="server" Text="." /></td>
  <td class=xl854998 style="border-left:none; height: 41pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl854998 style="border-left:none; height: 41pt;">
      <asp:CheckBox ID="C_ANA_Q2_nuovo" runat="server" Text="." /></td>
  <td class=xl844998 width=145 style="border-left:none;width:109pt; height: 41pt;">Q.E. non
  protetto/accessibile<span style='mso-spacerun:yes'> </span></td>
  <td class=xl834998 style="border-left:none;width:20pt; height: 41pt;">
      <asp:TextBox ID="T_ANO_Q2_non0protetto" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl844998 width=145 style="border-left:none;width:109pt; height: 41pt;">Spostamento
  Q.E.<span style='mso-spacerun:yes'> </span></td>

  <td class=xl874998 style="border-left:none; height: 41pt;">
      <asp:CheckBox ID="C_INT_Q2_spostamento0qe" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl774998 width=180 style="height:36pt;border-top:none;
  border-left:none;width:135pt">Q.E. CT</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 36pt;">
      <asp:CheckBox ID="C_MAT_Q2_CT" runat="server" Text="." /></td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 36pt;">2. lieve
  obsolescenza</td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 36pt;">
      <asp:CheckBox ID="C_ANA_Q2_lieve" runat="server" Text="." /></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 36pt;">inadeguatezza posizione Q.E. elettrico</td>

  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 36pt;">
      <asp:TextBox ID="T_ANO_Q2_inadeguatezza0posizione" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 36pt;">protezione Q.E.<span style='mso-spacerun:yes'> </span></td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 36pt;">
      <asp:CheckBox ID="C_INT_Q2_protezione0que" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl774998 width=180 style="height:25pt;border-top:none;
  border-left:none;width:135pt">Q.E. ascensori<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 25pt;">
      <asp:CheckBox ID="C_MAT_Q2_ascensori" runat="server" Text="." /></td>

  <td class=xl664998 style="border-top:none;border-left:none; height: 25pt;">3. forte
  obsolescenza</td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 25pt;">
      <asp:CheckBox ID="C_ANA_Q2_forte" runat="server" Text="." /></td>
  <td class=xl674998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 25pt;">Mancanza schemi elttrici sul posto</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 25pt;">
      <asp:TextBox ID="T_ANO_Q2_mancanza0schemi" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 25pt;">Aggiornamento documentale<span style='mso-spacerun:yes'> </span></td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 25pt;">
      <asp:CheckBox ID="C_INT_Q2_aggiornamento0documentale" runat="server" Text="." /></td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl674998 width=180 style="height:22pt;border-top:none;
  border-left:none;width:135pt">Q.E. autoclave<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 22pt;">
      <asp:CheckBox ID="C_MAT_Q2_autoclave" runat="server" Text="." /></td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 22pt;">4. totale
  obsolescenza</td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_ANA_Q2_tot" runat="server" Text="." /></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 22pt;">&nbsp;</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 22pt;">&nbsp;</td>

  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 22pt;">&nbsp;</td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 22pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl824998 width=180 style="height:14pt;border-top:none;
  border-left:none;width:135pt">&nbsp;</td>
  <td class=xl824998 style="border-top:none;border-left:none;
  width:20pt; height: 14pt;">&nbsp;</td>
  <td class=xl904998 style="border-top:none;border-left:none; height: 14pt;">&nbsp;</td>
  <td class=xl904998 style="border-top:none;border-left:none; height: 14pt;">&nbsp;</td>
  <td class=xl884998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 14pt;">&nbsp;</td>

  <td class=xl824998 style="border-top:none;border-left:none;
  width:20pt; height: 14pt;">&nbsp;</td>
  <td class=xl884998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 14pt;">&nbsp;</td>
  <td class=xl914998 style="border-top:none;border-left:none; height: 14pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=4 height=86 class=xl1214998 width=54 style='border-bottom:1.0pt solid black;
  height:64.5pt;border-top:none;width:41pt'>Q 3</td>
  <td rowspan=4 class=xl1094998 width=145 style='border-bottom:1.0pt solid black;
  border-top:none;width:109pt'>Linee distribuzione<span
  style='mso-spacerun:yes'> 
      <br />
      cad<asp:TextBox ID="Text_mq_Q3" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="Text_mq_Q3"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></span></td>

  <td rowspan=4 class=xl1114998 style="border-bottom:1.0pt solid black;
  border-top:none;width:43pt"></td>
  <td class=xl834998 width=180 style="border-left:none;width:135pt; height: 22pt;">
      cavi
  elettrici</td>
  <td class=xl834998 style="border-left:none;width:20pt; height: 22pt;">
      <asp:CheckBox ID="C_MAT_Q3_cavi0elettrici" runat="server" Text="." /></td>
  <td class=xl854998 style="border-left:none; height: 22pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl854998 style="border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_ANA_Q3_nuovo" runat="server" Text="." /></td>
  <td class=xl834998 width=145 style="border-left:none;width:109pt; height: 22pt;">cavi a
  vista<span style='mso-spacerun:yes'> </span></td>

  <td class=xl834998 style="border-left:none;width:20pt; height: 22pt;">
      <asp:TextBox ID="T_ANO_Q3_cavi0vista" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl834998 width=145 style="border-left:none;width:109pt; height: 22pt;">chiusura
  scatolette</td>
  <td class=xl874998 style="border-left:none; height: 22pt;">
      <asp:CheckBox ID="C_INT_Q3_chiusura0scatolette" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl674998 width=180 style="height:32pt;border-top:none;
  border-left:none;width:135pt">scatolette di derivazione<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 32pt;">
      <asp:CheckBox ID="C_MAT_Q3_scatolette0derivazione" runat="server" Text="." /></td>

  <td class=xl664998 style="border-top:none;border-left:none; height: 32pt;">2. lieve
  obsolescenza</td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 32pt;">
      <asp:CheckBox ID="C_ANA_Q3_lieve" runat="server" Text="." /></td>
  <td class=xl944998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">allacciamenti abusivi</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 32pt;">
      <asp:TextBox ID="T_ANO_Q3_allacciamenti0abusivi" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">ripristino regolarità impianto<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 32pt;">
      <asp:CheckBox ID="C_INT_Q3_ripristino0regolarità" runat="server" Text="." /></td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl674998 width=180 style="height:24pt;border-top:none;
  border-left:none;width:135pt">canalizzazioni/tubazioni<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">
      <asp:CheckBox ID="C_MAT_Q3_canalizzazioni0tubazioni" runat="server" Text="." /></td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 24pt;">3. forte
  obsolescenza</td>
  <td class=xl664998 style="border-top:none;border-left:none; height: 24pt;">
      <asp:CheckBox ID="C_ANA_Q3_forte" runat="server" Text="." /></td>
  <td class=xl674998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 24pt;">&nbsp;</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">&nbsp;</td>

  <td class=xl674998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 24pt;">ripristino canalizzazioni<span style='mso-spacerun:yes'> </span></td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 24pt;">
      <asp:CheckBox ID="C_INT_Q3_ripristino0canalizzazioni" runat="server" Text="." /></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl824998 width=180 style="height:19pt;border-top:none;
  border-left:none;width:135pt">&nbsp;</td>
  <td class=xl824998 style="border-top:none;border-left:none;
  width:20pt; height: 19pt;">&nbsp;</td>
  <td class=xl904998 style="border-top:none;border-left:none; height: 19pt;">4. totale
  obsolescenza</td>

  <td class=xl904998 style="border-top:none;border-left:none; height: 19pt;">
      <asp:CheckBox ID="C_ANA_Q3_tot" runat="server" Text="." /></td>
  <td class=xl824998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 19pt;">&nbsp;</td>
  <td class=xl824998 style="border-top:none;border-left:none;
  width:20pt; height: 19pt;">&nbsp;</td>
  <td class=xl884998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 19pt;">&nbsp;</td>
  <td class=xl914998 style="border-top:none;border-left:none; height: 19pt;">&nbsp;</td>
 </tr>
 <tr height=51 style='page-break-before:always;height:38.25pt'>
  <td rowspan=4 height=120 class=xl1214998 width=54 style='border-bottom:1.0pt solid black;
  height:90.0pt;border-top:none;width:41pt'>Q 4</td>

  <td rowspan=4 class=xl1094998 width=145 style='border-bottom:1.0pt solid black;
  border-top:none;width:109pt'>messa a terra/protezione da scariche
  elettriche<br />
      <span style='mso-spacerun:yes'> cad<asp:TextBox ID="Text_mq_Q4" runat="server" MaxLength="9" Width="96px"></asp:TextBox> 
          <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="Text_mq_Q4"
              ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
              ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator>
      </span></td>
  <td rowspan=4 class=xl1114998 style="border-bottom:1.0pt solid black;
  border-top:none;width:43pt"></td>
  <td class=xl924998 width=180 style='border-top:none;border-left:none;
  width:135pt'>dispersori</td>
  <td class=xl924998 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_Q4_dispersori" runat="server" Text="." /></td>
  <td class=xl794998 width=145 style='border-top:none;border-left:none;
  width:109pt'>1. nuovo</td>
  <td class=xl794998 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_ANA_Q4_nuovo" runat="server" Text="." /></td>

  <td class=xl924998 width=145 style='border-top:none;border-left:none;
  width:109pt'>mancata individuazione<span style='mso-spacerun:yes'> 
  </span>posizionamento dispersori</td>
  <td class=xl924998 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_Q4_mancata0posizionamento0dispoersori" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl924998 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl894998 style='border-top:none;border-left:none'>&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl674998 width=180 style="height:31pt;border-top:none;
  border-left:none;width:135pt">rete di faraday</td>

  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:CheckBox ID="C_MAT_Q4_rete0faraday" runat="server" Text="." /></td>
  <td class=xl804998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">2. lieve obsolescenza</td>
  <td class=xl804998 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:CheckBox ID="C_ANA_Q4_lieve" runat="server" Text="." /></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">manomissione rete di faraday</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:TextBox ID="T_ANO_Q4_manomissione0faraday" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl674998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">&nbsp;</td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 31pt;">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td class=xl674998 width=180 style="height:21pt;border-top:none;
  border-left:none;width:135pt">&nbsp;</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">&nbsp;</td>
  <td class=xl804998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">3. forte obsolescenza</td>
  <td class=xl804998 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">
      <asp:CheckBox ID="C_ANA_Q4_forte" runat="server" Text="." /></td>
  <td class=xl674998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">&nbsp;</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">&nbsp;</td>
  <td class=xl674998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">&nbsp;</td>

  <td class=xl784998 style="border-top:none;border-left:none; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl824998 width=180 style="height:18pt;border-top:none;
  border-left:none;width:135pt">&nbsp;</td>
  <td class=xl824998 style="border-top:none;border-left:none;
  width:20pt; height: 18pt;">&nbsp;</td>
  <td class=xl814998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 18pt;">4. totale obsolescenza</td>
  <td class=xl814998 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 18pt;">
      <asp:CheckBox ID="C_ANA_Q4_tot" runat="server" Text="." /></td>
  <td class=xl824998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 18pt;">&nbsp;</td>

  <td class=xl824998 style="border-top:none;border-left:none;
  width:20pt; height: 18pt;">&nbsp;</td>
  <td class=xl824998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 18pt;">&nbsp;</td>
  <td class=xl914998 style="border-top:none;border-left:none; height: 18pt;">&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=6 height=239 class=xl1224998 width=54 style='border-bottom:1.0pt solid black;
  height:179.25pt;width:41pt'>Q 5</td>
  <td rowspan=6 class=xl1044998 width=145 style="border-bottom:1.0pt solid black;
  width:109pt; vertical-align: top; text-align: center;">terminali<br />
      <table style="height: 141px" width="100%">
          <tr>
              <td style="height: 34px">
                  cad<asp:TextBox ID="Text_mq_Q51" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="Text_mq_Q51"
                      ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
                      ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td style="height: 29px">
                  cad<asp:TextBox ID="Text_mq_Q52" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="Text_mq_Q52"
                      ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
                      ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td style="height: 43px">
                  cad<asp:TextBox ID="Text_mq_Q53" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                      ControlToValidate="Text_mq_Q53" ErrorMessage="!" Font-Bold="True" Height="1px"
                      Style="left: 432px; top: 264px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                      Width="1px"></asp:RegularExpressionValidator></td>
          </tr>
          <tr>
              <td style="height: 74px">
                  cad<asp:TextBox ID="Text_mq_Q54" runat="server" MaxLength="9" Width="96px"></asp:TextBox>
                  <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server"
                      ControlToValidate="Text_mq_Q54" ErrorMessage="!" Font-Bold="True" Height="1px"
                      Style="left: 432px; top: 264px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                      Width="1px"></asp:RegularExpressionValidator></td>
          </tr>
      </table>
  </td>

  <td rowspan=6 class=xl1074998 style="border-bottom:1.0pt solid black;
  width:43pt; vertical-align: text-top; text-align: left;">
  </td>
  <td class=xl844998 width=180 style="border-left:none;width:135pt; height: 42pt;">apparecchi
  di illuminazione prese<span style='mso-spacerun:yes'> </span></td>
  <td class=xl834998 style="border-left:none;width:20pt; height: 42pt;">
      <asp:CheckBox ID="C_MAT_Q4_illuminazione0prese" runat="server" Text="." /></td>
  <td class=xl864998 width=145 style="border-left:none;width:109pt; height: 42pt;">1. nuovo</td>
  <td class=xl934998 width=26 style="border-left:none;width:20pt; height: 42pt;">
      <asp:CheckBox ID="C_ANA_Q5_nuovo" runat="server" Text="." /></td>
  <td class=xl844998 width=145 style="border-left:none;width:109pt; height: 42pt;">non
  funzionamento luci scale<span style='mso-spacerun:yes'> </span></td>

  <td class=xl834998 style="border-left:none;width:20pt; height: 42pt;">
      <asp:TextBox ID="T_ANO_Q5_non0luci0scale" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl844998 width=145 style="border-left:none;width:109pt; height: 42pt;">revisione
  impianto</td>
  <td class=xl874998 style="border-left:none; height: 42pt;">
      <asp:CheckBox ID="C_INT_Q5_revisione0impianto" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl774998 width=180 style='height:25.5pt;border-top:none;
  border-left:none;width:135pt'>interruttori</td>
  <td class=xl674998 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_Q4_interrutori" runat="server" Text="." /></td>
  <td class=xl804998 width=145 style='border-top:none;border-left:none;
  width:109pt'>2. lieve obsolescenza</td>

  <td class=xl804998 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_ANA_Q5_lieve" runat="server" Text="." /></td>
  <td class=xl774998 width=145 style='border-top:none;border-left:none;
  width:109pt'>non funzionamento luci esterne<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl674998 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_Q5_non0luci0esterne" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl774998 width=145 style='border-top:none;border-left:none;
  width:109pt'>sostituzione lamapadine<span style='mso-spacerun:yes'> </span></td>
  <td class=xl784998 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_Q5_sostituz0lampadine" runat="server" Text="." /></td>
 </tr>
 <tr height=51 style='height:38.25pt'>

  <td class=xl774998 width=180 style="height:41pt;border-top:none;
  border-left:none;width:135pt">crepuscolari<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 41pt;">
      <asp:CheckBox ID="C_MAT_Q4_crepuscolari" runat="server" Text="." /></td>
  <td class=xl804998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 41pt;">3. forte obsolescenza</td>
  <td class=xl804998 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 41pt;">
      <asp:CheckBox ID="C_ANA_Q5_forte" runat="server" Text="." /></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 41pt;">non funzionamento crepuscolare<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 41pt;">
      <asp:TextBox ID="T_ANO_Q5_non0crepuscolare" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 41pt;">sostituzione apparecchi illuminazione<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 41pt;">
      <asp:CheckBox ID="C_INT_Q5_sostituz0illuminaz" runat="server" Text="." /></td>
 </tr>
 <tr height=51 style='height:38.25pt'>
  <td class=xl674998 width=180 style="height:46pt;border-top:none;
  border-left:none;width:135pt">&nbsp;luci</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 46pt;"><asp:CheckBox ID="C_MAT_Q5_luci" runat="server" Text="." />&nbsp;</td>
  <td class=xl804998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 46pt;">4. totale obsolescenza</td>

  <td class=xl804998 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 46pt;">
      <asp:CheckBox ID="C_ANA_Q5_tot" runat="server" Text="." /></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 46pt;">rottura/mancanza di apparecchi di illuminazione<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 46pt;">
      <asp:TextBox ID="T_ANO_Q5_rottura0illuminazione" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 46pt;">sostituzione frutti</td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 46pt;">
      <asp:CheckBox ID="C_INT_Q5_sostituz0frutti" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>

  <td class=xl674998 width=180 style="height:26pt;border-top:none;
  border-left:none;width:135pt">&nbsp;</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">&nbsp;</td>
  <td class=xl804998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">&nbsp;</td>
  <td class=xl804998 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">&nbsp;</td>
  <td class=xl774998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">non funzionamento interruttori</td>
  <td class=xl674998 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_Q5_non0interruttori" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl674998 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">&nbsp;</td>
  <td class=xl784998 style="border-top:none;border-left:none; height: 26pt;">&nbsp;</td>

 </tr>
 <tr height=35 style='height:26.25pt'>
  <td height=35 class=xl824998 width=180 style='height:26.25pt;border-top:none;
  border-left:none;width:135pt'>&nbsp;</td>
  <td class=xl824998 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td class=xl814998 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl814998 width=26 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td class=xl884998 width=145 style='border-top:none;border-left:none;
  width:109pt'>rottura/mancanza di prese/interruttori</td>
  <td class=xl824998 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_Q5_mancanza0interruttori" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl824998 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl914998 style='border-top:none;border-left:none'>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=2 rowspan=2 height=34 class=xl1284998 style='height:25.5pt'>STATO
  DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl1294998>Stato 1</td>
  <td rowspan=2 class=xl984998 style="width: 20pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl974998 style="border-left:none; height: 21pt;">Stato 2.1</td>

  <td class=xl984998 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl974998 style="border-left:none; height: 21pt;">Stato 3.1</td>
  <td class=xl984998 style="border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl1294998>Stato 3.3</td>
  <td class=xl984998 style="border-left:none; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl974998 style="height:21pt;border-top:none;
  border-left:none">Stato 2.2</td>

  <td class=xl984998 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl974998 style="border-top:none;border-left:none; height: 21pt;">Stato 3.2</td>
  <td class=xl984998 style="border-top:none;border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl984998 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl1144998 width=993 style='border-right:1.0pt solid black;
  height:12.75pt;width:748pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl724998 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl734998></td>
  <td class=xl734998 style="width: 20pt"></td>
  <td class=xl734998></td>
  <td class=xl734998></td>
  <td class=xl734998></td>

  <td class=xl734998 style="width: 20pt"></td>
  <td class=xl734998></td>
  <td class=xl744998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl154998></td>
  <td class=xl154998 style="width: 43pt"></td>
  <td class=xl154998></td>

  <td class=xl154998 style="width: 20pt"></td>
  <td class=xl154998></td>
  <td class=xl154998></td>
  <td class=xl154998></td>
  <td class=xl154998 style="width: 20pt"></td>
  <td class=xl154998></td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl754998 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl154998></td>
  <td class=xl154998></td>
  <td class=xl154998 style="width: 20pt"></td>
  <td class=xl154998></td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td colspan=11 height=17 class=xl1174998 width=993 style='border-right:1.0pt solid black;
  height:12.75pt;width:748pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola<br>
    <span style='mso-spacerun:yes'> </span>anomalia&gt;= 60%) –</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 colspan=10 style='height:12.75pt'>Per alcuni El.
  Tecnici potrebbe risultare necessario inserire diverse dimensioni in
  relazione alle diverse tipologie costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
  <td class=xl764998>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl154998 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="456px" TextMode="MultiLine" Width="912px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>

  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>

  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>

  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl704998 style='height:13.5pt'>&nbsp;</td>
  <td class=xl654998>&nbsp;</td>

  <td class=xl654998 style="width: 43pt">&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl654998 style="width: 20pt">&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl654998 style="width: 20pt">&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl714998>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl954998 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl684998 style="border-top:none; width: 43pt;">&nbsp;</td>
  <td class=xl684998 style='border-top:none'>&nbsp;</td>
  <td class=xl684998 style="border-top:none; width: 20pt;">&nbsp;</td>
  <td class=xl684998 style='border-top:none'>&nbsp;</td>
  <td class=xl684998 style='border-top:none'>&nbsp;</td>

  <td class=xl684998 style='border-top:none'>&nbsp;</td>
  <td class=xl684998 style="border-top:none; width: 20pt;">&nbsp;</td>
  <td class=xl684998 style='border-top:none'>&nbsp;</td>
  <td class=xl694998 style='border-top:none'>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl1204998 width=993
  style='border-right:1.0pt solid black;height:25.5pt;width:748pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl154998 colspan="9" rowspan="30">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="528px" TextMode="MultiLine" Width="912px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>

  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>

  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl754998 style="height:13pt">&nbsp;</td>
  <td class=xl764998 style="height: 13pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>

  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl754998 style='height:12.75pt'>&nbsp;</td>
  <td class=xl764998>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl704998 style='height:13.5pt'>&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl654998 style="width: 43pt">&nbsp;</td>

  <td class=xl654998>&nbsp;</td>
  <td class=xl654998 style="width: 20pt">&nbsp;</td>
  <td class=xl654998>
      <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl654998 style="width: 20pt">&nbsp;</td>
  <td class=xl654998>&nbsp;</td>
  <td class=xl714998>&nbsp;</td>
 </tr>


 <tr height=0 style='display:none'>
  <td width=54 style='width:41pt'></td>
  <td width=145 style='width:109pt'></td>
  <td style="width:43pt"></td>
  <td width=180 style='width:135pt'></td>
  <td style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>

  <td width=145 style='width:109pt'></td>
  <td style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
 </tr>

</table>

    </div>
    </form>
</body>
</html>
