
Partial Class ANAUT_GestioneTabelleBando
    Inherits PageSetIdMode
    Dim par As New CM.Global


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If Not IsPostBack Then
            Carica()
        End If
    End Sub

    Public Property AnnoAU() As String
        Get
            If Not (ViewState("par_AnnoAU") Is Nothing) Then
                Return CStr(ViewState("par_AnnoAU"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_AnnoAU") = value
        End Set

    End Property

    Public Property IdBando() As Long
        Get
            If Not (ViewState("par_IdBando") Is Nothing) Then
                Return CLng(ViewState("par_IdBando"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdBando") = value
        End Set

    End Property

    Public Property IdNuovo() As Long
        Get
            If Not (ViewState("par_IdNuovo") Is Nothing) Then
                Return CLng(ViewState("par_IdNuovo"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_IdNuovo") = value
        End Set

    End Property

    Private Sub Carica()
        CreaTabella("2015")
RIPETI_LETTURA:
        Try
            IdBando = CLng(Request.QueryString("ID"))

            par.OracleConn.Open()
            par.SettaCommand(par)

            par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI WHERE id=" & IdBando
            Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader.Read Then
                Label1.Text = "PARAMETRI ANAGRAFE UTENZA ANNO " & myReader("ANNO_AU") & " REDDITI " & myReader("ANNO_ISEE")
                AnnoAU = myReader("ANNO_AU")

                par.cmd.CommandText = "SELECT * FROM SEPA.UTENZA_BANDI_PARAMETRI WHERE ID_AU=" & IdBando
                Dim myReaderX As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReaderX.HasRows = False Then
                    par.cmd.CommandText = "INSERT INTO SEPA.UTENZA_BANDI_PARAMETRI (ID_AU, LIMITE_PENSIONE, ISTAT_1_PR, ISTAT_2_PR, ISTAT_1_AC, ISTAT_2_AC, ISTAT_1_PE, ISTAT_2_PE, ISTAT_1_DE, ISTAT_2_DE, ICI_1_2, ICI_3_4, ICI_5_6, ICI_7) VALUES (" & IdBando & ", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)"
                    par.cmd.ExecuteNonQuery()
                End If
                myReaderX.Close()

                par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "")
                Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='A1'"
                    Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox2.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox3.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox4.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox5.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='A2'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox6.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox7.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox8.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox9.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='A3'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox10.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox11.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox12.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox13.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='A4'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox14.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox15.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox16.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox17.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='A5'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox18.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox19.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox20.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox21.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()


                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='B1'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox22.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox23.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox24.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox25.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='B2'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox26.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox27.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox28.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox29.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='B3'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox30.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox31.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox32.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox33.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='B4'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox34.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox35.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox36.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox37.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='B5'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox38.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox39.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox40.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox41.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    'PERMANENZA
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C1'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox42.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox43.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox44.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox45.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C2'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox46.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox47.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox48.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox49.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C3'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox50.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox51.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox52.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox53.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C4'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox54.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox55.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox56.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox57.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C5'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox58.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox59.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox60.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox61.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C6'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox62.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox63.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox64.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox65.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C7'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox66.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox67.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox68.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox69.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C8'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox70.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox71.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox72.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox73.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C9'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox74.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox75.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox76.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox77.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C10'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox78.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox79.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox80.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox81.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C11'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox82.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox83.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox84.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox85.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='C12'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox86.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox87.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox88.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox89.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='D1'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox90.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox91.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox92.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox93.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='D2'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox94.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox95.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox96.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox97.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='D3'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        TextBox98.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox99.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox100.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox101.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & par.IfNull(myReader("anno_AU"), "") & " where sotto_area='D4'"
                    myReader2 = par.cmd.ExecuteReader()
                    If myReader2.Read Then
                        'TextBox102.Text = par.IfNull(myReader2("isee_erp"), "")
                        TextBox103.Text = par.IfNull(myReader2("perc_val_locativo"), "0")
                        TextBox104.Text = par.IfNull(myReader2("inc_max_isee_erp"), "0")
                        TextBox105.Text = par.IfNull(myReader2("canone_minimo"), "0")
                    End If
                    myReader2.Close()
                End If
                myReader1.Close()

                par.cmd.CommandText = "SELECT * FROM UTENZA_BANDI_PARAMETRI WHERE ID_AU=" & IdBando
                myReader1 = par.cmd.ExecuteReader()
                If myReader1.Read Then
                    txtPensione.Text = par.IfNull(myReader1("LIMITE_PENSIONE"), "0")

                    txtISTAT_1_PR.Text = par.IfNull(myReader1("ISTAT_1_PR"), "0")
                    txtISTAT_2_PR.Text = par.IfNull(myReader1("ISTAT_2_PR"), "0")

                    txtISTAT_1_AC.Text = par.IfNull(myReader1("ISTAT_1_AC"), "0")
                    txtISTAT_2_AC.Text = par.IfNull(myReader1("ISTAT_2_AC"), "0")

                    txtISTAT_1_PE.Text = par.IfNull(myReader1("ISTAT_1_PE"), "0")
                    txtISTAT_2_PE.Text = par.IfNull(myReader1("ISTAT_2_PE"), "0")

                    txtISTAT_1_DE.Text = par.IfNull(myReader1("ISTAT_1_DE"), "0")
                    txtISTAT_2_DE.Text = par.IfNull(myReader1("ISTAT_2_DE"), "0")

                    txtICI_1_2.Text = par.IfNull(myReader1("ICI_1_2"), "0")
                    txtICI_3_4.Text = par.IfNull(myReader1("ICI_3_4"), "0")
                    txtICI_5_6.Text = par.IfNull(myReader1("ICI_5_6"), "0")
                    txtICI_7.Text = par.IfNull(myReader1("ICI_7"), "0")

                End If
                myReader1.Close()
            Else
                Response.Write("<script>alert('Attenzione...prima di inserire questi valori assicurarsi di aver creato una AU!');</script>")
                btnSalva.Visible = False
            End If
            myReader.Close()




            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex1 As Oracle.DataAccess.Client.OracleException
            If EX1.Number = 942 Then

                'par.cmd.CommandText = "INSERT INTO SEPA.UTENZA_BANDI_PARAMETRI (ID_AU, LIMITE_PENSIONE, ISTAT_1_PR, ISTAT_2_PR, ISTAT_1_AC, ISTAT_2_AC, ISTAT_1_PE, ISTAT_2_PE, ISTAT_1_DE, ISTAT_2_DE, ICI_1_2, ICI_3_4, ICI_5_6, ICI_7) VALUES (" & IdBando & ", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)"
                'par.cmd.ExecuteNonQuery()

                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                CreaTabella(AnnoAU)

                GoTo RIPETI_LETTURA
            Else
                lblErrore.Text = ex1.Message
                par.OracleConn.Close()
                par.cmd.Dispose()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            lblErrore.Text = ex.Message
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try

    End Sub

    Private Sub CreaTabella(ByVal Anno As String)
        Dim OracleConn As Oracle.DataAccess.Client.OracleConnection
        Dim cmd As New Oracle.DataAccess.Client.OracleCommand()
        Try
            Dim strConn As String = ""
            strConn = par.StringaSiscom
            OracleConn = New Oracle.DataAccess.Client.OracleConnection(strConn)

            OracleConn.Open()
            cmd = OracleConn.CreateCommand()

            cmd.CommandText = "CREATE TABLE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE NUMBER NOT NULL,ISEE_ERP NUMBER,PERC_VAL_LOCATIVO NUMBER,INC_MAX_ISEE_ERP NUMBER,CANONE_MINIMO NUMBER,SOTTO_AREA VARCHAR2(5 BYTE))"
            Dim i As Integer = cmd.ExecuteNonQuery()

            cmd.CommandText = "GRANT ALTER, DELETE, INDEX, INSERT, REFERENCES, SELECT, UPDATE, ON COMMIT REFRESH, QUERY REWRITE, DEBUG, FLASHBACK ON  CANONE_SOPPORTABILE_AU_" & Anno & " TO SEPA"
            cmd.ExecuteNonQuery()

            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (1, 0, 0, 0, 20, 'A1')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (1, 0, 21, 14, 20, 'A2')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (1, 0, 25, 14, 20, 'A3')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (1, 0, 30, 14, 20, 'A4')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (1, 0, 36, 16, 20, 'A5')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (2, 0, 43,20, 70, 'B1')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (2, 0, 48, 20,70, 'B2')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (2, 0, 53, 20,70, 'B3')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (2, 0, 57, 20,70, 'B4')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (2, 0, 61, 20, 70, 'B5')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 66, 22, 120, 'C1')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 70, 22, 120, 'C2')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 74, 22, 120, 'C3')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 78, 22, 120, 'C4')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 82, 22, 120, 'C5')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 87, 22, 120, 'C6')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 92, 22, 120, 'C7')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 96, 22, 120, 'C8')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 100, 22, 120, 'C9')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 105, 22, 120, 'C10')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 0, 110, 22, 120, 'C11')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (3, 35000, 120, 24, 200, 'C12')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (4, 38000, 140, NULL, 250, 'D1')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (4, 43000, 160, NULL, 250, 'D2')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (4, 48000, 180, NULL, 250, 'D3')"
            cmd.ExecuteNonQuery()
            cmd.CommandText = "Insert into SISCOM_MI.CANONE_SOPPORTABILE_AU_" & Anno & " (AREE, ISEE_ERP, PERC_VAL_LOCATIVO, INC_MAX_ISEE_ERP, CANONE_MINIMO, SOTTO_AREA) Values (4, 9999999, 200, NULL, 250, 'D4')"
            cmd.ExecuteNonQuery()


            cmd.Dispose()
            OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        Catch ex As Exception
            cmd.Dispose()
            OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

        End Try
    End Sub

    Protected Sub btnSalva_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnSalva.Click
        Salva()
    End Sub

    Private Function Salva()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            par.cmd.CommandText = "UPDATE UTENZA_BANDI_PARAMETRI SET LIMITE_PENSIONE=" & par.VirgoleInPunti(txtPensione.Text) _
                                & ",ISTAT_1_PR=" & par.VirgoleInPunti(txtISTAT_1_PR.Text) & "," _
                                & "ISTAT_2_PR=" & par.VirgoleInPunti(txtISTAT_2_PR.Text) & "," _
                                & "ISTAT_1_AC=" & par.VirgoleInPunti(txtISTAT_1_AC.Text) & "," _
                                & "ISTAT_2_AC=" & par.VirgoleInPunti(txtISTAT_2_AC.Text) & "," _
                                & "ISTAT_1_PE=" & par.VirgoleInPunti(txtISTAT_1_PE.Text) & "," _
                                & "ISTAT_2_PE=" & par.VirgoleInPunti(txtISTAT_2_PE.Text) & "," _
                                & "ISTAT_1_DE=" & par.VirgoleInPunti(txtISTAT_1_DE.Text) & "," _
                                & "ISTAT_2_DE=" & par.VirgoleInPunti(txtISTAT_2_DE.Text) & "," _
                                & "ICI_1_2=" & par.VirgoleInPunti(txtICI_1_2.Text) & "," _
                                & "ICI_3_4=" & par.VirgoleInPunti(txtICI_3_4.Text) & "," _
                                & "ICI_5_6=" & par.VirgoleInPunti(txtICI_5_6.Text) & "," _
                                & "ICI_7=" & par.VirgoleInPunti(txtICI_7.Text) & " " _
                                & "WHERE ID_AU=" & IdBando
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox2.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox3.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox4.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox5.Text) & " WHERE SOTTO_AREA='A1'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox6.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox7.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox8.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox9.Text) & " WHERE SOTTO_AREA='A2'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox10.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox11.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox12.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox13.Text) & " WHERE SOTTO_AREA='A3'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox14.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox15.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox16.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox17.Text) & " WHERE SOTTO_AREA='A4'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox18.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox19.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox20.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox21.Text) & " WHERE SOTTO_AREA='A5'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox22.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox23.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox24.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox25.Text) & " WHERE SOTTO_AREA='B1'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox26.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox27.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox28.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox29.Text) & " WHERE SOTTO_AREA='B2'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox30.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox31.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox32.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox33.Text) & " WHERE SOTTO_AREA='B3'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox34.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox35.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox36.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox37.Text) & " WHERE SOTTO_AREA='B4'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox38.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox39.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox40.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox41.Text) & " WHERE SOTTO_AREA='B5'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox42.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox43.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox44.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox45.Text) & " WHERE SOTTO_AREA='C1'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox46.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox47.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox48.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox49.Text) & " WHERE SOTTO_AREA='C2'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox50.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox51.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox52.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox53.Text) & " WHERE SOTTO_AREA='C3'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox54.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox55.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox56.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox57.Text) & " WHERE SOTTO_AREA='C4'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox58.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox59.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox60.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox61.Text) & " WHERE SOTTO_AREA='C5'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox62.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox63.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox64.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox65.Text) & " WHERE SOTTO_AREA='C6'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox66.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox67.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox68.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox69.Text) & " WHERE SOTTO_AREA='C7'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox70.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox71.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox72.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox73.Text) & " WHERE SOTTO_AREA='C8'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox74.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox75.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox76.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox77.Text) & " WHERE SOTTO_AREA='C9'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox78.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox79.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox80.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox81.Text) & " WHERE SOTTO_AREA='C10'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox82.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox83.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox84.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox85.Text) & " WHERE SOTTO_AREA='C11'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox86.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox87.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox88.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox89.Text) & " WHERE SOTTO_AREA='C12'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox90.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox91.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox92.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox93.Text) & " WHERE SOTTO_AREA='D1'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox94.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox95.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox96.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox97.Text) & " WHERE SOTTO_AREA='D2'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET ISEE_ERP=" & par.VirgoleInPunti(TextBox98.Text) & ", PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox99.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox100.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox101.Text) & " WHERE SOTTO_AREA='D3'"
            par.cmd.ExecuteNonQuery()
            par.cmd.CommandText = "UPDATE SISCOM_MI.CANONE_SOPPORTABILE_AU_" & AnnoAU & " SET PERC_VAL_LOCATIVO=" & par.VirgoleInPunti(TextBox103.Text) & ", INC_MAX_ISEE_ERP=" & par.VirgoleInPunti(TextBox104.Text) & ", CANONE_MINIMO=" & par.VirgoleInPunti(TextBox105.Text) & " WHERE SOTTO_AREA='D4'"
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>alert('Operazione effettuata!');</script>")

        Catch ex As Exception
            par.myTrans.Rollback()
            par.OracleConn.Close()
            par.cmd.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)
        End Try
    End Function

    Protected Sub btnApplica_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnApplica.Click
        If txtAumento.Text <> "" Then
            If IsNumeric(txtAumento.Text) = True Then
                Try
                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "select * from utenza_bandi where id<>" & IdBando & " order by id desc"
                    Dim myReader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader.Read Then
                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='A1'"
                        Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox2.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='A2'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox6.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='A3'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox10.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='A4'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox14.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='A5'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox18.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='B1'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox22.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='B1'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox26.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='B3'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox30.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='B4'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox34.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='B5'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox38.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C1'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox42.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C2'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox46.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C3'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox50.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C4'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox54.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C5'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox58.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C6'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox62.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C7'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox66.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C8'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox70.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C9'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox74.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C10'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox78.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.CANONE_SOPPORTABILE_AU_" & myReader("ANNO_AU") & " WHERE SOTTO_AREA='C11'"
                        myReader1 = par.cmd.ExecuteReader()
                        If myReader1.Read Then
                            TextBox82.Text = Format(CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) + ((CDbl(par.IfNull(myReader1("ISEE_ERP"), 0)) * CDbl(par.PuntiInVirgole(txtAumento.Text))) / 100), "0")
                        End If
                        myReader1.Close()

                    End If
                    myReader.Close()

                    par.OracleConn.Close()
                    par.cmd.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


                Catch ex As Exception
                    par.OracleConn.Close()
                    par.cmd.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                    Response.Redirect("../Errore.aspx", False)
                End Try
            Else
                Response.Write("<script>alert('Specificare un valore percentuale!');</script>")
            End If
        Else
            Response.Write("<script>alert('Specificare un valore percentuale!');</script>")
        End If
    End Sub
End Class
