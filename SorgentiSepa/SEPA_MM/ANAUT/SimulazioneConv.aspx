<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SimulazioneConv.aspx.vb" Inherits="ANAUT_SimulazioneConv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
    var Selezionato;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
    <title>Gestione Motivi Esclusione</title>
    <style type="text/css">
        .style1
        {
            width: 145px;
        }
        .style2
        {
            width: 132px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
<br/>
        <table style="left: 0px; BACKGROUND-IMAGE: url('../NuoveImm/SfondoMaschere.jpg'); WIDTH: 674px;
            position: absolute; top: 0px; height: 596px;">
            <tr>
                <td style="width: 674px">
                   <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    <br />
&nbsp;Simulazione Convocazioni </strong>
                    <asp:Label ID="Label1" runat="server" style="font-weight: 700" Text="Label"></asp:Label>
                    <br />
                    </span><br />
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="9pt" 
                                    Text="Clicca qui per visualizzare maggiori informazioni su questa AU" 
                                    Font-Underline="True" style="cursor:pointer"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width:80%;">
                                    <tr>
                                        <td class="style1">
                                            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" 
                                                Text="Data Prima Convocazione"></asp:Label>
                                        </td>
                                        <td>
                                    <asp:TextBox ID="txtDal" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            TabIndex="1" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                                        </td>
                                        <td class="style2">
                                            <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" 
                                                Text="Data Ultima Convocazione"></asp:Label>
                                        </td>
                                        <td>
                                    <asp:TextBox ID="txtAl" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            TabIndex="2" ToolTip="gg/mm/aaaa" Width="68px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width:100%;">
                                    <tr style="border: 2px solid #000080">
                            <td valign="top" class="style1" bgcolor="#EBEBEB">
                    <asp:Label ID="Label11" runat="server" Text="Liste Convocazione" Font-Names="arial" Font-Size="8pt" 
                                   ></asp:Label>
                            </td>
                            <td valign="top" style="border: 1px solid #000080" bgcolor="#EBEBEB">
                                <asp:CheckBoxList ID="chListListe" runat="server" AutoPostBack="True" 
                                    CausesValidation="True" Font-Names="arial" Font-Size="8pt" 
                                    RepeatColumns="3">
                                </asp:CheckBoxList>
                            </td>
                        </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
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
                    <asp:ImageButton ID="imgSalva" runat="server" 
                        style="position:absolute; top: 514px; left: 482px;" 
                        ImageUrl="~/NuoveImm/Img_Procedi.png" TabIndex="3" />
                    <br />
                    
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 513px; left: 586px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="PaginaHome();"/>
                    <br />
                    <br />
                    <br />
                    
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 499px; left: 15px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />

   

                </td>
            </tr>

             <asp:HiddenField ID="confermato" runat="server" />
        </table>
        
           

        <script type="text/javascript">


            

            function PaginaHome() {
                document.location.href = 'pagina_home.aspx';
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
    
           
    
           
           <p>
                                        &nbsp;</p>
                                   <p>
                                        &nbsp;</p>

    </form>
    

    
    

        
        </body>
</html>