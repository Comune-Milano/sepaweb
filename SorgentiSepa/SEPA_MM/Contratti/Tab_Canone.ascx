<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Canone.ascx.vb" Inherits="Contratti_Canone" %>
<style type="text/css">
    .style5
    {
        width: 174px;
    }
    .style6
    {
        height: 22px;
        width: 174px;
    }
    .style15
    {
        width: 128px;
    }
    #DettagliCanone
    {
        top: 2px;
        left: 267px;
    }
    .style32
    {
        width: 163px;
    }
    .style33
    {
        height: 22px;
        width: 163px;
    }
    .style34
    {
        width: 162px;
    }
    .style35
    {
        height: 22px;
        width: 162px;
    }
    .style36
    {
        width: 138px;
    }
    .style37
    {
        height: 22px;
        width: 138px;
    }
    .style38
    {
        width: 169px;
    }
    .style39
    {
        height: 22px;
        width: 169px;
    }
    .style40
    {
        width: 136px;
    }
    .style41
    {
        height: 22px;
        width: 136px;
    }
</style>
<div style="left: 8px; width: 1130px; position: absolute; top: 168px; height: 520px">
    <table width="100%" style="border-top-width: 3px; border-left-width: 3px; border-left-color: lightgrey;
        border-bottom-width: 3px; border-bottom-color: lightgrey; border-top-color: lightgrey;
        border-right-width: 3px; border-right-color: lightgrey;">
        <tr>
            <td class="style5">
                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Canone Corrente"></asp:Label>
            </td>
            <td class="style32">
                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Canone Iniziale" Visible="False"></asp:Label>
            </td>
            <td class="style40">
                <asp:Label ID="lblProvvisiorio" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" Text="Canone Provvisiorio*" Visible="False"></asp:Label>
            </td>
            <td class="style38">
                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Importo Cauzione"></asp:Label>
                <%--                <img id="INFO" alt="La cauzione è pari a 3 mensilità di affitto per questo tipo di contratto."
                    src="Immagini/Img_Help.png" width="20px" style="cursor: pointer" onclick="document.getElementById('USCITA').value='1';ApriDepositoCauz();" />
                --%>
                <img id="INFO" alt="La cauzione è pari a 3 mensilità di affitto per questo tipo di contratto."
                    src="../NuoveImm/INFO.png" />
            </td>
            <td class="style34">
                <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Data Rimborso Cauzione"></asp:Label>
            </td>
            <td class="style36">
                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Libretto deposito"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Mesi Affitto Anticip."></asp:Label>
                <img id="Img1" alt="Importo da anticipare al momento della firma del contratto. Viene restituito con la prima bolletta utile."
                    src="../NuoveImm/INFO.png" />
            </td>
        </tr>
        <tr>
            <td class="style5" style="vertical-align: top">
                <asp:TextBox ID="txtCanoneCorrente" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="85px" TabIndex="37" BorderColor="#CC0000" BorderStyle="Inset" BorderWidth="3px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtCanoneCorrente"
                    ErrorMessage="Errato" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" ValidationExpression="\b\d*,\d{2}\b"
                    ToolTip="Valore numerico con 2 cifre decimali separati dalla virgola!" ValidationGroup="a"></asp:RegularExpressionValidator>
            </td>
            <td class="style32" style="vertical-align: top">
                <asp:TextBox ID="txtCanoneIniziale" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="100px" TabIndex="36" Visible="False"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtCanoneIniziale"
                    ErrorMessage="Errato" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" ValidationExpression="\b\d*,\d{2}\b"
                    ToolTip="Valore numerico con 2 cifre decimali separati dalla virgola!" 
                    ValidationGroup="a" Visible="False"></asp:RegularExpressionValidator>
            </td>
            <td class="style40" style="vertical-align: top">
                <asp:Label ID="lblCanoneProvvisiorio" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="9pt" Text="0,00" Visible="False" Width="89px" Style="text-align: right"
                    BackColor="#FFFFCC" BorderColor="Black" BorderWidth="1px" Height="18px"></asp:Label>
            </td>
            <td class="style38" style="vertical-align: top">
                <asp:TextBox ID="txtImportoCauzione" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="85px" TabIndex="38"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtImportoCauzione"
                    ErrorMessage="Errato" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" ValidationExpression="\b\d*,\d{2}\b"
                    ToolTip="Valore numerico con 2 cifre decimali separati dalla virgola!" ValidationGroup="a"></asp:RegularExpressionValidator>
            </td>
            <td class="style34" style="vertical-align: top">
                <asp:TextBox ID="txtRimborsoDC" runat="server" BorderStyle="Solid" BorderWidth="1px"
                    MaxLength="10" TabIndex="41" ToolTip="gg/mm/aaaa" Width="68px" Style="z-index: 500;"></asp:TextBox><asp:RegularExpressionValidator
                        ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtRimborsoDC"
                    Display="Dynamic" ErrorMessage="!!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">
                </asp:RegularExpressionValidator>
            </td>
            <td class="style36" style="vertical-align: top">
                <asp:TextBox ID="txtLibrettoDeposito" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="90px" TabIndex="39"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtImportoAnticipo" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="55px" TabIndex="40"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtImportoAnticipo"
                    ErrorMessage="Errato" Font-Bold="False" Font-Names="ARIAL" Font-Size="8pt" ValidationExpression="\b\d*,\d{2}\b"
                    ToolTip="Valore numerico con 2 cifre decimali separati dalla virgola!" ValidationGroup="a"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Frequenza Canone"></asp:Label>
            </td>
            <td class="style33">
                &nbsp;
            </td>
            <td class="style41">
                <asp:Label ID="lblProvvisiorio0" runat="server" Font-Bold="False" Font-Names="Arial"
                    Font-Size="8pt" Text="* Domanda Rid. Canone" Visible="False" Width="119px"></asp:Label>
            </td>
            <td class="style39">
                &nbsp;
            </td>
            <td class="style35">
                &nbsp;
            </td>
            <td class="style37">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:DropDownList ID="cmbFreqCanone" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="170px" TabIndex="41">
                    <asp:ListItem Value="12" Selected="True">Mensile</asp:ListItem>
                    <asp:ListItem Value="4">Trimestrale</asp:ListItem>
                    <asp:ListItem Value="2">Semestrale</asp:ListItem>
                    <asp:ListItem Value="1">Annuale</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style32">
                &nbsp;
            </td>
            <td class="style40">
                &nbsp;
            </td>
            <td class="style38">
                &nbsp;
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Destinazione rate"></asp:Label>
            </td>
            <td class="style32">
            </td>
            <td class="style40">
                &nbsp;
            </td>
            <td class="style38">
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:DropDownList ID="cmbDestRate" runat="server" Font-Names="Arial" Font-Size="9pt"
                    Width="170px" TabIndex="42">
                    <asp:ListItem Selected="True" Value="1">Inquilino</asp:ListItem>
                    <asp:ListItem Value="2">Studio Amministratore</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style32">
            </td>
            <td class="style40">
                &nbsp;
            </td>
            <td class="style38">
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;
            </td>
            <td class="style32">
                &nbsp;
            </td>
            <td class="style40">
                &nbsp;
            </td>
            <td class="style38">
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial; font-size: 9pt; color: Blue; font-weight: bold; width: 200px;">
                >>
                <asp:HyperLink ID="HLink_SchedaDep" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" Font-Underline="True" ForeColor="Blue" Style="cursor: pointer"
                    ToolTip="Visualizza le informazioni relative al deposito cauzionale">Visual. Scheda Depos. Cauz.</asp:HyperLink>
                <<
            </td>
            <td class="style32">
                &nbsp;
            </td>
            <td class="style40">
                &nbsp;
            </td>
            <td class="style38">
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style5">
                &nbsp;
            </td>
            <td class="style32">
                &nbsp;
            </td>
            <td class="style40">
                &nbsp;
            </td>
            <td class="style38">
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:CheckBox ID="checkInteressiCauzione" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Text="Interessi su cauzione" ToolTip="Interessi su Cauzione" Checked="True" TabIndex="43"
                    Enabled="False" />
            </td>
            <td class="style15" colspan="3">
                
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:CheckBox ID="checkConguaglioBoll" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Text="Conguaglio bollette" Checked="True" ToolTip="Indica se partecipa o meno al conguaglio"
                    TabIndex="44" />
            </td>
            <td class="style32">
            </td>
            <td class="style40">
                &nbsp;
            </td>
            <td class="style38">
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="style6">
                <asp:CheckBox ID="checkInvioBoll" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Text="Invio bollette" Checked="True" ToolTip="Indica se devono essere create/spedite o meno le bollette per questo contratto"
                    TabIndex="45" />
            </td>
            <td class="style33">
            </td>
            <td class="style41">
                &nbsp;
            </td>
            <td class="style39">
            </td>
            <td class="style35">
                &nbsp;
            </td>
            <td class="style37">
            </td>
            <td style="height: 22px">
            </td>
        </tr>
        <tr>
            <td class="style5">
                <asp:CheckBox ID="chRitardoPagamenti" runat="server" Font-Names="Arial" Font-Size="8pt"
                    Text="Interessi su Ritardo Pagamenti" ToolTip="Interessi su Cauzione" Checked="True"
                    TabIndex="46" />
            </td>
              <td class="style32">
            </td>
            <td class="style40">
                &nbsp;
            </td>
            <td class="style38">
            </td>
            <td class="style34">
                &nbsp;
            </td>
            <td class="style36">
            </td>
            <td>
            </td>
        </tr>
    </table>
    <table width="100%">
        <tr>
            <td>
                <asp:Label ID="LBLISTAT" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="AGGIORNAMENTI ISTAT" Visible="False" Width="194px"></asp:Label><br />
                <br />
                <asp:Label ID="lblistat1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="Frequenza" Visible="False"></asp:Label>&nbsp;<asp:DropDownList ID="lblistat2"
                        runat="server" Font-Names="Arial" Font-Size="9pt" Width="137px" Visible="False"
                        TabIndex="47">
                        <asp:ListItem Value="A">Annuale</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;
                <asp:Label ID="lblistat3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                    Text="% Var." Visible="False"></asp:Label>&nbsp;<asp:DropDownList ID="lblistat4"
                        runat="server" Font-Names="Arial" Font-Size="9pt" Width="63px" Visible="False"
                        TabIndex="48">
                        <asp:ListItem>0</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem>15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>31</asp:ListItem>
                        <asp:ListItem>32</asp:ListItem>
                        <asp:ListItem>33</asp:ListItem>
                        <asp:ListItem>34</asp:ListItem>
                        <asp:ListItem>35</asp:ListItem>
                        <asp:ListItem>36</asp:ListItem>
                        <asp:ListItem>37</asp:ListItem>
                        <asp:ListItem>38</asp:ListItem>
                        <asp:ListItem>39</asp:ListItem>
                        <asp:ListItem>40</asp:ListItem>
                        <asp:ListItem>41</asp:ListItem>
                        <asp:ListItem>42</asp:ListItem>
                        <asp:ListItem>43</asp:ListItem>
                        <asp:ListItem>44</asp:ListItem>
                        <asp:ListItem>45</asp:ListItem>
                        <asp:ListItem>46</asp:ListItem>
                        <asp:ListItem>47</asp:ListItem>
                        <asp:ListItem>48</asp:ListItem>
                        <asp:ListItem>49</asp:ListItem>
                        <asp:ListItem>50</asp:ListItem>
                        <asp:ListItem>51</asp:ListItem>
                        <asp:ListItem>52</asp:ListItem>
                        <asp:ListItem>53</asp:ListItem>
                        <asp:ListItem>54</asp:ListItem>
                        <asp:ListItem>55</asp:ListItem>
                        <asp:ListItem>56</asp:ListItem>
                        <asp:ListItem>57</asp:ListItem>
                        <asp:ListItem>58</asp:ListItem>
                        <asp:ListItem>59</asp:ListItem>
                        <asp:ListItem>60</asp:ListItem>
                        <asp:ListItem>61</asp:ListItem>
                        <asp:ListItem>62</asp:ListItem>
                        <asp:ListItem>63</asp:ListItem>
                        <asp:ListItem>64</asp:ListItem>
                        <asp:ListItem>65</asp:ListItem>
                        <asp:ListItem>66</asp:ListItem>
                        <asp:ListItem>67</asp:ListItem>
                        <asp:ListItem>68</asp:ListItem>
                        <asp:ListItem>69</asp:ListItem>
                        <asp:ListItem>70</asp:ListItem>
                        <asp:ListItem>71</asp:ListItem>
                        <asp:ListItem>72</asp:ListItem>
                        <asp:ListItem>73</asp:ListItem>
                        <asp:ListItem>74</asp:ListItem>
                        <asp:ListItem Selected="True">75</asp:ListItem>
                        <asp:ListItem>76</asp:ListItem>
                        <asp:ListItem>77</asp:ListItem>
                        <asp:ListItem>78</asp:ListItem>
                        <asp:ListItem>79</asp:ListItem>
                        <asp:ListItem>80</asp:ListItem>
                        <asp:ListItem>81</asp:ListItem>
                        <asp:ListItem>82</asp:ListItem>
                        <asp:ListItem>83</asp:ListItem>
                        <asp:ListItem>84</asp:ListItem>
                        <asp:ListItem>85</asp:ListItem>
                        <asp:ListItem>86</asp:ListItem>
                        <asp:ListItem>87</asp:ListItem>
                        <asp:ListItem>88</asp:ListItem>
                        <asp:ListItem>89</asp:ListItem>
                        <asp:ListItem>90</asp:ListItem>
                        <asp:ListItem>91</asp:ListItem>
                        <asp:ListItem>92</asp:ListItem>
                        <asp:ListItem>93</asp:ListItem>
                        <asp:ListItem>94</asp:ListItem>
                        <asp:ListItem>95</asp:ListItem>
                        <asp:ListItem>96</asp:ListItem>
                        <asp:ListItem>97</asp:ListItem>
                        <asp:ListItem>98</asp:ListItem>
                        <asp:ListItem>99</asp:ListItem>
                        <asp:ListItem>100</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;
                <asp:Image ID="ImgIstat" runat="server" ImageUrl="~/NuoveImm/Img_VisIstat.png" Style="cursor: pointer"
                    ToolTip="Visualizza tutti gli aggiornamenti ISTAT di questo contratto" Visible="False"
                    TabIndex="49" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="#8080FF" Text="DETTAGLIO CANONI CALCOLATI" Width="583px" Height="16px"></asp:Label><br />
                <div id="contenitorecalcoli" style="overflow: auto; height: 110px; position: relative;
                    width: 950px; left: 0px; top: 0px;">
                    <asp:Label ID="lblDettaglioCanoni" runat="server" Font-Names="arial" Font-Size="8pt"
                        Width="100%" TabIndex="50"></asp:Label></div>
            </td>
            <td>
                <asp:ImageButton ID="ImgRicalcolaC" runat="server" ImageUrl="~/NuoveImm/Img_RicalcolaCanone.png"
                    ToolTip="Calcola nuovamente il canone L27/36 se il contratto è in bozza" Visible="False"
                    OnClientClick="ConfermaRicalcolo();" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HH1" runat="server" Value="0" />
    <div id="DettagliCanone" style="background-position: 0px top; position: absolute;
        left:50px; width: 326px; height: 226px; visibility: hidden; background-image: url('../NuoveImm/DettagliCanone.png');
        background-repeat: no-repeat;">
        <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Text="Dettagli" Style="position: absolute; top: 37px; left: 80px; height: 130px;
            width: 186px;"></asp:Label>
    </div>
</div>
