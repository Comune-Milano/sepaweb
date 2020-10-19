<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Contratto.aspx.vb" Inherits="ASS_Contratto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 0;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Contratto</title>
</head>
<body>
<script type="text/javascript">

    function Verifica() {
        if (document.getElementById('HiddenField2').value != 'NON ANCORA INSERITO') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sicuri di voler inserire gli estremi del contratto e rimuovere dalla graduatoria questa domanda? Premere OK per confermare o ANNULLA per non procedere.");
            if (chiediConferma == false) {
                document.getElementById('HiddenField1').value = '0';
            }
            else {
                document.getElementById('HiddenField1').value = '1';
            }
        }
        else {
            alert('Provvedimento di Assegnazione non ancora inserito!');
        }
    }

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
    <form id="form1" runat="server" 
    defaultfocus="TXTCONTRATTO">
    <div>
            <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
            left: 9px; position: absolute; top: 42px" Text="PG:"></asp:Label>
        <asp:Label ID="Label12" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 101;
            left: 12px; position: absolute; top: 300px" Text="NUMERO CONTRATTO:"></asp:Label>
        <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 102;
            left: 12px; position: absolute; top: 331px" Text="DATA:"></asp:Label>
        <asp:Label ID="lblOfferta" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 103; left: 9px; position: absolute; top: 9px" Width="184px"></asp:Label>
        <asp:Label ID="lblPG" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 104; left: 80px; position: absolute; top: 43px"
            Text="Label" Width="120px"></asp:Label>
        <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 106;
            left: 201px; position: absolute; top: 43px" Text="Nominativo"></asp:Label>
        <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 107;
            left: 9px; position: absolute; top: 63px" Text="ISBARC/R"></asp:Label>
        <asp:Label ID="lblNominativo" runat="server" Font-Bold="True" Font-Names="arial"
            Font-Size="10pt" ForeColor="#0000C0" Style="z-index: 108; left: 272px; position: absolute;
            top: 43px" Text="Label" Width="262px"></asp:Label>
        <asp:Label ID="lblIsbarcr" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 109; left: 80px; position: absolute; top: 63px"
            Text="Label" Width="104px"></asp:Label>
            <asp:Label ID="Label18" runat="server" 
                Text="Inserendo gli estremi contrattuali, la domanda verrà cancellata dalla graduatoria ERP" 
                style="position:absolute; top: 398px; left: 14px;" ForeColor="#3333CC" 
                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"></asp:Label>
            <asp:Label ID="Label15" runat="server" 
                style="position:absolute; top: 421px; left: 14px; width: 546px;" ForeColor="#CC0000" 
                Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt" Visible="False"></asp:Label>
        &nbsp;&nbsp;
        <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 110;
            left: 202px; position: absolute; top: 63px" Text="N. Comp."></asp:Label>
        &nbsp;
        <asp:Label ID="lblComp" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
            ForeColor="#0000C0" Style="z-index: 111; left: 272px; position: absolute; top: 63px"
            Text="Label" Width="104px"></asp:Label>
        &nbsp; &nbsp; &nbsp;
        <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 112;
            left: 10px; position: absolute; top: 114px" Text="Codice"></asp:Label>
        <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 113;
            left: 338px; position: absolute; top: 114px" Text="Zona"></asp:Label>
        <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 114;
            left: 10px; position: absolute; top: 136px" Text="Proprietà"></asp:Label>
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 115; left: 10px; position: absolute; top: 89px" Width="523px">DATI ALLOGGIO OFFERTO</asp:Label>
        <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 116; left: 12px; position: absolute; top: 206px" Width="523px">ESTREMI PROVVEDIMENTO ASSEGNAZIONE</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 116; left: 12px; position: absolute; top: 266px" Width="523px">ESTREMI CONTRATTO</asp:Label>
        <asp:Label ID="lblCodice" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 117;
            left: 67px; position: absolute; top: 114px" Width="29%"></asp:Label>
        <asp:Label ID="lblProprieta" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 118;
            left: 67px; position: absolute; top: 136px" Width="29%"></asp:Label>
        <asp:Label ID="lblZona" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 119;
            left: 373px; position: absolute; top: 114px" Width="14%"></asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 120;
            left: 10px; position: absolute; top: 157px" Text="Gestore"></asp:Label>
        <asp:Label ID="lblGestore" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 121;
            left: 67px; position: absolute; top: 157px" Width="55%"></asp:Label>
        <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 122;
            left: 11px; position: absolute; top: 234px" Text="Numero/Data"></asp:Label>
        <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="10pt" Style="z-index: 122;
            left: 11px; position: absolute; top: 178px" Text="Stato"></asp:Label>
        <asp:Label ID="lblProvvedimento" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 123;
            left: 101px; position: absolute; top: 234px; height: 17px; width: 44%;">NON ANCORA INSERITO</asp:Label>
        <asp:Label ID="lblStato" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 123;
            left: 67px; position: absolute; top: 178px" Width="50%"></asp:Label>
        &nbsp; &nbsp;
        <asp:Label ID="lblIdAll" runat="server" Style="z-index: 124; left: 456px; position: absolute;
            top: 133px" Text="Label" Visible="False"></asp:Label>
        &nbsp;
        <asp:Label ID="lblScad" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 125; left: 202px; position: absolute; top: 9px" Width="328px"></asp:Label>
        &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="lblRelazione" runat="server" Style="z-index: 126; left: 506px; position: absolute;
            top: 133px" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="LblDataPG" runat="server" Style="z-index: 127; left: 533px; position: absolute;
            top: 113px" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblTipoPratica" runat="server" Style="z-index: 128; left: 499px; position: absolute;
            top: 67px" Text="Label" Visible="False"></asp:Label>
        <asp:TextBox ID="TXTCONTRATTO" runat="server" Style="z-index: 130; left: 162px; position: absolute;
            top: 297px" TabIndex="1"></asp:TextBox>
        <asp:TextBox ID="TXTDATA" runat="server" Style="z-index: 131; left: 162px; position: absolute;
            top: 330px" TabIndex="1" Width="71px"></asp:TextBox>
        <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 132;
            left: 12px; position: absolute; top: 363px" Text="DATA DECOR.:"></asp:Label>
        <asp:TextBox ID="TXTDATADEC" runat="server" Style="z-index: 134; left: 162px; position: absolute;
            top: 361px" TabIndex="2" Width="71px"></asp:TextBox>
    </div>
    <asp:ImageButton style="position:absolute; top: 483px; left: 532px;" 
        ID="ImgEsci" runat="server" 
        ImageUrl="~/NuoveImm/Img_EsciCorto.png" />
    <asp:ImageButton style="position:absolute; top: 484px; left: 435px;" 
        ID="ImgSalva" runat="server" 
        ImageUrl="~/NuoveImm/img_SalvaModelli.png" onclientclick="Verifica();" />
    <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
    <asp:HiddenField ID="HiddenField2" runat="server" Value="NON ANCORA INSERITO" />
    </form>
    <script type ="text/javascript">
        
    </script>
</body>

</html>
