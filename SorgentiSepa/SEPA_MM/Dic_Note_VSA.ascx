<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dic_Note_VSA.ascx.vb"
    Inherits="Dic_Note_VSA" %>
<script type="text/javascript">
<!--

    function MyDialogArguments2() {
        this.Sender = null;
        this.StringValue = "";
    }


    function AggiungiDocumento() {
        dialogArgs = new MyDialogArguments2();
        dialogArgs.StringValue = '';
        dialogArgs.Sender = window;

        mioval = '';
        if (document.getElementById('Dic_Nucleo1_ListBox1') != null) {
            obj1 = document.getElementById('Dic_Nucleo1_ListBox1');
            for (i = 0; i <= obj1.length - 1; i++) {
                str = obj1.options[i].text;
                str1 = obj1.options[i].value;
                mioval = mioval + str.substring(0, 51) + ';' + str1 + '!';
                if (i == 7) { i = obj1.length - 1; }
            }
            window.showModalDialog("com_documenti.aspx?OP=0&COMPONENTE=" + mioval + "&TDOM=" + document.getElementById('tipoRichiesta').value, window, 'status:no;dialogWidth:580px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');

        }
        else {
            window.showModalDialog("com_documenti.aspx?OP=0&COMPONENTE=" + mioval + "&DIC=" + document.getElementById('Dic_Note1_idDic').value + "&TDOM=" + document.getElementById('tipoRichiesta').value, window, 'status:no;dialogWidth:580px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');
        }
    }



    // Funzione javascript per l'inserimento in automatico degli slash nella data
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


    
// -->
</script>
<asp:HiddenField ID="V2" runat="server" />
<asp:HiddenField ID="idDic" runat="server" Value="0" />
<asp:HiddenField ID="documMancante" runat="server" Value="0" />
<asp:HiddenField ID="documAlleg" runat="server" Value="0" />
<div id="not" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    z-index: 200; left: 10px; border-left: lightsteelblue 1px solid; width: 641px;
    border-bottom: lightsteelblue 1px solid; position: absolute; top: 106px; height: 390px;
    background-color: #ffffff">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="4000"
        Style="z-index: 100; left: 10px; position: absolute; top: 16px; height: 30px;
        width: 621px;" TextMode="MultiLine"></asp:TextBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 12px; position: absolute;
        top: 2px" Width="519px">NOTE</asp:Label>
    <asp:Label ID="Label4" runat="server" CssClass="CssLabel" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 12px; position: absolute;
        top: 63px" Width="237px">DOCUMENTAZIONE MANCANTE</asp:Label>
    <asp:CheckBox ID="chkDocManc" runat="server" Font-Names="arial" Font-Size="10pt"
        ForeColor="Black" Style="z-index: 100; left: 208px; position: absolute; top: 61px;
        width: 233px;" Text="Documentaz. mancante presentata" Font-Bold="False" Enabled="False" />
    <asp:Label ID="lblDataManc" runat="server" Font-Bold="True" Font-Names="Times New Roman"
        Font-Size="8pt" Height="18px" Style="z-index: 100; left: 456px; position: absolute;
        top: 65px; width: 50px;" Text="DATA"></asp:Label>
    <asp:TextBox ID="txtDataDocManc" runat="server" Style="z-index: 100; left: 499px;
        position: absolute; top: 60px; width: 80px;" MaxLength="10" ToolTip="Inserire la data di presentazione della documentaz. mancante"></asp:TextBox>
    <div style="overflow: auto; position: absolute; width: 580px; height: 100px; top: 86px;
        left: 6px;">
        <asp:ListBox ID="ListBox1" runat="server" Style="z-index: 106; left: 4px; position: absolute;
            top: 0px; width: 750px; height: 75px;" Font-Names="Courier New" Font-Size="8pt">
        </asp:ListBox>
    </div>
    <asp:Button ID="Button1" runat="server" Style="z-index: 117; left: 592px; position: absolute;
        top: 87px; bottom: 277px; height: 26px;" Text="+" Width="38px" ToolTip="Aggiungi Elemento" />
    <asp:Button ID="Button2" runat="server" Style="z-index: 119; left: 592px; position: absolute;
        top: 121px" Text="-" Width="38px" ToolTip="Elimina Elemento" />
    <div id="docallegati" style="left: 1px; width: 626px; position: absolute; top: 196px;
        height: 160px; background-color: #ffffff; z-index: 200;">
        <table style="width: 100%;">
            <tr>
                <td style="font-family: Arial; font-weight: bold; font-size: 10pt;">
                    Elenco dei documenti allegati alla domanda &nbsp&nbsp&nbsp
                    <asp:Label ID="lblAggDocumenti" runat="server" Font-Bold="True" Font-Names="ARIAL"
                        Font-Size="9pt" Width="290px"></asp:Label>
                    <img id="imgCercaRapida" alt="Ricerca Rapida" onclick="cerca();" src="../Condomini/Immagini/Search_16x16.png"
                        style="border: 1px solid #0000FF; padding: 3px; left: 115px; cursor: pointer;
                        " title="Ricerca Rapida" />
                </td>
            </tr>
            <tr>
                <td class="style1" style="border-bottom-style: dotted; border-bottom-width: thin;
                    border-bottom-color: #C0C0C0">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <div style="overflow: auto; width: 100%; height: 140px">
                        <asp:CheckBoxList ID="chkListDocumenti" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Width="97%">
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</div>
