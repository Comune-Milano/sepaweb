<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DecidiProposte.aspx.vb" Inherits="ANAUT_DecidiProposte" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Decisione Proposte Decadenza</title>
    <style type="text/css">
        #elenco
        {
            height: 382px;
            width: 644px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                    &nbsp;Decisione Proposte di Decadenza</strong></span><br />
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
                    <asp:HiddenField ID="procedi" runat="server" Value="0" />
                    <asp:HiddenField ID="id" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        <p>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 556px; position: absolute; top: 471px" TabIndex="8" 
                ToolTip="Home" />
        </p>
                            <div id="elenco" 
        style="overflow: scroll; position:absolute; top: 50px;">
                    <asp:Label ID="lblElenco" runat="server" Font-Names="arial" Font-Size="10pt"></asp:Label>
                    </div>
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Names="arial" Font-Size="8pt" 
                        ForeColor="Red"></asp:Label>
        <script type ="text/javascript">
            function Apri(id) 
    {
    window.open('max.aspx?CHIUDI=1&ID='+id+'&LE=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');
}

function Decidi(id,id1) {
    document.getElementById('iddichiarazione').value = id;
    document.getElementById('id').value = id1;
    document.getElementById('Decidi').style.visibility = 'visible';
}
    </script>
    <asp:HiddenField ID="iddichiarazione" runat="server" />
        <div id="Decidi" 
        
        style="border: 1px solid #0000FF; width: 602px; position:absolute; background-color: #C0C0C0; top: 74px; left: 23px; height: 321px; text-align: center;">
        &nbsp;<br />
            <table style="width:90%;">
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                            Font-Size="12pt" Text="La proposta di decadenza per questa dichiarazione è:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:RadioButton ID="RadioButton1" runat="server" Font-Bold="True" 
                            Font-Names="ARIAL" Font-Size="12pt" Text="CONFERMATA" />
&nbsp;
                        <asp:RadioButton ID="RadioButton2" runat="server" Font-Bold="True" 
                            Font-Names="ARIAL" Font-Size="12pt" Text="RESPINTA" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        &nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:ImageButton ID="ImgSalva" runat="server" 
                            ImageUrl="~/NuoveImm/Img_SalvaVal.png" style="cursor:pointer;" 
                            onclientclick="Conferma();" />
&nbsp;&nbsp;&nbsp;
        <img id="IMG3" src="../NuoveImm/Img_AnnullaVal.png" onclick="document.getElementById('Decidi').style.visibility='hidden';" 
                style=" z-index: 200;cursor:pointer;" /></td>
                </tr>
            </table>
            <br />
    </div>
    </form>
    <script type ="text/javascript">
        document.getElementById('Decidi').style.visibility = 'hidden';

        function Conferma() {

            //if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Confermi la decisione presa?");
                if (chiediConferma == true) {
                    document.getElementById('procedi').value = '1';

                }
                else {
                    document.getElementById('procedi').value = '0';
                }
            //}
        } 

    </script>
</body>
</html>
