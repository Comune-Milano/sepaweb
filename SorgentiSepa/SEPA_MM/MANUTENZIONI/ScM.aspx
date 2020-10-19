<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ScM.aspx.vb" Inherits="ScM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <meta http-equiv=Content-Type content="text/html; charset=windows-1252">
    <meta name=ProgId content=Excel.Sheet>
    <meta name=Generator content="Microsoft Excel 12">

    <title>Scheda M</title>
    <style id="SCHEDE RILIEVO TUTTE_29795_Styles">

	{mso-displayed-decimal-separator:"\,";
	mso-displayed-thousand-separator:"\.";}
.xl1529795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6529795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6629795
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
.xl6729795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6829795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl6929795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7029795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7129795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7229795
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
.xl7329795
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
.xl7429795
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
.xl7529795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7629795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl7729795
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
.xl7829795
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
.xl7929795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8029795
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
.xl8129795
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
.xl8229795
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
.xl8329795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8429795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8529795
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
.xl8629795
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
.xl8729795
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
.xl8829795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl8929795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9029795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9129795
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
.xl9229795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9329795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9429795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9529795
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
.xl9629795
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
.xl9729795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9829795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl9929795
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
.xl10029795
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
.xl10129795
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
.xl10229795
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
.xl10329795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
	white-space:normal;}
.xl10429795
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
.xl10529795
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
.xl10629795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10729795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl10829795
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
.xl10929795
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
.xl11029795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11129795
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
.xl11229795
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
.xl11329795
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
.xl11429795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11529795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11629795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl11729795
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
.xl11829795
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
.xl11929795
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
.xl12029795
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
.xl12129795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl12229795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl12329795
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
.xl12429795
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
.xl12529795
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
.xl12629795
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
.xl12729795
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
.xl12829795
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
.xl12929795
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
.xl13029795
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
.xl13129795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13229795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13329795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13429795
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
.xl13529795
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
.xl13629795
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
.xl13729795
	{padding-top:1px;
	padding-right:1px;
	padding-left:1px;
	mso-ignore:padding;
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
.xl13829795
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
.xl13929795
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
.xl14029795
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
  <td colspan=9 height=34 class=xl11729795 width=738 style='height:26.1pt;
  width:556pt'>U. T.IMPIANTI ANTINCENDIO -<span style='mso-spacerun:yes'> 
  </span>SCHEDA RILIEVO IMPIANTI ANTINCENDIO</td>

  <td colspan=2 class=xl13929795 width=171 style='border-right:1.0pt solid black;
  width:129pt'>M</td>
 </tr>
 <tr height=53 style='mso-height-source:userset;height:39.95pt'>
  <td height=53 class=xl10429795 width=54 style='height:39.95pt;width:41pt'>Scheda
  M</td>
  <td class=xl10529795 width=131 style='border-left:none;width:98pt'>ELEMENTO
  TECNICO</td>
  <td class=xl10529795 style='border-left:none;width:41pt'></td>
  <td colspan=2 class=xl11929795 width=157 style='border-right:.5pt solid black;
  border-left:none;width:118pt'>MATERIALI E TIPOLOGIE COSTRUTTIVE (x)<br>

    (solo il prevalente)</td>
  <td colspan=2 class=xl11929795 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANALISI PRESTAZIONALE (X)</td>
  <td colspan=2 class=xl11929795 width=171 style='border-right:.5pt solid black;
  border-left:none;width:129pt'>ANOMALIE (%)</td>
  <td colspan=2 class=xl11929795 width=171 style='border-right:1.0pt solid black;
  border-left:none;width:129pt'>INTERVENTI</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td rowspan=4 height=86 class=xl12329795 width=54 style='border-bottom:1.0pt solid black;
  height:64.5pt;border-top:none;width:41pt'>M 1</td>

  <td rowspan=4 class=xl11329795 width=131 style='border-bottom:1.0pt solid black;
  border-top:none;width:98pt'>RETE DI DISTRIBUZIONE<br />
      ml<asp:TextBox ID="Text_mq_M1" runat="server" MaxLength="9" Width="96px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Text_mq_M1"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8729795 style="border-left:none;width:41pt; height: 31pt;"></td>
  <td class=xl9629795 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 31pt;">rete indraulica</td>
  <td class=xl8529795 width=26 style="border-left:none;width:20pt; height: 31pt;">
      <asp:CheckBox ID="C_MAT_M1_rete0idraulica" runat="server" Text="." />&nbsp;</td>
  <td class=xl8029795 style="border-top:none;border-left:none; height: 31pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>
  <td class=xl8629795 style="border-left:none; height: 31pt;">
      <asp:CheckBox ID="C_ANA_M1_nuovo" runat="server" Text="." /></td>

  <td class=xl9629795 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">perdita da<span style='mso-spacerun:yes'>  </span>vasca</td>
  <td class=xl8529795 width=26 style="border-left:none;width:20pt; height: 31pt;">
      <asp:TextBox ID="T_ANO_M1_perdita0vasca" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9629795 style="border-top:none;border-left:none;
  width:109pt; height: 31pt;">eliminazione perdite</td>
  <td class=xl8829795 style="border-left:none; height: 31pt;">
      <asp:CheckBox ID="C_INT_M1_eliminazione0perdite" runat="server" Text="." />&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td class=xl6729795 style="height:21pt;border-left:
  none;width:41pt">
      &nbsp;</td>

  <td class=xl9529795 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 21pt;">vasca di accumulo</td>
  <td class=xl7829795 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">
      <asp:CheckBox ID="C_MAT_M1_vasca0accumulo" runat="server" Text="." />&nbsp;</td>
  <td class=xl6629795 style="border-top:none;border-left:none; height: 21pt;">2. lieve
  obsolescenza</td>
  <td class=xl6629795 style="border-top:none;border-left:none; height: 21pt;">
      <asp:CheckBox ID="C_ANA_M1_lieve" runat="server" Text="." /></td>
  <td class=xl9529795 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">perdita da rete</td>
  <td class=xl7829795 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 21pt;">
      <asp:TextBox ID="T_ANO_M1_perdita0rete" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9529795 style="border-top:none;border-left:none;
  width:109pt; height: 21pt;">tinteggiatura tubazioni</td>

  <td class=xl7929795 style="border-top:none;border-left:none; height: 21pt;">
      <asp:CheckBox ID="C_INT_M1_tinteggiatura0tubazioni" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td height=34 class=xl6729795 style='height:25.5pt;border-left:none;
  width:41pt'>&nbsp;</td>
  <td class=xl9929795 width=131 style='border-top:none;border-left:none;
  width:98pt'>&nbsp;</td>
  <td class=xl6829795 width=26 style='border-top:none;border-left:none;
  width:20pt'></td>
  <td class=xl6629795 style='border-top:none;border-left:none'>3. forte
  obsolescenza</td>
  <td class=xl6629795 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_M1_forte" runat="server" Text="." /></td>

  <td class=xl9529795 style='border-top:none;border-left:none;
  width:109pt'>mancata identificazione rete</td>
  <td class=xl6829795 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_M1_mancanza0identificazione0rete" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl9929795 style='border-top:none;border-left:none;
  width:109pt'>&nbsp;</td>
  <td class=xl8929795 style='border-top:none;border-left:none'>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl6729795 style="height:24pt;border-left:none;
  width:41pt">&nbsp;</td>
  <td class=xl10229795 width=131 style="border-left:none;width:98pt; height: 24pt;">&nbsp;</td>

  <td class=xl7729795 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">&nbsp;</td>
  <td class=xl9929795 width=145 style="border-top:none;border-left:none;
  width:109pt; height: 24pt;">4. totale obsolescenza</td>
  <td class=xl8129795 style="border-top:none;border-left:none; height: 24pt;">
      <asp:CheckBox ID="C_ANA_M1_tot" runat="server" Text="." /></td>
  <td class=xl9929795 style="border-top:none;border-left:none;
  width:109pt; height: 24pt;">&nbsp;</td>
  <td class=xl7729795 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 24pt;">&nbsp;</td>
  <td class=xl10229795 style="border-left:none;width:109pt; height: 24pt;">&nbsp;</td>
  <td class=xl9729795 style="border-left:none; height: 24pt;">&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td rowspan=4 height=120 class=xl12329795 width=54 style='border-bottom:1.0pt solid black;
  height:90.0pt;border-top:none;width:41pt'>M 2</td>
  <td rowspan=4 class=xl11329795 width=131 style="border-bottom:1.0pt solid black;
  border-top:none;width:98pt; vertical-align: top; text-align: center;">TERMINALI<br />
      cad<asp:TextBox ID="Text_mq_M21" runat="server" MaxLength="9" Width="90px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Text_mq_M21"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator><br />
      <br />
      cad<asp:TextBox ID="Text_mq_M22" runat="server" MaxLength="9" Width="90px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Text_mq_M22"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator><br />
      <br />
      cad<asp:TextBox ID="Text_mq_M23" runat="server" MaxLength="9" Width="90px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="Text_mq_M23"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator><br />
      <br />
      cad<asp:TextBox ID="Text_mq_M24" runat="server" MaxLength="9" Width="90px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="Text_mq_M24"
          ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 432px; top: 264px"
          ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?" Width="1px"></asp:RegularExpressionValidator></td>
  <td class=xl8229795 style="border-left:none;width:41pt; height: 30pt;"></td>
  <td class=xl9629795 width=131 style="border-left:none;width:98pt; height: 30pt;">rilevatori
  fumo</td>
  <td class=xl9329795 width=26 style="border-left:none;width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_M2_rilevatori0fumo" runat="server" Text="." /></td>
  <td class=xl8029795 style="border-left:none; height: 30pt;">1. nuovo<span
  style='mso-spacerun:yes'> </span></td>

  <td class=xl8029795 style="border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_ANA_M2_nuovo" runat="server" Text="." /></td>
  <td class=xl9829795 style="border-left:none;width:109pt; height: 30pt;">perdite da
  terminale</td>
  <td class=xl9329795 width=26 style="border-left:none;width:20pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_M2_perdite0terminale" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9629795 style="border-left:none;width:109pt; height: 30pt;">ripristino
  perdite</td>
  <td class=xl9029795 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_M2_ripristino0perdite" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6729795 style="height:27pt;border-left:none;
  width:41pt">
      </td>

  <td class=xl9529795 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 27pt;">sprinkler</td>
  <td class=xl6829795 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 27pt;">
      <asp:CheckBox ID="C_MAT_M2_sprinkler" runat="server" Text="." /></td>
  <td class=xl6629795 style="border-top:none;border-left:none; height: 27pt;">2. lieve
  obsolescenza</td>
  <td class=xl6629795 style="border-top:none;border-left:none; height: 27pt;">
      <asp:CheckBox ID="C_ANA_M2_lieve" runat="server" Text="." /></td>
  <td class=xl9429795 style="border-top:none;border-left:none;
  width:109pt; height: 27pt;">mancata revisione terminale</td>
  <td class=xl6829795 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 27pt;">
      <asp:TextBox ID="T_ANO_M2_mancanza0terminale" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl9529795 style="border-top:none;border-left:none;
  width:109pt; height: 27pt;">manutenzione terminali</td>

  <td class=xl7929795 style="border-top:none;border-left:none; height: 27pt;">
      <asp:CheckBox ID="C_INT_M2_manutenzione0terminali" runat="server" Text="." /></td>
 </tr>
 <tr height=34 style='height:25.5pt'>
  <td class=xl6729795 style="height:30pt;border-left:none;
  width:41pt">
      </td>
  <td class=xl9529795 width=131 style="border-top:none;border-left:none;
  width:98pt; height: 30pt;">idranti</td>
  <td class=xl6829795 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:CheckBox ID="C_MAT_M2_idranti" runat="server" Text="." /></td>
  <td class=xl6629795 style="border-top:none;border-left:none; height: 30pt;">3. forte
  obsolescenza</td>
  <td class=xl6629795 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_ANA_M2_forte" runat="server" Text="." /></td>

  <td class=xl9429795 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">asportazione di parti di impianto</td>
  <td class=xl6829795 width=26 style="border-top:none;border-left:none;
  width:20pt; height: 30pt;">
      <asp:TextBox ID="T_ANO_M2_asportazione0parti0impianto" runat="server" MaxLength="4"
          Width="24px"></asp:TextBox></td>
  <td class=xl9529795 style="border-top:none;border-left:none;
  width:109pt; height: 30pt;">ripristino terminali</td>
  <td class=xl7929795 style="border-top:none;border-left:none; height: 30pt;">
      <asp:CheckBox ID="C_INT_M2_ripristino0terminali" runat="server" Text="." /></td>
 </tr>
 <tr height=35 style='height:26.25pt'>
  <td height=35 class=xl8329795 style='height:26.25pt;border-left:
  none;width:41pt'>
      </td>
  <td class=xl10129795 width=131 style='border-top:none;border-left:none;
  width:98pt'>estintori</td>

  <td class=xl8429795 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:CheckBox ID="C_MAT_M2_estintori" runat="server" Text="." /></td>
  <td class=xl9129795 style='border-top:none;border-left:none'>4. totale
  obsolescenza</td>
  <td class=xl9129795 style='border-top:none;border-left:none'>
      <asp:CheckBox ID="C_ANA_M2_tot" runat="server" Text="." /></td>
  <td class=xl10329795 style='border-top:none;border-left:none;
  width:109pt'>inadeguatezza alloggiamenti impianti</td>
  <td class=xl8429795 width=26 style='border-top:none;border-left:none;
  width:20pt'>
      <asp:TextBox ID="T_ANO_M2_inadeguatezza0impianti" runat="server" MaxLength="4" Width="24px"></asp:TextBox></td>
  <td class=xl10029795 style='width:109pt'>adeguamento alloggiamenti</td>
  <td class=xl9229795 style='border-top:none'>
      <asp:CheckBox ID="C_INT_M2_adeguamento0alloggiamenti" runat="server" Text="." /></td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td colspan=2 rowspan=2 height=36 class=xl13429795 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black;height:27.0pt'>STATO DI DEGRADO</td>
  <td colspan=2 rowspan=2 class=xl13629795 style='border-right:1.0pt solid black;
  border-bottom:1.0pt solid black'>Stato 1</td>
  <td rowspan=2 class=xl11029795 style="border-bottom:1.0pt solid black; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt1" runat="server" Height="24px" Style="position: static" Text="."
          Width="1px" /></td>
  <td class=xl10829795 style="border-top:none;border-left:none; height: 22pt;">Stato 2.1</td>
  <td class=xl10729795 style="border-left:none; height: 22pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt21" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>

  <td class=xl10829795 style="border-top:none;border-left:none; width: 109pt; height: 22pt;">Stato 3.1</td>
  <td class=xl10729795 style="border-left:none; height: 22pt; vertical-align: top; text-align: left;">
      <asp:CheckBox ID="ChkSt31" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td rowspan=2 class=xl13829795 style="border-bottom:1.0pt solid black; width: 109pt;">Stato
  3.3</td>
  <td class=xl11029795 style="border-left:none; height: 22pt; vertical-align: top; text-align: left;">&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td class=xl10929795 style="height:20pt;border-top:none;
  border-left:none">Stato 2.2</td>

  <td class=xl10629795 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt22" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl10929795 style="border-top:none;border-left:none; width: 109pt; height: 20pt;">Stato 3.2</td>
  <td class=xl10629795 style="border-top:none;border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt32" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
  <td class=xl10729795 style="border-left:none; vertical-align: top; text-align: left; height: 20pt;">
      <asp:CheckBox ID="ChkSt33" runat="server" Height="24px" Style="position: static"
          Text="." Width="1px" /></td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>
  <td colspan=11 height=17 class=xl12629795 width=909 style='border-right:1.0pt solid black;
  height:12.75pt;width:685pt'><span
  style='mso-spacerun:yes'> </span>ANNOTAZIONI RELATIVE ALLA INDIVIDUAZIONE
  SPAZIALE E PLANIMETRICA DEGLI ELEMENTI TECNICI OGGETTO DI INDAGINE<span
  style='mso-spacerun:yes'> </span></td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7229795 colspan=3 style='height:12.75pt'>– NOTE A
  MARGINE GENERALI -</td>
  <td class=xl7329795></td>
  <td class=xl7329795></td>
  <td class=xl7329795></td>
  <td class=xl7329795></td>
  <td class=xl7329795 style="width: 109pt"></td>

  <td class=xl7329795></td>
  <td class=xl7329795 style="width: 109pt"></td>
  <td class=xl7429795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7229795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7329795></td>
  <td class=xl7329795 style="width: 41pt"></td>
  <td class=xl7329795></td>

  <td class=xl7329795></td>
  <td class=xl7329795></td>
  <td class=xl7329795></td>
  <td class=xl7329795 style="width: 109pt"></td>
  <td class=xl7329795></td>
  <td class=xl7329795 style="width: 109pt"></td>
  <td class=xl7429795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7529795 colspan=6 style='height:12.75pt'>Inserire
  informazioni attinenti alla collocazione degli elementi tecnici e delle
  anomalie.</td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 109pt"></td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 109pt"></td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7529795 colspan=10 style='height:12.75pt'>In
  particolare inserire gli elementi tecnici riportati, dimensioni generali (
  U.M.) ubicazioni di anomalie più significative ( percentuale per singola
  anomalia<span style='mso-spacerun:yes'> </span></td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&#8805; 60%)</td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 41pt"></td>

  <td class=xl1529795></td>
  <td class=xl1529795></td>
  <td class=xl1529795></td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 109pt"></td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 109pt"></td>
  <td class=xl7629795>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 colspan=10 style='height:12.75pt'>Per alcuni
  El. Tecnici potrebbe risultare necessario inserire diverse dimensioni in
  relazione alle diverse tipologie costruttive che sono state catalogate.<span
  style='mso-spacerun:yes'>  </span></td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 41pt"></td>

  <td class=xl1529795></td>
  <td class=xl1529795></td>
  <td class=xl1529795></td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 109pt"></td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 109pt"></td>
  <td class=xl7629795>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1529795 colspan="9" rowspan="19">
      <asp:TextBox ID="TxtNote_1" runat="server" Height="336px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7029795 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795 style="width: 41pt">&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>

  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795 style="width: 109pt">&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795 style="width: 109pt">&nbsp;</td>
  <td class=xl7129795>&nbsp;</td>
 </tr>
 <tr height=17 style='page-break-before:always;height:12.75pt'>

  <td height=17 class=xl7529795 style='height:12.75pt'>NOTE E <span
  style='display:none'>COMMENTI:</span></td>
  <td class=xl6929795 style='border-top:none'>&nbsp;</td>
  <td class=xl6929795 style="border-top:none; width: 41pt;">&nbsp;</td>
  <td class=xl6929795 style='border-top:none'>&nbsp;</td>
  <td class=xl6929795 style='border-top:none'>&nbsp;</td>
  <td class=xl6929795 style='border-top:none'>&nbsp;</td>
  <td class=xl6929795 style='border-top:none'>&nbsp;</td>
  <td class=xl6929795 style="border-top:none; width: 109pt;">&nbsp;</td>

  <td class=xl6929795 style='border-top:none'>&nbsp;</td>
  <td class=xl6929795 style="border-top:none; width: 109pt;">&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td colspan=11 rowspan=2 height=34 class=xl11629795 width=909
  style='border-right:1.0pt solid black;height:25.5pt;width:685pt'>Inserire il
  repertorio fotografico (con precisazione della postazione e collegamento con
  el.tecnico/anomalia) e le note inerenti ad anomalie non previste nella scheda
  di rilievo, nonché le segnalazioni di eventuali difficoltà logistiche (ad
  esempio, di accesso).</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 41pt"></td>
  <td class=xl1529795></td>
  <td class=xl1529795></td>
  <td class=xl1529795></td>
  <td class=xl1529795></td>

  <td class=xl1529795 style="width: 109pt"></td>
  <td class=xl1529795></td>
  <td class=xl1529795 style="width: 109pt"></td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl1529795 colspan="9" rowspan="26">
      <asp:TextBox ID="TxtNote_2" runat="server" Height="464px" TextMode="MultiLine" Width="824px" Font-Size="12pt"></asp:TextBox></td>
  <td class=xl7629795>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>

 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>

 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>

  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>

  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>
 </tr>
 <tr height=17 style='height:12.75pt'>
  <td height=17 class=xl7529795 style='height:12.75pt'>&nbsp;</td>
  <td class=xl7629795>&nbsp;</td>

 </tr>
 <tr height=18 style='height:13.5pt'>
  <td height=18 class=xl7029795 style='height:13.5pt'>&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795 style="width: 41pt">&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795>
      <asp:Button ID="Button1" runat="server" Text="Button" Visible="False" />&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>

  <td class=xl6529795 style="width: 109pt">&nbsp;</td>
  <td class=xl6529795>&nbsp;</td>
  <td class=xl6529795 style="width: 109pt">&nbsp;</td>
  <td class=xl7129795>&nbsp;</td>
 </tr>

 <tr height=0 style='display:none'>
  <td width=54 style='width:41pt'></td>
  <td width=131 style='width:98pt'></td>

  <td style='width:41pt'></td>
  <td width=131 style='width:98pt'></td>
  <td width=26 style='width:20pt'></td>
  <td width=145 style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
  <td style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>
  <td style='width:109pt'></td>
  <td width=26 style='width:20pt'></td>

 </tr>

</table>

    </div>
    </form>
</body>
</html>
