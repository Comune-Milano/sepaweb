<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScN.aspx.vb" Inherits="ScN" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv=Content-Type content="text/html; charset=windows-1252">
<meta name=ProgId content=Excel.Sheet>
<meta name=Generator content="Microsoft Excel 12">
   <title>Scheda N</title>
<style id="SCHEDE RILIEVO TUTTE_22762_Styles">
	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1522762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6522762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6622762
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
.xl6722762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6822762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6922762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7022762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7122762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7222762
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
.xl7322762
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
.xl7422762
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
.xl7522762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7622762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7722762
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
.xl7822762
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
.xl7922762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8022762
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
.xl8122762
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
.xl8222762
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
.xl8322762
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
.xl8422762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8522762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8622762
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
.xl8722762
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
.xl8822762
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
.xl8922762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9022762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9122762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9222762
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
.xl9322762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9422762
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
.xl9522762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9622762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9722762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9822762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9922762
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
.xl10022762
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
.xl10122762
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
.xl10222762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10322762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10422762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10522762
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
.xl10622762
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
.xl10722762
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
.xl10822762
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
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl10922762
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
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11022762
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
	border-bottom:none;
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11122762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	white-space:normal;}
.xl11222762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	white-space:normal;}
.xl11322762
	{padding-top:1px;
	padding-right:1px;

	padding-left:1px;
	mso-ignore:padding;
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
	border-bottom:1.0pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl11422762
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
.xl11522762
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
.xl11622762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11722762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11822762
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
.xl11922762
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
.xl12022762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl12122762
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
.xl12222762
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
.xl12322762
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
.xl12422762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl12522762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl12622762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl12722762
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
.xl12822762
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
.xl12922762
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
.xl13022762
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
.xl13122762
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
.xl13222762
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
.xl13322762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13422762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13522762
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
.xl13622762
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
.xl13722762
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
.xl13822762
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
.xl13922762
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
.xl14022762
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
.xl14122762
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
.xl14222762
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
.xl14322762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl14422762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl14522762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl14622762
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
.xl14722762
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
.xl14822762
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
.xl14922762
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl15022762
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
 <tr height=34 style='mso-height-source:userset;height:26.1pt'>
  <td colspan=9 height=34 class=xl12722762 width=738 style='height:26.1pt;
  width:556pt'>U. T. RETE SCARICO / FOGNARIA -<span style='mso-spacerun:yes'> 
  </span>SCHEDA RILIEVO RETE SCARICO / FOGNARIA</td>

  <td colspan=2 class=xl12922762 width=171 style='border-right:1.0pt solid black;
  width:129pt'>N</td>
 </tr>
 <tr height=53 style='mso-height-source:userset;height:39.95pt'>
  <td height=53 class=xl11422762 width=54 style='height:39.95pt;width:41pt'>Scheda
  N</td>
  <td class=xl11522762 width=131 style='border-left:none;width:98pt'>ELEMENTO
  TECNICO</td>
  <td class=xl11522762 width=54 style='border-left:none;width:41pt'></td>
  <td colspan=2 class=xl13122762 width=157 style='border-right:.5pt solid black;
  border-left:none;width:118pt'>MATERIALI E TIPOLOGIE COSTRUTTIVE (x)<br>

    (solo il prevalente)</td>
  <td colspan=2 class=xl13122762 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl13122762 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANOMALIE (%)</td>
  <td colspan=2 class=xl13122762 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=4 height=120 class=xl13522762 width=54 style='border-bottom:1.0pt solid black;
  height:90.0pt;border-top:none;width:41pt'>N 1</td>

  <td rowspan=4 class=xl12322762 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>COLONNE DI SERVIZIO E DIRAMAZIONI SUB-ORIZZONTALI<br />
      ml<asp:TextBox ID="Text_mq_N1" runat="server" MaxLength="9" Width="96px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_N1"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8822762 width=54 style='border-left:none;width:41pt'></td>
  <td class=xl10122762 width=131 style='border-top:none;border-left:none;
  width:98pt'>accessibili</td>
  <td class=xl8622762 width=26 style='border-left:none;width:20pt'>
      <asp:CheckBox ID="C_MAT_N1_accessibili" runat="server" Text="." />&nbsp;</td>
  <td class=xl8122762>1. nuovo<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8722762>
      <asp:CheckBox ID="C_ANA_N1_nuovo" runat="server" Text="." /></td>

  <td class=xl10122762 width=145 style='border-top:none;border-left:none;
  width:109pt'>perdite generalizzate da colonna</td>
  <td class=xl8622762 style='border-left:none;width:20pt'>
      <asp:TextBox ID="T_ANO_N1_perdite0colonna" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10122762 width=145 style='border-top:none;border-left:none;
  width:109pt'>rifacimento colonne</td>
  <td class=xl9022762 style='border-left:none'>
      <asp:CheckBox ID="C_INT_N1_rifacimento0colonne" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6722762 width=54 style="height:36pt;border-left:none;
  width:41pt">
      </td>
  <td class=xl9922762 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 36pt;">non accessibili</td>

  <td class=xl7822762 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 36pt;">
      <asp:CheckBox ID="C_MAT_N1_non0accessibili" runat="server" Text="." />&nbsp;</td>
  <td class=xl8722762 style="border-left:none; height: 36pt;">2. lieve obsolescenza</td>
  <td class=xl6622762 style="border-top:none;border-left:none; height: 36pt;">
      <asp:CheckBox ID="C_ANA_N1_lieve" runat="server" Text="." /></td>
  <td class=xl9922762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 36pt;">sospetta presenza di amianto</td>
  <td class=xl7822762 style="border-top:none;border-left:none;
  width:20pt; height: 36pt;">
      <asp:TextBox ID="T_ANO_N1_sospetta0amianto" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9922762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 36pt;">analisi amianto</td>
  <td class=xl7922762 style="border-top:none;border-left:none; height: 36pt;">
      <asp:CheckBox ID="C_INT_N1_analisi0amianto" runat="server" Text="." /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6722762 width=54 style="height:26pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl9922762 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 26pt;">ghisa</td>
  <td class=xl6822762 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:CheckBox ID="C_MAT_N1_ghisa" runat="server" Text="." />&nbsp;</td>
  <td class=xl6622762 style="border-top:none;border-left:none; height: 26pt;">3. forte
  obsolescenza</td>
  <td class=xl6622762 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_ANA_N1_forte" runat="server" Text="." /></td>
  <td class=xl9922762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">perdite generalizzate da diramazioni</td>

  <td class=xl6822762 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_N1_perdite0diramazioni" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9922762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">rifacimento diramazioni</td>
  <td class=xl7922762 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_INT_N1_rifacimento0diramazioni" runat="server" Text="." /></td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl6722762 width=54 style="height:17pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl10622762 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 17pt;">pvc</td>
  <td class=xl7722762 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 17pt;">
      <asp:CheckBox ID="C_MAT_N1_pvc" runat="server" Text="." /></td>

  <td class=xl10622762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 17pt;">4. totale obsolescenza</td>
  <td class=xl8222762 style="border-top:none;border-left:none; height: 17pt;">
      <asp:CheckBox ID="C_ANA_N1_tot" runat="server" Text="." /></td>
  <td class=xl10922762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 17pt;">&nbsp;</td>
  <td class=xl9222762 style="border-top:none;width:20pt; height: 17pt;">&nbsp;</td>
  <td class=xl11022762 width=145 style="border-top:none;width:109pt; height: 17pt;">&nbsp;</td>
  <td class=xl9522762 style="border-top:none; height: 17pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td rowspan=4 height=69 class=xl13522762 width=54 style='border-bottom:1.0pt solid black;
  height:51.75pt;border-top:none;width:41pt'>N 2</td>
  <td rowspan=4 class=xl12322762 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>COLLETTORI<br />
      ml<asp:TextBox ID="Text_mq_N2" runat="server" MaxLength="7" Width="96px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_N2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8322762 width=54 style='border-left:none;width:41pt'></td>
  <td class=xl10122762 width=131 style='border-left:none;width:98pt'>accessibili</td>
  <td class=xl9622762 width=26 style='border-left:none;width:20pt'>
      <asp:CheckBox ID="C_MAT_N2_accessibili" runat="server" Text="." />&nbsp;</td>
  <td class=xl8022762 style='border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl8022762 style='border-left:none'>
      <asp:CheckBox ID="C_ANA_N2_nuovo" runat="server" Text="." /></td>
  <td class=xl10422762 width=145 style='border-left:none;width:109pt'>perdite
  generalizzate</td>
  <td class=xl9622762 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_N2_perdite0generalizzate" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10122762 width=145 style='border-left:none;width:109pt'>sostituzioni
  tubazioni</td>
  <td class=xl9322762 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_INT_N1_sostituzioni0tubazioni" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6722762 width=54 style="height:34pt;border-left:
  none;width:41pt">
      &nbsp;</td>

  <td class=xl9922762 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 34pt;">non accessibili</td>
  <td class=xl6822762 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">
      <asp:CheckBox ID="C_MAT_N2_non0accessibili" runat="server" Text="." />&nbsp;</td>
  <td class=xl6622762 style="border-top:none;border-left:none; height: 34pt;">2. lieve
  obsolescenza</td>
  <td class=xl6622762 style="border-top:none;border-left:none; height: 34pt;">
      <asp:CheckBox ID="C_ANA_N2_lieve" runat="server" Text="." /></td>
  <td class=xl11122762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">&nbsp;</td>
  <td class=xl8922762 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">&nbsp;</td>
  <td class=xl10622762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">&nbsp;</td>
  <td class=xl9122762 style="border-top:none;border-left:none; height: 34pt;">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl6722762 width=54 style='height:12.75pt;border-left:
  none;width:41pt'>&nbsp;</td>
  <td class=xl10522762 width=131 style='width:98pt'>ghisa</td>
  <td class=xl6822762 width=26 style='border-top:none;width:20pt'>
      <asp:CheckBox ID="C_MAT_N2_ghisa" runat="server" Text="." />&nbsp;</td>
  <td class=xl6622762 style='border-top:none;border-left:none'>3. forte
  obsolescenza</td>
  <td class=xl6622762 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_N2_forte" runat="server" Text="." /></td>
  <td class=xl9822762 width=145 style='border-left:none;width:109pt'>&nbsp;</td>

  <td class=xl6722762 style='border-left:none;width:20pt'>&nbsp;</td>
  <td class=xl10822762 width=145 style='border-left:none;width:109pt'>&nbsp;</td>
  <td class=xl10222762 style='border-left:none'>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl8422762 width=54 style='height:13.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl10722762 width=131 style='width:98pt'>pvc</td>
  <td class=xl8522762 width=26 style='border-top:none;width:20pt'>
      <asp:CheckBox ID="C_MAT_N2_pvc" runat="server" Text="." />&nbsp;</td>

  <td class=xl9422762 style='border-top:none;border-left:none'>4. totale
  obsolescenza</td>
  <td class=xl9422762 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_N2_tot" runat="server" Text="." /></td>
  <td class=xl11222762 width=145 style='border-left:none;width:109pt'>&nbsp;</td>
  <td class=xl8422762 style='border-left:none;width:20pt'>&nbsp;</td>
  <td class=xl10022762 width=145 style='border-left:none;width:109pt'>&nbsp;</td>
  <td class=xl10322762 style='border-left:none'>&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>

  <td rowspan=4 height=86 class=xl13522762 width=54 style='border-bottom:1.0pt solid black;
  height:64.5pt;border-top:none;width:41pt'>N 3</td>
  <td rowspan=4 class=xl12322762 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>TERMINALI<br />
      cad<asp:TextBox ID="Text_mq_N3" runat="server" MaxLength="9" Width="90px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Text_mq_N3"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8322762 width=54 style="border-top:none;border-left:none;
  width:41pt; height: 26pt;"></td>
  <td class=xl10122762 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 26pt;">ispezioni</td>
  <td class=xl9622762 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:CheckBox ID="C_MAT_N3_ispezioni" runat="server" Text="." />&nbsp;</td>
  <td class=xl8022762 style="border-top:none;border-left:none; height: 26pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl8022762 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_ANA_N3_nuovo" runat="server" Text="." /></td>
  <td class=xl10422762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">mancanza ispezione al piede della colonna<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl9622762 style="border-top:none;border-left:none;
  width:20pt; height: 26pt;">
      <asp:TextBox ID="T_ANO_N3_mancanza0ispezione0colonna" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl10122762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 26pt;">adeguamento ret</td>
  <td class=xl9322762 style="border-top:none;border-left:none; height: 26pt;">
      <asp:CheckBox ID="C_INT_N2_adeguamento0ret" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td class=xl6722762 width=54 style="height:31pt;border-left:
  none;width:41pt">
      </td>
  <td class=xl9922762 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 31pt;">vasca</td>
  <td class=xl6822762 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:CheckBox ID="C_MAT_N3_vasca" runat="server" Text="." />&nbsp;</td>
  <td class=xl6622762 style="border-top:none;border-left:none; height: 31pt;">2. lieve
  obsolescenza</td>
  <td class=xl6622762 style="border-top:none;border-left:none; height: 31pt;">
      <asp:CheckBox ID="C_ANA_N3_lieve" runat="server" Text="." /></td>
  <td class=xl9722762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">rigurgiti da vasche</td>
  <td class=xl6822762 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:TextBox ID="T_ANO_N3_rigurgiti0vasche" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>

  <td class=xl9922762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">spurgo</td>
  <td class=xl7922762 style="border-top:none;border-left:none; height: 31pt;">
      <asp:CheckBox ID="C_INT_N2_spurgo" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6722762 width=54 style="height:21pt;border-left:
  none;width:41pt">&nbsp;</td>
  <td class=xl9922762 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 21pt;">sifone</td>
  <td class=xl6822762 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">
      <asp:CheckBox ID="C_MAT_N3_" runat="server" Text="." /></td>
  <td class=xl6622762 style="border-top:none;border-left:none; height: 21pt;">3. forte
  obsolescenza</td>

  <td class=xl6622762 style="border-top:none;border-left:none; height: 21pt;">
      <asp:CheckBox ID="C_ANA_N3_forte" runat="server" Text="." /></td>
  <td class=xl9722762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">sifone non accessibile</td>
  <td class=xl6822762 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">
      <asp:TextBox ID="T_ANO_N3_sifone0non0accessibile" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9922762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">&nbsp;</td>
  <td class=xl7922762 style="border-top:none;border-left:none; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl8422762 width=54 style="height:20pt;border-left:none;
  width:41pt">&nbsp;</td>

  <td class=xl10722762 width=131 style="width:98pt; height: 20pt;">&nbsp;</td>
  <td class=xl8522762 width=26 style="border-top:none;width:20pt; height: 20pt;">&nbsp;</td>
  <td class=xl9422762 style="border-top:none;border-left:none; height: 20pt;">4. totale
  obsolescenza</td>
  <td class=xl9422762 style="border-top:none;border-left:none; height: 20pt;">
      <asp:CheckBox ID="C_ANA_N3_tot" runat="server" Text="." /></td>
  <td class=xl11322762 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 20pt;">&nbsp;</td>
  <td class=xl8522762 style="border-top:none;width:20pt; height: 20pt;">&nbsp;</td>
  <td class=xl10722762 width=145 style="width:109pt; height: 20pt;">&nbsp;</td>
  <td class=xl9522762 style="border-top:none; height: 20pt;">&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td colspan=2 rowspan=2 height=36 class=xl14622762 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black;height:27.0pt'>STATO DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl14822762 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black'>Stato 1</td>
  <td rowspan=2 class=xl12022762 style="border-bottom:1.0pt solid black; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl11822762 style="border-top:none;border-left:none; height: 21pt;">Stato 2.1</td>
  <td class=xl11722762 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td class=xl11822762 style="border-top:none;border-left:none; height: 21pt;">Stato 3.1</td>
  <td class=xl11722762 style="border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl15022762 style='border-bottom:1.0pt solid black'>Stato
  3.3</td>
  <td class=xl12022762 style="border-left:none; height: 21pt;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl11922762 style="height:21pt;border-top:none;
  border-left:none">Stato 2.2</td>

  <td class=xl11622762 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl11922762 style="border-top:none;border-left:none; height: 21pt;">Stato 3.2</td>
  <td class=xl11622762 style="border-top:none;border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl11722762 style="border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 height=17 class=xl13822762 width=909 style='border-right:1.0pt solid black;
  height:12.75pt;width:685pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7222762 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7322762></td>
  <td class=xl7322762></td>
  <td class=xl7322762></td>
  <td class=xl7322762></td>
  <td class=xl7322762></td>

  <td class=xl7322762 style="width: 20pt"></td>
  <td class=xl7322762></td>
  <td class=xl7422762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7222762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7322762></td>
  <td class=xl7322762></td>
  <td class=xl7322762></td>

  <td class=xl7322762></td>
  <td class=xl7322762></td>
  <td class=xl7322762></td>
  <td class=xl7322762></td>
  <td class=xl7322762 style="width: 20pt"></td>
  <td class=xl7322762></td>
  <td class=xl7422762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7522762 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762 style="width: 20pt"></td>
  <td class=xl1522762></td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7522762 colspan=10 style='height:12.75pt'>In
  particolare inserire gli elementi tecnici riportati, dimensioni generali (
  U.M.) ubicazioni di anomalie più significative ( percentuale per singola
  anomalia<span style='mso-spacerun:yes'> </span></td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&#8805; 60%)</td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>

  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762 style="width: 20pt"></td>
  <td class=xl1522762></td>
  <td class=xl7622762>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 colspan=10 style='height:12.75pt'>Per alcuni
  El. Tecnici potrebbe risultare necessario inserire diverse dimensioni in
  relazione alle diverse tipologie costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>

  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762 style="width: 20pt"></td>
  <td class=xl1522762></td>
  <td class=xl7622762>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1522762 colspan="9" rowspan="19">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="336px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7022762 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6522762>&nbsp;</td>
  <td class=xl6522762>&nbsp;</td>
  <td class=xl6522762>&nbsp;</td>

  <td class=xl6522762>&nbsp;</td>
  <td class=xl6522762>&nbsp;</td>
  <td class=xl6522762>&nbsp;</td>
  <td class=xl6522762>&nbsp;</td>
  <td class=xl6522762 style="width: 20pt">&nbsp;</td>
  <td class=xl6522762>&nbsp;</td>
  <td class=xl7122762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7522762 style='height:12.75pt'> <span
  style='display:none'></span></td>
  <td class=xl6922762 style='border-top:none'>&nbsp;</td>
  <td class=xl6922762 style='border-top:none'>&nbsp;</td>
  <td class=xl6922762 style='border-top:none'>&nbsp;</td>
  <td class=xl6922762 style='border-top:none'>&nbsp;</td>
  <td class=xl6922762 style='border-top:none'>&nbsp;</td>
  <td class=xl6922762 style='border-top:none'>&nbsp;</td>
  <td class=xl6922762 style='border-top:none'>&nbsp;</td>

  <td class=xl6922762 style="border-top:none; width: 20pt;">&nbsp;</td>
  <td class=xl6922762 style='border-top:none'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl12622762 width=909
  style='border-right:1.0pt solid black;height:25.5pt;width:685pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>
  <td class=xl1522762></td>

  <td class=xl1522762></td>
  <td class=xl1522762 style="width: 20pt"></td>
  <td class=xl1522762></td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1522762 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="456px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7622762>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7522762 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7622762>&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl7022762 style="height:14pt">&nbsp;</td>
  <td class=xl6522762 style="height: 14pt">&nbsp;</td>
  <td class=xl6522762 style="height: 14pt">&nbsp;</td>
  <td class=xl6522762 style="height: 14pt">&nbsp;</td>
  <td class=xl6522762 style="height: 14pt">&nbsp;</td>
  <td class=xl6522762 style="height: 14pt">&nbsp;<asp:Button ID="Button1" runat="server" Text="Button" Visible="False" /></td>
  <td class=xl6522762 style="height: 14pt">&nbsp;</td>

  <td class=xl6522762 style="height: 14pt">&nbsp;</td>
  <td class=xl6522762 style="width: 20pt; height: 14pt;">&nbsp;</td>
  <td class=xl6522762 style="height: 14pt">&nbsp;</td>
  <td class=xl7122762 style="height: 14pt">&nbsp;</td>
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
  <td style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>

 </tr>

</table>

    </div>
    </form>
</body>
</html>
