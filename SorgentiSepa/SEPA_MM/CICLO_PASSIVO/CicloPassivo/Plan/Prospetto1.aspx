<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Prospetto1.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_Prospetto1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>

<script type="text/javascript">
    function aprichiudi(item) {
        elem = document.getElementById(item);
        visibile = (elem.style.display != "none")
        prefisso = document.getElementById("menu" + item);
        if (visibile) {
            elem.style.display = "none";
            prefisso.innerHTML = "<img src='Immagini/cartella.gif' width='16' height='16' hspace='0' vspace='0' border='0'>";
        } else {
            elem.style.display = "block";
            prefisso.innerHTML = "<img src='Immagini/cartellaaperta.gif' width='16' height='16' hspace='0' vspace='0' border='0'>";
        }
    }

    function NascondiStandard() {
//        document.getElementById('AggVociS1V1').style.visibility = 'hidden';
//        document.getElementById('AggVociS1V1').style.position = 'absolute';
//        document.getElementById('AggVociS1V1').style.left = '-100px';
//        document.getElementById('AggVociS1V1').style.display = 'none';

//        document.getElementById('AggVociS1V2').style.visibility = 'hidden';
//        document.getElementById('AggVociS1V2').style.position = 'absolute';
//        document.getElementById('AggVociS1V2').style.left = '-100px';
//        document.getElementById('AggVociS1V2').style.display = 'none';

////        document.getElementById('AggVociS1V3').style.visibility = 'hidden';
////        document.getElementById('AggVociS1V3').style.position = 'absolute';
////        document.getElementById('AggVociS1V3').style.left = '-100px';
////        document.getElementById('AggVociS1V3').style.display = 'none';

////        document.getElementById('AggVociS1V4').style.visibility = 'hidden';
////        document.getElementById('AggVociS1V4').style.position = 'absolute';
////        document.getElementById('AggVociS1V4').style.left = '-100px';
////        document.getElementById('AggVociS1V4').style.display = 'none';

////        document.getElementById('AggVociS1V5').style.visibility = 'hidden';
////        document.getElementById('AggVociS1V5').style.position = 'absolute';
////        document.getElementById('AggVociS1V5').style.left = '-100px';
////        document.getElementById('AggVociS1V5').style.display = 'none';

////        document.getElementById('AggVociS1V6').style.visibility = 'hidden';
////        document.getElementById('AggVociS1V6').style.position = 'absolute';
////        document.getElementById('AggVociS1V6').style.left = '-100px';
////        document.getElementById('AggVociS1V6').style.display = 'none';

////        document.getElementById('AggVociS1V7').style.visibility = 'hidden';
////        document.getElementById('AggVociS1V7').style.position = 'absolute';
////        document.getElementById('AggVociS1V7').style.left = '-100px';
////        document.getElementById('AggVociS1V7').style.display = 'none';

////        document.getElementById('AggVociS1V8').style.visibility = 'hidden';
////        document.getElementById('AggVociS1V8').style.position = 'absolute';
////        document.getElementById('AggVociS1V8').style.left = '-100px';
////        document.getElementById('AggVociS1V8').style.display = 'none';

////        document.getElementById('AggVociS1V9').style.visibility = 'hidden';
////        document.getElementById('AggVociS1V9').style.position = 'absolute';
////        document.getElementById('AggVociS1V9').style.left = '-100px';
////        document.getElementById('AggVociS1V9').style.display = 'none';

////        document.getElementById('AggVociS1V10').style.visibility = 'hidden';
////        document.getElementById('AggVociS1V10').style.position = 'absolute';
////        document.getElementById('AggVociS1V10').style.left = '-100px';
////        document.getElementById('AggVociS1V10').style.display = 'none';

//        document.getElementById('AggVociS2V1').style.visibility = 'hidden';
//        document.getElementById('AggVociS2V1').style.position = 'absolute';
//        document.getElementById('AggVociS2V1').style.left = '-100px';
//        document.getElementById('AggVociS2V1').style.display = 'none';

//        document.getElementById('AggVociS2V2').style.visibility = 'hidden';
//        document.getElementById('AggVociS2V2').style.position = 'absolute';
//        document.getElementById('AggVociS2V2').style.left = '-100px';
//        document.getElementById('AggVociS2V2').style.display = 'none';

//        document.getElementById('AggVociS2V3').style.visibility = 'hidden';
//        document.getElementById('AggVociS2V3').style.position = 'absolute';
//        document.getElementById('AggVociS2V3').style.left = '-100px';
//        document.getElementById('AggVociS2V3').style.display = 'none';

//        document.getElementById('AggVociS2V4').style.visibility = 'hidden';
//        document.getElementById('AggVociS2V4').style.position = 'absolute';
//        document.getElementById('AggVociS2V4').style.left = '-100px';
//        document.getElementById('AggVociS2V4').style.display = 'none';

////        document.getElementById('AggVociS2V5').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V5').style.position = 'absolute';
////        document.getElementById('AggVociS2V5').style.left = '-100px';
////        document.getElementById('AggVociS2V5').style.display = 'none';

////        document.getElementById('AggVociS2V6').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V6').style.position = 'absolute';
////        document.getElementById('AggVociS2V6').style.left = '-100px';
////        document.getElementById('AggVociS2V6').style.display = 'none';

////        document.getElementById('AggVociS2V7').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V7').style.position = 'absolute';
////        document.getElementById('AggVociS2V7').style.left = '-100px';
////        document.getElementById('AggVociS2V7').style.display = 'none';

////        document.getElementById('AggVociS2V8').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V8').style.position = 'absolute';
////        document.getElementById('AggVociS2V8').style.left = '-100px';
////        document.getElementById('AggVociS2V8').style.display = 'none';

////        document.getElementById('AggVociS2V9').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V9').style.position = 'absolute';
////        document.getElementById('AggVociS2V9').style.left = '-100px';
////        document.getElementById('AggVociS2V9').style.display = 'none';

////        document.getElementById('AggVociS2V10').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V10').style.position = 'absolute';
////        document.getElementById('AggVociS2V10').style.left = '-100px';
////        document.getElementById('AggVociS2V10').style.display = 'none';

////        document.getElementById('AggVociS2V11').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V11').style.position = 'absolute';
////        document.getElementById('AggVociS2V11').style.left = '-100px';
////        document.getElementById('AggVociS2V11').style.display = 'none';

////        document.getElementById('AggVociS2V12').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V12').style.position = 'absolute';
////        document.getElementById('AggVociS2V12').style.left = '-100px';
////        document.getElementById('AggVociS2V12').style.display = 'none';

////        document.getElementById('AggVociS2V13').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V13').style.position = 'absolute';
////        document.getElementById('AggVociS2V13').style.left = '-100px';
////        document.getElementById('AggVociS2V13').style.display = 'none';

////        document.getElementById('AggVociS2V14').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V14').style.position = 'absolute';
////        document.getElementById('AggVociS2V14').style.left = '-100px';
////        document.getElementById('AggVociS2V14').style.display = 'none';

////        document.getElementById('AggVociS2V15').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V15').style.position = 'absolute';
////        document.getElementById('AggVociS2V15').style.left = '-100px';
////        document.getElementById('AggVociS2V15').style.display = 'none';

////        document.getElementById('AggVociS2V16').style.visibility = 'hidden';
////        document.getElementById('AggVociS2V16').style.position = 'absolute';
////        document.getElementById('AggVociS2V16').style.left = '-100px';
////        document.getElementById('AggVociS2V16').style.display = 'none';

//        document.getElementById('AggVociS3V1').style.visibility = 'hidden';
//        document.getElementById('AggVociS3V1').style.position = 'absolute';
//        document.getElementById('AggVociS3V1').style.left = '-100px';
//        document.getElementById('AggVociS3V1').style.display = 'none';

//        document.getElementById('AggVociS3V2').style.visibility = 'hidden';
//        document.getElementById('AggVociS3V2').style.position = 'absolute';
//        document.getElementById('AggVociS3V2').style.left = '-100px';
//        document.getElementById('AggVociS3V2').style.display = 'none';

    }

    function Nascondi() {
        if (document.getElementById('statop').value == '1') {
            document.getElementById('AggVociS1V1').style.visibility = 'hidden';
            document.getElementById('AggVociS1V1').style.position = 'absolute';
            document.getElementById('AggVociS1V1').style.left = '-100px';
            document.getElementById('AggVociS1V1').style.display = 'none';

            document.getElementById('AggVociS1V2').style.visibility = 'hidden';
            document.getElementById('AggVociS1V2').style.position = 'absolute';
            document.getElementById('AggVociS1V2').style.left = '-100px';
            document.getElementById('AggVociS1V2').style.display = 'none';

            document.getElementById('AggVociS1V3').style.visibility = 'hidden';
            document.getElementById('AggVociS1V3').style.position = 'absolute';
            document.getElementById('AggVociS1V3').style.left = '-100px';
            document.getElementById('AggVociS1V3').style.display = 'none';

            document.getElementById('AggVociS1V4').style.visibility = 'hidden';
            document.getElementById('AggVociS1V4').style.position = 'absolute';
            document.getElementById('AggVociS1V4').style.left = '-100px';
            document.getElementById('AggVociS1V4').style.display = 'none';

            document.getElementById('AggVociS1V5').style.visibility = 'hidden';
            document.getElementById('AggVociS1V5').style.position = 'absolute';
            document.getElementById('AggVociS1V5').style.left = '-100px';
            document.getElementById('AggVociS1V5').style.display = 'none';

            document.getElementById('AggVociS1V6').style.visibility = 'hidden';
            document.getElementById('AggVociS1V6').style.position = 'absolute';
            document.getElementById('AggVociS1V6').style.left = '-100px';
            document.getElementById('AggVociS1V6').style.display = 'none';

            document.getElementById('AggVociS1V7').style.visibility = 'hidden';
            document.getElementById('AggVociS1V7').style.position = 'absolute';
            document.getElementById('AggVociS1V7').style.left = '-100px';
            document.getElementById('AggVociS1V7').style.display = 'none';

            document.getElementById('AggVociS1V8').style.visibility = 'hidden';
            document.getElementById('AggVociS1V8').style.position = 'absolute';
            document.getElementById('AggVociS1V8').style.left = '-100px';
            document.getElementById('AggVociS1V8').style.display = 'none';

            document.getElementById('AggVociS1V9').style.visibility = 'hidden';
            document.getElementById('AggVociS1V9').style.position = 'absolute';
            document.getElementById('AggVociS1V9').style.left = '-100px';
            document.getElementById('AggVociS1V9').style.display = 'none';

            document.getElementById('AggVociS1V10').style.visibility = 'hidden';
            document.getElementById('AggVociS1V10').style.position = 'absolute';
            document.getElementById('AggVociS1V10').style.left = '-100px';
            document.getElementById('AggVociS1V10').style.display = 'none';

            document.getElementById('AggVociS1V11').style.visibility = 'hidden';
            document.getElementById('AggVociS1V11').style.position = 'absolute';
            document.getElementById('AggVociS1V11').style.left = '-100px';
            document.getElementById('AggVociS1V11').style.display = 'none';

            document.getElementById('AggVociS1V12').style.visibility = 'hidden';
            document.getElementById('AggVociS1V12').style.position = 'absolute';
            document.getElementById('AggVociS1V12').style.left = '-100px';
            document.getElementById('AggVociS1V12').style.display = 'none';

            document.getElementById('AggVociS1V13').style.visibility = 'hidden';
            document.getElementById('AggVociS1V13').style.position = 'absolute';
            document.getElementById('AggVociS1V13').style.left = '-100px';
            document.getElementById('AggVociS1V13').style.display = 'none';

            document.getElementById('AggVociS1V14').style.visibility = 'hidden';
            document.getElementById('AggVociS1V14').style.position = 'absolute';
            document.getElementById('AggVociS1V14').style.left = '-100px';
            document.getElementById('AggVociS1V14').style.display = 'none';

            document.getElementById('AggVociS1V15').style.visibility = 'hidden';
            document.getElementById('AggVociS1V15').style.position = 'absolute';
            document.getElementById('AggVociS1V15').style.left = '-100px';
            document.getElementById('AggVociS1V15').style.display = 'none';

            document.getElementById('AggVociS1V16').style.visibility = 'hidden';
            document.getElementById('AggVociS1V16').style.position = 'absolute';
            document.getElementById('AggVociS1V16').style.left = '-100px';
            document.getElementById('AggVociS1V16').style.display = 'none';

            document.getElementById('AggVociS1V17').style.visibility = 'hidden';
            document.getElementById('AggVociS1V17').style.position = 'absolute';
            document.getElementById('AggVociS1V17').style.left = '-100px';
            document.getElementById('AggVociS1V17').style.display = 'none';

            document.getElementById('AggVociS1V18').style.visibility = 'hidden';
            document.getElementById('AggVociS1V18').style.position = 'absolute';
            document.getElementById('AggVociS1V18').style.left = '-100px';
            document.getElementById('AggVociS1V18').style.display = 'none';

            document.getElementById('AggVociS1V19').style.visibility = 'hidden';
            document.getElementById('AggVociS1V19').style.position = 'absolute';
            document.getElementById('AggVociS1V19').style.left = '-100px';
            document.getElementById('AggVociS1V19').style.display = 'none';

            document.getElementById('AggVociS1V20').style.visibility = 'hidden';
            document.getElementById('AggVociS1V20').style.position = 'absolute';
            document.getElementById('AggVociS1V20').style.left = '-100px';
            document.getElementById('AggVociS1V20').style.display = 'none';



            document.getElementById('AggVociS2V1').style.visibility = 'hidden';
            document.getElementById('AggVociS2V1').style.position = 'absolute';
            document.getElementById('AggVociS2V1').style.left = '-100px';
            document.getElementById('AggVociS2V1').style.display = 'none';

            document.getElementById('AggVociS2V2').style.visibility = 'hidden';
            document.getElementById('AggVociS2V2').style.position = 'absolute';
            document.getElementById('AggVociS2V2').style.left = '-100px';
            document.getElementById('AggVociS2V2').style.display = 'none';

            document.getElementById('AggVociS2V3').style.visibility = 'hidden';
            document.getElementById('AggVociS2V3').style.position = 'absolute';
            document.getElementById('AggVociS2V3').style.left = '-100px';
            document.getElementById('AggVociS2V3').style.display = 'none';

            document.getElementById('AggVociS2V4').style.visibility = 'hidden';
            document.getElementById('AggVociS2V4').style.position = 'absolute';
            document.getElementById('AggVociS2V4').style.left = '-100px';
            document.getElementById('AggVociS2V4').style.display = 'none';

            document.getElementById('AggVociS2V5').style.visibility = 'hidden';
            document.getElementById('AggVociS2V5').style.position = 'absolute';
            document.getElementById('AggVociS2V5').style.left = '-100px';
            document.getElementById('AggVociS2V5').style.display = 'none';

            document.getElementById('AggVociS2V6').style.visibility = 'hidden';
            document.getElementById('AggVociS2V6').style.position = 'absolute';
            document.getElementById('AggVociS2V6').style.left = '-100px';
            document.getElementById('AggVociS2V6').style.display = 'none';

            document.getElementById('AggVociS2V7').style.visibility = 'hidden';
            document.getElementById('AggVociS2V7').style.position = 'absolute';
            document.getElementById('AggVociS2V7').style.left = '-100px';
            document.getElementById('AggVociS2V7').style.display = 'none';

            document.getElementById('AggVociS2V8').style.visibility = 'hidden';
            document.getElementById('AggVociS2V8').style.position = 'absolute';
            document.getElementById('AggVociS2V8').style.left = '-100px';
            document.getElementById('AggVociS2V8').style.display = 'none';

            document.getElementById('AggVociS2V9').style.visibility = 'hidden';
            document.getElementById('AggVociS2V9').style.position = 'absolute';
            document.getElementById('AggVociS2V9').style.left = '-100px';
            document.getElementById('AggVociS2V9').style.display = 'none';

            document.getElementById('AggVociS2V10').style.visibility = 'hidden';
            document.getElementById('AggVociS2V10').style.position = 'absolute';
            document.getElementById('AggVociS2V10').style.left = '-100px';
            document.getElementById('AggVociS2V10').style.display = 'none';

            document.getElementById('AggVociS2V11').style.visibility = 'hidden';
            document.getElementById('AggVociS2V11').style.position = 'absolute';
            document.getElementById('AggVociS2V11').style.left = '-100px';
            document.getElementById('AggVociS2V11').style.display = 'none';

            document.getElementById('AggVociS2V12').style.visibility = 'hidden';
            document.getElementById('AggVociS2V12').style.position = 'absolute';
            document.getElementById('AggVociS2V12').style.left = '-100px';
            document.getElementById('AggVociS2V12').style.display = 'none';

            document.getElementById('AggVociS2V13').style.visibility = 'hidden';
            document.getElementById('AggVociS2V13').style.position = 'absolute';
            document.getElementById('AggVociS2V13').style.left = '-100px';
            document.getElementById('AggVociS2V13').style.display = 'none';

            document.getElementById('AggVociS2V14').style.visibility = 'hidden';
            document.getElementById('AggVociS2V14').style.position = 'absolute';
            document.getElementById('AggVociS2V14').style.left = '-100px';
            document.getElementById('AggVociS2V14').style.display = 'none';

            document.getElementById('AggVociS2V15').style.visibility = 'hidden';
            document.getElementById('AggVociS2V15').style.position = 'absolute';
            document.getElementById('AggVociS2V15').style.left = '-100px';
            document.getElementById('AggVociS2V15').style.display = 'none';

            document.getElementById('AggVociS2V16').style.visibility = 'hidden';
            document.getElementById('AggVociS2V16').style.position = 'absolute';
            document.getElementById('AggVociS2V16').style.left = '-100px';
            document.getElementById('AggVociS2V16').style.display = 'none';

            document.getElementById('AggVociS2V17').style.visibility = 'hidden';
            document.getElementById('AggVociS2V17').style.position = 'absolute';
            document.getElementById('AggVociS2V17').style.left = '-100px';
            document.getElementById('AggVociS2V17').style.display = 'none';

            document.getElementById('AggVociS2V18').style.visibility = 'hidden';
            document.getElementById('AggVociS2V18').style.position = 'absolute';
            document.getElementById('AggVociS2V18').style.left = '-100px';
            document.getElementById('AggVociS2V18').style.display = 'none';

            document.getElementById('AggVociS2V19').style.visibility = 'hidden';
            document.getElementById('AggVociS2V19').style.position = 'absolute';
            document.getElementById('AggVociS2V19').style.left = '-100px';
            document.getElementById('AggVociS2V19').style.display = 'none';

            document.getElementById('AggVociS2V20').style.visibility = 'hidden';
            document.getElementById('AggVociS2V20').style.position = 'absolute';
            document.getElementById('AggVociS2V20').style.left = '-100px';
            document.getElementById('AggVociS2V20').style.display = 'none';


            document.getElementById('AggVociS3V1').style.visibility = 'hidden';
            document.getElementById('AggVociS3V1').style.position = 'absolute';
            document.getElementById('AggVociS3V1').style.left = '-100px';
            document.getElementById('AggVociS3V1').style.display = 'none';

            document.getElementById('AggVociS3V2').style.visibility = 'hidden';
            document.getElementById('AggVociS3V2').style.position = 'absolute';
            document.getElementById('AggVociS3V2').style.left = '-100px';
            document.getElementById('AggVociS3V2').style.display = 'none';

            document.getElementById('AggVociS3V3').style.visibility = 'hidden';
            document.getElementById('AggVociS3V3').style.position = 'absolute';
            document.getElementById('AggVociS3V3').style.left = '-100px';
            document.getElementById('AggVociS3V3').style.display = 'none';

            document.getElementById('AggVociS3V4').style.visibility = 'hidden';
            document.getElementById('AggVociS3V4').style.position = 'absolute';
            document.getElementById('AggVociS3V4').style.left = '-100px';
            document.getElementById('AggVociS3V4').style.display = 'none';

            document.getElementById('AggVociS3V5').style.visibility = 'hidden';
            document.getElementById('AggVociS3V5').style.position = 'absolute';
            document.getElementById('AggVociS3V5').style.left = '-100px';
            document.getElementById('AggVociS3V5').style.display = 'none';


            document.getElementById('AggVociS4V1').style.visibility = 'hidden';
            document.getElementById('AggVociS4V1').style.position = 'absolute';
            document.getElementById('AggVociS4V1').style.left = '-100px';
            document.getElementById('AggVociS4V1').style.display = 'none';

            document.getElementById('AggVociS4V2').style.visibility = 'hidden';
            document.getElementById('AggVociS4V2').style.position = 'absolute';
            document.getElementById('AggVociS4V2').style.left = '-100px';
            document.getElementById('AggVociS4V2').style.display = 'none';

            document.getElementById('AggVociS4V3').style.visibility = 'hidden';
            document.getElementById('AggVociS4V3').style.position = 'absolute';
            document.getElementById('AggVociS4V3').style.left = '-100px';
            document.getElementById('AggVociS4V3').style.display = 'none';

            document.getElementById('AggVociS4V4').style.visibility = 'hidden';
            document.getElementById('AggVociS4V4').style.position = 'absolute';
            document.getElementById('AggVociS4V4').style.left = '-100px';
            document.getElementById('AggVociS4V4').style.display = 'none';

            document.getElementById('AggVociS4V5').style.visibility = 'hidden';
            document.getElementById('AggVociS4V5').style.position = 'absolute';
            document.getElementById('AggVociS4V5').style.left = '-100px';
            document.getElementById('AggVociS4V5').style.display = 'none';
        }
    }
    
    
</script>

<head id="Head1" runat="server">
    <title>Prospetto</title>
    <style type="text/css">
        .style1
        {
            color: #000099;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Piano Finanziario-</strong>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    -
                    <asp:Label ID="lblStato" runat="server" style="font-weight: 700"></asp:Label>
                    </span><br />
                    <br />

                    <div id="contenitore" 
                        style="position: absolute; overflow: auto; top: 62px; left: 12px; width: 773px; height: 434px;">
                        <table style="width:100%;">
                            <tr>
                                <td style="width: 15px">
                                    <a id="menuSEZ1" href="javascript:aprichiudi('SEZ1');">
    <img alt="img" src='Immagini/cartella.gif' width='16' height='16' hspace='0' vspace='0' 
        border='0'/></a></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <strong>1.0 SPESE PROPERTY MANAGEMENT</strong></td>
                            </tr>
                            </table>
                            <div id="SEZ1" style="margin-left: 2em; display: none; ">
                            <table style="width:100%;">
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V1" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce1').value,'1.01','S1voce1');"/></td>
                <td><asp:ImageButton ID="AggOperatoriS1V1" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce1').value;document.getElementById('casella').value='S1voce1';myOpacity1.toggle();" />  <img id="imgElencoS1V1" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce1').value+'&V='+document.getElementById('S1Op1').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce1" runat="server" />
                                    <asp:HiddenField ID="S1Op1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto1Sotto10" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V2" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce2').value,'1.02','S1voce2'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V2" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce2').value;document.getElementById('casella').value='S1voce2';myOpacity1.toggle();" />  <img id="imgElencoS1V2" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce2').value+'&V='+document.getElementById('S1Op2').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce2" runat="server" />
                                    <asp:HiddenField ID="S1Op2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto2Sotto10" runat="server" />
                                </td>
                            </tr>    
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V3" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce3').value,'1.03','S1voce3'); "/></td>
       <td><asp:ImageButton ID="AggOperatoriS1V3" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce3').value;document.getElementById('casella').value='S1voce3';myOpacity1.toggle();" />  <img id="imgElencoS1V3" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce3').value+'&V='+document.getElementById('S1Op3').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce3" runat="server" />
                                    <asp:HiddenField ID="S1Op3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto3Sotto10" runat="server" />
                                </td>
                            </tr>      
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V4" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce4').value,'1.04','S1voce4'); "/></td>
        <td><asp:ImageButton ID="AggOperatoriS1V4" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce4').value;document.getElementById('casella').value='S1voce4';myOpacity1.toggle();" />  <img id="imgElencoS1V4" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce4').value+'&V='+document.getElementById('S1Op4').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce4" runat="server" />
                                    <asp:HiddenField ID="S1Op4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto4Sotto10" runat="server" />
                                </td>
                            </tr>          
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V5" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce5').value,'1.05','S1voce5'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V5" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce5').value;document.getElementById('casella').value='S1voce5';myOpacity1.toggle();" />  <img id="imgElencoS1V5" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce5').value+'&V='+document.getElementById('S1Op5').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce5" runat="server" />
                                    <asp:HiddenField ID="S1Op5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto5Sotto10" runat="server" />
                                </td>
                            </tr>   
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod6" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce6" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V6" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce6').value,'1.06','S1voce6');"/></td>
        <td><asp:ImageButton ID="AggOperatoriS1V6" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce6').value;document.getElementById('casella').value='S1voce6';myOpacity1.toggle();" />  <img id="imgElencoS1V6" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce6').value+'&V='+document.getElementById('S1Op6').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce6" runat="server" />
                                    <asp:HiddenField ID="S1Op6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto6Sotto10" runat="server" />
                                </td>
                            </tr>        
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod7" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce7" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V7" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce7').value,'1.07','S1voce7'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V7" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce7').value;document.getElementById('casella').value='S1voce7';myOpacity1.toggle();" />  <img id="imgElencoS1V7" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce7').value+'&V='+document.getElementById('S1Op7').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce7" runat="server" />
                                    <asp:HiddenField ID="S1Op7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto7Sotto10" runat="server" />
                                </td>
                            </tr>    
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod8" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce8" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V8" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce8').value,'1.08','S1voce8'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V8" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce8').value;document.getElementById('casella').value='S1voce8';myOpacity1.toggle();" />  <img id="imgElencoS1V8" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce8').value+'&V='+document.getElementById('S1Op8').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce8" runat="server" />
                                    <asp:HiddenField ID="S1Op8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto8Sotto10" runat="server" />
                                </td>
                            </tr>       
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod9" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce9" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V9" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce9').value,'1.09','S1voce9'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V9" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce9').value;document.getElementById('casella').value='S1voce9';myOpacity1.toggle();" />  <img id="imgElencoS1V9" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce9').value+'&V='+document.getElementById('S1Op9').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce9" runat="server" />
                                    <asp:HiddenField ID="S1Op9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto9Sotto10" runat="server" />
                                </td>
                            </tr>       
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod10" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce10" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V10" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce10').value,'1.10','S1voce10');"/></td>
<td><asp:ImageButton ID="AggOperatoriS1V10" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce10').value;document.getElementById('casella').value='S1voce10';myOpacity1.toggle();" />  <img id="imgElencoS1V10" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce10').value+'&V='+document.getElementById('S1Op10').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce10" runat="server" />
                                    <asp:HiddenField ID="S1Op10" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto10Sotto10" runat="server" />
                                </td>
                            </tr>   
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod11" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce11" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V11" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce11').value,'1.11','S1voce11'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V11" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce11').value;document.getElementById('casella').value='S1voce11';myOpacity1.toggle();" />  <img id="imgElencoS1V11" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce11').value+'&V='+document.getElementById('S1Op11').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce11" runat="server" />
                                    <asp:HiddenField ID="S1Op11" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto11Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod12" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce12" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V12" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce12').value,'1.12','S1voce12'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V12" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce12').value;document.getElementById('casella').value='S1voce12';myOpacity1.toggle();" />  <img id="imgElencoS1V12" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce12').value+'&V='+document.getElementById('S1Op12').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce12" runat="server" />
                                    <asp:HiddenField ID="S1Op12" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto12Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod13" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce13" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V13" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce13').value,'1.13','S1voce13');"/></td>
<td><asp:ImageButton ID="AggOperatoriS1V13" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce13').value;document.getElementById('casella').value='S1voce13';myOpacity1.toggle();" />  <img id="imgElencoS1V13" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce13').value+'&V='+document.getElementById('S1Op13').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce13" runat="server" />
                                    <asp:HiddenField ID="S1Op13" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto13Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod14" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce14" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V14" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce14').value,'1.14','S1voce14'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V14" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce14').value;document.getElementById('casella').value='S1voce14';myOpacity1.toggle();" />  <img id="imgElencoS1V14" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce14').value+'&V='+document.getElementById('S1Op14').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce14" runat="server" />
                                    <asp:HiddenField ID="S1Op14" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto14Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod15" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce15" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V15" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce15').value,'1.15','S1voce15'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V15" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce15').value;document.getElementById('casella').value='S1voce15';myOpacity1.toggle();" />  <img id="imgElencoS1V15" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce15').value+'&V='+document.getElementById('S1Op15').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce15" runat="server" />
                                    <asp:HiddenField ID="S1Op15" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto15Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod16" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce16" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V16" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce16').value,'1.16','S1voce16'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V16" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce16').value;document.getElementById('casella').value='S1voce16';myOpacity1.toggle();" />  <img id="imgElencoS1V16" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce16').value+'&V='+document.getElementById('S1Op16').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce16" runat="server" />
                                    <asp:HiddenField ID="S1Op16" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto16Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod17" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce17" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V17" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce17').value,'1.17','S1voce17');"/></td>
<td><asp:ImageButton ID="AggOperatoriS1V17" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce17').value;document.getElementById('casella').value='S1voce17';myOpacity1.toggle();" />  <img id="imgElencoS1V17" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce17').value+'&V='+document.getElementById('S1Op17').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce17" runat="server" />
                                    <asp:HiddenField ID="S1Op17" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto17Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod18" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce18" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V18" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce18').value,'1.18','S1voce18'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS1V18" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce18').value;document.getElementById('casella').value='S1voce18';myOpacity1.toggle();" />  <img id="imgElencoS1V18" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce18').value+'&V='+document.getElementById('S1Op18').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce18" runat="server" />
                                    <asp:HiddenField ID="S1Op18" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto18Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod19" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce19" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V19" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce19').value,'1.19','S1voce19');"/></td>
<td><asp:ImageButton ID="AggOperatoriS1V19" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce19').value;document.getElementById('casella').value='S1voce19';myOpacity1.toggle();" />  <img id="imgElencoS1V19" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce19').value+'&V='+document.getElementById('S1Op19').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce19" runat="server" />
                                    <asp:HiddenField ID="S1Op19" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto19Sotto10" runat="server" />
                                </td>
                            </tr> 
                            
                                                                                    <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S1txtCod20" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S1txtVoce20" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS1V20" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S1txtVoce20').value,'1.20','S1voce20');"/></td>
<td><asp:ImageButton ID="AggOperatoriS1V20" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S1txtVoce20').value;document.getElementById('casella').value='S1voce20';myOpacity1.toggle();" />  <img id="imgElencoS1V20" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S1txtVoce20').value+'&V='+document.getElementById('S1Op20').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S1voce20" runat="server" />
                                    <asp:HiddenField ID="S1Op20" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto1" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto2" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto3" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto4" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto5" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto6" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto7" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto8" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto9" runat="server" />
                                    <asp:HiddenField ID="S1Sotto20Sotto10" runat="server" />
                                </td>
                            </tr> 
                                
                            </table>
                            </div>
                                                    <table style="width:100%;">
                            <tr>
                                <td style="width: 15px">
                                    <a id="menuSEZ2" href="javascript:aprichiudi('SEZ2');">
    <img alt="img" src='Immagini/cartella.gif' width='16' height='16' hspace='0' vspace='0' 
        border='0'/></a></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <strong>2.0 SPESE FACILITY MANAGEMENT</strong></td>
                            </tr>
                            </table>
                            <div id="SEZ2" style="margin-left: 2em; display: none; ">
                            <table style="width:100%;">
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V1" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce1').value,'2.01','S2voce1'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V1" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce1').value;document.getElementById('casella').value='S2voce1';myOpacity1.toggle();" />  <img id="imgElencoS2V1" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce1').value+'&V='+document.getElementById('S2Op1').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce1" runat="server" />
                                    <asp:HiddenField ID="S2Op1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V2" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce2').value,'2.02','S2voce2'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V2" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce2').value;document.getElementById('casella').value='S2voce2';myOpacity1.toggle();" />  <img id="imgElencoS2V2" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce2').value+'&V='+document.getElementById('S2Op2').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce2" runat="server" />
                                    <asp:HiddenField ID="S2Op2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2" runat="server" />
                                </td>
                            </tr>    
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V3" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce3').value,'2.03','S2voce3'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V3" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce3').value;document.getElementById('casella').value='S2voce3';myOpacity1.toggle();" />  <img id="imgElencoS2V3" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce3').value+'&V='+document.getElementById('S2Op3').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce3" runat="server" />
                                    <asp:HiddenField ID="S2Op3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3" runat="server" />
                                </td>
                            </tr>      
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V4" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce4').value,'2.04','S2voce4');"/></td>
<td><asp:ImageButton ID="AggOperatoriS2V4" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce4').value;document.getElementById('casella').value='S2voce4';myOpacity1.toggle();" />  <img id="imgElencoS2V4" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce4').value+'&V='+document.getElementById('S2Op4').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce4" runat="server" />
                                    <asp:HiddenField ID="S2Op4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4" runat="server" />
                                </td>
                            </tr>          
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V5" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce5').value,'2.05','S2voce5'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V5" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce5').value;document.getElementById('casella').value='S2voce5';myOpacity1.toggle();" />  <img id="imgElencoS2V5" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce5').value+'&V='+document.getElementById('S2Op5').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce5" runat="server" />
                                    <asp:HiddenField ID="S2Op5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5" runat="server" />
                                </td>
                            </tr>   
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod6" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce6" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V6" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce6').value,'2.06','S2voce6'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V6" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce6').value;document.getElementById('casella').value='S2voce6';myOpacity1.toggle();" />  <img id="imgElencoS2V6" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce6').value+'&V='+document.getElementById('S2Op6').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce6" runat="server" />
                                    <asp:HiddenField ID="S2Op6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6" runat="server" />
                                </td>
                            </tr>        
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod7" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce7" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V7" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce7').value,'2.07','S2voce7'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V7" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce7').value;document.getElementById('casella').value='S2voce7';myOpacity1.toggle();" />  <img id="imgElencoS2V7" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce7').value+'&V='+document.getElementById('S2Op7').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce7" runat="server" />
                                    <asp:HiddenField ID="S2Op7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7" runat="server" />
                                </td>
                            </tr>    
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod8" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce8" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V8" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce8').value,'2.08','S2voce8'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V8" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce8').value;document.getElementById('casella').value='S2voce8';myOpacity1.toggle();" />  <img id="imgElencoS2V8" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce8').value+'&V='+document.getElementById('S2Op8').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce8" runat="server" />
                                    <asp:HiddenField ID="S2Op8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8" runat="server" />
                                </td>
                            </tr>       
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod9" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce9" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V9" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce9').value,'2.09','S2voce9'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V9" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce9').value;document.getElementById('casella').value='S2voce9';myOpacity1.toggle();" />  <img id="imgElencoS2V9" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce9').value+'&V='+document.getElementById('S2Op9').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce9" runat="server" />
                                    <asp:HiddenField ID="S2Op9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9" runat="server" />
                                </td>
                            </tr>       
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod10" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce10" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V10" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce10').value,'2.10','S2voce10'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V10" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce10').value;document.getElementById('casella').value='S2voce10';myOpacity1.toggle();" />  <img id="imgElencoS2V10" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce10').value+'&V='+document.getElementById('S2Op10').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce10" runat="server" />
                                    <asp:HiddenField ID="S2Op10" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10" runat="server" />
                                </td>
                            </tr>       
                            
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod11" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce11" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V11" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce11').value,'2.11','S2voce11'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V11" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce11').value;document.getElementById('casella').value='S2voce11';myOpacity1.toggle();" />  <img id="imgElencoS2V11" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce11').value+'&V='+document.getElementById('S2Op11').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce11" runat="server" />
                                    <asp:HiddenField ID="S2Op11" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11" runat="server" />
                                </td>
                            </tr>       
                            
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod12" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce12" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V12" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce12').value,'2.12','S2voce12'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V12" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce12').value;document.getElementById('casella').value='S2voce12';myOpacity1.toggle();" />  <img id="imgElencoS2V12" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce12').value+'&V='+document.getElementById('S2Op12').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce12" runat="server" />
                                    <asp:HiddenField ID="S2Op12" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12" runat="server" />
                                </td>
                            </tr>       
                            
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod13" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce13" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V13" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce13').value,'2.13','S2voce13'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V13" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce13').value;document.getElementById('casella').value='S2voce13';myOpacity1.toggle();" />  <img id="imgElencoS2V13" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce13').value+'&V='+document.getElementById('S2Op13').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce13" runat="server" />
                                    <asp:HiddenField ID="S2Op13" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13" runat="server" />
                                </td>
                            </tr>       
                            
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod14" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce14" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V14" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce14').value,'2.14','S2voce14'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V14" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce14').value;document.getElementById('casella').value='S2voce14';myOpacity1.toggle();" />  <img id="imgElencoS2V14" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce14').value+'&V='+document.getElementById('S2Op14').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce14" runat="server" />
                                    <asp:HiddenField ID="S2Op14" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14" runat="server" />
                                </td>
                            </tr>       
                            
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod15" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce15" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V15" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce15').value,'2.15','S2voce15'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V15" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce15').value;document.getElementById('casella').value='S2voce15';myOpacity1.toggle();" />  <img id="imgElencoS2V15" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce15').value+'&V='+document.getElementById('S2Op15').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce15" runat="server" />
                                    <asp:HiddenField ID="S2Op15" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15" runat="server" />
                                </td>
                            </tr>       
                            
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod16" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce16" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V16" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce16').value,'2.16','S2voce16');"/></td>
<td><asp:ImageButton ID="AggOperatoriS2V16" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce16').value;document.getElementById('casella').value='S2voce16';myOpacity1.toggle();" />  <img id="imgElencoS2V16" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce16').value+'&V='+document.getElementById('S2Op16').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce16" runat="server" />
                                    <asp:HiddenField ID="S2Op16" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16" runat="server" />
                                </td>
                            </tr>       

                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod17" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce17" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V17" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce17').value,'2.17','S2voce17');"/></td>
<td><asp:ImageButton ID="AggOperatoriS2V17" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce17').value;document.getElementById('casella').value='S2voce17';myOpacity1.toggle();" />  <img id="imgElencoS2V17" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce17').value+'&V='+document.getElementById('S2Op17').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce17" runat="server" />
                                    <asp:HiddenField ID="S2Op17" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17" runat="server" />
                                </td>
                            </tr> 
                            
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod18" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce18" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V18" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce18').value,'2.18','S2voce18'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V18" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce18').value;document.getElementById('casella').value='S2voce18';myOpacity1.toggle();" />  <img id="imgElencoS2V18" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce18').value+'&V='+document.getElementById('S2Op18').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce18" runat="server" />
                                    <asp:HiddenField ID="S2Op18" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18" runat="server" />
                                </td>
                            </tr> 
                            
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod19" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce19" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V19" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce19').value,'2.19','S2voce19'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V19" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce19').value;document.getElementById('casella').value='S2voce19';myOpacity1.toggle();" />  <img id="imgElencoS2V19" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce19').value+'&V='+document.getElementById('S2Op19').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce19" runat="server" />
                                    <asp:HiddenField ID="S2Op19" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19" runat="server" />
                                </td>
                            </tr> 
                            
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S2txtCod20" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S2txtVoce20" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS2V20" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S2txtVoce20').value,'2.20','S2voce20'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS2V20" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S2txtVoce20').value;document.getElementById('casella').value='S2voce20';myOpacity1.toggle();" />  <img id="imgElencoS2V20" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S2txtVoce20').value+'&V='+document.getElementById('S2Op20').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S2voce20" runat="server" />
                                    <asp:HiddenField ID="S2Op20" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20" runat="server" />
                                </td>
                            </tr> 
                                                        
                            </table>

                            </div>
                             <table style="width:100%;">
                            <tr>
                                <td style="width: 15px">
                                    <a id="menuSEZ3" href="javascript:aprichiudi('SEZ3');">
    <img alt="img" src='Immagini/cartella.gif' width='16' height='16' hspace='0' vspace='0' 
        border='0'/></a></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <strong>3.0 SPESE CONTRIBUTI PER SOSTEGNO AGLI INQUILINI</strong></td>
                            </tr>
                            </table>
                            <div id="SEZ3" style="margin-left: 2em; display: none; ">
                            <table style="width:100%;">
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S3txtCod1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S3txtVoce1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS3V1" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S3txtVoce1').value,'3.01','S3voce1'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS3V1" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S3txtVoce1').value;document.getElementById('casella').value='S3voce1';myOpacity1.toggle();" />  <img id="imgElencoS3V1" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S3txtVoce1').value+'&V='+document.getElementById('S3Op1').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S3voce1" runat="server" />
                                    <asp:HiddenField ID="S3Op1" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S3txtCod2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S3txtVoce2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS3V2" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S3txtVoce2').value,'3.02','S3voce2'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS3V2" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S3txtVoce2').value;document.getElementById('casella').value='S3voce2';myOpacity1.toggle();" />  <img id="imgElencoS3V2" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S3txtVoce2').value+'&V='+document.getElementById('S3Op2').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S3voce2" runat="server" />
                                    <asp:HiddenField ID="S3Op2" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2" runat="server" />
                                </td>
                            </tr>    
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S3txtCod3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S3txtVoce3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS3V3" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S3txtVoce3').value,'3.03','S3voce3'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS3V3" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S3txtVoce3').value;document.getElementById('casella').value='S3voce3';myOpacity1.toggle();" />  <img id="imgElencoS3V3" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S3txtVoce3').value+'&V='+document.getElementById('S3Op3').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S3voce3" runat="server" />
                                    <asp:HiddenField ID="S3Op3" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3" runat="server" />
                                </td>
                            </tr>      
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S3txtCod4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S3txtVoce4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS3V4" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S3txtVoce4').value,'3.04','S3voce4');"/></td>
<td><asp:ImageButton ID="AggOperatoriS3V4" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S3txtVoce4').value;document.getElementById('casella').value='S3voce4';myOpacity1.toggle();" />  <img id="imgElencoS3V4" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S3txtVoce4').value+'&V='+document.getElementById('S3Op4').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S3voce4" runat="server" />
                                    <asp:HiddenField ID="S3Op4" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4" runat="server" />
                                </td>
                            </tr>          
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S3txtCod5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S3txtVoce5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="550px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS3V5" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S3txtVoce5').value,'3.05','S3voce5'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS3V5" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S3txtVoce5').value;document.getElementById('casella').value='S3voce5';myOpacity1.toggle();" />  <img id="imgElencoS3V5" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S3txtVoce5').value+'&V='+document.getElementById('S3Op5').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S3voce5" runat="server" />
                                    <asp:HiddenField ID="S3Op5" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5" runat="server" />
                                </td>
                            </tr>   
                                                           
                            </table>
                            </div>
                            <table style="width:100%;">
                            <tr>
                                <td style="width: 15px">
                                    <a id="menuSEZ4" href="javascript:aprichiudi('SEZ4');">
    <img alt="img" src='Immagini/cartella.gif' width='16' height='16' hspace='0' vspace='0' 
        border='0'/></a></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <strong>4.0 SPESE DIVERSE</strong></td>
                            </tr>
                            </table>
                            <div id="SEZ4" style="margin-left: 2em; display: none; ">
                            <table style="width:100%;">
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S4txtCod1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S4txtVoce1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="600px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img id="AggVociS4V1" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S4txtVoce1').value,'4.01','S4voce1'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS4V1" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S4txtVoce1').value;document.getElementById('casella').value='S4voce1';myOpacity1.toggle();" />  <img id="imgElencoS4V1" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S4txtVoce1').value+'&V='+document.getElementById('S4Op1').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S4voce1" runat="server" />
                                    <asp:HiddenField ID="S4Op1" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S4txtCod2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S4txtVoce2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="600px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img  id="AggVociS4V2" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S4txtVoce2').value,'4.02','S4voce2');"/></td>
<td><asp:ImageButton ID="AggOperatoriS4V2" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S4txtVoce2').value;document.getElementById('casella').value='S4voce2';myOpacity1.toggle();" />  <img id="imgElencoS4V2" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S4txtVoce2').value+'&V='+document.getElementById('S4Op2').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S4voce2" runat="server" />
                                    <asp:HiddenField ID="S4Op2" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2" runat="server" />
                                </td>
                            </tr>    
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S4txtCod3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S4txtVoce3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="600px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img  id="AggVociS4V3" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S4txtVoce3').value,'4.03','S4voce3');"/></td>
<td><asp:ImageButton ID="AggOperatoriS4V3" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S4txtVoce3').value;document.getElementById('casella').value='S4voce3';myOpacity1.toggle();" />  <img id="imgElencoS4V3" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S4txtVoce3').value+'&V='+document.getElementById('S4Op3').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S4voce3" runat="server" />
                                    <asp:HiddenField ID="S4Op3" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3" runat="server" />
                                </td>
                            </tr>      
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S4txtCod4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S4txtVoce4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="600px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img  id="AggVociS4V4" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
        border='0' style="cursor: pointer" onclick="SottoVoci(document.getElementById('S4txtVoce4').value,'4.04','S4voce4'); "/></td>
<td><asp:ImageButton ID="AggOperatoriS4V4" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S4txtVoce4').value;document.getElementById('casella').value='S4voce4';myOpacity1.toggle();" />  <img id="imgElencoS4V4" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S4txtVoce4').value+'&V='+document.getElementById('S4Op4').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S4voce4" runat="server" />
                                    <asp:HiddenField ID="S4Op4" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4" runat="server" />
                                </td>
                            </tr>          
                                                        <tr>
                                <td style="width: 15px; font-family: arial; font-size: 10pt;">
                                    <asp:TextBox ID="S4txtCod5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="50px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 500px;">
                                    <asp:TextBox ID="S4txtVoce5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="300" ToolTip="Descrizione Voce" Width="600px"></asp:TextBox></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <img  id="AggVociS4V5" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' border='0' style="cursor: pointer" 
                                    onclick="SottoVoci(document.getElementById('S4txtVoce5').value,'4.05','S4voce5');"/></td>
<td><asp:ImageButton ID="AggOperatoriS4V5" runat="server" ImageUrl="Immagini/Operatori.png"
                        TabIndex="31" onclientclick="document.getElementById('testovoce').value=document.getElementById('S4txtVoce5').value;document.getElementById('casella').value='S4voce5';myOpacity1.toggle();" />  <img id="imgElencoS4V5" alt="Operatori associati a questa voce" src='Immagini/Operatori_Inseriti.png' hspace='0' vspace='0' border='0' style="cursor: pointer" onclick="window.open('ElencoOp.aspx?T='+document.getElementById('S4txtVoce5').value+'&V='+document.getElementById('S4Op5').value.replace(/\#/g,'x'),'Elenco','');"/></td>
                                <td style="font-family: ARIAL; font-size: 10pt; width: 20px;">
                                    <asp:HiddenField ID="S4voce5" runat="server" />
                                    <asp:HiddenField ID="S4Op5" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5" runat="server" />
                                </td>
                            </tr>   
                                                           
                            </table>
                            </div>
                    </div>

                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Image ID="imgOperatoriAss" runat="server" 
                        style="position:absolute; top: 516px; left: 182px; cursor:pointer" 
                        ImageUrl="~/CICLO_PASSIVO/CicloPassivo/Plan/Immagini/img_Operatori_Voci_Funzioni.png" 
                                                
                        
                        onclick="window.open('SitOperatori.aspx?IDP=' + document.getElementById('pianoF').value, 'Situazione', '');" 
                        ToolTip="Operatori/Voci/Funzioni"/>
                    <asp:Image ID="imgEventi" runat="server" 
                        style="position:absolute; top: 516px; left: 365px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Eventi_Grande.png" 
                        onclick="ConfermaEventi();"/>
                    <asp:Image ID="imgStampa" runat="server" 
                        style="position:absolute; top: 516px; left: 467px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" 
                        onclick="ConfermaStampa();" ToolTip="Stampa"/>
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 516px; left: 669px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <asp:HiddenField ID="idEF" runat="server" />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False"></asp:Label>
                    <br />
                    <asp:HiddenField ID="casella" runat="server" />
                    <asp:HiddenField ID="casellasottosottovoci" runat="server" />
                    <asp:HiddenField ID="codicescelto" runat="server" />
                    <asp:HiddenField ID="codicescelto1" runat="server" />
                    <asp:HiddenField ID="per" runat="server" />
                    <br />
                    <asp:HiddenField ID="visualizza" runat="server" Value="0" />
                    <asp:HiddenField ID="statop" runat="server" Value="" />
                    <asp:HiddenField ID="standard" runat="server" Value="1" />
                    <asp:HiddenField ID="testovoce" runat="server" Value="" />

                                                        <asp:HiddenField ID="S2Sotto1Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto1Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto2Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto2Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto3Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto3Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto4Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto4Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto5Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto5Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto6Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto6Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto7Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto7Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto8Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto8Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto9Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto9Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto10Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto10Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto11Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto11Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto12Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto12Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto13Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto13Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto14Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto14Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto15Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto15Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto16Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto16Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto17Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto17Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto18Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto18Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto19Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto19Sotto10" runat="server" />

                                    <asp:HiddenField ID="S2Sotto20Sotto1" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto2" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto3" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto4" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto5" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto6" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto7" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto8" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto9" runat="server" />
                                    <asp:HiddenField ID="S2Sotto20Sotto10" runat="server" />

                                                                        <asp:HiddenField ID="S3Sotto1Sotto1" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto2" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto3" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto4" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto5" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto6" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto7" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto8" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto9" runat="server" />
                                    <asp:HiddenField ID="S3Sotto1Sotto10" runat="server" />

                                    <asp:HiddenField ID="S3Sotto2Sotto1" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto2" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto3" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto4" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto5" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto6" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto7" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto8" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto9" runat="server" />
                                    <asp:HiddenField ID="S3Sotto2Sotto10" runat="server" />

                                    <asp:HiddenField ID="S3Sotto3Sotto1" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto2" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto3" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto4" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto5" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto6" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto7" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto8" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto9" runat="server" />
                                    <asp:HiddenField ID="S3Sotto3Sotto10" runat="server" />

                                    <asp:HiddenField ID="S3Sotto4Sotto1" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto2" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto3" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto4" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto5" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto6" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto7" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto8" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto9" runat="server" />
                                    <asp:HiddenField ID="S3Sotto4Sotto10" runat="server" />

                                    <asp:HiddenField ID="S3Sotto5Sotto1" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto2" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto3" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto4" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto5" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto6" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto7" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto8" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto9" runat="server" />
                                    <asp:HiddenField ID="S3Sotto5Sotto10" runat="server" />

                                     <asp:HiddenField ID="S4Sotto1Sotto1" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto2" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto3" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto4" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto5" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto6" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto7" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto8" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto9" runat="server" />
                                    <asp:HiddenField ID="S4Sotto1Sotto10" runat="server" />

                                    <asp:HiddenField ID="S4Sotto2Sotto1" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto2" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto3" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto4" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto5" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto6" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto7" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto8" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto9" runat="server" />
                                    <asp:HiddenField ID="S4Sotto2Sotto10" runat="server" />

                                    <asp:HiddenField ID="S4Sotto3Sotto1" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto2" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto3" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto4" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto5" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto6" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto7" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto8" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto9" runat="server" />
                                    <asp:HiddenField ID="S4Sotto3Sotto10" runat="server" />

                                    <asp:HiddenField ID="S4Sotto4Sotto1" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto2" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto3" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto4" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto5" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto6" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto7" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto8" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto9" runat="server" />
                                    <asp:HiddenField ID="S4Sotto4Sotto10" runat="server" />

                                    <asp:HiddenField ID="S4Sotto5Sotto1" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto2" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto3" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto4" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto5" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto6" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto7" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto8" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto9" runat="server" />
                                    <asp:HiddenField ID="S4Sotto5Sotto10" runat="server" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    <div>
    
                    <asp:ImageButton ID="ImgCompleto" runat="server" ImageUrl="~/NuoveImm/img_Schema_Completo.png"
                        Style="left: 12px; position: absolute; top: 516px; height: 20px;" 
                        TabIndex="31" onclientclick="ConfermaChiusura();" />
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                        Style="left: 574px; position: absolute; top: 516px; height: 20px;" 
                        TabIndex="31" onclientclick="VerificaDati('0');" ToolTip="Salva" />
    
    </div>
    <div id="InserimentoVoce"             
            
            
            style="left: 0px; width: 796px; position: absolute;
    top: 0px; height: 620px;  text-align: left; background-repeat: no-repeat; background-image: url('Immagini/SfondoDiv1.png'); z-index: 200; visibility: hidden;">
    <span style="font-family: Arial"></span><br />
    <br />
    <table cellpadding="0" cellspacing="0" 
        style="border-style: inherit; width: 646px; left: 77px; position: absolute; top: 78px; background-color: #FFFFFF; z-index: 200; height: 435px;" 
        border="0">
        <tr style="width: 100%">
            <td style="width: 100%; height: 19px; text-align: left">
                <strong><span style="font-family: Arial">Aggiungi Voci di Livello 1 a "
                <asp:Label ID="lblDecrVoce" runat="server"></asp:Label>"
                </span></strong></td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto1" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto1" runat="server" />
                        <img id="img1" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto1').value,document.getElementById('txtVoceSotto1').value,HVoceSotto1); "/>         
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto2" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto2" runat="server" />
                        <img id="img2" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto2').value,document.getElementById('txtVoceSotto2').value,HVoceSotto2); "/>      
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto3" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto3" runat="server" />
                        <img id="img3" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto3').value,document.getElementById('txtVoceSotto3').value,HVoceSotto3); "/>       
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto4" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto4" runat="server" />
                        <img id="img4" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto4').value,document.getElementById('txtVoceSotto4').value,HVoceSotto4); "/>         
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto5" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto5" runat="server" />
                        <img id="img5" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto5').value,document.getElementById('txtVoceSotto5').value,HVoceSotto5); "/>          
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto6" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto6" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto6" runat="server" />
                        <img id="img6" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto6').value,document.getElementById('txtVoceSotto6').value,HVoceSotto6); "/>          
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto7" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto7" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto7" runat="server" />
                        <img id="img7" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto7').value,document.getElementById('txtVoceSotto7').value,HVoceSotto7); "/>          
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto8" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto8" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto8" runat="server" />
                        <img id="img8" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto8').value,document.getElementById('txtVoceSotto8').value,HVoceSotto8); "/>         
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto9" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto9" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto9" runat="server" />
                        <img id="img9" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto9').value,document.getElementById('txtVoceSotto9').value,HVoceSotto9); "/>        
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSotto10" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSotto10" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox><asp:HiddenField ID="HVoceSotto10" runat="server" />
                        <img id="img10" alt="Aggiungi/Modifica Sotto Voci" src='Immagini/Aggiungi.png' width='16' height='16' hspace='0' vspace='0' 
                        border='0' style="cursor: pointer" onclick="SottoSottoVoci(document.getElementById('txtCodSotto10').value,document.getElementById('txtVoceSotto10').value,HVoceSotto10); "/>          
                        </td>
        </tr>
        
        
        <tr align="right" style="width: 100%">
            <td style="width: 100%; height: 19px; text-align: right;" align="right">
                <table border="0" cellpadding="1" cellspacing="1" width="width: 100%">
                    <tr>
                        <td style="text-align: right"><img id="imgInserisciSottoVoci" 
                                alt="" src="../../../NuoveImm/Img_InserisciVal.png" 
                                onclick="InserisciSottoVoci();" 
                                style="cursor: pointer"/>&nbsp;&nbsp; 
                            <img id="ImgChiudiSchema" 
                                alt="" src="../../../NuoveImm/Img_AnnullaVal.png" 
                                
                                onclick="document.getElementById('img1').style.visibility = 'hidden';document.getElementById('img2').style.visibility = 'hidden'; document.getElementById('img3').style.visibility = 'hidden';document.getElementById('img4').style.visibility = 'hidden';document.getElementById('img5').style.visibility = 'hidden';document.getElementById('img6').style.visibility = 'hidden';document.getElementById('img7').style.visibility = 'hidden';document.getElementById('img8').style.visibility = 'hidden';document.getElementById('img9').style.visibility = 'hidden';document.getElementById('img10').style.visibility = 'hidden'; myOpacity.toggle();" 
                                style="cursor: pointer"/></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   
</div>


<div id="DivInserimentoSottoVoci"          
            
            
            
            
            
            style="left: 0px; width: 796px; position: absolute;
    top: 0px; height: 620px;  text-align: left; background-repeat: no-repeat; background-image: url('Immagini/SfondoDiv1.png'); z-index: 200; visibility: hidden; clip: rect(auto, auto, auto, 0px);">
    <span style="font-family: Arial"></span><br />
    <br />
    <table cellpadding="0" cellspacing="0" 
        style="border-style: inherit; width: 646px; left: 77px; position: absolute; top: 78px; background-color: #FFFFFF; z-index: 200; height: 435px;" 
        border="0">
        <tr style="width: 100%">
            <td style="width: 100%; height: 19px; text-align: left">
                <strong><span style="font-family: Arial"><span class="style1">Aggiungi Voci di Livello 2 a "
                </span>
                <asp:Label ID="lblDescrSottoVoce" runat="server" CssClass="style1"></asp:Label>"
                </span></strong></td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto1" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto1" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>      
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto2" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto2" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>     
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto3" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto3" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>      
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto4" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto4" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>        
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto5" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto5" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto6" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto6" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto7" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto7" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto8" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto8" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto9" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto9" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>
                        </td>
        </tr>
        <tr style="width: 100%">
            <td style="width:100%; height: 15px; text-align: left">
                <asp:TextBox ID="txtCodSottoSotto10" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="10" ToolTip="Codice" Width="100px" ReadOnly="True"></asp:TextBox>          
                        &nbsp;<asp:TextBox ID="txtVoceSottoSotto10" runat="server" 
                    Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Voce" Width="460px"></asp:TextBox>
                        </td>
        </tr>
        
        
        <tr align="right" style="width: 100%">
            <td style="width: 100%; height: 19px; text-align: right;" align="right">
                <table border="0" cellpadding="1" cellspacing="1" width="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            &nbsp;<img id="imgInserisciSottoSottoVoci" 
                                alt="" src="../../../NuoveImm/Img_InserisciVal.png" 
                                
                                onclick="InserisciSottoSottoVoci();" 
                                style="cursor: pointer"/>&nbsp;&nbsp; <img id="Img21" 
                                alt="" src="../../../NuoveImm/Img_AnnullaVal.png" 
                                
                                onclick="myOpacity2.toggle();" 
                                style="cursor: pointer"/></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   
</div>

<div id="InserimentoOperatori" 
            style="left: 0px; width: 796px; position: absolute;
    top: 0px; height: 620px;  visibility: hidden; overflow: auto; background-repeat: no-repeat; background-image: url('Immagini/SfondoDiv.gif'); z-index: 200; clip: rect(auto, auto, auto, 0px);">
    <span style="font-family: Arial"></span><br />
    <br />
    <table cellpadding="1" cellspacing="1" 
        style="border-style: inherit; width: 411px; left: 195px; position: absolute; top: 78px; background-color: #FFFFFF; z-index: 200;" 
        border="0">
        <tr>
            <td style="width: 300px; height: 19px; text-align: left">
                <strong><span style="font-family: Arial">Operatori Abilitati (BP-Compilazione)</span></strong></td>
        </tr>
        <tr>
            <td style="width: 300px; height: 19px; text-align: left">
                <strong><span style="font-family: Arial">
                <asp:Label ID="lblTestoVoce" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt"></asp:Label>
                </span></strong></td>
        </tr>
        <tr>
            <td style="width: 300px; height: 19px">
                <br />
                <br />
                <br />
                <br />
                <br />
                <div id="Div2" 
                    
                    style="overflow: auto; position: absolute; top: 53px; left: 10px; width: 388px; height: 335px;">
                    <asp:CheckBoxList ID="ListaOperatori" runat="server" Font-Names="arial" 
                        Font-Size="9pt">
                    </asp:CheckBoxList>
                </div>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 300px; height: 19px; text-align: right;" align="right">
                <table border="0" cellpadding="1" cellspacing="1" style="width: 100%">
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="imgInserisciOperatore" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png" 
                                ToolTip="Inserisci la nuova voce nello schema" TabIndex="704" 
                                
                                
                                onclientclick="document.getElementById('visualizza').value ='0';document.getElementById('Modificato').value='1';" />&nbsp;<img id="Img1" 
                                alt="" src="../../../NuoveImm/Img_AnnullaVal.png" 
                                
                                onclick="document.getElementById('visualizza').value ='0';myOpacity1.toggle();Sezione();" 
                                style="cursor: pointer"/></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
   

</div>

       <script type="text/javascript">
           myOpacity = new fx.Opacity('InserimentoVoce', { duration: 200 });
           myOpacity.hide();

           myOpacity2 = new fx.Opacity('DivInserimentoSottoVoci', { duration: 200 });
           myOpacity2.hide();

           myOpacity1 = new fx.Opacity('InserimentoOperatori', { duration: 200 });
           if (document.getElementById('visualizza').value == '0') {
               myOpacity1.hide();
           }
           
        </script>
        
<asp:HiddenField ID="apri" runat="server" />
<asp:HiddenField ID="pianoF" runat="server" />
<asp:HiddenField ID="Modificato" runat="server" Value="0" />
    <script type="text/javascript">

        Nascondi();
        if (document.getElementById('standard').value == '1') {
            NascondiStandard();
        }

        if (document.getElementById('apri').value != '') {
            aprichiudi(document.getElementById('apri').value);
        }



        function ConfermaEsci() {

            if (document.getElementById('Modificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente e perdere le modifiche effettuate?");
                if (chiediConferma == true) {
                    document.location.href = '../../pagina_home.aspx';
                }
            }
            else {
                document.location.href = '../../pagina_home.aspx';
            }
        }


        function VerificaDati(tipo) {

            var Messaggio;

            Messaggio = '';
            document.getElementById('salvaok').value = '0';

            if (document.getElementById('S1txtVoce1').value != '') {
                if (document.getElementById('S1Op1').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  1!\n';

                }
                if (document.getElementById('S1Sotto1').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  1!\n';
                }
            }

            if (document.getElementById('S1txtVoce2').value != '') {
                if (document.getElementById('S1Op2').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  2!\n';
                }
                if (document.getElementById('S1Sotto2').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  2!\n';
                }
            }

            if (document.getElementById('S1txtVoce3').value != '') {
                if (document.getElementById('S1Op3').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  3!\n';
                }
                if (document.getElementById('S1Sotto3').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  3!\n';
                }
            }

            if (document.getElementById('S1txtVoce4').value != '') {
                if (document.getElementById('S1Op4').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  4!\n';
                }
                if (document.getElementById('S1Sotto4').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  4!\n';
                }
            }

            if (document.getElementById('S1txtVoce5').value != '') {
                if (document.getElementById('S1Op5').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  5!\n';
                }
                if (document.getElementById('S1Sotto5').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  5!\n';
                }
            }

            if (document.getElementById('S1txtVoce6').value != '') {
                if (document.getElementById('S1Op6').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  6!\n';
                }
                if (document.getElementById('S1Sotto6').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  6!\n';
                }
            }

            if (document.getElementById('S1txtVoce7').value != '') {
                if (document.getElementById('S1Op7').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  7!\n';
                }
                if (document.getElementById('S1Sotto7').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  7!\n';
                }
            }

            if (document.getElementById('S1txtVoce8').value != '') {
                if (document.getElementById('S1Op8').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  8!\n';
                }
                if (document.getElementById('S1Sotto8').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  8!\n';
                }
            }

            if (document.getElementById('S1txtVoce9').value != '') {
                if (document.getElementById('S1Op9').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  9!\n';
                }
                if (document.getElementById('S1Sotto9').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  9!\n';
                }
            }

            if (document.getElementById('S1txtVoce10').value != '') {
                if (document.getElementById('S1Op10').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  10!\n';
                }
                if (document.getElementById('S1Sotto10').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  10!\n';
                }
            }

            if (document.getElementById('S1txtVoce11').value != '') {
                if (document.getElementById('S1Op11').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  11!\n';
                }
                if (document.getElementById('S1Sotto11').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  11!\n';
                }
            }

            if (document.getElementById('S1txtVoce12').value != '') {
                if (document.getElementById('S1Op12').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  12!\n';
                }
                if (document.getElementById('S1Sotto12').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  12!\n';
                }
            }

            if (document.getElementById('S1txtVoce13').value != '') {
                if (document.getElementById('S1Op13').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  13!\n';
                }
                if (document.getElementById('S1Sotto13').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  13!\n';
                }
            }

            if (document.getElementById('S1txtVoce14').value != '') {
                if (document.getElementById('S1Op14').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella 14!\n';
                }
                if (document.getElementById('S1Sotto14').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  14!\n';
                }
            }

            if (document.getElementById('S1txtVoce15').value != '') {
                if (document.getElementById('S1Op15').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  15!\n';
                }
                if (document.getElementById('S1Sotto15').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  15!\n';
                }
            }

            if (document.getElementById('S1txtVoce16').value != '') {
                if (document.getElementById('S1Op16').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  16!\n';
                }
                if (document.getElementById('S1Sotto16').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  16!\n';
                }
            }

            if (document.getElementById('S1txtVoce17').value != '') {
                if (document.getElementById('S1Op17').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  17!\n';
                }
                if (document.getElementById('S1Sotto17').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  17!\n';
                }
            }

            if (document.getElementById('S1txtVoce18').value != '') {
                if (document.getElementById('S1Op18').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  18!\n';
                }
                if (document.getElementById('S1Sotto18').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  18!\n';
                }
            }

            if (document.getElementById('S1txtVoce19').value != '') {
                if (document.getElementById('S1Op19').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  19!\n';
                }
                if (document.getElementById('S1Sotto19').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  19!\n';
                }
            }

            if (document.getElementById('S1txtVoce20').value != '') {
                if (document.getElementById('S1Op20').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 1 casella  20!\n';
                }
                if (document.getElementById('S1Sotto20').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 1 casella  20!\n';
                }
            }

            if (document.getElementById('S2txtVoce1').value != '') {
                if (document.getElementById('S2Op1').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  1!\n';
                }
                if (document.getElementById('S2Sotto1').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  1!\n';
                }
            }

            if (document.getElementById('S2txtVoce2').value != '') {
                if (document.getElementById('S2Op2').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  2!\n';
                }
                if (document.getElementById('S2Sotto2').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  2!\n';
                }
            }

            if (document.getElementById('S2txtVoce3').value != '') {
                if (document.getElementById('S2Op3').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  3!\n';
                }
                if (document.getElementById('S2Sotto3').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  3!\n';
                }
            }

            if (document.getElementById('S2txtVoce4').value != '') {
                if (document.getElementById('S2Op4').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  4!\n';
                }
                if (document.getElementById('S2Sotto4').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  4!\n';
                }
            }

            if (document.getElementById('S2txtVoce5').value != '') {
                if (document.getElementById('S2Op5').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  5!\n';
                }
                if (document.getElementById('S2Sotto5').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  5!\n';
                }
            }

            if (document.getElementById('S2txtVoce6').value != '') {
                if (document.getElementById('S2Op6').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  6!\n';
                }
                if (document.getElementById('S2Sotto6').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  6!\n';
                }
            }

            if (document.getElementById('S2txtVoce7').value != '') {
                if (document.getElementById('S2Op7').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  7!\n';
                }
                if (document.getElementById('S2Sotto7').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  7!\n';
                }
            }


            if (document.getElementById('S2txtVoce8').value != '') {
                if (document.getElementById('S2Op8').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  8!\n';
                }
                if (document.getElementById('S2Sotto8').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  8!\n';
                }
            }

            if (document.getElementById('S2txtVoce9').value != '') {
                if (document.getElementById('S2Op9').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  9!\n';
                }
                if (document.getElementById('S2Sotto9').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  9!\n';
                }
            }

            if (document.getElementById('S2txtVoce10').value != '') {
                if (document.getElementById('S2Op10').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  10!\n';
                }
                if (document.getElementById('S2Sotto10').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  10!\n';
                }
            }

            if (document.getElementById('S2txtVoce11').value != '') {
                if (document.getElementById('S2Op11').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  11!\n';
                }
                if (document.getElementById('S2Sotto11').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  11!\n';
                }
            }

            if (document.getElementById('S2txtVoce12').value != '') {
                if (document.getElementById('S2Op12').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  12!\n';
                }
                if (document.getElementById('S2Sotto12').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  12!\n';
                }
            }

            if (document.getElementById('S2txtVoce13').value != '') {
                if (document.getElementById('S2Op13').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  13!\n';
                }
                if (document.getElementById('S2Sotto13').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  13!\n';
                }
            }

            if (document.getElementById('S2txtVoce14').value != '') {
                if (document.getElementById('S2Op14').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  14!\n';
                }
                if (document.getElementById('S2Sotto14').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  14!\n';
                }
            }

            if (document.getElementById('S2txtVoce15').value != '') {
                if (document.getElementById('S2Op15').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  15!\n';
                }
                if (document.getElementById('S2Sotto15').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  15!\n';
                }
            }

            if (document.getElementById('S2txtVoce16').value != '') {
                if (document.getElementById('S2Op16').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  16!\n';
                }
                if (document.getElementById('S2Sotto16').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  16!\n';
                }
            }

            if (document.getElementById('S2txtVoce17').value != '') {
                if (document.getElementById('S2Op17').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  17!\n';
                }
                if (document.getElementById('S2Sotto17').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  17!\n';
                }
            }

            if (document.getElementById('S2txtVoce18').value != '') {
                if (document.getElementById('S2Op18').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  18!\n';
                }
                if (document.getElementById('S2Sotto18').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  18!\n';
                }
            }

            if (document.getElementById('S2txtVoce19').value != '') {
                if (document.getElementById('S2Op19').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  19!\n';
                }
                if (document.getElementById('S2Sotto19').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  19!\n';
                }
            }

            if (document.getElementById('S2txtVoce20').value != '') {
                if (document.getElementById('S2Op20').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 2 casella  20!\n';
                }
                if (document.getElementById('S2Sotto20').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 2 casella  20!\n';
                }
            }

            if (document.getElementById('S3txtVoce1').value != '') {
                if (document.getElementById('S3Op1').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 3 casella  1!\n';
                }

                if (document.getElementById('S3Sotto1').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 3 casella  1!\n';
                }


            }

            if (document.getElementById('S3txtVoce2').value != '') {
                if (document.getElementById('S3Op2').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 3 casella  2!\n';
                }
                if (document.getElementById('S3Sotto2').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 3 casella  2!\n';
                }
            }

            if (document.getElementById('S3txtVoce3').value != '') {
                if (document.getElementById('S3Op3').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 3 casella  3!\n';
                }
                if (document.getElementById('S3Sotto3').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 3 casella  1!\n';
                }
            }

            if (document.getElementById('S3txtVoce4').value != '') {
                if (document.getElementById('S3Op4').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 3 casella  4!\n';
                }
                if (document.getElementById('S3Sotto4').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 3 casella  4!\n';
                }
            }

            if (document.getElementById('S3txtVoce5').value != '') {
                if (document.getElementById('S3Op5').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 3 casella  5!\n';
                }
                if (document.getElementById('S3Sotto5').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 3 casella  5!\n';
                }
            }

            if (document.getElementById('S4txtVoce1').value != '') {
                if (document.getElementById('S4Op1').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 4 casella  1!\n';
                }
                if (document.getElementById('S4Sotto1').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 4 casella  1!\n';
                }
            }

            if (document.getElementById('S4txtVoce2').value != '') {
                if (document.getElementById('S4Op2').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 4 casella  2!\n';
                }
                if (document.getElementById('S4Sotto2').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 4 casella  2!\n';
                }
            }

            if (document.getElementById('S4txtVoce3').value != '') {
                if (document.getElementById('S4Op3').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 4 casella  3!\n';
                }
                if (document.getElementById('S4Sotto3').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 4 casella  3!\n';
                }
            }

            if (document.getElementById('S4txtVoce4').value != '') {
                if (document.getElementById('S4Op4').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 4 casella  4!\n';
                }
                if (document.getElementById('S4Sotto4').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 4 casella  4!\n';
                }
            }

            if (document.getElementById('S4txtVoce5').value != '') {
                if (document.getElementById('S4Op5').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare gli operatori alla SEZIONE 4 casella  5!\n';
                }
                if (document.getElementById('S4Sotto5').value == '') {
                    Messaggio = Messaggio + 'Attenzione...Assegnare almeno una sotto voce alla SEZIONE 4 casella  5!\n';
                }
            }






            if (Messaggio != '') {
                if (tipo == '0') {
                    alert(Messaggio + 'Salvataggio effettuato comunque!');
                    document.getElementById('salvaok').value = '1';
                }
                else {
                    alert(Messaggio + 'Operazione non Effettuata!');
                    document.getElementById('salvaok').value = '0';
                }

            }
            else {
                document.getElementById('salvaok').value = '1';
            }

        }

        function ConfermaChiusura() {
            if (document.getElementById('Modificato').value == '1') {
                alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!');
                document.getElementById('salvaok').value = '0';
            }
            else {
                VerificaDati('1');
                if (document.getElementById('salvaok').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sei sicuro di voler concludere la fase di inserimento dello schema Piano Finanziario?\nIn caso affermativo, gli operatori abilitati potranno iniziare a insererire gli importi.");
                    if (chiediConferma == false) {
                        document.getElementById('salvaok').value = '0';
                    }
                }

            }
        }


        function ConfermaStampa() {
            if (document.getElementById('Modificato').value == '1') {
                alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!');

            }
            else {
                VerificaDati('1');
                if (document.getElementById('salvaok').value == '1') {
                    window.open('StampaPF.aspx?T=0&ID=' + document.getElementById('pianoF').value + '&P=' + document.getElementById('per').value, 'Stampa', '');
                }

            }
        }


        function ConfermaEventi() {
            if (document.getElementById('Modificato').value == '1') {
                alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!');

            }
            else {
                VerificaDati('1');
                if (document.getElementById('salvaok').value == '1') {
                    window.open('EventiPF.aspx?ID=' + document.getElementById('pianoF').value + '&P=' + document.getElementById('per').value, 'Eventi', '');
                }

            }
        }


        function CaricaListaOperatori() {

            if (document.getElementById('casella').value == 'S1voce1') {
                if (document.getElementById('S1Op1').value != '') {
                    alert(document.getElementById('S1Op1').value);
                    var i = 0;
                    var modulo = document.form1.elements;
                    for (i = 0; i < modulo.length; i++) {
                        if (modulo[i].type == "checkbox") {

                            if (modulo[i].value == 'on') {
                                //alert(modulo[i].text);
                                modulo[i].checked = true;
                            }

                        }
                    }
                }
            }
        }

        function Sezione() {
            var str = document.getElementById('casella').value;

            switch (str.substr(0, 2)) {
                case 'S1':
                    document.getElementById('apri').value = 'SEZ1';
                    break;
                case 'S2':
                    document.getElementById('apri').value = 'SEZ2';
                    break;
                case 'S3':
                    document.getElementById('apri').value = 'SEZ3';
                    break;
                case 'S4':
                    document.getElementById('apri').value = 'SEZ4';
                    break;
            }

            if (document.getElementById('apri').value != '') {
                aprichiudi(document.getElementById('apri').value);
            }

        }



        if (document.getElementById('S1Op1').value == '') {
            document.getElementById('imgElencoS1V1').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op2').value == '') {
            document.getElementById('imgElencoS1V2').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op3').value == '') {
            document.getElementById('imgElencoS1V3').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op4').value == '') {
            document.getElementById('imgElencoS1V4').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op5').value == '') {
            document.getElementById('imgElencoS1V5').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op6').value == '') {
            document.getElementById('imgElencoS1V6').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op7').value == '') {
            document.getElementById('imgElencoS1V7').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op8').value == '') {
            document.getElementById('imgElencoS1V8').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op9').value == '') {
            document.getElementById('imgElencoS1V9').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op10').value == '') {
            document.getElementById('imgElencoS1V10').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op11').value == '') {
            document.getElementById('imgElencoS1V11').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op12').value == '') {
            document.getElementById('imgElencoS1V12').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op13').value == '') {
            document.getElementById('imgElencoS1V13').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op14').value == '') {
            document.getElementById('imgElencoS1V14').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op15').value == '') {
            document.getElementById('imgElencoS1V15').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op16').value == '') {
            document.getElementById('imgElencoS1V16').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op17').value == '') {
            document.getElementById('imgElencoS1V17').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op18').value == '') {
            document.getElementById('imgElencoS1V18').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op19').value == '') {
            document.getElementById('imgElencoS1V19').style.visibility = 'hidden';
        }
        if (document.getElementById('S1Op20').value == '') {
            document.getElementById('imgElencoS1V20').style.visibility = 'hidden';
        }


        if (document.getElementById('S2Op1').value == '') {
            document.getElementById('imgElencoS2V1').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op2').value == '') {
            document.getElementById('imgElencoS2V2').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op3').value == '') {
            document.getElementById('imgElencoS2V3').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op4').value == '') {
            document.getElementById('imgElencoS2V4').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op5').value == '') {
            document.getElementById('imgElencoS2V5').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op6').value == '') {
            document.getElementById('imgElencoS2V6').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op7').value == '') {
            document.getElementById('imgElencoS2V7').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op8').value == '') {
            document.getElementById('imgElencoS2V8').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op9').value == '') {
            document.getElementById('imgElencoS2V9').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op10').value == '') {
            document.getElementById('imgElencoS2V10').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op11').value == '') {
            document.getElementById('imgElencoS2V11').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op12').value == '') {
            document.getElementById('imgElencoS2V12').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op13').value == '') {
            document.getElementById('imgElencoS2V13').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op14').value == '') {
            document.getElementById('imgElencoS2V14').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op15').value == '') {
            document.getElementById('imgElencoS2V15').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op16').value == '') {
            document.getElementById('imgElencoS2V16').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op17').value == '') {
            document.getElementById('imgElencoS2V17').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op18').value == '') {
            document.getElementById('imgElencoS2V18').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op19').value == '') {
            document.getElementById('imgElencoS2V19').style.visibility = 'hidden';
        }
        if (document.getElementById('S2Op20').value == '') {
            document.getElementById('imgElencoS2V20').style.visibility = 'hidden';
        }



        if (document.getElementById('S3Op1').value == '') {
            document.getElementById('imgElencoS3V1').style.visibility = 'hidden';
        }
        if (document.getElementById('S3Op2').value == '') {
            document.getElementById('imgElencoS3V2').style.visibility = 'hidden';
        }
        if (document.getElementById('S3Op3').value == '') {
            document.getElementById('imgElencoS3V3').style.visibility = 'hidden';
        }
        if (document.getElementById('S3Op4').value == '') {
            document.getElementById('imgElencoS3V4').style.visibility = 'hidden';
        }
        if (document.getElementById('S3Op5').value == '') {
            document.getElementById('imgElencoS3V5').style.visibility = 'hidden';
        }



        if (document.getElementById('S4Op1').value == '') {
            document.getElementById('imgElencoS4V1').style.visibility = 'hidden';
        }
        if (document.getElementById('S4Op2').value == '') {
            document.getElementById('imgElencoS4V2').style.visibility = 'hidden';
        }
        if (document.getElementById('S4Op3').value == '') {
            document.getElementById('imgElencoS4V3').style.visibility = 'hidden';
        }
        if (document.getElementById('S4Op4').value == '') {
            document.getElementById('imgElencoS4V4').style.visibility = 'hidden';
        }
        if (document.getElementById('S4Op5').value == '') {
            document.getElementById('imgElencoS4V5').style.visibility = 'hidden';
        }

         
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        function SottoSottoVoci(Codice,Descrizione,Voce) {
            if (Descrizione != '') {
                document.getElementById('codicescelto1').value = Codice;
                document.getElementById('lblDescrSottoVoce').innerText = Descrizione;

                document.getElementById('txtVoceSottoSotto1').value = '';
                document.getElementById('txtVoceSottoSotto2').value = '';
                document.getElementById('txtVoceSottoSotto3').value = '';
                document.getElementById('txtVoceSottoSotto4').value = '';
                document.getElementById('txtVoceSottoSotto5').value = '';
                document.getElementById('txtVoceSottoSotto6').value = '';
                document.getElementById('txtVoceSottoSotto7').value = '';
                document.getElementById('txtVoceSottoSotto8').value = '';
                document.getElementById('txtVoceSottoSotto9').value = '';
                document.getElementById('txtVoceSottoSotto10').value = '';

                document.getElementById('txtCodSottoSotto1').value = Codice + '.01';
                document.getElementById('txtCodSottoSotto2').value = Codice + '.02';
                document.getElementById('txtCodSottoSotto3').value = Codice + '.03';
                document.getElementById('txtCodSottoSotto4').value = Codice + '.04';
                document.getElementById('txtCodSottoSotto5').value = Codice + '.05';
                document.getElementById('txtCodSottoSotto6').value = Codice + '.06';
                document.getElementById('txtCodSottoSotto7').value = Codice + '.07';
                document.getElementById('txtCodSottoSotto8').value = Codice + '.08';
                document.getElementById('txtCodSottoSotto9').value = Codice + '.09';
                document.getElementById('txtCodSottoSotto10').value = Codice + '.10';

                var Valore
                var mioval

                switch (Codice.substr(5,7)) {
                    case '01':
                        Valore = document.getElementById('HVoceSotto1').value;
                        break;
                    case '02':
                        Valore = document.getElementById('HVoceSotto2').value;
                        break;
                    case '03':
                        Valore = document.getElementById('HVoceSotto3').value;
                        break;
                    case '04':
                        Valore = document.getElementById('HVoceSotto4').value;
                        break;
                    case '05':
                        Valore = document.getElementById('HVoceSotto5').value;
                        break;
                    case '06':
                        Valore = document.getElementById('HVoceSotto6').value;
                        break;
                    case '07':
                        Valore = document.getElementById('HVoceSotto7').value;
                        break;
                    case '08':
                        Valore = document.getElementById('HVoceSotto8').value;
                        break;
                    case '09':
                        Valore = document.getElementById('HVoceSotto9').value;
                        break;
                    case '10':
                        Valore = document.getElementById('HVoceSotto10').value;
                        break;
                }


                if ((Valore != "") && (Valore != null)) {
                    if (Valore.indexOf("#") == -1) {
                        document.getElementById('txtVoceSottoSotto1').value = Valore;
                    }
                    else {
                        var Val_array = Valore.split("#");
                        var part_num = 0;
                        while (part_num < Val_array.length) {
                            if (part_num == 0) { document.getElementById('txtVoceSottoSotto1').value = Val_array[part_num] }
                            if (part_num == 1) { document.getElementById('txtVoceSottoSotto2').value = Val_array[part_num] }
                            if (part_num == 2) { document.getElementById('txtVoceSottoSotto3').value = Val_array[part_num] }
                            if (part_num == 3) { document.getElementById('txtVoceSottoSotto4').value = Val_array[part_num] }
                            if (part_num == 4) { document.getElementById('txtVoceSottoSotto5').value = Val_array[part_num] }
                            if (part_num == 5) { document.getElementById('txtVoceSottoSotto6').value = Val_array[part_num] }
                            if (part_num == 6) { document.getElementById('txtVoceSottoSotto7').value = Val_array[part_num] }
                            if (part_num == 7) { document.getElementById('txtVoceSottoSotto8').value = Val_array[part_num] }
                            if (part_num == 8) { document.getElementById('txtVoceSottoSotto9').value = Val_array[part_num] }
                            if (part_num == 9) { document.getElementById('txtVoceSottoSotto10').value = Val_array[part_num] }
                            part_num += 1;
                        }
                    }
                }

                document.getElementById('casellasottosottovoci').value = Voce;

                myOpacity2.toggle();
            }
            else {
                alert('Inserire una descrizione per la voce codice ' + Codice + ' prima di inserire sotto voci!');
            }
        }


        function ValorizzaSottoSotto(v1, v2, v3, v4, v5, v6, v7, v8, v9, v10) {
            //alert(document.getElementById('HVoceSotto1').value);
            v1.value = document.getElementById('HVoceSotto1').value;
            v2.value = document.getElementById('HVoceSotto2').value;
            v3.value = document.getElementById('HVoceSotto3').value;
            v4.value = document.getElementById('HVoceSotto4').value;
            v5.value = document.getElementById('HVoceSotto5').value;
            v6.value = document.getElementById('HVoceSotto6').value;
            v7.value = document.getElementById('HVoceSotto7').value;
            v8.value = document.getElementById('HVoceSotto8').value;
            v9.value = document.getElementById('HVoceSotto9').value;
            v10.value = document.getElementById('HVoceSotto10').value;
        }


        function InserisciSottoVoci() {
         var i;
         var TestoDaScrivere;

         TestoDaScrivere = '';

         if (document.getElementById('txtVoceSotto1').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto1').value + '#';
         }
         if (document.getElementById('txtVoceSotto2').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto2').value + '#';
         }
         if (document.getElementById('txtVoceSotto3').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto3').value + '#';
         }
         if (document.getElementById('txtVoceSotto4').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto4').value + '#';
         }
         if (document.getElementById('txtVoceSotto5').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto5').value + '#';
         }
         if (document.getElementById('txtVoceSotto6').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto6').value + '#';
         }
         if (document.getElementById('txtVoceSotto7').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto7').value + '#';
         }
         if (document.getElementById('txtVoceSotto8').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto8').value + '#';
         }
         if (document.getElementById('txtVoceSotto9').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto9').value + '#';
         }
         if (document.getElementById('txtVoceSotto10').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSotto10').value + '#';
        }

        
        if (TestoDaScrivere != '') {
            TestoDaScrivere = TestoDaScrivere.substr(0, TestoDaScrivere.length - 1);
        }

        //MAX
        
        switch (document.getElementById('casella').value) {
            case 'S1voce1':
                document.getElementById('S1Sotto1').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto1Sotto1'),document.getElementById('S1Sotto1Sotto2'),document.getElementById('S1Sotto1Sotto3'),document.getElementById('S1Sotto1Sotto4'),document.getElementById('S1Sotto1Sotto5'),document.getElementById('S1Sotto1Sotto6'),document.getElementById('S1Sotto1Sotto7'),document.getElementById('S1Sotto1Sotto8'),document.getElementById('S1Sotto1Sotto9'),document.getElementById('S1Sotto1Sotto10'));
                break;
            case 'S1voce2':
                document.getElementById('S1Sotto2').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto2Sotto1'), document.getElementById('S1Sotto2Sotto2'), document.getElementById('S1Sotto2Sotto3'), document.getElementById('S1Sotto2Sotto4'), document.getElementById('S1Sotto2Sotto5'), document.getElementById('S1Sotto2Sotto6'), document.getElementById('S1Sotto2Sotto7'), document.getElementById('S1Sotto2Sotto8'), document.getElementById('S1Sotto2Sotto9'), document.getElementById('S1Sotto2Sotto10'));
                break;
            case 'S1voce3':
                document.getElementById('S1Sotto3').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto3Sotto1'), document.getElementById('S1Sotto3Sotto2'), document.getElementById('S1Sotto3Sotto3'), document.getElementById('S1Sotto3Sotto4'), document.getElementById('S1Sotto3Sotto5'), document.getElementById('S1Sotto3Sotto6'), document.getElementById('S1Sotto3Sotto7'), document.getElementById('S1Sotto3Sotto8'), document.getElementById('S1Sotto3Sotto9'), document.getElementById('S1Sotto3Sotto10'));
                break;
            case 'S1voce4':
                document.getElementById('S1Sotto4').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto4Sotto1'), document.getElementById('S1Sotto4Sotto2'), document.getElementById('S1Sotto4Sotto3'), document.getElementById('S1Sotto4Sotto4'), document.getElementById('S1Sotto4Sotto5'), document.getElementById('S1Sotto4Sotto6'), document.getElementById('S1Sotto4Sotto7'), document.getElementById('S1Sotto4Sotto8'), document.getElementById('S1Sotto4Sotto9'), document.getElementById('S1Sotto4Sotto10'));
                break;
            case 'S1voce5':
                document.getElementById('S1Sotto5').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto5Sotto1'), document.getElementById('S1Sotto5Sotto2'), document.getElementById('S1Sotto5Sotto3'), document.getElementById('S1Sotto5Sotto4'), document.getElementById('S1Sotto5Sotto5'), document.getElementById('S1Sotto5Sotto6'), document.getElementById('S1Sotto5Sotto7'), document.getElementById('S1Sotto5Sotto8'), document.getElementById('S1Sotto5Sotto9'), document.getElementById('S1Sotto5Sotto10'));
                break;
            case 'S1voce6':
                document.getElementById('S1Sotto6').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto6Sotto1'), document.getElementById('S1Sotto6Sotto2'), document.getElementById('S1Sotto6Sotto3'), document.getElementById('S1Sotto6Sotto4'), document.getElementById('S1Sotto6Sotto5'), document.getElementById('S1Sotto6Sotto6'), document.getElementById('S1Sotto6Sotto7'), document.getElementById('S1Sotto6Sotto8'), document.getElementById('S1Sotto6Sotto9'), document.getElementById('S1Sotto6Sotto10'));
                break;
            case 'S1voce7':
                document.getElementById('S1Sotto7').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto7Sotto1'), document.getElementById('S1Sotto7Sotto2'), document.getElementById('S1Sotto7Sotto3'), document.getElementById('S1Sotto7Sotto4'), document.getElementById('S1Sotto7Sotto5'), document.getElementById('S1Sotto7Sotto6'), document.getElementById('S1Sotto7Sotto7'), document.getElementById('S1Sotto7Sotto8'), document.getElementById('S1Sotto7Sotto9'), document.getElementById('S1Sotto7Sotto10'));
                break;
            case 'S1voce8':
                document.getElementById('S1Sotto8').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto8Sotto1'), document.getElementById('S1Sotto8Sotto2'), document.getElementById('S1Sotto8Sotto3'), document.getElementById('S1Sotto8Sotto4'), document.getElementById('S1Sotto8Sotto5'), document.getElementById('S1Sotto8Sotto6'), document.getElementById('S1Sotto8Sotto7'), document.getElementById('S1Sotto8Sotto8'), document.getElementById('S1Sotto8Sotto9'), document.getElementById('S1Sotto8Sotto10'));
                break;
            case 'S1voce9':
                document.getElementById('S1Sotto9').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto9Sotto1'), document.getElementById('S1Sotto9Sotto2'), document.getElementById('S1Sotto9Sotto3'), document.getElementById('S1Sotto9Sotto4'), document.getElementById('S1Sotto9Sotto5'), document.getElementById('S1Sotto9Sotto6'), document.getElementById('S1Sotto9Sotto7'), document.getElementById('S1Sotto9Sotto8'), document.getElementById('S1Sotto9Sotto9'), document.getElementById('S1Sotto9Sotto10'));
                break;
            case 'S1voce10':
                document.getElementById('S1Sotto10').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto10Sotto1'), document.getElementById('S1Sotto10Sotto2'), document.getElementById('S1Sotto10Sotto3'), document.getElementById('S1Sotto10Sotto4'), document.getElementById('S1Sotto10Sotto5'), document.getElementById('S1Sotto10Sotto6'), document.getElementById('S1Sotto10Sotto7'), document.getElementById('S1Sotto10Sotto8'), document.getElementById('S1Sotto10Sotto9'), document.getElementById('S1Sotto10Sotto10'));
                break;
            case 'S1voce11':
                document.getElementById('S1Sotto11').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto11Sotto1'), document.getElementById('S1Sotto11Sotto2'), document.getElementById('S1Sotto11Sotto3'), document.getElementById('S1Sotto11Sotto4'), document.getElementById('S1Sotto11Sotto5'), document.getElementById('S1Sotto11Sotto6'), document.getElementById('S1Sotto11Sotto7'), document.getElementById('S1Sotto11Sotto8'), document.getElementById('S1Sotto11Sotto9'), document.getElementById('S1Sotto11Sotto10'));
                break;
            case 'S1voce12':
                document.getElementById('S1Sotto12').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto12Sotto1'), document.getElementById('S1Sotto12Sotto2'), document.getElementById('S1Sotto12Sotto3'), document.getElementById('S1Sotto12Sotto4'), document.getElementById('S1Sotto12Sotto5'), document.getElementById('S1Sotto12Sotto6'), document.getElementById('S1Sotto12Sotto7'), document.getElementById('S1Sotto12Sotto8'), document.getElementById('S1Sotto12Sotto9'), document.getElementById('S1Sotto12Sotto10'));
                break;
            case 'S1voce13':
                document.getElementById('S1Sotto13').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto13Sotto1'), document.getElementById('S1Sotto13Sotto2'), document.getElementById('S1Sotto13Sotto3'), document.getElementById('S1Sotto13Sotto4'), document.getElementById('S1Sotto13Sotto5'), document.getElementById('S1Sotto13Sotto6'), document.getElementById('S1Sotto13Sotto7'), document.getElementById('S1Sotto13Sotto8'), document.getElementById('S1Sotto13Sotto9'), document.getElementById('S1Sotto13Sotto10'));
                break;
            case 'S1voce14':
                document.getElementById('S1Sotto14').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto14Sotto1'), document.getElementById('S1Sotto14Sotto2'), document.getElementById('S1Sotto14Sotto3'), document.getElementById('S1Sotto14Sotto4'), document.getElementById('S1Sotto14Sotto5'), document.getElementById('S1Sotto14Sotto6'), document.getElementById('S1Sotto14Sotto7'), document.getElementById('S1Sotto14Sotto8'), document.getElementById('S1Sotto14Sotto9'), document.getElementById('S1Sotto14Sotto10'));
                break;
            case 'S1voce15':
                document.getElementById('S1Sotto15').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto15Sotto1'), document.getElementById('S1Sotto15Sotto2'), document.getElementById('S1Sotto15Sotto3'), document.getElementById('S1Sotto15Sotto4'), document.getElementById('S1Sotto15Sotto5'), document.getElementById('S1Sotto15Sotto6'), document.getElementById('S1Sotto15Sotto7'), document.getElementById('S1Sotto15Sotto8'), document.getElementById('S1Sotto15Sotto9'), document.getElementById('S1Sotto15Sotto10'));
                break;
            case 'S1voce16':
                document.getElementById('S1Sotto16').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto16Sotto1'), document.getElementById('S1Sotto16Sotto2'), document.getElementById('S1Sotto16Sotto3'), document.getElementById('S1Sotto16Sotto4'), document.getElementById('S1Sotto16Sotto5'), document.getElementById('S1Sotto16Sotto6'), document.getElementById('S1Sotto16Sotto7'), document.getElementById('S1Sotto16Sotto8'), document.getElementById('S1Sotto16Sotto9'), document.getElementById('S1Sotto16Sotto10'));
                break;
            case 'S1voce17':
                document.getElementById('S1Sotto17').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto17Sotto1'), document.getElementById('S1Sotto17Sotto2'), document.getElementById('S1Sotto17Sotto3'), document.getElementById('S1Sotto17Sotto4'), document.getElementById('S1Sotto17Sotto5'), document.getElementById('S1Sotto17Sotto6'), document.getElementById('S1Sotto17Sotto7'), document.getElementById('S1Sotto17Sotto8'), document.getElementById('S1Sotto17Sotto9'), document.getElementById('S1Sotto17Sotto10'));
                break;
            case 'S1voce18':
                document.getElementById('S1Sotto18').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto18Sotto1'), document.getElementById('S1Sotto18Sotto2'), document.getElementById('S1Sotto18Sotto3'), document.getElementById('S1Sotto18Sotto4'), document.getElementById('S1Sotto18Sotto5'), document.getElementById('S1Sotto18Sotto6'), document.getElementById('S1Sotto18Sotto7'), document.getElementById('S1Sotto18Sotto8'), document.getElementById('S1Sotto18Sotto9'), document.getElementById('S1Sotto18Sotto10'));
                break;
            case 'S1voce19':
                document.getElementById('S1Sotto19').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto19Sotto1'), document.getElementById('S1Sotto19Sotto2'), document.getElementById('S1Sotto19Sotto3'), document.getElementById('S1Sotto19Sotto4'), document.getElementById('S1Sotto19Sotto5'), document.getElementById('S1Sotto19Sotto6'), document.getElementById('S1Sotto19Sotto7'), document.getElementById('S1Sotto19Sotto8'), document.getElementById('S1Sotto19Sotto9'), document.getElementById('S1Sotto19Sotto10'));
                break;
            case 'S1voce20':
                document.getElementById('S1Sotto20').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S1Sotto20Sotto1'), document.getElementById('S1Sotto20Sotto2'), document.getElementById('S1Sotto20Sotto3'), document.getElementById('S1Sotto20Sotto4'), document.getElementById('S1Sotto20Sotto5'), document.getElementById('S1Sotto20Sotto6'), document.getElementById('S1Sotto20Sotto7'), document.getElementById('S1Sotto20Sotto8'), document.getElementById('S1Sotto20Sotto9'), document.getElementById('S1Sotto20Sotto10'));
                break;

            case 'S2voce1':
                document.getElementById('S2Sotto1').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto1Sotto2'), document.getElementById('S2Sotto1Sotto3'), document.getElementById('S2Sotto1Sotto4'), document.getElementById('S2Sotto1Sotto5'), document.getElementById('S2Sotto1Sotto6'), document.getElementById('S2Sotto1Sotto7'), document.getElementById('S2Sotto1Sotto8'), document.getElementById('S2Sotto1Sotto9'), document.getElementById('S2Sotto1Sotto10'));
                break;
            case 'S2voce2':
                document.getElementById('S2Sotto2').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto2Sotto1'), document.getElementById('S2Sotto2Sotto2'), document.getElementById('S2Sotto2Sotto3'), document.getElementById('S2Sotto2Sotto4'), document.getElementById('S2Sotto2Sotto5'), document.getElementById('S2Sotto2Sotto6'), document.getElementById('S2Sotto2Sotto7'), document.getElementById('S2Sotto2Sotto8'), document.getElementById('S2Sotto2Sotto9'), document.getElementById('S2Sotto2Sotto10'));
                break;
            case 'S2voce3':
                document.getElementById('S2Sotto3').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto3Sotto1'), document.getElementById('S2Sotto3Sotto2'), document.getElementById('S2Sotto3Sotto3'), document.getElementById('S2Sotto3Sotto4'), document.getElementById('S2Sotto3Sotto5'), document.getElementById('S2Sotto3Sotto6'), document.getElementById('S2Sotto3Sotto7'), document.getElementById('S2Sotto3Sotto8'), document.getElementById('S2Sotto3Sotto9'), document.getElementById('S2Sotto3Sotto10'));
                break;
            case 'S2voce4':
                document.getElementById('S2Sotto4').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto4Sotto1'), document.getElementById('S2Sotto4Sotto2'), document.getElementById('S2Sotto4Sotto3'), document.getElementById('S2Sotto4Sotto4'), document.getElementById('S2Sotto4Sotto5'), document.getElementById('S2Sotto4Sotto6'), document.getElementById('S2Sotto4Sotto7'), document.getElementById('S2Sotto4Sotto8'), document.getElementById('S2Sotto4Sotto9'), document.getElementById('S2Sotto4Sotto10'));
                break;
            case 'S2voce5':
                document.getElementById('S2Sotto5').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto5Sotto1'), document.getElementById('S2Sotto5Sotto2'), document.getElementById('S2Sotto5Sotto3'), document.getElementById('S2Sotto5Sotto4'), document.getElementById('S2Sotto5Sotto5'), document.getElementById('S2Sotto5Sotto6'), document.getElementById('S2Sotto5Sotto7'), document.getElementById('S2Sotto5Sotto8'), document.getElementById('S2Sotto5Sotto9'), document.getElementById('S2Sotto5Sotto10'));
                break;
            case 'S2voce6':
                document.getElementById('S2Sotto6').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto6Sotto1'), document.getElementById('S2Sotto6Sotto2'), document.getElementById('S2Sotto6Sotto3'), document.getElementById('S2Sotto6Sotto4'), document.getElementById('S2Sotto6Sotto5'), document.getElementById('S2Sotto6Sotto6'), document.getElementById('S2Sotto6Sotto7'), document.getElementById('S2Sotto6Sotto8'), document.getElementById('S2Sotto6Sotto9'), document.getElementById('S2Sotto6Sotto10'));
                break;
            case 'S2voce7':
                document.getElementById('S2Sotto7').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto7Sotto1'), document.getElementById('S2Sotto7Sotto2'), document.getElementById('S2Sotto7Sotto3'), document.getElementById('S2Sotto7Sotto4'), document.getElementById('S2Sotto7Sotto5'), document.getElementById('S2Sotto7Sotto6'), document.getElementById('S2Sotto7Sotto7'), document.getElementById('S2Sotto7Sotto8'), document.getElementById('S2Sotto7Sotto9'), document.getElementById('S2Sotto7Sotto10'));
                break;
            case 'S2voce8':
                document.getElementById('S2Sotto8').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto8Sotto1'), document.getElementById('S2Sotto8Sotto2'), document.getElementById('S2Sotto8Sotto3'), document.getElementById('S2Sotto8Sotto4'), document.getElementById('S2Sotto8Sotto5'), document.getElementById('S2Sotto8Sotto6'), document.getElementById('S2Sotto8Sotto7'), document.getElementById('S2Sotto8Sotto8'), document.getElementById('S2Sotto8Sotto9'), document.getElementById('S2Sotto8Sotto10'));
                break;
            case 'S2voce9':
                document.getElementById('S2Sotto9').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto9Sotto1'), document.getElementById('S2Sotto9Sotto2'), document.getElementById('S2Sotto9Sotto3'), document.getElementById('S2Sotto9Sotto4'), document.getElementById('S2Sotto9Sotto5'), document.getElementById('S2Sotto9Sotto6'), document.getElementById('S2Sotto9Sotto7'), document.getElementById('S2Sotto9Sotto8'), document.getElementById('S2Sotto9Sotto9'), document.getElementById('S2Sotto9Sotto10'));
                break;
            case 'S2voce10':
                document.getElementById('S2Sotto10').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto10Sotto1'), document.getElementById('S2Sotto10Sotto2'), document.getElementById('S2Sotto10Sotto3'), document.getElementById('S2Sotto10Sotto4'), document.getElementById('S2Sotto10Sotto5'), document.getElementById('S2Sotto10Sotto6'), document.getElementById('S2Sotto10Sotto7'), document.getElementById('S2Sotto10Sotto8'), document.getElementById('S2Sotto10Sotto9'), document.getElementById('S2Sotto10Sotto10'));
                break;
            case 'S2voce11':
                document.getElementById('S2Sotto11').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto11Sotto2'), document.getElementById('S2Sotto11Sotto3'), document.getElementById('S2Sotto11Sotto4'), document.getElementById('S2Sotto11Sotto5'), document.getElementById('S2Sotto11Sotto6'), document.getElementById('S2Sotto11Sotto7'), document.getElementById('S2Sotto11Sotto8'), document.getElementById('S2Sotto11Sotto9'), document.getElementById('S2Sotto11Sotto10'));
                break;
            case 'S2voce12':
                document.getElementById('S2Sotto12').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto12Sotto2'), document.getElementById('S2Sotto12Sotto3'), document.getElementById('S2Sotto12Sotto4'), document.getElementById('S2Sotto12Sotto5'), document.getElementById('S2Sotto12Sotto6'), document.getElementById('S2Sotto12Sotto7'), document.getElementById('S2Sotto12Sotto8'), document.getElementById('S2Sotto12Sotto9'), document.getElementById('S2Sotto12Sotto10'));
                break;
            case 'S2voce13':
                document.getElementById('S2Sotto13').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto13Sotto2'), document.getElementById('S2Sotto13Sotto3'), document.getElementById('S2Sotto13Sotto4'), document.getElementById('S2Sotto13Sotto5'), document.getElementById('S2Sotto13Sotto6'), document.getElementById('S2Sotto13Sotto7'), document.getElementById('S2Sotto13Sotto8'), document.getElementById('S2Sotto13Sotto9'), document.getElementById('S2Sotto13Sotto10'));
                break;
            case 'S2voce14':
                document.getElementById('S2Sotto14').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto14Sotto2'), document.getElementById('S2Sotto14Sotto3'), document.getElementById('S2Sotto14Sotto4'), document.getElementById('S2Sotto14Sotto5'), document.getElementById('S2Sotto14Sotto6'), document.getElementById('S2Sotto14Sotto7'), document.getElementById('S2Sotto14Sotto8'), document.getElementById('S2Sotto14Sotto9'), document.getElementById('S2Sotto14Sotto10'));
                break;
            case 'S2voce15':
                document.getElementById('S2Sotto15').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto15Sotto2'), document.getElementById('S2Sotto15Sotto3'), document.getElementById('S2Sotto15Sotto4'), document.getElementById('S2Sotto15Sotto5'), document.getElementById('S2Sotto15Sotto6'), document.getElementById('S2Sotto15Sotto7'), document.getElementById('S2Sotto15Sotto8'), document.getElementById('S2Sotto15Sotto9'), document.getElementById('S2Sotto15Sotto10'));
                break;
            case 'S2voce16':
                document.getElementById('S2Sotto16').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto16Sotto2'), document.getElementById('S2Sotto16Sotto3'), document.getElementById('S2Sotto16Sotto4'), document.getElementById('S2Sotto16Sotto5'), document.getElementById('S2Sotto16Sotto6'), document.getElementById('S2Sotto16Sotto7'), document.getElementById('S2Sotto16Sotto8'), document.getElementById('S2Sotto16Sotto9'), document.getElementById('S2Sotto16Sotto10'));
                break;
            case 'S2voce17':
                document.getElementById('S2Sotto17').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto17Sotto2'), document.getElementById('S2Sotto17Sotto3'), document.getElementById('S2Sotto17Sotto4'), document.getElementById('S2Sotto17Sotto5'), document.getElementById('S2Sotto17Sotto6'), document.getElementById('S2Sotto17Sotto7'), document.getElementById('S2Sotto17Sotto8'), document.getElementById('S2Sotto17Sotto9'), document.getElementById('S2Sotto17Sotto10'));
                break;
            case 'S2voce18':
                document.getElementById('S2Sotto18').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto18Sotto2'), document.getElementById('S2Sotto18Sotto3'), document.getElementById('S2Sotto18Sotto4'), document.getElementById('S2Sotto18Sotto5'), document.getElementById('S2Sotto18Sotto6'), document.getElementById('S2Sotto18Sotto7'), document.getElementById('S2Sotto18Sotto8'), document.getElementById('S2Sotto18Sotto9'), document.getElementById('S2Sotto18Sotto10'));
                break;
            case 'S2voce19':
                document.getElementById('S2Sotto19').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto19Sotto2'), document.getElementById('S2Sotto19Sotto3'), document.getElementById('S2Sotto19Sotto4'), document.getElementById('S2Sotto19Sotto5'), document.getElementById('S2Sotto19Sotto6'), document.getElementById('S2Sotto19Sotto7'), document.getElementById('S2Sotto19Sotto8'), document.getElementById('S2Sotto19Sotto9'), document.getElementById('S2Sotto19Sotto10'));
                break;
            case 'S2voce20':
                document.getElementById('S2Sotto20').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S2Sotto1Sotto1'), document.getElementById('S2Sotto20Sotto2'), document.getElementById('S2Sotto20Sotto3'), document.getElementById('S2Sotto20Sotto4'), document.getElementById('S2Sotto20Sotto5'), document.getElementById('S2Sotto20Sotto6'), document.getElementById('S2Sotto20Sotto7'), document.getElementById('S2Sotto20Sotto8'), document.getElementById('S2Sotto20Sotto9'), document.getElementById('S2Sotto20Sotto10'));
                break;

            case 'S3voce1':
                document.getElementById('S3Sotto1').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S3Sotto1Sotto1'), document.getElementById('S3Sotto1Sotto2'), document.getElementById('S3Sotto1Sotto3'), document.getElementById('S3Sotto1Sotto4'), document.getElementById('S3Sotto1Sotto5'), document.getElementById('S3Sotto1Sotto6'), document.getElementById('S3Sotto1Sotto7'), document.getElementById('S3Sotto1Sotto8'), document.getElementById('S3Sotto1Sotto9'), document.getElementById('S3Sotto1Sotto10'));
                break;
            case 'S3voce2':
                document.getElementById('S3Sotto2').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S3Sotto2Sotto1'), document.getElementById('S3Sotto2Sotto2'), document.getElementById('S3Sotto2Sotto3'), document.getElementById('S3Sotto2Sotto4'), document.getElementById('S3Sotto2Sotto5'), document.getElementById('S3Sotto2Sotto6'), document.getElementById('S3Sotto2Sotto7'), document.getElementById('S3Sotto2Sotto8'), document.getElementById('S3Sotto2Sotto9'), document.getElementById('S3Sotto2Sotto10'));
                break;
            case 'S3voce3':
                document.getElementById('S3Sotto3').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S3Sotto3Sotto1'), document.getElementById('S3Sotto3Sotto2'), document.getElementById('S3Sotto3Sotto3'), document.getElementById('S3Sotto3Sotto4'), document.getElementById('S3Sotto3Sotto5'), document.getElementById('S3Sotto3Sotto6'), document.getElementById('S3Sotto3Sotto7'), document.getElementById('S3Sotto3Sotto8'), document.getElementById('S3Sotto3Sotto9'), document.getElementById('S3Sotto3Sotto10'));
                break;
            case 'S3voce4':
                document.getElementById('S3Sotto4').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S3Sotto4Sotto1'), document.getElementById('S3Sotto4Sotto2'), document.getElementById('S3Sotto4Sotto3'), document.getElementById('S3Sotto4Sotto4'), document.getElementById('S3Sotto4Sotto5'), document.getElementById('S3Sotto4Sotto6'), document.getElementById('S3Sotto4Sotto7'), document.getElementById('S3Sotto4Sotto8'), document.getElementById('S3Sotto4Sotto9'), document.getElementById('S3Sotto4Sotto10'));
                break;
            case 'S3voce5':
                document.getElementById('S3Sotto5').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S3Sotto5Sotto1'), document.getElementById('S3Sotto5Sotto2'), document.getElementById('S3Sotto5Sotto3'), document.getElementById('S3Sotto5Sotto4'), document.getElementById('S3Sotto5Sotto5'), document.getElementById('S3Sotto5Sotto6'), document.getElementById('S3Sotto5Sotto7'), document.getElementById('S3Sotto5Sotto8'), document.getElementById('S3Sotto5Sotto9'), document.getElementById('S3Sotto5Sotto10'));
                break;

            case 'S4voce1':
                document.getElementById('S4Sotto1').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S4Sotto1Sotto1'), document.getElementById('S4Sotto1Sotto2'), document.getElementById('S4Sotto1Sotto3'), document.getElementById('S4Sotto1Sotto4'), document.getElementById('S4Sotto1Sotto5'), document.getElementById('S4Sotto1Sotto6'), document.getElementById('S4Sotto1Sotto7'), document.getElementById('S4Sotto1Sotto8'), document.getElementById('S4Sotto1Sotto9'), document.getElementById('S4Sotto1Sotto10'));
                break;
            case 'S4voce2':
                document.getElementById('S4Sotto2').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S4Sotto2Sotto1'), document.getElementById('S4Sotto2Sotto2'), document.getElementById('S4Sotto2Sotto3'), document.getElementById('S4Sotto2Sotto4'), document.getElementById('S4Sotto2Sotto5'), document.getElementById('S4Sotto2Sotto6'), document.getElementById('S4Sotto2Sotto7'), document.getElementById('S4Sotto2Sotto8'), document.getElementById('S4Sotto2Sotto9'), document.getElementById('S4Sotto2Sotto10'));
                break;
            case 'S4voce3':
                document.getElementById('S4Sotto3').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S4Sotto3Sotto1'), document.getElementById('S4Sotto3Sotto2'), document.getElementById('S4Sotto3Sotto3'), document.getElementById('S4Sotto3Sotto4'), document.getElementById('S4Sotto3Sotto5'), document.getElementById('S4Sotto3Sotto6'), document.getElementById('S4Sotto3Sotto7'), document.getElementById('S4Sotto3Sotto8'), document.getElementById('S4Sotto3Sotto9'), document.getElementById('S4Sotto3Sotto10'));
                break;
            case 'S4voce4':
                document.getElementById('S4Sotto4').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S4Sotto4Sotto1'), document.getElementById('S4Sotto4Sotto2'), document.getElementById('S4Sotto4Sotto3'), document.getElementById('S4Sotto4Sotto4'), document.getElementById('S4Sotto4Sotto5'), document.getElementById('S4Sotto4Sotto6'), document.getElementById('S4Sotto4Sotto7'), document.getElementById('S4Sotto4Sotto8'), document.getElementById('S4Sotto4Sotto9'), document.getElementById('S4Sotto4Sotto10'));
                break;
            case 'S4voce5':
                document.getElementById('S4Sotto5').value = TestoDaScrivere;
                ValorizzaSottoSotto(document.getElementById('S4Sotto5Sotto1'), document.getElementById('S4Sotto5Sotto2'), document.getElementById('S4Sotto5Sotto3'), document.getElementById('S4Sotto5Sotto4'), document.getElementById('S4Sotto5Sotto5'), document.getElementById('S4Sotto5Sotto6'), document.getElementById('S4Sotto5Sotto7'), document.getElementById('S4Sotto5Sotto8'), document.getElementById('S4Sotto5Sotto9'), document.getElementById('S4Sotto5Sotto10'));
                break;
        }
        myOpacity.toggle();
        

        }



        function InserisciSottoSottoVoci() {
         var i;
         var TestoDaScrivere;

         TestoDaScrivere = '';

         if (document.getElementById('txtVoceSottoSotto1').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto1').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto2').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto2').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto3').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto3').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto4').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto4').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto5').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto5').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto6').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto6').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto7').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto7').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto8').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto8').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto9').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto9').value + '#';
         }
         if (document.getElementById('txtVoceSottoSotto10').value!='') {
            TestoDaScrivere = TestoDaScrivere + document.getElementById('txtVoceSottoSotto10').value + '#';
        }

        
        if (TestoDaScrivere != '') {
            TestoDaScrivere = TestoDaScrivere.substr(0, TestoDaScrivere.length - 1);
        }
        
        switch (document.getElementById('codicescelto1').value.substr(5, 7)) {
            case '01':
                document.getElementById('HVoceSotto1').value = TestoDaScrivere;
                break;
            case '02':
                document.getElementById('HVoceSotto2').value = TestoDaScrivere;
                break;
            case '03':
                document.getElementById('HVoceSotto3').value = TestoDaScrivere;
                break;
            case '04':
                document.getElementById('HVoceSotto4').value = TestoDaScrivere;
                break;
            case '05':
                document.getElementById('HVoceSotto5').value = TestoDaScrivere;
                break;
            case '06':
                document.getElementById('HVoceSotto6').value = TestoDaScrivere;
                break;
            case '07':
                document.getElementById('HVoceSotto7').value = TestoDaScrivere;
                break;
            case '08':
                document.getElementById('HVoceSotto8').value = TestoDaScrivere;
                break;
            case '09':
                document.getElementById('HVoceSotto9').value = TestoDaScrivere;
                break;
            case '10':
                document.getElementById('HVoceSotto10').value = TestoDaScrivere;
                break;
         }
       
        myOpacity2.toggle();
        
        }

        function SottoVoci(Descrizione, codice,voce) {
            //if (document.getElementById('Modificato').value != '1') {
                if (Descrizione != '') {
                    document.getElementById('codicescelto').value = codice;
                    document.getElementById('lblDecrVoce').innerText = Descrizione;

                    document.getElementById('txtCodSotto1').value = codice + '.01';
                    document.getElementById('txtCodSotto2').value = codice + '.02';
                    document.getElementById('txtCodSotto3').value = codice + '.03';
                    document.getElementById('txtCodSotto4').value = codice + '.04';
                    document.getElementById('txtCodSotto5').value = codice + '.05';
                    document.getElementById('txtCodSotto6').value = codice + '.06';
                    document.getElementById('txtCodSotto7').value = codice + '.07';
                    document.getElementById('txtCodSotto8').value = codice + '.08';
                    document.getElementById('txtCodSotto9').value = codice + '.09';
                    document.getElementById('txtCodSotto10').value = codice + '.10';

                    var Valore
                    var mioval

                    document.getElementById('txtVoceSotto1').value = '';
                    document.getElementById('txtVoceSotto2').value = '';
                    document.getElementById('txtVoceSotto3').value =  '';
                    document.getElementById('txtVoceSotto4').value = '';
                    document.getElementById('txtVoceSotto5').value = '';
                    document.getElementById('txtVoceSotto6').value= '';
                    document.getElementById('txtVoceSotto7').value = '';
                    document.getElementById('txtVoceSotto8').value = '';
                    document.getElementById('txtVoceSotto9').value = '';
                    document.getElementById('txtVoceSotto10').value = '';


                    document.getElementById('img1').style.visibility = 'visible';
                    document.getElementById('img2').style.visibility = 'visible';
                    document.getElementById('img3').style.visibility = 'visible';
                    document.getElementById('img4').style.visibility = 'visible';
                    document.getElementById('img5').style.visibility = 'visible';
                    document.getElementById('img6').style.visibility = 'visible';
                    document.getElementById('img7').style.visibility = 'visible';
                    document.getElementById('img8').style.visibility = 'visible';
                    document.getElementById('img9').style.visibility = 'visible';
                    document.getElementById('img10').style.visibility = 'visible';
                                        
                    switch (codice) {
                        case '1.01':
                            Valore = document.getElementById('S1Sotto1').value;

                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto1Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto1Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto1Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto1Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto1Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto1Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto1Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto1Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto1Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto1Sotto10').value;
                            break;
                        case '1.02':
                            Valore = document.getElementById('S1Sotto2').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto2Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto2Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto2Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto2Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto2Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto2Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto2Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto2Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto2Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto2Sotto10').value;
                            break;
                        case '1.03':
                            Valore = document.getElementById('S1Sotto3').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto3Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto3Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto3Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto3Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto3Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto3Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto3Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto3Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto3Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto3Sotto10').value;
                            break;
                        case '1.04':
                            Valore = document.getElementById('S1Sotto4').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto4Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto4Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto4Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto4Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto4Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto4Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto4Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto4Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto4Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto4Sotto10').value;
                            break;
                        case '1.05':
                            Valore = document.getElementById('S1Sotto5').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto5Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto5Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto5Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto5Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto5Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto5Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto5Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto5Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto5Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto5Sotto10').value;
                            break;
                        case '1.06':
                            Valore = document.getElementById('S1Sotto6').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto6Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto6Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto6Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto6Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto6Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto6Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto6Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto6Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto6Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto6Sotto10').value;
                            break;
                        case '1.07':
                            Valore = document.getElementById('S1Sotto7').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto7Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto7Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto7Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto7Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto7Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto7Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto7Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto7Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto7Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto7Sotto10').value;
                            break;
                        case '1.08':
                            Valore = document.getElementById('S1Sotto8').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto8Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto8Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto8Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto8Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto8Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto8Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto8Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto8Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto8Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto8Sotto10').value;
                            break;
                        case '1.09':
                            Valore = document.getElementById('S1Sotto9').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto9Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto9Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto9Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto9Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto9Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto9Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto9Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto9Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto9Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto9Sotto10').value;
                            break;
                        case '1.10':
                            Valore = document.getElementById('S1Sotto10').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto10Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto10Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto10Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto10Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto10Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto10Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto10Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto10Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto10Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto10Sotto10').value;
                            break;
                        case '1.11':
                            Valore = document.getElementById('S1Sotto11').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto11Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto11Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto11Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto11Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto11Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto11Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto11Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto11Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto11Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto11Sotto10').value;
                            break;
                        case '1.12':
                            Valore = document.getElementById('S1Sotto12').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto12Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto12Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto12Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto12Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto12Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto12Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto12Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto12Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto12Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto12Sotto10').value;
                            break;
                        case '1.13':
                            Valore = document.getElementById('S1Sotto13').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto13Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto13Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto13Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto13Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto13Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto13Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto13Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto13Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto13Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto13Sotto10').value;
                            break;
                        case '1.14':
                            Valore = document.getElementById('S1Sotto14').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto14Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto14Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto14Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto14Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto14Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto14Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto14Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto14Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto14Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto14Sotto10').value;
                            break;
                        case '1.15':
                            Valore = document.getElementById('S1Sotto15').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto15Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto15Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto15Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto15Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto15Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto15Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto15Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto15Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto15Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto15Sotto10').value;
                            break;
                        case '1.16':
                            Valore = document.getElementById('S1Sotto16').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto16Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto16Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto16Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto16Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto16Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto16Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto16Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto16Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto16Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto16Sotto10').value;
                            break;
                        case '1.17':
                            Valore = document.getElementById('S1Sotto17').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto17Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto17Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto17Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto17Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto17Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto17Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto17Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto17Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto17Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto17Sotto10').value;
                            break;
                        case '1.18':
                            Valore = document.getElementById('S1Sotto18').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto18Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto18Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto18Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto18Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto18Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto18Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto18Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto18Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto18Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto18Sotto10').value;
                            break;
                        case '1.19':
                            Valore = document.getElementById('S1Sotto19').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto19Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto19Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto19Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto19Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto19Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto19Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto19Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto19Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto19Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto19Sotto10').value;
                            break;
                        case '1.20':
                            Valore = document.getElementById('S1Sotto20').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S1Sotto20Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S1Sotto20Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S1Sotto20Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S1Sotto20Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S1Sotto20Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S1Sotto20Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S1Sotto20Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S1Sotto20Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S1Sotto20Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S1Sotto20Sotto10').value;
                            break;
                        case '2.01':
                            Valore = document.getElementById('S2Sotto1').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto1Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto1Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto1Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto1Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto1Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto1Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto1Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto1Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto1Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto1Sotto10').value;
                            break;
                        case '2.02':
                            Valore = document.getElementById('S2Sotto2').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto2Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto2Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto2Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto2Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto2Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto2Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto2Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto2Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto2Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto2Sotto10').value;


                            document.getElementById('img1').style.visibility = 'hidden';
                            document.getElementById('img2').style.visibility = 'hidden';
                            document.getElementById('img3').style.visibility = 'hidden';
                            
                            
                            break;
                        case '2.03':
                            Valore = document.getElementById('S2Sotto3').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto3Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto3Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto3Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto3Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto3Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto3Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto3Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto3Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto3Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto3Sotto10').value;

                            document.getElementById('img1').style.visibility = 'hidden';
                            break;
                        case '2.04':
                            Valore = document.getElementById('S2Sotto4').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto4Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto4Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto4Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto4Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto4Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto4Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto4Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto4Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto4Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto4Sotto10').value;

                            document.getElementById('img1').style.visibility = 'hidden';
                            document.getElementById('img2').style.visibility = 'hidden';
                            document.getElementById('img3').style.visibility = 'hidden';
                            document.getElementById('img4').style.visibility = 'hidden';


                            break;
                        case '2.05':
                            Valore = document.getElementById('S2Sotto5').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto5Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto5Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto5Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto5Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto5Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto5Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto5Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto5Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto5Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto5Sotto10').value;
                            break;
                        case '2.06':
                            Valore = document.getElementById('S2Sotto6').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto6Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto6Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto6Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto6Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto6Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto6Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto6Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto6Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto6Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto6Sotto10').value;
                            break;
                        case '2.07':
                            Valore = document.getElementById('S2Sotto7').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto7Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto7Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto7Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto7Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto7Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto7Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto7Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto7Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto7Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto7Sotto10').value;
                            break;
                        case '2.08':
                            Valore = document.getElementById('S2Sotto8').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto8Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto8Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto8Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto8Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto8Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto8Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto8Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto8Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto8Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto8Sotto10').value;
                            break;
                        case '2.09':
                            Valore = document.getElementById('S2Sotto9').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto9Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto9Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto9Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto9Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto9Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto9Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto9Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto9Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto9Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto9Sotto10').value;
                            break;
                        case '2.10':
                            Valore = document.getElementById('S2Sotto10').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto10Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto10Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto10Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto10Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto10Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto10Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto10Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto10Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto10Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto10Sotto10').value;
                            break;
                        case '2.11':
                            Valore = document.getElementById('S2Sotto11').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto11Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto11Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto11Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto11Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto11Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto11Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto11Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto11Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto11Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto11Sotto10').value;
                            break;
                        case '2.12':
                            Valore = document.getElementById('S2Sotto12').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto12Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto12Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto12Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto12Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto12Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto12Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto12Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto12Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto12Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto12Sotto10').value;
                            break;
                        case '2.13':
                            Valore = document.getElementById('S2Sotto13').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto13Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto13Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto13Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto13Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto13Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto13Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto13Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto13Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto13Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto13Sotto10').value;
                            break;
                        case '2.14':
                            Valore = document.getElementById('S2Sotto14').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto14Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto14Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto14Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto14Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto14Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto14Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto14Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto14Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto14Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto14Sotto10').value;
                            break;
                        case '2.15':
                            Valore = document.getElementById('S2Sotto15').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto15Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto15Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto15Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto15Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto15Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto15Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto15Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto15Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto15Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto15Sotto10').value;
                            break;
                        case '2.16':
                            Valore = document.getElementById('S2Sotto16').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto16Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto16Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto16Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto16Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto16Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto16Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto16Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto16Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto16Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto16Sotto10').value;
                            break;
                        case '2.17':
                            Valore = document.getElementById('S2Sotto17').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto17Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto17Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto17Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto17Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto17Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto17Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto17Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto17Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto17Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto17Sotto10').value;
                            break;
                        case '2.18':
                            Valore = document.getElementById('S2Sotto18').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto18Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto18Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto18Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto18Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto18Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto18Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto18Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto18Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto18Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto18Sotto10').value;
                            break;
                        case '2.19':
                            Valore = document.getElementById('S2Sotto19').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto19Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto19Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto19Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto19Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto19Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto19Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto19Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto19Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto19Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto19Sotto10').value;
                            break;
                        case '2.20':
                            Valore = document.getElementById('S2Sotto20').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S2Sotto20Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S2Sotto20Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S2Sotto20Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S2Sotto20Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S2Sotto20Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S2Sotto20Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S2Sotto20Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S2Sotto20Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S2Sotto20Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S2Sotto20Sotto10').value;
                            break;
                        case '3.01':
                            Valore = document.getElementById('S3Sotto1').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S3Sotto1Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S3Sotto1Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S3Sotto1Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S3Sotto1Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S3Sotto1Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S3Sotto1Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S3Sotto1Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S3Sotto1Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S3Sotto1Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S3Sotto1Sotto10').value;
                            break;
                        case '3.02':
                            Valore = document.getElementById('S3Sotto2').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S3Sotto2Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S3Sotto2Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S3Sotto2Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S3Sotto2Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S3Sotto2Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S3Sotto2Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S3Sotto2Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S3Sotto2Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S3Sotto2Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S3Sotto2Sotto10').value;
                            break;
                        case '3.03':
                            Valore = document.getElementById('S3Sotto3').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S3Sotto3Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S3Sotto3Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S3Sotto3Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S3Sotto3Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S3Sotto3Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S3Sotto3Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S3Sotto3Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S3Sotto3Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S3Sotto3Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S3Sotto3Sotto10').value;
                            break;
                        case '3.04':
                            Valore = document.getElementById('S3Sotto4').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S3Sotto4Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S3Sotto4Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S3Sotto4Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S3Sotto4Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S3Sotto4Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S3Sotto4Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S3Sotto4Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S3Sotto4Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S3Sotto4Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S3Sotto4Sotto10').value;
                            break;
                        case '3.05':
                            Valore = document.getElementById('S3Sotto5').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S3Sotto5Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S3Sotto5Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S3Sotto5Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S3Sotto5Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S3Sotto5Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S3Sotto5Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S3Sotto5Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S3Sotto5Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S3Sotto5Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S3Sotto5Sotto10').value;
                            break;

                        case '4.01':
                            Valore = document.getElementById('S4Sotto1').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S4Sotto1Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S4Sotto1Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S4Sotto1Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S4Sotto1Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S4Sotto1Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S4Sotto1Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S4Sotto1Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S4Sotto1Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S4Sotto1Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S4Sotto1Sotto10').value;
                            break;
                        case '4.02':
                            Valore = document.getElementById('S4Sotto2').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S4Sotto2Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S4Sotto2Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S4Sotto2Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S4Sotto2Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S4Sotto2Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S4Sotto2Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S4Sotto2Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S4Sotto2Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S4Sotto2Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S4Sotto2Sotto10').value;
                            break;
                        case '4.03':
                            Valore = document.getElementById('S4Sotto3').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S4Sotto3Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S4Sotto3Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S4Sotto3Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S4Sotto3Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S4Sotto3Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S4Sotto3Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S4Sotto3Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S4Sotto3Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S4Sotto3Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S4Sotto3Sotto10').value;
                            break;
                        case '4.04':
                            Valore = document.getElementById('S4Sotto4').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S4Sotto4Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S4Sotto4Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S4Sotto4Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S4Sotto4Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S4Sotto4Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S4Sotto4Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S4Sotto4Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S4Sotto4Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S4Sotto4Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S4Sotto4Sotto10').value;
                            break;
                        case '4.05':
                            Valore = document.getElementById('S4Sotto5').value;
                            document.getElementById('HVoceSotto1').value = document.getElementById('S4Sotto5Sotto1').value;
                            document.getElementById('HVoceSotto2').value = document.getElementById('S4Sotto5Sotto2').value;
                            document.getElementById('HVoceSotto3').value = document.getElementById('S4Sotto5Sotto3').value;
                            document.getElementById('HVoceSotto4').value = document.getElementById('S4Sotto5Sotto4').value;
                            document.getElementById('HVoceSotto5').value = document.getElementById('S4Sotto5Sotto5').value;
                            document.getElementById('HVoceSotto6').value = document.getElementById('S4Sotto5Sotto6').value;
                            document.getElementById('HVoceSotto7').value = document.getElementById('S4Sotto5Sotto7').value;
                            document.getElementById('HVoceSotto8').value = document.getElementById('S4Sotto5Sotto8').value;
                            document.getElementById('HVoceSotto9').value = document.getElementById('S4Sotto5Sotto9').value;
                            document.getElementById('HVoceSotto10').value = document.getElementById('S4Sotto5Sotto10').value;
                            break;

                    }


                    if ((Valore != "") && (Valore != null)) {
                        if (Valore.indexOf("#") == -1) {
                            document.getElementById('txtVoceSotto1').value = Valore;
                        }
                        else {
                            var Val_array = Valore.split("#");
                            var part_num = 0;
                            while (part_num < Val_array.length) {
                                if (part_num == 0) { document.getElementById('txtVoceSotto1').value = Val_array[part_num] }
                                if (part_num == 1) { document.getElementById('txtVoceSotto2').value = Val_array[part_num] }
                                if (part_num == 2) { document.getElementById('txtVoceSotto3').value = Val_array[part_num] }
                                if (part_num == 3) { document.getElementById('txtVoceSotto4').value = Val_array[part_num] }
                                if (part_num == 4) { document.getElementById('txtVoceSotto5').value = Val_array[part_num] }
                                if (part_num == 5) { document.getElementById('txtVoceSotto6').value = Val_array[part_num] }
                                if (part_num == 6) { document.getElementById('txtVoceSotto7').value = Val_array[part_num] }
                                if (part_num == 7) { document.getElementById('txtVoceSotto8').value = Val_array[part_num] }
                                if (part_num == 8) { document.getElementById('txtVoceSotto9').value = Val_array[part_num] }
                                if (part_num == 9) { document.getElementById('txtVoceSotto10').value = Val_array[part_num] }
                                part_num += 1;
                            }
                        }
                    }

                    document.getElementById('casella').value=voce;
                    myOpacity.toggle();
                }
                else {
                    alert('Inserire una descrizione per la voce codice ' + codice + ' prima di inserire sotto voci!');
                }
        }
        
    </script>
        

        
        <asp:HiddenField ID="salvaok" runat="server" Value="0" />
        

        
        
        

        
    </form>
</body>
</html>
