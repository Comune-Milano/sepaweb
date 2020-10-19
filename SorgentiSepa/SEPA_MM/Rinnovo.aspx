<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Rinnovo.aspx.vb" Inherits="Rinnovo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript">
    var Uscita;
    Uscita = 0;
    function inAssegn() {
       
         if (document.getElementById('inAssegnazione')) {
            
             if (document.getElementById('inAssegnazione').value == '1') {
                 document.getElementById('inAssegnazione').value = '0';
             }
             else {
                 document.getElementById('inAssegnazione').value = '1';
             }
         }
        
    }
    function Conferma() {
        var annulla = window.confirm("Attenzione, confermi di avere a disposizione la situazione reddituale del <%=AnnoIsee%> e di voler integrare questa domanda? L'operazione non è reversibile!");
        if (annulla) {
            document.getElementById('t1').value = '1';
            return annulla;
        }
        else {
            document.getElementById('t1').value = '0';
            window.alert("Operazione Annullata!");
        }
    }

    function VisDomanda() {
        var a
        a = document.getElementById('TextBox1').value;
        window.open('domanda.aspx?INASS=' + document.getElementById('inAssegnazione').value + '&SS=1&ID=<%=sValoreID%>&ID1=-1&PROGR=-1&LE=1&INT=1&DER=<%=sValoreDE%>&VER=' + a, '', 'top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');
    }

    function VisDichiarazione() {
        window.open('max.aspx?ID=<%=sValoreDI%>&LE=1&INT=1', '', 'top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');
    }

    function CONSDichiarazione() {
        window.open('CONS/max.aspx?ID=<%=sValoreDI%>&LE=1', '', 'top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');
    }


    function CONSDomanda() {
        window.open('CONS/domanda.aspx?ID=<%=sValoreID%>&ID1=-1&PROGR=-1&LE=1&APP=0', '', 'top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');
    }

    function cambia() {
        if (document.getElementById('TextBox1').value == '0') {
            document.getElementById('TextBox1').value = '1';
        }
        else {
            document.getElementById('TextBox1').value = '0';
           
        }
    }

     

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rinnovo/Integrazione Domanda</title>
    
    <style type="text/css">
        .auto-style1 {
            z-index: 104;
            left: 6px;
            position: absolute;
            top: 360px;
        }
    </style>
    
</head>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server" >
        <div>
            <asp:ImageButton ID="btnAnnulla" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Esci_Grande.png"
                Style="z-index: 100; left: 515px; position: absolute; top: 360px" TabIndex="6" ToolTip="Esci" />
            <asp:ImageButton ID="btnDomanda" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_ModDomanda_Grande.png"
                Style="z-index: 101; left: 109px; position: absolute; top: 360px" TabIndex="4"
                Visible="False" OnClientClick="VisDomanda();" ToolTip="Modifica Domanda" />
            <asp:ImageButton ID="VisDomanda" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_VisDomanda_Grande.png"
                Style="z-index: 102; left: 310px; position: absolute; top: 360px" TabIndex="4" OnClientClick="CONSDomanda();" ToolTip="Visualizza Domanda" />
            <asp:ImageButton ID="VisDichiarazione" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_VisDichiarazione_Grande.png"
                Style="z-index: 103; left: 212px; position: absolute; top: 360px" TabIndex="3" OnClientClick="CONSDichiarazione();" ToolTip="Visualizza Dichiarazione" />
            <asp:ImageButton ID="btnDichiarazione" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_ModDichiarazione_Grande.png" TabIndex="3"
                Visible="False" OnClientClick="VisDichiarazione();" ToolTip="Modifica Dichiarazione" CssClass="auto-style1" />
            <asp:ImageButton ID="btnSalva" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_SalvaContinua_Grande.png"
                Style="z-index: 105; left: 412px; position: absolute; top: 360px" TabIndex="5" OnClientClick="Conferma();" ToolTip="Salva e Continua" />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                Style="z-index: 106; left: 12px; position: absolute; top: 14px" Width="569px"></asp:Label>
            <asp:Label ID="lblData" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 107; left: 111px; position: absolute; top: 101px"
                Text="Label" Font-Bold="True"></asp:Label>
            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                ForeColor="#0000C0" Style="z-index: 108; left: 232px; position: absolute; top: 101px"
                Text="Label" Visible="False"></asp:Label>
            &nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 109; left: 14px; position: absolute; top: 133px"
            Text="Motivazione"></asp:Label>
            <asp:Label ID="lblErrore" runat="server" Font-Names="arial"
                Font-Size="8pt" Style="z-index: 109; left: 284px; position: absolute; top: 473px; width: 298px;"
                Font-Bold="True" ForeColor="#CC0000" Visible="False"></asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="8pt" Style="z-index: 110; left: 14px; position: absolute; top: 101px"
                Text="Data Integrazione"></asp:Label>
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                ForeColor="Red"
                Text="ATTENZIONE, Si ricorda di elaborare e stampare COMUNQUE  la domanda dopo aver premuto il pulsante SALVA e CONTINUA poichè l'ISBARC/R sarà azzerato!" Width="613px"
                Style="z-index: 111; left: 6px; position: absolute; top: 220px;"></asp:Label>
            &nbsp;
        <asp:TextBox ID="txtMotivo" runat="server" BorderStyle="Solid" BorderWidth="1px"
            Font-Names="arial" Font-Size="8pt" Height="65px" Rows="5" Style="z-index: 112; left: 75px; position: absolute; top: 133px"
            TabIndex="2" TextMode="MultiLine"
            Width="504px"></asp:TextBox>
            <asp:Image ID="Image1" runat="server" ImageUrl="~/ImmMaschere/Avviso_Elabora.jpg" Visible="False" Style="z-index: 113; left: 64px; position: absolute; top: 385px; " />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/IMG/Alert.gif" Style="z-index: 114; left: 206px; position: absolute; top: 100px"
                Visible="False" />
            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 115; left: 31px; position: absolute; top: 62px"
                Text="ATTENZIONE, Integrare la domanda solo se si ha a disposizione la situazione reddituale dell'anno"
                Width="531px"></asp:Label>
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 121; left: 102px; position: absolute; top: 77px"
                Text="Puoi visualizzare la dichiarazione e la domanda utilizzando le funzioni sottostanti."
                Width="452px"></asp:Label>
            <asp:Label ID="Label8" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                ForeColor="Red" Style="z-index: 117; left: 564px; position: absolute; top: 60px"
                Text="2006" Width="43px"></asp:Label>
            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                ForeColor="#0000C0" Style="z-index: 118; left: 31px; position: absolute; top: 62px"
                Visible="False" Width="398px"></asp:Label>
            <asp:Image ID="Image3" runat="server" ImageUrl="~/IMG/Alert.gif" Style="z-index: 119; left: 8px; position: absolute; top: 61px" />
            <asp:TextBox ID="t1" runat="server" Style="z-index: 120; left: 589px; position: absolute; top: 582px"
                Width="43px"></asp:TextBox>
            <asp:CheckBox ID="CHVERIFICA" runat="server" Font-Names="ARIAL" Font-Size="10pt" Text="Selezionare questa casella  se trattasi di verifica mantenimento requisiti. Se la Domanda, dopo essere stata elaborata, risulterà essere in possesso dei requisiti, sarà inviata in ASSEGNAZIONE, e pronta per essere abbinata ad un alloggio." Width="614px" Enabled="False"
                Style="left: 3px; position: absolute; top: 266px;" />
            <asp:CheckBox ID="chNoAssegnazione" runat="server" Font-Names="ARIAL" AutoPostBack="True" Font-Size="10pt" Text="Non portare IN ASSEGNAZIONE" Width="614px" Enabled="False" Style="left: 3px; position: absolute; top: 322px;" />

            <asp:TextBox ID="TextBox1" runat="server" Style="left: 527px; position: absolute; top: 580px"
                Width="48px">0</asp:TextBox>
            <asp:HiddenField ID="inAssegnazione" runat="server" Value="1" />
        </div>
    </form>
    <script type="text/javascript">
          var a
          a = document.getElementById('TextBox1').value;
    </script>
</body>
</html>
