<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScC.aspx.vb" Inherits="ScE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">

    <title>Scheda C</title>
    <style id="SCHEDE RILIEVO TUTTE_17929_Styles">
	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1517929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6517929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl6617929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl6717929
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl6817929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6917929
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
.xl7017929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7117929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7217929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7317929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7417929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7517929
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
.xl7617929
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
.xl7717929
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
.xl7817929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7917929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8017929
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
.xl8117929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8217929
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
.xl8317929
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
.xl8417929
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
	border-right:none;
	border-bottom:1.0pt solid windowtext;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl8517929
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
.xl8617929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8717929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl8817929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8917929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9017929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9117929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9217929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9317929
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
.xl9417929
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
.xl9517929
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
.xl9617929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9717929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9817929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9917929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10017929
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
.xl10117929
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
.xl10217929
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
.xl10317929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10417929
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
.xl10517929
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
.xl10617929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10717929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10817929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10917929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11017929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11117929
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
.xl11217929
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
.xl11317929
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
.xl11417929
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
.xl11517929
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
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl11617929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11717929
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
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl11817929
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
	border-right:none;
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11917929
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl12017929
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
	border-right:none;
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl12117929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl12217929
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
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl12317929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl12417929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl12517929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:nowrap;}
.xl12617929
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
.xl12717929
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
.xl12817929
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl12917929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13017929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13117929
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
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl13217929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13317929
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
.xl13417929
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
.xl13517929
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
.xl13617929
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
.xl13717929
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
.xl13817929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13917929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl14017929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl14117929
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
.xl14217929
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
.xl14317929
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
.xl14417929
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
.xl14517929
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
.xl14617929
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
.xl14717929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl14817929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl14917929
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
.xl15017929
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
.xl15117929
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
.xl15217929
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
	border-left:1.0pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl15317929
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
.xl15417929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:italic;
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
.xl15517929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:italic;
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
.xl15617929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:italic;
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
.xl15717929
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
.xl15817929
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
.xl15917929
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
.xl16017929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
	color:windowtext;
	font-size:10.0pt;
	font-weight:400;
	font-style:italic;
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
.xl16117929
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
.xl16217929
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
.xl16317929
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
.xl16417929
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
.xl16517929
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
.xl16617929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl16717929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl16817929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl16917929
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
.xl17017929
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
        <br />
        <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Red"
            Style="text-align: left" Visible="False" Width="652px"></asp:Label><br />
  <table border=0 cellpadding=0 cellspacing=0 width=902 style='border-collapse:
 collapse;table-layout:fixed;width:679pt'>
 <col width=54 style='mso-width-source:userset;mso-width-alt:1974;width:41pt'>
 <col width=131 style='mso-width-source:userset;mso-width-alt:4790;width:98pt'>
 <col width=54 style='mso-width-source:userset;mso-width-alt:1974;width:41pt'>
 <col width=131 style='mso-width-source:userset;mso-width-alt:4790;width:98pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>

 <col width=33 style='mso-width-source:userset;mso-width-alt:1206;width:25pt'>
 <col width=131 style='mso-width-source:userset;mso-width-alt:4790;width:98pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <tr height=34 style='mso-height-source:userset;height:26.1pt'>
  <td colspan=9 height=34 class=xl14117929 width=731 style='height:26.1pt;
  width:550pt'>U. T. PARTIZIONI INTERNI - SCHEDA RILIEVO PARTIZIONI INTERNE</td>
  <td colspan=2 class=xl14317929 width=171 style="border-right:1.0pt solid black;
  width:129pt; text-align: center;">C</td>

 </tr>
 <tr height=53 style='mso-height-source:userset;height:39.95pt'>
  <td height=53 class=xl12617929 width=54 style='height:39.95pt;width:41pt'>Scheda
  C</td>
  <td class=xl12717929 width=131 style='border-left:none;width:98pt'>ELEMENTO
  TECNICO</td>
  <td class=xl12717929 width=54 style='border-left:none;width:41pt'></td>
  <td colspan=2 class=xl14517929 width=157 style='border-right:.5pt solid black;
  border-left:none;width:118pt'>MATERIALI E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td colspan=2 class=xl14517929 width=178 style='border-right:.5pt solid black;
  border-left:none;width:134pt'>ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl14517929 width=157 style='border-right:.5pt solid black;
  border-left:none;width:118pt'>ANOMALIE (%)</td>
  <td colspan=2 class=xl14517929 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=5 height=137 class=xl14917929 width=54 style='border-bottom:1.0pt solid black;
  height:102.75pt;border-top:none;width:41pt'>C 1</td>
  <td rowspan=5 class=xl13517929 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>TAVOLATI<br />
      mq<asp:TextBox ID="Text_mq_C1" runat="server" MaxLength="9" Width="92px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server"
          ControlToValidate="Text_mq_C1" ErrorMessage="!" Font-Bold="True" Height="1px"
          Style="left: 432px; top: 264px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
          Width="1px"></asp:RegularExpressionValidator></td>

  <td class=xl9517929 width=54 style="border-left:none;width:41pt; height: 32pt;"></td>
  <td class=xl11117929 style="border-left:none;width:98pt; height: 32pt;">blocchi di
  laterizio</td>
  <td class=xl9317929 width=26 style="border-left:none;width:20pt; height: 32pt;">
      <asp:CheckBox ID="C_MAT_C1_blocchi0laterizio" runat="server" Text="." />&nbsp;</td>
  <td class=xl9417929 style="border-left:none; height: 32pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl9417929 style="border-left:none; height: 32pt;">&nbsp;<asp:CheckBox ID="C_ANA_C1_nuovo" runat="server" Text="." /></td>
  <td class=xl11117929 width=131 style="border-left:none;width:98pt; height: 32pt;">graffiti</td>

  <td class=xl9317929 style="border-left:none;width:20pt; height: 32pt;">
      <asp:TextBox ID="T_ANO_C1_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11117929 width=145 style="border-left:none;width:109pt; height: 32pt;">puilizia</td>
  <td class=xl9717929 style="border-left:none; height: 32pt;">
      <asp:CheckBox ID="C_INT_C1_pulizia" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7017929 width=54 style="height:32pt;border-left:
  none;width:41pt">&nbsp;<br />
      </td>
  <td class=xl11217929 style="border-top:none;border-left:none;
  width:98pt; height: 32pt;">vetro cemento</td>
  <td class=xl8017929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 32pt;">
      <asp:CheckBox ID="C_MAT_C1_vetro0cemento" runat="server" Text="." />&nbsp;</td>

  <td class=xl6917929 style="border-top:none;border-left:none; height: 32pt;">2. lieve
  obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 32pt;">&nbsp;<asp:CheckBox ID="C_ANA_C1_lieve" runat="server" Text="." /></td>
  <td class=xl11217929 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 32pt;">fessurazioni</td>
  <td class=xl8017929 style="border-top:none;border-left:none;
  width:20pt; height: 32pt;">
      <asp:TextBox ID="T_ANO_C1_fessurazioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 32pt;">ricostruzione<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 32pt;">
      <asp:CheckBox ID="C_INT_C1_ricostruzione" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl7017929 width=54 style="height:28pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl11217929 style="border-top:none;border-left:none;
  width:98pt; height: 28pt;">pannello prefabricato</td>
  <td class=xl7117929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_C1_pannelli0prefab" runat="server" Text="." />&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 28pt;">3. forte
  obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 28pt;">&nbsp;<asp:CheckBox ID="C_ANA_C1_forte" runat="server" Text="." /></td>
  <td class=xl11217929 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 28pt;">distacchi finitura</td>

  <td class=xl7117929 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_C1_distacchi0finitura" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">risanamento conservativo</td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_INT_C1_risanamento0conservativo" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl7017929 width=54 style="height:26pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl11217929 style="border-top:none;border-left:none;
  width:98pt; height: 26pt;">cartongesso</td>
  <td class=xl8017929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:CheckBox ID="C_MAT_C1_cartongesso" runat="server" Text="." />&nbsp;</td>

  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">4. totale obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 26pt;">&nbsp;<asp:CheckBox ID="C_ANA_C1_tot" runat="server" Text="." /></td>
  <td class=xl11117929 width=131 style="border-left:none;width:98pt; height: 26pt;">distacco
  parte portante</td>
  <td class=xl8017929 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_C1_distacco0portante" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11117929 width=145 style="border-left:none;width:109pt; height: 26pt;">rifacimento
  finitura</td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_INT_C1_rifacimento0finitura" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=35 style='height:26.25pt'>
  <td height=35 class=xl8617929 width=54 style='height:26.25pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl8417929 style='width:98pt'>&nbsp;</td>
  <td class=xl10217929 width=26 style='border-top:none;width:20pt'>&nbsp;</td>
  <td class=xl10417929>&nbsp;</td>
  <td class=xl10517929 style='border-top:none'>&nbsp;</td>
  <td class=xl10117929 style='border-left:none'>ritenzione<span
  style='mso-spacerun:yes'>  </span>umidità</td>

  <td class=xl10217929 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_C1_ritenzione0umidita" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11317929 width=145 style='border-left:none;width:109pt'>fissaggio
  elementi finitura</td>
  <td class=xl10617929 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_C1_fissaggio0finitura" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=5 height=136 class=xl14917929 width=54 style='border-bottom:.5pt solid black;
  height:102.0pt;border-top:none;width:41pt'>C2</td>
  <td rowspan=5 class=xl13517929 width=131 style='border-bottom:.5pt solid black;
  border-top:none;width:98pt'>INFISSI INTERNI<br />
      mq<asp:TextBox ID="Text_mq_C2" runat="server" MaxLength="9" Width="92px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_C2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>

  <td class=xl8517929 width=54 style="border-top:none;border-left:none;
  width:41pt; height: 30pt;"></td>
  <td class=xl11417929 style="border-top:none;border-left:none;
  width:98pt; height: 30pt;">telaio in legno</td>
  <td class=xl10717929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_C2_telaio0legno" runat="server" Text="." />&nbsp;</td>
  <td class=xl8217929 style="border-top:none;border-left:none; height: 30pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8217929 style="border-top:none;border-left:none; height: 30pt;">&nbsp;<asp:CheckBox ID="C_ANA_C2_nuovo" runat="server" Text="." /></td>
  <td class=xl9117929 style="border-top:none;border-left:none; height: 30pt;">corrosione
  profili</td>

  <td class=xl10717929 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_C2_corrosione0profili" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11417929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">sostituzione serramenti</td>
  <td class=xl10317929 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_C1_sostituzione0serramenti" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7017929 width=54 style="height:34pt;border-left:
  none;width:41pt">&nbsp;<br />
      </td>
  <td class=xl11217929 style="border-top:none;border-left:none;
  width:98pt; height: 34pt;">telaio in alluminio</td>
  <td class=xl7117929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">
      <asp:CheckBox ID="C_MAT_C2_telaio0alluminio" runat="server" Text="." />&nbsp;</td>

  <td class=xl6917929 style="border-top:none;border-left:none; height: 34pt;">2. lieve
  obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 34pt;">&nbsp;<asp:CheckBox ID="C_ANA_C2_lieve" runat="server" Text="." /></td>
  <td class=xl9217929 style="border-top:none;border-left:none; height: 34pt;">rottura lastra</td>
  <td class=xl7117929 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">
      <asp:TextBox ID="T_ANO_C2_rottura0lastra" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">pulizia</td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 34pt;">
      <asp:CheckBox ID="C_INT_C2_pulizia" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td class=xl7017929 width=54 style="height:33pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl11217929 style="border-top:none;border-left:none;
  width:98pt; height: 33pt;">telaio in ferro /acciaio</td>
  <td class=xl7117929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 33pt;">
      <asp:CheckBox ID="C_MAT_C2_telaio0ferro" runat="server" Text="." />&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 33pt;">3. forte
  obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 33pt;">&nbsp;<asp:CheckBox ID="C_ANA_C2_forte" runat="server" Text="." /></td>
  <td class=xl9217929 style="border-top:none;border-left:none; height: 33pt;">graffiti</td>

  <td class=xl7117929 style="border-top:none;border-left:none;
  width:20pt; height: 33pt;">
      <asp:TextBox ID="T_ANO_C2_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 33pt;">sostituzione lastre</td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 33pt;">
      <asp:CheckBox ID="C_INT_C2_sostituzione0lastre" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl7017929 width=54 style="height:29pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl11217929 style="border-top:none;border-left:none;
  width:98pt; height: 29pt;">telaio in PVC</td>
  <td class=xl7117929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:CheckBox ID="C_MAT_C2_telaio0pvc" runat="server" Text="." />&nbsp;</td>

  <td class=xl6917929 style="border-top:none;border-left:none; height: 29pt;">4. totale
  obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 29pt;">&nbsp;<asp:CheckBox ID="C_ANA_C2_tot" runat="server" Text="." /></td>
  <td class=xl9217929 style="border-top:none;border-left:none; height: 29pt;">degrado
  guarnizioni</td>
  <td class=xl7117929 style="border-top:none;border-left:none;
  width:20pt; height: 29pt;">
      <asp:TextBox ID="T_ANO_C2_degrado0guarnizioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 29pt;">revisione intergale serramento</td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 29pt;">
      <asp:CheckBox ID="C_INT_C2_integrale0serramento" runat="server" Text="." />&nbsp;</td>
 </tr>

 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl9017929 width=54 style='height:25.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl11217929 style='border-top:none;border-left:none;
  width:98pt'>telaio misto</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C2_telaio0misto" runat="server" Text="." />&nbsp;</td>
  <td class=xl11517929 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl6917929 style='border-top:none'>&nbsp;</td>
  <td class=xl11617929 style='border-top:none'>sporco</td>
  <td class=xl7117929 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_C2_sporco" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>

  <td class=xl11217929 width=145 style='border-top:none;border-left:none;
  width:109pt'>revisione semplice serramento</td>
  <td class=xl8117929 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_C2_semplice0serramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=5 height=86 class=xl15317929 width=54 style='border-bottom:1.0pt solid black;
  height:64.5pt;border-top:none;width:41pt'>C 2.1</td>
  <td rowspan=5 class=xl15417929 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>serramento interno luce fissa</td>
  <td class=xl9617929 width=54 style='border-top:none;border-left:none;
  width:41pt'>&nbsp;</td>

  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">
      vetro</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C21_vetro" runat="server" Text="." />&nbsp;</td>
  <td class=xl11717929 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl8317929 style='border-top:none'>&nbsp;</td>
  <td class=xl11617929 style='border-top:none'>distacchi da muratura</td>
  <td class=xl7117929 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_C21_distacchi0muratura" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11817929 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl9917929 style='border-top:none'>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7017929 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">singolo</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C21_singolo" runat="server" Text="." />&nbsp;</td>
  <td class=xl11917929 style='border-left:none'>&nbsp;</td>
  <td class=xl10017929>&nbsp;</td>
  <td class=xl12017929 width=131 style='border-top:none;width:98pt'>&nbsp;</td>

  <td class=xl9617929 style='border-top:none;width:20pt'>&nbsp;</td>
  <td class=xl6717929 width=145 style='width:109pt'></td>
  <td class=xl12117929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7017929 width=54 style="height:13pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 13pt; width: 98pt;">doppio</td>
  <td class=xl7117929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 13pt;">
      <asp:CheckBox ID="C_MAT_C21_doppio" runat="server" Text="." />&nbsp;</td>

  <td class=xl11917929 style="border-left:none; height: 13pt;">&nbsp;</td>
  <td class=xl10017929 style="height: 13pt">&nbsp;</td>
  <td class=xl6717929 width=131 style="width:98pt; height: 13pt;"></td>
  <td class=xl7017929 style="width:20pt; height: 13pt;">&nbsp;</td>
  <td class=xl6717929 width=145 style="width:109pt; height: 13pt;"></td>
  <td class=xl12117929 style="height: 13pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7017929 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>

  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">retinato</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C21_retinato" runat="server" Text="." />&nbsp;</td>
  <td class=xl11917929 style='border-left:none'>&nbsp;</td>
  <td class=xl10017929>&nbsp;</td>
  <td class=xl6717929 width=131 style='width:98pt'></td>
  <td class=xl7017929 style='width:20pt'>&nbsp;</td>
  <td class=xl6717929 width=145 style='width:109pt'></td>
  <td class=xl12117929>&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl8617929 width=54 style='height:13.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl10117929 style="border-left:none; width: 98pt;">rete</td>
  <td class=xl8917929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C21_rete" runat="server" Text="." />&nbsp;</td>
  <td class=xl12217929 style='border-left:none'>&nbsp;</td>
  <td class=xl10117929>&nbsp;</td>
  <td class=xl8417929 width=131 style='width:98pt'>&nbsp;</td>

  <td class=xl8617929 style='width:20pt'>&nbsp;</td>
  <td class=xl8417929 width=145 style='width:109pt'>&nbsp;</td>
  <td class=xl12317929>&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td rowspan=14 height=375 class=xl14917929 width=54 style='border-bottom:
  1.0pt solid black;height:281.25pt;border-top:none;width:41pt'>C 2.2</td>
  <td rowspan=14 class=xl16017929 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>serramenti interni a luce con apertura<br />
      <strong>mq<asp:TextBox ID="Text_mq_C22" runat="server" MaxLength="9" Width="90px"></asp:TextBox>
          <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_C22"
              ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
              ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></strong></td>
  <td class=xl8817929 width=54 style="border-top:none;border-left:none;
  width:41pt; height: 34pt;"></td>

  <td class=xl9117929 style="border-top:none;border-left:none; width: 98pt; height: 34pt;">telaio in legno</td>
  <td class=xl10717929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">
      <asp:CheckBox ID="C_MAT_C3_telaio0legno" runat="server" Text="." />&nbsp;</td>
  <td class=xl8217929 style="border-top:none;border-left:none; height: 34pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8217929 style="border-top:none;border-left:none; height: 34pt;">&nbsp;<asp:CheckBox ID="C_ANA_C3_nuovo" runat="server" Text="." /></td>
  <td class=xl12417929 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 34pt;">corrosione profili</td>
  <td class=xl10717929 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">
      <asp:TextBox ID="T_ANO_C3_corrosione0profili" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11417929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">sostituzione serramenti</td>

  <td class=xl10317929 style="border-top:none;border-left:none; height: 34pt;">
      <asp:CheckBox ID="C_INT_C3_sostituzione0serramenti" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7017929 width=54 style="height:37pt;border-left:
  none;width:41pt">&nbsp;<br />
      </td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt; height: 37pt;">telaio in
  alluminio</td>
  <td class=xl7117929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 37pt;">
      <asp:CheckBox ID="C_MAT_C3_telaio0alluminio" runat="server" Text="." />&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 37pt;">2. lieve
  obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 37pt;">&nbsp;<asp:CheckBox ID="C_ANA_C3_lieve" runat="server" Text="." /></td>

  <td class=xl10817929 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 37pt;">rottura lastra</td>
  <td class=xl7117929 style="border-top:none;border-left:none;
  width:20pt; height: 37pt;">
      <asp:TextBox ID="T_ANO_C3_rottura0lastra" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 37pt;">pulizia</td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 37pt;">
      <asp:CheckBox ID="C_INT_C3_pulizia" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl7017929 width=54 style="height:28pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt; height: 28pt;">telaio in ferro
  /acciaio</td>

  <td class=xl7117929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_C3_telaio0ferro" runat="server" Text="." />&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 28pt;">3. forte
  obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 28pt;">&nbsp;<asp:CheckBox ID="C_ANA_C3_forte" runat="server" Text="." /></td>
  <td class=xl10817929 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 28pt;">graffiti</td>
  <td class=xl7117929 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:TextBox ID="T_ANO_C3_graffiti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">sostituzione lastre</td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_INT_C3_sostituzione0lastre" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl7017929 width=54 style="height:30pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt; height: 30pt;">telaio in PVC</td>
  <td class=xl7117929 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_C3_telaio0pvc" runat="server" Text="." />&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 30pt;">4. totale
  obsolescenza</td>
  <td class=xl6917929 style="border-top:none;border-left:none; height: 30pt;">&nbsp;<asp:CheckBox ID="C_ANA_C3_totale" runat="server" Text="." /></td>
  <td class=xl10817929 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 30pt;">degrado guarnizioni</td>

  <td class=xl7117929 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_C3_degrado0guarnizioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">revisione intergale serramento</td>
  <td class=xl8117929 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_C3_integrale0serramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl7017929 width=54 style='height:25.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">telaio misto</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_telaio0misto" runat="server" Text="." />&nbsp;</td>

  <td class=xl10917929 style='border-left:none'>&nbsp;</td>
  <td class=xl9817929 style='border-top:none;border-left:none'>&nbsp;</td>
  <td class=xl10817929 width=131 style='border-top:none;border-left:none;
  width:98pt'>sporco</td>
  <td class=xl7117929 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_C3_sporco" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style='border-top:none;border-left:none;
  width:109pt'>revisione semplice serramento</td>
  <td class=xl8117929 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_C3_semplice0serramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>

  <td height=34 class=xl7017929 width=54 style='height:25.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">vetro:</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_vetro" runat="server" Text="." />&nbsp;</td>
  <td class=xl12517929></td>
  <td class=xl10917929>&nbsp;</td>
  <td class=xl10817929 width=131 style='border-top:none;border-left:none;
  width:98pt'>distacchi da muratura</td>
  <td class=xl7117929 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_C3_distacchi0muratura" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style='border-top:none;border-left:none;
  width:109pt'>sostituzione sistemi oscuramento</td>

  <td class=xl8117929 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_C3_sostituzione0oscuramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl7017929 width=54 style='height:25.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">singolo</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_singolo" runat="server" Text="." />&nbsp;</td>
  <td class=xl12517929></td>
  <td class=xl10917929>&nbsp;</td>

  <td class=xl10817929 width=131 style='border-top:none;border-left:none;
  width:98pt'>malfunzionamento organi</td>
  <td class=xl7117929 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_C3_malfunzionamento0organi" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style='border-top:none;border-left:none;
  width:109pt'>revisione integrale sistemi oscuramento</td>
  <td class=xl8117929 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_C3_integrale0oscuramento" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=51 style='height:38.25pt'>
  <td height=51 class=xl7017929 width=54 style='height:38.25pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">doppio</td>

  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_doppio" runat="server" Text="." />&nbsp;</td>
  <td class=xl12517929></td>
  <td class=xl10917929>&nbsp;</td>
  <td class=xl10817929 width=131 style='border-top:none;border-left:none;
  width:98pt'>apertura<span style='mso-spacerun:yes'>  </span>/chiusura /
  movimento dei serramenti</td>
  <td class=xl7117929 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_C3_movimenti0serramenti" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl11217929 width=145 style='border-top:none;border-left:none;
  width:109pt'>revisione semplice sistemi oscuramento</td>
  <td class=xl8117929 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_C3_semplice0oscuramento" runat="server" Text="." />&nbsp;</td>

 </tr>
 <tr height=51 style='height:38.25pt'>
  <td height=51 class=xl7017929 width=54 style='height:38.25pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">rete metallica</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_rete0metallica" runat="server" Text="." />&nbsp;</td>
  <td class=xl12517929></td>
  <td class=xl10917929>&nbsp;</td>
  <td class=xl6617929 width=131 style='width:98pt'></td>

  <td class=xl7117929 style='border-top:none;width:20pt'>&nbsp;</td>
  <td class=xl11217929 width=145 style='border-top:none;border-left:none;
  width:109pt'>trattamento anticorrosivo /finitura superficie metallica</td>
  <td class=xl8117929 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_C3_anticorrosivo0metallica" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7017929 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">modalita
  apertura</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_modalita0apertur" runat="server" Text="." />&nbsp;</td>

  <td class=xl12517929></td>
  <td class=xl10917929>&nbsp;</td>
  <td class=xl6617929 width=131 style='width:98pt'></td>
  <td class=xl9617929 style='border-top:none;width:20pt'>&nbsp;</td>
  <td class=xl6617929 width=145 style='width:109pt'></td>
  <td class=xl9917929 style='border-top:none'>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7017929 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>

  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">battente<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_battente" runat="server" Text="." />&nbsp;</td>
  <td class=xl12517929></td>
  <td class=xl10917929>&nbsp;</td>
  <td class=xl6617929 width=131 style='width:98pt'></td>
  <td class=xl7017929 style='width:20pt'>&nbsp;</td>
  <td class=xl6617929 width=145 style='width:109pt'></td>
  <td class=xl12117929>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7017929 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">vasistas</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_vesistas" runat="server" Text="." />&nbsp;</td>
  <td class=xl12517929></td>
  <td class=xl10917929>&nbsp;</td>
  <td class=xl6617929 width=131 style='width:98pt'></td>

  <td class=xl7017929 style='width:20pt'>&nbsp;</td>
  <td class=xl6617929 width=145 style='width:109pt'></td>
  <td class=xl12117929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7017929 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl6917929 style="border-top:none;border-left:none; width: 98pt;">scorrevole</td>
  <td class=xl7117929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_scorrevole" runat="server" Text="." />&nbsp;</td>

  <td class=xl12517929></td>
  <td class=xl10917929>&nbsp;</td>
  <td class=xl6617929 width=131 style='width:98pt'></td>
  <td class=xl7017929 style='width:20pt'>&nbsp;</td>
  <td class=xl6617929 width=145 style='width:109pt'></td>
  <td class=xl12117929>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl8617929 width=54 style='height:13.5pt;border-left:none;
  width:41pt'>&nbsp;</td>

  <td class=xl10517929 style="border-top:none;border-left:none; width: 98pt;">REI</td>
  <td class=xl8917929 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_C3_REI" runat="server" Text="." />&nbsp;</td>
  <td class=xl8717929>&nbsp;</td>
  <td class=xl11017929>&nbsp;</td>
  <td class=xl6517929 width=131 style='width:98pt'>&nbsp;</td>
  <td class=xl8617929 style='width:20pt'>&nbsp;</td>
  <td class=xl6517929 width=145 style='width:109pt'>&nbsp;</td>
  <td class=xl12317929>&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td colspan=2 rowspan=2 height=36 class=xl16117929 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black;height:27.0pt'>STATO DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl16517929 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black'>Stato 1</td>
  <td rowspan=2 class=xl13017929 style="border-bottom:1.0pt solid black; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl12817929 style="border-top:none;border-left:none; height: 21pt;">Stato 2.1</td>
  <td class=xl12917929 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td class=xl12817929 style="border-top:none;border-left:none; height: 21pt;">Stato 3.1</td>
  <td class=xl12917929 style="border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl16917929 style='border-bottom:1.0pt solid black'>Stato
  3.3</td>
  <td class=xl13017929 style="border-left:none; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl13117929 style="height:23pt;border-top:none;
  border-left:none">Stato 2.2</td>

  <td class=xl13217929 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 23pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl13117929 style="border-top:none;border-left:none; height: 23pt;">Stato 3.2</td>
  <td class=xl13217929 style="border-top:none;border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 23pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl12917929 style="border-left:none; vertical-align: top; text-align: left; height: 23pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl15717929 width=902 style='border-right:1.0pt solid black;
  height:12.75pt;width:679pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7517929 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7617929 style="width: 98pt"></td>
  <td class=xl7617929></td>
  <td class=xl7617929></td>
  <td class=xl7617929></td>
  <td class=xl7617929></td>

  <td class=xl7617929 style="width: 20pt"></td>
  <td class=xl7617929></td>
  <td class=xl7717929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1517929></td>
  <td class=xl1517929></td>
  <td class=xl1517929 style="width: 98pt"></td>

  <td class=xl1517929></td>
  <td class=xl1517929></td>
  <td class=xl1517929></td>
  <td class=xl1517929></td>
  <td class=xl1517929 style="width: 20pt"></td>
  <td class=xl1517929></td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7817929 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1517929></td>
  <td class=xl1517929></td>
  <td class=xl1517929 style="width: 20pt"></td>
  <td class=xl1517929></td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=33 style='mso-height-source:userset;height:24.75pt'>

  <td colspan=11 height=33 class=xl13717929 width=902 style='border-right:1.0pt solid black;
  height:24.75pt;width:679pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola<br>
    <span style='mso-spacerun:yes'> </span>anomalia&gt;= 60%) –</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 height=17 class=xl7817929 style='border-right:1.0pt solid black;
  height:12.75pt'>Per alcuni El. Tecnici potrebbe risultare necessario inserire
  diverse dimensioni in relazione alle diverse tipologie costruttive che sono
  state catalogate.<span style='mso-spacerun:yes'>  </span></td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 colspan=10 style='height:12.75pt'>Per gli
  IMPIANTI ELEVATORI riportare: .- numero impianti per edificio; - per ciascun
  impianto univocamente identificato misure vani porta e cabina</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 colspan=10 style='height:12.75pt'>Per i
  MONTASCALE riportare: - numero impianti per edificio; - per ciascun impianto
  univocamente identificato ubicazione e piani serviti</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1517929 colspan="9" rowspan="22">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="384px" TextMode="MultiLine" Width="816px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=18 style='page-break-before:always;height:13.5pt'>
  <td class=xl7317929 style="height:14pt">&nbsp;</td>

  <td class=xl6817929 style="height: 14pt">&nbsp;</td>
  <td class=xl6817929 style="height: 14pt">&nbsp;</td>
  <td class=xl6817929 style="height: 14pt; width: 98pt;">&nbsp;</td>
  <td class=xl6817929 style="height: 14pt">&nbsp;</td>
  <td class=xl6817929 style="height: 14pt">&nbsp;</td>
  <td class=xl6817929 style="height: 14pt">&nbsp;</td>
  <td class=xl6817929 style="height: 14pt">&nbsp;</td>
  <td class=xl6817929 style="height: 14pt; width: 20pt;">&nbsp;</td>
  <td class=xl6817929 style="height: 14pt">&nbsp;</td>

  <td class=xl7417929 style="height: 14pt">&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>NOTE E <span
  style='display:none'>COMMENTI:</span></td>
  <td class=xl7217929 style='border-top:none'>&nbsp;</td>
  <td class=xl7217929 style='border-top:none'>&nbsp;</td>
  <td class=xl7217929 style="border-top:none; width: 98pt;">&nbsp;</td>
  <td class=xl7217929 style='border-top:none'>&nbsp;</td>

  <td class=xl7217929 style='border-top:none'>&nbsp;</td>
  <td class=xl7217929 style='border-top:none'>&nbsp;</td>
  <td class=xl7217929 style='border-top:none'>&nbsp;</td>
  <td class=xl7217929 style="border-top:none; width: 20pt;">&nbsp;</td>
  <td class=xl7217929 style='border-top:none'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl14017929 width=902
  style='border-right:1.0pt solid black;height:25.5pt;width:679pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1517929 colspan="9" rowspan="27">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="480px" TextMode="MultiLine" Width="816px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7817929 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7917929>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl7817929 style="height:13pt">&nbsp;</td>
  <td class=xl7917929 style="height: 13pt">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl7317929 style="height:21pt">&nbsp;</td>
  <td class=xl6817929 style="height: 21pt">
      &nbsp;</td>
  <td class=xl6817929 style="height: 21pt">&nbsp;</td>
  <td class=xl6817929 style="width: 98pt; height: 21pt">&nbsp;</td>
  <td class=xl6817929 style="height: 21pt">&nbsp;</td>

  <td class=xl6817929 style="height: 21pt">
      <asp:Button ID="Crea" runat="server" Text="Crea" Visible="False" />&nbsp;</td>
  <td class=xl6817929 style="height: 21pt">&nbsp;</td>
  <td class=xl6817929 style="height: 21pt">&nbsp;</td>
  <td class=xl6817929 style="width: 20pt; height: 21pt;">&nbsp;</td>
  <td class=xl6817929 style="height: 21pt">
      &nbsp;</td>
  <td class=xl7417929 style="height: 21pt">&nbsp;</td>
 </tr>

 <tr height=0 style='display:none'>

  <td width=54 style='width:41pt'></td>
  <td width=131 style='width:98pt'></td>
  <td width=54 style='width:41pt'></td>
  <td style='width:98pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=33 style='width:25pt'></td>
  <td width=131 style='width:98pt'></td>
  <td style='width:20pt'></td>

  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
 </tr>
 
</table>

  
    </div>
    </form>
</body>
</html>
