Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class ANAUT_NuovoGruppoConv_1
    Inherits PageSetIdMode
    Dim PAR As New CM.Global
    Dim DT As New Data.DataTable
    Dim INDICEBANDO As Long = 0
    Dim DataInzio As String = ""

    Dim Tipo1 As Integer = 0
    Dim Tipo2 As Integer = 0
    Dim Tipo3 As Integer = 0
    Dim Tipo4 As Integer = 0
    Dim Tipo5 As Integer = 0
    Dim Tipo6 As Integer = 0
    Dim Tipo7 As Integer = 0
    Dim Tipo8 As Integer = 0
    Dim Tipo9 As Integer = 0
    Dim Tipo10 As Integer = 0
    Dim Tipo11 As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Loading As String = "<div id=""divLoading"" Style=""position:absolute;margin: 0px; width: 100%; height: 100%;" _
           & "top: 0px; left: 0px;background-color: #ffffff;z-index:1000;"">" _
           & "<div style=""position: absolute; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;" _
           & "margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');"">" _
           & "<table style=""width: 100%; height: 100%;""><tr><td valign=""middle"" align=""center"">" _
           & "<img src=""../NuoveImm/load.gif"" alt=""Caricamento in corso"" /><br /><br />" _
           & "<span id=""Label4"" style=""font-family:Arial;font-size:10pt;"">Caricamento in corso...</span>" _
           & "</td></tr></table></div></div>"
        Response.Write(Loading)

        If Not IsPostBack Then
            Response.Flush()
            CaricaDati()
            CaricaDatiAU()
        End If
    End Sub

    Private Function CaricaDatiAU()
        Try
            If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                PAR.OracleConn.Open()
                par.SettaCommand(par)
            End If
            PAR.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & Request.QueryString("IDB")
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
            If myReader.Read Then
                Label1.Text = "Convocabili AU: " & PAR.IfNull(myReader("descrizione"), "") & " - " & PAR.DeCripta(Request.QueryString("Q")) & " - " & Format(Now, "dd/MM/yyyy HH:mm") & " - " & Session.Item("operatore")
                INDICEBANDO = PAR.IfNull(myReader("ID"), 0)
                DataInzio = PAR.IfNull(myReader("DATA_INIZIO"), "")

                Tipo1 = PAR.IfNull(myReader("ERP_1"), 0) 'ERP SOCIALE
                Tipo2 = PAR.IfNull(myReader("ERP_2"), 0) 'ERP MODERATO
                Tipo3 = PAR.IfNull(myReader("ERP_ART_22"), 0) 'ART 200 C 10
                Tipo4 = PAR.IfNull(myReader("ERP_4"), 0)
                Tipo5 = PAR.IfNull(myReader("ERP_5"), 0)
                Tipo10 = PAR.IfNull(myReader("ERP_3"), 0)
                Tipo6 = PAR.IfNull(myReader("L43198"), 0)
                Tipo7 = PAR.IfNull(myReader("L39278"), 0)
                Tipo8 = PAR.IfNull(myReader("ERP_FF_OO"), 0) 'FF.OO.
                Tipo9 = PAR.IfNull(myReader("ERP_CONV"), 0) 'ERP CONVENZIONATO
                Tipo11 = PAR.IfNull(myReader("OA"), 0)

                Dim ss As String = ""

                If Tipo1 = 1 Then ss = ss & "Erp Sociale,"
                If Tipo2 = 1 Then ss = ss & "Erp Moderato,"
                If Tipo3 = 1 Then ss = ss & "ART.22 C.10 RR 1/2004,"
                If Tipo4 = 1 Then ss = ss & "4.	art.15 comma 2-vizi amministrativi,"
                If Tipo5 = 1 Then ss = ss & "5.	Legge 10/86,"
                If Tipo6 = 1 Then ss = ss & "431/98,"
                If Tipo7 = 1 Then ss = ss & "392/78,"
                If Tipo8 = 1 Then ss = ss & "Erp FF.OO.,"
                If Tipo9 = 1 Then ss = ss & "Erp Convenzionato,"
                If Tipo10 = 1 Then ss = ss & "Erp Art.15 let. a, b - 431 Deroga,"
                If Tipo11 = 1 Then ss = ss & "Occupazioni Abusive,"
                ss = Mid(ss, 1, Len(ss) - 1)

            End If
            myReader.Close()

            If Request.QueryString("ID") <> "" Then


                Dim I As Integer = 0

                Dim oDataGridItem As DataGridItem
                Dim chkExport As System.Web.UI.WebControls.CheckBox
                DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)


                PAR.cmd.CommandText = "select * from UTENZA_LISTE_CDETT WHERE MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV=" & Request.QueryString("ID")


                Dim myreader2 As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                Do While myreader2.Read
                    I = 0
                    For Each oDataGridItem In Me.DataGridRateEmesse.Items
                        chkExport = oDataGridItem.FindControl("ChSelezionato")
                        If PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("ID_LISTA"), "")) = myreader2("ID_LISTA") And PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("ID_CONTRATTO"), "")) = myreader2("ID_CONTRATTO") Then
                            chkExport.Checked = True
                        End If
                        I = I + 1
                    Next
                Loop
                myreader2.Close()

                PAR.cmd.CommandText = "select * from UTENZA_GRUPPI_CONV WHERE ID=" & Request.QueryString("ID")
                myreader2 = PAR.cmd.ExecuteReader()
                If myreader2.Read Then
                    txtDescrizione.Text = PAR.IfNull(myreader2("descrizione"), "")
                End If
                myreader2.Close()

            End If

            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            PAR.OracleConn.Close()
            PAR.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Label1.Text = ex.Message
            Label1.Visible = True
        End Try
    End Function

    Private Function CaricaDati()
        Try
            Dim S As String = ""
            Dim S1 As String = ""
            Dim da As Oracle.DataAccess.Client.OracleDataAdapter
            Dim S2 As String = ""
            Dim ss As String = "("

            If Request.QueryString("ID") = "" Then
                If Request.QueryString("TIPOC") = "" Then
                    Tabella = "select UTENZA_LISTE_CDETT.*,'FALSE' AS SELEZIONA from UTENZA_LISTE_CDETT,SISCOM_MI.RAPPORTI_UTENZA WHERE " & PAR.DeCripta(Request.QueryString("S")) & " RAPPORTI_UTENZA.ID=UTENZA_LISTE_CDETT.ID_CONTRATTO AND ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU=" & Request.QueryString("IDB") & ") AND  MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV IS NULL AND ID_LISTA_CONV IS NULL ORDER BY FILIALE,SEDE,INTESTATARIO"
                Else
                    Select Case Request.QueryString("TIPOC")
                        Case "1"
                            ss = ss & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or "
                        Case "2"
                            ss = ss & " unita_immobiliari.id_destinazione_uso = 2 or "
                        Case "3"
                            'ss = ss & " unita_immobiliari.id_destinazione_uso = 10 or "
                            ss = ss & " rapporti_utenza.provenienza_ass = 10 or "
                        Case "4"
                            ss = ss & " rapporti_utenza.provenienza_ass = 8 or "
                        Case "5"
                            ss = ss & " unita_immobiliari.id_destinazione_uso = 12 or "
                        Case "6"

                        Case "7"
                            ss = ss & " rapporti_utenza.dest_uso = 'D' OR "
                        Case "8"

                        Case "9"

                        Case "10"
                            ss = ss & " rapporti_utenza.provenienza_ass = 7 or "
                    End Select

                    If ss = "(" Then
                        ss = "(rapporti_utenza.dest_uso='X') "
                    Else
                        ss = Mid(ss, 1, Len(ss) - 4) & ") AND "
                    End If

                    Tabella = "select UTENZA_LISTE_CDETT.*,'FALSE' AS SELEZIONA from UTENZA_LISTE_CDETT,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " & ss & " " & PAR.DeCripta(Request.QueryString("S")) & " UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.ID=UTENZA_LISTE_CDETT.ID_CONTRATTO AND ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU=" & Request.QueryString("IDB") & ") AND  MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV IS NULL AND ID_LISTA_CONV IS NULL ORDER BY FILIALE,SEDE,INTESTATARIO"

                End If
            Else
                If Request.QueryString("TIPOC") = "" Then
                    'Tabella = "select 'TRUE' AS SELEZIONA,ID_LISTA, ID_CONTRATTO, UTENZA_LISTE_CDETT.COD_CONTRATTO, INTESTATARIO, QUARTIERE, EDIFICIO, INDIRIZZO_UI, UTENZA_LISTE_CDETT.TIPO_COR, UTENZA_LISTE_CDETT.VIA_COR, UTENZA_LISTE_CDETT.CIVICO_COR, UTENZA_LISTE_CDETT.LUOGO_COR, UTENZA_LISTE_CDETT.SIGLA_COR, UTENZA_LISTE_CDETT.CAP_COR, FILIALE, SEDE, TIPO_CONTRATTO, TIPO_SPECIFICO, UTENZA_LISTE_CDETT.DATA_STIPULA, DATA_SLOGGIO, UI_VENDUTA, BOLL_SPESE, NUM_COMP, NUM_COMP_66, NUM_COMP_100, NUM_COMP_100_CON, PREVALENTE_DIPENDENTE, REDDITI_IMMOBILIARI, AREA_ECONOMICA, CLASSE, MOTIVAZIONE, MINORI_15, MAGGIORI_65, ID_SPORTELLO, ID_TAB_FILIALI, ID_GRUPPO_CONV from UTENZA_LISTE_CDETT.RAPPORTI_UTENZA WHERE " & PAR.DeCripta(Request.QueryString("S")) & " RAPPORTI_UTENZA.ID=UTENZA_LISTE_CDETT.ID_CONTRATTO AND ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU=" & Request.QueryString("IDB") & ") AND  MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV=" & Request.QueryString("ID") & "  UNION SELECT 'FALSE' AS SELEZIONA,ID_LISTA, ID_CONTRATTO, UTENZA_LISTE_CDETT.COD_CONTRATTO, INTESTATARIO, QUARTIERE, EDIFICIO, INDIRIZZO_UI, UTENZA_LISTE_CDETT.TIPO_COR, UTENZA_LISTE_CDETT.VIA_COR, UTENZA_LISTE_CDETT.CIVICO_COR, UTENZA_LISTE_CDETT.LUOGO_COR, UTENZA_LISTE_CDETT.SIGLA_COR, UTENZA_LISTE_CDETT.CAP_COR, FILIALE, SEDE, TIPO_CONTRATTO, TIPO_SPECIFICO, UTENZA_LISTE_CDETT.DATA_STIPULA, DATA_SLOGGIO, UI_VENDUTA, BOLL_SPESE, NUM_COMP, NUM_COMP_66, NUM_COMP_100, NUM_COMP_100_CON, PREVALENTE_DIPENDENTE, REDDITI_IMMOBILIARI, AREA_ECONOMICA, CLASSE, MOTIVAZIONE, MINORI_15, MAGGIORI_65, ID_SPORTELLO, ID_TAB_FILIALI, ID_GRUPPO_CONV from UTENZA_LISTE_CDETT,SISCOM_MI.RAPPORTI_UTENZA WHERE " & PAR.DeCripta(Request.QueryString("S")) & " RAPPORTI_UTENZA.ID=UTENZA_LISTE_CDETT.ID_CONTRATTO AND ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU=" & Request.QueryString("IDB") & ") AND  MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV IS NULL ORDER BY FILIALE,SEDE,INTESTATARIO"
                    Tabella = "select 'TRUE' AS SELEZIONA,ID_LISTA, ID_CONTRATTO, UTENZA_LISTE_CDETT.COD_CONTRATTO, INTESTATARIO, QUARTIERE, EDIFICIO, INDIRIZZO_UI, UTENZA_LISTE_CDETT.TIPO_COR, UTENZA_LISTE_CDETT.VIA_COR, UTENZA_LISTE_CDETT.CIVICO_COR, UTENZA_LISTE_CDETT.LUOGO_COR, UTENZA_LISTE_CDETT.SIGLA_COR, UTENZA_LISTE_CDETT.CAP_COR, FILIALE, SEDE, TIPO_CONTRATTO, TIPO_SPECIFICO, UTENZA_LISTE_CDETT.DATA_STIPULA, DATA_SLOGGIO, UI_VENDUTA, BOLL_SPESE, NUM_COMP, NUM_COMP_66, NUM_COMP_100, NUM_COMP_100_CON, PREVALENTE_DIPENDENTE, REDDITI_IMMOBILIARI, AREA_ECONOMICA, CLASSE, MOTIVAZIONE, MINORI_15, MAGGIORI_65, ID_SPORTELLO, ID_TAB_FILIALI, ID_GRUPPO_CONV from UTENZA_LISTE_CDETT WHERE MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV=" & Request.QueryString("ID") & "  UNION SELECT 'FALSE' AS SELEZIONA,ID_LISTA, ID_CONTRATTO, UTENZA_LISTE_CDETT.COD_CONTRATTO, INTESTATARIO, QUARTIERE, EDIFICIO, INDIRIZZO_UI, UTENZA_LISTE_CDETT.TIPO_COR, UTENZA_LISTE_CDETT.VIA_COR, UTENZA_LISTE_CDETT.CIVICO_COR, UTENZA_LISTE_CDETT.LUOGO_COR, UTENZA_LISTE_CDETT.SIGLA_COR, UTENZA_LISTE_CDETT.CAP_COR, FILIALE, SEDE, TIPO_CONTRATTO, TIPO_SPECIFICO, UTENZA_LISTE_CDETT.DATA_STIPULA, DATA_SLOGGIO, UI_VENDUTA, BOLL_SPESE, NUM_COMP, NUM_COMP_66, NUM_COMP_100, NUM_COMP_100_CON, PREVALENTE_DIPENDENTE, REDDITI_IMMOBILIARI, AREA_ECONOMICA, CLASSE, MOTIVAZIONE, MINORI_15, MAGGIORI_65, ID_SPORTELLO, ID_TAB_FILIALI, ID_GRUPPO_CONV from UTENZA_LISTE_CDETT,SISCOM_MI.RAPPORTI_UTENZA WHERE " & PAR.DeCripta(Request.QueryString("S")) & " RAPPORTI_UTENZA.ID=UTENZA_LISTE_CDETT.ID_CONTRATTO AND ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU=" & Request.QueryString("IDB") & ") AND  MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV IS NULL ORDER BY FILIALE,SEDE,INTESTATARIO"
                Else
                    Select Case Request.QueryString("TIPOC")
                        Case "1"
                            ss = ss & " (rapporti_utenza.provenienza_ass = 1 AND unita_immobiliari.id_destinazione_uso <> 2) or "
                        Case "2"
                            ss = ss & " unita_immobiliari.id_destinazione_uso = 2 or "
                        Case "3"
                            'ss = ss & " unita_immobiliari.id_destinazione_uso = 10 or "
                            ss = ss & " rapporti_utenza.provenienza_ass = 10 or "
                        Case "4"
                            ss = ss & " rapporti_utenza.provenienza_ass = 8 or "
                        Case "5"
                            ss = ss & " unita_immobiliari.id_destinazione_uso = 12 or "
                        Case "6"

                        Case "7"
                            ss = ss & " rapporti_utenza.dest_uso = 'D' OR "
                        Case "8"

                        Case "9"

                        Case "10"
                            ss = ss & " rapporti_utenza.provenienza_ass = 7 or "
                    End Select

                    If ss = "(" Then
                        ss = "(rapporti_utenza.dest_uso='X') "
                    Else
                        ss = Mid(ss, 1, Len(ss) - 4) & ") AND "
                    End If
                    Tabella = "select 'TRUE' AS SELEZIONA,ID_LISTA, ID_CONTRATTO, UTENZA_LISTE_CDETT.COD_CONTRATTO, INTESTATARIO, QUARTIERE, EDIFICIO, INDIRIZZO_UI, UTENZA_LISTE_CDETT.TIPO_COR, UTENZA_LISTE_CDETT.VIA_COR, UTENZA_LISTE_CDETT.CIVICO_COR, UTENZA_LISTE_CDETT.LUOGO_COR, UTENZA_LISTE_CDETT.SIGLA_COR, UTENZA_LISTE_CDETT.CAP_COR, FILIALE, SEDE, TIPO_CONTRATTO, TIPO_SPECIFICO, UTENZA_LISTE_CDETT.DATA_STIPULA, DATA_SLOGGIO, UI_VENDUTA, BOLL_SPESE, NUM_COMP, NUM_COMP_66, NUM_COMP_100, NUM_COMP_100_CON, PREVALENTE_DIPENDENTE, REDDITI_IMMOBILIARI, AREA_ECONOMICA, CLASSE, MOTIVAZIONE, MINORI_15, MAGGIORI_65, ID_SPORTELLO, ID_TAB_FILIALI, ID_GRUPPO_CONV from UTENZA_LISTE_CDETT WHERE MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV=" & Request.QueryString("ID") & "  UNION SELECT 'FALSE' AS SELEZIONA,ID_LISTA, UTENZA_LISTE_CDETT.ID_CONTRATTO, UTENZA_LISTE_CDETT.COD_CONTRATTO, INTESTATARIO, QUARTIERE, EDIFICIO, INDIRIZZO_UI, UTENZA_LISTE_CDETT.TIPO_COR, UTENZA_LISTE_CDETT.VIA_COR, UTENZA_LISTE_CDETT.CIVICO_COR, UTENZA_LISTE_CDETT.LUOGO_COR, UTENZA_LISTE_CDETT.SIGLA_COR, UTENZA_LISTE_CDETT.CAP_COR, FILIALE, SEDE, TIPO_CONTRATTO, TIPO_SPECIFICO, UTENZA_LISTE_CDETT.DATA_STIPULA, DATA_SLOGGIO, UI_VENDUTA, BOLL_SPESE, NUM_COMP, NUM_COMP_66, NUM_COMP_100, NUM_COMP_100_CON, PREVALENTE_DIPENDENTE, REDDITI_IMMOBILIARI, AREA_ECONOMICA, CLASSE, MOTIVAZIONE, MINORI_15, MAGGIORI_65, ID_SPORTELLO, ID_TAB_FILIALI, ID_GRUPPO_CONV from UTENZA_LISTE_CDETT,SISCOM_MI.RAPPORTI_UTENZA,SISCOM_MI.UNITA_CONTRATTUALE,SISCOM_MI.UNITA_IMMOBILIARI WHERE " & ss & " " & PAR.DeCripta(Request.QueryString("S")) & " UNITA_IMMOBILIARI.ID=UNITA_CONTRATTUALE.ID_UNITA AND UNITA_CONTRATTUALE.ID_UNITA_PRINCIPALE IS NULL AND UNITA_CONTRATTUALE.ID_CONTRATTO=RAPPORTI_UTENZA.ID AND RAPPORTI_UTENZA.ID=UTENZA_LISTE_CDETT.ID_CONTRATTO AND ID_LISTA IN (SELECT ID FROM UTENZA_LISTE_CONV WHERE ID_AU=" & Request.QueryString("IDB") & ") AND  MOTIVAZIONE IS NULL AND ID_GRUPPO_CONV IS NULL ORDER BY FILIALE,SEDE,INTESTATARIO"



                End If
                End If


                PAR.cmd.CommandText = Tabella
                da = New Oracle.DataAccess.Client.OracleDataAdapter(PAR.cmd.CommandText, PAR.OracleConn)
                da.Fill(DT)

                DataGridRateEmesse.DataSource = DT
                DataGridRateEmesse.DataBind()
                Session.Add("MIADT", DT)

                Label2.Text = DataGridRateEmesse.Items.Count & " nella pagina - Totale :" & DT.Rows.Count


        Catch ex As Exception
            'Beep()
        End Try
    End Function

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set
    End Property


    Protected Sub imgSelezionaTutto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSelezionaTutto.Click
        'Dim oDataGridItem As DataGridItem
        'Dim chkExport As System.Web.UI.WebControls.CheckBox


        'For Each oDataGridItem In Me.DataGridRateEmesse.Items
        '    chkExport = oDataGridItem.FindControl("ChSelezionato")
        '    chkExport.Checked = True
        'Next
        Try
            Dim DT1 As New Data.DataTable
            DT1 = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            For Each row As Data.DataRow In DT1.Rows

                row.Item("SELEZIONA") = "TRUE"

            Next
            For Each riga As DataGridItem In DataGridRateEmesse.Items

                If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = False Then
                    CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True
                End If
            Next
            Session.Item("MIADT") = DT1

        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Nuovogruppoconv_1 - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub

    Protected Sub imgDeselezionaTutto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDeselezionaTutto.Click
        'Dim oDataGridItem As DataGridItem
        'Dim chkExport As System.Web.UI.WebControls.CheckBox


        'For Each oDataGridItem In Me.DataGridRateEmesse.Items
        '    chkExport = oDataGridItem.FindControl("ChSelezionato")
        '    chkExport.Checked = False
        'Next
        Try
            Dim DT1 As New Data.DataTable
            DT1 = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            For Each row As Data.DataRow In DT1.Rows

                row.Item("SELEZIONA") = "FALSE"

            Next
            For Each riga As DataGridItem In DataGridRateEmesse.Items

                If CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = True Then
                    CType(riga.FindControl("ChSelezionato"), CheckBox).Checked = False
                End If

            Next
            Session.Item("MIADT") = DT1
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza: Nuovogruppoconv_1 - " & ex.Message)
            Response.Write("<script>top.location.href=""../Errore.aspx""</script>")
        End Try
    End Sub

    'Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click

    'End Sub

    Protected Sub btnSalva_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        If txtDescrizione.Text <> "" Then
            Try
                If PAR.OracleConn.State = Data.ConnectionState.Closed Then
                    PAR.OracleConn.Open()
                    par.SettaCommand(par)
                End If
                PAR.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                Dim S As String = ""

                If Request.QueryString("ID") = "" Then
                    PAR.cmd.CommandText = "insert into UTENZA_GRUPPI_CONV values (SEQ_UTENZA_GRUPPI_CONV.NEXTVAL," & Request.QueryString("IDB") & ",'" & PAR.PulisciStrSql(txtDescrizione.Text) & "','" & Format(Now, "yyyyMMddHHmm") & "','" & PAR.PulisciStrSql(Session.Item("OPERATORE")) & "','" & PAR.PulisciStrSql(Label1.Text) & "')"
                    PAR.cmd.ExecuteNonQuery()


                    PAR.cmd.CommandText = "SELECT SEQ_UTENZA_GRUPPI_CONV.CURRVAL FROM DUAL"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = PAR.cmd.ExecuteReader()
                    If myReader.Read() Then
                        S = myReader(0)
                    End If
                    myReader.Close()

                Else
                    PAR.cmd.CommandText = "UPDATE UTENZA_GRUPPI_CONV SET DESCRIZIONE='" & PAR.PulisciStrSql(txtDescrizione.Text) & "',DATA_ORA='" & Format(Now, "yyyyMMddHHmm") & "',OPERATORE='" & PAR.PulisciStrSql(Session.Item("OPERATORE")) & "',CRITERI='" & PAR.PulisciStrSql(Label1.Text) & "' WHERE ID=" & Request.QueryString("ID")
                    PAR.cmd.ExecuteNonQuery()
                    S = Request.QueryString("ID")
                End If

                Dim I As Long = 0

                'Dim oDataGridItem As DataGridItem
                'Dim chkExport As System.Web.UI.WebControls.CheckBox
                AggiustaCompSessione()
                DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
                'For Each oDataGridItem In Me.DataGridRateEmesse.Items
                '    chkExport = oDataGridItem.FindControl("ChSelezionato")
                '    If chkExport.Checked Then
                '        PAR.cmd.CommandText = "UPDATE UTENZA_LISTE_CDETT SET ID_GRUPPO_CONV=" & S & " WHERE ID_LISTA=" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("ID_LISTA"), "")) & " AND ID_CONTRATTO=" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("ID_CONTRATTO"), ""))
                '        PAR.cmd.ExecuteNonQuery()
                '    End If
                '    I = I + 1
                'Next

                For Each row As Data.DataRow In DT.Rows
                    If row.Item("SELEZIONA") = "TRUE" Then
                        PAR.cmd.CommandText = "UPDATE UTENZA_LISTE_CDETT SET ID_GRUPPO_CONV=" & S & " WHERE ID_LISTA=" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("ID_LISTA"), "")) & " AND ID_CONTRATTO=" & PAR.PulisciStrSql(PAR.IfNull(DT.Rows(I).Item("ID_CONTRATTO"), ""))
                        PAR.cmd.ExecuteNonQuery()
                    End If
                    I = I + 1
                Next


                PAR.myTrans.Commit()
                PAR.OracleConn.Close()
                PAR.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Response.Write("<script>alert('Operazione effettuata! Il Gruppo è richiamabile dalla funzione Elenco Gruppi Conv.');window.opener.location.reload('GestGruppiConv.aspx');self.close();</script>")

            Catch ex As Exception
                PAR.myTrans.Rollback()
                PAR.OracleConn.Close()
                PAR.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Label1.Text = ex.Message
                Label1.Visible = True
            End Try
        Else
            Response.Write("<script>alert('Inserire una descrizione per il Gruppo');</script>")
        End If
    End Sub

    Protected Sub DataGridRateEmesse_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridRateEmesse.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            AggiustaCompSessione()
            DataGridRateEmesse.CurrentPageIndex = e.NewPageIndex
            DataGridRateEmesse.DataSource = Session.Item("MIADT")
            DataGridRateEmesse.DataBind()
            Label2.Text = DataGridRateEmesse.Items.Count & " nella pagina - Totale :" & CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable).Rows.Count
        End If
    End Sub

    Private Sub AggiustaCompSessione()
        Try
            Dim dt As Data.DataTable = Session.Item("MIADT")
            Dim row As Data.DataRow
            For i As Integer = 0 To DataGridRateEmesse.Items.Count - 1
                If DirectCast(DataGridRateEmesse.Items(i).Cells(1).FindControl("ChSelezionato"), CheckBox).Checked = False Then
                    row = dt.Select("ID_CONTRATTO = " & DataGridRateEmesse.Items(i).Cells(2).Text)(0)
                    row.Item("SELEZIONA") = "FALSE"
                Else
                    row = dt.Select("ID_CONTRATTO = " & DataGridRateEmesse.Items(i).Cells(2).Text)(0)
                    row.Item("SELEZIONA") = "TRUE"
                End If
            Next
            Session.Item("MIADT") = dt
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub DataGridRateEmesse_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridRateEmesse.SelectedIndexChanged

    End Sub
End Class
