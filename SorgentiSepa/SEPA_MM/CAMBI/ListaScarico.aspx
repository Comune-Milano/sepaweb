<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaScarico.aspx.vb" Inherits="CAMBI_ListaScarico" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Elenco Domande da scaricare</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <span style="font-size: 10pt; font-family: Courier New">
            <br />
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                Height="22px" Style="z-index: 100; left: 24px; position: static; top: 17px" Text="Label"
                Width="100%"></asp:Label><br />
            <br />
            &nbsp;&nbsp; PG &nbsp; &nbsp; &nbsp; &nbsp;DATA &nbsp; &nbsp; NOMINATIVO &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp;&nbsp; ISBARC/R</span><br />
        <asp:CheckBoxList ID="CheckOperatori" runat="server" BackColor="#FFFFC0" BorderColor="Black"
            Font-Names="Courier New" Font-Size="8pt" RepeatColumns="1" Style="z-index: 100; left: 0px;
            position: static; top: 0px" Width="100%">
        </asp:CheckBoxList></div>
        <br />
        <asp:Button ID="btnStampa" runat="server" Style="z-index: 100; left: 0px; position: static;
            top: 0px" Text="Stampa" />
        &nbsp;&nbsp; &nbsp;
        <asp:Button ID="btnTutti" runat="server" Style="z-index: 100; left: 0px; position: static;
            top: 0px" Text="Seleziona Tutti" />
        &nbsp;&nbsp;
        <asp:Button ID="btnScarica" runat="server" Style="z-index: 100; left: 0px; position: static;
            top: 0px" Text="Scarica Selezionati" BackColor="Red" Font-Bold="True" ForeColor="White" />
        &nbsp; &nbsp;
        <asp:Button ID="btnChiudi" runat="server" Style="z-index: 100; left: 0px; position: static;
            top: 0px" Text="Chiudi" />
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 100; left: 0px; position: static; top: 0px" Text="ATTENZIONE"
            Width="100%"></asp:Label><br />
        <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 100;
            left: 0px; position: static; top: 0px" Text="Se fosse necessario modificare la lista delle domande da inviare al Comune di Milano nell'ambito della stessa operazione, il pulsante &quot;Scarica Selezionati&quot; può essere premuto più volte mantenendo inalterato il numero di distinta eventualmente già assegnato dal sistema. Qualora non fosse stata generata alcuna distinta e per terminare l'operazione, premere direttamente il pulsante 'CHIUDI'. Per annullare l'operazione avendo già generato una distinta, rigenerare la stessa premendo il pulsante 'Scarica selezionati' assicurandosi che nessuna domanda risulti selezionata e premendo successivamente il pulsante 'Chiudi'. Il numero della distinta già assegnato dal sistema non sarà più riutilizzabile.'"
            Width="100%"></asp:Label>
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 100; left: 0px; position: static; top: 0px" Text=" DOPO AVER PREMUTO IL PULSANTE &quot;CHIUDI&quot;,  INVECE, NON SARA' PIU' POSSIBILE APPORTARE MODIFICHE!"
            Width="100%"></asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            Style="z-index: 100; left: 0px; position: static; top: 0px" Text="E' possibile scaricare al massimo 40 Domande per volta!"
            Width="100%"></asp:Label>
  
    </form>
</body>
</html>

