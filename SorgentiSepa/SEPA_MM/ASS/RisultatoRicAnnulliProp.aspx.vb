
Partial Class ASS_RisultatoRicAnnulliProp
    Inherits PageSetIdMode
    Dim par As New CM.Global()
    Dim sValoreCG As String
    Dim sValoreNM As String
    Dim sValoreOF As String
    Dim sStringaSql As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Dim Str As String

        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)
        Response.Flush()
        If Not IsPostBack Then
            sValoreOF = Request.QueryString("OF")
            'btnVisualizza.Attributes.Add("onclick", "this.style.visibility='hidden'")
            LBLID.Text = "-1"
            Cerca()
        End If
    End Sub

    Private Sub Cerca()
        Try


            Dim bTrovato As Boolean
            Dim sValore As String
            'Dim sCompara As String

            bTrovato = False
            sStringaSql = ""

            If sValoreOF <> "" Then
                sValore = sValoreOF
                bTrovato = True
                sStringaSql = sStringaSql & " DOMANDE_OFFERTE_SCAD.ID =" & par.PulisciStrSql(sValore) & " AND "
            End If

            'par.OracleConn.Open()

            'Dim STRINGA As String = ""
            'If sValore <> "" Then
            '    STRINGA = "SELECT ID_DOMANDA FROM DOMANDE_OFFERTE_SCAD WHERE ID=" & par.PulisciStrSql(sValore)
            'Else
            '    STRINGA = "SELECT ID_DOMANDA FROM DOMANDE_OFFERTE_SCAD"
            'End If
            'Dim cmd As Oracle.DataAccess.Client.OracleCommand = New Oracle.DataAccess.Client.OracleCommand(STRINGA, par.OracleConn)
            'Dim myReader As Oracle.DataAccess.Client.OracleDataReader = cmd.ExecuteReader()

            sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
           & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
           & " WHERE " & sStringaSql & "  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
           & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
           & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
           & " " _
           & "AND (DOMANDE_BANDO.ID_STATO='9') ORDER BY COMP_NUCLEO.COGNOME ASC,COMP_NUCLEO.NOME ASC"



            sStringaSQL2 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
                           & "DOMANDE_BANDO_CAMBI.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                           & " FROM domande_offerte_scad,bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
                           & " WHERE " & sStringaSql & "  DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
                           & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
                           & "DOMANDE_BANDO_CAMBI.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
                           & " DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID_STATO<>'10' AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
                           & "AND (DOMANDE_BANDO_CAMBI.ID_STATO='9') ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"



            sStringaSQL3 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_VSA.reddito_isee,2) as ""reddito_isee"",DOMANDE_BANDO_VSA.ID,COMP_NUCLEO_VSA.COGNOME,COMP_NUCLEO_VSA.NOME," _
                        & "DOMANDE_BANDO_VSA.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
                        & " FROM domande_offerte_scad,DOMANDE_BANDO_VSA,COMP_NUCLEO_VSA,DICHIARAZIONI_VSA " _
                        & " WHERE " & sStringaSql & "  DOMANDE_BANDO_VSA.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO_VSA.PROGR_COMPONENTE=COMP_NUCLEO_VSA.PROGR AND DOMANDE_BANDO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID AND COMP_NUCLEO_VSA.ID_DICHIARAZIONE=DICHIARAZIONI_VSA.ID " _
                        & " AND  " _
                        & "DOMANDE_BANDO_VSA.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
                        & " DOMANDE_BANDO_VSA.FL_INVITO='1' AND DOMANDE_BANDO_VSA.ID_STATO<>'10' AND DOMANDE_BANDO_VSA.FL_PRATICA_CHIUSA<>'1' " _
                        & "AND (DOMANDE_BANDO_VSA.ID_STATO='9') ORDER BY COMP_NUCLEO_VSA.COGNOME ASC,COMP_NUCLEO_VSA.NOME ASC"


            'If myReader.Read() Then
            '    If myReader("ID_DOMANDA") >= 8000000 Then
            '        sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_VSA.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_VSA.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
            '                    & "DOMANDE_BANDO_CAMBI.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
            '                    & " FROM domande_offerte_scad,bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
            '                    & " WHERE " & sStringaSql & "  DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
            '                    & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
            '                    & "DOMANDE_BANDO_CAMBI.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
            '                    & " DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID_STATO<>'10' AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
            '                    & "AND (DOMANDE_BANDO_CAMBI.ID_STATO='9') ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"

            '    Else
            '        If myReader("ID_DOMANDA") >= 500000 Then
            '            sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO_CAMBI.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO_CAMBI.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def_CAMBI.posizione,DOMANDE_BANDO_CAMBI.ID,COMP_NUCLEO_CAMBI.COGNOME,COMP_NUCLEO_CAMBI.NOME," _
            '                           & "DOMANDE_BANDO_CAMBI.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
            '                           & " FROM domande_offerte_scad,bandi_graduatoria_def_CAMBI,DOMANDE_BANDO_CAMBI,COMP_NUCLEO_CAMBI,DICHIARAZIONI_CAMBI " _
            '                           & " WHERE " & sStringaSql & "  DOMANDE_BANDO_CAMBI.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO_CAMBI.PROGR_COMPONENTE=COMP_NUCLEO_CAMBI.PROGR AND DOMANDE_BANDO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID AND COMP_NUCLEO_CAMBI.ID_DICHIARAZIONE=DICHIARAZIONI_CAMBI.ID " _
            '                           & " AND  domande_bando_CAMBI.id = bandi_graduatoria_def_CAMBI.id_domanda and " _
            '                           & "DOMANDE_BANDO_CAMBI.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
            '                           & " DOMANDE_BANDO_CAMBI.FL_INVITO='1' AND DOMANDE_BANDO_CAMBI.ID_STATO<>'10' AND DOMANDE_BANDO_CAMBI.FL_PRATICA_CHIUSA<>'1' " _
            '                           & "AND (DOMANDE_BANDO_CAMBI.ID_STATO='9') ORDER BY COMP_NUCLEO_CAMBI.COGNOME ASC,COMP_NUCLEO_CAMBI.NOME ASC"
            '        Else
            '            sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
            '           & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
            '           & "" _
            '           & "" _
            '           & "" _
            '           & "" _
            '           & " " _
            '           & " FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
            '           & " WHERE " & sStringaSql & "  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
            '           & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and " _
            '           & "" _
            '           & "" _
            '           & "" _
            '           & "DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
            '           & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
            '           & " " _
            '           & "AND (DOMANDE_BANDO.ID_STATO='9') ORDER BY COMP_NUCLEO.COGNOME ASC,COMP_NUCLEO.NOME ASC"

            '        End If

            '    End If

            'Else
            '    sStringaSQL1 = "SELECT TO_CHAR(TO_DATE(domande_offerte_scad.DATA_SCADENZA,'YYYYmmdd'),'DD/MM/YYYY') AS ""SCADENZA"",trunc(DOMANDE_BANDO.isbarc_r,4) as ""isbarc_r"",trunc(DOMANDE_BANDO.reddito_isee,2) as ""reddito_isee"",bandi_graduatoria_def.posizione,DOMANDE_BANDO.ID,COMP_NUCLEO.COGNOME,COMP_NUCLEO.NOME," _
            '                   & "DOMANDE_BANDO.PG AS ""PG"",DOMANDE_OFFERTE_SCAD.ID AS ""OFFERTA"" " _
            '                   & "" _
            '                   & "" _
            '                   & "" _
            '                   & "" _
            '                   & " " _
            '                   & " FROM domande_offerte_scad,bandi_graduatoria_def,DOMANDE_BANDO,COMP_NUCLEO,DICHIARAZIONI " _
            '                   & " WHERE  DOMANDE_BANDO.FL_ASS_ESTERNA='1' AND domande_offerte_scad.FL_VALIDA='1' AND DOMANDE_BANDO.PROGR_COMPONENTE=COMP_NUCLEO.PROGR AND DOMANDE_BANDO.ID_DICHIARAZIONE=DICHIARAZIONI.ID AND COMP_NUCLEO.ID_DICHIARAZIONE=DICHIARAZIONI.ID " _
            '                   & " AND  domande_bando.id = bandi_graduatoria_def.id_domanda and " _
            '                   & "" _
            '                   & "" _
            '                   & "" _
            '                   & "DOMANDE_BANDO.ID=DOMANDE_OFFERTE_SCAD.ID_DOMANDA AND " _
            '                   & " DOMANDE_BANDO.FL_INVITO='1' AND DOMANDE_BANDO.ID_STATO<>'10' AND DOMANDE_BANDO.FL_PRATICA_CHIUSA<>'1' " _
            '                   & " " _
            '                   & "AND (DOMANDE_BANDO.ID_STATO='9') ORDER BY COMP_NUCLEO.COGNOME ASC,COMP_NUCLEO.NOME ASC"

            'End If





            'cmd.Dispose()
            'myReader.Close()
            'par.OracleConn.Close()
            BindGrid()
            BindGridCambi()
            BindGridEmergenze()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Public Property sStringaSQL3() As String
        Get
            If Not (ViewState("par_sStringaSQL3") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL3"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL3") = value
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

    Public Property sStringaSQL2() As String
        Get
            If Not (ViewState("par_sStringaSQL2") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL2"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL2") = value
        End Set

    End Property

    Private Sub BindGridEmergenze()
        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL3, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO_vsa,COMP_NUCLEO_vsa")
            DataGrid3.DataSource = ds
            DataGrid3.DataBind()
            'Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub


    Private Sub BindGridCambi()
        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL2, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO_cambi,COMP_NUCLEO_cambi")
            DataGrid2.DataSource = ds
            DataGrid2.DataBind()
            'Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Private Sub BindGrid()
        Try


            par.OracleConn.Open()

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            Dim ds As New Data.DataSet()
            da.Fill(ds, "DOMANDE_BANDO,COMP_NUCLEO")
            DataGrid1.DataSource = ds
            DataGrid1.DataBind()
            'Label3.Text = "  - " & DataGrid1.Items.Count & " nella pagina - Totale:" & ds.Tables(0).Rows.Count
            par.OracleConn.Close()
        Catch ex As Exception
            par.OracleConn.Close()
        End Try
    End Sub

    Protected Sub DataGrid1_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid1.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        HiddenField2.Value = LBLID.Text
        LBLPROGR.Text = e.Item.Cells(2).Text
        lblScad.Text = e.Item.Cells(3).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text
    End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
    e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid1_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid1.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid1.CurrentPageIndex = e.NewPageIndex
            BindGrid()
        End If
    End Sub



    Protected Sub btnAnnulla_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""Pagina_home.aspx""</script>")

    End Sub

    Protected Sub btnRicerca_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnRicerca.Click
        Response.Write("<script>document.location.href=""RicercaAnnulliProp.aspx""</script>")

    End Sub

    'Protected Sub btnVisualizza_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnVisualizza.Click
    '    'If LBLID.Text = "-1" Or LBLID.Text = "" Then
    '    '    Response.Write("<script>alert('Nessuna Domanda selezionata!')</script>")
    '    'Else
    '    '    Response.Write("<script>location.replace('DecidiOfferta.aspx?ID=" & LBLID.Text & "&OF=" & LBLPROGR.Text & "&SC=" & lblScad.Text & "');</script>")
    '    'End If
    'End Sub

    Protected Sub btnAccetta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAccetta.Click
        If HiddenField1.Value = "1" Then
            Try
                Dim RELAZIONE As String = "-1"
                Dim ID_ALLOGGIO As String = "-1"
                Dim MOTIVO As String = ""

                par.OracleConn.Open()
                par.SettaCommand(par)
                par.myTrans = par.OracleConn.BeginTransaction()
                ‘‘par.cmd.Transaction = par.myTrans

                If RadioButton1.Checked = True Then
                    MOTIVO = RadioButton1.Text
                End If

                If RadioButton2.Checked = True Then
                    MOTIVO = RadioButton2.Text
                End If

                If RadioButton3.Checked = True Then
                    MOTIVO = RadioButton3.Text
                End If

                If RadioButton4.Checked = True Then
                    MOTIVO = RadioButton4.Text
                End If

                If RadioButton5.Checked = True Then
                    MOTIVO = RadioButton5.Text
                End If

                If RadioButton6.Checked = True Then
                    MOTIVO = RadioButton6.Text
                End If

                If MOTIVO <> "" Then
                    par.cmd.CommandText = "SELECT id,ID_ALLOGGIO from rel_prat_all_ccaa_erp where id_pratica=" & LBLID.Text & " and ultimo='1'"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read() Then
                        RELAZIONE = myReader("id")
                        ID_ALLOGGIO = myReader("id_ALLOGGIO")
                    Else
                        RELAZIONE = "1"
                    End If
                    myReader.Close()

                    par.cmd.CommandText = "UPDATE REL_PRAT_ALL_CCAA_ERP SET DATA='" & Format(Now, "yyyyMMdd") & "',ESITO='3',MOTIVAZIONE='" & MOTIVO & "' WHERE ID=" & RELAZIONE
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "UPDATE ALLOGGI SET STATO=5,PRENOTATO='0',ID_PRATICA=null,ASSEGNATO='0' WHERE ID=" & ID_ALLOGGIO
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "INSERT INTO EVENTI_ALLOGGI (ID,DATA,ESITO,STATO,ID_ALLOGGIO,ID_PRATICA,MOTIVAZIONE) " _
                                        & "VALUES (SEQ_EVENTI_ALLOGGI.NEXTVAL ,'" & Format(Now, "yyyyMMdd") & "'," _
                                        & "3,5," _
                                        & ID_ALLOGGIO & "," _
                                        & LBLID.Text & ",'" & "" & "')"
                    par.cmd.ExecuteNonQuery()

                    If LBLID.Text > 8000000 Then
                        par.cmd.CommandText = "UPDATE DOMANDE_BANDO_VSA SET FL_PROPOSTA='0' WHERE ID=" & LBLID.Text
                    Else
                        If LBLID.Text < 500000 Then
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO SET FL_PROPOSTA='0' WHERE ID=" & LBLID.Text
                        Else
                            par.cmd.CommandText = "UPDATE DOMANDE_BANDO_CAMBI SET FL_PROPOSTA='0' WHERE ID=" & LBLID.Text
                        End If
                    End If

                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO EVENTI_BANDI (ID_DOMANDA,ID_OPERATORE,DATA_ORA,STATO_PRATICA,COD_EVENTO,MOTIVAZIONE,TIPO_OPERATORE) " _
                                    & "VALUES (" & LBLID.Text & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "','9" _
                                    & "','F64','','I')"
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "UPDATE DOMANDE_OFFERTE_SCAD SET FL_VALIDA='0' WHERE ID_DOMANDA=" & LBLID.Text & " AND ID=" & LBLPROGR.Text
                par.cmd.ExecuteNonQuery()

                LBLID.Text = ""
                Label2.Text = "Nessuna Selezione"
                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Operazione Effettuata!');</script>")
                    BindGrid()
                    BindGridCambi()
                    BindGridEmergenze()

                Else

                par.myTrans.Commit()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Response.Write("<script>alert('Specificare il motivo!');</script>")
                End If

            Catch ex As Exception
                par.myTrans.Rollback()
                par.OracleConn.Close()
                Label2.Text = ex.Message
            End Try


        End If
    End Sub

    Protected Sub DataGrid2_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid2.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        HiddenField2.Value = LBLID.Text
        LBLPROGR.Text = e.Item.Cells(2).Text
        lblScad.Text = e.Item.Cells(3).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text

    End Sub

    Protected Sub DataGrid2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid2.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub


    Protected Sub DataGrid2_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid2.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid2.CurrentPageIndex = e.NewPageIndex
            BindGridCambi()
        End If
    End Sub

    Protected Sub DataGrid3_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGrid3.EditCommand
        LBLID.Text = e.Item.Cells(0).Text
        HiddenField2.Value = LBLID.Text
        LBLPROGR.Text = e.Item.Cells(2).Text
        lblScad.Text = e.Item.Cells(3).Text
        Label2.Text = "Hai selezionato: PG " & e.Item.Cells(1).Text

    End Sub

    Protected Sub DataGrid3_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid3.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or _
e.Item.ItemType = ListItemType.AlternatingItem Then
            '---------------------------------------------------         
            ' Add the OnMouseOver and OnMouseOut method to the Row of DataGrid         
            '---------------------------------------------------         
            e.Item.Attributes.Add("onmouseover", "this.style.backgroundColor='Silver'")
            e.Item.Attributes.Add("onmouseout", "this.style.backgroundColor='white'")
        End If
    End Sub

    Protected Sub DataGrid3_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGrid3.PageIndexChanged
        If e.NewPageIndex >= 0 Then
            DataGrid3.CurrentPageIndex = e.NewPageIndex
            BindGridEmergenze()
        End If
    End Sub
End Class
