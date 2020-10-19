<%@ Page Language="VB" AutoEventWireup="false" CodeFile="com_documenti.aspx.vb" Inherits="ANAUT_com_documenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self"></base>
    <title>Documentazione</title>

     
  
    <style type="text/css">
        .CssMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
        .CssComuniNazioni
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 166px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssPresenta
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 450px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssFamiAbit
        {
            font-size: 8pt;
            width: 600px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssProv
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 48px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssIndirizzo
        {
            font-size: 8pt;
            text-transform: uppercase;
            width: 66px;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 20px;
            font-variant: normal;
        }
        .CssLabel
        {
            font-size: 8pt;
            color: black;
            line-height: normal;
            font-style: normal;
            font-family: times;
            height: 20px;
            font-variant: normal;
        }
        .CssLblValori
        {
            font-size: 8pt;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
        .CssEtichetta
        {
            alignment: center;
        }
        .CssNuovoMaiuscolo
        {
            font-size: 8pt;
            text-transform: uppercase;
            color: blue;
            line-height: normal;
            font-style: normal;
            font-family: arial;
            height: 16px;
            font-variant: normal;
        }
    </style>
</head>
<script type="text/javascript" src="../Funzioni.js"></script>
<body bgcolor="lightsteelblue">
    <script type="text/javascript">
        window.name = "modal";
    </script>
    <form id="form1" runat="server">
    <div>
       <input id="Button2" language="javascript" onclick="ConfermaEsci();" style="z-index: 112;
            left: 496px; position: absolute; top: 218px" type="button" value="Chiudi" />



        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            ForeColor="#0000C0" Style="z-index: 100; left: 10px; position: absolute; top: 11px"
            Text="Documentazione Mancante" Width="209px"></asp:Label>
        <asp:Button ID="btnSalva" runat="server" Style="z-index: 101; left: 340px; position: absolute;
            top: 218px" TabIndex="7" Text="SALVA e Chiudi" />
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Style="z-index: 102; left: 12px; position: absolute; top: 92px;
            width: 535px; height: 31px;">ATTENZIONE...Selezionare un componente dalla lista se si desidera specificare che il tipo di documento scelto è riferito al singolo individuo.</asp:Label>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 12px; position: absolute;
            top: 132px" Width="50px">Componente</asp:Label>
        <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 102; left: 12px; position: absolute;
            top: 162px; width: 85px;">Note</asp:Label>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
            Font-Size="8pt" Height="18px" Style="z-index: 103; left: 11px; position: absolute;
            top: 61px; width: 96px;">Tipo Documento</asp:Label>
        <asp:DropDownList ID="cmbTipo" runat="server" CssClass="CssMaiuscolo" Style="z-index: 104;
            left: 103px; position: absolute; top: 58px" TabIndex="1" Width="450px">
        </asp:DropDownList>
        <asp:DropDownList ID="cmbComponente" runat="server" CssClass="CssMaiuscolo" Style="z-index: 105;
            left: 103px; position: absolute; top: 128px" TabIndex="2" Width="311px">
        </asp:DropDownList>
        <asp:TextBox ID="txtOperazione" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="12px" MaxLength="6" Style="left: 443px; position: absolute; top: 222px"
            TabIndex="3" Width="5px"></asp:TextBox>
        <asp:TextBox ID="txtRiga" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="11px" MaxLength="6"
            Style="left: 348px; position: absolute; top: 222px" TabIndex="3" Width="5px"></asp:TextBox>
    </div>
    <asp:TextBox ID="txtDescr" runat="server" TextMode="MultiLine" Style="position: absolute;
        top: 163px; left: 102px; width: 447px;"></asp:TextBox>
   
    <asp:HiddenField ID="salvaDocumenti" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />

  

    <script type="text/javascript">
        document.getElementById('txtRiga').style.visibility = 'hidden';
        document.getElementById('txtOperazione').style.visibility = 'hidden';

        function Chiudi() {
            //            CloseModal2(document.getElementById('txtModificato').value);
            document.getElementById('txtModificato').value = '0';
            window.close();
        }

        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }
        function CloseModal2(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }

        function ConfermaEsci() {

            if ((document.getElementById('txtModificato').value == '1')||(document.getElementById('txtModificato').value == '111')) {

                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche.\nUscire senza salvare causerà la perdita delle modifiche!\n Uscire ugualmente? Per non uscire premere ANNULLA.");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                }
                else {
                    if (document.getElementById('caric')) {
                        document.getElementById('caric').style.visibility = 'visible';

                    }
                   Chiudi();
                }
            }
            else {
                if (document.getElementById('caric')) {
                    document.getElementById('caric').style.visibility = 'visible';

                }
                Chiudi();
                        }



        }



    </script>
    </form>
</body>
</html>
