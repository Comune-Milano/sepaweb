Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_RisultatoRicercaD
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreCF As String
    Dim sValorePG As String
    Dim sValoreCO As String
    Dim sValoreST As String
    Dim sValoreUN As String
    Dim sValoreENTE As String
    Dim sValorebando As String
    Dim sValoreTipo As String
    Dim sValore45 As String
    Dim sValoreAU As String
    Dim sValoreSDAL As String
    Dim sValoreSAL As String
    Dim sValoreART15 As String
    Dim sValoreINVAL As String
    Dim sStringaSql As String
    Dim scriptblock As String
    Dim sValoreOP As String
    Dim dt As New System.Data.DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='../NuoveImm/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            sValoreCG = Request.QueryString("CG")
            sValoreNM = Request.QueryString("NM")
            sValoreCF = Request.QueryString("CF")
            sValorePG = Request.QueryString("PG")
            sValoreST = Request.QueryString("ST")
            sValoreCO = Request.QueryString("CO")
            sValoreUN = Request.QueryString("UN")
            sValoreENTE = Request.QueryString("ENTE")
            sValorebando = Request.QueryString("BD")
            sValoreTipo = Request.QueryString("TI")
            sValore45 = Request.QueryString("S45")
            sValoreAU = Request.QueryString("GA")
            XX.Value = Request.QueryString("XX")
            sValoreSDAL = Request.QueryString("SDAL")
            sValoreSAL = Request.QueryString("SAL")
            sValoreART15 = Request.QueryString("ART15")
            sValoreINVAL = Request.QueryString("INVAL")
            sValoreOP = par.DeCripta(Request.QueryString("OP"))

            If sValoreST = -1 Then sValoreST = ""
            If sValorebando = -1 Then sValorebando = ""

            btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Value = "-1"
            Cerca()
            If XX.Value = "1" Then btnRicerca.Visible = False
        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key", "<script>MakeStaticHeader('" + DataGrid1.ClientID + "', 350, 635 , 25 ,true); </script>", False)
    End Sub

    Private Sub BindGrid()

        par.OracleConn.Open()

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

        Dim ds As New Data.DataSet()

        da.Fill(ds, "UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO")
        da.Fill(dt)
        DataGrid1.DataSource = ds
        DataGrid1.DataBind()
        HttpContext.Current.Session.Add("ELENCOAU", dt)
        Label9.Text = "  - Totale:" & ds.Tables(0).Rows.Count
        par.OracleConn.Close()
        Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
    End Sub

    Private Function Cerca()
        Dim bTrovato As Boolean
        Dim sValore As String
        Dim sCompara As String


        bTrovato = False
        sStringaSql = ""

        If sValoreCG <> "" Then
            sValore = sValoreCG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_COMP_NUCLEO.COGNOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreNM <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreNM
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_COMP_NUCLEO.NOME " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreCF <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreCF
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_COMP_NUCLEO.COD_FISCALE " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValorePG <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorePG
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.PG" & sCompara & "'" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreST <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreST
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.ID_STATO " & sCompara & " " & par.PulisciStrSql(sValore) & " "
        End If

        If sValorebando <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValorebando
            sCompara = " = "

            bTrovato = True

            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.ID_BANDO " & sCompara & " " & par.PulisciStrSql(sValore) & " "
        End If

        If sValoreTipo = "1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreTipo
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.FL_DA_VERIFICARE='1' "
        End If

        If sValoreTipo = "2" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreTipo
            sCompara = " = "

            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.FL_SOSPENSIONE='1' "
        End If

        If sValoreCO <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreCO
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.rapporto " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreUN <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreUN
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.posizione " & sCompara & " '" & par.PulisciStrSql(sValore) & "' "
        End If


        If sValoreSDAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSDAL
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.DATA>='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValoreSAL <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "

            sValore = sValoreSAL
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.DATA<='" & par.PulisciStrSql(sValore) & "' "
        End If

        If sValore45 = "1" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            bTrovato = True
            sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.FL_4_5_LOTTO =1 "
        End If

        If sValoreAU = "1" Then
            'If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            'bTrovato = True
            'sStringaSql = sStringaSql & " (fl_generaz_auto='1') "
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            bTrovato = True
            sStringaSql = sStringaSql & " (fl_generaz_auto='0' or fl_generaz_auto is null) "
        Else
            'If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            'bTrovato = True
            'sStringaSql = sStringaSql & " (fl_generaz_auto='0' or fl_generaz_auto is null) "
        End If

        If sValoreART15 <> "" Then
            If sValoreART15 = "1" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.ART_15 =1 "
            Else
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " UTENZA_DICHIARAZIONI.ART_15 =0"
            End If
        End If


        If (sValoreINVAL <> "" And sValoreINVAL <> "0") Then
            If sValoreINVAL = "SI" Then
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " id_dichiarazione IN ( SELECT id_dichiarazione FROM UTENZA_COMP_NUCLEO, UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID = UTENZA_COMP_NUCLEO.id_dichiarazione AND natura_inval = 'Motoria con carrozzella') "
            Else
                If bTrovato = True Then sStringaSql = sStringaSql & " AND "
                bTrovato = True
                sStringaSql = sStringaSql & " id_dichiarazione NOT IN ( SELECT id_dichiarazione FROM UTENZA_COMP_NUCLEO, UTENZA_DICHIARAZIONI WHERE UTENZA_DICHIARAZIONI.ID = UTENZA_COMP_NUCLEO.id_dichiarazione AND natura_inval = 'Motoria con carrozzella') "
            End If
        End If

        If sValoreOP <> "" Then
            If bTrovato = True Then sStringaSql = sStringaSql & " AND "
            sValore = sValoreOP
            If InStr(sValore, "*") Then
                sCompara = " LIKE "
                Call par.ConvertiJolly(sValore)
            Else
                sCompara = " = "
            End If
            bTrovato = True
            sStringaSql = sStringaSql & " upper(GetOperatoreAU(UTENZA_DICHIARAZIONI.ID)) " & sCompara & " '" & par.PulisciStrSql(UCase(sValore)) & "' "
        End If

        If Session.Item("ANAGRAFE_CONSULTAZIONE") = "1" Then
            SoloLettura = "1"
        End If


        If Request.QueryString("F") <> "1" Then
            sStringaSQL1 = "SELECT utenza_bandi.descrizione||(CASE WHEN UTENZA_DICHIARAZIONI.ART_15=1 THEN '-ART.15' WHEN UTENZA_DICHIARAZIONI.ART_15=0 THEN '' END) as bando,UTENZA_DICHIARAZIONI.ID,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME," _
                                           & "UTENZA_COMP_NUCLEO.COD_FISCALE ," _
                                            & "UTENZA_DICHIARAZIONI.PG ," _
                                            & "TO_CHAR(TO_DATE(UTENZA_DICHIARAZIONI.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY')" _
                                            & " AS  ""DATA_PG""," _
                                           & "T_STATI_DICHIARAZIONE.DESCRIZIONE AS ""STATO"",UTENZA_DICHIARAZIONI.RAPPORTO,UTENZA_DICHIARAZIONI.ISEE,(case when NVL(FL_VERIFICA_REDDITO,'0')<>'0' THEN 'REDDITI/' ELSE '' END)||(case when NVL(FL_VERIFICA_NUCLEO,'0')<>'0' THEN 'NUCLEO/' ELSE '' END)||(case when NVL(FL_VERIFICA_PATRIMONIO,'0')<>'0' THEN 'PATRIMONIO/' ELSE '' END) AS VERIFICA,GetOperatoreAU(UTENZA_DICHIARAZIONI.ID) AS OPERATORE,'' as CAMBIO,ISE_ERP,VSE " _
                                           & " FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO,T_STATI_DICHIARAZIONE,utenza_bandi WHERE utenza_bandi.id=utenza_dichiarazioni.id_bando and (NVL(fl_generaz_auto,'0')='0' AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE')) and " _
                                           & "  UTENZA_DICHIARAZIONI.PG IS NOT NULL AND UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE (+) AND UTENZA_COMP_NUCLEO.PROGR=0" _
                                           & " AND UTENZA_DICHIARAZIONI.ID_STATO=T_STATI_DICHIARAZIONE.COD (+) "

        Else
            sStringaSQL1 = "SELECT utenza_bandi.descrizione||(CASE WHEN UTENZA_DICHIARAZIONI.ART_15=1 THEN '-ART.15' WHEN UTENZA_DICHIARAZIONI.ART_15=0 THEN '' END) as bando,UTENZA_DICHIARAZIONI.ID,UTENZA_COMP_NUCLEO.COGNOME,UTENZA_COMP_NUCLEO.NOME," _
                                           & "UTENZA_COMP_NUCLEO.COD_FISCALE ," _
                                            & "UTENZA_DICHIARAZIONI.PG ," _
                                            & "TO_CHAR(TO_DATE(UTENZA_DICHIARAZIONI.DATA_PG,'YYYYmmdd'),'DD/MM/YYYY')" _
                                            & " AS  ""DATA_PG""," _
                                           & "T_STATI_DICHIARAZIONE.DESCRIZIONE AS ""STATO"",UTENZA_DICHIARAZIONI.RAPPORTO,UTENZA_DICHIARAZIONI.ISEE,(case when NVL(FL_VERIFICA_REDDITO,'0')<>'0' THEN 'REDDITI/' ELSE '' END)||(case when NVL(FL_VERIFICA_NUCLEO,'0')<>'0' THEN 'NUCLEO/' ELSE '' END)||(case when NVL(FL_VERIFICA_PATRIMONIO,'0')<>'0' THEN 'PATRIMONIO/' ELSE '' END) AS VERIFICA,GetOperatoreAU(UTENZA_DICHIARAZIONI.ID) AS OPERATORE,(case when utenza_bandi.stato=1 then replace(replace('<img alt=£Cambio£ title=£Cambia intestatario Scheda AU£ src=£../NuoveImm/Img_CambioDich.png£ onclick=£window.open(''CambioIntestazioneAU.aspx?ID='||utenza_dichiarazioni.ID||'$PG='||utenza_dichiarazioni.PG||''',''CambioDich'',''height=550,top=200,left=350,width=670'');£ style=£cursor: pointer£ />','$','&'),'£','" & Chr(34) & "') else '' end) as CAMBIO,ISE_ERP,VSE " _
                                           & " FROM UTENZA_DICHIARAZIONI,UTENZA_COMP_NUCLEO,T_STATI_DICHIARAZIONE,utenza_bandi WHERE utenza_bandi.id=utenza_dichiarazioni.id_bando and (NVL(fl_generaz_auto,'0')='0' AND (NOTE_WEB IS NULL OR NOTE_WEB<>'GENERATA_AUTOMATICAMENTE')) and " _
                                           & "  UTENZA_DICHIARAZIONI.PG IS NOT NULL AND UTENZA_DICHIARAZIONI.ID=UTENZA_COMP_NUCLEO.ID_DICHIARAZIONE (+) AND UTENZA_COMP_NUCLEO.PROGR=0" _
                                           & " AND UTENZA_DICHIARAZIONI.ID_STATO=T_STATI_DICHIARAZIONE.COD (+) "
        End If

        If sStringaSql <> "" Then
            sStringaSQL1 = sStringaSQL1 & " AND " & sStringaSql
        End If
        sStringaSQL1 = sStringaSQL1 & " ORDER BY to_number(pg) desc,COGNOME ASC,NOME ASC"

        BindGrid()
    End Function


    Public Property SoloLettura() As String
        Get
            If Not (ViewState("par_SoloLettura") Is Nothing) Then
                Return CStr(ViewState("par_SoloLettura"))
            Else
                Return "0"
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_SoloLettura") = value
        End Set

    End Property

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property



    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
    e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")

            e.Item.Attributes.Add("onclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato :PG " & e.Item.Cells(1).Text & "';document.getElementById('pgd').value='" & e.Item.Cells(1).Text & "';")
            e.Item.Attributes.Add("onDblclick", "this.style.backgroundColor='red';document.getElementById('LBLID').value='" & e.Item.Cells(0).Text & "';document.getElementById('TextBox7').value='Hai selezionato :PG " & e.Item.Cells(1).Text & "';document.getElementById('pgd').value='" & e.Item.Cells(1).Text & "';ApriDichiarazione();")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            'Label3.Text = "0"
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub

    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        If XX.Value = "1" Then
            Response.Write("<script>self.close();</script>")
        Else
            Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
        End If


    End Sub

    Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
        If LBLID.Value = "-1" Or LBLID.Value = "" Then
            Response.Write("<script>alert('Nessuna Dichiarazione selezionata!')</script>")
        Else

            If XX.Value = "1" Then
                If ControlloIDContrAbusivo() = True Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "window.open('DichAUnuova.aspx?TORNA=1&CHIUDI=1&CR=1&LE=" & SoloLettura & "&ID=" & LBLID.Value & "');" _
                            & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
                    End If
                Else
                    If ControlloAssTemp() = True Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "window.open('DichAUnuova.aspx?TORNA=1&CHIUDI=1&CR=1&ASST=1&LE=" & SoloLettura & "&ID=" & LBLID.Value & "');" _
                            & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
                        End If
                    Else
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "window.open('DichAUnuova.aspx?TORNA=1&CHIUDI=1&LE=" & SoloLettura & "&ID=" & LBLID.Value & "');" _
                                & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
                        End If
                    End If
                End If
            Else
                If ControlloIDContrAbusivo() = True Then
                    scriptblock = "<script language='javascript' type='text/javascript'>" _
                            & "window.open('DichAUnuova.aspx?CR=1&LE=" & SoloLettura & "&ID=" & LBLID.Value & "');" _
                            & "</script>"
                    If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
                    End If
                Else
                    If ControlloAssTemp() = True Then
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "window.open('DichAUnuova.aspx?LE=" & SoloLettura & "&CR=1&ASST=1&ID=" & LBLID.Value & "');" _
                                & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
                        End If
                    Else
                        scriptblock = "<script language='javascript' type='text/javascript'>" _
                                & "window.open('DichAUnuova.aspx?LE=" & SoloLettura & "&ID=" & LBLID.Value & "','" & pgd.Value & "','');" _
                                & "</script>"
                        If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript20")) Then
                            Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript20", scriptblock)
                        End If
                        LBLID.Value = "-1"

                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaDichiarazioni.aspx""</script>")
    End Sub

    Protected Sub DataGrid1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGrid1.SelectedIndexChanged

    End Sub

    Protected Sub TextBox7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged

    End Sub

    Private Function ControlloIDContrAbusivo() As Boolean
        Dim abusivo As Boolean = False
        Dim codcontratto As String = ""
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            codcontratto = Request.QueryString("CO")

            If codcontratto = "" Then
                par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID =" & LBLID.Value
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    codcontratto = par.IfNull(myReader0("RAPPORTO"), "")
                End If
                myReader0.Close()
            End If

            par.cmd.CommandText = "SELECT * from SISCOM_MI.RAPPORTI_UTENZA_AU_ABUSIVI WHERE ID_CONTRATTO IN (SELECT ID FROM SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & codcontratto & "')"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                abusivo = True
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return abusivo

    End Function

    Private Function ControlloAssTemp() As Boolean
        Dim assTemp As Boolean = False
        Dim codcontratto As String = ""
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            codcontratto = Request.QueryString("CO")

            If codcontratto = "" Then
                par.cmd.CommandText = "SELECT * FROM UTENZA_DICHIARAZIONI WHERE ID =" & LBLID.Value
                Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader0.Read Then
                    codcontratto = par.IfNull(myReader0("RAPPORTO"), "")
                End If
                myReader0.Close()
            End If

            par.cmd.CommandText = "SELECT * from SISCOM_MI.RAPPORTI_UTENZA WHERE COD_CONTRATTO='" & codcontratto & "' AND FL_ASSEGN_TEMP=1"
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                assTemp = True
            End If
            myReader.Close()

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try

        Return assTemp
    End Function

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        ExportXLS()
    End Sub

    Private Function ExportXLS()
        Dim myExcelFile As New CM.ExcelFile
        Dim i As Long
        Dim K As Long
        Dim sNomeFile As String = ""
        Dim row As System.Data.DataRow
        Dim par As New CM.Global

        Dim FileCSV As String = ""

        Try

            dt = CType(HttpContext.Current.Session.Item("ELENCOAU"), Data.DataTable)

            FileCSV = "Estrazione" & Format(Now, "yyyyMMddHHmmss")

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    i = 0
                    With myExcelFile

                        .CreateFile(Server.MapPath("..\FileTemp\" & FileCSV & ".xls"))
                        .PrintGridLines = False
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsTopMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsLeftMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsRightMargin, 1.5)
                        .SetMargin(CM.ExcelFile.MarginTypes.xlsBottomMargin, 1.5)
                        .SetDefaultRowHeight(14)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsNoFormat)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold)
                        .SetFont("Arial", 10, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsUnderline)
                        .SetFont("Courier", 16, CM.ExcelFile.FontFormatting.xlsBold + CM.ExcelFile.FontFormatting.xlsItalic)

                        'COD.CONTRATTO, COGNOME, NOME, CODICE FISCALE, A.U., NUMERO, DATA PR., STATO, ISE, VSE, ISEE, MOT. VERIFICA, OPERATORE
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 1, "COD_CONTRATTO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 2, "COGNOME")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 3, "NOME")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 4, "CODICE FISCALE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 5, "A.U.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 6, "NUMERO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 7, "DATA PR.")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 8, "STATO")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 9, "ISE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 10, "VSE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 11, "ISEE")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 12, "MOT. VERIFICA")
                        .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont1, CM.ExcelFile.CellAlignment.xlsCentreAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, 1, 13, "OPERATORE")
                        K = 2
                        For Each row In dt.Rows
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 1, par.IfNull(dt.Rows(i).Item("RAPPORTO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 2, par.IfNull(dt.Rows(i).Item("COGNOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 3, par.IfNull(dt.Rows(i).Item("NOME"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 4, par.IfNull(dt.Rows(i).Item("COD_FISCALE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 5, par.IfNull(dt.Rows(i).Item("BANDO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 6, par.IfNull(dt.Rows(i).Item("PG"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 7, par.IfNull(dt.Rows(i).Item("DATA_PG"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 8, par.IfNull(dt.Rows(i).Item("STATO"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 9, par.IfNull(dt.Rows(i).Item("ISE_ERP"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 10, par.IfNull(dt.Rows(i).Item("VSE"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 11, Replace(par.IfNull(dt.Rows(i).Item("ISEE"), ""), ".", ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 12, par.IfNull(dt.Rows(i).Item("VERIFICA"), ""))
                            .WriteValue(CM.ExcelFile.ValueTypes.xlsText, CM.ExcelFile.CellFont.xlsFont0, CM.ExcelFile.CellAlignment.xlsLeftAlign, CM.ExcelFile.CellHiddenLocked.xlsNormal, K, 13, par.IfNull(dt.Rows(i).Item("OPERATORE"), ""))
                            i = i + 1
                            K = K + 1
                        Next

                        .CloseFile()
                    End With

                End If

                Dim objCrc32 As New Crc32()
                Dim strmZipOutputStream As ZipOutputStream
                Dim zipfic As String

                zipfic = Server.MapPath("..\FileTemp\" & FileCSV & ".zip")

                strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
                strmZipOutputStream.SetLevel(6)

                Dim strFile As String
                strFile = Server.MapPath("..\FileTemp\" & FileCSV & ".xls")
                Dim strmFile As FileStream = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte

                strmFile.Read(abyBuffer, 0, abyBuffer.Length)

                Dim sFile As String = Path.GetFileName(strFile)
                Dim theEntry As ZipEntry = New ZipEntry(sFile)
                Dim fi As New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                strmZipOutputStream.Finish()
                strmZipOutputStream.Close()

                File.Delete(strFile)

                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                Dim scriptblock As String = "<script language='javascript' type='text/javascript'>" _
                                        & "window.open('../FileTemp/" & FileCSV & ".zip','Estrazione','');" _
                                        & "</script>"
                If (Not Page.ClientScript.IsClientScriptBlockRegistered("clientScript30023")) Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "clientScript30023", scriptblock)
                End If


                'Response.Redirect("..\FileTemp\" & FileCSV & ".zip")
            Else

            End If

        Catch ex As Exception
            HttpContext.Current.Session.Remove("AA1")

            Session.Add("ERRORE", "Provenienza:Export Excel Convocazioni - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try



    End Function

End Class
