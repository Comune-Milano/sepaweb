<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScR.aspx.vb" Inherits="ScR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv=Content-Type content="text/html; charset=windows-1252">
<meta name=ProgId content=Excel.Sheet>
<meta name=Generator content="Microsoft Excel 12">
    <title>Scheda R</title>
<style id="SCHEDE RILIEVO TUTTE_26832_Styles">

	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1526832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6526832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6626832
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
.xl6726832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6826832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6926832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7026832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7126832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7226832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7326832
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
.xl7426832
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
.xl7526832
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
.xl7626832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7726832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7826832
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
.xl7926832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8026832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8126832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8226832
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
.xl8326832
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
.xl8426832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8526832
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
.xl8626832
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
.xl8726832
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
.xl8826832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8926832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9026832
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
.xl9126832
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
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9226832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9326832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9426832
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
	border-bottom:none;
	border-left:.5pt solid windowtext;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9526832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	border-left:none;
	mso-background-source:auto;
	mso-pattern:auto;
	white-space:normal;}
.xl9626832
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
.xl9726832
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
.xl9826832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9926832
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
.xl10026832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10126832
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
.xl10226832
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
.xl10326832
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
.xl10426832
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
.xl10526832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10626832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10726832
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
.xl10826832
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
.xl10926832
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
.xl11026832
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
.xl11126832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11226832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11326832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11426832
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
.xl11526832
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
.xl11626832
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11726832
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
	vertical-align:bottom;
	border-top:.5pt solid windowtext;
	border-right:none;
	border-bottom:.5pt solid windowtext;
	border-left:.5pt solid windowtext;
	background:#D8D8D8;
	mso-pattern:black none;
	white-space:nowrap;}
.xl11826832
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
	vertical-align:bottom;
	border-top:.5pt solid windowtext;
	border-right:.5pt solid windowtext;
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
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                            Style="left: 120px; top: 24px" ToolTip="SALVA" />&nbsp;</td>
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
    <table border=0 cellpadding=0 cellspacing=0 width=857 style='border-collapse:
 collapse;table-layout:fixed;width:645pt'>
 <col width=75 style='mso-width-source:userset;mso-width-alt:2742;width:56pt'>
 <col width=83 style='mso-width-source:userset;mso-width-alt:3035;width:62pt'>
 <col width=51 style='mso-width-source:userset;mso-width-alt:1865;width:38pt'>
 <col width=113 style='mso-width-source:userset;mso-width-alt:4132;width:85pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=141 style='mso-width-source:userset;mso-width-alt:5156;width:106pt'>

 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <col width=145 style='mso-width-source:userset;mso-width-alt:5302;width:109pt'>
 <col width=26 style='mso-width-source:userset;mso-width-alt:950;width:20pt'>
 <tr height=34 style='mso-height-source:userset;height:26.1pt'>
  <td colspan=9 height=34 class=xl9926832 width=686 style='height:26.1pt;
  width:516pt'>U. T. IMPIANTI TELEVISIVI - SCHEDA RILIEVO IMPIANTI TELEVISIVI</td>
  <td colspan=2 class=xl11726832 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>R</td>

 </tr>
 <tr height=70 style='mso-height-source:userset;height:52.5pt'>
  <td height=70 class=xl9626832 width=75 style='height:52.5pt;border-top:none;
  width:56pt'>Scheda R</td>
  <td class=xl9626832 style="border-top:none;border-left:none;
  width:69pt">ELEMENTO TECNICO</td>
  <td class=xl9626832 style='border-top:none;border-left:none;
  width:38pt'></td>
  <td colspan=2 class=xl9626832 width=139 style='border-left:none;width:105pt'>MATERIALI
  E TIPOLOGIE COSTRUTTIVE (x)<br>
    (solo il prevalente)</td>

  <td class=xl9626832 width=141 style='border-top:none;border-left:none;
  width:106pt'>ANALISI PRESTAZIONALE (X)</td>
  <td class=xl9626832 width=26 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td colspan=2 class=xl9626832 width=171 style='border-left:none;width:129pt'>ANOMALIE
  (%)</td>
  <td colspan=2 class=xl9626832 width=171 style='border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=4 height=120 class=xl10126832 width=75 style='border-bottom:1.0pt solid black;
  height:90.0pt;border-top:none;width:56pt'>R 1</td>

  <td rowspan=4 class=xl10126832 style="border-bottom:1.0pt solid black;
  border-top:none;width:69pt">ANTENNA<br />
      cad<br />
      <asp:TextBox ID="Text_mq_R1" runat="server" MaxLength="9" Width="64px"></asp:TextBox>
      <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_R1"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=4 class=xl10226832 style='border-bottom:1.0pt solid black;
  border-top:none;width:38pt'></td>
  <td class=xl7826832 width=113 style='border-top:none;border-left:none;
  width:85pt'>centralizzata</td>
  <td class=xl7826832 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_R1_centralizzata" runat="server" Text="." /></td>
  <td class=xl6626832 style='border-top:none;border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl6626832 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_R1_nuovo" runat="server" Text="." /></td>

  <td class=xl7826832 width=145 style='border-top:none;border-left:none;
  width:109pt'>instabilità</td>
  <td class=xl7826832 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_R1_instabilità" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl7826832 width=145 style='border-top:none;border-left:none;
  width:109pt'>sostituzione antenna</td>
  <td class=xl8126832 style="border-top:none;border-left:none; width: 20pt;">
      <asp:CheckBox ID="C_INT_R1_sostituzione0antenna" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl7826832 width=113 style="height:30pt;border-top:none;
  border-left:none;width:85pt">singola</td>

  <td class=xl7826832 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_R1_singola" runat="server" Text="." /></td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 30pt;">2. lieve
  obsolescenza</td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_ANA_R1_lieve0obsolescenza" runat="server" Text="." /></td>
  <td class=xl7826832 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">disuso</td>
  <td class=xl7826832 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_R1_disuso" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl7826832 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">sostituzione sistemi di ancoraggio</td>
  <td class=xl8126832 style="border-top:none;border-left:none; width: 20pt; height: 30pt;">
      <asp:CheckBox ID="C_INT_R1_sostituzione0ancoraggio" runat="server" Text="." /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6726832 width=113 style="height:28pt;border-top:none;
  border-left:none;width:85pt">satellitare singola</td>
  <td class=xl6726832 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">
      <asp:CheckBox ID="C_MAT_R1_satellitare" runat="server" Text="." /></td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 28pt;">3. forte
  obsolescenza</td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 28pt;">
      <asp:CheckBox ID="C_ANA_R1_forte" runat="server" Text="." /></td>
  <td class=xl8426832 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">&nbsp;</td>
  <td class=xl6726832 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 28pt;">&nbsp;</td>

  <td class=xl7826832 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 28pt;">rimozione parti in disuso</td>
  <td class=xl8126832 style="border-top:none;border-left:none; width: 20pt; height: 28pt;">
      <asp:CheckBox ID="C_INT_R1_rimozione0disuso" runat="server" Text="." /></td>
 </tr>
 <tr height=35 style='height:26.25pt'>
  <td height=35 class=xl7926832 width=113 style='height:26.25pt;border-top:
  none;border-left:none;width:85pt'>satellitare centralizzata</td>
  <td class=xl8526832 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_R1_satellitare0centralizzata" runat="server" Text="." /></td>
  <td class=xl8726832 style='border-top:none;border-left:none'>4. totale
  obsolescenza</td>

  <td class=xl8726832 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_R1_tot" runat="server" Text="." /></td>
  <td class=xl8626832 width=145 style='border-left:none;width:109pt'>&nbsp;</td>
  <td class=xl8526832 width=26 style='border-top:none;border-left:none;
  width:20pt'>&nbsp;</td>
  <td class=xl8526832 width=145 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl8826832 style="border-top:none;border-left:none; width: 20pt;"></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td rowspan=4 height=119 class=xl10326832 width=75 style='border-bottom:.5pt solid black;
  height:89.25pt;border-top:none;width:56pt'>R2</td>

  <td rowspan=4 class=xl10326832 style="border-bottom:.5pt solid black;
  border-top:none;width:69pt">TERMINALI<br />
      cad<asp:TextBox ID="Text_mq_R2" runat="server" MaxLength="9" Width="68px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_R2"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td rowspan=4 class=xl10426832 style='border-bottom:.5pt solid black;
  border-top:none;width:38pt'></td>
  <td class=xl8226832 width=113 style='border-left:none;width:85pt'>centralino
  Tv</td>
  <td class=xl8026832 width=26 style='border-left:none;width:20pt'>
      <asp:CheckBox ID="C_MAT_R2_centralino0tv" runat="server" Text="." /></td>
  <td class=xl8326832 style='border-left:none'>1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8326832 style='border-left:none'>
      <asp:CheckBox ID="C_ANA_R2_nuovo" runat="server" Text="." /></td>

  <td class=xl8226832 width=145 style='border-left:none;width:109pt'>Accesso al
  centralino difficoltoso</td>
  <td class=xl8026832 width=26 style='border-left:none;width:20pt'>
      <asp:TextBox ID="T_ANO_R2_centralino0difficoltoso" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl8226832 width=145 style='border-left:none;width:109pt'>sostitzione
  centralino<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8926832 style="border-left:none; width: 20pt;">
      <asp:CheckBox ID="C_INT_R2_sostituzione0centralino" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl9026832 width=113 style="height:34pt;border-top:none;
  border-left:none;width:85pt">&nbsp;</td>

  <td class=xl9326832 width=26 style="border-top:none;width:20pt; height: 34pt;">&nbsp;</td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 34pt;">2. lieve
  obsolescenza</td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 34pt;">
      <asp:CheckBox ID="C_ANA_R2_lieve" runat="server" Text="." /></td>
  <td class=xl7826832 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">Mancata protezione centralino</td>
  <td class=xl6726832 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 34pt;">
      <asp:TextBox ID="T_ANO_R2_mancata0protezione0centralino" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox>&nbsp;</td>
  <td class=xl7826832 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 34pt;">
      spostamaento centralino<span style='mso-spacerun:yes'> </span></td>
  <td class=xl8126832 style="border-top:none;border-left:none; width: 20pt; height: 34pt;">
      <asp:CheckBox ID="C_INT_R2_spostamento0centralino" runat="server" Text="." /></td>

 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl9426832 width=113 style="height:31pt;border-left:
  none;width:85pt">&nbsp;</td>
  <td class=xl9526832 width=26 style="width:20pt; height: 31pt;">&nbsp;</td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 31pt;">3. forte
  obsolescenza</td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 31pt;">
      <asp:CheckBox ID="C_ANA_R2_forte" runat="server" Text="." /></td>
  <td class=xl6726832 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">segnalazioni di anomala ricezione</td>
  <td class=xl6726832 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 31pt;">
      <asp:TextBox ID="T_ANO_R2_anomalie0ricezione" runat="server" MaxLength="4" Width="24px"></asp:TextBox>&nbsp;</td>

  <td class=xl7826832 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">verifica impianto</td>
  <td class=xl8126832 style="border-top:none;border-left:none; width: 20pt; height: 31pt;">
      <asp:CheckBox ID="C_INT_R2_verifica0impianto" runat="server" Text="." /></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl9126832 width=113 style="height:17pt;border-left:
  none;width:85pt">&nbsp;</td>
  <td class=xl9226832 width=26 style="width:20pt; height: 17pt;">&nbsp;</td>
  <td class=xl6626832 style="border-top:none;border-left:none; height: 17pt;">4. totale
  obsolescenza</td>
  <td class=xl8326832 style="border-left:none; height: 17pt;">
      <asp:CheckBox ID="C_ANA_R2_tot" runat="server" Text="." /></td>

  <td class=xl8026832 width=145 style="border-left:none;width:109pt; height: 17pt;">&nbsp;</td>
  <td class=xl8026832 width=26 style="border-left:none;width:20pt; height: 17pt;">&nbsp;</td>
  <td class=xl8226832 width=145 style="border-left:none;width:109pt; height: 17pt;">&nbsp;</td>
  <td class=xl8926832 style="border-left:none; width: 20pt; height: 17pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=2 rowspan=2 height=34 class=xl11426832 style='height:25.5pt'>STATO
  DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl11526832>Stato 1</td>

  <td rowspan=2 class=xl9826832 style="border-top:none; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl9726832 style="border-top:none;border-left:none; height: 21pt;">Stato 2.1</td>
  <td class=xl9826832 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9726832 style="border-top:none;border-left:none; height: 21pt;">Stato 3.1</td>
  <td class=xl9826832 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl11526832 style='border-top:none'>Stato 3.3</td>
  <td class=xl9826832 style="border-top:none;border-left:none; width: 20pt; height: 21pt;">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl9726832 style="height:21pt;border-top:none;
  border-left:none">Stato 2.2</td>
  <td class=xl9826832 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9726832 style="border-top:none;border-left:none; height: 21pt;">Stato 3.2</td>
  <td class=xl9826832 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl9826832 style="border-top:none;border-left:none; width: 20pt; vertical-align: top; text-align: left; height: 21pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>

 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl10726832 width=857 style='border-right:1.0pt solid black;
  height:12.75pt;width:645pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7326832 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7426832></td>
  <td class=xl7426832></td>

  <td class=xl7426832></td>
  <td class=xl7426832></td>
  <td class=xl7426832></td>
  <td class=xl7426832></td>
  <td class=xl7426832></td>
  <td class=xl7526832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>

  <td class=xl1526832 style="width: 69pt"></td>
  <td class=xl1526832 style="width: 38pt"></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>

  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 colspan=7 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=31 style='mso-height-source:userset;height:23.25pt'>
  <td colspan=11 height=31 class=xl11026832 width=857 style='border-right:1.0pt solid black;
  height:23.25pt;width:645pt'>In particolare inserire per gli elementi tecnici
  riportati: dimensioni generali (U.M.) – ubicazione di anomalie più
  significative (percentuale per singola<br>
    <span style='mso-spacerun:yes'> </span>anomalia&gt;= 60%) –</td>
 </tr>
 <tr height=41 style='mso-height-source:userset;height:30.75pt'>
  <td colspan=11 class=xl11326832 width=857 style="border-right:1.0pt solid black;
  height:31pt;width:645pt">Per alcuni El. Tecnici potrebbe risultare
  necessario inserire diverse dimensioni in relazione alle diverse tipologie
  costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1526832 style="width: 69pt"></td>
  <td class=xl1526832 style="width: 38pt"></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>

  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1526832 colspan="9" rowspan="25">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="440px" TextMode="MultiLine" Width="752px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7126832 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6526832 style="width: 69pt">&nbsp;</td>

  <td class=xl6526832 style="width: 38pt">&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl7226832 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td height=17 class=xl6826832 colspan=2 style='height:12.75pt'>NOTE E
  COMMENTI:</td>
  <td class=xl6926832 style="border-top:none; width: 38pt;">&nbsp;</td>
  <td class=xl6926832 style='border-top:none'>&nbsp;</td>
  <td class=xl6926832 style='border-top:none'>&nbsp;</td>
  <td class=xl6926832 style='border-top:none'>&nbsp;</td>
  <td class=xl6926832 style='border-top:none'>&nbsp;</td>

  <td class=xl6926832 style='border-top:none'>&nbsp;</td>
  <td class=xl6926832 style='border-top:none'>&nbsp;</td>
  <td class=xl6926832 style='border-top:none'>&nbsp;</td>
  <td class=xl7026832 style="border-top:none; width: 20pt;">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl11326832 width=857
  style='border-right:1.0pt solid black;height:25.5pt;width:645pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1526832 style="width: 69pt"></td>
  <td class=xl1526832 style="width: 38pt"></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>

  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl1526832></td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1526832 colspan="9" rowspan="29">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="512px" TextMode="MultiLine" Width="752px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7626832 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7726832 style="width: 20pt">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7126832 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6526832 style="width: 69pt">&nbsp;</td>
  <td class=xl6526832 style="width: 38pt">&nbsp;</td>

  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>
      <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl6526832>&nbsp;</td>
  <td class=xl7226832 style="width: 20pt">&nbsp;</td>
 </tr>


 <tr height=0 style='display:none'>
  <td width=75 style='width:56pt'></td>
  <td style="width:69pt"></td>
  <td style='width:38pt'></td>
  <td width=113 style='width:85pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=141 style='width:106pt'></td>
  <td width=26 style='width:20pt'></td>

  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td style='width:20pt'></td>
 </tr>

</table>

    </div>
    </form>
</body>
</html>
