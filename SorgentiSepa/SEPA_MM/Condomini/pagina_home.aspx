<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagina_home.aspx.vb" Inherits="ANAUT_pagina_home" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Pagina Principale</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		
<script language="javascript" type="text/javascript">
    var Uscita;
    Uscita = 1;

    function ApriEventiPat() {
        document.getElementById('splash').style.visibility = 'visible';

		    parent.main.location.replace('EventiPatrimoniali.aspx?TUTTI=0');
		}
		function ApriPagamenti() {
		    document.getElementById('splash').style.visibility = 'visible';

		    parent.main.location.replace('PagamScadenza.aspx');
		}

		function ApriMorositaDaStampare() {
		    document.getElementById('splash').style.visibility = 'visible';

		    parent.main.location.replace('MorositaDaStampare.aspx');
		}

</script>

	</head>
	<body style="background-attachment: fixed; background-image: url(../NuoveImm/sfondocopertinaContratti1.jpg); background-repeat :no-repeat;">
          <div id="splash"
              
              style="border: thin dashed #000066; position :absolute; z-index :500; text-align:center; font-size:10px; width: 100%; height: 95%; vertical-align: top; line-height: normal; top: 22px; left: 10px; background-color:#FFFFFF; visibility: visible;">
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
            <img src='Immagini/load.gif' alt='caricamento in corso'/><br/><br/>
            caricamento in corso...<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;</div>  
		<form id="Form1" method="post" runat="server">
			&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Tahoma" Font-Size="8pt"
                Style="z-index: 100; left: 23px; position: absolute; top: 69px" 
                Text="VERSIONE 1.20" Font-Italic="False"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="txtmessaggio" runat="server" Style="left: 193px; position: absolute;
                top: 512px; z-index: 101;" Height="10px" Width="7px" BackColor="White" 
                BorderStyle="None" ForeColor="White" TabIndex="-1"></asp:TextBox>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt"
                ForeColor="#404040" Style="z-index: 102; left: 23px; position: absolute; top: 91px"
                Text="Label" Width="314px"></asp:Label>
            &nbsp; &nbsp;&nbsp;
            <img src="../immagini/sistemiesoluzionisrl.gif" style="z-index: 111; left: 26px; position: absolute;
                top: 497px" alt='sistemiesoluzioni' />

            &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
		    <p>
            <asp:Image ID="imgNuoviEventi" 
                style="position:absolute; top: 455px; left: 517px; cursor: hand; right: 529px;" 
                runat="server" 
                ImageUrl="Immagini/NewEventgif.gif" 
                    ToolTip="Visualizza l'elenco dei nuovi eventi patrimoniali"  />
            <asp:Image ID="imgPagScad" 
                style="position:absolute; top: 453px; left: 392px; cursor: hand;" 
                runat="server" 
                ImageUrl="Immagini/ScadPagam.gif" 
                    ToolTip="PAGAMENTI IN SCADENZA" Visible="False"  />
            <asp:Image ID="imgEventMorToPrint" 
                style="position:absolute; top: 456px; left: 259px; cursor: hand; right: 787px;" 
                runat="server" 
                ImageUrl="Immagini/Img_EvMorosita.gif" 
                    ToolTip="Visualizza l'elenco degli eventi reativi alla Morosità"  />
            </p>
		    </form>
            <script language ="javascript" type ="text/javascript" >
                document.getElementById('splash').style.visibility = 'hidden';
            </script>
	    </body>
	

</html>
