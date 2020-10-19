<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnnoCalcComp.aspx.vb" Inherits="Contabilita_AnnoCalcComp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Anno Calcolo Compensi Gestore</title>
    
    
<script type="text/javascript">

    function selezAnno() {
        if (navigator.userAgent.toLowerCase().indexOf("msie") != -1) {

            var obj = document.getElementById("cmbAnnoBollette");
            document.getElementById("ANNO").value = obj.options[obj.selectedIndex].innerText;
//            alert(document.getElementById("ANNO").value)
        }
        else {
            var obj = document.getElementById("cmbAnnoBollette");
            document.getElementById("ANNO").value = obj.options[obj.selectedIndex].text;
//            alert(document.getElementById("ANNO").value)
        } 
        }

        function ChiChiamare() {
            if (document.getElementById("CHIAMATA").value == "POLI") {
                window.open('CompensiAler.aspx?ANNO=' + document.getElementById('ANNO').value + '', 'Compensi', '');
            }
            
            else {
                window.open('CompMensAler.aspx?ANNO=' + document.getElementById('ANNO').value + '', 'CompensiMensili', '');
                }
        }
        
</script>

</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat">
    <form id="form1" runat="server">
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Calcolo 
        Rimborso Spese
            Gestore</span></strong><br />
        <br />
        <strong><span style="font-size: 10pt; color: #801f1c; font-family: Arial">
            <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Size="10pt" ForeColor="Black" Style="z-index: 100; left: 227px; position: absolute;
                top: 148px; width: 38px;">Anno</asp:Label>
            <asp:DropDownList ID="cmbAnnoBollette" runat="server" Style="left: 269px; position: absolute;
                top: 146px" Width="110px" onclick="selezAnno();" TabIndex="1">
            </asp:DropDownList>
            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="z-index: 10; left: 13px; width: 719px; top: 598px; height: 13px"
                Text="Label" Visible="False" Width="100%"></asp:Label></span></strong>
        <img id="IMG1" alt="Avvia il calcolo dei compensi dovuti al Gestore" 
            src="IMMCONTABILITA/Img_Calcola.png"onclick="ChiChiamare();"   
            style="left: 450px; cursor: hand; position: absolute;top: 272px" />
        <asp:HiddenField ID="ANNO" runat="server" />
        <asp:HiddenField ID="CHIAMATA" runat="server" />
        <strong><span style="font-size: 10pt; color: #801f1c; font-family: Arial">
            <asp:Label ID="lblSottotitolo" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="10pt"
                ForeColor="#0066FF" Style="z-index: 10; left: 9px; width: 719px; top: 48px; height: 13px; position: absolute;"
                
            
            Text="Sceglierel'anno per il quale si vuole calcolare l'ammontare del Rimborso Spese dovuto al Gestore " 
            Width="100%"></asp:Label></span></strong>
        <p>
                        &nbsp;</p>
        <p>
                        &nbsp;</p>
        <p>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
            
                Style="z-index: 106; left: 544px; position: absolute; top: 273px; bottom: 290px;" 
                ToolTip="Esci" TabIndex="9" />
        </p>
    </form>
</body>
</html>
