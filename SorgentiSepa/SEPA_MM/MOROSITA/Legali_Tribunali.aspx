<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Legali_Tribunali.aspx.vb" Inherits="MOROSITA_Legali_Tribunali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>GESTIONE TRIBUNALI DI COMPETENZA</title>
</head>
<body>

    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 801px;">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                        <br />
                        &nbsp;Tribunali Competenti per Comune<br />
                    </strong></span>
                    <table width="97%">
                        <tbody>
                            <tr>
                                <td style="width: 50px">
                                    &nbsp; &nbsp; &nbsp;
                                </td>
                                <td style="width: 100px">
                                    <br />
                                    <asp:ImageButton ID="btnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Aggiungi.png"
                                                        Style="z-index: 106; left: 216px; top: 480px" ToolTip="Aggiunge un Nuovo Tribunale Competente" OnClientClick="document.getElementById('TextBox1').value='1'; document.getElementById('InsVariazione').style.visibility='visible';" /></td>
                                <td style="width: 101px">
                                    <br />
                                    <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png"
                                                        Style="z-index: 103; left: 360px; top: 480px" ToolTip="Modifica il Tribunale Selezionato" /></td>
                                <td style="width: 101px">
                                    <br />
                                    <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                                                        Style="z-index: 102; right: 931px; left: 400px; top: 480px" ToolTip="Elimina il Tribunale Selezionato" OnClientClick="ConfermaAnnullo();" /></td>
                                <td width="600">
                                </td>
                                <td style="width: 193px">
                                    <br />
                                    <img id="ImgEventi" alt="Eventi" border="0" onclick="window.open('Report/Eventi_Tab.aspx?ID=TAB_TRIBUNALI_COMPETENTI','Report', '');"
                                        src="../NuoveImm/Img_Eventi.png" style="left: 616px; cursor: pointer; top: 32px" /></td>
                                <td style="width: 93px">
                                    <br />
                                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                                                        Style="z-index: 103; left: 448px; top: 480px" ToolTip="Home" /></td>
                            </tr>
                        </tbody>
                    </table>
                    <table>
                        <tbody>
                            <tr>
                                <td>
                                    <div style="left: 8px; overflow: auto; width: 776px; top: 56px; height: 384px">
                                        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#000099" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                                            Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                                            Font-Underline="False" ForeColor="Black" PageSize="1" Style="table-layout: auto;
                                            z-index: 101; left: 0px; clip: rect(auto auto auto auto); direction: ltr;
                                            top: 8px; border-collapse: separate" Width="95%" Height="1px">
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                    <HeaderStyle Width="0%" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="40%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="COMPETENZA" HeaderText="COMPETENZA">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="60%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                                            Text="Annulla"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                                            Text="Modifica">Seleziona</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                ForeColor="#0000C0" Wrap="False" />
                                        </asp:DataGrid></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSelezione" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
                                        Style="left: 16px; top: 392px" Width="768px">Nessuna Selezione</asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    
    <div id="InsVariazione" style="display: block; z-index: 1; left: 0px; width: 800px;
        position: absolute; top: 0px; height: 573px; background-color: #ffffff; text-align: left">
        <span style="font-family: Arial"></span>
        <br />
        <br />
        <table border="0" cellpadding="1" cellspacing="1" style="border-right: blue 2px solid;
            border-top: blue 2px solid; left: 101px; border-left: blue 2px solid; width: 76%;
            border-bottom: blue 2px solid; position: absolute; top: 109px; height: 343px;
            background-color: gainsboro">
            <tr>
                <td style="width: 80px; height: 20px; text-align: left">
                    <strong><span style="font-family: Arial"></span></strong></td>
                <td style="width: 363px; height: 20px; text-align: left">
                    <strong><span style="font-family: Arial">Tribunali Competenti per Comune</span></strong></td>
            </tr>
            <tr>
                <td style="width: 80px; height: 15px; text-align: left">
                </td>
                <td style="width: 363px; height: 15px; text-align: left">
                </td>
            </tr>
            <tr>
                <td style="width: 80px; height: 20px; text-align: left">
                    <span style="font-size: 10pt; font-family: Arial">Comune*</span></td>
                <td style="font-size: 12pt; width: 363px; font-family: Times New Roman; height: 20px;
                    text-align: left">
                    <asp:DropDownList ID="cmbComune" runat="server" BackColor="White" Font-Names="arial"
                        Font-Size="10pt" Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                        z-index: 111; left: 128px; border-left: black 1px solid; border-bottom: black 1px solid;
                        top: 64px" TabIndex="2" Width="500px">
                    </asp:DropDownList></td>
            </tr>
            <tr style="font-size: 10pt; color: #000000; font-family: Times New Roman">
                <td style="width: 80px; height: 20px; text-align: left">
                    <span style="font-size: 12pt">Competenza*</span></td>
                <td style="font-size: 12pt; width: 363px; height: 20px; text-align: left">
                    <asp:TextBox ID="txtCompetenza" runat="server" Font-Names="Arial" Font-Size="9pt"
                        MaxLength="50" TabIndex="3" ToolTip="Competenza" Width="500px"></asp:TextBox><span
                            style="font-size: 10pt; font-family: Arial"> </span>
                </td>
            </tr>
            <tr style="font-size: 12pt; color: #000000; font-family: Times New Roman">
                <td style="width: 80px; height: 20px; text-align: left">
                </td>
                <td style="width: 363px; height: 20px; text-align: left"><asp:HiddenField ID="txtComuneODL" runat="server" />
                    <asp:HiddenField ID="txtCompetenzaODL" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 80px; height: 19px">
                    &nbsp;</td>
                <td style="width: 363px; height: 19px">
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;
                    <asp:ImageButton ID="btn_Inserisci" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                        TabIndex="4" ToolTip="Salva i dati " />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:ImageButton ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                        OnClientClick="document.getElementById('TextBox1').value='0';document.getElementById('InsVariazione').style.visibility='hidden';"
                        TabIndex="5" ToolTip="Annulla operazione" /></td>
            </tr>
        </table>
    </div>

                                    <asp:HiddenField ID="TextBox1" runat="server" />
                                    <asp:HiddenField ID="txtannullo" runat="server" />
                                    <asp:HiddenField ID="txtID" runat="server" />

    <script type="text/javascript">

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


    function controlla_div() {
    if (document.getElementById('txtID').value != "-1") {
        document.getElementById('TextBox1').value = '1';
        document.getElementById('InsVariazione').style.visibility = 'visible';
    }
    else {
        alert('Nessuna riga selezionata!')
    }
    }
        
    function ConfermaAnnullo() {
    var sicuro = confirm('Sei sicuro di voler cancellare questo tribunale?');
    if (sicuro == true) {
    document.getElementById('txtannullo').value='1';
    }
    else
    {
    document.getElementById('txtannullo').value='0'; 
    }
    }            

        
    if (document.getElementById('TextBox1').value!='1') {
    document.getElementById('InsVariazione').style.visibility='hidden';
    }
    </script>

    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
      
    </form>

</body>

</html>
