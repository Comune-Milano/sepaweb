<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SchedaAppuntamento.aspx.vb"
    Inherits="ANAUT_SchedaAppuntamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        //if (event.keyCode==13) 
        //{  
        //alert('Usare il tasto <Avvia Ricerca>');
        //history.go(0);
        //event.keyCode=0;
        //}  
    }

</script>
<script type="text/javascript">
    //document.onkeydown = $onkeydown;


    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                // don't insert last non-numeric character
                if (window.event) {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
            }
        }
        else {
            if (obj.value.length == 2) {
                obj.value += "/";
            }
            else if (obj.value.length == 5) {
                obj.value += "/";
            }
            else if (obj.value.length > 9) {
                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                if (selText.length == 0) {
                    // make sure the field doesn't exceed the maximum length
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }
    }


</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Ricerca</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <style type="text/css">
        #Contenitore
        {
            left: 14px;
        }
    </style>
</head>
<body bgcolor="#f2f5f1">
    <form id="Form1" method="post" runat="server">
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
        position: absolute; top: 0px">
        <tr>
            <td style="width: 670px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Scheda
                    Convocazione </strong>
                    <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label>
                </span>
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
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:HiddenField ID="pagine" runat="server" Value="0" />
                <asp:HiddenField ID="chiamante" runat="server" />
                <br />
                <img alt="Indietro" src="../NuoveImm/Img_IndietroGrande.png" style="cursor: pointer;
                    position: absolute; top: 512px; left: 444px;" onclick="indietro();" id="imgIndietro" />
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 557px; position: absolute; top: 511px" TabIndex="6"
        ToolTip="Home" />
    <asp:Label ID="Label10" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 405px; position: absolute; top: 94px;">Scheda AU</asp:Label>
    <asp:Label ID="lblSospesa" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 405px; position: absolute; top: 119px; right: 1000px;
        height: 45px;" Visible="False">Scheda AU Sospesa per:</asp:Label>
    <asp:Label ID="lblConvocazione" runat="server" Font-Size="9pt" Font-Names="Arial"
        Font-Bold="True" Style="text-align: center; z-index: 110; left: 473px; position: absolute;
        top: 92px; width: 188px; height: 16px;" BackColor="#FFFF99" BorderStyle="Solid"
        BorderWidth="1px">--</asp:Label>
    <asp:Label ID="lblSospensione" runat="server" Font-Size="9pt" Font-Names="Arial"
        Font-Bold="True" Style="text-align: center; z-index: 110; left: 473px; position: absolute;
        top: 117px; width: 188px; height: 51px;" BackColor="#FFFF99" BorderStyle="Solid"
        BorderWidth="1px" Visible="False">--</asp:Label>
    <asp:Label ID="lblAppuntamento" runat="server" Font-Size="9pt" Font-Names="Arial"
        Font-Bold="True" Style="text-align: center; z-index: 110; left: 97px; position: absolute;
        top: 169px; width: 69px;" BackColor="#FFFFCC" BorderStyle="Solid" BorderWidth="1px"
        Height="16px"></asp:Label>
    <asp:Label ID="lblOraApp" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="True"
        Style="text-align: center; z-index: 110; left: 97px; position: absolute; top: 189px;
        width: 69px;" BackColor="#FFFFCC" BorderStyle="Solid" BorderWidth="1px" Height="16px"></asp:Label>
    <asp:Label ID="lblFiliale" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="True"
        Style="text-align: center; z-index: 110; left: 223px; position: absolute; top: 189px;
        width: 172px;" BackColor="#FFFFCC" BorderStyle="Solid" BorderWidth="1px" Height="32px"></asp:Label>
    <asp:Label ID="lblSportello" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="True"
        Style="text-align: center; z-index: 110; left: 473px; position: absolute; top: 189px;
        width: 187px;" BackColor="#FFFFCC" BorderStyle="Solid" BorderWidth="1px" Height="32px"></asp:Label>
        <asp:Label ID="LBL392" runat="server" Font-Size="12pt" Font-Names="Arial" Font-Bold="True"
        Style="text-align: center; z-index: 110; left: 472px; position: absolute; top: 24px;
        width: 189px;" BackColor="Maroon" BorderStyle="Solid" BorderWidth="1px" Height="20px"
        ForeColor="White" BorderColor="Black" Visible="False">R.U. 392/78</asp:Label>
        <asp:Label ID="LBL431" runat="server" Font-Size="12pt" Font-Names="Arial" Font-Bold="True"
        Style="text-align: center; z-index: 110; left: 472px; position: absolute; top: 24px;
        width: 189px;" BackColor="Maroon" BorderStyle="Solid" BorderWidth="1px" Height="20px"
        ForeColor="White" BorderColor="Black" Visible="False">R.U. 431/98</asp:Label>
    <asp:Label ID="lblStato" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="True"
        Style="text-align: center; z-index: 110; left: 472px; position: absolute; top: 67px;
        width: 189px;" BackColor="#009933" BorderStyle="Solid" BorderWidth="1px" Height="16px"
        ForeColor="White" BorderColor="Black"></asp:Label>
    <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 15px; position: absolute; top: 120px">Cod.Contratto</asp:Label>
    <asp:Label ID="Label16" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 275px; position: absolute; top: 119px">Cod.Contratto</asp:Label>
    <asp:Label ID="Label6" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 15px; position: absolute; top: 70px">Cognome</asp:Label>
    <asp:Label ID="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 15px; position: absolute; top: 95px">Nome</asp:Label>
    <asp:Label ID="lblCognome" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 95px; position: absolute; top: 68px; width: 300px;"
        BackColor="White" BorderStyle="Solid" BorderWidth="1px" Height="16px"></asp:Label>
    <asp:Label ID="lblNome" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 95px; position: absolute; top: 93px; width: 300px;"
        BackColor="White" BorderStyle="Solid" BorderWidth="1px" Height="16px"></asp:Label>
    <asp:Label ID="lblCodContratto" runat="server" Font-Size="8pt" Font-Names="Arial"
        Font-Bold="False" Style="z-index: 110; left: 95px; position: absolute; top: 117px;
        width: 164px;" BackColor="White" BorderStyle="Solid" BorderWidth="1px" Height="16px"></asp:Label>
    <asp:Label ID="Label8" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 16px; position: absolute; top: 150px">APPUNTAMENTO</asp:Label>
    <asp:Label ID="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 16px; position: absolute; top: 172px">GIORNO</asp:Label>
    <asp:Label ID="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 16px; position: absolute; top: 191px">ORA</asp:Label>
    <asp:Label ID="Label9" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 177px; position: absolute; top: 191px">SEDE T.</asp:Label>
    <asp:Label ID="Label12" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 404px; position: absolute; top: 191px">SPORTELLO</asp:Label>
    <asp:Label ID="Label7" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
        Style="z-index: 110; left: 406px; position: absolute; top: 69px">STATO</asp:Label>
    <asp:Label ID="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 15px; position: absolute; top: 215px">STORICO CONVOCAZIONE</asp:Label>
    <div id="Contenitore" style="visibility: visible; overflow: auto; position: absolute;
        width: 648px; height: 112px; top: 235px">
        <asp:DataGrid ID="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
            Font-Size="8pt" PageSize="8" Style="z-index: 105; left: 0px; position: absolute;
            top: 0px; width: 647px; height: 79px;" Font-Bold="False" Font-Italic="False" Font-Overline="False"
            Font-Strikeout="False" Font-Underline="False" GridLines="None" CellPadding="1"
            ForeColor="#333333">
            <EditItemStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="#507CD1"
                ForeColor="White"></HeaderStyle>
            <AlternatingItemStyle BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="DATA_APP" HeaderText="DATA-ORA"></asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
            </Columns>
            <ItemStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:DataGrid>
    </div>
    <asp:Label ID="lblDatiUltimoNucleo" runat="server" Font-Size="8pt" 
        Font-Names="Arial" Font-Bold="True"
        Style="z-index: 110; left: 15px; position: absolute; top: 380px"></asp:Label>
    <div style="border: 1px solid #000000; background-color: #FFFFCC; position: absolute;
        top: 430px; left: 14px; width: 643px; height: 68px;">
        <asp:ImageButton ID="imgReimposta" runat="server" ImageUrl="~/NuoveImm/img_ReimpostaAppuntamento.png"
            OnClientClick="ReimpostaAppuntamento();" ToolTip="Fissa altro appuntamento per questo inquilino"
            Style="position: absolute; top: 12px; left: 524px;" Height="30px" Width="30px" />
        <asp:Label ID="Label15" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
            Style="z-index: 110; left: 471px; position: absolute; top: 50px">FISSA ALTRO APPUNTAMENTO</asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
            Style="z-index: 110; left: 296px; position: absolute; top: 50px">ANNULLA APPUNTAMENTO</asp:Label>
        <asp:ImageButton ID="imgAnnulla" runat="server" ImageUrl="~/NuoveImm/img_AnnullaAppuntamento.png"
            OnClientClick="AnnullaAppuntamento();" ToolTip="Sospendi appuntamento" Style="position: absolute;
            top: 14px; left: 337px;" Height="30px" Width="30px" />
        <asp:ImageButton ID="imgSposta" runat="server" ImageUrl="~/NuoveImm/img_SpostaAppuntamento.png"
            OnClientClick="SpostaAppuntamento();" ToolTip="Sposta appuntamento ad altra data"
            Style="position: absolute; top: 13px; left: 171px;" Height="30px" Width="30px" />
        <asp:Label ID="Label11" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
            Style="z-index: 110; left: 8px; position: absolute; top: 50px; height: 14px;
            width: 104px;">CREA SCHEDA AU</asp:Label>
        <asp:ImageButton ID="imgCreaAU" runat="server" ImageUrl="~/NuoveImm/img_Nuova_AU.png"
            OnClientClick="CreaAnagrafe();" ToolTip="Crea una nuova scheda anagrafe utenza"
            Style="position: absolute; top: 10px; left: 29px;" Height="30px" Width="30px" />
        <asp:Label ID="Label14" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="True"
            Style="z-index: 110; left: 130px; position: absolute; top: 50px">SPOSTA APPUNTAMENTO</asp:Label>
    </div>
    <script type="text/javascript">
        function indietro() {
            history.go(document.getElementById('pagine').value * -1);
        }
        function disabilitaMinore(e) {
            var key;
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 226)
                return false;
            else
                return true;
        }

        function CreaAnagrafe() {
            if (document.getElementById('iddich').value == '') {
                var chiediConferma;
                chiediConferma = window.confirm("Attenzione...Sei sicuro di volere creare una nuova scheda anagrafe utenza?");
                if (chiediConferma == true) {
                    if (document.getElementById('DicAUSI').value == '1') {
                        if (document.getElementById('imgCreaAU')) {
                            document.getElementById('imgCreaAU').style.visibility = 'hidden';
                            document.getElementById('imgCreaAU').style.position = 'absolute';
                            document.getElementById('imgCreaAU').style.left = '-100px';
                            document.getElementById('imgCreaAU').style.display = 'none';
                        }
                        window.showModalDialog('CreaAU_new.aspx?AUS=1&IC=' + document.getElementById('IDC').value + '&IDA=' + document.getElementById('IDA').value + '&IDC=' + document.getElementById('txtIdContratto').value + '&S392=' + document.getElementById('S392').value, window, 'status:no;dialogWidth:800;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
                    }
                    else {
                        if (document.getElementById('imgCreaAU')) {
                            document.getElementById('imgCreaAU').style.visibility = 'hidden';
                            document.getElementById('imgCreaAU').style.position = 'absolute';
                            document.getElementById('imgCreaAU').style.left = '-100px';
                            document.getElementById('imgCreaAU').style.display = 'none';
                        }
                        window.showModalDialog('CreaAU_new.aspx?AUS=0&IC=' + document.getElementById('IDC').value + '&IDA=' + document.getElementById('IDA').value + '&IDC=' + document.getElementById('txtIdContratto').value + '&S392=' + document.getElementById('S392').value, window, 'status:no;dialogWidth:800px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
                    }
                }
                else {

                    alert('Operazione annullata!');
                }
            }
            else {
                //window.open('max.aspx?ID=' + document.getElementById('iddich').value + '&CHIUDI=1&CH=1', 'Dettagli', 'height=540,top=200,left=350,width=670');
                window.open('DichAUnuova.aspx?ID=' + document.getElementById('iddich').value + '&CHIUDI=1&CH=1', 'Dettagli', '')
            }
        }

        function SpostaAppuntamento() {

            window.showModalDialog('SituazioneAppuntamenti.aspx?T=0&F=' + document.getElementById('FILIALE').value + '&O=' + document.getElementById('ORA').value + '&G=' + document.getElementById('GIO').value + '&IC=' + document.getElementById('IDC').value + '&IDA=' + document.getElementById('IDA').value + '&SP=' + document.getElementById('SPORTELLO').value + '&GR=' + document.getElementById('GRUPPO').value, window, 'status:no;dialogWidth:950px;dialogHeight:700px;dialogHide:true;help:no;scroll:no');

        }


        function AnnullaAppuntamento() {
            if (document.getElementById('presa').value == '0') {
                if (document.getElementById('STATOC').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sei sicuro di volere RIPRISTINARE questo appuntamento?");
                    if (chiediConferma == true) {

                        window.showModalDialog('AnnullaAppuntamento.aspx?T=1&IC=' + document.getElementById('IDC').value + '&IDA=' + document.getElementById('IDA').value + '&IDC=' + document.getElementById('txtIdContratto').value, window, 'status:no;dialogWidth:400px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
                    }
                    else {

                        alert('Operazione annullata!');
                    }
                }
                else {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sei sicuro di volere SOSPENDERE questo appuntamento?");
                    if (chiediConferma == true) {

                        window.showModalDialog('AnnullaAppuntamento.aspx?IC=' + document.getElementById('IDC').value + '&IDA=' + document.getElementById('IDA').value + '&IDC=' + document.getElementById('txtIdContratto').value, window, 'status:no;dialogWidth:400px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
                    }
                    else {

                        alert('Operazione annullata!');
                    }
                }
            }
            else {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sei sicuro di volere PRENDERE IN CARICO questo appuntamento?");
                if (chiediConferma == true) {

                    window.showModalDialog('PrendiAppuntamento.aspx?IC=' + document.getElementById('IDC').value + '&IDA=' + document.getElementById('IDA').value + '&IDC=' + document.getElementById('txtIdContratto').value, window, 'status:no;dialogWidth:400px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
                }
                else {

                    alert('Operazione annullata!');
                }
            }
        }

        function ReimpostaAppuntamento() {
            //alert('Non disponibile');
            window.showModalDialog('SituazioneAppuntamenti.aspx?T=1&F=' + document.getElementById('FILIALE').value + '&O=' + document.getElementById('ORA').value + '&G=' + document.getElementById('GIO').value + '&IC=' + document.getElementById('IDC').value + '&IDA=' + document.getElementById('IDA').value + '&SP=' + document.getElementById('SPORTELLO').value + '&GR=' + document.getElementById('GRUPPO').value, window, 'status:no;dialogWidth:950px;dialogHeight:700px;dialogHide:true;help:no;scroll:no');
        }

           

    </script>
    <asp:HiddenField ID="txtIdContratto" runat="server" />
    <asp:HiddenField ID="IDA" runat="server" />
    <asp:HiddenField ID="IDC" runat="server" />
    <asp:HiddenField ID="SPORTELLO" runat="server" />
    <asp:HiddenField ID="GIO" runat="server" />
    <asp:HiddenField ID="ORA" runat="server" />
    <asp:HiddenField ID="FILIALE" runat="server" />
    <asp:HiddenField ID="STATOC" runat="server" />
    <asp:HiddenField ID="presa" runat="server" />
    <asp:HiddenField ID="iddich" runat="server" />
    <asp:HiddenField ID="DicAUSI" runat="server" />
    <asp:HiddenField ID="GRUPPO" runat="server" />
    <asp:HiddenField ID="S392" runat="server" />
    <asp:HiddenField ID="L431" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
        if (document.getElementById('chiamante').value) {
            if (document.getElementById('chiamante').value == '1') {
                document.getElementById('imgIndietro').style.visibility = 'hidden';
                document.getElementById('imgIndietro').style.position = 'absolute';
                document.getElementById('imgIndietro').style.left = '-100px';
                document.getElementById('imgIndietro').style.display = 'none';
            }
        }
    </script>
</body>
</html>
